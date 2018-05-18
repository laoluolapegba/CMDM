using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
    public class CustIncomeController : BaseController
    {
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        private IMessagingService _messageService;

        public CustIncomeController()
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

            CustomerIncomeModel model = new CustomerIncomeModel();

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }
            //get all changed columns

            var changeId = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_CHANGE_LOGS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId); //.Select(a=>a.PROPERTYNAME);
            
            model = (from c in _db.CDMA_CUSTOMER_INCOME
                        where c.CUSTOMER_NO == querecord.CUST_ID
                        where c.AUTHORISED == "U"
                        select new CustomerIncomeModel
                        {
                            CUSTOMER_NO = c.CUSTOMER_NO,
                            INCOME_BAND = c.INCOME_BAND,
                            INITIAL_DEPOSIT = c.INITIAL_DEPOSIT,
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

            int records = _db.CDMA_CUSTOMER_INCOME.Count(o => o.CUSTOMER_NO == id);
            CustomerIncomeModel model = new CustomerIncomeModel();
            if (records > 1)
            {
                model = (from c in _db.CDMA_CUSTOMER_INCOME
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "U"
                         select new CustomerIncomeModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             INCOME_BAND = c.INCOME_BAND,
                             INITIAL_DEPOSIT = c.INITIAL_DEPOSIT
                         }).FirstOrDefault();
            }
            else if (records == 1)
            {
                model = (from c in _db.CDMA_CUSTOMER_INCOME
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "A"
                         select new CustomerIncomeModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             INCOME_BAND = c.INCOME_BAND,
                             INITIAL_DEPOSIT = c.INITIAL_DEPOSIT
                         }).FirstOrDefault();
            }

            PrepareModel(model);
            return View(model);
        }

        // POST: CustIncome/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerIncomeModel cimodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            bool updateFlag = false;
            if (ModelState.IsValid)
            {
                CDMA_CUSTOMER_INCOME originalObject = new CDMA_CUSTOMER_INCOME();
                using (var db = new AppDbContext())
                {

                    int records = db.CDMA_CUSTOMER_INCOME.Count(o => o.CUSTOMER_NO == cimodel.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records > 1)
                    {
                        updateFlag = true;
                        originalObject = _db.CDMA_CUSTOMER_INCOME.Where(o => o.CUSTOMER_NO == cimodel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var entity = db.CDMA_CUSTOMER_INCOME.FirstOrDefault(o => o.CUSTOMER_NO == cimodel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity != null)
                        {
                            entity.INCOME_BAND = cimodel.INCOME_BAND;
                            entity.INITIAL_DEPOSIT = cimodel.INITIAL_DEPOSIT;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;

                            //entity.AUTHORISED = "U";
                            db.CDMA_CUSTOMER_INCOME.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), cimodel.CUSTOMER_NO, updateFlag, originalObject);
                            _messageService.LogEmailJob(identity.ProfileId, entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                        }
                        
                    }
                    else if (records == 1)
                    {
                        updateFlag = false;
                        var entity = db.CDMA_CUSTOMER_INCOME.FirstOrDefault(o => o.CUSTOMER_NO == cimodel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject = _db.CDMA_CUSTOMER_INCOME.Where(o => o.CUSTOMER_NO == cimodel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject != null)
                        {
                            entity.INCOME_BAND = cimodel.INCOME_BAND;
                            entity.INITIAL_DEPOSIT = cimodel.INITIAL_DEPOSIT;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            db.CDMA_CUSTOMER_INCOME.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), cimodel.CUSTOMER_NO, updateFlag, originalObject);  //track the audit


                            var newentity = new CDMA_CUSTOMER_INCOME();
                            newentity.INCOME_BAND = cimodel.INCOME_BAND;
                            newentity.INITIAL_DEPOSIT = cimodel.INITIAL_DEPOSIT;
                            newentity.AUTHORISED = "U";
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.CUSTOMER_NO = cimodel.CUSTOMER_NO;
                            db.CDMA_CUSTOMER_INCOME.Add(newentity);
                            db.SaveChanges();
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", cimodel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                }

                    SuccessNotification("CUSTI Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = cimodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(cimodel);
            return View(cimodel);
        }
        public ActionResult Create()
        {
            CustomerIncomeModel model = new CustomerIncomeModel();
            PrepareModel(model);
            return View(model);
        }

        // POST: CustIncome/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerIncomeModel cimodel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string ip_address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (ModelState.IsValid)
            {
                CDMA_CUSTOMER_INCOME custi = new CDMA_CUSTOMER_INCOME
                {
                    CUSTOMER_NO = cimodel.CUSTOMER_NO,
                    INCOME_BAND = cimodel.INCOME_BAND,
                    INITIAL_DEPOSIT = cimodel.INITIAL_DEPOSIT,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address,
                };
                _db.CDMA_CUSTOMER_INCOME.Add(custi);
                _db.SaveChanges();


                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New CUSTI has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = cimodel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(cimodel);
            return View(cimodel);
        }

        [NonAction]
        protected virtual void PrepareModel(CustomerIncomeModel model)
        {
            //if (model == null)
            //    throw new ArgumentNullException("model");

            if (model == null)
                throw new ArgumentNullException("model");
            model.IncomeBand.Add(new SelectListItem
            {
                Text = "0 - 250 Thousand",
                Value = "0 - 250 Thousand"
            });
            model.IncomeBand.Add(new SelectListItem
            {
                Text = "250 - 500 Thousand",
                Value = "250 - 500 Thousand"
            });
            model.InitialDeposit.Add(new SelectListItem
            {
                Text = "Below 250,000",
                Value = "Below 250,000"
            });
            model.InitialDeposit.Add(new SelectListItem
            {
                Text = "Between 250,000 to 500,000",
                Value = "Between 250,000 to 500,000"
            });
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(CustomerIncomeModel cimodel, bool disapproveRecord)
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

                    _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), cimodel.AuthoriserRemarks);
                    SuccessNotification("CustI Not Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, cimodel.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(cimodel.LastUpdatedby));
                }

                else
                {
                    _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(), identity.ProfileId);
                    SuccessNotification("CustI Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, cimodel.CUSTOMER_NO, MessageJobEnum.MailType.Authorize, Convert.ToInt32(cimodel.LastUpdatedby));
                }

                return RedirectToAction("AuthList", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(cimodel);
            return View(cimodel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove_(CustomerIncomeModel cimodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_CUSTOMER_INCOME.FirstOrDefault(o => o.CUSTOMER_NO == cimodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", cimodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.AUTHORISED = "N";
                        db.CDMA_CUSTOMER_INCOME.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                SuccessNotification("CUSTI Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = cimodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "CustIncome");
                //return RedirectToAction("Index");
            }
            PrepareModel(cimodel);
            return View(cimodel);
        }

        #region scaffolded
        // GET: CustIncome
        public ActionResult Index()
        {
            return View(_db.CDMA_CUSTOMER_INCOME.ToList());
        }

        // GET: CustIncome/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME = _db.CDMA_CUSTOMER_INCOME.Find(id);
            if (cDMA_CUSTOMER_INCOME == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_CUSTOMER_INCOME);
        }

        // GET: CustIncome/Create
        public ActionResult Create_()
        {
            return View();
        }

        // POST: CustIncome/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_1([Bind(Include = "CUSTOMER_NO,INCOME_BAND,INITIAL_DEPOSIT,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME)
        {
            if (ModelState.IsValid)
            {
                _db.CDMA_CUSTOMER_INCOME.Add(cDMA_CUSTOMER_INCOME);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_CUSTOMER_INCOME);
        }

        // GET: CustIncome/Edit/5
        public ActionResult Edit_(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME = _db.CDMA_CUSTOMER_INCOME.Find(id);
            if (cDMA_CUSTOMER_INCOME == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_CUSTOMER_INCOME);
        }

        // POST: CustIncome/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_1([Bind(Include = "CUSTOMER_NO,INCOME_BAND,INITIAL_DEPOSIT,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cDMA_CUSTOMER_INCOME).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_CUSTOMER_INCOME);
        }

        // GET: CustIncome/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME = _db.CDMA_CUSTOMER_INCOME.Find(id);
            if (cDMA_CUSTOMER_INCOME == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_CUSTOMER_INCOME);
        }

        // POST: CustIncome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME = _db.CDMA_CUSTOMER_INCOME.Find(id);
            _db.CDMA_CUSTOMER_INCOME.Remove(cDMA_CUSTOMER_INCOME);
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
