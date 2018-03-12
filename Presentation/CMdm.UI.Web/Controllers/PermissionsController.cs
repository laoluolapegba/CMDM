using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Data.Rbac;
using CMdm.UI.Web.Models.UserAdmin;
using CMdm.Framework.Kendoui;
using CMdm.Services.UserAdmin;
using CMdm.Framework.Controllers;

namespace CMdm.UI.Web.Controllers
{
    public class PermissionsController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IPermissionsService _permService;

        #region Constructors
        public PermissionsController()
        {
            _permService = new PermissionsService();
        }
        #endregion
        // GET: Permissions
        public ActionResult Index()
        {

            return RedirectToAction("List");// View(db.CM_PERMISSIONS.ToList());
        }

        // GET: Permissions/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CM_PERMISSIONS cM_PERMISSIONS = db.CM_PERMISSIONS.Find(id);
            if (cM_PERMISSIONS == null)
            {
                return HttpNotFound();
            }
            return View(cM_PERMISSIONS);
        }

        // GET: Permissions/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var model = new PermissionListModel();
            //AppDbContext db = new AppDbContext();
            model.Permissions = new SelectList(db.CM_PERMISSIONS, "PERMISSION_ID", "PERMISSIONDESCRIPTION").ToList();
            
            return View(model);
        }

        // POST: Permissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]        
        [ValidateAntiForgeryToken]
        public ActionResult Create(PermissionListModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                CM_PERMISSIONS cM_PERMISSIONS = new CM_PERMISSIONS
                {
                    PERMISSION_ID = model.Id,
                    PERMISSIONDESCRIPTION = model.PERMISSIONDESCRIPTION,
                    PARENT_PERMISSION = model.PARENT_PERMISSION,
                    ISACTIVE = model.ISACTIVE,
                    ACTION_NAME = model.ACTION_NAME,
                    CONTROLLER_NAME = model.CONTROLLER_NAME,
                    FORM_URL = model.FORM_URL,
                    ICON_CLASS = "",
                    TOGGLE_ICON = "fa fa-angle-left",
                    ISOPEN_CLASS = "",

                };
                db.CM_PERMISSIONS.Add(cM_PERMISSIONS);
                db.SaveChanges();
                db.Entry(cM_PERMISSIONS).GetDatabaseValues();

                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New Permission has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { PERMISSION_ID = cM_PERMISSIONS.PERMISSION_ID }) : RedirectToAction("Index");
            }
            model.Permissions = new SelectList(db.CM_PERMISSIONS, "PERMISSION_ID", "PERMISSIONDESCRIPTION").ToList();

            return View(model);
        }

        // GET: Permissions/Edit/5
        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var item = _permService.GetItembyId(id);
            if (item == null)
                //No store found with the specified id
                return RedirectToAction("List");


            var mdmPerm = (from x in db.CM_PERMISSIONS
                           where x.PERMISSION_ID == id
                           select new PermissionListModel
                           {
                               Id = (int)x.PERMISSION_ID,
                               PERMISSIONDESCRIPTION = x.PERMISSIONDESCRIPTION,
                               PARENT_PERMISSION = x.PARENT_PERMISSION,
                               ISACTIVE = x.ISACTIVE,
                               ACTION_NAME = x.ACTION_NAME,
                               CONTROLLER_NAME = x.CONTROLLER_NAME,
                               FORM_URL = x.FORM_URL,
                               ICON_CLASS = x.ICON_CLASS,
                               TOGGLE_ICON = x.TOGGLE_ICON,
                               ISOPEN_CLASS = x.ISOPEN_CLASS
                           }).FirstOrDefault();

            var model = new PermissionListModel
            {
                Id = mdmPerm.Id,
                PERMISSIONDESCRIPTION = mdmPerm.PERMISSIONDESCRIPTION,
                PARENT_PERMISSION = mdmPerm.PARENT_PERMISSION,
                ISACTIVE = mdmPerm.ISACTIVE,
                ACTION_NAME = mdmPerm.ACTION_NAME,
                CONTROLLER_NAME = mdmPerm.CONTROLLER_NAME,
                FORM_URL = mdmPerm.FORM_URL,
                ICON_CLASS = mdmPerm.ICON_CLASS,
                TOGGLE_ICON = mdmPerm.TOGGLE_ICON,
                ISOPEN_CLASS = mdmPerm.ISOPEN_CLASS
            };
            //AppDbContext db = new AppDbContext();
            model.Permissions = new SelectList(db.CM_PERMISSIONS, "PERMISSION_ID", "PERMISSIONDESCRIPTION").ToList();

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //CM_PERMISSIONS cM_PERMISSIONS = db.CM_PERMISSIONS.Find(id);
            //if (cM_PERMISSIONS == null)
            //{
            //    return HttpNotFound();
            //}
            return View(model );
        }

        // POST: Permissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PermissionListModel model, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CM_PERMISSIONS.FirstOrDefault(o => o.PERMISSION_ID == model.Id );
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", model.Id);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.PERMISSIONDESCRIPTION = model.PERMISSIONDESCRIPTION;
                        entity.PARENT_PERMISSION = model.PARENT_PERMISSION;
                        entity.ACTION_NAME = model.ACTION_NAME;
                        entity.CONTROLLER_NAME = model.CONTROLLER_NAME;
                        entity.FORM_URL = model.FORM_URL;
                        
                        entity.ISACTIVE = model.ISACTIVE;
                        db.CM_PERMISSIONS.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                SuccessNotification("User Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("Index");

               // db.Entry(cM_PERMISSIONS).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            model.Permissions = new SelectList(db.CM_PERMISSIONS, "PERMISSION_ID", "PERMISSIONDESCRIPTION").ToList();
            return View(model);
        }

        // GET: Permissions/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var item = _permService.GetItembyId(id);
            if (item == null)
                //No store found with the specified id
                return RedirectToAction("List");
            try
            {
                //_permService.Delete(item);
                CM_PERMISSIONS cM_PERMISSIONS = db.CM_PERMISSIONS.Find(id);
                db.CM_PERMISSIONS.Remove(cM_PERMISSIONS);
                db.SaveChanges();
                SuccessNotification("Admin.Configuration.Stores.Deleted");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.PERMISSION_ID });
            }
            //CM_PERMISSIONS cM_PERMISSIONS = db.CM_PERMISSIONS.Find(id);
            //if (cM_PERMISSIONS == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cM_PERMISSIONS);
        }

        // POST: Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            CM_PERMISSIONS cM_PERMISSIONS = db.CM_PERMISSIONS.Find(id);
            db.CM_PERMISSIONS.Remove(cM_PERMISSIONS);
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
        public ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            
            var model = new PermissionListModel(); //new SelectList(database.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME").ToList();
            model.Permissions = new SelectList(db.CM_PERMISSIONS, "PERMISSION_ID", "PERMISSIONDESCRIPTION").ToList();
            model.Permissions.Add(new SelectListItem
            {
                Text = "None",
                Value = "0"
            });
            
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult List(DataSourceRequest command, PermissionListModel model, string sort, string sortDir)
        {

            var items = _permService.GetAllPermissions(
                permdesc: model.SearchPermission,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize,
                sortExpression: "");

            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new PermissionListModel
                {
                    Id = (int)x.PERMISSION_ID,
                    PERMISSIONDESCRIPTION = x.PERMISSIONDESCRIPTION,
                    PARENT_PERMISSION = x.PARENT_PERMISSION,
                    ACTION_NAME = x.ACTION_NAME,
                    CONTROLLER_NAME = x.CONTROLLER_NAME,
                    FORM_URL = x.FORM_URL,
                    ISACTIVE = x.ISACTIVE,
                    ICON_CLASS = x.ICON_CLASS,
                    ISOPEN_CLASS = x.ISOPEN_CLASS,
                    IMAGE_URL = x.IMAGE_URL,
                    TOGGLE_ICON = x.TOGGLE_ICON


                }),
                Total = items.TotalCount
            };
            return Json(gridModel);
        }
    }
}
