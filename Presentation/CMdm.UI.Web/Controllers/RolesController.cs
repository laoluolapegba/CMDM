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
using CMdm.Framework.Controllers;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Entities.Domain.User;

namespace CMdm.UI.Web.Controllers
{
    public class RolesController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: Roles
        public ActionResult Index()
        {
            ViewBag.PARENT_ID = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            ViewBag.CHECKERS = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            
            return View(db.CM_USER_ROLES.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.CHECKERS = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CM_USER_ROLES cM_USER_ROLES = db.CM_USER_ROLES.Find(id);
            if (cM_USER_ROLES == null)
            {
                return HttpNotFound();
            }
            return View(cM_USER_ROLES);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            ViewBag.CHECKERS = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            ViewBag.USER_LEVELS = new SelectList(db.CM_AUTH_LEVELS, "LEVEL_ID", "LEVEL_NAME");
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ROLE_ID")]CM_USER_ROLES cM_USER_ROLES, bool continueEditing)
        {
            ViewBag.CHECKERS = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;

            if (ModelState.IsValid)
            {
                cM_USER_ROLES.CREATED_BY = identity.Name;
                cM_USER_ROLES.CREATED_DATE = DateTime.Now;
                db.CM_USER_ROLES.Add(cM_USER_ROLES);
                db.SaveChanges();
                db.Entry(cM_USER_ROLES).GetDatabaseValues();
                SuccessNotification("Role Created");
                return continueEditing ? RedirectToAction("Edit", new { id = cM_USER_ROLES.ROLE_ID }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            ViewBag.PARENT_ID = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            ViewBag.USER_LEVELS = new SelectList(db.CM_AUTH_LEVELS, "LEVEL_ID", "LEVEL_NAME");
            return View(cM_USER_ROLES);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.CHECKERS = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            ViewBag.USER_LEVELS = new SelectList(db.CM_AUTH_LEVELS, "LEVEL_ID", "LEVEL_NAME");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CM_USER_ROLES cM_USER_ROLES = db.CM_USER_ROLES.Find(id);
            if (cM_USER_ROLES == null)
            {
                return HttpNotFound();
            }
            return View(cM_USER_ROLES);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CM_USER_ROLES cM_USER_ROLES, bool continueEditing)
        {
            ViewBag.CHECKERS = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                cM_USER_ROLES.LAST_MODIFIED_DATE = DateTime.Now;
                cM_USER_ROLES.LAST_MODIFIED_BY = identity.Name;
                db.Entry(cM_USER_ROLES).State = EntityState.Modified;

                db.SaveChanges();
                SuccessNotification("Role Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = cM_USER_ROLES.ROLE_ID }) : RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            ViewBag.PARENT_ID = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            //var userLevels = Enum.GetValues(typeof(CMdm.Entities.Domain.User.AuthorizationLevel))
            //                .Cast<CMdm.Entities.Domain.User.AuthorizationLevel>()
            //                .Select(v => v.ToString())
            //                .ToList();
            //List<AuthorizationLevelModel> userLevels = ((CMdm.Entities.Domain.User.AuthorizationLevel[])Enum.GetValues(typeof(CMdm.Entities.Domain.User.AuthorizationLevel))).Select(c => new AuthorizationLevelModel() { Value = (int)c, Name = c.ToString() }).ToList();
            ViewBag.USER_LEVELS = new SelectList(db.CM_AUTH_LEVELS, "LEVEL_ID", "LEVEL_NAME");
            return View(cM_USER_ROLES);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.CHECKERS = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CM_USER_ROLES cM_USER_ROLES = db.CM_USER_ROLES.Find(id);
            if (cM_USER_ROLES == null)
            {
                return HttpNotFound();
            }
            return View(cM_USER_ROLES);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.CHECKERS = new SelectList(db.CM_USER_ROLES, "ROLE_ID", "ROLE_NAME");
            CM_USER_ROLES cM_USER_ROLES = db.CM_USER_ROLES.Find(id);
            db.CM_USER_ROLES.Remove(cM_USER_ROLES);
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
    }
}
