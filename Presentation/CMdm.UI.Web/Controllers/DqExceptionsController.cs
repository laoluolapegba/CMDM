using CMdm.Data;
using CMdm.Entities.Domain.Dqi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Controllers
{
    public class DqExceptionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: MdmDqRunExceptions
        public ActionResult Index()
        {
            var mdmDqRunExceptions = db.MdmDqRunExceptions.Include(m => m.MdmDQPriorities).Include(m => m.MdmDQQueStatuses);
            return View(mdmDqRunExceptions.ToList());
        }

        // GET: MdmDqRunExceptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDqRunException mdmDqRunException = db.MdmDqRunExceptions.Find(id);
            if (mdmDqRunException == null)
            {
                return HttpNotFound();
            }
            return View(mdmDqRunException);
        }

        // GET: MdmDqRunExceptions/Create
        public ActionResult Create()
        {
            ViewBag.ISSUE_PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION");
            ViewBag.ISSUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION");
            return View();
        }

        // POST: MdmDqRunExceptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EXCEPTION_ID,RULE_ID,RULE_NAME,CUST_ID,BRANCH_CODE,BRANCH_NAME,RUN_DATE,RUN_BY,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,ISSUE_STATUS,ISSUE_PRIORITY")] MdmDqRunException mdmDqRunException)
        {
            if (ModelState.IsValid)
            {
                db.MdmDqRunExceptions.Add(mdmDqRunException);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ISSUE_PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDqRunException.ISSUE_PRIORITY);
            ViewBag.ISSUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION", mdmDqRunException.ISSUE_STATUS);
            return View(mdmDqRunException);
        }

        // GET: MdmDqRunExceptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDqRunException mdmDqRunException = db.MdmDqRunExceptions.Find(id);
            if (mdmDqRunException == null)
            {
                return HttpNotFound();
            }
            ViewBag.ISSUE_PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDqRunException.ISSUE_PRIORITY);
            ViewBag.ISSUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION", mdmDqRunException.ISSUE_STATUS);
            return View(mdmDqRunException);
        }

        // POST: MdmDqRunExceptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EXCEPTION_ID,RULE_ID,RULE_NAME,CUST_ID,BRANCH_CODE,BRANCH_NAME,RUN_DATE,RUN_BY,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,ISSUE_STATUS,ISSUE_PRIORITY")] MdmDqRunException mdmDqRunException)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mdmDqRunException).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ISSUE_PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDqRunException.ISSUE_PRIORITY);
            ViewBag.ISSUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION", mdmDqRunException.ISSUE_STATUS);
            return View(mdmDqRunException);
        }

        // GET: MdmDqRunExceptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDqRunException mdmDqRunException = db.MdmDqRunExceptions.Find(id);
            if (mdmDqRunException == null)
            {
                return HttpNotFound();
            }
            return View(mdmDqRunException);
        }

        // POST: MdmDqRunExceptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MdmDqRunException mdmDqRunException = db.MdmDqRunExceptions.Find(id);
            db.MdmDqRunExceptions.Remove(mdmDqRunException);
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
