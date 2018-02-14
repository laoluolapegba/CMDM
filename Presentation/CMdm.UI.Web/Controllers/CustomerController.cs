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

namespace CMdm.UI.Web.Controllers
{
    public class CustomerController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            return View(db.CDMA_INDIVIDUAL_BIO_DATA.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.Find(id);
            if (cDMA_INDIVIDUAL_BIO_DATA == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_BIO_DATA);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,TITLE,SURNAME,FIRST_NAME,OTHER_NAME,NICKNAME_ALIAS,DATE_OF_BIRTH,PLACE_OF_BIRTH,COUNTRY_OF_BIRTH,SEX,AGE,MARITAL_STATUS,NATIONALITY,STATE_OF_ORIGIN,MOTHER_MAIDEN_NAME,DISABILITY,COMPLEXION,NUMBER_OF_CHILDREN,RELIGION,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS,BRANCH_CODE")] CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_INDIVIDUAL_BIO_DATA.Add(cDMA_INDIVIDUAL_BIO_DATA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_INDIVIDUAL_BIO_DATA);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.Find(id);
            ViewBag.record = cDMA_INDIVIDUAL_BIO_DATA;
            if (cDMA_INDIVIDUAL_BIO_DATA == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_BIO_DATA);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_NO,TITLE,SURNAME,FIRST_NAME,OTHER_NAME,NICKNAME_ALIAS,DATE_OF_BIRTH,PLACE_OF_BIRTH,COUNTRY_OF_BIRTH,SEX,AGE,MARITAL_STATUS,NATIONALITY,STATE_OF_ORIGIN,MOTHER_MAIDEN_NAME,DISABILITY,COMPLEXION,NUMBER_OF_CHILDREN,RELIGION,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS,BRANCH_CODE")] CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDMA_INDIVIDUAL_BIO_DATA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_INDIVIDUAL_BIO_DATA);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.Find(id);
            if (cDMA_INDIVIDUAL_BIO_DATA == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_BIO_DATA);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.Find(id);
            db.CDMA_INDIVIDUAL_BIO_DATA.Remove(cDMA_INDIVIDUAL_BIO_DATA);
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
