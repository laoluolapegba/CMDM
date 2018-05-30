using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.Customer;
using CMdm.UI.Web.Models.Customer;
using CMdm.Framework.Controllers;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Services.DqQue;
using CMdm.Services.Messaging;
using CMdm.Entities.ViewModels;

namespace CMdm.UI.Web.Controllers
{
    public class CustForeignerController : BaseController
    {
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        private IMessagingService _messageService;

        public CustForeignerController()
        {
            _dqQueService = new DqQueService();
            _messageService = new MessagingService();
        }

        public ActionResult Authorize(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            CustomerForeignerModel model = new CustomerForeignerModel();

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }
            //get all changed columns

            var changeId = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_FOREIGN_DETAILS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId); //.Select(a=>a.PROPERTYNAME);

            model = (from c in _db.CDMA_FOREIGN_DETAILS
                     where c.CUSTOMER_NO == querecord.CUST_ID
                     where c.AUTHORISED == "U"
                     select new CustomerForeignerModel
                    {
                        CUSTOMER_NO = c.CUSTOMER_NO,
                        FOREIGNER = c.FOREIGNER,
                        PASSPORT_RESIDENCE_PERMIT = c.PASSPORT_RESIDENCE_PERMIT,
                        PERMIT_ISSUE_DATE = c.PERMIT_ISSUE_DATE,
                        PERMIT_EXPIRY_DATE = c.PERMIT_EXPIRY_DATE,
                        FOREIGN_ADDRESS = c.FOREIGN_ADDRESS,
                        CITY = c.CITY,
                        FOREIGN_TEL_NUMBER = c.FOREIGN_TEL_NUMBER,
                        ZIP_POSTAL_CODE = c.ZIP_POSTAL_CODE,
                        PURPOSE_OF_ACCOUNT = c.PURPOSE_OF_ACCOUNT,
                        COUNTRY = c.COUNTRY,
                        MULTIPLE_CITEZENSHIP = c.MULTIPLE_CITEZENSHIP,
                        LastUpdatedby = c.LAST_MODIFIED_BY,
                        LastUpdatedDate = c.LAST_MODIFIED_DATE,
                        LastAuthdby = c.AUTHORISED_BY,
                        LastAuthDate = c.AUTHORISED_DATE,
                        ExceptionId = querecord.EXCEPTION_ID
                    }).FirstOrDefault();

