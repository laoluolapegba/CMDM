using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.UI.Web.BLL;
using CMdm.Data;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Entities.Domain.Dqi;

namespace CMdm.UI.Web.Controllers
{
    public class DQQueController : Controller
    {
        #region Fields

        private readonly IDqQueService _dqQueService;
        private AppDbContext db = new AppDbContext();
        private DQQueBiz bizrule;
        #endregion
        #region Constructors
        public DQQueController(IDqQueService dqQueService)
        {
            bizrule = new DQQueBiz();
            this._dqQueService = dqQueService;
        }
        #endregion

        #region Methods
        #region Que list / create / edit / delete
        // GET: MdmDQQues
        public ActionResult Index()
        {
            //|TODO implement a permission provider Service
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageDataQualityQue))
            //    return AccessDeniedView();

            var identity = ((CustomPrincipal)User).CustomIdentity;
            ViewBag.BrnQueCount = bizrule.GetDQQuesCountbyBrn(identity.BranchId);
            var mdmDQQues = db.MdmDQQues.Include(m => m.MdmDQImpacts).Include(m => m.MdmDQPriorities).Include(m => m.MdmDQQueStatuses);
            return View(mdmDQQues.ToList());
        }
        public ActionResult Indexa()
        {
            var mdmDQQues = db.MdmDQQues.Include(m => m.MdmDQImpacts).Include(m => m.MdmDQPriorities).Include(m => m.MdmDQQueStatuses);
            return View(mdmDQQues.ToList());
        }
        public ActionResult DqDetails(int ruleid, int branchid)
        {
            var identity = ((CustomPrincipal)User).CustomIdentity;
            //ViewBag.BrnQueCount = bizrule.GetDQQuesCountbyBrn(identity.BranchId);
            var mdmDqRunExceptions = db.MdmDqRunExceptions.Where(a=>a.RULE_ID == ruleid).Where(a=>a.BRANCH_CODE==branchid).Include(m => m.MdmDQPriorities).Include(m => m.MdmDQQueStatuses);
            return View(mdmDqRunExceptions.ToList());
        }

        // GET: MdmDQQues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDQQue mdmDQQue = db.MdmDQQues.Find(id);
            if (mdmDQQue == null)
            {
                return HttpNotFound();
            }
            return View(mdmDQQue);
        }

        // GET: MdmDQQues/Create
        public ActionResult Create()
        {
            ViewBag.IMPACT_LEVEL = new SelectList(db.MdmDQImpacts, "IMPACT_CODE", "IMPACT_DESCRIPTION");
            ViewBag.PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION");
            ViewBag.QUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION");
            return View();
        }

        // POST: MdmDQQues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MdmDQQue mdmDQQue)
        {
            if (ModelState.IsValid)
            {
                db.MdmDQQues.Add(mdmDQQue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IMPACT_LEVEL = new SelectList(db.MdmDQImpacts, "IMPACT_CODE", "IMPACT_DESCRIPTION", mdmDQQue.IMPACT_LEVEL);
            ViewBag.PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDQQue.PRIORITY);
            ViewBag.QUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION", mdmDQQue.QUE_STATUS);
            return View(mdmDQQue);
        }

        // GET: MdmDQQues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDQQue mdmDQQue = db.MdmDQQues.Find(id);
            if (mdmDQQue == null)
            {
                return HttpNotFound();
            }
            ViewBag.IMPACT_LEVEL = new SelectList(db.MdmDQImpacts, "IMPACT_CODE", "IMPACT_DESCRIPTION", mdmDQQue.IMPACT_LEVEL);
            ViewBag.PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDQQue.PRIORITY);
            ViewBag.QUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION", mdmDQQue.QUE_STATUS);
            return View(mdmDQQue);
        }

        // POST: MdmDQQues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RECORD_ID,DATA_SOURCE,CATALOG_NAME,ERROR_CODE,ERROR_DESC,DQ_PROCESS_NAME,IMPACT_LEVEL,PRIORITY,QUE_STATUS,CREATED_BY,CREATED_DATE")] MdmDQQue mdmDQQue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mdmDQQue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IMPACT_LEVEL = new SelectList(db.MdmDQImpacts, "IMPACT_CODE", "IMPACT_DESCRIPTION", mdmDQQue.IMPACT_LEVEL);
            ViewBag.PRIORITY = new SelectList(db.MdmDQPriorities, "PRIORITY_CODE", "PRIORITY_DESCRIPTION", mdmDQQue.PRIORITY);
            ViewBag.QUE_STATUS = new SelectList(db.MdmDQQueStatuses, "STATUS_CODE", "STATUS_DESCRIPTION", mdmDQQue.QUE_STATUS);
            return View(mdmDQQue);
        }

        // GET: MdmDQQues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmDQQue mdmDQQue = db.MdmDQQues.Find(id);
            if (mdmDQQue == null)
            {
                return HttpNotFound();
            }
            return View(mdmDQQue);
        }

        // POST: MdmDQQues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MdmDQQue mdmDQQue = db.MdmDQQues.Find(id);
            db.MdmDQQues.Remove(mdmDQQue);
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
        #endregion
        #endregion
    }
}
