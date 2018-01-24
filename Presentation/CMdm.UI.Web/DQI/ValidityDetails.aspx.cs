using CMdm.UI.Web.BLL;
using Elmah;
using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Cdma.Web.DQI
{
    public partial class ValidityDetails : System.Web.UI.Page
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        //public MdmDqi.Data.MdmModel DQIdb = new MdmDqi.Data.MdmModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataForgridValidity();
            }
        }
        private void LoadDataForgridValidity()
        {
            gridValidity.DataSource = GetDataTable();
        }
        protected void gridValidity_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = GetDataTable();
        }
        public DataTable GetDataTable()
        {

            OracleConnection conn = new OracleConnection(connString);
            DataTable myDataTable = new DataTable();
            try
            {
                string column = Request.QueryString["column"].ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT RECORD_ID,CIF_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,DOB,GENDER,OCCUPATION,MINOR_FLAG,MARITAL_STATUS,NATIONALITY from cdma_individual_data_gap23 where ");
                sb.Append(column);
                sb.Append(" is not null and rownum < 500");

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = new OracleCommand(sb.ToString(), conn);

                conn.Open();
                adapter.Fill(myDataTable);
            }
            finally
            {
                conn.Close();
            }

            return myDataTable;
        }

        protected void gridValidity_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            LoadDataForgridValidity();
        }

        protected void gridValidity_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadDataForgridValidity();
        }

        protected void gridValidity_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadDataForgridValidity();
        }

        protected void gridValidity_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            using (OracleConnection connection = new OracleConnection(connString))
            {
                string strSQL = @"update cdma_individual_data_gap23
                                    set FIRST_NAME = :first_name,MIDDLE_NAME = :middle_name,LAST_NAME = :lastname,DOB = :dob,GENDER = :gender,
                                    OCCUPATION = :occupation,MINOR_FLAG = :minor_flag,MARITAL_STATUS = :marital_status,NATIONALITY = :nationality
                                    where record_id = :record_id";

                try
                {
                    connection.Open();
                    OracleCommand oracmd = new OracleCommand(strSQL, connection);
                    oracmd.BindByName = true;
                    oracmd.CommandType = System.Data.CommandType.Text;

                    foreach (GridBatchEditingCommand command in e.Commands)
                    {
                        // For Update
                        if ((command.Type == GridBatchEditingCommandType.Update))
                        {
                            Hashtable newValues = command.NewValues;
                            Hashtable oldValues = command.OldValues;
                            //CIF_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,DOB,GENDER,OCCUPATION,MINOR_FLAG,MARITAL_STATUS,NATIONALITY
                            string ID = newValues["RECORD_ID"].ToString();
                            //string cifId = newValues["CIF_ID"].ToString();
                            string fname = newValues["FIRST_NAME"] == null ? "" : newValues["FIRST_NAME"].ToString();
                            string midname = newValues["MIDDLE_NAME"] ==null ? "" : newValues["MIDDLE_NAME"].ToString();
                            string lname = newValues["LAST_NAME"] ==null ? "" : newValues["LAST_NAME"].ToString();
                            string dob = newValues["DOB"] ==null ? "" : newValues["DOB"].ToString();
                            string gender = newValues["GENDER"] ==null ? "" : newValues["GENDER"].ToString();
                            string occupation = newValues["OCCUPATION"] ==null ? "" : newValues["OCCUPATION"].ToString();
                            string minor = newValues["MINOR_FLAG"] ==null ? "" : newValues["MINOR_FLAG"].ToString();
                            string maritalstat = newValues["MARITAL_STATUS"] ==null ? "" : newValues["MARITAL_STATUS"].ToString();
                            string nation = newValues["NATIONALITY"] ==null ? "" : newValues["NATIONALITY"].ToString();


                            oracmd.Parameters.Add(":record_id", OracleDbType.Int32).Value = ID;
                            oracmd.Parameters.Add(":first_name", OracleDbType.Varchar2).Value = fname;
                            oracmd.Parameters.Add(":middle_name", OracleDbType.Varchar2).Value = midname;
                            oracmd.Parameters.Add(":lastname", OracleDbType.Varchar2).Value = lname;
                            oracmd.Parameters.Add(":dob", OracleDbType.Varchar2).Value = dob;
                            oracmd.Parameters.Add(":gender", OracleDbType.Varchar2).Value = gender;
                            oracmd.Parameters.Add(":occupation", OracleDbType.Varchar2).Value = occupation;

                            oracmd.Parameters.Add(":minor_flag", OracleDbType.Varchar2).Value = minor;
                            oracmd.Parameters.Add(":marital_status", OracleDbType.Varchar2).Value = maritalstat;
                            oracmd.Parameters.Add(":nationality", OracleDbType.Varchar2).Value = nation;
                            oracmd.ExecuteNonQuery();
                            oracmd.Parameters.Clear();


                        }
                    }


                }
                catch (Exception ex)
                {
                    NotifyUser("Unable to update " + ex.Message);
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            gridValidity.Rebind();
            SavedChangesList.Visible = true;
        }

        protected void gridValidity_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void gridValidity_ItemDeleted(object sender, GridDeletedEventArgs e)
        {

        }

        protected void gridValidity_ItemUpdated(object sender, GridUpdatedEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            String id = dataItem.GetDataKeyValue("Record_Id").ToString();
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                NotifyUser("Item with ID " + id + " cannot be deleted. Reason: " + e.Exception.Message);
            }
            else
            {
                NotifyUser("Item with ID " + id + " is deleted!");
            }
        }

        protected void gridValidity_ItemInserted(object sender, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                NotifyUser("Item cannot be inserted. Reason: " + e.Exception.Message);
            }
            else
            {
                NotifyUser("New item is inserted!");
            }
        }
        private void NotifyUser(string message)
        {
            RadListBoxItem commandListItem = new RadListBoxItem();
            commandListItem.Text = message;
            SavedChangesList.Items.Add(commandListItem);
        }

        protected void gridValidity_UpdateCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gridValidity_PreRender(object sender, EventArgs e)
        {
            foreach (GridCommandItem item in gridValidity.MasterTableView.GetItems(GridItemType.CommandItem))
            {
                Button btn = (Button)item.FindControl("AddNewRecordButton");
                //LinkButton Addbtn = (LinkButton)item.FindControl("InitInsertButton");
                btn.Visible = false;
                //Addbtn.Visible = false;
            }
        }

        protected void btnMerge_Click(object sender, EventArgs e)
        {
            using (OracleConnection connection = new OracleConnection(connString))
            {
                string strSQL = @"insert into UBA_CDMA_RESULT(bank_id,cif_id,first_name,middle_name,last_name,dob,gender,occupation,minor_flag,marital_status,employer_name,nationality)
select null, cif_id,first_name,middle_name,last_name,dob,gender,occupation,minor_flag,marital_status,null,nationality
from cdma_individual_data_gap23";
                try
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand(strSQL, connection);
                    command.BindByName = true;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();


                    // BindDDL();
                    this.lblmsgs.Text = MessageFormatter.GetFormattedSuccessMessage("Successfully merged!");

                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    lblmsgs.Text = MessageFormatter.GetFormattedErrorMessage("Error:" + ex.Message);
                }

            }
        }
    }
}