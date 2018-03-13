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
    public class CustomerIncomeController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: CustomerIncome
        public ActionResult Index()
        {
            return View(db.CDMA_CUSTOMER_INCOME.ToList());
        }

        // GET: CustomerIncome/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME = db.CDMA_CUSTOMER_INCOME.Find(id);
            if (cDMA_CUSTOMER_INCOME == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_CUSTOMER_INCOME);
        }

        // GET: CustomerIncome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerIncome/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,INCOME_BAND,INITIAL_DEPOSIT,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_CUSTOMER_INCOME.Add(cDMA_CUSTOMER_INCOME);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_CUSTOMER_INCOME);
        }

        // GET: CustomerIncome/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME = db.CDMA_CUSTOMER_INCOME.Find(id);
            if (cDMA_CUSTOMER_INCOME == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_CUSTOMER_INCOME);
        }

        // POST: CustomerIncome/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_NO,INCOME_BAND,INITIAL_DEPOSIT,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDMA_CUSTOMER_INCOME).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_CUSTOMER_INCOME);
        }

        // GET: CustomerIncome/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME = db.CDMA_CUSTOMER_INCOME.Find(id);
            if (cDMA_CUSTOMER_INCOME == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_CUSTOMER_INCOME);
        }

        // POST: CustomerIncome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME = db.CDMA_CUSTOMER_INCOME.Find(id);
            db.CDMA_CUSTOMER_INCOME.Remove(cDMA_CUSTOMER_INCOME);
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
