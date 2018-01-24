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

namespace Cdma.Web.DQI
{
    public partial class DQWizard : System.Web.UI.Page
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
                DDLTblCat.AppendDataBoundItems = true;
                //String strConnString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
                String strQuery = "SELECT DISTINCT TABLE_DESC FROM MDM_DQI_PARAMETERS";
                OracleCommand objCmd = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    DDLTblCat.DataSource = objCmd.ExecuteReader();
                    DDLTblCat.DataTextField = "TABLE_DESC";
                    DDLTblCat.DataValueField = "TABLE_DESC";
                    DDLTblCat.DataBind();
                    DDLTblCat.Items.Insert(0, new ListItem("--SELECT CATALOG--", ""));

                    this.btnUpdateDQIParam.Visible = false;
                    //this.GridView1.Visible = false;
                    this.GridView1.Visible = true;


                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    //throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
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

            if (bizRule.IsCatalogExist(RadListBoxSource.SelectedValue))
            {
                this.lblmsgs.Text = MessageFormatter.GetFormattedNoticeMessage("Catalog Already Exists");
                return;
            }
            if (RadListBoxDestination.CheckedItems.Count < 1)
            {
                this.lblmsgs.Text = MessageFormatter.GetFormattedNoticeMessage("At least 1 Catalog Attribute must be selected.");
                return;
            }
            using (OracleConnection connection = new OracleConnection(connString))
            {
                string strSQL = @"insert into MDM_DQI_PARAMETERS(table_categories, table_names,table_desc,column_names,column_desc,
                    created_by,created_date,record_status,column_order, column_weight)
                    values(:table_categories, :table_names,:table_desc,:column_names,:column_desc,
                    :created_by,:created_date,:record_status, :column_order, :column_weight)";
                try
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand(strSQL, connection);
                    command.BindByName = true;
                    command.CommandType = System.Data.CommandType.Text;

                    foreach (var item in RadListBoxDestination.CheckedItems)
                    {
                        command.Parameters.Add(":table_categories", OracleDbType.Varchar2).Value = "A";
                        command.Parameters.Add(":table_names", OracleDbType.Varchar2).Value = RadListBoxSource.SelectedValue;
                        command.Parameters.Add(":table_desc", OracleDbType.Varchar2).Value = RadListBoxSource.SelectedValue;
                        command.Parameters.Add(":column_names", OracleDbType.Varchar2).Value = item.Text;
                        command.Parameters.Add(":column_desc", OracleDbType.Varchar2).Value = item.Text;
                        command.Parameters.Add(":created_by", OracleDbType.Varchar2).Value = User.Identity.Name;
                        command.Parameters.Add(":created_date", OracleDbType.Date).Value = DateTime.Now;

                        command.Parameters.Add(":record_status", OracleDbType.Varchar2).Value = "Y";
                        command.Parameters.Add(":column_order", OracleDbType.Int32).Value = item.Value;
                        command.Parameters.Add(":column_weight", OracleDbType.Int32).Value = 5;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
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
            DDLTblCat.Items.Clear();
            DDLTblCat.AppendDataBoundItems = true;
            //String strConnString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            String strQuery = "SELECT DISTINCT TABLE_DESC FROM MDM_DQI_PARAMETERS";
            OracleCommand objCmd = new OracleCommand();
            con = new OracleConnection(new Connection().ConnectionString);
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = strQuery;
            objCmd.Connection = con;
            try
            {
                con.Open();
                DDLTblCat.DataSource = objCmd.ExecuteReader();
                DDLTblCat.DataTextField = "TABLE_DESC";
                DDLTblCat.DataValueField = "TABLE_DESC";
                DDLTblCat.DataBind();
                DDLTblCat.Items.Insert(0, new ListItem("--SELECT CATALOG--", ""));
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                //throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            #endregion
        }
        protected void NewRegistrationButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/DQI/DataQualityQueue.aspx");

        }
        #endregion
        #region Tab2

