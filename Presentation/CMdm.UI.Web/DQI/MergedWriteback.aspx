<%@ Page Title="" Language="C#" MasterPageFile="~/Wiz.Master" AutoEventWireup="true" CodeBehind="MergedWriteback.aspx.cs" Inherits="Cdma.Web.DQI.MergedWriteback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="WizContent" runat="server">
    <telerik:RadGrid ID="gridAll" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowAutomaticUpdates="True"
         OnNeedDataSource="gridValidity_NeedDataSource"

        OnPreRender="gridValidity_PreRender">

        <MasterTableView DataKeyNames="CIF_ID" CommandItemDisplay="Top"
            HorizontalAlign="NotSet">
            <PagerStyle Mode="NumericPages" />

            <Columns>
                <telerik:GridBoundColumn DataField="BANK_ID" HeaderText="Bank Id" DataType="System.String" >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CIF_ID" HeaderText="CIF Id" DataType="System.String">
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
</asp:Content>
