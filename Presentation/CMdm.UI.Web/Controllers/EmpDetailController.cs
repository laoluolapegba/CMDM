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
using CMdm.UI.Web.Models.Customer;
using CMdm.Framework.Controllers;
using CMdm.UI.Web.Helpers.CrossCutting.Security;


namespace CMdm.UI.Web.Controllers
{
    public class EmpDetailController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: EmpDetail
        public ActionResult Index()
        {
            var cDMA_EMPLOYMENT_DETAILS = db.CDMA_EMPLOYMENT_DETAILS.Include(c => c.Businessnature).Include(c => c.Indsegment).Include(c => c.Occupationtype).Include(c => c.Subsectortype);
            return View(cDMA_EMPLOYMENT_DETAILS.ToList());
        }

        // GET: EmpDetail/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_EMPLOYMENT_DETAILS cDMA_EMPLOYMENT_DETAILS = db.CDMA_EMPLOYMENT_DETAILS.Find(id);
            if (cDMA_EMPLOYMENT_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_EMPLOYMENT_DETAILS);
        }

        // GET: EmpDetail/Create
        public ActionResult Create(string id="")
        {
            EmpDetailsModel model = new EmpDetailsModel();
            if (id != "") model.CUSTOMER_NO = id;
            // ViewBag.SUB_SECTOR = new SelectList(db.CDMA_INDUSTRY_SUBSECTOR, "SUBSECTOR_CODE", "SUBSECTOR_NAME");
            ViewBag.SECTOR_CLASS = new SelectList(db.CDMA_OCCUPATION_LIST, "OCCUPATION_CODE", "OCCUPATION" );
            ViewBag.SUB_SECTOR = new SelectList(db.CDMA_INDUSTRY_SUBSECTOR, "SUBSECTOR_CODE", "SUBSECTOR_NAME");
            ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS" );
            ViewBag.INDUSTRY_SEGMENT = new SelectList(db.CDMA_INDUSTRY_SEGMENT, "SEGMENT_CODE", "SEGMENT" );
          //  ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(db.CDMA_OCCUPATION_LIST, "OCCUPATION_CODE", "OCCUPATION");
           
            return View(model);
        }

        // POST: EmpDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,EMPLOYMENT_STATUS,EMPLOYER_INSTITUTION_NAME,DATE_OF_EMPLOYMENT,SECTOR_CLASS,SUB_SECTOR,NATURE_OF_BUSINESS_OCCUPATION,INDUSTRY_SEGMENT,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_EMPLOYMENT_DETAILS cDMA_EMPLOYMENT_DETAILS)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_EMPLOYMENT_DETAILS.Add(cDMA_EMPLOYMENT_DETAILS);
                db.SaveChanges();
               
                return RedirectToAction("Index", "DQQue");
            }

            ViewBag.SECTOR_CLASS = new SelectList(db.CDMA_OCCUPATION_LIST, "OCCUPATION_CODE", "OCCUPATION", cDMA_EMPLOYMENT_DETAILS.SECTOR_CLASS);
            ViewBag.SUB_SECTOR = new SelectList(db.CDMA_INDUSTRY_SUBSECTOR, "SUBSECTOR_CODE", "SUBSECTOR_NAME", cDMA_EMPLOYMENT_DETAILS.SUB_SECTOR);
            ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS", cDMA_EMPLOYMENT_DETAILS.NATURE_OF_BUSINESS_OCCUPATION);
            ViewBag.INDUSTRY_SEGMENT = new SelectList(db.CDMA_INDUSTRY_SEGMENT, "SEGMENT_CODE", "SEGMENT", cDMA_EMPLOYMENT_DETAILS.INDUSTRY_SEGMENT);
            RedirectToAction("Index", "DQQue");
            return View(cDMA_EMPLOYMENT_DETAILS);
        }

        // GET: EmpDetail/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }
             
           // CDMA_EMPLOYMENT_DETAILS cDMA_EMPLOYMENT_DETAILS = db.CDMA_EMPLOYMENT_DETAILS.Find(id);
            

            var x = (from c in db.CDMA_EMPLOYMENT_DETAILS
                     where c.CUSTOMER_NO == id
                     select new
                     {
                         a = c.CUSTOMER_NO,
                         b = c.EMPLOYMENT_STATUS
                     });

            var model = (from c in db.CDMA_EMPLOYMENT_DETAILS
                         where c.CUSTOMER_NO == id
                         select new EmpDetailsModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             NATURE_OF_BUSINESS_OCCUPATION = c.NATURE_OF_BUSINESS_OCCUPATION,
                             SECTOR_CLASS = c.SECTOR_CLASS,
                             SUB_SECTOR = c.SUB_SECTOR,
                             DATE_OF_EMPLOYMENT = c.DATE_OF_EMPLOYMENT,
                             EMPLOYER_INSTITUTION_NAME = c.EMPLOYER_INSTITUTION_NAME,
                             EMPLOYMENT_STATUS = c.EMPLOYMENT_STATUS,
                             INDUSTRY_SEGMENT = c.INDUSTRY_SEGMENT,
                             IP_ADDRESS = c.IP_ADDRESS,
                             LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                             LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                             CREATED_BY = c.CREATED_BY,
                             CREATED_DATE = c.CREATED_DATE,
                             AUTHORISED = c.AUTHORISED,
                             AUTHORISED_BY = c.AUTHORISED_BY,
                             AUTHORISED_DATE = c.AUTHORISED_DATE
                         }).FirstOrDefault();

            if (model == null)
            {
                return RedirectToAction("Create");
            }
         //   PrepareModel(model);
         //   return View(model);

            ViewBag.SECTOR_CLASS = new SelectList(db.CDMA_OCCUPATION_LIST, "OCCUPATION_CODE", "OCCUPATION", model.SECTOR_CLASS);
            ViewBag.SUB_SECTOR = new SelectList(db.CDMA_INDUSTRY_SUBSECTOR, "SUBSECTOR_CODE", "SUBSECTOR_NAME", model.SUB_SECTOR);
            ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS", model.NATURE_OF_BUSINESS_OCCUPATION);
            ViewBag.INDUSTRY_SEGMENT = new SelectList(db.CDMA_INDUSTRY_SEGMENT, "SEGMENT_CODE", "SEGMENT", model.INDUSTRY_SEGMENT);
            return View(model);
        }

        // POST: EmpDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CDMA_EMPLOYMENT_DETAILS empmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
             
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_EMPLOYMENT_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == empmodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", empmodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.CREATED_BY = empmodel.CREATED_BY;
                        entity.CREATED_DATE = empmodel.CREATED_DATE;
                        entity.DATE_OF_EMPLOYMENT = empmodel.DATE_OF_EMPLOYMENT;
                        entity.EMPLOYER_INSTITUTION_NAME = empmodel.EMPLOYER_INSTITUTION_NAME;
                        entity.EMPLOYMENT_STATUS = empmodel.EMPLOYMENT_STATUS;
                        entity.INDUSTRY_SEGMENT = empmodel.INDUSTRY_SEGMENT;                        
                        entity.NATURE_OF_BUSINESS_OCCUPATION = empmodel.NATURE_OF_BUSINESS_OCCUPATION;
                        entity.SECTOR_CLASS = empmodel.SECTOR_CLASS;
                        entity.SUB_SECTOR = empmodel.SUB_SECTOR;
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_EMPLOYMENT_DETAILS.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                         


                    }

                }

                ViewBag.SECTOR_CLASS = new SelectList(db.CDMA_OCCUPATION_LIST, "OCCUPATION_CODE", "OCCUPATION", empmodel.SECTOR_CLASS);
                ViewBag.SUB_SECTOR = new SelectList(db.CDMA_INDUSTRY_SUBSECTOR, "SUBSECTOR_CODE", "SUBSECTOR_NAME", empmodel.SUB_SECTOR);
                ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS", empmodel.NATURE_OF_BUSINESS_OCCUPATION);
                ViewBag.INDUSTRY_SEGMENT = new SelectList(db.CDMA_INDUSTRY_SEGMENT, "SEGMENT_CODE", "SEGMENT", empmodel.INDUSTRY_SEGMENT);
                //  PrepareModel(empmodel);
                SuccessNotification("EMPd Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = empmodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
            }

           
          

            return View(empmodel);
        }
          
                     
    
    [NonAction]
    protected virtual void PrepareModel(CDMA_EMPLOYMENT_DETAILS model)
    {
        if (model == null)
            throw new ArgumentNullException("model");

        if (model == null)
            throw new ArgumentNullException("model");
         

    }

    // GET: EmpDetail/Delete/5
    public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_EMPLOYMENT_DETAILS cDMA_EMPLOYMENT_DETAILS = db.CDMA_EMPLOYMENT_DETAILS.Find(id);
            if (cDMA_EMPLOYMENT_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_EMPLOYMENT_DETAILS);
        }

        // POST: EmpDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_EMPLOYMENT_DETAILS cDMA_EMPLOYMENT_DETAILS = db.CDMA_EMPLOYMENT_DETAILS.Find(id);
            db.CDMA_EMPLOYMENT_DETAILS.Remove(cDMA_EMPLOYMENT_DETAILS);
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
