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
using CMdm.UI.Web.Models.DqCatalogs;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Framework.Controllers;

namespace CMdm.UI.Web.Controllers
{
    public class MdmCatalogsController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: MdmCatalogs
        public ActionResult Index()
        {
            //IQueryable<MdmCatalog> catalogs = db.MdmCatalogs;

            var result = (from c in db.MdmCatalogs
                          join u in db.CM_USER_PROFILE on c.CREATED_BY equals u.PROFILE_ID
                          //join u2 in db.CM_USER_PROFILE on c.LAST_MODIFIED_BY equals u2.PROFILE_ID into uu2
                          //from u2 in uu2.DefaultIfEmpty()
                          select new DqCatalogListModel
                          {
                              CATALOG_ID = c.CATALOG_ID,

                              CATALOG_NAME = c.CATALOG_NAME,
                              CATEGORY_ID = c.CATEGORY_ID,
                              CREATED_BY = u.FIRSTNAME + " " + u.LASTNAME,
                              CREATED_DATE = c.CREATED_DATE,
                              LAST_MODIFIED_BY = "", // u2.FIRSTNAME + " " + u2.LASTNAME,
                              LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                          }).AsQueryable();

                              //var result = catalogs.Select(c => new DqCatalogListModel
                              //{
                              //    CATALOG_ID = c.CATALOG_ID,

                              //    CATALOG_NAME = c.CATALOG_NAME,
                              //    CATEGORY_ID = c.CATEGORY_ID,
                              //    CREATED_BY = "",
                              //    CREATED_DATE = c.CREATED_DATE,
                              //    LAST_MODIFIED_BY = "",
                              //    LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                              //    //RECORD_STATUS = c.RECORD_STATUS,
                              //});

            return View( result.ToList());
            //return View(db.MdmCatalogs.ToList());
        }

        // GET: MdmCatalogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmCatalog mdmCatalog = db.MdmCatalogs.Find(id);
            if (mdmCatalog == null)
            {
                return HttpNotFound();
            }
            return View(mdmCatalog);
        }

        // GET: MdmCatalogs/Create
        public ActionResult Create()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();

            var model = new DqCatalogListModel();
            PrepareModel(model);
            return View(model);
        }

        // POST: MdmCatalogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DqCatalogListModel mdmCatalog, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                MdmCatalog mdmCat = new MdmCatalog
                {
                    CATALOG_ID = mdmCatalog.CATALOG_ID,
                    CATEGORY_ID = mdmCatalog.CATEGORY_ID,
                    CATALOG_NAME = mdmCatalog.CATALOG_NAME,
                    CREATED_BY = identity.ProfileId,
                    CREATED_DATE = DateTime.Now

                };
                db.MdmCatalogs.Add(mdmCat);
                    db.SaveChanges();
                db.Entry(mdmCat).GetDatabaseValues();

                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New Catalog has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = mdmCat.CATALOG_ID }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            PrepareModel(mdmCatalog);
            return View(mdmCatalog);
        }

        // GET: MdmCatalogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //MdmCatalog mdmCatalog = db.MdmCatalogs.Find(id);

            
            var mdmCatalog = (from c in db.MdmCatalogs
                          join u in db.CM_USER_PROFILE on c.CREATED_BY equals u.PROFILE_ID
                              //join u2 in db.CM_USER_PROFILE on c.LAST_MODIFIED_BY equals u2.PROFILE_ID into uu2
                              //from u2 in uu2.DefaultIfEmpty()
                              where c.CATALOG_ID == id
                          select new DqCatalogListModel
                          {
                              CATALOG_ID = c.CATALOG_ID,

                              CATALOG_NAME = c.CATALOG_NAME,
                              CATEGORY_ID = c.CATEGORY_ID,
                              CREATED_BY = u.FIRSTNAME + " " + u.LASTNAME,
                              CREATED_DATE = c.CREATED_DATE,
                              LAST_MODIFIED_BY = "",
                              LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                          }).FirstOrDefault();
            if (mdmCatalog == null)
            {
                return HttpNotFound();
            }
            var model = new DqCatalogListModel
            {
                CATALOG_ID = mdmCatalog.CATALOG_ID,
                CATALOG_NAME = mdmCatalog.CATALOG_NAME,
                CATEGORY_ID = mdmCatalog.CATEGORY_ID,
                CREATED_DATE = mdmCatalog.CREATED_DATE,
                CREATED_BY = mdmCatalog.CREATED_BY,
                LAST_MODIFIED_BY = mdmCatalog.LAST_MODIFIED_BY,
                LAST_MODIFIED_DATE = mdmCatalog.LAST_MODIFIED_DATE
            };
            PrepareModel(model);
            return View(model);
        }

        // POST: MdmCatalogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DqCatalogListModel mdmCatalog, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.MdmCatalogs.FirstOrDefault(o => o.CATALOG_ID == mdmCatalog.CATALOG_ID);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", mdmCatalog.CATALOG_ID);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.CATALOG_NAME = mdmCatalog.CATALOG_NAME;
                        entity.CATEGORY_ID = mdmCatalog.CATEGORY_ID;
                        entity.LAST_MODIFIED_BY = identity.ProfileId;
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        db.MdmCatalogs.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                        
                    }
                }
                
                SuccessNotification("Catalog Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = mdmCatalog.CATALOG_ID }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            PrepareModel(mdmCatalog);
            return View(mdmCatalog);
        }

        // GET: MdmCatalogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MdmCatalog mdmCatalog = db.MdmCatalogs.Find(id);
            if (mdmCatalog == null)
            {
                return HttpNotFound();
            }
            return View(mdmCatalog);
        }

        // POST: MdmCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MdmCatalog mdmCatalog = db.MdmCatalogs.Find(id);
            db.MdmCatalogs.Remove(mdmCatalog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetCatalogs()
        {
            var db = new AppDbContext();
            var catalogs = from Catalogs in db.MdmCatalogs
                          select new
                          {
                              CATALOG_ID = Catalogs.CATALOG_ID,
                              CATALOG_NAME = Catalogs.CATALOG_NAME
                          };

            return Json(catalogs.OrderBy(a => a.CATALOG_NAME), JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        protected virtual void PrepareModel(DqCatalogListModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model == null)
                throw new ArgumentNullException("model");
            model.Categories.Add(new SelectListItem
            {
                Text = "Corporate",
                Value = "1"
            });
            model.Categories.Add(new SelectListItem
            {
                Text = "Individual",
                Value = "2"
            });
            model.Categories.Add(new SelectListItem
            {
                Text = "Select Category",
                Value = "0"
            });
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
