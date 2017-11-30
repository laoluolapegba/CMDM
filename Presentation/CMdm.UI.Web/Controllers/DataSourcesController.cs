using CMdm.Data;
using CMdm.Entities.Domain.Mdm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Cdma.Web.Controllers
{
    public class DataSourcesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: DataSources
        public ActionResult Index()
        {
            var mdmDQDataSources = db.MdmDQDataSources.Include(m => m.MdmDQDsTypes);
            return View(mdmDQDataSources.ToList());
        }

        // GET: DataSources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDQDataSource mdmDQDataSource = db.MdmDQDataSources.Find(id);
            if (mdmDQDataSource == null)
            {
                return HttpNotFound();
            }
            return View(mdmDQDataSource);
        }

        // GET: DataSources/Create
        public ActionResult Create()
        {
            ViewBag.DS_TYPE = new SelectList(db.MdmDQDsTypes, "TYPE_ID", "TYPE_NAME");
            return View();
        }

        // POST: DataSources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MdmDQDataSource mdmDQDataSource)
        {
            if (ModelState.IsValid)
            {
                db.MdmDQDataSources.Add(mdmDQDataSource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DS_TYPE = new SelectList(db.MdmDQDsTypes, "TYPE_ID", "TYPE_NAME", mdmDQDataSource.DS_TYPE);
            return View(mdmDQDataSource);
        }

        // GET: DataSources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDQDataSource mdmDQDataSource = db.MdmDQDataSources.Find(id);
            if (mdmDQDataSource == null)
            {
                return HttpNotFound();
            }
            ViewBag.DS_TYPE = new SelectList(db.MdmDQDsTypes, "TYPE_ID", "TYPE_NAME", mdmDQDataSource.DS_TYPE);
            return View(mdmDQDataSource);
        }

        // POST: DataSources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MdmDQDataSource mdmDQDataSource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mdmDQDataSource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DS_TYPE = new SelectList(db.MdmDQDsTypes, "TYPE_ID", "TYPE_NAME", mdmDQDataSource.DS_TYPE);
            return View(mdmDQDataSource);
        }

        // GET: DataSources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDQDataSource mdmDQDataSource = db.MdmDQDataSources.Find(id);
            if (mdmDQDataSource == null)
            {
                return HttpNotFound();
            }
            return View(mdmDQDataSource);
        }

        // POST: DataSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MdmDQDataSource mdmDQDataSource = db.MdmDQDataSources.Find(id);
            db.MdmDQDataSources.Remove(mdmDQDataSource);
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
