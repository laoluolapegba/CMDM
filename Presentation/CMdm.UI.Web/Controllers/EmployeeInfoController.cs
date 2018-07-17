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
using CMdm.Services.Messaging;
using CMdm.Entities.ViewModels;

namespace CMdm.UI.Web.Controllers
{
    public class EmployeeInfoController : BaseController
    {
        #region Constructors
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        private IMessagingService _messageService;

        public EmployeeInfoController()
        {
            _dqQueService = new DqQueService();
            _messageService = new MessagingService();
        }
        #endregion

        public ActionResult Authorize(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            EmpInfoModel model = new EmpInfoModel();

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }
            //get all changed columns

            model = (from c in _db.CDMA_EMPLOYMENT_DETAILS
                        where c.CUSTOMER_NO == querecord.CUST_ID
                        where c.AUTHORISED == "U"
                        select new EmpInfoModel
                        {
                            CUSTOMER_NO = c.CUSTOMER_NO,
                            OCCUPATION = c.OCCUPATION,
                            EMPLOYMENT_STATUS = c.EMPLOYMENT_STATUS,
                            FAX_NO = c.FAX_NO,
                            DATE_OF_EMPLOYMENT = c.DATE_OF_EMPLOYMENT,
                            EMP_ADDRESS_NO = c.EMP_ADDRESS_NO,
                            EMPLOYMENT_ADD_LGA = c.EMPLOYMENT_ADD_LGA,
                            EMPLOYMENT_ADDRESS_CITY = c.EMPLOYMENT_ADDRESS_CITY,
                            EMPLOYER_INSTITUTION_NAME = c.EMPLOYER_INSTITUTION_NAME,
                            NATURE_OF_BUSINESS_OCCUPATION = c.NATURE_OF_BUSINESS_OCCUPATION,
                            OFFICE_NO_CUSTOMER = c.OFFICE_NO_CUSTOMER,
                            EMPLOYER_ADD_STATE = c.EMPLOYER_ADD_STATE,
                            STREET_NAME = c.STREET_NAME,
                            ANNUAL_INCOME = c.ANNUAL_INCOME,
                            BUSTOP_LANDMARK_EMPLOYMENT_ADD = c.BUSTOP_LANDMARK_EMPLOYMENT_ADD,
                            BRANCH_CODE = c.BRANCH_CODE,
                            TIER = c.TIER,
                            LastUpdatedby = c.LAST_MODIFIED_BY,
                            LastUpdatedDate = c.LAST_MODIFIED_DATE,
                            LastAuthdby = c.AUTHORISED_BY,
                            LastAuthDate = c.AUTHORISED_DATE,
                            ExceptionId = querecord.EXCEPTION_ID
                        }).FirstOrDefault();

            var changelog = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_EMPLOYMENT_DETAILS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();
            
            if (changelog != null && model != null)
            {
                string changeId = changelog.CHANGEID;
                var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId);
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
            }

