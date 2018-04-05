﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.Customer;
using CMdm.UI.Web.Models.Customer;
using CMdm.Framework.Controllers;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Services.DqQue;

namespace CMdm.UI.Web.Controllers
{
    public class AuthFinInclusionController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IDqQueService _dqQueService;

        public AuthFinInclusionController()
        {
            _dqQueService = new DqQueService();
        }

        public ActionResult Authorize(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            var model = new AuthFIModel();

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
                model = (from c in db.CDMA_AUTH_FINANCE_INCLUSION
                         where c.CUSTOMER_NO == querecord.CUST_ID
                         select new AuthFIModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             SOCIAL_FINANCIAL_DISADVTAGE = c.SOCIAL_FINANCIAL_DISADVTAGE,
                             SOCIAL_FINANCIAL_DOCUMENTS = c.SOCIAL_FINANCIAL_DOCUMENTS,
                             ENJOYED_TIERED_KYC = c.ENJOYED_TIERED_KYC,
                             RISK_CATEGORY = c.RISK_CATEGORY,
                             MANDATE_AUTH_COMBINE_RULE = c.MANDATE_AUTH_COMBINE_RULE,
                             ACCOUNT_WITH_OTHER_BANKS = c.ACCOUNT_WITH_OTHER_BANKS,
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

            var model = (from c in db.CDMA_AUTH_FINANCE_INCLUSION

                         where c.CUSTOMER_NO == id
                         select new AuthFIModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             SOCIAL_FINANCIAL_DISADVTAGE = c.SOCIAL_FINANCIAL_DISADVTAGE,
                             SOCIAL_FINANCIAL_DOCUMENTS = c.SOCIAL_FINANCIAL_DOCUMENTS,
                             ENJOYED_TIERED_KYC = c.ENJOYED_TIERED_KYC,
                             RISK_CATEGORY = c.RISK_CATEGORY,
                             MANDATE_AUTH_COMBINE_RULE = c.MANDATE_AUTH_COMBINE_RULE,
                             ACCOUNT_WITH_OTHER_BANKS = c.ACCOUNT_WITH_OTHER_BANKS,
                         }).FirstOrDefault();

            PrepareModel(model);
            return View(model);
        }

        // POST: AuthFinInclusion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthFIModel afimodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_AUTH_FINANCE_INCLUSION.FirstOrDefault(o => o.CUSTOMER_NO == afimodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", afimodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.SOCIAL_FINANCIAL_DISADVTAGE = afimodel.SOCIAL_FINANCIAL_DISADVTAGE;
                        entity.SOCIAL_FINANCIAL_DOCUMENTS = afimodel.SOCIAL_FINANCIAL_DOCUMENTS;
                        entity.ENJOYED_TIERED_KYC = afimodel.ENJOYED_TIERED_KYC;
                        entity.RISK_CATEGORY = afimodel.RISK_CATEGORY;
                        entity.MANDATE_AUTH_COMBINE_RULE = afimodel.MANDATE_AUTH_COMBINE_RULE;
                        entity.ACCOUNT_WITH_OTHER_BANKS = afimodel.ACCOUNT_WITH_OTHER_BANKS;
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_AUTH_FINANCE_INCLUSION.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                SuccessNotification("AFI Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = afimodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(afimodel);
            return View(afimodel);
        }
        public ActionResult Create()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();

