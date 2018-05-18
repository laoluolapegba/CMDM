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
    public class CustJuratController : BaseController
    {
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        private IMessagingService _messageService;

        public CustJuratController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new DqQueService();
            _messageService = new MessagingService();
        }

        public ActionResult Authorize(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            var changeId = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_JURAT" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId); //.Select(a=>a.PROPERTYNAME);

            CustomerJuratModel model = (from c in _db.CDMA_JURAT
                         where c.CUSTOMER_NO == querecord.CUST_ID
                         where c.AUTHORISED == "U"
                         select new CustomerJuratModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             DATE_OF_OATH = c.DATE_OF_OATH,
                             NAME_OF_INTERPRETER = c.NAME_OF_INTERPRETER,
                             ADDRESS_OF_INTERPRETER = c.ADDRESS_OF_INTERPRETER,
                             TELEPHONE_NO = c.TELEPHONE_NO,
                             LANGUAGE_OF_INTERPRETATION = c.LANGUAGE_OF_INTERPRETATION,
                             LastUpdatedby = c.LAST_MODIFIED_BY,
                             LastUpdatedDate = c.LAST_MODIFIED_DATE,
                             LastAuthdby = c.AUTHORISED_BY,
                             LastAuthDate = c.AUTHORISED_DATE,
                             ExceptionId = querecord.EXCEPTION_ID
                         }).FirstOrDefault();

            if(model != null)
            {
                foreach (var item in model.GetType().GetProperties()) 
                {
                    foreach (var item2 in changedSet)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }
                    //props.Add(item.Name);

                }
            }
            //var matchItems = props.Intersect(changedSet);
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
            int records = _db.CDMA_JURAT.Count(o => o.CUSTOMER_NO == id);

            CustomerJuratModel model = new CustomerJuratModel();
            if(records > 1)
            {
                model = (from c in _db.CDMA_JURAT
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "U"
                         select new CustomerJuratModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             DATE_OF_OATH = c.DATE_OF_OATH,
                             NAME_OF_INTERPRETER = c.NAME_OF_INTERPRETER,
                             ADDRESS_OF_INTERPRETER = c.ADDRESS_OF_INTERPRETER,
                             TELEPHONE_NO = c.TELEPHONE_NO,
                             LANGUAGE_OF_INTERPRETATION = c.LANGUAGE_OF_INTERPRETATION,
                         }).FirstOrDefault();

            }
            else if(records == 1)
            {
                model = (from c in _db.CDMA_JURAT
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "A"
                         select new CustomerJuratModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             DATE_OF_OATH = c.DATE_OF_OATH,
                             NAME_OF_INTERPRETER = c.NAME_OF_INTERPRETER,
                             ADDRESS_OF_INTERPRETER = c.ADDRESS_OF_INTERPRETER,
                             TELEPHONE_NO = c.TELEPHONE_NO,
                             LANGUAGE_OF_INTERPRETATION = c.LANGUAGE_OF_INTERPRETATION,
                         }).FirstOrDefault();
            }

            PrepareModel(model);
            return View(model);
        }

        // POST: CustJurat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerJuratModel cjmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            bool updateFlag = false;
            if (ModelState.IsValid)
            {
                CDMA_JURAT originalObject = new CDMA_JURAT();
                using (var db = new AppDbContext())
                {
                    int records = db.CDMA_JURAT.Count(o => o.CUSTOMER_NO == cjmodel.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records > 1)
                    {
                        updateFlag = true;
                        originalObject = _db.CDMA_JURAT.Where(o => o.CUSTOMER_NO == cjmodel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var entity = db.CDMA_JURAT.FirstOrDefault(o => o.CUSTOMER_NO == cjmodel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity != null)
                        {
                            entity.DATE_OF_OATH = cjmodel.DATE_OF_OATH;
                            entity.NAME_OF_INTERPRETER = cjmodel.NAME_OF_INTERPRETER;
                            entity.ADDRESS_OF_INTERPRETER = cjmodel.ADDRESS_OF_INTERPRETER;
                            entity.TELEPHONE_NO = cjmodel.TELEPHONE_NO;
                            entity.LANGUAGE_OF_INTERPRETATION = cjmodel.LANGUAGE_OF_INTERPRETATION;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";
                            db.CDMA_JURAT.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), cjmodel.CUSTOMER_NO, updateFlag, originalObject);
                            _messageService.LogEmailJob(identity.ProfileId, entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if (records == 1)
                    {
                        updateFlag = false;
                        var entity = db.CDMA_JURAT.FirstOrDefault(o => o.CUSTOMER_NO == cjmodel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject = _db.CDMA_JURAT.Where(o => o.CUSTOMER_NO == cjmodel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject != null)
                        {
                            entity.DATE_OF_OATH = cjmodel.DATE_OF_OATH;
                            entity.NAME_OF_INTERPRETER = cjmodel.NAME_OF_INTERPRETER;
                            entity.ADDRESS_OF_INTERPRETER = cjmodel.ADDRESS_OF_INTERPRETER;
                            entity.TELEPHONE_NO = cjmodel.TELEPHONE_NO;
                            entity.LANGUAGE_OF_INTERPRETATION = cjmodel.LANGUAGE_OF_INTERPRETATION; 
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;

                            db.CDMA_JURAT.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), cjmodel.CUSTOMER_NO, updateFlag, originalObject);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_JURAT();
                            newentity.DATE_OF_OATH = cjmodel.DATE_OF_OATH;
                            newentity.NAME_OF_INTERPRETER = cjmodel.NAME_OF_INTERPRETER;
                            newentity.ADDRESS_OF_INTERPRETER = cjmodel.ADDRESS_OF_INTERPRETER;
                            newentity.TELEPHONE_NO = cjmodel.TELEPHONE_NO;
                            newentity.LANGUAGE_OF_INTERPRETATION = cjmodel.LANGUAGE_OF_INTERPRETATION;
                            newentity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            newentity.LAST_MODIFIED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = cjmodel.CUSTOMER_NO;
                            db.CDMA_JURAT.Add(newentity);


                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", cjmodel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                }

                SuccessNotification("JURAT Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = cjmodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
            }
            PrepareModel(cjmodel);
            return View(cjmodel);
        }
        public ActionResult Create()
        {
            CustomerJuratModel model = new CustomerJuratModel();
            PrepareModel(model);
            return View(model);
        }

        // POST: CustJurat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerJuratModel cjmodel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string ip_address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (ModelState.IsValid)
            {
                CDMA_JURAT jurat = new CDMA_JURAT
                {
                    CUSTOMER_NO = cjmodel.CUSTOMER_NO,
                    DATE_OF_OATH = cjmodel.DATE_OF_OATH,
                    NAME_OF_INTERPRETER = cjmodel.NAME_OF_INTERPRETER,
                    ADDRESS_OF_INTERPRETER = cjmodel.ADDRESS_OF_INTERPRETER,
                    TELEPHONE_NO = cjmodel.TELEPHONE_NO,
                    LANGUAGE_OF_INTERPRETATION = cjmodel.LANGUAGE_OF_INTERPRETATION,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                _db.CDMA_JURAT.Add(jurat);
                _db.SaveChanges();


                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New JURAT has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = cjmodel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(cjmodel);
            return View(cjmodel);
        }

        [NonAction]
        protected virtual void PrepareModel(CustomerJuratModel model)
        {
            //if (model == null)
            //    throw new ArgumentNullException("model");

            if (model == null)
                throw new ArgumentNullException("model");
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(CustomerJuratModel cjmodel, bool disapproveRecord)
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

                    _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), cjmodel.AuthoriserRemarks);
                    SuccessNotification("JURAT Not Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, cjmodel.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(cjmodel.LastUpdatedby));
                }

                else
                {
                    _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(), identity.ProfileId);
                    SuccessNotification("JURAT Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, cjmodel.CUSTOMER_NO, MessageJobEnum.MailType.Authorize, Convert.ToInt32(cjmodel.LastUpdatedby));
                }
                
                return RedirectToAction("AuthList", "DQQue");
            }
            PrepareModel(cjmodel);
            return View(cjmodel);
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove_(CustomerJuratModel cjmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                _dqQueService.DisApproveExceptionQueItems(cjmodel.ExceptionId.ToString(), cjmodel.AuthoriserRemarks);

                SuccessNotification("JURAT Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = cjmodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "CustJurat");
                //return RedirectToAction("Index");
            }
            PrepareModel(cjmodel);
            return View(cjmodel);
        }

        #region scaffolded
        // GET: CustJurat
        public ActionResult Index()
        {
            return View(_db.CDMA_JURAT.ToList());
        }

        // GET: CustJurat/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_JURAT cDMA_JURAT = _db.CDMA_JURAT.Find(id);
            if (cDMA_JURAT == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_JURAT);
        }

        // GET: CustJurat/Create
        public ActionResult Create_()
        {
            return View();
        }

        // POST: CustJurat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_1([Bind(Include = "CUSTOMER_NO,DATE_OF_OATH,NAME_OF_INTERPRETER,ADDRESS_OF_INTERPRETER,TELEPHONE_NO,LANGUAGE_OF_INTERPRETATION,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_JURAT cDMA_JURAT)
        {
            if (ModelState.IsValid)
            {
                _db.CDMA_JURAT.Add(cDMA_JURAT);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_JURAT);
        }

        // GET: CustJurat/Edit/5
        public ActionResult Edit_(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_JURAT cDMA_JURAT = _db.CDMA_JURAT.Find(id);
            if (cDMA_JURAT == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_JURAT);
        }

        // POST: CustJurat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_1([Bind(Include = "CUSTOMER_NO,DATE_OF_OATH,NAME_OF_INTERPRETER,ADDRESS_OF_INTERPRETER,TELEPHONE_NO,LANGUAGE_OF_INTERPRETATION,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_JURAT cDMA_JURAT)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cDMA_JURAT).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_JURAT);
        }

        // GET: CustJurat/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_JURAT cDMA_JURAT = _db.CDMA_JURAT.Find(id);
            if (cDMA_JURAT == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_JURAT);
        }

        // POST: CustJurat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_JURAT cDMA_JURAT = _db.CDMA_JURAT.Find(id);
            _db.CDMA_JURAT.Remove(cDMA_JURAT);
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
        #endregion scaffolded
    }
}
