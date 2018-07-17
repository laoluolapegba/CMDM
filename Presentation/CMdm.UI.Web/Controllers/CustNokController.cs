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
using System.Reflection;
using CMdm.Services.DqQue;
using CMdm.Services.Messaging;
using CMdm.Entities.ViewModels;

namespace CMdm.UI.Web.Controllers
{
    public class CustNokController : BaseController
    {
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        private IMessagingService _messageService;
        public CustNokController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new DqQueService();
            _messageService = new MessagingService();
        }
        public ActionResult Authorize(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("AuthList","DQQue");
            }            

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }
            //get all changed columns


            CustomerNOKModel model = new CustomerNOKModel();
            model = (from c in _db.CDMA_INDIVIDUAL_NEXT_OF_KIN
                         where c.CUSTOMER_NO == querecord.CUST_ID
                         where c.AUTHORISED == "U"
                         select new CustomerNOKModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             TITLE = c.TITLE,
                             SURNAME = c.SURNAME,
                             FIRST_NAME = c.FIRST_NAME,
                             OTHER_NAME = c.OTHER_NAME,
                             DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                             SEX = c.SEX,
                             RELATIONSHIP = c.RELATIONSHIP,
                             EMAIL_ADDRESS = c.EMAIL_ADDRESS,
                             NEXT_OF_KIN_RESIDENTIALSTREET = c.NEXT_OF_KIN_RESIDENTIALSTREET,
                             NOK_ADDRESS_NO = c.NOK_ADDRESS_NO,
                             CITY_TOWN = c.CITY_TOWN,
                             LGA = c.LGA,
                             NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
                             STATE = c.STATE,
                             NEXT_OF_KIN_PHONE_NUMBER = c.NEXT_OF_KIN_PHONE_NUMBER,
                             NEXT_OF_KIN_PHONE_NUMBER2 = c.NEXT_OF_KIN_PHONE_NUMBER2,
                             BRANCH_CODE = c.BRANCH_CODE,
                             LastUpdatedby = c.LAST_MODIFIED_BY,
                             LastUpdatedDate = c.LAST_MODIFIED_DATE,
                             LastAuthdby = c.AUTHORISED_BY,
                             LastAuthDate = c.AUTHORISED_DATE,
                             ExceptionId = querecord.EXCEPTION_ID
                         }).FirstOrDefault();

            var changelog = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_NEXT_OF_KIN" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();
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

            int records = _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Count(o => o.CUSTOMER_NO == id);
            var model = new CustomerNOKModel();
            if (records > 1)
            {
                model = (from c in _db.CDMA_INDIVIDUAL_NEXT_OF_KIN

                             where c.CUSTOMER_NO == id
                             where c.AUTHORISED == "U"
                             select new CustomerNOKModel
                             {
                                 CUSTOMER_NO = c.CUSTOMER_NO,
                                 TITLE = c.TITLE,
                                 SURNAME = c.SURNAME,
                                 FIRST_NAME = c.FIRST_NAME,
                                 OTHER_NAME = c.OTHER_NAME,
                                 DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                                 SEX = c.SEX,
                                 RELATIONSHIP = c.RELATIONSHIP,
                                 EMAIL_ADDRESS = c.EMAIL_ADDRESS,
                                 NEXT_OF_KIN_RESIDENTIALSTREET = c.NEXT_OF_KIN_RESIDENTIALSTREET,
                                 NOK_ADDRESS_NO = c.NOK_ADDRESS_NO,
                                 CITY_TOWN = c.CITY_TOWN,
                                 LGA = c.LGA,
                                 NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
                                 STATE = c.STATE,
                                 NEXT_OF_KIN_PHONE_NUMBER = c.NEXT_OF_KIN_PHONE_NUMBER,
                                 NEXT_OF_KIN_PHONE_NUMBER2 = c.NEXT_OF_KIN_PHONE_NUMBER2,
                                 BRANCH_CODE = c.BRANCH_CODE,
                             }).FirstOrDefault();
            }
            else if(records ==1)
            {
                model = (from c in _db.CDMA_INDIVIDUAL_NEXT_OF_KIN

                         where c.CUSTOMER_NO == id
                         where c.AUTHORISED == "A"
                         select new CustomerNOKModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             TITLE = c.TITLE,
                             SURNAME = c.SURNAME,
                             FIRST_NAME = c.FIRST_NAME,
                             OTHER_NAME = c.OTHER_NAME,
                             DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                             SEX = c.SEX,
                             RELATIONSHIP = c.RELATIONSHIP,
                             EMAIL_ADDRESS = c.EMAIL_ADDRESS,
                             NEXT_OF_KIN_RESIDENTIALSTREET = c.NEXT_OF_KIN_RESIDENTIALSTREET,
                             NOK_ADDRESS_NO = c.NOK_ADDRESS_NO,
                             CITY_TOWN = c.CITY_TOWN,
                             LGA = c.LGA,
                             NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
                             STATE = c.STATE,
                             NEXT_OF_KIN_PHONE_NUMBER = c.NEXT_OF_KIN_PHONE_NUMBER,
                             NEXT_OF_KIN_PHONE_NUMBER2 = c.NEXT_OF_KIN_PHONE_NUMBER2,
                             BRANCH_CODE = c.BRANCH_CODE,
                         }).FirstOrDefault();
            }
            
            PrepareModel(model);

            var nokcustid = "";
            try
            {
                nokcustid = _db.MdmDqRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_INDIVIDUAL_NEXT_OF_KIN" && a.CUST_ID == model.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (nokcustid != "" && model != null)
            {
                var exceptionSet = _db.MdmDqRunExceptions.Where(a => a.CUST_ID == nokcustid); //.Select(a=>a.PROPERTYNAME);
                if (nokcustid != null)
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
            return View(model);
        }

        // POST: MdmCatalogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerNOKModel nokmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            bool updateFlag = false;
            if (ModelState.IsValid)
            {
                CDMA_INDIVIDUAL_NEXT_OF_KIN originalObject = new CDMA_INDIVIDUAL_NEXT_OF_KIN();
                
                using (var db = new AppDbContext())
                {
                    int records = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Count(o => o.CUSTOMER_NO == nokmodel.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records > 1)
                    {
                        updateFlag = true;                        
                        originalObject = _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Where(o => o.CUSTOMER_NO == nokmodel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();
                        var entity = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.FirstOrDefault(o => o.CUSTOMER_NO == nokmodel.CUSTOMER_NO && o.AUTHORISED == "U");
                        
                        if (entity != null)
                        {
                            entity.TITLE = nokmodel.TITLE;
                            entity.SURNAME = nokmodel.SURNAME;
                            entity.FIRST_NAME = nokmodel.FIRST_NAME;
                            entity.OTHER_NAME = nokmodel.OTHER_NAME;
                            entity.DATE_OF_BIRTH = nokmodel.DATE_OF_BIRTH;
                            entity.SEX = nokmodel.SEX;
                            entity.RELATIONSHIP = nokmodel.RELATIONSHIP;
                            entity.EMAIL_ADDRESS = nokmodel.EMAIL_ADDRESS;
                            entity.NEXT_OF_KIN_RESIDENTIALSTREET = nokmodel.NEXT_OF_KIN_RESIDENTIALSTREET;
                            entity.NOK_ADDRESS_NO = nokmodel.NOK_ADDRESS_NO;
                            entity.CITY_TOWN = nokmodel.CITY_TOWN;
                            entity.LGA = nokmodel.LGA;
                            entity.NEAREST_BUS_STOP_LANDMARK = nokmodel.NEAREST_BUS_STOP_LANDMARK;
                            entity.STATE = nokmodel.STATE;
                            entity.NEXT_OF_KIN_PHONE_NUMBER = nokmodel.NEXT_OF_KIN_PHONE_NUMBER;
                            entity.NEXT_OF_KIN_PHONE_NUMBER2 = nokmodel.NEXT_OF_KIN_PHONE_NUMBER2;
                            entity.BRANCH_CODE = nokmodel.BRANCH_CODE;
                            entity.QUEUE_STATUS = 1;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";

                            db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), nokmodel.CUSTOMER_NO, updateFlag, originalObject);
                            _messageService.LogEmailJob(identity.ProfileId, entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                        }
                    }
                    else if (records == 1)
                    {
                        updateFlag = false;
                        var entity = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.FirstOrDefault(o => o.CUSTOMER_NO == nokmodel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject = _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Where(o => o.CUSTOMER_NO == nokmodel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject != null)
                        {
                            entity.TITLE = nokmodel.TITLE;
                            entity.SURNAME = nokmodel.SURNAME;
                            entity.FIRST_NAME = nokmodel.FIRST_NAME;
                            entity.OTHER_NAME = nokmodel.OTHER_NAME;
                            entity.DATE_OF_BIRTH = nokmodel.DATE_OF_BIRTH;
                            entity.SEX = nokmodel.SEX;
                            entity.RELATIONSHIP = nokmodel.RELATIONSHIP;
                            entity.EMAIL_ADDRESS = nokmodel.EMAIL_ADDRESS;
                            entity.NEXT_OF_KIN_RESIDENTIALSTREET = nokmodel.NEXT_OF_KIN_RESIDENTIALSTREET;
                            entity.NOK_ADDRESS_NO = nokmodel.NOK_ADDRESS_NO;
                            entity.CITY_TOWN = nokmodel.CITY_TOWN;
                            entity.LGA = nokmodel.LGA;
                            entity.NEAREST_BUS_STOP_LANDMARK = nokmodel.NEAREST_BUS_STOP_LANDMARK;
                            entity.STATE = nokmodel.STATE;
                            entity.NEXT_OF_KIN_PHONE_NUMBER = nokmodel.NEXT_OF_KIN_PHONE_NUMBER;
                            entity.NEXT_OF_KIN_PHONE_NUMBER2 = nokmodel.NEXT_OF_KIN_PHONE_NUMBER2;
                            entity.BRANCH_CODE = nokmodel.BRANCH_CODE;
                            entity.QUEUE_STATUS = 1;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            entity.NEAREST_BUS_STOP_LANDMARK = nokmodel.NEAREST_BUS_STOP_LANDMARK;
                            //entity.AUTHORISED = "U";

                            //

                            db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), nokmodel.CUSTOMER_NO, updateFlag, originalObject);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_INDIVIDUAL_NEXT_OF_KIN();
                            newentity.TITLE = nokmodel.TITLE;
                            newentity.SURNAME = nokmodel.SURNAME;
                            newentity.FIRST_NAME = nokmodel.FIRST_NAME;
                            newentity.OTHER_NAME = nokmodel.OTHER_NAME;
                            newentity.DATE_OF_BIRTH = nokmodel.DATE_OF_BIRTH;
                            newentity.SEX = nokmodel.SEX;
                            newentity.RELATIONSHIP = nokmodel.RELATIONSHIP;
                            newentity.EMAIL_ADDRESS = nokmodel.EMAIL_ADDRESS;
                            newentity.NEXT_OF_KIN_RESIDENTIALSTREET = nokmodel.NEXT_OF_KIN_RESIDENTIALSTREET;
                            newentity.NOK_ADDRESS_NO = nokmodel.NOK_ADDRESS_NO;
                            newentity.CITY_TOWN = nokmodel.CITY_TOWN;
                            newentity.LGA = nokmodel.LGA;
                            newentity.NEAREST_BUS_STOP_LANDMARK = nokmodel.NEAREST_BUS_STOP_LANDMARK;
                            newentity.STATE = nokmodel.STATE;
                            newentity.NEXT_OF_KIN_PHONE_NUMBER = nokmodel.NEXT_OF_KIN_PHONE_NUMBER;
                            newentity.NEXT_OF_KIN_PHONE_NUMBER2 = nokmodel.NEXT_OF_KIN_PHONE_NUMBER2;
                            newentity.BRANCH_CODE = nokmodel.BRANCH_CODE;
                            newentity.QUEUE_STATUS = 1;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = nokmodel.CUSTOMER_NO;
                            newentity.NEAREST_BUS_STOP_LANDMARK = nokmodel.NEAREST_BUS_STOP_LANDMARK;
                            db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", nokmodel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }                    
                }

                SuccessNotification("Next of Kin Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = nokmodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(nokmodel);
            return View(nokmodel);
        }

        [NonAction]
        protected virtual void PrepareModel(CustomerNOKModel model)
        {
            //if (model == null)
            //    throw new ArgumentNullException("model");

            if (model == null)
                throw new ArgumentNullException("model");
            model.Genders.Add(new SelectListItem
            {
                Text = "Male",
                Value = "Male"
            });
            model.Genders.Add(new SelectListItem
            {
                Text = "Female",
                Value = "Female"
            });
            model.IdTypes = new SelectList(_db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE").ToList();
            model.Countries = new SelectList(_db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
            model.LocalGovts = new SelectList(_db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME").ToList();
            model.RelationshipTypes = new SelectList(_db.CDMA_CUST_REL_TYPE, "REL_CODE", "REL_DESC").ToList();
            model.States = new SelectList(_db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME").ToList();
            model.TitleTypes = new SelectList(_db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC").ToList();
            model.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(CustomerNOKModel nokmodel, bool disapproveRecord)
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

                    _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), nokmodel.AuthoriserRemarks);
                    SuccessNotification("Next of Kin Not Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, nokmodel.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(nokmodel.LastUpdatedby));
                }

                else
                {
                    _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(), identity.ProfileId);
                    SuccessNotification("Next of Kin Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, nokmodel.CUSTOMER_NO, MessageJobEnum.MailType.Authorize, Convert.ToInt32(nokmodel.LastUpdatedby));
                }
               
                return RedirectToAction("AuthList", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(nokmodel);
            return View(nokmodel);
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove_(CustomerNOKModel nokmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                _dqQueService.DisApproveExceptionQueItems(nokmodel.ExceptionId.ToString(), nokmodel.AuthoriserRemarks);

                SuccessNotification("Next of Kin Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = nokmodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "CustNok");
                //return RedirectToAction("Index");
            }
            PrepareModel(nokmodel);
            return View(nokmodel);
        }

        #region Scaffolded
        public ActionResult Create()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();

            var model = new CustomerNOKModel();
            PrepareModel(model);
            return View(model);
        }

        // POST: MdmCatalogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerNOKModel nokmodel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                CDMA_INDIVIDUAL_NEXT_OF_KIN nok = new CDMA_INDIVIDUAL_NEXT_OF_KIN
                {


                };
                _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Add(nok);
                _db.SaveChanges();


                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New NOK has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = nokmodel.CUSTOMER_NO }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            PrepareModel(nokmodel);
            return View(nokmodel);
        }

        // GET: CustNok
        //public ActionResult Index()
        //{
        //    var cDMA_INDIVIDUAL_NEXT_OF_KIN = _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Include(c => c.Countries).Include(c => c.IdTypes).Include(c => c.LocalGovts).Include(c => c.RelationshipTypes).Include(c => c.States).Include(c => c.TitleTypes);
        //    return View(cDMA_INDIVIDUAL_NEXT_OF_KIN.ToList());
        //}

        // GET: CustNok/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_NEXT_OF_KIN cDMA_INDIVIDUAL_NEXT_OF_KIN = _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Find(id);
            if (cDMA_INDIVIDUAL_NEXT_OF_KIN == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_NEXT_OF_KIN);
        }

        // GET: CustNok/Create
        public ActionResult Create_()
        {
            ViewBag.COUNTRY = new SelectList(_db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.IDENTIFICATION_TYPE = new SelectList(_db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE");
            ViewBag.LGA = new SelectList(_db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME");
            ViewBag.RELATIONSHIP = new SelectList(_db.CDMA_CUST_REL_TYPE, "REL_CODE", "REL_DESC");
            ViewBag.STATE = new SelectList(_db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME");
            ViewBag.TITLE = new SelectList(_db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC");
            return View();
        }

        // POST: CustNok/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_NO,TITLE,SURNAME,FIRST_NAME,OTHER_NAME,DATE_OF_BIRTH,SEX,RELATIONSHIP,OFFICE_NO,MOBILE_NO,EMAIL_ADDRESS,HOUSE_NUMBER,IDENTIFICATION_TYPE,ID_EXPIRY_DATE,ID_ISSUE_DATE,RESIDENT_PERMIT_NUMBER,PLACE_OF_ISSUANCE,STREET_NAME,NEAREST_BUS_STOP_LANDMARK,CITY_TOWN,LGA,ZIP_POSTAL_CODE,STATE,COUNTRY,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS,BRANCH_CODE")] CDMA_INDIVIDUAL_NEXT_OF_KIN cDMA_INDIVIDUAL_NEXT_OF_KIN)
        {
            if (ModelState.IsValid)
            {
                _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Add(cDMA_INDIVIDUAL_NEXT_OF_KIN);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LGA = new SelectList(_db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.LGA);
            ViewBag.RELATIONSHIP = new SelectList(_db.CDMA_CUST_REL_TYPE, "REL_CODE", "REL_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.RELATIONSHIP);
            ViewBag.STATE = new SelectList(_db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.STATE);
            ViewBag.TITLE = new SelectList(_db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.TITLE);
            return View(cDMA_INDIVIDUAL_NEXT_OF_KIN);
        }

        // GET: CustNok/Edit/5
        public ActionResult Edit_(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_NEXT_OF_KIN cDMA_INDIVIDUAL_NEXT_OF_KIN = _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Find(id);
            if (cDMA_INDIVIDUAL_NEXT_OF_KIN == null)
            {
                return HttpNotFound();
            }
            ViewBag.LGA = new SelectList(_db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.LGA);
            ViewBag.RELATIONSHIP = new SelectList(_db.CDMA_CUST_REL_TYPE, "REL_CODE", "REL_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.RELATIONSHIP);
            ViewBag.STATE = new SelectList(_db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.STATE);
            ViewBag.TITLE = new SelectList(_db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.TITLE);
            return View(cDMA_INDIVIDUAL_NEXT_OF_KIN);
        }

        // POST: CustNok/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Scaffold([Bind(Include = "CUSTOMER_NO,TITLE,SURNAME,FIRST_NAME,OTHER_NAME,DATE_OF_BIRTH,SEX,RELATIONSHIP,OFFICE_NO,MOBILE_NO,EMAIL_ADDRESS,HOUSE_NUMBER,IDENTIFICATION_TYPE,ID_EXPIRY_DATE,ID_ISSUE_DATE,RESIDENT_PERMIT_NUMBER,PLACE_OF_ISSUANCE,STREET_NAME,NEAREST_BUS_STOP_LANDMARK,CITY_TOWN,LGA,ZIP_POSTAL_CODE,STATE,COUNTRY,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS,BRANCH_CODE")] CDMA_INDIVIDUAL_NEXT_OF_KIN cDMA_INDIVIDUAL_NEXT_OF_KIN)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cDMA_INDIVIDUAL_NEXT_OF_KIN).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LGA = new SelectList(_db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.LGA);
            ViewBag.RELATIONSHIP = new SelectList(_db.CDMA_CUST_REL_TYPE, "REL_CODE", "REL_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.RELATIONSHIP);
            ViewBag.STATE = new SelectList(_db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.STATE);
            ViewBag.TITLE = new SelectList(_db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.TITLE);
            return View(cDMA_INDIVIDUAL_NEXT_OF_KIN);
        }

        // GET: CustNok/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_NEXT_OF_KIN cDMA_INDIVIDUAL_NEXT_OF_KIN = _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Find(id);
            if (cDMA_INDIVIDUAL_NEXT_OF_KIN == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_NEXT_OF_KIN);
        }

        // POST: CustNok/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_INDIVIDUAL_NEXT_OF_KIN cDMA_INDIVIDUAL_NEXT_OF_KIN = _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Find(id);
            _db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Remove(cDMA_INDIVIDUAL_NEXT_OF_KIN);
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

        #endregion
        
    }
}
