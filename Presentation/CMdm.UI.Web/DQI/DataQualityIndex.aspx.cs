using CMdm.Data;
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.Dqi;
using CMdm.UI.Web.BLL;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cdma.Web.DQI
{
    public partial class DataQualityIndex : System.Web.UI.Page
    {
        //public Cdma.External.CDMA_Model db = new Cdma.External.CDMA_Model();
        AppDbContext db = new AppDbContext();

        private static string logs = "";
        //private Customer custObj;
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        // public OracleCommand cmd = new OracleCommand();
        public OracleDataReader rdr;
        public OracleDataAdapter adapter = new OracleDataAdapter();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnCreateDQIScript.Visible = true;
        }

        protected void btnSave_DQIScript(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDQIName.Text))
            {
                this.lblDQIMsg.Text = MessageFormatter.GetFormattedErrorMessage("DQI Name required!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtDQIScript.Text))
            {
                this.lblDQIMsg.Text = MessageFormatter.GetFormattedErrorMessage("DQI Script required!");
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
                cmd.CommandText = "insert into MDM_DQI_SETTING (DQI_NAME,DQI_DESC,DQI_SCRIPT)" +
                                    " values (:p_DQI_NAME,:p_DQI_DESC,:p_DQI_SCRIPT)";//

                OracleParameter prm = new OracleParameter();
                cmd.Parameters.Add(":p_DQI_NAME", OracleDbType.NVarchar2).Value = this.txtDQIName.Text;
                cmd.Parameters.Add(":p_DQI_DESC", OracleDbType.NVarchar2).Value = this.txtDQLDesc.Text;
                cmd.Parameters.Add(":p_DQI_SCRIPT", OracleDbType.NVarchar2).Value = this.txtDQIScript.Text;
                // Execute the command
                var rst = cmd.ExecuteNonQuery();
                this.lblDQIMsg.Text = MessageFormatter.GetFormattedSuccessMessage("DQI Script Successfully!");

                this.txtDQIName.Text = "";
                this.txtDQLDesc.Text = "";
                this.txtDQIScript.Text = "";
            }
            catch (Exception ex)
            {
                lblDQIMsg.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
            }

            // Close and Dispose OracleConnection object
            con.Close();
            con.Dispose();
        }


        protected void btnUpdate_DQIScript(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDQIName.Text))
            {
                this.lblDQIMsg.Text = MessageFormatter.GetFormattedErrorMessage("DQI Name required!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtDQIScript.Text))
            {
                this.lblDQIMsg.Text = MessageFormatter.GetFormattedErrorMessage("DQI Script required!");
                return;
            }

            int HIDID = Convert.ToInt32(HidID.Value);

            try
            {
                MDM_DQI_SETTING objDQI = db.MDM_DQI_SETTING.SingleOrDefault(x => x.DQI_ID == HIDID);

                objDQI.DQI_NAME = txtDQIName.Text;
                objDQI.DQI_DESC = txtDQLDesc.Text;
                objDQI.DQI_SCRIPT = txtDQIScript.Text;

                db.SaveChanges();
                this.lblDQIMsg.Text = MessageFormatter.GetFormattedSuccessMessage("DQI Updated Succesfully!");
                clearForm();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                this.lblDQIMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                clearForm();
            }


            this.btnUpdateDQIScript.Visible = false;
            this.btnCreateDQIScript.Visible = true;
        }

        private void clearForm()
        {
            txtDQIName.Text = "";
            txtDQLDesc.Text = "";
            txtDQIScript.Text = "";

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView1.PageIndex = e.NewPageIndex;
            //BindPageGrid();
        }

        protected void OnEdit_DQI(object sender, EventArgs e)
        {

            this.btnUpdateDQIScript.Visible = true;
            this.btnCreateDQIScript.Visible = false;

            this.HidID.Value = "";

            LinkButton lnk = sender as LinkButton;

            decimal DQIId = Convert.ToDecimal(lnk.Attributes["RecId"]);

            var edit = from n in db.MDM_DQI_SETTING
                       where n.DQI_ID == DQIId
                       select n;
            try
            {

                this.txtDQIName.Text = edit.FirstOrDefault().DQI_NAME;
                this.txtDQLDesc.Text = edit.FirstOrDefault().DQI_DESC;
                this.txtDQIScript.Text = edit.FirstOrDefault().DQI_SCRIPT;
                this.HidID.Value = Convert.ToString(edit.FirstOrDefault().DQI_ID);

            }
            catch (Exception ex)
            {
                lblDQIMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error: " + ex.Message);
            }
            //Usrs.Attributes.Add("class", "active");

        }


        protected void OnDelete_DQI(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            string recId = lnk.Attributes["RecId"];
            int ID = Convert.ToInt32(recId);
            try
            {
                MDM_DQI_SETTING ObjDQI = db.MDM_DQI_SETTING.SingleOrDefault(x => x.DQI_ID == ID );

                db.MDM_DQI_SETTING.Remove(ObjDQI);
                db.SaveChanges();

                this.lblDQIMsg.Text = MessageFormatter.GetFormattedSuccessMessage("DQI Deleted Succesfully!");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                this.lblDQIMsg.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
            }
            // Usrs.Attributes.Add("class", "active");

        }


    }
}