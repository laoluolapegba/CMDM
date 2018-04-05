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

namespace CMdm.UI.Web.Controllers
{
    public class CustNokController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IDqQueService _dqQueService;
        public CustNokController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new DqQueService();
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

            var changeId = db.CDMA_CHANGE_LOGS.Where(a => a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet = db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId); //.Select(a=>a.PROPERTYNAME);
            var model = (from c in db.CDMA_INDIVIDUAL_NEXT_OF_KIN
                         where c.CUSTOMER_NO == querecord.CUST_ID
                         select new CustomerNOKModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             COUNTRY = c.COUNTRY,
                             CITY_TOWN = c.CITY_TOWN,
                             DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                             EMAIL_ADDRESS = c.EMAIL_ADDRESS,
                             FIRST_NAME = c.FIRST_NAME,
                             HOUSE_NUMBER = c.HOUSE_NUMBER,
                             IDENTIFICATION_TYPE = c.IDENTIFICATION_TYPE,
                             ID_EXPIRY_DATE = c.ID_EXPIRY_DATE,
                             ID_ISSUE_DATE = c.ID_ISSUE_DATE,
                             LGA = c.LGA,
                             MOBILE_NO = c.MOBILE_NO,
                             NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
                             OFFICE_NO = c.OFFICE_NO,
                             OTHER_NAME = c.OTHER_NAME,
                             PLACE_OF_ISSUANCE = c.PLACE_OF_ISSUANCE,
                             RELATIONSHIP = c.RELATIONSHIP,
                             RESIDENT_PERMIT_NUMBER = c.RESIDENT_PERMIT_NUMBER,
                             SEX = c.SEX,
                             STATE = c.STATE,
                             STREET_NAME = c.STREET_NAME,
                             SURNAME = c.SURNAME,
                             TITLE = c.TITLE,
                             ZIP_POSTAL_CODE = c.ZIP_POSTAL_CODE,
                             LastUpdatedby = c.LAST_MODIFIED_BY,
                             LastUpdatedDate = c.LAST_MODIFIED_DATE,
                             LastAuthdby = c.AUTHORISED_BY,
                             LastAuthDate = c.AUTHORISED_DATE,
                             ExceptionId = querecord.EXCEPTION_ID
                         }).FirstOrDefault();

            //var modelProperties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static);

            
            //List<string> props = new List<string>();
            foreach (var item in model.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
            {
                foreach (var item2 in changedSet)
                {
                    if (item2.PROPERTYNAME == item.Name)
                    {
                        ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                    }
                }
                //props.Add(item.Name);

            }
            //var matchItems = props.Intersect(changedSet);
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
           
            var model = (from c in db.CDMA_INDIVIDUAL_NEXT_OF_KIN
                              
                              where c.CUSTOMER_NO == id
                              select new CustomerNOKModel
                              {
                                  CUSTOMER_NO = c.CUSTOMER_NO,
                                  COUNTRY = c.COUNTRY,
                                  CITY_TOWN = c.CITY_TOWN,
                                  DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                                  EMAIL_ADDRESS = c.EMAIL_ADDRESS,
                                  FIRST_NAME = c.FIRST_NAME,
                                  HOUSE_NUMBER = c.HOUSE_NUMBER,
                                  IDENTIFICATION_TYPE = c.IDENTIFICATION_TYPE,
                                  ID_EXPIRY_DATE = c.ID_EXPIRY_DATE,
                                  ID_ISSUE_DATE = c.ID_ISSUE_DATE,
                                  LGA = c.LGA,
                                  MOBILE_NO = c.MOBILE_NO,
                                  NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
                                  OFFICE_NO = c.OFFICE_NO,
                                  OTHER_NAME = c.OTHER_NAME,
                                  PLACE_OF_ISSUANCE = c.PLACE_OF_ISSUANCE,
                                  RELATIONSHIP = c.RELATIONSHIP,
                                  RESIDENT_PERMIT_NUMBER =c.RESIDENT_PERMIT_NUMBER,
                                  SEX = c.SEX,
                                  STATE = c.STATE,
                                  STREET_NAME = c.STREET_NAME,
                                  SURNAME = c.SURNAME,
                                  TITLE = c.TITLE,
                                  ZIP_POSTAL_CODE = c.ZIP_POSTAL_CODE

                                  
                              }).FirstOrDefault();

            
            PrepareModel(model);
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
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.FirstOrDefault(o => o.CUSTOMER_NO == nokmodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", nokmodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.SURNAME = nokmodel.SURNAME;
                        entity.CITY_TOWN = nokmodel.CITY_TOWN;
                        entity.COUNTRY = nokmodel.COUNTRY;
                        entity.DATE_OF_BIRTH = nokmodel.DATE_OF_BIRTH;
                        entity.EMAIL_ADDRESS = nokmodel.EMAIL_ADDRESS;
                        entity.FIRST_NAME = nokmodel.FIRST_NAME;
                        entity.HOUSE_NUMBER = nokmodel.HOUSE_NUMBER;

                        entity.MOBILE_NO = nokmodel.MOBILE_NO;
                        entity.PLACE_OF_ISSUANCE = nokmodel.PLACE_OF_ISSUANCE;
                        entity.RELATIONSHIP = nokmodel.RELATIONSHIP;
                        entity.RESIDENT_PERMIT_NUMBER = nokmodel.RESIDENT_PERMIT_NUMBER;
                        entity.SEX = nokmodel.SEX;
                        entity.STREET_NAME = nokmodel.STREET_NAME;
                        entity.TITLE = nokmodel.TITLE;
                        entity.ZIP_POSTAL_CODE = nokmodel.ZIP_POSTAL_CODE;
                        entity.ID_EXPIRY_DATE = nokmodel.ID_EXPIRY_DATE;
                        entity.ID_ISSUE_DATE = nokmodel.ID_ISSUE_DATE;
                        entity.IDENTIFICATION_TYPE = nokmodel.IDENTIFICATION_TYPE;
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges(identity.ProfileId.ToString(), nokmodel.CUSTOMER_NO);

                    }
                }

                SuccessNotification("NOK Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = nokmodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(nokmodel);
            return View(nokmodel);
        }
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
                db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Add(nok);
                db.SaveChanges();
                

                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New NOK has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = nokmodel.CUSTOMER_NO }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            PrepareModel(nokmodel);
            return View(nokmodel);
        }

        [NonAction]
        protected virtual void PrepareModel(CustomerNOKModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

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
            model.IdTypes = new SelectList(db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE").ToList();

            model.Countries = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
            
            model.LocalGovts = new SelectList(db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME").ToList();
            model.RelationshipTypes = new SelectList(db.CDMA_CUST_REL_TYPE, "REL_CODE", "REL_DESC").ToList();
            model.States = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME").ToList();
            model.TitleTypes = new SelectList(db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC").ToList();


        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("approve")]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(CustomerNOKModel nokmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                
                _dqQueService.ApproveExceptionQueItems(nokmodel.ExceptionId.ToString());
                //using (var db = new AppDbContext())
                //{
                //    var entity = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.FirstOrDefault(o => o.CUSTOMER_NO == nokmodel.CUSTOMER_NO);
                //    if (entity == null)
                //    {
                //        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", nokmodel.CUSTOMER_NO);
                //        ModelState.AddModelError("", errorMessage);
                //    }
                //    else
                //    {                       
                //        entity.AUTHORISED = "A";
                //        db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Attach(entity);
                //        db.Entry(entity).State = EntityState.Modified;
                //        db.SaveChanges();

                //    }
                //}

                SuccessNotification("NOK Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = nokmodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "CustNok");
                //return RedirectToAction("Index");
            }
            PrepareModel(nokmodel);
            return View(nokmodel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove(CustomerNOKModel nokmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.FirstOrDefault(o => o.CUSTOMER_NO == nokmodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", nokmodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.AUTHORISED = "N";
                        db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                SuccessNotification("NOK Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = nokmodel.CUSTOMER_NO }) : RedirectToAction("Authorize", "CustNok");
                //return RedirectToAction("Index");
            }
            PrepareModel(nokmodel);
            return View(nokmodel);
        }

        #region Scaffolded
        // GET: CustNok
        public ActionResult Index()
        {
            var cDMA_INDIVIDUAL_NEXT_OF_KIN = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Include(c => c.Countries).Include(c => c.IdTypes).Include(c => c.LocalGovts).Include(c => c.RelationshipTypes).Include(c => c.States).Include(c => c.TitleTypes);
            return View(cDMA_INDIVIDUAL_NEXT_OF_KIN.ToList());
        }

        // GET: CustNok/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_NEXT_OF_KIN cDMA_INDIVIDUAL_NEXT_OF_KIN = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Find(id);
            if (cDMA_INDIVIDUAL_NEXT_OF_KIN == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_INDIVIDUAL_NEXT_OF_KIN);
        }

        // GET: CustNok/Create
        public ActionResult Create_()
        {
            ViewBag.COUNTRY = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.IDENTIFICATION_TYPE = new SelectList(db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE");
            ViewBag.LGA = new SelectList(db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME");
            ViewBag.RELATIONSHIP = new SelectList(db.CDMA_CUST_REL_TYPE, "REL_CODE", "REL_DESC");
            ViewBag.STATE = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME");
            ViewBag.TITLE = new SelectList(db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC");
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
                db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Add(cDMA_INDIVIDUAL_NEXT_OF_KIN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.COUNTRY = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.COUNTRY);
            ViewBag.IDENTIFICATION_TYPE = new SelectList(db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE", cDMA_INDIVIDUAL_NEXT_OF_KIN.IDENTIFICATION_TYPE);
            ViewBag.LGA = new SelectList(db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.LGA);
            ViewBag.RELATIONSHIP = new SelectList(db.CDMA_CUST_REL_TYPE, "REL_CODE", "REL_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.RELATIONSHIP);
            ViewBag.STATE = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.STATE);
            ViewBag.TITLE = new SelectList(db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.TITLE);
            return View(cDMA_INDIVIDUAL_NEXT_OF_KIN);
        }

        // GET: CustNok/Edit/5
        public ActionResult Edit_(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_NEXT_OF_KIN cDMA_INDIVIDUAL_NEXT_OF_KIN = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Find(id);
            if (cDMA_INDIVIDUAL_NEXT_OF_KIN == null)
            {
                return HttpNotFound();
            }
            ViewBag.COUNTRY = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.COUNTRY);
            ViewBag.IDENTIFICATION_TYPE = new SelectList(db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE", cDMA_INDIVIDUAL_NEXT_OF_KIN.IDENTIFICATION_TYPE);
            ViewBag.LGA = new SelectList(db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.LGA);
            ViewBag.RELATIONSHIP = new SelectList(db.CDMA_CUST_REL_TYPE, "REL_CODE", "REL_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.RELATIONSHIP);
            ViewBag.STATE = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.STATE);
            ViewBag.TITLE = new SelectList(db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.TITLE);
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
                db.Entry(cDMA_INDIVIDUAL_NEXT_OF_KIN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COUNTRY = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.COUNTRY);
            ViewBag.IDENTIFICATION_TYPE = new SelectList(db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE", cDMA_INDIVIDUAL_NEXT_OF_KIN.IDENTIFICATION_TYPE);
            ViewBag.LGA = new SelectList(db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.LGA);
            ViewBag.RELATIONSHIP = new SelectList(db.CDMA_CUST_REL_TYPE, "REL_CODE", "REL_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.RELATIONSHIP);
            ViewBag.STATE = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", cDMA_INDIVIDUAL_NEXT_OF_KIN.STATE);
            ViewBag.TITLE = new SelectList(db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC", cDMA_INDIVIDUAL_NEXT_OF_KIN.TITLE);
            return View(cDMA_INDIVIDUAL_NEXT_OF_KIN);
        }

        // GET: CustNok/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_INDIVIDUAL_NEXT_OF_KIN cDMA_INDIVIDUAL_NEXT_OF_KIN = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Find(id);
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
            CDMA_INDIVIDUAL_NEXT_OF_KIN cDMA_INDIVIDUAL_NEXT_OF_KIN = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Find(id);
            db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Remove(cDMA_INDIVIDUAL_NEXT_OF_KIN);
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
        
    }
}
