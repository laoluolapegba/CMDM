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
using CMdm.Services.DqQue;

namespace CMdm.UI.Web.Controllers
{
    public class EmployeeInfoController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IDqQueService _dqQueService;

        public EmployeeInfoController()
        {
            _dqQueService = new DqQueService();
        }

        public ActionResult Authorize(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            var model = new EmpInfoModel();

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }
            //get all changed columns

            var changeId = db.CDMA_CHANGE_LOGS.Where(a => a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();
            if (changeId != null)
            {
                var changedSet = db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId.CHANGEID); //.Select(a=>a.PROPERTYNAME);
                model = (from c in db.CDMA_EMPLOYMENT_DETAILS
                         where c.CUSTOMER_NO == querecord.CUST_ID

                         select new EmpInfoModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             EMPLOYMENT_STATUS = c.EMPLOYMENT_STATUS,
                             EMPLOYER_INSTITUTION_NAME = c.EMPLOYER_INSTITUTION_NAME,
                             DATE_OF_EMPLOYMENT = c.DATE_OF_EMPLOYMENT,
                             SECTOR_CLASS = c.SECTOR_CLASS,
                             SUB_SECTOR = c.SUB_SECTOR,
                             NATURE_OF_BUSINESS_OCCUPATION = c.NATURE_OF_BUSINESS_OCCUPATION,
                             INDUSTRY_SEGMENT = c.INDUSTRY_SEGMENT,
                             LastUpdatedby = c.LAST_MODIFIED_BY,
                             LastUpdatedDate = c.LAST_MODIFIED_DATE,
                             LastAuthdby = c.AUTHORISED_BY,
                             LastAuthDate = c.AUTHORISED_DATE,
                             ExceptionId = querecord.EXCEPTION_ID
                         }).FirstOrDefault();

