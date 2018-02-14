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
using CMdm.UI.Web.Models.DqValidator;
using CMdm.Framework.Controllers;
using CMdm.UI.Web.Helpers.CrossCutting.Security;

namespace CMdm.UI.Web.Controllers
{
    public class MdmValidatorsController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: MdmValidators
        public ActionResult Index()
        {
            IQueryable<MdmRegex> catalogs = db.MdmRegex;

            var result = catalogs.Select(c => new DqValidatorModel
            {
                REGEX_ID = c.REGEX_ID,
                REGEX_DESC = c.REGEX_DESC,
                REGEX_NAME = c.REGEX_NAME,
                REGEX_STRING = c.REGEX_STRING
                //RECORD_STATUS = c.RECORD_STATUS,
            });

            return View(result.ToList());
           // return View(db.MdmRegex.ToList());
        }

        // GET: MdmValidators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmRegex mdmRegex = db.MdmRegex.Find(id);
            if (mdmRegex == null)
            {
                return HttpNotFound();
            }
            return View(mdmRegex);
        }

        // GET: MdmValidators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MdmValidators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DqValidatorModel mdmRegex, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                MdmRegex mdmCat = new MdmRegex
                {
                    REGEX_NAME = mdmRegex.REGEX_NAME,
                    REGEX_STRING = mdmRegex.REGEX_STRING,
                    REGEX_DESC = mdmRegex.REGEX_DESC

                };
                db.MdmRegex.Add(mdmCat);
                db.SaveChanges();
                db.Entry(mdmCat).GetDatabaseValues();

                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New Validator has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = mdmCat.REGEX_ID }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            
            return View(mdmRegex);
        }

        // GET: MdmValidators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmRegex mdmRegex = db.MdmRegex.Find(id);
            if (mdmRegex == null)
            {
                return HttpNotFound();
            }
            var model = new DqValidatorModel
            {
                REGEX_ID = mdmRegex.REGEX_ID,
                REGEX_NAME = mdmRegex.REGEX_NAME,
                REGEX_DESC= mdmRegex.REGEX_DESC,
                REGEX_STRING = mdmRegex.REGEX_STRING,

            };
            
            return View(model);
        }

        // POST: MdmValidators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DqValidatorModel mdmRegex, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.MdmRegex.FirstOrDefault(o => o.REGEX_ID == mdmRegex.REGEX_ID);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", mdmRegex.REGEX_ID);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.REGEX_NAME = mdmRegex.REGEX_NAME;
                        entity.REGEX_STRING = mdmRegex.REGEX_STRING;
                        entity.REGEX_DESC = mdmRegex.REGEX_DESC;
                        
                        db.MdmRegex.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                SuccessNotification("Validator Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = mdmRegex.REGEX_ID }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            //PrepareModel(mdmCatalog);
            return View(mdmRegex);
        }

        // GET: MdmValidators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmRegex mdmRegex = db.MdmRegex.Find(id);
            if (mdmRegex == null)
            {
                return HttpNotFound();
            }
            return View(mdmRegex);
        }

        // POST: MdmValidators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MdmRegex mdmRegex = db.MdmRegex.Find(id);
            db.MdmRegex.Remove(mdmRegex);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetValidators()
        {
            var db = new AppDbContext();
            var vaidators = from regex in db.MdmRegex
                          select new
                          {
                              REGEX_ID = regex.REGEX_ID,
                              REGEX_NAME = regex.REGEX_NAME
                          };

            return Json(vaidators.OrderBy(a => a.REGEX_NAME), JsonRequestBehavior.AllowGet);
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
