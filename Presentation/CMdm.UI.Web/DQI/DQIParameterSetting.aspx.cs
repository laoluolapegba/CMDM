using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMdm.UI.Web.BLL;
//using MdmDqi.Data;
using CMdm.Data;
using System.Configuration;
using Oracle.DataAccess.Client;
using System.Data;


namespace Cdma.Web.DQI
{
    public partial class DQIParameterSetting : System.Web.UI.Page
    {

        public DQIParamSettingClass dqiC = new DQIParamSettingClass();
        //public MdmDqi.Data.MdmModel DQIdb = new MdmDqi.Data.MdmModel();
        //public Cdma.External.CDMA_Model db = new Cdma.External.CDMA_Model();
        AppDbContext db = new AppDbContext();
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLTblCat.AppendDataBoundItems = true;
                //String strConnString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
                String strQuery = "select a.catalog_id, a.catalog_name, b.group_name from mdm_catalog a, MDM_DQ_CATALOG_GROUP b where a.category_id = b.group_id";
                OracleCommand objCmd = new OracleCommand();
                con = new OracleConnection(new Connection().ConnectionString);
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strQuery;
                objCmd.Connection = con;
                try
                {
                    con.Open();
                    DDLTblCat.DataSource = objCmd.ExecuteReader();
                    DDLTblCat.DataTextField = "catalog_name";
                    DDLTblCat.DataValueField = "catalog_id";
                    DDLTblCat.DataBind();
                    DDLTblCat.Items.Insert(0, new ListItem("--SELECT CATALOG--", ""));

                    this.btnUpdateDQIParam.Visible = false;
                    //this.GridView1.Visible = false;
                    this.GridView1.Visible = true;


                }
                catch (Exception ex)
                {
                    lblDQIParamMsg.Text = "Error Occured:" + ex.Message;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
                // BindPageGrid();
            }
        }

        protected void ddlDQITblCat_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.DDLTblCat.SelectedItem.Value))
            {
                lblDQIParamMsg.Text = MessageFormatter.GetFormattedErrorMessage("Please select a Catalog.");
                return;
            }
            //DDLTblCol.Items.Add(new ListItem("--SELECT TABLE COLUMN--", ""));
            //DDLTblCol.AppendDataBoundItems = true;

            String strQuery = "SELECT COLUMNID,COLUMN_DESC,IMPORTANCE_LEVEL,COLUMN_WEIGHT,REGEX,CAST(COLUMN_REQUIRED AS number) AS COLMN_REQUIRED FROM MDM_DQI_PARAMETERS WHERE catalog_id=:catalog_id";
            OracleCommand objCmd = new OracleCommand();
            con = new OracleConnection(new Connection().ConnectionString);
            objCmd.Parameters.Add(":catalog_id", DDLTblCat.SelectedItem.Value);
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
                       DropDownList ddlREgex = (gvrow.Cells[6].FindControl("ddlRegex") as DropDownList);

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

    }
}