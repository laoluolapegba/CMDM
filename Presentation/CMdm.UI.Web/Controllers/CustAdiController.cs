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
    public class CustAdiController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }


            var model = (from c in db.CDMA_ADDITIONAL_INFORMATION

                         where c.CUSTOMER_NO == id
                         select new CustomerADIModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             ANNUAL_SALARY_EXPECTED_INC = c.ANNUAL_SALARY_EXPECTED_INC,
                             FAX_NUMBER = c.FAX_NUMBER,
                         }).FirstOrDefault();


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
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_ADDITIONAL_INFORMATION.FirstOrDefault(o => o.CUSTOMER_NO == adimodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", adimodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.ANNUAL_SALARY_EXPECTED_INC = adimodel.ANNUAL_SALARY_EXPECTED_INC;
                        entity.FAX_NUMBER = adimodel.FAX_NUMBER;
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_ADDITIONAL_INFORMATION.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

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

            var model = new CustomerADIModel();
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
                db.CDMA_ADDITIONAL_INFORMATION.Add(adi);
                db.SaveChanges();


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
            if (model == null)
                throw new ArgumentNullException("model");

            if (model == null)
                throw new ArgumentNullException("model");
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

        #region scaffolded
        // GET: CustAdi
        public ActionResult Index()
        {
            return View(db.CDMA_ADDITIONAL_INFORMATION.ToList());
        }

        // GET: CustAdi/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_ADDITIONAL_INFORMATION cDMA_ADDITIONAL_INFORMATION = db.CDMA_ADDITIONAL_INFORMATION.Find(id);
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
                db.CDMA_ADDITIONAL_INFORMATION.Add(cDMA_ADDITIONAL_INFORMATION);
                db.SaveChanges();
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
            CDMA_ADDITIONAL_INFORMATION cDMA_ADDITIONAL_INFORMATION = db.CDMA_ADDITIONAL_INFORMATION.Find(id);
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
                db.Entry(cDMA_ADDITIONAL_INFORMATION).State = EntityState.Modified;
                db.SaveChanges();
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
            CDMA_ADDITIONAL_INFORMATION cDMA_ADDITIONAL_INFORMATION = db.CDMA_ADDITIONAL_INFORMATION.Find(id);
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
            CDMA_ADDITIONAL_INFORMATION cDMA_ADDITIONAL_INFORMATION = db.CDMA_ADDITIONAL_INFORMATION.Find(id);
            db.CDMA_ADDITIONAL_INFORMATION.Remove(cDMA_ADDITIONAL_INFORMATION);
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
