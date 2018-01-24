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
using Telerik.Web.UI;
//using MdmDqi.Data;
namespace Cdma.Web.DQI
{
    public partial class DataCatalog : System.Web.UI.Page
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        public OracleConnection con = new OracleConnection(connString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if(HttpContext.Current.User != null)
            {

            }
            RadListBoxSource.SelectedIndexChanged += new EventHandler(RadListBoxSource_SelectedIndexChanged);
            RadListBoxSource.SelectedIndexChanged += new EventHandler(RadListBoxSource_SelectedIndexChanged);
        }

        protected void RadListSrc2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

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
            if(RadListBoxDestination.CheckedItems.Count < 1)
            {
                this.lblmsgs.Text = MessageFormatter.GetFormattedNoticeMessage("At least 1 Catalog Attribute must be selected.");
                return;
            }
            using (OracleConnection connection = new OracleConnection(connString))
            {
                string strSQL = @"insert into MDM_DQI_PARAMETERS(table_categories, table_names,table_desc,column_names,column_desc,
                    created_by,created_date,record_status,column_order)
                    values(:table_categories, :table_names,:table_desc,:column_names,:column_desc,
                    :created_by,:created_date,:record_status, :column_order)";
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
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    gridCat.Rebind();
                    this.lblmsgs.Text = MessageFormatter.GetFormattedSuccessMessage("Catalog Added Succesfully");
                    //command.Parameters.Add(":table_categories", OracleDbType.Varchar2).Value = "";




                    //cmd.BindByName = true;
                    //cmd.CommandText = "insert into MDM_WEIGHTS (WEIGHT_VALUE,CREATED_BY,CREATED_DATE,RECORD_STATUS,WEIGHT_DESC)" +
                    //                    " values (:p_WeightVal,:p_createdby,:p_createdDate,:p_recStatus,:p_weight_desc)";//

                    //OracleParameter prm = new OracleParameter();
                    //cmd.Parameters.Add(":p_WeightVal", OracleDbType.Int16).Value = this.txtWeightValue.Text;
                    //cmd.Parameters.Add(":p_createdby", OracleDbType.NVarchar2).Value = "Admin";



                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    lblmsgs.Text = MessageFormatter.GetFormattedErrorMessage(ex.Message);
                }

            }
        }
    }
}