using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.Dqi;

namespace CMdm.UI.Web.Controllers
{
    public class DQISummaryController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: DQISummary
        public ActionResult Index()
        {
            return View(db.CDMA_DQI_PROCESSING_RESULT.ToList());
        }

        // GET: DQISummary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_DQI_PROCESSING_RESULT cDMA_DQI_PROCESSING_RESULT = db.CDMA_DQI_PROCESSING_RESULT.Find(id);
            if (cDMA_DQI_PROCESSING_RESULT == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_DQI_PROCESSING_RESULT);
        }

        // GET: DQISummary/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DQISummary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PROCESS_ID,BRANCH_CODE,DQI_RESULT,PREVIOUS_DQI_RESULT")] CDMA_DQI_PROCESSING_RESULT cDMA_DQI_PROCESSING_RESULT)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_DQI_PROCESSING_RESULT.Add(cDMA_DQI_PROCESSING_RESULT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_DQI_PROCESSING_RESULT);
        }

        // GET: DQISummary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_DQI_PROCESSING_RESULT cDMA_DQI_PROCESSING_RESULT = db.CDMA_DQI_PROCESSING_RESULT.Find(id);
            if (cDMA_DQI_PROCESSING_RESULT == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_DQI_PROCESSING_RESULT);
        }

        // POST: DQISummary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PROCESS_ID,BRANCH_CODE,DQI_RESULT,PREVIOUS_DQI_RESULT")] CDMA_DQI_PROCESSING_RESULT cDMA_DQI_PROCESSING_RESULT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDMA_DQI_PROCESSING_RESULT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_DQI_PROCESSING_RESULT);
        }

        // GET: DQISummary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_DQI_PROCESSING_RESULT cDMA_DQI_PROCESSING_RESULT = db.CDMA_DQI_PROCESSING_RESULT.Find(id);
            if (cDMA_DQI_PROCESSING_RESULT == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_DQI_PROCESSING_RESULT);
        }

        // POST: DQISummary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CDMA_DQI_PROCESSING_RESULT cDMA_DQI_PROCESSING_RESULT = db.CDMA_DQI_PROCESSING_RESULT.Find(id);
            db.CDMA_DQI_PROCESSING_RESULT.Remove(cDMA_DQI_PROCESSING_RESULT);
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
