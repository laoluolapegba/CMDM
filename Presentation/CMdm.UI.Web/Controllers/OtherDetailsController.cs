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
    public class OtherDetailsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: OtherDetails
        public ActionResult Index()
        {
            return View(db.CDMA_INDIVIDUAL_OTHER_DETAILS.ToList());
        }

        // GET: OtherDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Find(id);
            if (cDMA_INDIVIDUAL_OTHER_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_OTHER_DETAILS);
        }

        // GET: OtherDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OtherDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,TIN_NO,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_INDIVIDUAL_OTHER_DETAILS.Add(cDMA_INDIVIDUAL_OTHER_DETAILS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_INDIVIDUAL_OTHER_DETAILS);
        }

        // GET: OtherDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Find(id);
            if (cDMA_INDIVIDUAL_OTHER_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_OTHER_DETAILS);
        }

        // POST: OtherDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_NO,TIN_NO,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDMA_INDIVIDUAL_OTHER_DETAILS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_INDIVIDUAL_OTHER_DETAILS);
        }

        // GET: OtherDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Find(id);
            if (cDMA_INDIVIDUAL_OTHER_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_OTHER_DETAILS);
        }

        // POST: OtherDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Find(id);
            db.CDMA_INDIVIDUAL_OTHER_DETAILS.Remove(cDMA_INDIVIDUAL_OTHER_DETAILS);
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
