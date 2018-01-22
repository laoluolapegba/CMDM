using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.Mdm;

namespace CMdm.UI.Web.Controllers
{
    public class DQIController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: DQI
        public ActionResult Index()
        {
            return View(db.MDM_WEIGHTS.ToList());
        }

        // GET: DQI/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            if (mdmWeights == null)
            {
                return HttpNotFound();
            }
            return View(mdmWeights);
        }

        // GET: DQI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DQI/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WEIGHT_ID,WEIGHT_VALUE,CREATED_BY,CREATED_DATE,LAST_MODIFIED_BY,LAST_MODIFIED_DATE,RECORD_STATUS")] MdmWeights mdmWeights)
        {
            if (ModelState.IsValid)
            {
                db.MDM_WEIGHTS.Add(mdmWeights);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mdmWeights);
        }

        // GET: DQI/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            if (mdmWeights == null)
            {
                return HttpNotFound();
            }
            return View(mdmWeights);
        }

        // POST: DQI/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WEIGHT_ID,WEIGHT_VALUE,CREATED_BY,CREATED_DATE,LAST_MODIFIED_BY,LAST_MODIFIED_DATE,RECORD_STATUS")] MdmWeights mdmWeights)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mdmWeights).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mdmWeights);
        }

        // GET: DQI/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            if (mdmWeights == null)
            {
                return HttpNotFound();
            }
            return View(mdmWeights);
        }

        // POST: DQI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            db.MDM_WEIGHTS.Remove(mdmWeights);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DQIParams()
        {
            var dqiParams = db.MdmDqiParams.Include(m => m.MdmWeights); //.Include(m => m.MdmRegex);
            return View(dqiParams.ToList());
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
