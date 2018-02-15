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
    public class ContactDetailsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: ContactDetails
        public ActionResult Index()
        {
            return View(db.CDMA_INDIVIDUAL_CONTACT_DETAIL.ToList());
        }

        // GET: ContactDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Find(id);
            if (cDMA_INDIVIDUAL_CONTACT_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_CONTACT_DETAIL);
        }

        // GET: ContactDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,MOBILE_NO,EMAIL_ADDRESS,MAILING_ADDRESS,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Add(cDMA_INDIVIDUAL_CONTACT_DETAIL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_INDIVIDUAL_CONTACT_DETAIL);
        }

        // GET: ContactDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Find(id);
            if (cDMA_INDIVIDUAL_CONTACT_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_CONTACT_DETAIL);
        }

        // POST: ContactDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_NO,MOBILE_NO,EMAIL_ADDRESS,MAILING_ADDRESS,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDMA_INDIVIDUAL_CONTACT_DETAIL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_INDIVIDUAL_CONTACT_DETAIL);
        }

        // GET: ContactDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Find(id);
            if (cDMA_INDIVIDUAL_CONTACT_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_CONTACT_DETAIL);
        }

        // POST: ContactDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Find(id);
            db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Remove(cDMA_INDIVIDUAL_CONTACT_DETAIL);
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
