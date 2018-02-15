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
using CMdm.UI.Web.Helpers.CrossCutting.Security;

namespace CMdm.UI.Web.Controllers
{
    public class ManageCustController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: ManageCust
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string brnId = identity.BranchId.ToString();
            return View(db.CDMA_INDIVIDUAL_BIO_DATA.Where(a => a.BRANCH_CODE == brnId).ToList());
        }
        public ActionResult Unauthorized()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string brnId = identity.BranchId.ToString();
            return View(db.CDMA_INDIVIDUAL_BIO_DATA.Where(a =>a.BRANCH_CODE == brnId).ToList());
        }
        // GET: ManageCust/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_BIO_DATA indCustomerBioData = db.CDMA_INDIVIDUAL_BIO_DATA.Find(id);
            if (indCustomerBioData == null)
            {
                return HttpNotFound();
            }
            return View(indCustomerBioData);
        }

        // GET: ManageCust/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageCust/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,TITLE,SURNAME,FIRST_NAME,OTHER_NAME,NICKNAME_ALIAS,DATE_OF_BIRTH,PLACE_OF_BIRTH,COUNTRY_OF_BIRTH,SEX,AGE,MARITAL_STATUS,NATIONALITY,STATE_OF_ORIGIN,MOTHER_MAIDEN_NAME,DISABILITY,COMPLEXION,NUMBER_OF_CHILDREN,RELIGION,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS,BRANCH_CODE")] CDMA_INDIVIDUAL_BIO_DATA indCustomerBioData)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_INDIVIDUAL_BIO_DATA.Add(indCustomerBioData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(indCustomerBioData);
        }

        // GET: ManageCust/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_BIO_DATA indCustomerBioData = db.CDMA_INDIVIDUAL_BIO_DATA.Find(id);
            if (indCustomerBioData == null)
            {
                return HttpNotFound();
            }
            return View(indCustomerBioData);
        }

        // POST: ManageCust/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_NO,TITLE,SURNAME,FIRST_NAME,OTHER_NAME,NICKNAME_ALIAS,DATE_OF_BIRTH,PLACE_OF_BIRTH,COUNTRY_OF_BIRTH,SEX,AGE,MARITAL_STATUS,NATIONALITY,STATE_OF_ORIGIN,MOTHER_MAIDEN_NAME,DISABILITY,COMPLEXION,NUMBER_OF_CHILDREN,RELIGION,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS,BRANCH_CODE")] IndCustomerBioData indCustomerBioData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indCustomerBioData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(indCustomerBioData);
        }

        // GET: ManageCust/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_BIO_DATA indCustomerBioData = db.CDMA_INDIVIDUAL_BIO_DATA.Find(id);
            if (indCustomerBioData == null)
            {
                return HttpNotFound();
            }
            return View(indCustomerBioData);
        }

        // POST: ManageCust/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_INDIVIDUAL_BIO_DATA indCustomerBioData = db.CDMA_INDIVIDUAL_BIO_DATA.Find(id);
            db.CDMA_INDIVIDUAL_BIO_DATA.Remove(indCustomerBioData);
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
