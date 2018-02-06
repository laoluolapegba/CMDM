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
    public class IndividualAddressController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: IndividualAddress
        public ActionResult Index()
        {
            return View(db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.ToList());
        }

        // GET: IndividualAddress/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Find(id);
            if (cDMA_INDIVIDUAL_ADDRESS_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_ADDRESS_DETAIL);
        }

        // GET: IndividualAddress/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IndividualAddress/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,RESIDENTIAL_ADDRESS,CITY_TOWN_OF_RESIDENCE,LGA_OF_RESIDENCE,NEAREST_BUS_STOP_LANDMARK,STATE_OF_RESIDENCE,COUNTRY_OF_RESIDENCE,RESIDENCE_OWNED_OR_RENT,ZIP_POSTAL_CODE,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Add(cDMA_INDIVIDUAL_ADDRESS_DETAIL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_INDIVIDUAL_ADDRESS_DETAIL);
        }

        // GET: IndividualAddress/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Find(id);
            if (cDMA_INDIVIDUAL_ADDRESS_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_ADDRESS_DETAIL);
        }

        // POST: IndividualAddress/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_NO,RESIDENTIAL_ADDRESS,CITY_TOWN_OF_RESIDENCE,LGA_OF_RESIDENCE,NEAREST_BUS_STOP_LANDMARK,STATE_OF_RESIDENCE,COUNTRY_OF_RESIDENCE,RESIDENCE_OWNED_OR_RENT,ZIP_POSTAL_CODE,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDMA_INDIVIDUAL_ADDRESS_DETAIL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_INDIVIDUAL_ADDRESS_DETAIL);
        }

        // GET: IndividualAddress/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Find(id);
            if (cDMA_INDIVIDUAL_ADDRESS_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_ADDRESS_DETAIL);
        }

        // POST: IndividualAddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Find(id);
            db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Remove(cDMA_INDIVIDUAL_ADDRESS_DETAIL);
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
