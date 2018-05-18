using CMdm.Data;
using CMdm.Entities.Domain.Report;
using CMdm.Services.Report;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMdm.UI.Web.Controllers
{
    public class ReportController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        // GET: Report
        public ActionResult ReportViewer()
        {
            //decimal BranchID = Convert.ToDecimal(Request.QueryString["BranchID"]);
            //switch (id)
            //{case 101:

            //    default:
            //        break;
            //}
            ViewBag.ShowIFrame = false;
            ReportRequestModel model = new ReportRequestModel();
            model.FROM_DATE = DateTime.Now;
            model.TO_DATE = DateTime.Now.AddDays(5);

            model.ReportList = new SelectList(db.RptDefinition, "REPORT_ID", "REPORT_NAME").ToList();
            model.Branches = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
            //ViewBag.REPORT_ID = new SelectList(db.RptDefinition, "REPORT_ID", "REPORT_NAME");            
            //ViewBag.BRANCH_ID = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME");

            #region ReportFormElementsDisplay
            switch (model.REPORT_ID)
            {
                case 101:
                    { }
                    break;
                case 102:
                    { }
                    break;
                case 103:
                    { }
                    break;
                case 104:
                    { }
                    break;
                case 105:
                    { }
                    break;
                case 106:
                    { }
                    break;
                case 107:
                    { }
                    break;
                case 108:
                    {

                    }
                    break;
                default:
                    break;
            }
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult ReportViewer(ReportRequestModel reportDefn, FormCollection formitems)
        {
            #region ReportFieldValidations
            switch (reportDefn.REPORT_ID)
            {
                case 101:
                    { }
                    break;
                case 102:
                    { }
                    break;
                case 103:
                    { }
                    break;
                case 104:
                    { }
                    break;
                case 105:
                    { }
                    break;
                case 106:
                    { }
                    break;
                case 107:
                    { }
                    break;
                case 108:
                    {
                        if (reportDefn.BRANCH_ID == string.Empty || reportDefn.BRANCH_ID == null)
                        {
                            string errorMessage = string.Format("Please select a branch ");
                            ModelState.AddModelError("BRANCH_ID", errorMessage);
                        }
                        //if (reportDefn.ACCT_NO == string.Empty || reportDefn.ACCT_NO == null)
                        //{
                        //    string errorMessage = string.Format("Please enter an acct number");
                        //    ModelState.AddModelError("ACCT_NO", errorMessage);
                        //}
                    }
                    break;
                default:
                    break;
            }
            #endregion
            if (ModelState.IsValid)
            {
                ViewBag.ShowIFrame = true;

                CmReportDefn report = db.RptDefinition.Find(reportDefn.REPORT_ID);
                reportDefn.REPORT_DESCRIPTION = report.REPORT_DESCRIPTION;
                reportDefn.REPORT_NAME = report.REPORT_NAME;
                reportDefn.REPORT_URL = report.REPORT_URL;
                reportDefn.DATASETNAME = report.DATASETNAME;
                Session["ReportModel"] = reportDefn;
            }
            else { ViewBag.ShowIFrame = false; }

            reportDefn.ReportList = new SelectList(db.RptDefinition, "REPORT_ID", "REPORT_NAME").ToList();
            reportDefn.Branches = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
            //ViewBag.REPORT_ID = new SelectList(db.RptDefinition, "REPORT_ID", "REPORT_NAME");
            //ViewBag.BRANCH_ID = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME");

            return View(reportDefn);
        }
        public ActionResult GetBranches()
        {
            var identity = ((CustomPrincipal)User).CustomIdentity;
            
            //string brnId = Convert.ToDecimal(identity.BranchId);
            var cashdb = new AppDbContext();
                var branches = (from o in cashdb.CM_BRANCH
                                where o.BRANCH_ID == identity.BranchId
                                select new
                                {
                                    BranchId = o.BRANCH_ID,
                                    BranchName = o.BRANCH_NAME,
                                    BranchSchedulerColor = "#F9722E"
                                });
                
                return Json(branches, JsonRequestBehavior.AllowGet);
            
        }
    }
}