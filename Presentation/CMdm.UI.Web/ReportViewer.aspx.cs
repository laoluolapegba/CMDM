using CMdm.Data;
using CMdm.Services.Report;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMdm.UI.Web
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        private AppDbContext db = new AppDbContext();
        string connString = ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
        private IReportService _reportService;

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkHome.Visible = false;
            if (!Page.IsPostBack)
            {
                if (Session["ReportModel"] != null)
                {
                    RenderReport(sender);
                }
                else
                {
                    Response.Write("It appears your session has expired.");
                    lnkHome.Visible = true;
                }

            }
        }
        private void RenderReport(object sender)
        {
            rptViewer.Reset();
            rptViewer.ProcessingMode = ProcessingMode.Local;

            rptViewer.LocalReport.ReportPath = "";
            ReportRequestModel model = (ReportRequestModel)Session["ReportModel"];
            rptViewer.LocalReport.ReportPath = Server.MapPath(model.REPORT_URL);
            rptViewer.LocalReport.EnableExternalImages = true;

            List<decimal> statusList = new List<decimal>();
            if (model.StatusCode != string.Empty && model.StatusCode != null)
            { statusList = model.StatusCode.Split(',').Select(decimal.Parse).ToList(); }

            rptViewer.LocalReport.SetParameters(new ReportParameter("ReportTitle", model.REPORT_DESCRIPTION));
            rptViewer.LocalReport.SetParameters(new ReportParameter("UserNamPrinting", User.Identity.Name));

            rptViewer.LocalReport.DataSources.Clear();
            DateTime _to_date = model.TO_DATE.AddDays(1);
            _reportService = new ReportServiceImp();
            var reportDataSet = _reportService.GetPendingExceptions(model);

            rptViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource(model.DATASETNAME, reportDataSet));
            
            rptViewer.DataBind();
        }
    }
}