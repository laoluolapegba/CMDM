<%@ Page Title="" Language="C#" MasterPageFile="~/Wiz.Master" AutoEventWireup="true" CodeBehind="ValidityDetails.aspx.cs" Inherits="Cdma.Web.DQI.ValidityDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="WizContent" runat="server">
    <p>
        Click on a cell/row to place it in edit mode. Use the Save changes or Cancel changes buttons to process/discard all changes at once.
    </p>
    <telerik:RadAjaxManager runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridValidity">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SavedChangesList" />
                    <telerik:AjaxUpdatedControl ControlID="gridValidity" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1"></telerik:RadAjaxLoadingPanel>
    <table style="width: 100%;">
        <tr>
            <td>Click to Merge:</td>
            <td>
                <asp:Button ID="btnMerge" runat="server" Text="Merge" OnClick="btnMerge_Click"/></td>
        </tr>
        <tr>

            <td>
                Click to View:
            </td>
            
            <td>&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/DQI/MergedWriteback.aspx">View</asp:LinkButton></td>
        </tr>
        <tr>

            <td><asp:Label ID="lblmsgs" runat="server" Text=""></asp:Label></td>
            
            <td>&nbsp;</td>
        </tr>
    </table>
    <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="SavedChangesList" Width="400px" Height="50px" Visible="false"></telerik:RadListBox>
    <telerik:RadGrid ID="gridValidity" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowAutomaticUpdates="True" OnNeedDataSource="gridValidity_NeedDataSource"
        OnPageIndexChanged="gridValidity_PageIndexChanged" OnPageSizeChanged="gridValidity_PageSizeChanged"
        OnSortCommand="gridValidity_SortCommand" OnBatchEditCommand="gridValidity_BatchEditCommand"
        OnItemCreated="gridValidity_ItemCreated" OnItemDeleted="gridValidity_ItemDeleted" OnItemInserted="gridValidity_ItemInserted"
        OnItemUpdated="gridValidity_ItemUpdated" OnUpdateCommand="gridValidity_UpdateCommand" OnPreRender="gridValidity_PreRender">

        <MasterTableView DataKeyNames="RECORD_ID" CommandItemDisplay="Top"
            HorizontalAlign="NotSet" EditMode="Batch">
            <BatchEditingSettings EditType="Cell" />
            <PagerStyle Mode="NumericPages" />

            <Columns>
                <telerik:GridBoundColumn DataField="RECORD_ID" HeaderText="Id" DataType="System.Int32" ReadOnly="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CIF_ID" HeaderText="CIF Id" DataType="System.String" ReadOnly="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FIRST_NAME" HeaderText="Firstname" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MIDDLE_NAME" HeaderText="Middlename" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LAST_NAME" HeaderText="Lastname" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DOB" HeaderText="Date of Birth" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GENDER" HeaderText="Gender" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OCCUPATION" HeaderText="Occupation" DataType="System.String"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MINOR_FLAG" HeaderText="Minor" DataType="System.String"></telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="MARITAL_STATUS" HeaderText="Marital Status" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NATIONALITY" HeaderText="Nationality" DataType="System.String">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

        <ClientSettings AllowKeyboardNavigation="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:SqlDataSource ID="dsValityexp" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
        SelectCommand="select cif_id,first_name,middle_name,last_name,dob,gender,occupation,minor_flag,marital_status,nationality from cdma_individual_data_gap23 "></asp:SqlDataSource>

</asp:Content>