            if(model != null)
            {
                foreach (var item in model.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }
                }
            }
            model.ReadOnlyForm = "True";
            
            PrepareModel(model);
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }

            int records = _db.CDMA_FOREIGN_DETAILS.Count(o => o.CUSTOMER_NO == id);
            CustomerForeignerModel model = new CustomerForeignerModel();

            if (records > 1)
            {
                model = (from c in _db.CDMA_FOREIGN_DETAILS
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "U"
                         select new CustomerForeignerModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             FOREIGNER = c.FOREIGNER,
                             PASSPORT_RESIDENCE_PERMIT = c.PASSPORT_RESIDENCE_PERMIT,
                             PERMIT_ISSUE_DATE = c.PERMIT_ISSUE_DATE,
                             PERMIT_EXPIRY_DATE = c.PERMIT_EXPIRY_DATE,
                             FOREIGN_ADDRESS = c.FOREIGN_ADDRESS,
                             CITY = c.CITY,
                             FOREIGN_TEL_NUMBER = c.FOREIGN_TEL_NUMBER,
                             ZIP_POSTAL_CODE = c.ZIP_POSTAL_CODE,
                             PURPOSE_OF_ACCOUNT = c.PURPOSE_OF_ACCOUNT,
                             COUNTRY = c.COUNTRY,
                             MULTIPLE_CITEZENSHIP = c.MULTIPLE_CITEZENSHIP
                         }).FirstOrDefault();
            }
            else if (records == 1)
            {
                model = (from c in _db.CDMA_FOREIGN_DETAILS
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "A"
                         select new CustomerForeignerModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             FOREIGNER = c.FOREIGNER,
                             PASSPORT_RESIDENCE_PERMIT = c.PASSPORT_RESIDENCE_PERMIT,
                             PERMIT_ISSUE_DATE = c.PERMIT_ISSUE_DATE,
                             PERMIT_EXPIRY_DATE = c.PERMIT_EXPIRY_DATE,
                             FOREIGN_ADDRESS = c.FOREIGN_ADDRESS,
                             CITY = c.CITY,
                             FOREIGN_TEL_NUMBER = c.FOREIGN_TEL_NUMBER,
                             ZIP_POSTAL_CODE = c.ZIP_POSTAL_CODE,
                             PURPOSE_OF_ACCOUNT = c.PURPOSE_OF_ACCOUNT,
                             COUNTRY = c.COUNTRY,
                             MULTIPLE_CITEZENSHIP = c.MULTIPLE_CITEZENSHIP
                         }).FirstOrDefault();
            }

            PrepareModel(model);
            return View(model);
        }

        // POST: CustForeigner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerForeignerModel formodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            bool updateFlag = false;
            if (ModelState.IsValid)
            {
                CDMA_FOREIGN_DETAILS originalObject = new CDMA_FOREIGN_DETAILS();
                using (var db = new AppDbContext())
                {
                    int records = db.CDMA_FOREIGN_DETAILS.Count(o => o.CUSTOMER_NO == formodel.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records > 1)
                    {
                        updateFlag = true;
                        originalObject = _db.CDMA_FOREIGN_DETAILS.Where(o => o.CUSTOMER_NO == formodel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var entity = db.CDMA_FOREIGN_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == formodel.CUSTOMER_NO && o.AUTHORISED == "U");
                        if (entity != null)
                        {
                            entity.FOREIGNER = formodel.FOREIGNER;
                            entity.PASSPORT_RESIDENCE_PERMIT = formodel.PASSPORT_RESIDENCE_PERMIT;
                            entity.COUNTRY = formodel.COUNTRY;
                            entity.PERMIT_ISSUE_DATE = formodel.PERMIT_ISSUE_DATE;
                            entity.PERMIT_EXPIRY_DATE = formodel.PERMIT_EXPIRY_DATE;
                            entity.FOREIGN_ADDRESS = formodel.FOREIGN_ADDRESS;
                            entity.CITY = formodel.CITY;
                            entity.COUNTRY = formodel.COUNTRY;
                            entity.ZIP_POSTAL_CODE = formodel.ZIP_POSTAL_CODE;
                            entity.FOREIGN_TEL_NUMBER = formodel.FOREIGN_TEL_NUMBER;
                            entity.PURPOSE_OF_ACCOUNT = formodel.PURPOSE_OF_ACCOUNT;
                            entity.MULTIPLE_CITEZENSHIP = formodel.MULTIPLE_CITEZENSHIP;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;

                            db.CDMA_FOREIGN_DETAILS.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), formodel.CUSTOMER_NO, updateFlag, originalObject);
                            _messageService.LogEmailJob(identity.ProfileId, entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if (records == 1)
                    {
                        updateFlag = false;
                        var entity = db.CDMA_FOREIGN_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == formodel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject = _db.CDMA_FOREIGN_DETAILS.Where(o => o.CUSTOMER_NO == formodel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject != null)
                        {
                            entity.FOREIGNER = formodel.FOREIGNER;
                            entity.PASSPORT_RESIDENCE_PERMIT = formodel.PASSPORT_RESIDENCE_PERMIT;
                            entity.COUNTRY = formodel.COUNTRY;
                            entity.PERMIT_ISSUE_DATE = formodel.PERMIT_ISSUE_DATE;
                            entity.PERMIT_EXPIRY_DATE = formodel.PERMIT_EXPIRY_DATE;
                            entity.FOREIGN_ADDRESS = formodel.FOREIGN_ADDRESS;
                            entity.CITY = formodel.CITY;
                            entity.COUNTRY = formodel.COUNTRY;
                            entity.ZIP_POSTAL_CODE = formodel.ZIP_POSTAL_CODE;
                            entity.FOREIGN_TEL_NUMBER = formodel.FOREIGN_TEL_NUMBER;
                            entity.PURPOSE_OF_ACCOUNT = formodel.PURPOSE_OF_ACCOUNT;
                            entity.MULTIPLE_CITEZENSHIP = formodel.MULTIPLE_CITEZENSHIP;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;


                            db.CDMA_FOREIGN_DETAILS.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), formodel.CUSTOMER_NO, updateFlag, originalObject);

                            var newentity = new CDMA_FOREIGN_DETAILS();
                            newentity.FOREIGNER = formodel.FOREIGNER;
                            newentity.PASSPORT_RESIDENCE_PERMIT = formodel.PASSPORT_RESIDENCE_PERMIT;
                            newentity.COUNTRY = formodel.COUNTRY;
                            newentity.PERMIT_ISSUE_DATE = formodel.PERMIT_ISSUE_DATE;
                            newentity.PERMIT_EXPIRY_DATE = formodel.PERMIT_EXPIRY_DATE;
                            newentity.FOREIGN_ADDRESS = formodel.FOREIGN_ADDRESS;
                            newentity.CITY = formodel.CITY;
                            newentity.COUNTRY = formodel.COUNTRY;
                            newentity.ZIP_POSTAL_CODE = formodel.ZIP_POSTAL_CODE;
                            newentity.FOREIGN_TEL_NUMBER = formodel.FOREIGN_TEL_NUMBER;
                            newentity.PURPOSE_OF_ACCOUNT = formodel.PURPOSE_OF_ACCOUNT;
                            newentity.MULTIPLE_CITEZENSHIP = formodel.MULTIPLE_CITEZENSHIP;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = formodel.CUSTOMER_NO;
                            
                            db.CDMA_FOREIGN_DETAILS.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", formodel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }

                    }
                }
                SuccessNotification("FORD Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = formodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(formodel);
            return View(formodel);
        }
        public ActionResult Create()
        {
            CustomerForeignerModel model = new CustomerForeignerModel();
            PrepareModel(model);
            return View(model);
        }

        // POST: CustForeigner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerForeignerModel formodel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string ip_address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (ModelState.IsValid)
            {
                CDMA_FOREIGN_DETAILS ford = new CDMA_FOREIGN_DETAILS
                {
                    CUSTOMER_NO = formodel.CUSTOMER_NO,
                    FOREIGNER = formodel.FOREIGNER,
                    PASSPORT_RESIDENCE_PERMIT = formodel.PASSPORT_RESIDENCE_PERMIT,
                    PERMIT_ISSUE_DATE = formodel.PERMIT_ISSUE_DATE,
                    PERMIT_EXPIRY_DATE = formodel.PERMIT_EXPIRY_DATE,
                    FOREIGN_ADDRESS = formodel.FOREIGN_ADDRESS,
                    CITY = formodel.CITY,
                    COUNTRY = formodel.COUNTRY,
                    ZIP_POSTAL_CODE = formodel.ZIP_POSTAL_CODE,
                    FOREIGN_TEL_NUMBER = formodel.FOREIGN_TEL_NUMBER,
                    PURPOSE_OF_ACCOUNT = formodel.PURPOSE_OF_ACCOUNT,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address,
                    MULTIPLE_CITEZENSHIP = formodel.MULTIPLE_CITEZENSHIP
            };
                _db.CDMA_FOREIGN_DETAILS.Add(ford);
                _db.SaveChanges();


                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New FORD has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = formodel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(formodel);
            return View(formodel);
        }

        [NonAction]
        protected virtual void PrepareModel(CustomerForeignerModel model)
        {
            //if (model == null)
            //    throw new ArgumentNullException("model");

            if (model == null)
                throw new ArgumentNullException("model");
            model.Foreigners.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });
            model.Foreigners.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });
            model.MultipleCitezenships.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });
            model.MultipleCitezenships.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });

            model.Countries = new SelectList(_db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
        }


        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(CustomerForeignerModel formodel, bool disapproveRecord)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {

                var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;

                int exceptionId = 0;
                if (routeValues.ContainsKey("id"))
                    exceptionId = int.Parse((string)routeValues["id"]);
                if (disapproveRecord)
                {
                    _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), formodel.AuthoriserRemarks);
                    SuccessNotification("FORD Not Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, formodel.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(formodel.LastUpdatedby));
                }
                else
                {
                    _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(), identity.ProfileId);
                    SuccessNotification("FORD Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, formodel.CUSTOMER_NO, MessageJobEnum.MailType.Authorize, Convert.ToInt32(formodel.LastUpdatedby));
                }
            }
            PrepareModel(formodel);
            return View(formodel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove(CustomerForeignerModel formodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_FOREIGN_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == formodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", formodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.AUTHORISED = "N";
                        db.CDMA_FOREIGN_DETAILS.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                SuccessNotification("FORD Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = formodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "CustForeigner");
                //return RedirectToAction("Index");
            }
            PrepareModel(formodel);
            return View(formodel);
        }


        #region scafolded
        // GET: CustForeigner
        public ActionResult Index()
        {
            return View(_db.CDMA_FOREIGN_DETAILS.ToList());
        }

        // GET: CustForeigner/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_FOREIGN_DETAILS cDMA_FOREIGN_DETAILS = _db.CDMA_FOREIGN_DETAILS.Find(id);
            if (cDMA_FOREIGN_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_FOREIGN_DETAILS);
        }

        // GET: CustForeigner/Create
        public ActionResult Create_()
        {
            return View();
        }

        // POST: CustForeigner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_1([Bind(Include = "CUSTOMER_NO,FOREIGNER,PASSPORT_RESIDENCE_PERMIT,PERMIT_ISSUE_DATE,PERMIT_EXPIRY_DATE,FOREIGN_ADDRESS,CITY,COUNTRY,ZIP_POSTAL_CODE,FOREIGN_TEL_NUMBER,PURPOSE_OF_ACCOUNT,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS,MULTIPLE_CITEZENSHIP")] CDMA_FOREIGN_DETAILS cDMA_FOREIGN_DETAILS)
        {
            if (ModelState.IsValid)
            {
                _db.CDMA_FOREIGN_DETAILS.Add(cDMA_FOREIGN_DETAILS);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_FOREIGN_DETAILS);
        }

        // GET: CustForeigner/Edit/5
        public ActionResult Edit_(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_FOREIGN_DETAILS cDMA_FOREIGN_DETAILS = _db.CDMA_FOREIGN_DETAILS.Find(id);
            if (cDMA_FOREIGN_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_FOREIGN_DETAILS);
        }

        // POST: CustForeigner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_1([Bind(Include = "CUSTOMER_NO,FOREIGNER,PASSPORT_RESIDENCE_PERMIT,PERMIT_ISSUE_DATE,PERMIT_EXPIRY_DATE,FOREIGN_ADDRESS,CITY,COUNTRY,ZIP_POSTAL_CODE,FOREIGN_TEL_NUMBER,PURPOSE_OF_ACCOUNT,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS,MULTIPLE_CITEZENSHIP")] CDMA_FOREIGN_DETAILS cDMA_FOREIGN_DETAILS)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cDMA_FOREIGN_DETAILS).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_FOREIGN_DETAILS);
        }

        // GET: CustForeigner/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_FOREIGN_DETAILS cDMA_FOREIGN_DETAILS = _db.CDMA_FOREIGN_DETAILS.Find(id);
            if (cDMA_FOREIGN_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_FOREIGN_DETAILS);
        }

        // POST: CustForeigner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_FOREIGN_DETAILS cDMA_FOREIGN_DETAILS = _db.CDMA_FOREIGN_DETAILS.Find(id);
            _db.CDMA_FOREIGN_DETAILS.Remove(cDMA_FOREIGN_DETAILS);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
