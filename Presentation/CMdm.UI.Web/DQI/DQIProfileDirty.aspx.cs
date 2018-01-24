using CMdm.UI.Web.BLL;
using Elmah;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading.Tasks;
using Telerik.Web.UI;
using CMdm.Data;

namespace CMdm.UI.Web.DQI
{
    public partial class DQIProfileDirty : System.Web.UI.Page
    {

        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        public DQIParamSettingClass dqiC = new DQIParamSettingClass();
        //public MdmDqi.Data.MdmModel DQIdb = new MdmDqi.Data.MdmModel();
        //public Cdma.External.CDMA_Model db = new Cdma.External.CDMA_Model();
        AppDbContext db = new AppDbContext();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();

        private static int taskProgress = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            RadListBoxSource.SelectedIndexChanged += new EventHandler(RadListBoxSource_SelectedIndexChanged);
            RadListBoxSource.SelectedIndexChanged += new EventHandler(RadListBoxSource_SelectedIndexChanged);
            if (!IsPostBack)
            {
            }
        }

        #region Tab1
        protected void RadListBoxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RadListBoxDestination.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataCatalogBiz bizRule = new DataCatalogBiz();
            Session["EntityName"] =  RadListBoxSource.SelectedValue;
            //if (bizRule.IsCatalogExist(RadListBoxSource.SelectedValue))
            //{
            //    this.lblmsgs.Text = MessageFormatter.GetFormattedNoticeMessage("Catalog Already Exists");
            //    return;
            //}
            //if (RadListBoxDestination.CheckedItems.Count < 1)
            //{
            //    this.lblmsgs.Text = MessageFormatter.GetFormattedNoticeMessage("At least 1 Catalog Attribute must be selected.");
            //    return;
            //}
            using (OracleConnection connection = new OracleConnection(connString))
            {
                string strSQL = @"update mdm_dqi_tables set table_name=:table_name";
                try
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand(strSQL, connection);
                    command.BindByName = true;
                    command.CommandType = System.Data.CommandType.Text;

                    command.Parameters.Add(":table_name", OracleDbType.Varchar2).Value = RadListBoxSource.SelectedValue;
                    command.ExecuteNonQuery();
                    gridCat.Rebind();

                    // BindDDL();
                    this.lblmsgs.Text = MessageFormatter.GetFormattedSuccessMessage("Catalog Added Succesfully");

                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    lblmsgs.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
                }

            }
        }
        private void BindDDL()
        {
            #region rebind DDL
            //DDLTblCat.Items.Clear();
            //DDLTblCat.AppendDataBoundItems = true;
            ////String strConnString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            //String strQuery = "SELECT DISTINCT TABLE_DESC FROM MDM_DQI_PARAMETERS";
            //OracleCommand objCmd = new OracleCommand();
            //con = new OracleConnection(new Connection().ConnectionString);
            //objCmd.CommandType = CommandType.Text;
            //objCmd.CommandText = strQuery;
            //objCmd.Connection = con;
            //try
            //{
            //    con.Open();
            //    DDLTblCat.DataSource = objCmd.ExecuteReader();
            //    DDLTblCat.DataTextField = "TABLE_DESC";
            //    DDLTblCat.DataValueField = "TABLE_DESC";
            //    DDLTblCat.DataBind();
            //    DDLTblCat.Items.Insert(0, new ListItem("--SELECT CATALOG--", ""));
            //}
            //catch (Exception ex)
            //{
            //    ErrorSignal.FromCurrentContext().Raise(ex);
            //    //throw ex;
            //}
            //finally
            //{
            //    con.Close();
            //    con.Dispose();
            //}
            #endregion
        }
        protected void NewRegistrationButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/DQI/DataQualityQueue.aspx");

        }
        #endregion
        #region Tab2

        
        protected void btnSaveMetrics_Click(object sender, EventArgs e)
        {

        }

        protected void OnDelete_Permission(object sender, EventArgs e)
        {

        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //hidTAB.Value = "#tab3";

            //GridView3.PageIndex = e.NewPageIndex;
            //CustInfoDetailPageDataBind();


        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //hidTAB.Value = "#tab3";

            //GridView3.PageIndex = e.NewPageIndex;
            //CustInfoDetailPageDataBind();


        }

        #endregion

        

        static DQIProfileDirty()
        {
            Task.Factory.StartNew(TrackProgressAsync);
        }

        public void XmlHttp_Request(object sender, RadXmlHttpPanelEventArgs e)
        {
            XmlHttpPanel.Controls.Add(new LiteralControl(taskProgress.ToString()));
        }

        public static void TrackProgressAsync()
        {

            for (var i = 0; i <= 100; i++)
            {
                taskProgress = i;
                System.Threading.Thread.Sleep(2000);
            }
        }

        protected void xmlBtn_Click(object sender, EventArgs e)
        {
            
            //DQIProfileBiz rep = new DQIProfileBiz();
            ////rep.RunDQIProfileProc();
            //this.Master.DQIValue = rep.DQIValue();
            //this.Master.CustCount = rep.CustCount(Session["EntityName"].ToString());


            //this.Master.
            
        }

        protected void btnViewSummary_Click(object sender, EventArgs e)
        {
            DQIProfileBiz rep = new DQIProfileBiz();
            string dqi = rep.DQIValue();
            int custCount = rep.CustCount(Session["EntityName"].ToString());

            lblCustCount.Text = custCount.ToString();
            lblDQI.Text = dqi.ToString();
            //this.Master.DQIValue = dqi;
            //this.Master.CustCount = custCount;
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            DQIProfileBiz rep = new DQIProfileBiz();
            rep.RunDQIProfileProc();
        }
    }
}