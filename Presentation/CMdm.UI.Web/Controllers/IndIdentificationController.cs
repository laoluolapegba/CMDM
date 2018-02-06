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
    public class IndIdentificationController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: IndIdentification
        public ActionResult Index()
        {
            return View(db.CDMA_INDIVIDUAL_IDENTIFICATION.ToList());
        }

        // GET: IndIdentification/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.Find(id);
            if (cDMA_INDIVIDUAL_IDENTIFICATION == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_IDENTIFICATION);
        }

        // GET: IndIdentification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IndIdentification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,IDENTIFICATION_TYPE,ID_NO,ID_EXPIRY_DATE,ID_ISSUE_DATE,PLACE_OF_ISSUANCE,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_INDIVIDUAL_IDENTIFICATION.Add(cDMA_INDIVIDUAL_IDENTIFICATION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_INDIVIDUAL_IDENTIFICATION);
        }

        // GET: IndIdentification/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.Find(id);
            if (cDMA_INDIVIDUAL_IDENTIFICATION == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_IDENTIFICATION);
        }

        // POST: IndIdentification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_NO,IDENTIFICATION_TYPE,ID_NO,ID_EXPIRY_DATE,ID_ISSUE_DATE,PLACE_OF_ISSUANCE,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDMA_INDIVIDUAL_IDENTIFICATION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_INDIVIDUAL_IDENTIFICATION);
        }

        // GET: IndIdentification/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.Find(id);
            if (cDMA_INDIVIDUAL_IDENTIFICATION == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_IDENTIFICATION);
        }

        // POST: IndIdentification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.Find(id);
            db.CDMA_INDIVIDUAL_IDENTIFICATION.Remove(cDMA_INDIVIDUAL_IDENTIFICATION);
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
