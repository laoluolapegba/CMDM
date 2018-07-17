using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.Customer;
using CMdm.UI.Web.Models.Customer;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Framework.Controllers;
using CMdm.Services.DqQue;
using CMdm.Services.Messaging;
using CMdm.Entities.ViewModels;

namespace CMdm.UI.Web.Controllers
{
    public class CustTcaController : BaseController
    {
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        private IMessagingService _messageService;

        public CustTcaController()
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

            //var model = new CustomerTCAModel();

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }
            //get all changed columns

            CustomerTCAModel model = new CustomerTCAModel();
            model = (from c in _db.CDMA_TRUSTS_CLIENT_ACCOUNTS
                         where c.CUSTOMER_NO == querecord.CUST_ID
                         where c.AUTHORISED == "U"
                         select new CustomerTCAModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             CUSTOMER_BUSINESS_ADDRESS = c.CUSTOMER_BUSINESS_ADDRESS,
                             CUSTOMER_SPOUSE_DOB = c.CUSTOMER_SPOUSE_DOB,
                             OTHER_SOURCE_EXPECT_ANN_INC = c.OTHER_SOURCE_EXPECT_ANN_INC,
                             CUSTOMER_BUSINESS_NAME = c.CUSTOMER_BUSINESS_NAME,
                             CUSTOMER_SPOUSE_NAME = c.CUSTOMER_SPOUSE_NAME,
                             CUSTOMER_SPOUSE_OCCUPATION = c.CUSTOMER_SPOUSE_OCCUPATION,
                             CUSTOMER_BUSINESS_TYPE = c.CUSTOMER_BUSINESS_TYPE,
                             SOURCES_OF_FUND_TO_ACCOUNT = c.SOURCES_OF_FUND_TO_ACCOUNT,
                             BRANCH_CODE = c.BRANCH_CODE,
                             LastUpdatedby = c.LAST_MODIFIED_BY,
                             LastUpdatedDate = c.LAST_MODIFIED_DATE,
                             LastAuthdby = c.AUTHORISED_BY,
                             LastAuthDate = c.AUTHORISED_DATE,
                             ExceptionId = querecord.EXCEPTION_ID
                         }).FirstOrDefault();

            var changelog = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_TRUSTS_CLIENT_ACCOUNTS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();
            if (changelog != null && model != null)
            {
                string changeId = changelog.CHANGEID;
                var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId);
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

            int records = _db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Count(o => o.CUSTOMER_NO == id);
            CustomerTCAModel model = new CustomerTCAModel();
            if(records > 1)
            {
                model = (from c in _db.CDMA_TRUSTS_CLIENT_ACCOUNTS
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "U"
                         select new CustomerTCAModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             CUSTOMER_BUSINESS_ADDRESS = c.CUSTOMER_BUSINESS_ADDRESS,
                             CUSTOMER_SPOUSE_DOB = c.CUSTOMER_SPOUSE_DOB,
                             OTHER_SOURCE_EXPECT_ANN_INC = c.OTHER_SOURCE_EXPECT_ANN_INC,
                             CUSTOMER_BUSINESS_NAME = c.CUSTOMER_BUSINESS_NAME,
                             CUSTOMER_SPOUSE_NAME = c.CUSTOMER_SPOUSE_NAME,
                             CUSTOMER_SPOUSE_OCCUPATION = c.CUSTOMER_SPOUSE_OCCUPATION,
                             CUSTOMER_BUSINESS_TYPE = c.CUSTOMER_BUSINESS_TYPE,
                             SOURCES_OF_FUND_TO_ACCOUNT = c.SOURCES_OF_FUND_TO_ACCOUNT,
                             BRANCH_CODE = c.BRANCH_CODE,
                         }).FirstOrDefault();
            }else if(records == 1)
            {
                model = (from c in _db.CDMA_TRUSTS_CLIENT_ACCOUNTS
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "A"
                         select new CustomerTCAModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             CUSTOMER_BUSINESS_ADDRESS = c.CUSTOMER_BUSINESS_ADDRESS,
                             CUSTOMER_SPOUSE_DOB = c.CUSTOMER_SPOUSE_DOB,
                             OTHER_SOURCE_EXPECT_ANN_INC = c.OTHER_SOURCE_EXPECT_ANN_INC,
                             CUSTOMER_BUSINESS_NAME = c.CUSTOMER_BUSINESS_NAME,
                             CUSTOMER_SPOUSE_NAME = c.CUSTOMER_SPOUSE_NAME,
                             CUSTOMER_SPOUSE_OCCUPATION = c.CUSTOMER_SPOUSE_OCCUPATION,
                             CUSTOMER_BUSINESS_TYPE = c.CUSTOMER_BUSINESS_TYPE,
                             SOURCES_OF_FUND_TO_ACCOUNT = c.SOURCES_OF_FUND_TO_ACCOUNT,
                             BRANCH_CODE = c.BRANCH_CODE,
                         }).FirstOrDefault();
            }

            PrepareModel(model);

            var tcacustid = "";
            try
            {
                tcacustid = _db.MdmDqRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_TRUSTS_CLIENT_ACCOUNTS" && a.CUST_ID == model.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (tcacustid != "" && model != null)
            {
                var exceptionSet = _db.MdmDqRunExceptions.Where(a => a.CUST_ID == tcacustid); //.Select(a=>a.PROPERTYNAME);
                if (tcacustid != null)
                {
                    foreach (var item in model.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in exceptionSet)
                        {
                            if (item2.CATALOG_TAB_COL == item.Name)
                            {
                                ModelState.AddModelError(item.Name, string.Format("Attention!"));
                            }
                        }
                        //props.Add(item.Name);

                    }
                }
            }

            return View(model);
        }

        // POST: CustTca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerTCAModel tcamodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            bool updateFlag = false;

            if (ModelState.IsValid)
            {
                CDMA_TRUSTS_CLIENT_ACCOUNTS originalObject = new CDMA_TRUSTS_CLIENT_ACCOUNTS();

                using (var db = new AppDbContext())
                {
                    int records = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Count(o => o.CUSTOMER_NO == tcamodel.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records > 1)
                    {
                        updateFlag = true;
                        originalObject = _db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Where(o => o.CUSTOMER_NO == tcamodel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var entity = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.FirstOrDefault(o => o.CUSTOMER_NO == tcamodel.CUSTOMER_NO && o.AUTHORISED == "U");
                        if (entity != null)
                        {
                            entity.CUSTOMER_BUSINESS_ADDRESS = tcamodel.CUSTOMER_BUSINESS_ADDRESS;
                            entity.CUSTOMER_SPOUSE_DOB = tcamodel.CUSTOMER_SPOUSE_DOB;
                            entity.OTHER_SOURCE_EXPECT_ANN_INC = tcamodel.OTHER_SOURCE_EXPECT_ANN_INC;
                            entity.CUSTOMER_BUSINESS_NAME = tcamodel.CUSTOMER_BUSINESS_NAME;
                            entity.CUSTOMER_SPOUSE_NAME = tcamodel.CUSTOMER_SPOUSE_NAME;
                            entity.CUSTOMER_SPOUSE_OCCUPATION = tcamodel.CUSTOMER_SPOUSE_OCCUPATION;
                            entity.CUSTOMER_BUSINESS_TYPE = tcamodel.CUSTOMER_BUSINESS_TYPE;
                            entity.SOURCES_OF_FUND_TO_ACCOUNT = tcamodel.SOURCES_OF_FUND_TO_ACCOUNT;
                            entity.BRANCH_CODE = tcamodel.BRANCH_CODE;
                            entity.QUEUE_STATUS = 1;
                            
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";
                            db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), tcamodel.CUSTOMER_NO, updateFlag, originalObject);
                            _messageService.LogEmailJob(identity.ProfileId, entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if (records == 1)
                    {
                        updateFlag = false;
                        var entity = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.FirstOrDefault(o => o.CUSTOMER_NO == tcamodel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject = _db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Where(o => o.CUSTOMER_NO == tcamodel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject != null)
                        {
                            entity.CUSTOMER_BUSINESS_ADDRESS = tcamodel.CUSTOMER_BUSINESS_ADDRESS;
                            entity.CUSTOMER_SPOUSE_DOB = tcamodel.CUSTOMER_SPOUSE_DOB;
                            entity.OTHER_SOURCE_EXPECT_ANN_INC = tcamodel.OTHER_SOURCE_EXPECT_ANN_INC;
                            entity.CUSTOMER_BUSINESS_NAME = tcamodel.CUSTOMER_BUSINESS_NAME;
                            entity.CUSTOMER_SPOUSE_NAME = tcamodel.CUSTOMER_SPOUSE_NAME;
                            entity.CUSTOMER_SPOUSE_OCCUPATION = tcamodel.CUSTOMER_SPOUSE_OCCUPATION;
                            entity.CUSTOMER_BUSINESS_TYPE = tcamodel.CUSTOMER_BUSINESS_TYPE;
                            entity.SOURCES_OF_FUND_TO_ACCOUNT = tcamodel.SOURCES_OF_FUND_TO_ACCOUNT;
                            entity.BRANCH_CODE = tcamodel.BRANCH_CODE;
                            entity.QUEUE_STATUS = 1;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";

                            db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), tcamodel.CUSTOMER_NO, updateFlag, originalObject);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_TRUSTS_CLIENT_ACCOUNTS();
                            newentity.CUSTOMER_BUSINESS_ADDRESS = tcamodel.CUSTOMER_BUSINESS_ADDRESS;
                            newentity.CUSTOMER_SPOUSE_DOB = tcamodel.CUSTOMER_SPOUSE_DOB;
                            newentity.OTHER_SOURCE_EXPECT_ANN_INC = tcamodel.OTHER_SOURCE_EXPECT_ANN_INC;
                            newentity.CUSTOMER_BUSINESS_NAME = tcamodel.CUSTOMER_BUSINESS_NAME;
                            newentity.CUSTOMER_SPOUSE_NAME = tcamodel.CUSTOMER_SPOUSE_NAME;
                            newentity.CUSTOMER_SPOUSE_OCCUPATION = tcamodel.CUSTOMER_SPOUSE_OCCUPATION;
                            newentity.CUSTOMER_BUSINESS_TYPE = tcamodel.CUSTOMER_BUSINESS_TYPE;
                            newentity.SOURCES_OF_FUND_TO_ACCOUNT = tcamodel.SOURCES_OF_FUND_TO_ACCOUNT;
                            newentity.BRANCH_CODE = tcamodel.BRANCH_CODE;
                            newentity.QUEUE_STATUS = 1;
                            newentity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            newentity.LAST_MODIFIED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = tcamodel.CUSTOMER_NO;
                            db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", tcamodel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                }

                SuccessNotification("Trust Clients Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = tcamodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(tcamodel);
            return View(tcamodel);
        }        

        [NonAction]
        protected virtual void PrepareModel(CustomerTCAModel model)
        {

            //if (model == null)
            //    throw new ArgumentNullException("model");

            if (model == null)
                throw new ArgumentNullException("model");
            model.CientAcc.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });
            model.CientAcc.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });
            model.FreqInt.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });
            model.FreqInt.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });

            model.InsidrRel.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });
            model.InsidrRel.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });
            model.PolExpose.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });
            model.PolExpose.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });
            model.PowAnto.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });
            model.PowAnto.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });

            model.Countries = new SelectList(_db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
            model.Nationalities = new SelectList(_db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
            model.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(CustomerTCAModel tcamodel, bool disapproveRecord)
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

                    _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), tcamodel.AuthoriserRemarks);
                    SuccessNotification("TCA Not Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, tcamodel.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(tcamodel.LastUpdatedby));
                }

                else
                {
                    _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(), identity.ProfileId);
                    SuccessNotification("TCA Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, tcamodel.CUSTOMER_NO, MessageJobEnum.MailType.Authorize, Convert.ToInt32(tcamodel.LastUpdatedby));
                }

                return RedirectToAction("AuthList", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(tcamodel);
            return View(tcamodel);
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove_(CustomerTCAModel tcamodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                _dqQueService.DisApproveExceptionQueItems(tcamodel.ExceptionId.ToString(), tcamodel.AuthoriserRemarks);

                SuccessNotification("NOK Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = tcamodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "CustTca");
                //return RedirectToAction("Index");
            }
            PrepareModel(tcamodel);
            return View(tcamodel);
        }

        #region scaffolded
        public ActionResult Create()
        {
            CustomerTCAModel model = new CustomerTCAModel();
            PrepareModel(model);
            return View(model);
        }

        // POST: CustTca/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerTCAModel tcamodel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string ip_address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (ModelState.IsValid)
            {
                CDMA_TRUSTS_CLIENT_ACCOUNTS tca = new CDMA_TRUSTS_CLIENT_ACCOUNTS
                {
                    CUSTOMER_NO = tcamodel.CUSTOMER_NO,
                    SOURCES_OF_FUND_TO_ACCOUNT = tcamodel.SOURCES_OF_FUND_TO_ACCOUNT,
                    OTHER_SOURCE_EXPECT_ANN_INC = tcamodel.OTHER_SOURCE_EXPECT_ANN_INC,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                _db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Add(tca);
                _db.SaveChanges();


                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New TCA has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = tcamodel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(tcamodel);
            return View(tcamodel);
        }
        // GET: CusClientAcc/Create
        public ActionResult Create_(string id = "")
        {
            CustomerTCAModel model = new CustomerTCAModel();
            if(id!="")model.CUSTOMER_NO = id;
            PrepareModel(model);


            return View(model);
        }

        // POST: CusClientAcc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Create_1(CustomerTCAModel clienaccmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;

            CustomerTCAModel model = new CustomerTCAModel();
            PrepareModel(model);

            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    CDMA_TRUSTS_CLIENT_ACCOUNTS entity = new CDMA_TRUSTS_CLIENT_ACCOUNTS();
                    //db.CDMA_TRUSTS_CLIENT_ACCOUNTS.FirstOrDefault(o => o.CUSTOMER_NO == clienaccmodel.CUSTOMER_NO);
                    entity.CUSTOMER_NO = clienaccmodel.CUSTOMER_NO;
                    entity.SOURCES_OF_FUND_TO_ACCOUNT = clienaccmodel.SOURCES_OF_FUND_TO_ACCOUNT;
                    entity.OTHER_SOURCE_EXPECT_ANN_INC = clienaccmodel.OTHER_SOURCE_EXPECT_ANN_INC;
                    entity.CREATED_BY = identity.ProfileId.ToString();
                    entity.CREATED_DATE = DateTime.Now;
                    entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                    entity.LAST_MODIFIED_DATE = DateTime.Now;
                    entity.AUTHORISED = "U";
                    entity.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                     
                    db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Add(entity);
                    // db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                  
                    SuccessNotification("Customer Client Acc Created");
                    return continueEditing ? RedirectToAction("Edit", new { id = clienaccmodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");

                }
                
            }
            return View(clienaccmodel);
        }

        // GET: CusClientAcc/Edit/5
        public ActionResult Edit_(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Create");
            }
            
            var model = (from c in _db.CDMA_TRUSTS_CLIENT_ACCOUNTS

                         where c.CUSTOMER_NO == id
                         select new CustomerTCAModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             SOURCES_OF_FUND_TO_ACCOUNT = c.SOURCES_OF_FUND_TO_ACCOUNT,
                             OTHER_SOURCE_EXPECT_ANN_INC = c.OTHER_SOURCE_EXPECT_ANN_INC,
                         }).FirstOrDefault();


            PrepareModel(model);
            return View(model);
        }


        // POST: CusClientAcc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_1(CustomerTCAModel clienaccmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.FirstOrDefault(o => o.CUSTOMER_NO == clienaccmodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", clienaccmodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.SOURCES_OF_FUND_TO_ACCOUNT = clienaccmodel.SOURCES_OF_FUND_TO_ACCOUNT;
                        entity.OTHER_SOURCE_EXPECT_ANN_INC = clienaccmodel.OTHER_SOURCE_EXPECT_ANN_INC;
                        
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                SuccessNotification("Customer Client Acc Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = clienaccmodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(clienaccmodel);
            return View(clienaccmodel);
        }

        // GET: CusClientAcc/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_TRUSTS_CLIENT_ACCOUNTS cDMA_TRUSTS_CLIENT_ACCOUNTS = _db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Find(id);
            if (cDMA_TRUSTS_CLIENT_ACCOUNTS == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_TRUSTS_CLIENT_ACCOUNTS);
        }

        // POST: CusClientAcc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_TRUSTS_CLIENT_ACCOUNTS cDMA_TRUSTS_CLIENT_ACCOUNTS = _db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Find(id);
            _db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Remove(cDMA_TRUSTS_CLIENT_ACCOUNTS);
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
