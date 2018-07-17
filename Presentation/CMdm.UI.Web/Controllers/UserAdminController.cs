using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System.Data.Entity;
using System.Net;
using System.Reflection;
using System.Web.Routing;
using CMdm.Data.Rbac;
using CMdm.UI.Web.Models.UserAdmin;
using CMdm.Framework.Kendoui;
using CMdm.Services.UserAdmin;
using CMdm.Framework.Mvc;
using CMdm.Core;
using CMdm.Framework.Controllers;

namespace CMdm.UI.Web.Controllers
{
    //public class UserAdminController : BaseController
    //{
    //    // GET: UserAdmin
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }
    //}

        public class UserAdminController : BaseController
        {
            
            static PasswordManager pwdManager = new PasswordManager();
        #region Fields

        private IUserService _userService;
        private AppDbContext database = new AppDbContext();
        #endregion
        #region Constructors
        public UserAdminController()
        { 
            _userService = new UserService();
        }
        #endregion
        #region USERS
        public ActionResult Index()
            {
                return RedirectToAction("ListUsers");
            }
        
        public ActionResult ListUsers()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            
            var defaultRoleIds = new List<int> { 1 };  // Laolu HardCode
            var model = new UsersListViewModel
            {
                SearchRoleIds = defaultRoleIds,
            };
            AppDbContext db = new AppDbContext();
            var allRoles = db.CM_USER_ROLES.ToList(); // _userService.GetAllRoles("", 1, int.MaxValue,"");
            foreach (var role in allRoles)
            {
                model.AvailableRoles.Add(new SelectListItem
                {
                    Text = role.ROLE_NAME,
                    Value = role.ROLE_ID.ToString(),
                    Selected = defaultRoleIds.Any(x => x == role.ROLE_ID)
                });
            }
            //model.UserRoles = new SelectList(database.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME").ToList();
            //model.UserRoles.Add(new SelectListItem
            //{
            //    Value = "0",
            //    Text = "All"
            //});
            return View(model);
        }
        [HttpPost]
        public ActionResult ListUsers(DataSourceRequest command, UsersListViewModel model, [ModelBinder(typeof(CommaSeparatedModelBinder))] int[] searchRoleIds)
        {

            var items = _userService.GetAllUsers(
                userRoleIds : searchRoleIds,
                email: model.SearchEmail,
                username: model.SearchUsername,
                firstName: model.SearchFirstName,
                lastName: model.SearchLastName,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize,
                sortExpression : "");

            var gridModel = new DataSourceResult
            {
                Data = items.Select(x => new UsersListViewModel
                {
                    USER_ID = x.USER_ID,
                    PROFILE_ID = x.PROFILE_ID,
                    EMAIL_ADDRESS = x.EMAIL_ADDRESS,
                    FIRSTNAME = x.FIRSTNAME,
                    LASTNAME = x.LASTNAME,
                    BRANCH_ID = x.BRANCH_ID,
                    CREATED_DATE = x.CREATED_DATE,
                    LASTLOGINDATE = x.LASTLOGINDATE,
                    ISLOCKED = x.ISLOCKED,
                    ROLE_ID = x.ROLE_ID,
                    ROLE_NAME = x.CM_USER_ROLES.ROLE_NAME,
                    BRANCH_NAME = x.CM_BRANCH.BRANCH_NAME,
                    FULLNAME = x.FIRSTNAME + " " + x.LASTNAME
                    

                }),
                Total = items.TotalCount
            };
            return Json(gridModel);
        }

        public virtual ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var model = new UsersListViewModel();
            AppDbContext db = new AppDbContext();

            model.UserRoles = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME").ToList();

            var curBranchList = db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME);
            model.Branches = new SelectList(curBranchList, "BRANCH_ID", "BRANCH_NAME").ToList();

            var currZoneList = db.CM_BRANCH.Select(x => new { x.ZONECODE, x.ZONENAME }).OrderBy(x => x.ZONENAME).Distinct();
            model.Zones = new SelectList(currZoneList, "ZONECODE", "ZONENAME").ToList();

            var currRegionList = db.CM_BRANCH.Select(x => new {x.REGION_ID, x.REGION_NAME }).OrderBy(x => x.REGION_NAME).Distinct();
            model.Regions = new SelectList(currRegionList, "REGION_ID", "REGION_NAME").ToList();
            //var allRoles = db.CM_USER_ROLES.ToList(); // _userService.GetAllRoles("", 1, int.MaxValue,"");
            //foreach (var role in allRoles)
            //{
            //    model.AvailableRoles.Add(new SelectListItem
            //    {
            //        Text = role.ROLE_NAME,
            //        Value = role.ROLE_ID.ToString(),
            //    });
            //}

            //default value
            model.ISLOCKED = false;
            model.COD_PASSWORD = "password";
            return View(model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsersListViewModel x, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            //decimal isActive = Convert.ToDecimal(x.ISACTIVE);
            bool isActive = x.ISACTIVE;
            if (ModelState.IsValid)
            {
                string salt = string.Empty;
                string passwordHash = pwdManager.GeneratePasswordHash(x.COD_PASSWORD, out salt);

                    CM_USER_PROFILE mdmUser = new CM_USER_PROFILE
                {
                    USER_ID = x.USER_ID,
                    EMAIL_ADDRESS = x.EMAIL_ADDRESS,
                    FIRSTNAME = x.FIRSTNAME,
                    LASTNAME = x.LASTNAME,
                    BRANCH_ID = x.BRANCH_ID,
                    COD_PASSWORD = passwordHash,
                    ISAPPROVED = true,
                    PASSWORDSALT = salt,
                    LASTLOGINDATE = DateTime.Now,
                    ISLOCKED = isActive,
                    ROLE_ID = x.ROLE_ID,
                    DISPLAY_NAME = x.FIRSTNAME + " " + x.LASTNAME,
                    
                    CREATED_DATE = DateTime.Now

                };

                x.UserTypes = Request.Form["UserTypes"];

                if (x.UserTypes.ToString().Equals("Branch User"))
                    mdmUser.BRANCH_ID = x.BRANCH_ID;
                if (x.UserTypes.Equals("Zonal User"))
                {
                    var branch = database.CM_BRANCH.Where(v => v.ZONECODE == x.ZONE_ID).Select(m => m.BRANCH_ID).FirstOrDefault();
                    mdmUser.BRANCH_ID = branch;
                }
                if (x.UserTypes.Equals("Regional User"))
                {
                    var branch = database.CM_BRANCH.Where(m => m.REGION_ID == x.REGION_ID).Select(m => m.BRANCH_ID).FirstOrDefault();
                    mdmUser.BRANCH_ID = branch;
                }
                    

                database.CM_USER_PROFILE.Add(mdmUser);
                database.SaveChanges();
                database.Entry(mdmUser).GetDatabaseValues();

                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New User has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id  = mdmUser.PROFILE_ID }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            x.UserRoles = new SelectList(database.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME").ToList();
            x.Branches = new SelectList(database.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
            return View(x);
        }

        public ActionResult Edit(int id)
        {
            var item = _userService.GetItembyId(id);
            if (item == null)
                //No store found with the specified id
                return RedirectToAction("Index");
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

           
            var mdmUser = (from x in database.CM_USER_PROFILE
                              where x.PROFILE_ID == id
                              select new UsersListViewModel
                              {
                                  USER_ID = x.USER_ID,
                                  PROFILE_ID = x.PROFILE_ID,
                                  EMAIL_ADDRESS = x.EMAIL_ADDRESS,
                                  FIRSTNAME = x.FIRSTNAME,
                                  LASTNAME = x.LASTNAME,
                                  BRANCH_ID = x.BRANCH_ID,
                                  CREATED_DATE = x.CREATED_DATE,
                                  LASTLOGINDATE = x.LASTLOGINDATE,
                                  ISLOCKED = x.ISLOCKED,
                                  ROLE_ID = x.ROLE_ID,
                                  FULLNAME = x.FIRSTNAME + " " + x.LASTNAME
                              }).FirstOrDefault();
            if (mdmUser == null)
            {
                return HttpNotFound();
            }
            var model = new UsersListViewModel
            {
                USER_ID = mdmUser.USER_ID,
                PROFILE_ID = mdmUser.PROFILE_ID,
                EMAIL_ADDRESS = mdmUser.EMAIL_ADDRESS,
                FIRSTNAME = mdmUser.FIRSTNAME,
                LASTNAME = mdmUser.LASTNAME,
                BRANCH_ID = mdmUser.BRANCH_ID,
                CREATED_DATE = mdmUser.CREATED_DATE,
                LASTLOGINDATE = mdmUser.LASTLOGINDATE,
                ISLOCKED = mdmUser.ISLOCKED,
                ROLE_ID = mdmUser.ROLE_ID,
                FULLNAME = mdmUser.FIRSTNAME + " " + mdmUser.LASTNAME
            };
            model.UserRoles = new SelectList(database.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME").ToList();
            model.Branches = new SelectList(database.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();

            var currZoneList = database.CM_BRANCH.Select(x => new { x.ZONECODE, x.ZONENAME }).OrderBy(x => x.ZONENAME).Distinct();
            model.Zones = new SelectList(currZoneList, "ZONECODE", "ZONENAME").ToList();

            var currRegionList = database.CM_BRANCH.Select(x => new { x.REGION_ID, x.REGION_NAME }).OrderBy(x => x.REGION_NAME).Distinct();
            model.Regions = new SelectList(currRegionList, "REGION_ID", "REGION_NAME").ToList();
            return View(model);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsersListViewModel mdmUser, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                mdmUser.UserTypes = Request.Form["UserTypes"];

                using (var db = new AppDbContext())
                {
                    var entity = db.CM_USER_PROFILE.FirstOrDefault(o => o.PROFILE_ID == mdmUser.PROFILE_ID);
                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", mdmUser.PROFILE_ID);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.BRANCH_ID = mdmUser.BRANCH_ID;
                        entity.ROLE_ID = mdmUser.ROLE_ID;
                        entity.USER_ID = mdmUser.USER_ID;
                        entity.FIRSTNAME = mdmUser.FIRSTNAME;
                        entity.LASTNAME = mdmUser.LASTNAME;
                        //Still defaulting
                        entity.ISLOCKED = !mdmUser.ISACTIVE; // Convert.ToDecimal(!mdmUser.ISACTIVE);
                        //entity.USER_ID = mdmUser.USER_ID;

                        if (mdmUser.UserTypes.ToString().Equals("Branch User"))
                            entity.BRANCH_ID = mdmUser.BRANCH_ID;
                        if (mdmUser.UserTypes.Equals("Zonal User"))
                        {
                            var branch = database.CM_BRANCH.Where(v => v.ZONECODE == mdmUser.ZONE_ID).Select(m => m.BRANCH_ID).FirstOrDefault();
                            entity.BRANCH_ID = branch;
                        }
                        if (mdmUser.UserTypes.Equals("Regional User"))
                        {
                            var branch = database.CM_BRANCH.Where(m => m.REGION_ID == mdmUser.REGION_ID).Select(m => m.BRANCH_ID).FirstOrDefault();
                            entity.BRANCH_ID = branch;
                        }
                        db.CM_USER_PROFILE.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }

                SuccessNotification("User Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = mdmUser.PROFILE_ID }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            mdmUser.UserRoles = new SelectList(database.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME").ToList();
            mdmUser.Branches = new SelectList(database.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
            return View(mdmUser);
        }


        public ViewResult UserDetails(int id)
            {
                CM_USER_PROFILE user = database.CM_USER_PROFILE.Find(id);
                SetViewBagData(id);
                return View(user);
            }

            public ActionResult UserCreate()
            {
                return View();
            }

            [HttpPost]
            public ActionResult UserCreate(CM_USER_PROFILE user)
            {
                if (user.USER_ID == "" || user.USER_ID == null)
                {
                    ModelState.AddModelError(string.Empty, "USER_ID cannot be blank");
                }

                try
                {
                    if (ModelState.IsValid)
                    {
                        List<string> results = database.Database.SqlQuery<String>(string.Format("SELECT user_id FROM cm_user_profile WHERE user_id = '{0}'", user.USER_ID)).ToList();
                        bool _userExistsInTable = (results.Count > 0);

                        CM_USER_PROFILE _user = null;
                        if (_userExistsInTable)
                        {
                            _user = database.CM_USER_PROFILE.Where(p => p.USER_ID == user.USER_ID).FirstOrDefault();
                            if (_user != null)
                            {
                                if (_user.ISLOCKED == false)
                                {
                                    ModelState.AddModelError(string.Empty, "USER already exists!");
                                }
                                else
                                {
                                    database.Entry(_user).Entity.ISLOCKED = false;
                                    //database.Entry(_user).Entity. = System.DateTime.Now;
                                    database.Entry(_user).State = EntityState.Modified;
                                    database.SaveChanges();
                                    return RedirectToAction("Index");
                                }
                            }
                        }
                        else
                        {
                            _user = new CM_USER_PROFILE();
                            _user.USER_ID = user.USER_ID;
                            _user.LASTNAME = user.LASTNAME;
                            _user.FIRSTNAME = user.FIRSTNAME;

                            //_user.Initial = user.Initial;
                            _user.EMAIL_ADDRESS = user.EMAIL_ADDRESS;

                            if (ModelState.IsValid)
                            {
                                _user.ISLOCKED = false;
                                //_user.LastModified = System.DateTime.Now;

                                database.CM_USER_PROFILE.Add(_user);
                                database.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    //return base.ShowError(ex);
                }

                return View(user);
            }

            [HttpGet]
            public ActionResult UserEdit(int id)
            {
                CM_USER_PROFILE user = database.CM_USER_PROFILE.Find(id);
                SetViewBagData(id);
                return View(user);
            }

            [HttpPost]
            public ActionResult UserEdit(CM_USER_PROFILE user)
            {
                CM_USER_PROFILE _user = database.CM_USER_PROFILE.Where(p => p.USER_ID == user.USER_ID).FirstOrDefault();
                if (_user != null)
                {
                    try
                    {
                        database.Entry(_user).CurrentValues.SetValues(user);
                        //database.Entry(_user).Entity.l = System.DateTime.Now;
                        database.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
                }
                return RedirectToAction("UserDetails", new RouteValueDictionary(new { id = user.USER_ID }));
            }

            [HttpPost]
            public ActionResult UserDetails(CM_USER_PROFILE user)
            {
                if (user.USER_ID == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid USER Name");
                }

                if (ModelState.IsValid)
                {
                    database.Entry(user).Entity.ISLOCKED = user.ISLOCKED;
                    //database.Entry(user).Entity.LastModified = System.DateTime.Now;
                    database.Entry(user).State = EntityState.Modified;
                    database.SaveChanges();
                }
                return View(user);
            }

            [HttpGet]
            public ActionResult DeleteUserRole(int id, int userId)
            {
                CM_USER_ROLES role = database.CM_USER_ROLES.Find(id);
                CM_USER_PROFILE user = database.CM_USER_PROFILE.Find(userId);

                if (role.CM_USER_PROFILE.Contains(user))
                {
                    role.CM_USER_PROFILE.Remove(user);
                    database.SaveChanges();
                }
                return RedirectToAction("Details", "USER", new { id = userId });
            }

            [HttpGet]
            public PartialViewResult filter4Users(string _surname)
            {
                return PartialView("_ListUserTable", GetFilteredUserList(_surname));
            }

            [HttpGet]
            public PartialViewResult filterReset()
            {
                return PartialView("_ListUserTable", database.CM_USER_PROFILE.Where(r => r.ISLOCKED == false).ToList());  //|| r.ISLOCKED == null
        }

            [HttpGet]
            public PartialViewResult DeleteUserReturnPartialView(int userId)
            {
                try
                {
                    CM_USER_PROFILE user = database.CM_USER_PROFILE.Find(userId);
                    if (user != null)
                    {
                        database.Entry(user).Entity.ISLOCKED = true;
                        database.Entry(user).Entity.USER_ID = user.USER_ID;
                        //database.Entry(user).Entity.LastModified = System.DateTime.Now;
                        database.Entry(user).State = EntityState.Modified;
                        database.SaveChanges();
                    }
                }
                catch
                {
                }
                return this.filterReset();
            }

            private IEnumerable<CM_USER_PROFILE> GetFilteredUserList(string _surname)
            {
                IEnumerable<CM_USER_PROFILE> _ret = null;
                try
                {
                    if (string.IsNullOrEmpty(_surname))
                    {
                        _ret = database.CM_USER_PROFILE.Where(r => r.ISLOCKED == false).ToList();
                    }
                    else
                    {
                        _ret = database.CM_USER_PROFILE.Where(p => p.LASTNAME == _surname).ToList();
                    }
                }
                catch
                {
                }
                return _ret;
            }

            protected override void Dispose(bool disposing)
            {
                database.Dispose();
                base.Dispose(disposing);
            }

            [HttpGet]
            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
            public PartialViewResult DeleteUserRoleReturnPartialView(int id, int userId)
            {
                CM_USER_ROLES role = database.CM_USER_ROLES.Find(id);
                CM_USER_PROFILE user = database.CM_USER_PROFILE.Find(userId);

                if (role.CM_USER_PROFILE.Contains(user))
                {
                    role.CM_USER_PROFILE.Remove(user);
                    database.SaveChanges();
                }
                SetViewBagData(userId);
                return PartialView("_ListUserRoleTable", database.CM_USER_PROFILE.Find(userId));
            }

            [HttpGet]
            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
            public PartialViewResult AddUserRoleReturnPartialView(int id, int userId)
            {
                CM_USER_ROLES role = database.CM_USER_ROLES.Find(id);
                CM_USER_PROFILE user = database.CM_USER_PROFILE.Find(userId);

                if (!role.CM_USER_PROFILE.Contains(user))
                {
                    role.CM_USER_PROFILE.Add(user);
                    database.SaveChanges();
                }
                SetViewBagData(userId);
                return PartialView("_ListUserRoleTable", database.CM_USER_PROFILE.Find(userId));
            }

            private void SetViewBagData(int _userId)
            {
                ViewBag.UserId = _userId;
                ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();
                ViewBag.RoleId = new SelectList(database.CM_USER_ROLES.OrderBy(p => p.ROLE_NAME), "ROLE_ID", "ROLE_NAME");
            }

            public List<SelectListItem> List_boolNullYesNo()
            {
                var _retVal = new List<SelectListItem>();
                try
                {
                    _retVal.Add(new SelectListItem { Text = "Not Set", Value = null });
                    _retVal.Add(new SelectListItem { Text = "Yes", Value = bool.TrueString });
                    _retVal.Add(new SelectListItem { Text = "No", Value = bool.FalseString });
                }
                catch { }
                return _retVal;
            }
            #endregion

            #region CM_USER_ROLES
            public ActionResult RoleIndex(string sortOrder, string currentFilter, string searchString, int? page)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
                var roles = from s in database.CM_USER_ROLES.Include(c => c.CM_USER_PROFILE)
                            select s;
                //return View(database.CM_USER_ROLES.OrderBy(r => r.ROLE_NAME).ToList());
                if (!String.IsNullOrEmpty(searchString))
                {
                    roles = roles.Where(s => s.ROLE_NAME.Contains(searchString)); //|| s.BRANCH_ID.Contains(searchString)
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        roles = roles.OrderByDescending(s => s.ROLE_NAME);
                        break;
                    case "Date":
                        roles = roles.OrderBy(s => s.CREATED_DATE);
                        break;
                    case "date_desc":
                        roles = roles.OrderByDescending(s => s.CREATED_DATE);
                        break;
                    default:
                        roles = roles.OrderBy(s => s.ROLE_NAME);
                        break;
                }
                int pageSize = 10;  // Attention : Laolu
                int pageNumber = (page ?? 1);
                return View(new PagedList<CM_USER_ROLES>( roles, pageNumber, pageSize));
            }

            public ViewResult RoleDetails(int id, string sortOrder, string currentFilter, string searchString, int? page)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
                //var perms = from s in db.CM_USER_ROLES.Include(c => c.CM_USER_PROFILE)
                //            select s;
                var perms = from o in database.CM_PERMISSIONS
                            join od in database.CM_ROLE_PERM_XREF on o.PERMISSION_ID equals od.PERMISSION_ID
                            where od.ROLE_ID == id
                            select new Permissions()
                            {
                                PERMISSION_ID = o.PERMISSION_ID,
                                PERMISSIONDESCRIPTION = o.PERMISSIONDESCRIPTION

                            };

                if (!String.IsNullOrEmpty(searchString))
                {
                    perms = perms.Where(s => s.PERMISSIONDESCRIPTION.Contains(searchString)); //|| s.BRANCH_ID.Contains(searchString)
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        perms = perms.OrderByDescending(s => s.PERMISSIONDESCRIPTION);
                        break;
                    case "Date":
                        perms = perms.OrderBy(s => s.PERMISSION_ID);
                        break;
                    case "date_desc":
                        perms = perms.OrderByDescending(s => s.PERMISSION_ID);
                        break;
                    default:
                        perms = perms.OrderBy(s => s.PERMISSIONDESCRIPTION);
                        break;
                }
                int pageSize = 10;  // Attention : Laolu
                int pageNumber = (page ?? 1);

                ViewBag.PermissionsinRole = new PagedList<CM_PERMISSIONS>(perms, pageNumber, pageSize);


                CM_USER_PROFILE user = database.CM_USER_PROFILE.Where(r => r.USER_ID == User.Identity.Name).FirstOrDefault();

                var identity = ((CustomPrincipal)User).CustomIdentity;

                CM_USER_ROLES role = database.CM_USER_ROLES.Find(id);
                ViewBag.UsersinRole = database.CM_USER_PROFILE.Where(r => r.ROLE_ID == identity.UserRoleId);



                //CM_USER_PROFILE user = database.CM_USER_PROFILE.Where(r => r.USER_ID == User.Identity.Name).FirstOrDefault();
                //CM_USER_ROLES role = database.CM_USER_ROLES.Where(r => r.ROLE_ID == id)
                //       .Include(a => a.CM_PERMISSIONS)
                //       .Include(a => a.CM_USER_PROFILE)
                //       .FirstOrDefault();

                // USERS combo
                ViewBag.UserId = new SelectList(database.CM_USER_PROFILE.Where(r => r.ISLOCKED == false), "PROFILE_ID", "USER_ID"); //|| r.ISLOCKED == null
            ViewBag.RoleId = id;

                // Rights combo
                ViewBag.PermissionId = new SelectList(database.CM_PERMISSIONS.OrderBy(a => a.PERMISSIONDESCRIPTION), "PERMISSIOND_ID", "PERMISSIONDESCRIPTION");
                ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();

                return View(role);
            }

            public ActionResult RoleCreate()
            {
                CM_USER_PROFILE user = database.CM_USER_PROFILE.Where(r => r.USER_ID == User.Identity.Name).FirstOrDefault();
                ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();
                return View();
            }

            [HttpPost]
            public ActionResult RoleCreate(CM_USER_ROLES _role)
            {
                if (_role.ROLE_NAME == null)
                {
                    ModelState.AddModelError("Role Description", "Role Description must be entered");
                }

                CM_USER_PROFILE user = database.CM_USER_PROFILE.Where(r => r.USER_ID == User.Identity.Name).FirstOrDefault();
                if (ModelState.IsValid)
                {


                    database.CM_USER_ROLES.Add(_role);
                    database.SaveChanges();
                    return RedirectToAction("RoleIndex");
                }
                ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();
                return View(_role);
            }


            public ActionResult RoleEdit(int id, string sortOrder, string currentFilter, string searchString, int? page)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                CM_USER_PROFILE user = database.CM_USER_PROFILE.Where(r => r.USER_ID == User.Identity.Name).FirstOrDefault();

                var identity = ((CustomPrincipal)User).CustomIdentity;

                CM_USER_ROLES _role = database.CM_USER_ROLES.Find(id);
                ViewBag.UsersinRole = database.CM_USER_PROFILE.Where(r => r.ROLE_ID == identity.UserRoleId);

                //var data = database.CM_PERMISSIONS.Join(database.CM_ROLE_PERM_XREF, c => c.PERMISSION_ID, o => o.PERMISSION_ID, (o, c) => new { Perm = o, Xref = c })
                //    .Where(d => d.Xref.ROLE_ID == id);

                //ViewBag.PermissionsinRole = data.Select(o => new CM_PERMISSIONS
                //{
                //    PERMISSION_ID = o.Perm.PERMISSION_ID,
                //    PERMISSIONDESCRIPTION = o.Perm.PERMISSIONDESCRIPTION

                //}).OrderBy(o => o.PERMISSIONDESCRIPTION);

                var perms = from o in database.CM_PERMISSIONS
                            join od in database.CM_ROLE_PERM_XREF on o.PERMISSION_ID equals od.PERMISSION_ID
                            where od.ROLE_ID == id
                            select new Permissions()
                            {
                                PERMISSION_ID = o.PERMISSION_ID,
                                PERMISSIONDESCRIPTION = o.PERMISSIONDESCRIPTION

                            };

                if (!String.IsNullOrEmpty(searchString))
                {
                    perms = perms.Where(s => s.PERMISSIONDESCRIPTION.Contains(searchString)); //|| s.BRANCH_ID.Contains(searchString)
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        perms = perms.OrderByDescending(s => s.PERMISSIONDESCRIPTION);
                        break;
                    case "Date":
                        perms = perms.OrderBy(s => s.PERMISSION_ID);
                        break;
                    case "date_desc":
                        perms = perms.OrderByDescending(s => s.PERMISSION_ID);
                        break;
                    default:
                        perms = perms.OrderBy(s => s.PERMISSIONDESCRIPTION);
                        break;
                }
                int pageSize = 10;  // Attention : Laolu
                int pageNumber = (page ?? 1);

                ViewBag.PermissionsinRole = new PagedList<CM_PERMISSIONS>( perms, pageNumber, pageSize);




                //    .Where(r => r.ROLE_ID == id)
                //        .Include(a => a.CM_PERMISSIONS)
                //        .Include(a => a.CM_USER_PROFILE)
                //.FirstOrDefault();

                // USERS combo
                ViewBag.UserId = new SelectList(database.CM_USER_PROFILE.Where(r => r.ISLOCKED == false), "USER_ID", "USER_ID");// 0 || r.ISLOCKED == null
            ViewBag.RoleId = id;

                // Rights combo
                ViewBag.PermissionId = new SelectList(database.CM_PERMISSIONS.OrderBy(a => a.PERMISSION_ID), "PERMISSION_ID", "PERMISSIONDESCRIPTION");
                ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();

                return View(_role);
            }

            [HttpPost]
            public ActionResult RoleEdit(CM_USER_ROLES _role)
            {
                if (string.IsNullOrEmpty(_role.ROLE_NAME))
                {
                    ModelState.AddModelError("Role Description", "Role Description must be entered");
                }

                //EntityState state = database.Entry(_role).State;
                CM_USER_PROFILE user = database.CM_USER_PROFILE.Where(r => r.USER_ID == User.Identity.Name).FirstOrDefault();
                if (ModelState.IsValid)
                {

                    database.Entry(_role).State = EntityState.Modified;
                    database.SaveChanges();
                    return RedirectToAction("RoleDetails", new RouteValueDictionary(new { id = _role.ROLE_ID }));
                }
                // USERS combo
                ViewBag.UserId = new SelectList(database.CM_USER_PROFILE.Where(r => r.ISLOCKED == false), "USER_ID", "USER_ID");  //r => r.ISLOCKED == 0 || r.ISLOCKED == null

            // Rights combo
            ViewBag.PermissionId = new SelectList(database.CM_PERMISSIONS.OrderBy(a => a.PERMISSION_ID), "PERMISSION_ID", "PERMISSIONDESCRIPTION");
                ViewBag.List_boolNullYesNo = this.List_boolNullYesNo();
                return View(_role);
            }


            public ActionResult RoleDelete(int id)
            {
                CM_USER_ROLES _role = database.CM_USER_ROLES.Find(id);
                if (_role != null)
                {
                    _role.CM_USER_PROFILE.Clear();
                    _role.CM_PERMISSIONS.Clear();

                    database.Entry(_role).State = EntityState.Deleted;
                    database.SaveChanges();
                }
                return RedirectToAction("RoleIndex");
            }

            [HttpGet]
            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
            public PartialViewResult DeleteUserFromRoleReturnPartialView(int id, int userId)
            {
                CM_USER_ROLES role = database.CM_USER_ROLES.Find(id);
                CM_USER_PROFILE user = database.CM_USER_PROFILE.Find(userId);

                if (role.CM_USER_PROFILE.Contains(user))
                {
                    role.CM_USER_PROFILE.Remove(user);
                    database.SaveChanges();
                }
                return PartialView("_ListUsersTable4Role", role);
            }

            [HttpGet]
            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
            public PartialViewResult AddUser2RoleReturnPartialView(int id, int userId)
            {
                CM_USER_ROLES role = database.CM_USER_ROLES.Find(id);
                CM_USER_PROFILE user = database.CM_USER_PROFILE.Find(userId);

                if (!role.CM_USER_PROFILE.Contains(user))
                {
                    role.CM_USER_PROFILE.Add(user);
                    database.SaveChanges();
                }
                return PartialView("_ListUsersTable4Role", role);
            }

            #endregion

            #region PERMISSIONS

            public ViewResult PermissionIndex(string sortOrder, string currentFilter, string searchString, int? page)
            {

                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var perms = from s in database.CM_PERMISSIONS
                            select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    perms = perms.Where(s => s.PERMISSIONDESCRIPTION.Contains(searchString)); //|| s.BRANCH_ID.Contains(searchString)
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        perms = perms.OrderByDescending(s => s.PERMISSIONDESCRIPTION);
                        break;
                    case "Date":
                        perms = perms.OrderBy(s => s.PERMISSION_ID);
                        break;
                    case "date_desc":
                        perms = perms.OrderByDescending(s => s.PERMISSION_ID);
                        break;
                    default:
                        perms = perms.OrderBy(s => s.PERMISSIONDESCRIPTION);
                        break;
                }
                int pageSize = 10;  // Attention : Laolu
                int pageNumber = (page ?? 1);
                return View( new PagedList<CM_PERMISSIONS>(perms, pageNumber, pageSize));


            //List<CM_PERMISSIONS> _permissions = database.CM_PERMISSIONS
            //                   .OrderBy(wn => wn.PERMISSIONDESCRIPTION)                               
            //                   .ToList();
            //return View(_permissions);
            //.Include(a => a.CM_USER_ROLES)
        }

            public ViewResult PermissionDetails(int id)
            {
                CM_PERMISSIONS _permission = database.CM_PERMISSIONS.Find(id);
                ViewBag.RoleId = new SelectList(database.CM_USER_ROLES.OrderBy(p => p.ROLE_NAME), "ROLE_ID", "ROLE_NAME");
                var roles = from o in database.CM_USER_ROLES
                            join od in database.CM_ROLE_PERM_XREF on o.ROLE_ID equals od.ROLE_ID
                            where od.PERMISSION_ID == id
                            select new Roles()
                            {
                                ROLE_ID = o.ROLE_ID,
                                ROLE_NAME = o.ROLE_NAME

                            };
                ViewBag.RolesinPermission = roles;
                return View(_permission);
            }

            public ActionResult PermissionCreate()
            {
                ViewBag.PARENT_PERMISSION = new SelectList(database.CM_PERMISSIONS.OrderBy(a => a.PERMISSION_ID), "PERMISSION_ID", "PERMISSIONDESCRIPTION");
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult PermissionCreate([Bind(Include = "PERMISSIONDESCRIPTION,ACTION_NAME,CONTROLLER_NAME,PARENT_PERMISSION,FORM_URL,IMAGE_URL,ICON_CLASS,ISOPEN_CLASS,TOGGLE_ICON,ISACTIVE")] CM_PERMISSIONS cM_PERMISSIONS)
            {
                if (cM_PERMISSIONS.PERMISSIONDESCRIPTION == null)
                {
                    ModelState.AddModelError("Permission Description", "Permission Description must be entered");
                }
                if (ModelState.IsValid)
                {
                    database.CM_PERMISSIONS.Add(cM_PERMISSIONS);
                    database.SaveChanges();
                    return RedirectToAction("PermissionIndex");
                }

                return View(cM_PERMISSIONS);
            }

            public ActionResult PermissionEdit(int id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CM_PERMISSIONS cM_PERMISSIONS = database.CM_PERMISSIONS.Find(id);
                ViewBag.RoleId = new SelectList(database.CM_USER_ROLES.OrderBy(p => p.ROLE_NAME), "ROLE_ID", "ROLE_NAME");
                if (cM_PERMISSIONS == null)
                {
                    return HttpNotFound();
                }

                ViewBag.PARENT_PERMISSION = new SelectList(database.CM_PERMISSIONS.OrderBy(a => a.PERMISSION_ID), "PERMISSION_ID", "PERMISSIONDESCRIPTION");
                var roles = from o in database.CM_USER_ROLES
                            join od in database.CM_ROLE_PERM_XREF on o.ROLE_ID equals od.ROLE_ID
                            where od.PERMISSION_ID == id
                            select new Roles()
                            {
                                ROLE_ID = o.ROLE_ID,
                                ROLE_NAME = o.ROLE_NAME

                            };
                ViewBag.RolesinPermission = roles;
                return View(cM_PERMISSIONS);

                //CM_PERMISSIONS _permission = database.CM_PERMISSIONS.Find(id);
                //ViewBag.RoleId = new SelectList(database.CM_USER_ROLES.OrderBy(p => p.ROLE_NAME), "ROLE_ID", "ROLE_NAME");
                //return View(_permission);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult PermissionEdit([Bind(Include = "PERMISSION_ID,PERMISSIONDESCRIPTION,ACTION_NAME,CONTROLLER_NAME,PARENT_PERMISSION,FORM_URL,IMAGE_URL,ICON_CLASS,ISOPEN_CLASS,TOGGLE_ICON,ISACTIVE")] CM_PERMISSIONS cM_PERMISSIONS)
            {
                if (ModelState.IsValid)
                {
                    database.Entry(cM_PERMISSIONS).State = EntityState.Modified;
                    database.SaveChanges();
                    return RedirectToAction("PermissionDetails", new RouteValueDictionary(new { id = cM_PERMISSIONS.PERMISSION_ID }));
                }

                return View(cM_PERMISSIONS);
            }

            [HttpGet]
            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
            public ActionResult PermissionDelete(int id)
            {
                CM_PERMISSIONS permission = database.CM_PERMISSIONS.Find(id);
                if (permission.CM_USER_ROLES.Count > 0)
                    permission.CM_USER_ROLES.Clear();

                database.Entry(permission).State = EntityState.Deleted;
                database.SaveChanges();
                return RedirectToAction("PermissionIndex");
            }

            [HttpGet]
            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
            public PartialViewResult AddPermission2RoleReturnPartialView(int id, int permissionId)
            {
                CM_USER_ROLES role = database.CM_USER_ROLES.Find(id);
                CM_PERMISSIONS _permission = database.CM_PERMISSIONS.Find(permissionId);

                if (!role.CM_PERMISSIONS.Contains(_permission))
                {
                    role.CM_PERMISSIONS.Add(_permission);
                    database.SaveChanges();
                }
                return PartialView("_ListPermissions", role);
            }

            [HttpGet]
            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
            public PartialViewResult AddAllPermissions2RoleReturnPartialView(int id)
            {
                CM_USER_ROLES _role = database.CM_USER_ROLES.Where(p => p.ROLE_ID == id).FirstOrDefault();
                List<CM_PERMISSIONS> _permissions = database.CM_PERMISSIONS.ToList();
                foreach (CM_PERMISSIONS _permission in _permissions)
                {
                    if (!_role.CM_PERMISSIONS.Contains(_permission))
                    {
                        _role.CM_PERMISSIONS.Add(_permission);

                    }
                }
                database.SaveChanges();
                return PartialView("_ListPermissions", _role);
            }

            [HttpGet]
            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
            public PartialViewResult DeletePermissionFromRoleReturnPartialView(int id, int permissionId)
            {
                CM_USER_ROLES _role = database.CM_USER_ROLES.Find(id);
                CM_PERMISSIONS _permission = database.CM_PERMISSIONS.Find(permissionId);

                if (_role.CM_PERMISSIONS.Contains(_permission))
                {
                    _role.CM_PERMISSIONS.Remove(_permission);
                    database.SaveChanges();
                }
                return PartialView("_ListPermissions", _role);
            }

            [HttpGet]
            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
            public PartialViewResult DeleteRoleFromPermissionReturnPartialView(int id, int permissionId)
            {
                CM_USER_ROLES role = database.CM_USER_ROLES.Find(id);
                CM_PERMISSIONS permission = database.CM_PERMISSIONS.Find(permissionId);

                if (role.CM_PERMISSIONS.Contains(permission))
                {
                    role.CM_PERMISSIONS.Remove(permission);
                    database.SaveChanges();
                }
                return PartialView("_ListRolesTable4Permission", permission);
            }

            [HttpGet]
            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
            public PartialViewResult AddRole2PermissionReturnPartialView(int permissionId, int roleId)
            {
                CM_USER_ROLES role = database.CM_USER_ROLES.Find(roleId);
                CM_PERMISSIONS _permission = database.CM_PERMISSIONS.Find(permissionId);

                if (!role.CM_PERMISSIONS.Contains(_permission))
                {
                    role.CM_PERMISSIONS.Add(_permission);
                    database.SaveChanges();
                }
                return PartialView("_ListRolesTable4Permission", _permission);
            }

            public ActionResult PermissionsImport()
            {
                var _controllerTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => t != null
                        && t.IsPublic
                        && t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                        && !t.IsAbstract
                        && typeof(IController).IsAssignableFrom(t));

                var _controllerMethods = _controllerTypes.ToDictionary(controllerType => controllerType,
                        controllerType => controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)));

                foreach (var _controller in _controllerMethods)
                {
                    string _controllerName = _controller.Key.Name;
                    foreach (var _controllerAction in _controller.Value)
                    {
                        string _controllerActionName = _controllerAction.Name;
                        if (_controllerName.EndsWith("Controller"))
                        {
                            _controllerName = _controllerName.Substring(0, _controllerName.LastIndexOf("Controller"));
                        }

                        string _permissionDescription = string.Format("{0}-{1}", _controllerName, _controllerActionName);
                        CM_PERMISSIONS _permission = database.CM_PERMISSIONS.Where(p => p.PERMISSIONDESCRIPTION == _permissionDescription).FirstOrDefault();
                        if (_permission == null)
                        {
                            if (ModelState.IsValid)
                            {
                                CM_PERMISSIONS _perm = new CM_PERMISSIONS();
                                _perm.PERMISSIONDESCRIPTION = _permissionDescription;

                                database.CM_PERMISSIONS.Add(_perm);
                                database.SaveChanges();
                            }
                        }
                    }
                }
                return RedirectToAction("PermissionIndex");
            }
            #endregion

            #region UserController
            // GET: Users/Details/5
            public ActionResult DetailsUser(decimal id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CM_USER_PROFILE cM_USER_PROFILE = database.CM_USER_PROFILE.Find(id);
                if (cM_USER_PROFILE == null)
                {
                    return HttpNotFound();
                }
                return View(cM_USER_PROFILE);
            }

            // GET: Users/Create
            public ActionResult CreateUser()
            {
                ViewBag.ROLE_ID = new SelectList(database.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
                ViewBag.BRANCH_ID = new SelectList(database.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME");

            return View();
            }

            // POST: Users/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult CreateUser([Bind(Include = "USER_ID,COD_PASSWORD,ROLE_ID,DISPLAY_NAME,FIRSTNAME,LASTNAME,EMAIL_ADDRESS,BRANCH_ID")] CM_USER_PROFILE cM_USER_PROFILE)
            {
                if (ModelState.IsValid)
                {
                    string salt = null;

                    string passwordHash = pwdManager.GeneratePasswordHash(cM_USER_PROFILE.COD_PASSWORD, out salt);
                    cM_USER_PROFILE.COD_PASSWORD = passwordHash;
                    cM_USER_PROFILE.PASSWORDSALT = salt;

                    database.CM_USER_PROFILE.Add(cM_USER_PROFILE);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ROLE_ID = new SelectList(database.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME", cM_USER_PROFILE.ROLE_ID);
                ViewBag.BRANCH_ID = new SelectList(database.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME", cM_USER_PROFILE.BRANCH_ID);
                return View(cM_USER_PROFILE);
            }

            // GET: Users/Edit/5
            public ActionResult EditUser(decimal id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
               CM_USER_PROFILE cM_USER_PROFILE = database.CM_USER_PROFILE.Find(id);
                if (cM_USER_PROFILE == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ROLE_ID = new SelectList(database.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME", cM_USER_PROFILE.ROLE_ID);
                ViewBag.BRANCH_ID = new SelectList(database.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME", cM_USER_PROFILE.BRANCH_ID);
                return View(cM_USER_PROFILE);
            }

            // POST: Users/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult EditUser([Bind(Include = "USER_ID,COD_PASSWORD,ROLE_ID,DISPLAY_NAME,FIRSTNAME,LASTNAME,EMAIL_ADDRESS,BRANCH_ID")] CM_USER_PROFILE cM_USER_PROFILE)
            {
                if (ModelState.IsValid)
                {
                    string salt = null;

                    string passwordHash = pwdManager.GeneratePasswordHash(cM_USER_PROFILE.COD_PASSWORD, out salt);
                    cM_USER_PROFILE.COD_PASSWORD = passwordHash;
                    cM_USER_PROFILE.PASSWORDSALT = salt;
                database.Entry(cM_USER_PROFILE).State = EntityState.Modified;
                database.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ROLE_ID = new SelectList(database.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME", cM_USER_PROFILE.ROLE_ID);
                ViewBag.BRANCH_ID = new SelectList(database.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME", cM_USER_PROFILE.BRANCH_ID);
                return View(cM_USER_PROFILE);
            }

            // GET: Users/Delete/5
            public ActionResult DeleteUser(decimal id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CM_USER_PROFILE cM_USER_PROFILE = database.CM_USER_PROFILE.Find(id);
                if (cM_USER_PROFILE == null)
                {
                    return HttpNotFound();
                }
                return View(cM_USER_PROFILE);
            }

            // POST: Users/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(decimal id)
            {
                CM_USER_PROFILE cM_USER_PROFILE = database.CM_USER_PROFILE.Find(id);
            database.CM_USER_PROFILE.Remove(cM_USER_PROFILE);
            database.SaveChanges();
                return RedirectToAction("Index");
            }
            #endregion

            #region PermRemoved
            //public JsonResult PermissionTree(int? id)
            //{
            //    //int roleId = Request.QueryString["branch"];
            //    var perms = from o in database.CM_PERMISSIONS
            //                join od in database.CM_ROLE_PERM_XREF on o.PERMISSION_ID equals od.PERMISSION_ID
            //                where (od.ROLE_ID == 4 && (id.HasValue ? o.PARENT_PERMISSION == id : o.PARENT_PERMISSION == null))
            //                select new
            //                {
            //                    id = o.PERMISSION_ID,
            //                    Name = o.PERMISSIONDESCRIPTION,
            //                    hasChildren = o.CM_PERMISSIONS1.Any()

            //                };

            //    return Json(perms, JsonRequestBehavior.AllowGet);
            //}
            //public JsonResult PermissionTreeView([DataSourceRequest] DataSourceRequest request, int? id)
            //{
            //    return Json(GetPermissions(id).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            //}
            //private static IQueryable<DemoCash.Web.Models.PermissionViewModel> GetPermissions(int? roleid)
            //{
            //    var db = new RBAC_Model();
            //    var data = db.PermissionsModel.Join(db.CM_ROLE_PERM_XREF, c => c.PERMISSION_ID, o => o.PERMISSION_ID, (o, c) => new { Perm = o, Xref = c });
            //    //.Where(r => r.Xref.ROLE_ID == roleid);
            //    //e => id.HasValue ? e.ReportsTo == id : e.ReportsTo == null,
            //    var perms = data.Select(perm => new DemoCash.Web.Models.PermissionViewModel
            //    {
            //        PERMISSION_ID = perm.Perm.PERMISSION_ID,
            //        PERMISSIONDESCRIPTION = perm.Perm.PERMISSIONDESCRIPTION,
            //        PARENT_PERMISSION = perm.Perm.PARENT_PERMISSION,
            //        ACTION_NAME = perm.Perm.ACTION_NAME,
            //        CONTROLLER_NAME = perm.Perm.CONTROLLER_NAME,
            //        FORM_URL = perm.Perm.FORM_URL,
            //        IMAGE_URL = perm.Perm.IMAGE_URL,
            //        ICON_CLASS = perm.Perm.ICON_CLASS,
            //        ISOPEN_CLASS = perm.Perm.ISOPEN_CLASS,
            //        TOGGLE_ICON = perm.Perm.TOGGLE_ICON,
            //        ISACTIVE = perm.Perm.ISACTIVE,
            //        hasChildren = perm.Perm.PermissionsModel1.Any()

            //    });

            //    return perms;
            //}


            //private IEnumerable<CM_PERMISSIONS> GetPermissions()
            //{
            //    return GetAll();
            //}
            //public virtual IQueryable<CM_PERMISSIONS> GetAll()
            //{
            //    return database.CM_PERMISSIONS;
            //}

            #endregion
        }
        public class Permissions : CM_PERMISSIONS { }
        public class Roles : CM_USER_ROLES { }
    
}