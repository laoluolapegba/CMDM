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
    public class MdmDimensionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: MdmDimensions
        public ActionResult Index()
        {
            return View(db.MDM_AGGR_DIMENSION.ToList());
        }

        // GET: MdmDimensions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmAggrDimensions mdmAggrDimensions = db.MDM_AGGR_DIMENSION.Find(id);
            if (mdmAggrDimensions == null)
            {
                return HttpNotFound();
            }
            return View(mdmAggrDimensions);
        }

        // GET: MdmDimensions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MdmDimensions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DIMENSIONID,DIMENSION_NAME,CREATED_BY,CREATED_DATE,LAST_MODIFIED_BY,LAST_MODIFIED_DATE,RECORD_STATUS")] MdmAggrDimensions mdmAggrDimensions)
        {
            if (ModelState.IsValid)
            {
                db.MDM_AGGR_DIMENSION.Add(mdmAggrDimensions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mdmAggrDimensions);
        }

        // GET: MdmDimensions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmAggrDimensions mdmAggrDimensions = db.MDM_AGGR_DIMENSION.Find(id);
            if (mdmAggrDimensions == null)
            {
                return HttpNotFound();
            }
            return View(mdmAggrDimensions);
        }

        // POST: MdmDimensions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DIMENSIONID,DIMENSION_NAME,CREATED_BY,CREATED_DATE,LAST_MODIFIED_BY,LAST_MODIFIED_DATE,RECORD_STATUS")] MdmAggrDimensions mdmAggrDimensions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mdmAggrDimensions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mdmAggrDimensions);
        }

        // GET: MdmDimensions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmAggrDimensions mdmAggrDimensions = db.MDM_AGGR_DIMENSION.Find(id);
            if (mdmAggrDimensions == null)
            {
                return HttpNotFound();
            }
            return View(mdmAggrDimensions);
        }

        // POST: MdmDimensions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MdmAggrDimensions mdmAggrDimensions = db.MDM_AGGR_DIMENSION.Find(id);
            db.MDM_AGGR_DIMENSION.Remove(mdmAggrDimensions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetDims()
        {
            var db = new AppDbContext();
            var dims = from MdmAggrDimensions in db.MDM_AGGR_DIMENSION
                          select new
                          {
                              DIMENSIONID = MdmAggrDimensions.DIMENSIONID,
                              DIMENSION_NAME = MdmAggrDimensions.DIMENSION_NAME
                          };

            return Json(dims.OrderBy(a => a.DIMENSION_NAME), JsonRequestBehavior.AllowGet);
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
