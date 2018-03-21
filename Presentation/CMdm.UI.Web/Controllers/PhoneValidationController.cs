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
using PagedList;

namespace CMdm.UI.Web.Controllers
{
    public class PhoneValidationController : Controller
    {
        private AppDbContext db = new AppDbContext();
      
        // GET: PhoneValidation
        public ActionResult Index(int? page)
        {
            ViewBag.BRANCH = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME");
           

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            if (Request != null && (page ==null))
            {
                this.Session["BRANCH"] = Request["BRANCH"];
            }
            else
            {
                this.Session["BRANCH"] = null;
            }
           

            if (this.Session["BRANCH"] != null)
            {
                var BRANCH = this.Session["BRANCH"];
               return  View(db.CMDM_PHONEVALIDATION_RESULTS.Where(s => s.BRANCH_CODE == BRANCH.ToString()).OrderBy(i => i.CUSTOMER_NO).ToPagedList(page ?? 1, pageSize));
                 
            }
            else
            {
                return View(db.CMDM_PHONEVALIDATION_RESULTS.OrderBy(i => i.CUSTOMER_NO).ToPagedList(page ?? 1, pageSize));

            }

             
        }

        // GET: PhoneValidation/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_PHONEVALIDATION_RESULTS cMDM_PHONEVALIDATION_RESULTS = db.CMDM_PHONEVALIDATION_RESULTS.Find(id);
            if (cMDM_PHONEVALIDATION_RESULTS == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_PHONEVALIDATION_RESULTS);
        }

        // GET: PhoneValidation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneValidation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,BRANCH_CODE,CUST_LAST_NAME,CUST_MIDDLE_NAME,CUST_FIRST_NAME,LAST_RUN_DATE,REASON")] CMDM_PHONEVALIDATION_RESULTS cMDM_PHONEVALIDATION_RESULTS)
        {
            if (ModelState.IsValid)
            {
                db.CMDM_PHONEVALIDATION_RESULTS.Add(cMDM_PHONEVALIDATION_RESULTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cMDM_PHONEVALIDATION_RESULTS);
        }

        // GET: PhoneValidation/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_PHONEVALIDATION_RESULTS cMDM_PHONEVALIDATION_RESULTS = db.CMDM_PHONEVALIDATION_RESULTS.Find(id);
            if (cMDM_PHONEVALIDATION_RESULTS == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_PHONEVALIDATION_RESULTS);
        }

        // POST: PhoneValidation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_NO,BRANCH_CODE,CUST_LAST_NAME,CUST_MIDDLE_NAME,CUST_FIRST_NAME,LAST_RUN_DATE,REASON")] CMDM_PHONEVALIDATION_RESULTS cMDM_PHONEVALIDATION_RESULTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMDM_PHONEVALIDATION_RESULTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cMDM_PHONEVALIDATION_RESULTS);
        }

        // GET: PhoneValidation/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CMDM_PHONEVALIDATION_RESULTS cMDM_PHONEVALIDATION_RESULTS = db.CMDM_PHONEVALIDATION_RESULTS.Find(id);
            if (cMDM_PHONEVALIDATION_RESULTS == null)
            {
                return HttpNotFound();
            }
            return View(cMDM_PHONEVALIDATION_RESULTS);
        }

        // POST: PhoneValidation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CMDM_PHONEVALIDATION_RESULTS cMDM_PHONEVALIDATION_RESULTS = db.CMDM_PHONEVALIDATION_RESULTS.Find(id);
            db.CMDM_PHONEVALIDATION_RESULTS.Remove(cMDM_PHONEVALIDATION_RESULTS);
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


public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}