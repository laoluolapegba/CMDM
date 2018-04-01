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

namespace CMdm.UI.Web.Controllers
{
    public class FinInclusionController : BaseController
    {
        private AppDbContext db = new AppDbContext();
 

        // GET: FinInclusion/Create
        public ActionResult Create(string id = "")
        {
            AuthForFinInclution model = new AuthForFinInclution();
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
        public ActionResult Create(AuthForFinInclution clienaccmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;

            AuthForFinInclution model = new AuthForFinInclution();
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
                    entity.LAST_MODIFIED_BY = clienaccmodel.LAST_MODIFIED_BY;
                    entity.LAST_MODIFIED_DATE = clienaccmodel.LAST_MODIFIED_DATE;


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
        public ActionResult Edit(string id)
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
                         select new AuthForFinInclution
                         {
                            
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             SOCIAL_FINANCIAL_DISADVTAGE = c.SOCIAL_FINANCIAL_DISADVTAGE,
                             SOCIAL_FINANCIAL_DOCUMENTS = c.SOCIAL_FINANCIAL_DOCUMENTS,
                             ENJOYED_TIERED_KYC = c.ENJOYED_TIERED_KYC,
                             RISK_CATEGORY = c.RISK_CATEGORY,
                             MANDATE_AUTH_COMBINE_RULE = c.MANDATE_AUTH_COMBINE_RULE,
                             ACCOUNT_WITH_OTHER_BANKS = c.ACCOUNT_WITH_OTHER_BANKS,
                             LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                             LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE


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
        public ActionResult Edit(AuthForFinInclution clienaccmodel, bool continueEditing)
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
                        entity.LAST_MODIFIED_BY = clienaccmodel.LAST_MODIFIED_BY;
                        entity.LAST_MODIFIED_DATE = clienaccmodel.LAST_MODIFIED_DATE;                       
                        
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

        // GET: FinInclusion/Delete/5
        [NonAction]
        protected virtual void PrepareModel(AuthForFinInclution model)
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
