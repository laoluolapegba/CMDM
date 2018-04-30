using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        public ActionResult Index(int? page, string branch)
        {
            var curBranchList = db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME);

            ViewBag.BRANCH = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME");

            int pageSize = 50;
            int pageNumber = (page ?? 1);

            var PhoneValidation = from s in db.CMDM_PHONEVALIDATION_RESULTS
                                 select s;

            if(!String.IsNullOrEmpty(branch))
            {
                PhoneValidation = PhoneValidation.Where(s => s.BRANCH_CODE.ToUpper().Contains(branch.ToUpper())).OrderBy(s => s.CUSTOMER_NO);
            }
            else
            {
                PhoneValidation = PhoneValidation.OrderBy(s => s.CUSTOMER_NO);
            }
            return View(PhoneValidation.ToPagedList(page ?? 1, pageSize));
        }


        public void WriteTsv<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }

        public void ExportListFromTsv()
        {
            //   CMDM_PHONEVALIDATION_RESULTS data = new CMDM_PHONEVALIDATION_RESULTS();
            List<CMDM_PHONEVALIDATION_RESULTS> data = new List<CMDM_PHONEVALIDATION_RESULTS>();
            if (this.Session["BRANCH"] != null)
            {
                var BRANCH = this.Session["BRANCH"];
                data = db.CMDM_PHONEVALIDATION_RESULTS.Where(s => s.BRANCH_CODE == BRANCH.ToString()).ToList();
                //return View(result);

            }
            else
            {
                data = db.CMDM_PHONEVALIDATION_RESULTS.ToList();


            }
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Customers.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            WriteTsv(data, Response.Output);
            Response.End();
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