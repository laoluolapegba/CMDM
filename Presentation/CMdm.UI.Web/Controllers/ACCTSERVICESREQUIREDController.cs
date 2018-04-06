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
    public class AcctServicesRequiredController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: ACCTSERVICESREQUIRED
        public ActionResult Index()
        {
            return View(db.CDMA_ACCT_SERVICES_REQUIRED.ToList());
        }

        // GET: ACCTSERVICESREQUIRED/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED = db.CDMA_ACCT_SERVICES_REQUIRED.Find(id);
            if (cDMA_ACCT_SERVICES_REQUIRED == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_ACCT_SERVICES_REQUIRED);
        }

        // GET: ACCTSERVICESREQUIRED/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ACCTSERVICESREQUIRED/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,ACCOUNT_NUMBER,CARD_PREFERENCE,ELECTRONIC_BANKING_PREFERENCE,STATEMENT_PREFERENCES,TRANSACTION_ALERT_PREFERENCE,STATEMENT_FREQUENCY,CHEQUE_BOOK_REQUISITION,CHEQUE_LEAVES_REQUIRED,CHEQUE_CONFIRMATION,CHEQUE_CONFIRMATION_THRESHOLD,ONLINE_TRANSFER_LIMIT,TOKEN,ACCOUNT_SIGNATORY,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_ACCT_SERVICES_REQUIRED.Add(cDMA_ACCT_SERVICES_REQUIRED);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_ACCT_SERVICES_REQUIRED);
        }

        // GET: ACCTSERVICESREQUIRED/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED = db.CDMA_ACCT_SERVICES_REQUIRED.Find(id);
            if (cDMA_ACCT_SERVICES_REQUIRED == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_ACCT_SERVICES_REQUIRED);
        }

        // POST: ACCTSERVICESREQUIRED/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_NO,ACCOUNT_NUMBER,CARD_PREFERENCE,ELECTRONIC_BANKING_PREFERENCE,STATEMENT_PREFERENCES,TRANSACTION_ALERT_PREFERENCE,STATEMENT_FREQUENCY,CHEQUE_BOOK_REQUISITION,CHEQUE_LEAVES_REQUIRED,CHEQUE_CONFIRMATION,CHEQUE_CONFIRMATION_THRESHOLD,ONLINE_TRANSFER_LIMIT,TOKEN,ACCOUNT_SIGNATORY,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDMA_ACCT_SERVICES_REQUIRED).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_ACCT_SERVICES_REQUIRED);
        }

        // GET: ACCTSERVICESREQUIRED/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED = db.CDMA_ACCT_SERVICES_REQUIRED.Find(id);
            if (cDMA_ACCT_SERVICES_REQUIRED == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_ACCT_SERVICES_REQUIRED);
        }

        // POST: ACCTSERVICESREQUIRED/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED = db.CDMA_ACCT_SERVICES_REQUIRED.Find(id);
            db.CDMA_ACCT_SERVICES_REQUIRED.Remove(cDMA_ACCT_SERVICES_REQUIRED);
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