        protected void ddlDQITblCat_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.DDLTblCat.SelectedItem.Value))
            {
                lblDQIParamMsg.Text = MessageFormatter.GetFormattedErrorMessage("Please select a Catalog.");
                return;
            }
            //DDLTblCol.Items.Add(new ListItem("--SELECT TABLE COLUMN--", ""));
            //DDLTblCol.AppendDataBoundItems = true;

            String strQuery = "SELECT COLUMNID,COLUMN_DESC,IMPORTANCE_LEVEL,COLUMN_WEIGHT,REGEX,CAST(COLUMN_REQUIRED AS number) AS COLMN_REQUIRED FROM MDM_DQI_PARAMETERS WHERE TABLE_DESC=:TABLE_DESC";
            OracleCommand objCmd = new OracleCommand();
            con = new OracleConnection(new Connection().ConnectionString);
            objCmd.Parameters.Add(":TABLE_DESC", DDLTblCat.SelectedItem.Value);
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = strQuery;
            objCmd.Connection = con;
            objCmd.BindByName = true;
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = objCmd;
                da.Fill(ds);



                if (ds.Tables[0].Rows.Count > 0)
                {

                    GridView1.DataSource = ds;

                    GridView1.DataBind();

                    this.btnUpdateDQIParam.Visible = true;

                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    int columncount = GridView1.Rows[0].Cells.Count;
                    GridView1.Rows[0].Cells.Clear();
                    GridView1.Rows[0].Cells.Add(new TableCell());
                    GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView1.Rows[0].Cells[0].Text = "No Records Found";
                }


            }
            catch (Exception ex)
            {
                this.lblDQIParamMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblSerial");
                lblSerial.Text = ((GridView1.PageIndex * GridView1.PageSize) + e.Row.RowIndex + 1).ToString();
            }
        }


        protected void btnUpdateDQIParam_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (GridViewRow gvrow in GridView1.Rows)
                {

                    if (gvrow.RowType == DataControlRowType.DataRow)
                    {
                        //Finding the Checkbox in the Gridview
                        //int colID = Convert.ToInt16(gvrow.Cells[1].Text);
                        Label ColID = (gvrow.Cells[1].FindControl("lblColID") as Label);
                        CheckBox chkRequired = (gvrow.Cells[2].FindControl("chkRequired") as CheckBox);
                        DropDownList ddlWeight = (gvrow.Cells[3].FindControl("ddlCWeight") as DropDownList);
                        // DropDownList ddlREgex = (gvrow.Cells[6].FindControl("ddlRegex") as DropDownList);

                        Int16 colID = Convert.ToInt16(ColID.Text);
                        bool required = Convert.ToBoolean(chkRequired.Checked);
                        Int16 weight = Convert.ToInt16(ddlWeight.SelectedValue);
                        //Int16 regex = Convert.ToInt16(ddlREgex.SelectedValue);

                        //UpdateDQIParameter(colID, chkRequired, ddlWeight, ddlREgex);
                        UpdateDQIParameter(colID, required, weight);//, regex
                    }

                }
            }
            catch (Exception ex)
            {
                //throw ex;
                lblDQIParamMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }

        }
        public void UpdateDQIParameter(Int16 ColumnID, Boolean Required, Int16 Weight)//, Int16 Regex
        {
            hidTAB.Value = "#tab1";

            con.Open();

            try
            {

                OracleCommand cmd = new OracleCommand();
                // Set the command text on an OracleCommand object
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;
                cmd.CommandText = "UPDATE MDM_DQI_PARAMETERS SET COLUMN_REQUIRED=:COLUMN_REQUIRED,COLUMN_WEIGHT=:COLUMN_WEIGHT," +
                    "LAST_MODIFIED_BY=:LAST_MODIFIED_BY,LAST_MODIFIED_DATE=:LAST_MODIFIED_DATE WHERE COLUMNID=:COLUMNID";//REGEX=:REGEX,

                OracleParameter prm = new OracleParameter();

                cmd.Parameters.Add(":COLUMN_REQUIRED", OracleDbType.Int16).Value = Required;
                cmd.Parameters.Add(":COLUMN_WEIGHT", OracleDbType.Int16).Value = Weight;
                //cmd.Parameters.Add(":REGEX", OracleDbType.Int16).Value = Regex;
                cmd.Parameters.Add(":COLUMNID", OracleDbType.Int16).Value = ColumnID;
                cmd.Parameters.Add(":LAST_MODIFIED_BY", OracleDbType.NVarchar2).Value = "Admin";
                cmd.Parameters.Add(":LAST_MODIFIED_DATE", OracleDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add(":RECORD_STATUS", OracleDbType.NVarchar2).Value = "Y";
                // Execute the command
                var rst = cmd.ExecuteNonQuery();
                this.lblDQIParamMsg.Text = MessageFormatter.GetFormattedSuccessMessage("DQI Parameter updated Succesfully!");

            }
            catch (Exception ex)
            {
                lblDQIParamMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }

            // Close and Dispose OracleConnection object
            con.Close();
            //con.Dispose();


        }



        protected void btnSaveWeight_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab2";
            if (string.IsNullOrEmpty(this.txtWeightValue.Text))
            {
                return;
            }

            if (string.IsNullOrEmpty(this.txtWeightDesc.Text))
            {
                return;
            }



            con.Open();

            try
            {

                OracleCommand cmd = new OracleCommand();
                // Set the command text on an OracleCommand object
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;
                cmd.CommandText = "insert into MDM_WEIGHTS (WEIGHT_VALUE,CREATED_BY,CREATED_DATE,RECORD_STATUS,WEIGHT_DESC)" +
                                    " values (:p_WeightVal,:p_createdby,:p_createdDate,:p_recStatus,:p_weight_desc)";//

                OracleParameter prm = new OracleParameter();
                cmd.Parameters.Add(":p_WeightVal", OracleDbType.Int16).Value = this.txtWeightValue.Text;
                cmd.Parameters.Add(":p_createdby", OracleDbType.NVarchar2).Value = "Admin";
                cmd.Parameters.Add(":p_createdDate", OracleDbType.Date).Value = System.DateTime.Now;
                cmd.Parameters.Add(":p_recStatus", OracleDbType.NVarchar2).Value = "Y";
                cmd.Parameters.Add(":p_Weight_desc", OracleDbType.NVarchar2).Value = this.txtWeightDesc.Text;
                // Execute the command
                var rst = cmd.ExecuteNonQuery();
                lblWeightstatus.Text = MessageFormatter.GetFormattedSuccessMessage("Weight Created Succesfully!");

                //
                this.txtWeightValue.Text = "";
                this.txtWeightDesc.Text = "";
            }
            catch (Exception ex)
            {
                lblWeightstatus.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }

            // Close and Dispose OracleConnection object
            con.Close();
            con.Dispose();

            //bind data to gridview
            GridView2.DataBind();

        }

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

        protected void DDLTblCat_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
        {

            if (string.IsNullOrEmpty(this.DDLTblCat.SelectedItem.Value))
            {
                lblDQIParamMsg.Text = MessageFormatter.GetFormattedErrorMessage("Please select a Catalog.");
                return;
            }
            //DDLTblCol.Items.Add(new ListItem("--SELECT TABLE COLUMN--", ""));
            //DDLTblCol.AppendDataBoundItems = true;

            String strQuery = "SELECT COLUMNID,COLUMN_DESC,IMPORTANCE_LEVEL,COLUMN_WEIGHT,REGEX,CAST(COLUMN_REQUIRED AS number) AS COLMN_REQUIRED FROM MDM_DQI_PARAMETERS WHERE TABLE_DESC=:TABLE_DESC";
            OracleCommand objCmd = new OracleCommand();
            con = new OracleConnection(new Connection().ConnectionString);
            objCmd.Parameters.Add(":TABLE_DESC", DDLTblCat.SelectedItem.Value);
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = strQuery;
            objCmd.Connection = con;
            objCmd.BindByName = true;
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = objCmd;
                da.Fill(ds);



                if (ds.Tables[0].Rows.Count > 0)
                {

                    GridView1.DataSource = ds;

                    GridView1.DataBind();

                    this.btnUpdateDQIParam.Visible = true;

                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    int columncount = GridView1.Rows[0].Cells.Count;
                    GridView1.Rows[0].Cells.Clear();
                    GridView1.Rows[0].Cells.Add(new TableCell());
                    GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView1.Rows[0].Cells[0].Text = "No Records Found";
                }


            }
            catch (Exception ex)
            {
                this.lblDQIParamMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        static DQWizard()
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
    }
}