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

namespace Cdma.Web.Controllers
{
    [ValidateInput(false)]
    public class DqRulesControllerOld : Controller
    {
        private AppDbContext db = new AppDbContext();
        
        // GET: MdmDqRules
        public ActionResult Index()
        {
            var mdmDqRules = db.MdmDqRules.Include(m => m.MdmDQDataSources).Include(m => m.MdmDqRunSchedules);
            return View(mdmDqRules.ToList());
        }

        // GET: MdmDqRules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDqRule mdmDqRule = db.MdmDqRules.Find(id);
            if (mdmDqRule == null)
            {
                return HttpNotFound();
            }
            return View(mdmDqRule);
        }

        // GET: MdmDqRules/Create
        public ActionResult Create()
        {
            ViewBag.DATA_SOURCE_ID = new SelectList(db.MdmDQDataSources, "DS_ID", "DS_USERNAME");
            ViewBag.RUN_SCHEDULE = new SelectList(db.MdmDqRunSchedules, "SCHEDULE_ID", "SCHEDULE_DESCRIPTION");
            return View();
        }

        // POST: MdmDqRules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "RECORD_ID,DATA_SOURCE_ID,CATALOG_NAME,RULE_NAME,POP_QUERY,EXCEPTION_QUERY,DESCRIPTION_RESOLUTION,RUN_SCHEDULE")]
        public ActionResult Create( MdmDqRule mdmDqRule)
        {
            if (ModelState.IsValid)
            {
                db.MdmDqRules.Add(mdmDqRule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DATA_SOURCE_ID = new SelectList(db.MdmDQDataSources, "DS_ID", "DS_USERNAME", mdmDqRule.DATA_SOURCE_ID);
            ViewBag.RUN_SCHEDULE = new SelectList(db.MdmDqRunSchedules, "SCHEDULE_ID", "SCHEDULE_DESCRIPTION", mdmDqRule.RUN_SCHEDULE);
            return View(mdmDqRule);
        }

        // GET: MdmDqRules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDqRule mdmDqRule = db.MdmDqRules.Find(id);
            if (mdmDqRule == null)
            {
                return HttpNotFound();
            }
            ViewBag.DATA_SOURCE_ID = new SelectList(db.MdmDQDataSources, "DS_ID", "DS_USERNAME", mdmDqRule.DATA_SOURCE_ID);
            ViewBag.RUN_SCHEDULE = new SelectList(db.MdmDqRunSchedules, "SCHEDULE_ID", "SCHEDULE_DESCRIPTION", mdmDqRule.RUN_SCHEDULE);
            return View(mdmDqRule);
        }

        // POST: MdmDqRules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "RECORD_ID,DATA_SOURCE_ID,CATALOG_NAME,RULE_NAME,POP_QUERY,EXCEPTION_QUERY,DESCRIPTION_RESOLUTION,RUN_SCHEDULE")] 
        public ActionResult Edit(MdmDqRule mdmDqRule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mdmDqRule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DATA_SOURCE_ID = new SelectList(db.MdmDQDataSources, "DS_ID", "DS_USERNAME", mdmDqRule.DATA_SOURCE_ID);
            ViewBag.RUN_SCHEDULE = new SelectList(db.MdmDqRunSchedules, "SCHEDULE_ID", "SCHEDULE_DESCRIPTION", mdmDqRule.RUN_SCHEDULE);
            return View(mdmDqRule);
        }

        // GET: MdmDqRules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDqRule mdmDqRule = db.MdmDqRules.Find(id);
            if (mdmDqRule == null)
            {
                return HttpNotFound();
            }
            return View(mdmDqRule);
        }

        // POST: MdmDqRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MdmDqRule mdmDqRule = db.MdmDqRules.Find(id);
            db.MdmDqRules.Remove(mdmDqRule);
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
