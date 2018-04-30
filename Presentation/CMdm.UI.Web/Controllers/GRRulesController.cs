using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.GoldenRecord;

namespace CMdm.UI.Web.Controllers
{
    public class GRRulesController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: GRRules
        public ActionResult Index()
        {
            return View(db.GoldenRecordRules.ToList());
        }

        // GET: GRRules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldenRecordRules goldenRecordRules = db.GoldenRecordRules.Find(id);
            if (goldenRecordRules == null)
            {
                return HttpNotFound();
            }
            return View(goldenRecordRules);
        }

        // GET: GRRules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GRRules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RULE_ID,RULE_NAME,RULE_DESCRIPTION,RULE_STATUS")] GoldenRecordRules goldenRecordRules)
        {
            if (ModelState.IsValid)
            {
                db.GoldenRecordRules.Add(goldenRecordRules);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goldenRecordRules);
        }

        // GET: GRRules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldenRecordRules goldenRecordRules = db.GoldenRecordRules.Find(id);
            if (goldenRecordRules == null)
            {
                return HttpNotFound();
            }
            return View(goldenRecordRules);
        }

        // POST: GRRules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RULE_ID,RULE_NAME,RULE_DESCRIPTION,RULE_STATUS")] GoldenRecordRules goldenRecordRules)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goldenRecordRules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goldenRecordRules);
        }

        // GET: GRRules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldenRecordRules goldenRecordRules = db.GoldenRecordRules.Find(id);
            if (goldenRecordRules == null)
            {
                return HttpNotFound();
            }
            return View(goldenRecordRules);
        }

        // POST: GRRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GoldenRecordRules goldenRecordRules = db.GoldenRecordRules.Find(id);
            db.GoldenRecordRules.Remove(goldenRecordRules);
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