            var model = new AuthFIModel();
            PrepareModel(model);
            return View(model);
        }

        // POST: AuthFinInclusion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthFIModel afimodel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string ip_address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (ModelState.IsValid)
            {
                CDMA_AUTH_FINANCE_INCLUSION afi = new CDMA_AUTH_FINANCE_INCLUSION
                {
                    CUSTOMER_NO = afimodel.CUSTOMER_NO,
                    SOCIAL_FINANCIAL_DISADVTAGE = afimodel.SOCIAL_FINANCIAL_DISADVTAGE,
                    SOCIAL_FINANCIAL_DOCUMENTS = afimodel.SOCIAL_FINANCIAL_DOCUMENTS,
                    ENJOYED_TIERED_KYC = afimodel.ENJOYED_TIERED_KYC,
                    RISK_CATEGORY = afimodel.RISK_CATEGORY,
                    MANDATE_AUTH_COMBINE_RULE = afimodel.MANDATE_AUTH_COMBINE_RULE,
                    ACCOUNT_WITH_OTHER_BANKS = afimodel.ACCOUNT_WITH_OTHER_BANKS,
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                db.CDMA_AUTH_FINANCE_INCLUSION.Add(afi);
                db.SaveChanges();
                
                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New AFI has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = afimodel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(afimodel);
            return View(afimodel);
        }


        [NonAction]
        protected virtual void PrepareModel(AuthFIModel model)
        {

            if (model == null)
                throw new ArgumentNullException("model");

            if (model == null)
                throw new ArgumentNullException("model");
            model.SocialOrFin.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });
            model.SocialOrFin.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });
            model.KycReq.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });
            model.KycReq.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });

            model.YesKyc.Add(new SelectListItem
            {
                Text = "Low",
                Value = "Low"
            });
            model.YesKyc.Add(new SelectListItem
            {
                Text = "Medium",
                Value = "Medium"
            });

            model.YesKyc.Add(new SelectListItem
            {
                Text = "High",
                Value = "High"
            });

        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("approve")]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(AuthFIModel afimodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {

                _dqQueService.ApproveExceptionQueItems(afimodel.ExceptionId.ToString());
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

                SuccessNotification("AFI Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = afimodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "AuthFinInclusion");
                //return RedirectToAction("Index");
            }
            PrepareModel(afimodel);
            return View(afimodel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove(AuthFIModel afimodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_AUTH_FINANCE_INCLUSION.FirstOrDefault(o => o.CUSTOMER_NO == afimodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", afimodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.AUTHORISED = "N";
                        db.CDMA_AUTH_FINANCE_INCLUSION.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                SuccessNotification("AFI Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = afimodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "AuthFinInclusion");
                //return RedirectToAction("Index");
            }
            PrepareModel(afimodel);
            return View(afimodel);
        }


        #region scaffolded
        // GET: FinInclusion/Create
        public ActionResult Create_(string id = "")
        {
            AuthFIModel model = new AuthFIModel();
            if (id != "") model.CUSTOMER_NO = id;
            PrepareModel(model);


            return View(model);
        }

        // POST: FinInclusion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Create_1(AuthFIModel clienaccmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;

            AuthFIModel model = new AuthFIModel();
            PrepareModel(model);

            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    CDMA_AUTH_FINANCE_INCLUSION entity = new CDMA_AUTH_FINANCE_INCLUSION();
                    //db.CDMA_TRUSTS_CLIENT_ACCOUNTS.FirstOrDefault(o => o.CUSTOMER_NO == clienaccmodel.CUSTOMER_NO);
                    entity.CUSTOMER_NO = clienaccmodel.CUSTOMER_NO;
                    entity.CUSTOMER_NO = clienaccmodel.CUSTOMER_NO;
                    entity.SOCIAL_FINANCIAL_DISADVTAGE = clienaccmodel.SOCIAL_FINANCIAL_DISADVTAGE;
                    entity.SOCIAL_FINANCIAL_DOCUMENTS = clienaccmodel.SOCIAL_FINANCIAL_DOCUMENTS;
                    entity.ENJOYED_TIERED_KYC = clienaccmodel.ENJOYED_TIERED_KYC;
                    entity.RISK_CATEGORY = clienaccmodel.RISK_CATEGORY;
                    entity.MANDATE_AUTH_COMBINE_RULE = clienaccmodel.MANDATE_AUTH_COMBINE_RULE;
                    entity.ACCOUNT_WITH_OTHER_BANKS = clienaccmodel.ACCOUNT_WITH_OTHER_BANKS;


                    entity.CREATED_BY = identity.ProfileId.ToString();
                    entity.CREATED_DATE = DateTime.Now;
                    entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                    entity.LAST_MODIFIED_DATE = DateTime.Now;
                    entity.AUTHORISED = "U";
                    entity.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                    db.CDMA_AUTH_FINANCE_INCLUSION.Add(entity);
                   // db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                     
                    SuccessNotification("Customer Client Acc Created");
                    return continueEditing ? RedirectToAction("Edit", new { id = clienaccmodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");

                }

            }
            return View(clienaccmodel);
        }

        // GET: FinInclusion/Edit/5
        public ActionResult Edit_(string id)
        {

            if (id == null)
            {
                return RedirectToAction("Create");
            }
              if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }
             
            var model = (from c in db.CDMA_AUTH_FINANCE_INCLUSION

                         where c.CUSTOMER_NO == id
                         select new AuthFIModel
                         {
                            
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             SOCIAL_FINANCIAL_DISADVTAGE = c.SOCIAL_FINANCIAL_DISADVTAGE,
                             SOCIAL_FINANCIAL_DOCUMENTS = c.SOCIAL_FINANCIAL_DOCUMENTS,
                             ENJOYED_TIERED_KYC = c.ENJOYED_TIERED_KYC,
                             RISK_CATEGORY = c.RISK_CATEGORY,
                             MANDATE_AUTH_COMBINE_RULE = c.MANDATE_AUTH_COMBINE_RULE,
                             ACCOUNT_WITH_OTHER_BANKS = c.ACCOUNT_WITH_OTHER_BANKS,
                         }).FirstOrDefault();
            

            PrepareModel(model);
            return View(model);


        }

        // POST: FinInclusion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_1(AuthFIModel clienaccmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_AUTH_FINANCE_INCLUSION.FirstOrDefault(o => o.CUSTOMER_NO == clienaccmodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", clienaccmodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {

                        entity.CUSTOMER_NO = clienaccmodel.CUSTOMER_NO;
                        entity.SOCIAL_FINANCIAL_DISADVTAGE = clienaccmodel.SOCIAL_FINANCIAL_DISADVTAGE;
                        entity.SOCIAL_FINANCIAL_DOCUMENTS = clienaccmodel.SOCIAL_FINANCIAL_DOCUMENTS;
                        entity.ENJOYED_TIERED_KYC = clienaccmodel.ENJOYED_TIERED_KYC;
                        entity.RISK_CATEGORY = clienaccmodel.RISK_CATEGORY;
                        entity.MANDATE_AUTH_COMBINE_RULE = clienaccmodel.MANDATE_AUTH_COMBINE_RULE;
                        entity.ACCOUNT_WITH_OTHER_BANKS = clienaccmodel.ACCOUNT_WITH_OTHER_BANKS;               
                        
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_AUTH_FINANCE_INCLUSION.Attach(entity);
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