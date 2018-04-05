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

namespace CMdm.UI.Web.Controllers
{
    public class CustTcaController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IDqQueService _dqQueService;

        public CustTcaController()
        {
            _dqQueService = new DqQueService();
        }

        public ActionResult Authorize(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            var model = new CustomerTCAModel();

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }
            //get all changed columns

            var changeId = db.CDMA_CHANGE_LOGS.Where(a => a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();
            if (changeId != null)
            {
                var changedSet = db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId.CHANGEID); //.Select(a=>a.PROPERTYNAME);
                model = (from c in db.CDMA_TRUSTS_CLIENT_ACCOUNTS
                         where c.CUSTOMER_NO == querecord.CUST_ID
                         select new CustomerTCAModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             TRUSTS_CLIENT_ACCOUNTS = c.TRUSTS_CLIENT_ACCOUNTS,
                             NAME_OF_BENEFICIAL_OWNER = c.NAME_OF_BENEFICIAL_OWNER,
                             SPOUSE_NAME = c.SPOUSE_NAME,
                             SPOUSE_DATE_OF_BIRTH = c.SPOUSE_DATE_OF_BIRTH,
                             SPOUSE_OCCUPATION = c.SPOUSE_OCCUPATION,
                             SOURCES_OF_FUND_TO_ACCOUNT = c.SOURCES_OF_FUND_TO_ACCOUNT,
                             OTHER_SOURCE_EXPECT_ANN_INC = c.OTHER_SOURCE_EXPECT_ANN_INC,
                             NAME_OF_ASSOCIATED_BUSINESS = c.NAME_OF_ASSOCIATED_BUSINESS,
                             FREQ_INTERNATIONAL_TRAVELER = c.FREQ_INTERNATIONAL_TRAVELER,
                             INSIDER_RELATION = c.INSIDER_RELATION,
                             POLITICALLY_EXPOSED_PERSON = c.POLITICALLY_EXPOSED_PERSON,
                             POWER_OF_ATTORNEY = c.POWER_OF_ATTORNEY,
                             HOLDER_NAME = c.HOLDER_NAME,
                             ADDRESS = c.ADDRESS,
                             COUNTRY = c.COUNTRY,
                             NATIONALITY = c.NATIONALITY,
                             TELEPHONE_NUMBER = c.TELEPHONE_NUMBER,
                             LastUpdatedby = c.LAST_MODIFIED_BY,
                             LastUpdatedDate = c.LAST_MODIFIED_DATE,
                             LastAuthdby = c.AUTHORISED_BY,
                             LastAuthDate = c.AUTHORISED_DATE,
                             ExceptionId = querecord.EXCEPTION_ID
                         }).FirstOrDefault();

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
                model.ReadOnlyForm = "True";
            }

            PrepareModel(model);
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }

            var model = (from c in db.CDMA_TRUSTS_CLIENT_ACCOUNTS
                         where c.CUSTOMER_NO == id

                         select new CustomerTCAModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             TRUSTS_CLIENT_ACCOUNTS = c.TRUSTS_CLIENT_ACCOUNTS,
                             NAME_OF_BENEFICIAL_OWNER = c.NAME_OF_BENEFICIAL_OWNER,
                             SPOUSE_NAME = c.SPOUSE_NAME,
                             SPOUSE_DATE_OF_BIRTH = c.SPOUSE_DATE_OF_BIRTH,
                             SPOUSE_OCCUPATION = c.SPOUSE_OCCUPATION,
                             SOURCES_OF_FUND_TO_ACCOUNT = c.SOURCES_OF_FUND_TO_ACCOUNT,
                             OTHER_SOURCE_EXPECT_ANN_INC = c.OTHER_SOURCE_EXPECT_ANN_INC,
                             NAME_OF_ASSOCIATED_BUSINESS = c.NAME_OF_ASSOCIATED_BUSINESS,
                             FREQ_INTERNATIONAL_TRAVELER = c.FREQ_INTERNATIONAL_TRAVELER,
                             INSIDER_RELATION = c.INSIDER_RELATION,
                             POLITICALLY_EXPOSED_PERSON = c.POLITICALLY_EXPOSED_PERSON,
                             POWER_OF_ATTORNEY = c.POWER_OF_ATTORNEY,
                             HOLDER_NAME = c.HOLDER_NAME,
                             ADDRESS = c.ADDRESS,
                             COUNTRY = c.COUNTRY,
                             NATIONALITY = c.NATIONALITY,
                             TELEPHONE_NUMBER = c.TELEPHONE_NUMBER
                         }).FirstOrDefault();

            PrepareModel(model);
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
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.FirstOrDefault(o => o.CUSTOMER_NO == tcamodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", tcamodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.TRUSTS_CLIENT_ACCOUNTS = tcamodel.TRUSTS_CLIENT_ACCOUNTS;
                        entity.NAME_OF_BENEFICIAL_OWNER = tcamodel.NAME_OF_BENEFICIAL_OWNER;
                        entity.SPOUSE_NAME = tcamodel.SPOUSE_NAME;
                        entity.SPOUSE_DATE_OF_BIRTH = tcamodel.SPOUSE_DATE_OF_BIRTH;
                        entity.SPOUSE_OCCUPATION = tcamodel.SPOUSE_OCCUPATION;
                        entity.SOURCES_OF_FUND_TO_ACCOUNT = tcamodel.SOURCES_OF_FUND_TO_ACCOUNT;
                        entity.OTHER_SOURCE_EXPECT_ANN_INC = tcamodel.OTHER_SOURCE_EXPECT_ANN_INC;
                        entity.NAME_OF_ASSOCIATED_BUSINESS = tcamodel.NAME_OF_ASSOCIATED_BUSINESS;
                        entity.FREQ_INTERNATIONAL_TRAVELER = tcamodel.FREQ_INTERNATIONAL_TRAVELER;
                        entity.INSIDER_RELATION = tcamodel.INSIDER_RELATION;
                        entity.POLITICALLY_EXPOSED_PERSON = tcamodel.POLITICALLY_EXPOSED_PERSON;
                        entity.POWER_OF_ATTORNEY = tcamodel.POWER_OF_ATTORNEY;
                        entity.HOLDER_NAME = tcamodel.HOLDER_NAME;
                        entity.ADDRESS = tcamodel.ADDRESS;
                        entity.COUNTRY = tcamodel.COUNTRY;
                        entity.NATIONALITY = tcamodel.NATIONALITY;
                        entity.TELEPHONE_NUMBER = tcamodel.TELEPHONE_NUMBER;
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                SuccessNotification("TCA Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = tcamodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(tcamodel);
            return View(tcamodel);
        }
        public ActionResult Create()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();

            var model = new CustomerTCAModel();
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
                    TRUSTS_CLIENT_ACCOUNTS = tcamodel.TRUSTS_CLIENT_ACCOUNTS,
                    NAME_OF_BENEFICIAL_OWNER = tcamodel.NAME_OF_BENEFICIAL_OWNER,
                    SPOUSE_NAME = tcamodel.SPOUSE_NAME,
                    SPOUSE_DATE_OF_BIRTH = tcamodel.SPOUSE_DATE_OF_BIRTH,
                    SPOUSE_OCCUPATION = tcamodel.SPOUSE_OCCUPATION,
                    SOURCES_OF_FUND_TO_ACCOUNT = tcamodel.SOURCES_OF_FUND_TO_ACCOUNT,
                    OTHER_SOURCE_EXPECT_ANN_INC = tcamodel.OTHER_SOURCE_EXPECT_ANN_INC,
                    NAME_OF_ASSOCIATED_BUSINESS = tcamodel.NAME_OF_ASSOCIATED_BUSINESS,
                    FREQ_INTERNATIONAL_TRAVELER = tcamodel.FREQ_INTERNATIONAL_TRAVELER,
                    INSIDER_RELATION = tcamodel.INSIDER_RELATION,
                    POLITICALLY_EXPOSED_PERSON = tcamodel.POLITICALLY_EXPOSED_PERSON,
                    POWER_OF_ATTORNEY = tcamodel.POWER_OF_ATTORNEY,
                    HOLDER_NAME = tcamodel.HOLDER_NAME,
                    ADDRESS = tcamodel.ADDRESS,
                    COUNTRY = tcamodel.COUNTRY,
                    NATIONALITY = tcamodel.NATIONALITY,
                    TELEPHONE_NUMBER = tcamodel.TELEPHONE_NUMBER,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Add(tca);
                db.SaveChanges();


                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New TCA has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = tcamodel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(tcamodel);
            return View(tcamodel);
        }

        [NonAction]
        protected virtual void PrepareModel(CustomerTCAModel model)
        {

            if (model == null)
                throw new ArgumentNullException("model");

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

            model.Countries = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
            model.Nationalities = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("approve")]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(CustomerTCAModel tcamodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                _dqQueService.ApproveExceptionQueItems(tcamodel.ExceptionId.ToString());

                SuccessNotification("TCA Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = tcamodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "CustTca");
                //return RedirectToAction("Index");
            }
            PrepareModel(tcamodel);
            return View(tcamodel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove(CustomerTCAModel tcamodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.FirstOrDefault(o => o.CUSTOMER_NO == tcamodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", tcamodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.AUTHORISED = "N";
                        db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                SuccessNotification("TCA Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = tcamodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "CustTca");
                //return RedirectToAction("Index");
            }
            PrepareModel(tcamodel);
            return View(tcamodel);
        }
        #region scaffolded
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
                    entity.TRUSTS_CLIENT_ACCOUNTS = clienaccmodel.TRUSTS_CLIENT_ACCOUNTS;
                    entity.NAME_OF_BENEFICIAL_OWNER = clienaccmodel.NAME_OF_BENEFICIAL_OWNER;
                    entity.SPOUSE_NAME = clienaccmodel.SPOUSE_NAME;
                    entity.SPOUSE_DATE_OF_BIRTH = clienaccmodel.SPOUSE_DATE_OF_BIRTH;
                    entity.SPOUSE_OCCUPATION = clienaccmodel.SPOUSE_OCCUPATION;
                    entity.SOURCES_OF_FUND_TO_ACCOUNT = clienaccmodel.SOURCES_OF_FUND_TO_ACCOUNT;
                    entity.OTHER_SOURCE_EXPECT_ANN_INC = clienaccmodel.OTHER_SOURCE_EXPECT_ANN_INC;
                    entity.NAME_OF_ASSOCIATED_BUSINESS = clienaccmodel.NAME_OF_ASSOCIATED_BUSINESS;
                    entity.FREQ_INTERNATIONAL_TRAVELER = clienaccmodel.FREQ_INTERNATIONAL_TRAVELER;
                    entity.INSIDER_RELATION = clienaccmodel.INSIDER_RELATION;
                    entity.POLITICALLY_EXPOSED_PERSON = clienaccmodel.POLITICALLY_EXPOSED_PERSON;
                    entity.POWER_OF_ATTORNEY = clienaccmodel.POWER_OF_ATTORNEY;
                    entity.HOLDER_NAME = clienaccmodel.HOLDER_NAME;
                    entity.ADDRESS = clienaccmodel.ADDRESS;
                    entity.COUNTRY = clienaccmodel.COUNTRY;
                    entity.NATIONALITY = clienaccmodel.NATIONALITY;
                    entity.TELEPHONE_NUMBER = clienaccmodel.TELEPHONE_NUMBER;
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
            
            var model = (from c in db.CDMA_TRUSTS_CLIENT_ACCOUNTS

                         where c.CUSTOMER_NO == id
                         select new CustomerTCAModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             TRUSTS_CLIENT_ACCOUNTS = c.TRUSTS_CLIENT_ACCOUNTS,
                             NAME_OF_BENEFICIAL_OWNER = c.NAME_OF_BENEFICIAL_OWNER,
                             SPOUSE_NAME = c.SPOUSE_NAME,
                             SPOUSE_DATE_OF_BIRTH = c.SPOUSE_DATE_OF_BIRTH,
                             SPOUSE_OCCUPATION = c.SPOUSE_OCCUPATION,
                             SOURCES_OF_FUND_TO_ACCOUNT = c.SOURCES_OF_FUND_TO_ACCOUNT,
                             OTHER_SOURCE_EXPECT_ANN_INC = c.OTHER_SOURCE_EXPECT_ANN_INC,
                             NAME_OF_ASSOCIATED_BUSINESS = c.NAME_OF_ASSOCIATED_BUSINESS,
                             FREQ_INTERNATIONAL_TRAVELER = c.FREQ_INTERNATIONAL_TRAVELER,
                             INSIDER_RELATION = c.INSIDER_RELATION,
                             POLITICALLY_EXPOSED_PERSON = c.POLITICALLY_EXPOSED_PERSON,
                             POWER_OF_ATTORNEY = c.POWER_OF_ATTORNEY,
                             HOLDER_NAME = c.HOLDER_NAME,
                             ADDRESS = c.ADDRESS,
                             COUNTRY = c.COUNTRY,
                             NATIONALITY = c.NATIONALITY,
                             TELEPHONE_NUMBER = c.TELEPHONE_NUMBER,
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

                        entity.TRUSTS_CLIENT_ACCOUNTS = clienaccmodel.TRUSTS_CLIENT_ACCOUNTS;
                        entity.NAME_OF_BENEFICIAL_OWNER = clienaccmodel.NAME_OF_BENEFICIAL_OWNER;
                        entity.SPOUSE_NAME = clienaccmodel.SPOUSE_NAME;
                        entity.SPOUSE_DATE_OF_BIRTH = clienaccmodel.SPOUSE_DATE_OF_BIRTH;
                        entity.SPOUSE_OCCUPATION = clienaccmodel.SPOUSE_OCCUPATION;
                        entity.SOURCES_OF_FUND_TO_ACCOUNT = clienaccmodel.SOURCES_OF_FUND_TO_ACCOUNT;
                        entity.OTHER_SOURCE_EXPECT_ANN_INC = clienaccmodel.OTHER_SOURCE_EXPECT_ANN_INC;
                        entity.NAME_OF_ASSOCIATED_BUSINESS = clienaccmodel.NAME_OF_ASSOCIATED_BUSINESS;
                        entity.FREQ_INTERNATIONAL_TRAVELER = clienaccmodel.FREQ_INTERNATIONAL_TRAVELER;
                        entity.INSIDER_RELATION = clienaccmodel.INSIDER_RELATION;
                        entity.POLITICALLY_EXPOSED_PERSON = clienaccmodel.POLITICALLY_EXPOSED_PERSON;
                        entity.POWER_OF_ATTORNEY = clienaccmodel.POWER_OF_ATTORNEY;
                        entity.HOLDER_NAME = clienaccmodel.HOLDER_NAME;
                        entity.ADDRESS = clienaccmodel.ADDRESS;
                        entity.COUNTRY = clienaccmodel.COUNTRY;
                        entity.NATIONALITY = clienaccmodel.NATIONALITY;
                        entity.TELEPHONE_NUMBER = clienaccmodel.TELEPHONE_NUMBER;
                        
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
            CDMA_TRUSTS_CLIENT_ACCOUNTS cDMA_TRUSTS_CLIENT_ACCOUNTS = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Find(id);
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
            CDMA_TRUSTS_CLIENT_ACCOUNTS cDMA_TRUSTS_CLIENT_ACCOUNTS = db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Find(id);
            db.CDMA_TRUSTS_CLIENT_ACCOUNTS.Remove(cDMA_TRUSTS_CLIENT_ACCOUNTS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion scaffolded
    }
}