                foreach (var item in model.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }
                }
                model.ReadOnlyForm = "True";
            }

            PrepareModel(model);
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }

            //var x = (from c in db.CDMA_FOREIGN_DETAILS
            //         where c.CUSTOMER_NO == id
            //         select new
            //         {
            //             a = c.CUSTOMER_NO,
            //             b = c.COUNTRY
            //         });

            var model = (from c in db.CDMA_EMPLOYMENT_DETAILS

                         where c.CUSTOMER_NO == id
                         select new EmpInfoModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             EMPLOYMENT_STATUS = c.EMPLOYMENT_STATUS,
                             EMPLOYER_INSTITUTION_NAME = c.EMPLOYER_INSTITUTION_NAME,
                             DATE_OF_EMPLOYMENT = c.DATE_OF_EMPLOYMENT,
                             SECTOR_CLASS = c.SECTOR_CLASS,
                             SUB_SECTOR = c.SUB_SECTOR,
                             NATURE_OF_BUSINESS_OCCUPATION = c.NATURE_OF_BUSINESS_OCCUPATION,
                             INDUSTRY_SEGMENT = c.INDUSTRY_SEGMENT,
                         }).FirstOrDefault();


            PrepareModel(model);
            return View(model);
        }

        // POST: CustForeigner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmpInfoModel empmodel, bool continueEditing)
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
                        entity.EMPLOYMENT_STATUS = empmodel.EMPLOYMENT_STATUS;
                        entity.EMPLOYER_INSTITUTION_NAME = empmodel.EMPLOYER_INSTITUTION_NAME;
                        entity.DATE_OF_EMPLOYMENT = empmodel.DATE_OF_EMPLOYMENT;
                        entity.SECTOR_CLASS = empmodel.SECTOR_CLASS;
                        entity.SUB_SECTOR = empmodel.SUB_SECTOR;
                        entity.NATURE_OF_BUSINESS_OCCUPATION = empmodel.NATURE_OF_BUSINESS_OCCUPATION;
                        entity.INDUSTRY_SEGMENT = empmodel.INDUSTRY_SEGMENT;
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_EMPLOYMENT_DETAILS.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                SuccessNotification("EMP Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = empmodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(empmodel);
            return View(empmodel);
        }
        public ActionResult Create()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();

            var model = new EmpInfoModel();
            PrepareModel(model);
            return View(model);
        }

        // POST: CustForeigner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpInfoModel empmodel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string ip_address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (ModelState.IsValid)
            {
                CDMA_EMPLOYMENT_DETAILS emp = new CDMA_EMPLOYMENT_DETAILS
                {
                    CUSTOMER_NO = empmodel.CUSTOMER_NO,
                    EMPLOYMENT_STATUS = empmodel.EMPLOYMENT_STATUS,
                    EMPLOYER_INSTITUTION_NAME = empmodel.EMPLOYER_INSTITUTION_NAME,
                    DATE_OF_EMPLOYMENT = empmodel.DATE_OF_EMPLOYMENT,
                    SECTOR_CLASS = empmodel.SECTOR_CLASS,
                    SUB_SECTOR = empmodel.SUB_SECTOR,
                    NATURE_OF_BUSINESS_OCCUPATION = empmodel.NATURE_OF_BUSINESS_OCCUPATION,
                    INDUSTRY_SEGMENT = empmodel.INDUSTRY_SEGMENT,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address,
                };
                db.CDMA_EMPLOYMENT_DETAILS.Add(emp);
                db.SaveChanges();


                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New EMP has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = empmodel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(empmodel);
            return View(empmodel);
        }

        [NonAction]
        protected virtual void PrepareModel(EmpInfoModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model == null)
                throw new ArgumentNullException("model");

            model.EmploymentStatus.Add(new SelectListItem
            {
                Text = "Employed",
                Value = "Employed"
            });
            model.EmploymentStatus.Add(new SelectListItem
            {
                Text = "Self Employed",
                Value = "Self Employed"
            });
            model.EmploymentStatus.Add(new SelectListItem
            {
                Text = "Unemployed",
                Value = "Unemployed"
            });

            model.Occupationtype = new SelectList(db.CDMA_OCCUPATION_LIST, "OCCUPATION_CODE", "OCCUPATION").ToList();
            model.Subsectortype = new SelectList(db.CDMA_INDUSTRY_SUBSECTOR, "SUBSECTOR_CODE", "SUBSECTOR_NAME").ToList();
            model.Businessnature = new SelectList(db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS").ToList();
            model.Indsegment = new SelectList(db.CDMA_INDUSTRY_SEGMENT, "SEGMENT_CODE", "SEGMENT").ToList();
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("approve")]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(EmpInfoModel empmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {

                _dqQueService.ApproveExceptionQueItems(empmodel.ExceptionId.ToString());

                SuccessNotification("FORD Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = empmodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "EmployeeInfo");
                //return RedirectToAction("Index");
            }
            PrepareModel(empmodel);
            return View(empmodel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove(EmpInfoModel empmodel, bool continueEditing)
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
                        entity.AUTHORISED = "N";
                        db.CDMA_EMPLOYMENT_DETAILS.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                SuccessNotification("EMP Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = empmodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "EmployeeInfo");
                //return RedirectToAction("Index");
            }
            PrepareModel(empmodel);
            return View(empmodel);
        }

        #region scaffolded
        // GET: EmployeeInfo
        public ActionResult Index()
        {
            var cDMA_EMPLOYMENT_DETAILS = db.CDMA_EMPLOYMENT_DETAILS.Include(c => c.Businessnature).Include(c => c.Indsegment).Include(c => c.Occupationtype).Include(c => c.Subsectortype);
            return View(cDMA_EMPLOYMENT_DETAILS.ToList());
        }

        // GET: EmployeeInfo/Details/5
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

        // GET: EmployeeInfo/Create
        public ActionResult Create_(string id="")
        {
            EmpInfoModel model = new EmpInfoModel();
            if (id != "") model.CUSTOMER_NO = id;
            // ViewBag.SUB_SECTOR = new SelectList(db.CDMA_INDUSTRY_SUBSECTOR, "SUBSECTOR_CODE", "SUBSECTOR_NAME");
            ViewBag.SECTOR_CLASS = new SelectList(db.CDMA_OCCUPATION_LIST, "OCCUPATION_CODE", "OCCUPATION" );
            ViewBag.SUB_SECTOR = new SelectList(db.CDMA_INDUSTRY_SUBSECTOR, "SUBSECTOR_CODE", "SUBSECTOR_NAME");
            ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS" );
            ViewBag.INDUSTRY_SEGMENT = new SelectList(db.CDMA_INDUSTRY_SEGMENT, "SEGMENT_CODE", "SEGMENT" );
          //  ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(db.CDMA_OCCUPATION_LIST, "OCCUPATION_CODE", "OCCUPATION");
           
            return View(model);
        }

        // POST: EmployeeInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_1([Bind(Include = "CUSTOMER_NO,EMPLOYMENT_STATUS,EMPLOYER_INSTITUTION_NAME,DATE_OF_EMPLOYMENT,SECTOR_CLASS,SUB_SECTOR,NATURE_OF_BUSINESS_OCCUPATION,INDUSTRY_SEGMENT,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_EMPLOYMENT_DETAILS cDMA_EMPLOYMENT_DETAILS)
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

        // GET: EmployeeInfo/Edit/5
        public ActionResult Edit_(string id)
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
                         select new EmpInfoModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             NATURE_OF_BUSINESS_OCCUPATION = c.NATURE_OF_BUSINESS_OCCUPATION,
                             SECTOR_CLASS = c.SECTOR_CLASS,
                             SUB_SECTOR = c.SUB_SECTOR,
                             DATE_OF_EMPLOYMENT = c.DATE_OF_EMPLOYMENT,
                             EMPLOYER_INSTITUTION_NAME = c.EMPLOYER_INSTITUTION_NAME,
                             EMPLOYMENT_STATUS = c.EMPLOYMENT_STATUS,
                             INDUSTRY_SEGMENT = c.INDUSTRY_SEGMENT,
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

        // POST: EmployeeInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_1(CDMA_EMPLOYMENT_DETAILS empmodel, bool continueEditing)
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

    // GET: EmployeeInfo/Delete/5
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

        // POST: EmployeeInfo/Delete/5
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
        #endregion scaffolded
    }
}
