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
using CMdm.UI.Web.Models.MdmWeight;
using CMdm.Framework.Controllers;
using CMdm.UI.Web.Helpers.CrossCutting.Security;

namespace CMdm.UI.Web.Controllers
{
    public class MdmWeightsController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: MdmWeights
        public ActionResult Index()
        {
            IQueryable<MdmWeights> weights = db.MDM_WEIGHTS;

            var result = weights.Select(c => new MdmWeightModel
            {
                WEIGHT_ID = c.WEIGHT_ID,

                WEIGHT_DESC = c.WEIGHT_DESC,
                WEIGHT_VALUE = c.WEIGHT_VALUE,
                CREATED_BY = c.CREATED_BY,
                CREATED_DATE = c.CREATED_DATE,
                LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                RECORD_STATUS = c.RECORD_STATUS
                //RECORD_STATUS = c.RECORD_STATUS,
            });

            return View(result.ToList());
            
        }

        // GET: MdmWeights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            if (mdmWeights == null)
            {
                return HttpNotFound();
            }
            return View(mdmWeights);
        }

        // GET: MdmWeights/Create
        public ActionResult Create()
        {
            var model = new MdmWeightModel();
            return View(model);
        }

        // POST: MdmWeights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MdmWeightModel mdmWeights, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                MdmWeights mdmCat = new MdmWeights
                {
                    WEIGHT_ID = mdmWeights.WEIGHT_ID,
                    WEIGHT_DESC = mdmWeights.WEIGHT_DESC,
                    WEIGHT_VALUE = mdmWeights.WEIGHT_VALUE,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    RECORD_STATUS = mdmWeights.RECORD_STATUS

                };
                db.MDM_WEIGHTS.Add(mdmCat);
                db.SaveChanges();
                db.Entry(mdmCat).GetDatabaseValues();

                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New Weight has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = mdmCat.WEIGHT_ID }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            

            return View(mdmWeights);
        }

        // GET: MdmWeights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            if (mdmWeights == null)
            {
                return HttpNotFound();
            }
            var model = new MdmWeightModel
            {
                WEIGHT_ID = mdmWeights.WEIGHT_ID,
                WEIGHT_VALUE = mdmWeights.WEIGHT_VALUE,
                WEIGHT_DESC = mdmWeights.WEIGHT_DESC,
                CREATED_DATE = mdmWeights.CREATED_DATE,
                CREATED_BY = mdmWeights.CREATED_BY,
                LAST_MODIFIED_BY = mdmWeights.LAST_MODIFIED_BY,
                LAST_MODIFIED_DATE = mdmWeights.LAST_MODIFIED_DATE
            };
            return View(model);
        }

        // POST: MdmWeights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MdmWeightModel mdmWeight, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.MDM_WEIGHTS.FirstOrDefault(o => o.WEIGHT_ID == mdmWeight.WEIGHT_ID);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", mdmWeight.WEIGHT_ID);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.WEIGHT_DESC = mdmWeight.WEIGHT_DESC;
                        entity.WEIGHT_VALUE = mdmWeight.WEIGHT_VALUE;
                        entity.RECORD_STATUS = mdmWeight.RECORD_STATUS;
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        db.MDM_WEIGHTS.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                SuccessNotification("Item Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = mdmWeight.WEIGHT_ID }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }           
            return View(mdmWeight);
        }

        // GET: MdmWeights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            if (mdmWeights == null)
            {
                return HttpNotFound();
            }
            return View(mdmWeights);
        }

        // POST: MdmWeights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MdmWeights mdmWeights = db.MDM_WEIGHTS.Find(id);
            db.MDM_WEIGHTS.Remove(mdmWeights);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetWeights()
        {
            var db = new AppDbContext();
            var weights = from Weights in db.MDM_WEIGHTS
                          select new
                          {
                              WEIGHT_ID = Weights.WEIGHT_ID,
                              WEIGHT_DESC = Weights.WEIGHT_DESC
                          };

            return Json(weights.OrderBy(a => a.WEIGHT_ID), JsonRequestBehavior.AllowGet);
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
