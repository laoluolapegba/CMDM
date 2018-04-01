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
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Framework.Controllers;

namespace CMdm.UI.Web.Controllers
{
    public class CusClientAccController : BaseController
    {
        private AppDbContext db = new AppDbContext();



        // GET: CusClientAcc/Create
        public ActionResult Create(string id = "")
        {
            CustomerTrustClientAccount model = new CustomerTrustClientAccount();
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
        public ActionResult Create(CustomerTrustClientAccount clienaccmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;

            CustomerTrustClientAccount model = new CustomerTrustClientAccount();
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
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Create");
            }
            
            var model = (from c in db.CDMA_TRUSTS_CLIENT_ACCOUNTS

                         where c.CUSTOMER_NO == id
                         select new CustomerTrustClientAccount
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
                             LAST_MODIFIED_BY = c.LAST_MODIFIED_BY


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
        public ActionResult Edit(CustomerTrustClientAccount clienaccmodel, bool continueEditing)
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

        [NonAction]
        protected virtual void PrepareModel(CustomerTrustClientAccount model)
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
    }
}