            model.ReadOnlyForm = "True";
            PrepareModel(model);
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }

            int records = _db.CDMA_EMPLOYMENT_DETAILS.Count(o => o.CUSTOMER_NO == id);
            EmpInfoModel model = new EmpInfoModel();

            if (records > 1)
            {
                model = (from c in _db.CDMA_EMPLOYMENT_DETAILS
                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "U"
                         select new EmpInfoModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             OCCUPATION = c.OCCUPATION,
                             EMPLOYMENT_STATUS = c.EMPLOYMENT_STATUS,
                             FAX_NO = c.FAX_NO,
                             DATE_OF_EMPLOYMENT = c.DATE_OF_EMPLOYMENT,
                             EMP_ADDRESS_NO = c.EMP_ADDRESS_NO,
                             EMPLOYMENT_ADD_LGA = c.EMPLOYMENT_ADD_LGA,
                             EMPLOYMENT_ADDRESS_CITY = c.EMPLOYMENT_ADDRESS_CITY,
                             EMPLOYER_INSTITUTION_NAME = c.EMPLOYER_INSTITUTION_NAME,
                             NATURE_OF_BUSINESS_OCCUPATION = c.NATURE_OF_BUSINESS_OCCUPATION,
                             OFFICE_NO_CUSTOMER = c.OFFICE_NO_CUSTOMER,
                             EMPLOYER_ADD_STATE = c.EMPLOYER_ADD_STATE,
                             STREET_NAME = c.STREET_NAME,
                             ANNUAL_INCOME = c.ANNUAL_INCOME,
                             BUSTOP_LANDMARK_EMPLOYMENT_ADD = c.BUSTOP_LANDMARK_EMPLOYMENT_ADD,
                             BRANCH_CODE = c.BRANCH_CODE,
                             TIER = c.TIER
                         }).FirstOrDefault();
            }
            else if (records == 1)
            {
                model = (from c in _db.CDMA_EMPLOYMENT_DETAILS

                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "A"
                         select new EmpInfoModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             OCCUPATION = c.OCCUPATION,
                             EMPLOYMENT_STATUS = c.EMPLOYMENT_STATUS,
                             FAX_NO = c.FAX_NO,
                             DATE_OF_EMPLOYMENT = c.DATE_OF_EMPLOYMENT,
                             EMP_ADDRESS_NO = c.EMP_ADDRESS_NO,
                             EMPLOYMENT_ADD_LGA = c.EMPLOYMENT_ADD_LGA,
                             EMPLOYMENT_ADDRESS_CITY = c.EMPLOYMENT_ADDRESS_CITY,
                             EMPLOYER_INSTITUTION_NAME = c.EMPLOYER_INSTITUTION_NAME,
                             NATURE_OF_BUSINESS_OCCUPATION = c.NATURE_OF_BUSINESS_OCCUPATION,
                             OFFICE_NO_CUSTOMER = c.OFFICE_NO_CUSTOMER,
                             EMPLOYER_ADD_STATE = c.EMPLOYER_ADD_STATE,
                             STREET_NAME = c.STREET_NAME,
                             ANNUAL_INCOME = c.ANNUAL_INCOME,
                             BUSTOP_LANDMARK_EMPLOYMENT_ADD = c.BUSTOP_LANDMARK_EMPLOYMENT_ADD,
                             BRANCH_CODE = c.BRANCH_CODE,
                             TIER = c.TIER
                         }).FirstOrDefault();
            }

            var idempid = "";
            try
            {
                idempid = _db.MdmDqRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_EMPLOYMENT_DETAILS" && a.CUST_ID == model.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (idempid != "" && model != null)
            {
                var exceptionSet = _db.MdmDqRunExceptions.Where(a => a.CUST_ID == idempid); //.Select(a=>a.PROPERTYNAME);
                if (model != null)
                {
                    foreach (var item in model.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in exceptionSet)
                        {
                            if (item2.CATALOG_TAB_COL == item.Name)
                            {
                                ModelState.AddModelError(item.Name, string.Format("Attention!"));
                            }
                        }
                        //props.Add(item.Name);

                    }
                }
            }

            model.ReadOnlyForm = "True";
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
            bool updateFlag = false;
            if (ModelState.IsValid)
            {
                CDMA_EMPLOYMENT_DETAILS originalObject = new CDMA_EMPLOYMENT_DETAILS();
                using (var db = new AppDbContext())
                {
                    int records = db.CDMA_EMPLOYMENT_DETAILS.Count(o => o.CUSTOMER_NO == empmodel.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records > 1)
                    {
                        updateFlag = true;
                        originalObject = _db.CDMA_EMPLOYMENT_DETAILS.Where(o => o.CUSTOMER_NO == empmodel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var entity = db.CDMA_EMPLOYMENT_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == empmodel.CUSTOMER_NO && o.AUTHORISED == "U");
                        if (entity != null)
                        {
                            entity.OCCUPATION = empmodel.OCCUPATION;
                            entity.EMPLOYMENT_STATUS = empmodel.EMPLOYMENT_STATUS;
                            entity.FAX_NO = empmodel.FAX_NO;
                            entity.DATE_OF_EMPLOYMENT = empmodel.DATE_OF_EMPLOYMENT;
                            entity.EMP_ADDRESS_NO = empmodel.EMP_ADDRESS_NO;
                            entity.EMPLOYMENT_ADD_LGA = empmodel.EMPLOYMENT_ADD_LGA;
                            entity.EMPLOYMENT_ADDRESS_CITY = empmodel.EMPLOYMENT_ADDRESS_CITY;
                            entity.EMPLOYER_INSTITUTION_NAME = empmodel.EMPLOYER_INSTITUTION_NAME;                                                    
                            entity.NATURE_OF_BUSINESS_OCCUPATION = empmodel.NATURE_OF_BUSINESS_OCCUPATION;
                            entity.OFFICE_NO_CUSTOMER = empmodel.OFFICE_NO_CUSTOMER;
                            entity.EMPLOYER_ADD_STATE = empmodel.EMPLOYER_ADD_STATE;
                            entity.STREET_NAME = empmodel.STREET_NAME;
                            entity.ANNUAL_INCOME = empmodel.ANNUAL_INCOME;
                            entity.BUSTOP_LANDMARK_EMPLOYMENT_ADD = empmodel.BUSTOP_LANDMARK_EMPLOYMENT_ADD;
                            entity.BRANCH_CODE = empmodel.BRANCH_CODE;
                            entity.TIER = empmodel.TIER;
                            entity.QUEUE_STATUS = 1;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            db.CDMA_EMPLOYMENT_DETAILS.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), empmodel.CUSTOMER_NO, updateFlag, originalObject);
                            _messageService.LogEmailJob(identity.ProfileId, entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if (records == 1)
                    {
                        updateFlag = false;
                        var entity = db.CDMA_EMPLOYMENT_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == empmodel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject = _db.CDMA_EMPLOYMENT_DETAILS.Where(o => o.CUSTOMER_NO == empmodel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject != null)
                        {
                            entity.OCCUPATION = empmodel.OCCUPATION;
                            entity.EMPLOYMENT_STATUS = empmodel.EMPLOYMENT_STATUS;
                            entity.FAX_NO = empmodel.FAX_NO;
                            entity.DATE_OF_EMPLOYMENT = empmodel.DATE_OF_EMPLOYMENT;
                            entity.EMP_ADDRESS_NO = empmodel.EMP_ADDRESS_NO;
                            entity.EMPLOYMENT_ADD_LGA = empmodel.EMPLOYMENT_ADD_LGA;
                            entity.EMPLOYMENT_ADDRESS_CITY = empmodel.EMPLOYMENT_ADDRESS_CITY;
                            entity.EMPLOYER_INSTITUTION_NAME = empmodel.EMPLOYER_INSTITUTION_NAME;
                            entity.NATURE_OF_BUSINESS_OCCUPATION = empmodel.NATURE_OF_BUSINESS_OCCUPATION;
                            entity.OFFICE_NO_CUSTOMER = empmodel.OFFICE_NO_CUSTOMER;
                            entity.EMPLOYER_ADD_STATE = empmodel.EMPLOYER_ADD_STATE;
                            entity.STREET_NAME = empmodel.STREET_NAME;
                            entity.ANNUAL_INCOME = empmodel.ANNUAL_INCOME;
                            entity.BUSTOP_LANDMARK_EMPLOYMENT_ADD = empmodel.BUSTOP_LANDMARK_EMPLOYMENT_ADD;
                            entity.BRANCH_CODE = empmodel.BRANCH_CODE;
                            entity.TIER = empmodel.TIER;
                            entity.QUEUE_STATUS = 1;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;

                            db.CDMA_EMPLOYMENT_DETAILS.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), empmodel.CUSTOMER_NO, updateFlag, originalObject);


                            var newentity = new CDMA_EMPLOYMENT_DETAILS();

                            newentity.OCCUPATION = empmodel.OCCUPATION;
                            newentity.EMPLOYMENT_STATUS = empmodel.EMPLOYMENT_STATUS;
                            newentity.FAX_NO = empmodel.FAX_NO;
                            newentity.DATE_OF_EMPLOYMENT = empmodel.DATE_OF_EMPLOYMENT;
                            newentity.EMP_ADDRESS_NO = empmodel.EMP_ADDRESS_NO;
                            newentity.EMPLOYMENT_ADD_LGA = empmodel.EMPLOYMENT_ADD_LGA;
                            newentity.EMPLOYMENT_ADDRESS_CITY = empmodel.EMPLOYMENT_ADDRESS_CITY;
                            newentity.EMPLOYER_INSTITUTION_NAME = empmodel.EMPLOYER_INSTITUTION_NAME;
                            newentity.NATURE_OF_BUSINESS_OCCUPATION = empmodel.NATURE_OF_BUSINESS_OCCUPATION;
                            newentity.OFFICE_NO_CUSTOMER = empmodel.OFFICE_NO_CUSTOMER;
                            newentity.EMPLOYER_ADD_STATE = empmodel.EMPLOYER_ADD_STATE;
                            newentity.STREET_NAME = empmodel.STREET_NAME;
                            newentity.ANNUAL_INCOME = empmodel.ANNUAL_INCOME;
                            newentity.BUSTOP_LANDMARK_EMPLOYMENT_ADD = empmodel.BUSTOP_LANDMARK_EMPLOYMENT_ADD;
                            newentity.BRANCH_CODE = empmodel.BRANCH_CODE;
                            newentity.TIER = empmodel.TIER;
                            newentity.QUEUE_STATUS = 1;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = empmodel.CUSTOMER_NO;
                            
                            db.CDMA_EMPLOYMENT_DETAILS.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", empmodel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                }

                SuccessNotification("Employee Details Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = empmodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(empmodel);
            return View(empmodel);
        }

        [NonAction]
        protected virtual void PrepareModel(EmpInfoModel model)
        {
            //if (model == null)
            //    throw new ArgumentNullException("model");

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

            model.Occupationtype = new SelectList(_db.CDMA_OCCUPATION_LIST, "OCCUPATION_CODE", "OCCUPATION").ToList();
            //model.Subsectortype = new SelectList(_db.CDMA_INDUSTRY_SUBSECTOR, "SUBSECTOR_CODE", "SUBSECTOR_NAME").ToList();
            model.Businessnature = new SelectList(_db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS").ToList();
            model.Indsegment = new SelectList(_db.CDMA_INDUSTRY_SEGMENT, "SEGMENT_CODE", "SEGMENT").ToList();
            model.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
            model.States = new SelectList(_db.SRC_CDMA_STATE.OrderBy(x => x.STATE_NAME), "STATE_ID", "STATE_NAME").ToList();
            model.LGAs = new SelectList(_db.SRC_CDMA_LGA.OrderBy(x => x.LGA_NAME), "LGA_ID", "LGA_NAME").ToList();
        }
    
        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(EmpInfoModel empmodel, bool disapproveRecord)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;

                int exceptionId = 0;
                if (routeValues.ContainsKey("id"))
                    exceptionId = int.Parse((string)routeValues["id"]);
                if (disapproveRecord)
                {
                    _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), empmodel.AuthoriserRemarks);
                    SuccessNotification("Employee Details Not Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, empmodel.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(empmodel.LastUpdatedby));
                }

                else
                {
                    _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(), identity.ProfileId);
                    SuccessNotification("Employee Details Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, empmodel.CUSTOMER_NO, MessageJobEnum.MailType.Authorize, Convert.ToInt32(empmodel.LastUpdatedby));
                }

                return RedirectToAction("AuthList", "DQQue");
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

                SuccessNotification("Employee Details Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = empmodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "EmployeeInfo");
                //return RedirectToAction("Index");
            }
            PrepareModel(empmodel);
            return View(empmodel);
        }

        #region scaffolded
        public ActionResult Create()
        {
            EmpInfoModel model = new EmpInfoModel();
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
                    NATURE_OF_BUSINESS_OCCUPATION = empmodel.NATURE_OF_BUSINESS_OCCUPATION,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address,
                };
                _db.CDMA_EMPLOYMENT_DETAILS.Add(emp);
                _db.SaveChanges();


                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New EMP has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = empmodel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(empmodel);
            return View(empmodel);
        }
        // GET: EmployeeInfo
        //public ActionResult Index()
        //{
          //  var cDMA_EMPLOYMENT_DETAILS = _db.CDMA_EMPLOYMENT_DETAILS.Include(c => c.Businessnature).Include(c => c.Indsegment).Include(c => c.Occupationtype).Include(c => c.Subsectortype);
            //return View(cDMA_EMPLOYMENT_DETAILS.ToList());
        //}

        // GET: EmployeeInfo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_EMPLOYMENT_DETAILS cDMA_EMPLOYMENT_DETAILS = _db.CDMA_EMPLOYMENT_DETAILS.Find(id);
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
            ViewBag.SECTOR_CLASS = new SelectList(_db.CDMA_OCCUPATION_LIST, "OCCUPATION_CODE", "OCCUPATION" );
            ViewBag.SUB_SECTOR = new SelectList(_db.CDMA_INDUSTRY_SUBSECTOR, "SUBSECTOR_CODE", "SUBSECTOR_NAME");
            ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(_db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS" );
            ViewBag.INDUSTRY_SEGMENT = new SelectList(_db.CDMA_INDUSTRY_SEGMENT, "SEGMENT_CODE", "SEGMENT" );
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
                _db.CDMA_EMPLOYMENT_DETAILS.Add(cDMA_EMPLOYMENT_DETAILS);
                _db.SaveChanges();
               
                return RedirectToAction("Index", "DQQue");
            }


            ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(_db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS", cDMA_EMPLOYMENT_DETAILS.NATURE_OF_BUSINESS_OCCUPATION);
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
            

            var x = (from c in _db.CDMA_EMPLOYMENT_DETAILS
                     where c.CUSTOMER_NO == id
                     select new
                     {
                         a = c.CUSTOMER_NO,
                         b = c.EMPLOYMENT_STATUS
                     });

            var model = (from c in _db.CDMA_EMPLOYMENT_DETAILS
                         where c.CUSTOMER_NO == id
                         select new EmpInfoModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             NATURE_OF_BUSINESS_OCCUPATION = c.NATURE_OF_BUSINESS_OCCUPATION,
                             DATE_OF_EMPLOYMENT = c.DATE_OF_EMPLOYMENT,
                             EMPLOYER_INSTITUTION_NAME = c.EMPLOYER_INSTITUTION_NAME,
                             EMPLOYMENT_STATUS = c.EMPLOYMENT_STATUS,
                         }).FirstOrDefault();

            if (model == null)
            {
                return RedirectToAction("Create");
            }
         //   PrepareModel(model);
         //   return View(model);

            ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(_db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS", model.NATURE_OF_BUSINESS_OCCUPATION);
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
                        entity.NATURE_OF_BUSINESS_OCCUPATION = empmodel.NATURE_OF_BUSINESS_OCCUPATION;
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_EMPLOYMENT_DETAILS.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                         


                    }

                }

                
                ViewBag.NATURE_OF_BUSINESS_OCCUPATION = new SelectList(_db.CDMA_NATURE_OF_BUSINESS, "BUSINESS_CODE", "BUSINESS", empmodel.NATURE_OF_BUSINESS_OCCUPATION);
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
            CDMA_EMPLOYMENT_DETAILS cDMA_EMPLOYMENT_DETAILS = _db.CDMA_EMPLOYMENT_DETAILS.Find(id);
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
            CDMA_EMPLOYMENT_DETAILS cDMA_EMPLOYMENT_DETAILS = _db.CDMA_EMPLOYMENT_DETAILS.Find(id);
            _db.CDMA_EMPLOYMENT_DETAILS.Remove(cDMA_EMPLOYMENT_DETAILS);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion scaffolded
    }
}
