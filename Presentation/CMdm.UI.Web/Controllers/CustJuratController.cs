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
    public class CustJuratController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }


            var model = (from c in db.CDMA_JURAT

                         where c.CUSTOMER_NO == id
                         select new CustomerJuratModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             DATE_OF_OATH = c.DATE_OF_OATH,
                             NAME_OF_INTERPRETER = c.NAME_OF_INTERPRETER,
                             ADDRESS_OF_INTERPRETER = c.ADDRESS_OF_INTERPRETER,
                             TELEPHONE_NO = c.TELEPHONE_NO,
                             LANGUAGE_OF_INTERPRETATION = c.LANGUAGE_OF_INTERPRETATION,
                         }).FirstOrDefault();


            PrepareModel(model);
            return View(model);
        }

        // POST: CustJurat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerJuratModel cjmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_JURAT.FirstOrDefault(o => o.CUSTOMER_NO == cjmodel.CUSTOMER_NO);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", cjmodel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.DATE_OF_OATH = cjmodel.DATE_OF_OATH;
                        entity.NAME_OF_INTERPRETER = cjmodel.NAME_OF_INTERPRETER;
                        entity.ADDRESS_OF_INTERPRETER = cjmodel.ADDRESS_OF_INTERPRETER;
                        entity.TELEPHONE_NO = cjmodel.TELEPHONE_NO;
                        entity.LANGUAGE_OF_INTERPRETATION = cjmodel.LANGUAGE_OF_INTERPRETATION;
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_JURAT.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                SuccessNotification("JURAT Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = cjmodel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(cjmodel);
            return View(cjmodel);
        }
        public ActionResult Create()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();

            var model = new CustomerJuratModel();
            PrepareModel(model);
            return View(model);
        }

        // POST: CustJurat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerJuratModel cjmodel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string ip_address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (ModelState.IsValid)
            {
                CDMA_JURAT jurat = new CDMA_JURAT
                {
                    CUSTOMER_NO = cjmodel.CUSTOMER_NO,
                    DATE_OF_OATH = cjmodel.DATE_OF_OATH,
                    NAME_OF_INTERPRETER = cjmodel.NAME_OF_INTERPRETER,
                    ADDRESS_OF_INTERPRETER = cjmodel.ADDRESS_OF_INTERPRETER,
                    TELEPHONE_NO = cjmodel.TELEPHONE_NO,
                    LANGUAGE_OF_INTERPRETATION = cjmodel.LANGUAGE_OF_INTERPRETATION,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                db.CDMA_JURAT.Add(jurat);
                db.SaveChanges();


                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New JURAT has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = cjmodel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(cjmodel);
            return View(cjmodel);
        }

        [NonAction]
        protected virtual void PrepareModel(CustomerJuratModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model == null)
                throw new ArgumentNullException("model");
        }

        #region scaffolded
        // GET: CustJurat
        public ActionResult Index()
        {
            return View(db.CDMA_JURAT.ToList());
        }

        // GET: CustJurat/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_JURAT cDMA_JURAT = db.CDMA_JURAT.Find(id);
            if (cDMA_JURAT == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_JURAT);
        }

        // GET: CustJurat/Create
        public ActionResult Create_()
        {
            return View();
        }

        // POST: CustJurat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_1([Bind(Include = "CUSTOMER_NO,DATE_OF_OATH,NAME_OF_INTERPRETER,ADDRESS_OF_INTERPRETER,TELEPHONE_NO,LANGUAGE_OF_INTERPRETATION,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_JURAT cDMA_JURAT)
        {
            if (ModelState.IsValid)
            {
                db.CDMA_JURAT.Add(cDMA_JURAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cDMA_JURAT);
        }

        // GET: CustJurat/Edit/5
        public ActionResult Edit_(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_JURAT cDMA_JURAT = db.CDMA_JURAT.Find(id);
            if (cDMA_JURAT == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_JURAT);
        }

        // POST: CustJurat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_1([Bind(Include = "CUSTOMER_NO,DATE_OF_OATH,NAME_OF_INTERPRETER,ADDRESS_OF_INTERPRETER,TELEPHONE_NO,LANGUAGE_OF_INTERPRETATION,CREATED_DATE,CREATED_BY,LAST_MODIFIED_DATE,LAST_MODIFIED_BY,AUTHORISED,AUTHORISED_BY,AUTHORISED_DATE,IP_ADDRESS")] CDMA_JURAT cDMA_JURAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDMA_JURAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cDMA_JURAT);
        }

        // GET: CustJurat/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDMA_JURAT cDMA_JURAT = db.CDMA_JURAT.Find(id);
            if (cDMA_JURAT == null)
            {
                return HttpNotFound();
            }
            return View(cDMA_JURAT);
        }

        // POST: CustJurat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CDMA_JURAT cDMA_JURAT = db.CDMA_JURAT.Find(id);
            db.CDMA_JURAT.Remove(cDMA_JURAT);
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
