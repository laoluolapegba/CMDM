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
using CMdm.Services.Security;
using CMdm.Framework.Controllers;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System.Reflection;
using System.Data.Entity.Validation;
using CMdm.Entities.ViewModels;

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
            /*
            var model = new PermissionListModel
            {
                Id = (int)item.PERMISSION_ID,
                PERMISSIONDESCRIPTION = item.PERMISSIONDESCRIPTION,
                PARENT_PERMISSION = item.PARENT_PERMISSION,
                ISACTIVE = item.ISACTIVE,
                ACTION_NAME = item.ACTION_NAME,
                CONTROLLER_NAME = item.CONTROLLER_NAME,
                FORM_URL = item.FORM_URL,
                ICON_CLASS = item.ICON_CLASS,
                TOGGLE_ICON = item.TOGGLE_ICON,
                ISOPEN_CLASS = item.ISOPEN_CLASS,
                UserRoles = item.CM_USER_ROLES
            };
            */
            
            //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var mdmPerm = (from x in db.CM_PERMISSIONS
                           //join rx in db.CM_ROLE_PERM_XREF on x.PERMISSION_ID equals rx.PERMISSION_ID 
                           //join r in db.CM_USER_ROLES on rx.ROLE_ID equals r.ROLE_ID                
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
                               ISOPEN_CLASS = x.ISOPEN_CLASS,
                               //UserRoles = rx.PermRoles
                           }).FirstOrDefault();
            /*
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
                ISOPEN_CLASS = mdmPerm.ISOPEN_CLASS,
                //UserRoles = mdmPerm.UserRoles
            };

            */
            
            //AppDbContext db = new AppDbContext();
            var roles = (from rx in db.CM_ROLE_PERM_XREF
                         join r in db.CM_USER_ROLES on rx.ROLE_ID equals r.ROLE_ID
                         where rx.PERMISSION_ID == id
                         select new RolesListModel
                         {
                             ROLE_ID = r.ROLE_ID,
                             ROLE_NAME = r.ROLE_NAME
                         }).ToList();
            //IEnumerable<CM_USER_ROLES> r = roles;
            //List<RolesListModel> llst = roles.Select(a => new { a.ROLE_ID, a.ROLE_NAME }).ToList();

            ViewBag.PermRoles = roles;
            mdmPerm.Permissions = new SelectList(db.CM_PERMISSIONS, "PERMISSION_ID", "PERMISSIONDESCRIPTION").ToList();
            ViewBag.RoleId = new SelectList(db.CM_USER_ROLES.OrderBy(p => p.ROLE_NAME), "ROLE_ID", "ROLE_NAME");


            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //CM_PERMISSIONS cM_PERMISSIONS = db.CM_PERMISSIONS.Find(id);
            //if (cM_PERMISSIONS == null)
            //{
            //    return HttpNotFound();
            //}
            return View(mdmPerm );
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
            ViewBag.RoleId = new SelectList(db.CM_USER_ROLES.OrderBy(p => p.ROLE_NAME), "ROLE_ID", "ROLE_NAME");
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

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult DeleteRoleFromPermissionReturnPartialView(int id, int permissionId)
        {
            //CM_USER_ROLES role = db.CM_USER_ROLES.Find(id);
            //CM_PERMISSIONS permission = db.CM_PERMISSIONS.Find(permissionId);

            CM_ROLE_PERM_XREF rp = db.CM_ROLE_PERM_XREF.FirstOrDefault(a => a.PERMISSION_ID == permissionId && a.ROLE_ID == id);
            db.CM_ROLE_PERM_XREF.Remove(rp);
            db.SaveChanges();
            //if (role.PERMISSIONS.Contains(permission))
            //{
            //    role.PERMISSIONS.Remove(permission);
            //    database.SaveChanges();
            //}
            var mdmPerm = (from x in db.CM_PERMISSIONS
                               //join rx in db.CM_ROLE_PERM_XREF on x.PERMISSION_ID equals rx.PERMISSION_ID 
                               //join r in db.CM_USER_ROLES on rx.ROLE_ID equals r.ROLE_ID                
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
                               ISOPEN_CLASS = x.ISOPEN_CLASS,
                               //UserRoles = rx.PermRoles
                           }).FirstOrDefault();
            var roles = (from rx in db.CM_ROLE_PERM_XREF
                         join r in db.CM_USER_ROLES on rx.ROLE_ID equals r.ROLE_ID
                         where rx.PERMISSION_ID == id
                         select new RolesListModel
                         {
                             ROLE_ID = r.ROLE_ID,
                             ROLE_NAME = r.ROLE_NAME
                         }).ToList();
            //IEnumerable<CM_USER_ROLES> r = roles;
            //List<RolesListModel> llst = roles.Select(a => new { a.ROLE_ID, a.ROLE_NAME }).ToList();

            ViewBag.PermRoles = roles;
            return PartialView("_ListRolesTable4Permission", mdmPerm);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult AddRole2PermissionReturnPartialView(int permissionId, int roleId)
        {
            //CM_USER_PROFILE role = db.CM_USER_PROFILE.Find(roleId);
            //CM_PERMISSIONS _permission = db.CM_PERMISSIONS.Find(permissionId);
            //string temp = Request.QueryString[0];
            //int id = Convert.ToInt32(RouteData.Values["id"]);

            CM_ROLE_PERM_XREF rp = db.CM_ROLE_PERM_XREF.FirstOrDefault(a => a.PERMISSION_ID == permissionId && a.ROLE_ID == roleId);
            if(rp == null)
            {
                var identity = ((CustomPrincipal)User).CustomIdentity;
                CM_ROLE_PERM_XREF roleperm = new CM_ROLE_PERM_XREF();
                roleperm.PERMISSION_ID = permissionId;
                roleperm.ROLE_ID = roleId;
                roleperm.CREATED_BY = identity.Name;
                roleperm.CREATED_DATE = DateTime.Now;
                db.CM_ROLE_PERM_XREF.Add(roleperm);
                db.SaveChanges();
            }

            var mdmPerm = (from x in db.CM_PERMISSIONS
                               //join rx in db.CM_ROLE_PERM_XREF on x.PERMISSION_ID equals rx.PERMISSION_ID 
                               //join r in db.CM_USER_ROLES on rx.ROLE_ID equals r.ROLE_ID                
                           where x.PERMISSION_ID == permissionId
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
                               ISOPEN_CLASS = x.ISOPEN_CLASS,
                               //UserRoles = rx.PermRoles
                           }).FirstOrDefault();
            var roles = (from rx in db.CM_ROLE_PERM_XREF
                         join r in db.CM_USER_ROLES on rx.ROLE_ID equals r.ROLE_ID
                         where rx.PERMISSION_ID == permissionId
                         select new RolesListModel
                         {
                             ROLE_ID = r.ROLE_ID,
                             ROLE_NAME = r.ROLE_NAME
                         }).ToList();
         
            ViewBag.PermRoles = roles;


            //if (!role.PERMISSIONS.Contains(_permission))
            //{
            //    role.PERMISSIONS.Add(_permission);
            //    database.SaveChanges();
            //}
            return PartialView("_ListRolesTable4Permission", mdmPerm);
        }

        public ActionResult Loadnew()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            GetAssemblyActions();
            var model = new PermissionListModel(); //new SelectList(database.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME").ToList();
            model.Permissions = new SelectList(db.CM_PERMISSIONS, "PERMISSION_ID", "PERMISSIONDESCRIPTION").ToList();
            model.Permissions.Add(new SelectListItem
            {
                Text = "None",
                Value = "0"
            });

            //return View(model);
            return RedirectToAction("List");
        }
        public static void GetAssemblyActions()
        {
            try
            {
                var projectName = Assembly.GetExecutingAssembly().FullName.Split(',')[0];

                Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));

                var model = asm.GetTypes().
                    SelectMany(t => t.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(d => d.ReturnType.Name == "ActionResult").Select(n => new AssemblyMenuModel()
                    {
                        Controller = n.DeclaringType?.Name.Replace("Controller", ""),
                        Action = n.Name,
                        ReturnType = n.ReturnType.Name,
                        Attributes = string.Join(",", n.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))),
                        Area = n.DeclaringType.Namespace.ToString().Replace(projectName + ".", "").Replace("Areas.", "").Replace(".Controllers", "").Replace("Controllers", "")
                    });

                SaveMenu(model.ToList());
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }
        [NonAction]
        private static void SaveMenu(List<AssemblyMenuModel> menuItems)
        {
            using (AppDbContext _db = new AppDbContext())
            {
                foreach (AssemblyMenuModel item in menuItems)
                {


                    int exists = _db.CM_PERMISSIONS.Where(a => a.CONTROLLER_NAME == item.Controller && a.ACTION_NAME == item.Action).Count();

                    if (exists < 1)
                    {
                        CM_PERMISSIONS newPermission = new CM_PERMISSIONS();
                        newPermission.ACTION_NAME = item.Action;
                        newPermission.CONTROLLER_NAME = item.Controller;
                        newPermission.TOGGLE_ICON = "fa fa-angle-left";
                        newPermission.ISACTIVE = false;
                        newPermission.FORM_URL = "/" + item.Controller + "/" + item.Action;
                        newPermission.PERMISSIONDESCRIPTION = item.Controller + " " + item.Action;
                        newPermission.PARENT_PERMISSION = 3;
                        //newPermission.PERMISSION_ID = 1;

                        _db.CM_PERMISSIONS.Add(newPermission);
                    }

                }
                _db.SaveChanges();
            }
        }
    }
}
