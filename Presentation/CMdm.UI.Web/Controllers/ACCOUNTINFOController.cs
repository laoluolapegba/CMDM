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
    public class AccountInfoController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: ACCOUNTINFO
        public ActionResult Index()
        {
            return View(db.CDMA_ACCOUNT_INFO.ToList());
        }

        // GET: ACCOUNTINFO/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO = db.CDMA_ACCOUNT_INFO.Find(id);
            if (cDMA_ACCOUNT_INFO == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_ACCOUNT_INFO);
        }

        // GET: ACCOUNTINFO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ACCOUNTINFO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,TYPE_OF_ACCOUNT,ACCOUNT_NUMBER,ACCOUNT_OFFICER,ACCOUNT_TITLE,BRANCH,BRANCH_CLASS,BUSINESS_DIVISION,BUSINESS_SEGMENT,BUSINESS_SIZE,BVN_NUMBER,CAV_REQUIRED,CHEQUE_CONFIRM_THRESHLDRANGE,ONLINE_TRANSFER_LIMIT_RANGE,CUSTOMER_IC,CUSTOMER_SEGMENT,CUSTOMER_TYPE,OPERATING_INSTRUCTION,ORIGINATING_BRANCH,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_ACCOUNT_INFO.Add(cDMA_ACCOUNT_INFO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_ACCOUNT_INFO);
        }

        // GET: ACCOUNTINFO/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO = db.CDMA_ACCOUNT_INFO.Find(id);
            if (cDMA_ACCOUNT_INFO == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_ACCOUNT_INFO);
        }

        // POST: ACCOUNTINFO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_NO,TYPE_OF_ACCOUNT,ACCOUNT_NUMBER,ACCOUNT_OFFICER,ACCOUNT_TITLE,BRANCH,BRANCH_CLASS,BUSINESS_DIVISION,BUSINESS_SEGMENT,BUSINESS_SIZE,BVN_NUMBER,CAV_REQUIRED,CHEQUE_CONFIRM_THRESHLDRANGE,ONLINE_TRANSFER_LIMIT_RANGE,CUSTOMER_IC,CUSTOMER_SEGMENT,CUSTOMER_TYPE,OPERATING_INSTRUCTION,ORIGINATING_BRANCH,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDMA_ACCOUNT_INFO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_ACCOUNT_INFO);
        }

        // GET: ACCOUNTINFO/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO = db.CDMA_ACCOUNT_INFO.Find(id);
            if (cDMA_ACCOUNT_INFO == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_ACCOUNT_INFO);
        }

        // POST: ACCOUNTINFO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO = db.CDMA_ACCOUNT_INFO.Find(id);
            db.CDMA_ACCOUNT_INFO.Remove(cDMA_ACCOUNT_INFO);
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
