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
    public class CustAdiController : BaseController
    {
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        private IMessagingService _messageService;
        public CustAdiController()
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
            //get all changed columns

            var changeId = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_ADDITIONAL_INFORMATION" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId); //.Select(a=>a.PROPERTYNAME);
            CustomerADIModel model = (from c in _db.CDMA_ADDITIONAL_INFORMATION
                         where c.CUSTOMER_NO == querecord.CUST_ID
                         where c.AUTHORISED == "U"
                         select new CustomerADIModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             ANNUAL_SALARY_EXPECTED_INC = c.ANNUAL_SALARY_EXPECTED_INC,
                             FAX_NUMBER = c.FAX_NUMBER,
                             LastUpdatedby = c.LAST_MODIFIED_BY,
                             LastUpdatedDate = c.LAST_MODIFIED_DATE,
                             LastAuthdby = c.AUTHORISED_BY,
                             LastAuthDate = c.AUTHORISED_DATE,
                             ExceptionId = querecord.EXCEPTION_ID
                         }).FirstOrDefault();

            //var modelProperties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static);


            //List<string> props = new List<string>();
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

            int records = _db.CDMA_ADDITIONAL_INFORMATION.Count(o => o.CUSTOMER_NO == id);
            CustomerADIModel model = new CustomerADIModel();
            if(records > 1)
            {
                model = (from c in _db.CDMA_ADDITIONAL_INFORMATION
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "U"
                         select new CustomerADIModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             ANNUAL_SALARY_EXPECTED_INC = c.ANNUAL_SALARY_EXPECTED_INC,
                             FAX_NUMBER = c.FAX_NUMBER,
                         }).FirstOrDefault();

            }
            else if(records == 1)
            {
                model = (from c in _db.CDMA_ADDITIONAL_INFORMATION
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "A"
                         select new CustomerADIModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             ANNUAL_SALARY_EXPECTED_INC = c.ANNUAL_SALARY_EXPECTED_INC,
                             FAX_NUMBER = c.FAX_NUMBER,
                         }).FirstOrDefault();

            }
            PrepareModel(model);
            return View(model);
        }

        // POST: CustAdi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerADIModel adimodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            bool updateFlag = false;

            if (ModelState.IsValid)
            {
                CDMA_ADDITIONAL_INFORMATION originalObject = new CDMA_ADDITIONAL_INFORMATION();

                using (var db = new AppDbContext())
                {
                    int records = db.CDMA_ADDITIONAL_INFORMATION.Count(o => o.CUSTOMER_NO == adimodel.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records > 1)
                    {
                        updateFlag = true;
                        originalObject = _db.CDMA_ADDITIONAL_INFORMATION.Where(o => o.CUSTOMER_NO == adimodel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var entity = db.CDMA_ADDITIONAL_INFORMATION.FirstOrDefault(o => o.CUSTOMER_NO == adimodel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity != null)
                        {
                            entity.ANNUAL_SALARY_EXPECTED_INC = adimodel.ANNUAL_SALARY_EXPECTED_INC;
                            entity.FAX_NUMBER = adimodel.FAX_NUMBER;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";
                            db.CDMA_ADDITIONAL_INFORMATION.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), adimodel.CUSTOMER_NO, updateFlag, originalObject);
                            _messageService.LogEmailJob(identity.ProfileId, entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }                     
                    else if(records == 1)
                    {
                        updateFlag = false;
                        var entity = db.CDMA_ADDITIONAL_INFORMATION.FirstOrDefault(o => o.CUSTOMER_NO == adimodel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject = _db.CDMA_ADDITIONAL_INFORMATION.Where(o => o.CUSTOMER_NO == adimodel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();

                        if(originalObject != null)
                        {
                            entity.ANNUAL_SALARY_EXPECTED_INC = adimodel.ANNUAL_SALARY_EXPECTED_INC;
                            entity.FAX_NUMBER = adimodel.FAX_NUMBER;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";
                            db.CDMA_ADDITIONAL_INFORMATION.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), adimodel.CUSTOMER_NO, updateFlag, originalObject);

                            var newentity = new CDMA_ADDITIONAL_INFORMATION();

                            newentity.ANNUAL_SALARY_EXPECTED_INC = adimodel.ANNUAL_SALARY_EXPECTED_INC;
                            newentity.FAX_NUMBER = adimodel.FAX_NUMBER;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = adimodel.CUSTOMER_NO;
                            db.CDMA_ADDITIONAL_INFORMATION.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges();
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", adimodel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);

                            //string errorMessage = string.Format("Unauthorized record exists for record with Id:{0} .", nokmodel.CUSTOMER_NO);
                            //ModelState.AddModelError("", errorMessage);
                        }
                    }
                }

                SuccessNotification("ADI Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = adimodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(adimodel);
            return View(adimodel);
        }
        public ActionResult Create()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();

            CustomerADIModel model = new CustomerADIModel();
            PrepareModel(model);
            return View(model);
        }

        // POST: CustAdi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerADIModel adimodel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string ip_address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (ModelState.IsValid)
            {
                CDMA_ADDITIONAL_INFORMATION adi = new CDMA_ADDITIONAL_INFORMATION
                {
                    CUSTOMER_NO = adimodel.CUSTOMER_NO,
                    ANNUAL_SALARY_EXPECTED_INC = adimodel.ANNUAL_SALARY_EXPECTED_INC,
                    FAX_NUMBER = adimodel.FAX_NUMBER,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address,
                };
                _db.CDMA_ADDITIONAL_INFORMATION.Add(adi);
                _db.SaveChanges();


                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New ADI has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = adimodel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(adimodel);
            return View(adimodel);
        }

        [NonAction]
        protected virtual void PrepareModel(CustomerADIModel model)
        {
            //if (model == null)
              //  throw new ArgumentNullException("model");

            model.Salaries.Add(new SelectListItem
            {
                Text = "Less Than N50,000",
                Value = "Less Than N50,000"
            });
            model.Salaries.Add(new SelectListItem
            {
                Text = "N51,000 - N250,000",
                Value = "N51,000 - N250,000"
            });
            model.Salaries.Add(new SelectListItem
            {
                Text = "N251,000 - N500,000",
                Value = "N251,000 - N500,000"
            });
            model.Salaries.Add(new SelectListItem
            {
                Text = "N501,000 - Less than N1 million",
                Value = "N501,000 - Less than N1 million"
            });
            model.Salaries.Add(new SelectListItem
            {
                Text = "N1 million - Less than N5 million",
                Value = "N1 million - Less than N5 million"
            });
            model.Salaries.Add(new SelectListItem
            {
                Text = "N5 million - Less than N10 million",
                Value = "N5 million - Less than N10 million"
            });
            model.Salaries.Add(new SelectListItem
            {
                Text = "N10 million - Less than N20 million",
                Value = "N10 million - Less than N20 million"
            });
            model.Salaries.Add(new SelectListItem
            {
                Text = "N20 million and above",
                Value = "N20 million and above"
            });
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(CustomerADIModel adimodel, bool disapproveRecord)
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

                    _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), adimodel.AuthoriserRemarks);
                    SuccessNotification("ADI Not Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, adimodel.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(adimodel.LastUpdatedby));
                }

                else
                {
                    _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(), identity.ProfileId);
                    SuccessNotification("ADI Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, adimodel.CUSTOMER_NO, MessageJobEnum.MailType.Authorize, Convert.ToInt32(adimodel.LastUpdatedby));
                }

                //using (var db = new AppDbContext())
                //{
                //    var entity = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.FirstOrDefault(o => o.CUSTOMER_NO == nokmodel.CUSTOMER_NO);
                //    if (entity == null)
                //    {
                //        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", nokmodel.CUSTOMER_NO);
                //        ModelState.AddModelError("", errorMessage);
                //    }
                //    else
                //    {                       
                //        entity.AUTHORISED = "A";
                //        db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Attach(entity);
                //        db.Entry(entity).State = EntityState.Modified;
                //        db.SaveChanges();

                //    }
                //}


                return RedirectToAction("AuthList", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(adimodel);
            return View(adimodel);
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove_(CustomerADIModel adimodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                _dqQueService.DisApproveExceptionQueItems(adimodel.ExceptionId.ToString(), adimodel.AuthoriserRemarks);

                SuccessNotification("ADI Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = adimodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "CustNok");
                //return RedirectToAction("Index");
            }
            PrepareModel(adimodel);
            return View(adimodel);
        }

        #region scaffolded
        // GET: CustAdi
        public ActionResult Index()
        {
            return View(_db.CDMA_ADDITIONAL_INFORMATION.ToList());
        }

        // GET: CustAdi/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_ADDITIONAL_INFORMATION cDMA_ADDITIONAL_INFORMATION = _db.CDMA_ADDITIONAL_INFORMATION.Find(id);
            if (cDMA_ADDITIONAL_INFORMATION == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_ADDITIONAL_INFORMATION);
        }

        // GET: CustAdi/Create
        public ActionResult Create_()
        {
            return View();
        }

        // POST: CustAdi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_1([Bind(Include = "CUSTOMER_NO,ANNUAL_SALARY_EXPECTED_INC,FAX_NUMBER,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_ADDITIONAL_INFORMATION cDMA_ADDITIONAL_INFORMATION)
        {
            if (ModelState.IsValid)
            {
                _db.CDMA_ADDITIONAL_INFORMATION.Add(cDMA_ADDITIONAL_INFORMATION);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_ADDITIONAL_INFORMATION);
        }

        // GET: CustAdi/Edit/5
        public ActionResult Edit_(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_ADDITIONAL_INFORMATION cDMA_ADDITIONAL_INFORMATION = _db.CDMA_ADDITIONAL_INFORMATION.Find(id);
            if (cDMA_ADDITIONAL_INFORMATION == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_ADDITIONAL_INFORMATION);
        }

        // POST: CustAdi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_1([Bind(Include = "CUSTOMER_NO,ANNUAL_SALARY_EXPECTED_INC,FAX_NUMBER,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_ADDITIONAL_INFORMATION cDMA_ADDITIONAL_INFORMATION)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cDMA_ADDITIONAL_INFORMATION).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_ADDITIONAL_INFORMATION);
        }

        // GET: CustAdi/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_ADDITIONAL_INFORMATION cDMA_ADDITIONAL_INFORMATION = _db.CDMA_ADDITIONAL_INFORMATION.Find(id);
            if (cDMA_ADDITIONAL_INFORMATION == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_ADDITIONAL_INFORMATION);
        }

        // POST: CustAdi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_ADDITIONAL_INFORMATION cDMA_ADDITIONAL_INFORMATION = _db.CDMA_ADDITIONAL_INFORMATION.Find(id);
            _db.CDMA_ADDITIONAL_INFORMATION.Remove(cDMA_ADDITIONAL_INFORMATION);
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
