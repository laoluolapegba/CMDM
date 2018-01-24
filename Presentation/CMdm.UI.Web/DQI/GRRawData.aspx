<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GRRawData.aspx.cs" Inherits="Cdma.Web.DQI.GRRawData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="content">
        <div class="main-header">
            <h2>Golden Record</h2>
        </div>

        <div class="main-content">
            <!-- WIDGET WIZARD -->
            <div class="widget">
                <div class="widget-header">
                    <h3><i class="fa fa-data"></i>Pre-Merge Data</h3>
                </div>
                <div class="widget-content">
    <telerik:RadGrid ID="gridRawData" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowAutomaticUpdates="True"
        OnNeedDataSource="gridValidity_NeedDataSource"
        OnPreRender="gridValidity_PreRender">

        <MasterTableView DataKeyNames="fld_key" CommandItemDisplay="Top"
            HorizontalAlign="NotSet">
            <PagerStyle Mode="NumericPages" />
           <Columns>
                <telerik:GridBoundColumn DataField="fld_key" HeaderText="fld_key" DataType="System.Int32" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BVN" HeaderText="BVN" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CUST_FIRST_NAME" HeaderText="Firstname" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CUST_MIDDLE_NAME" HeaderText="Middlename" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CUST_LAST_NAME" HeaderText="Lastname" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MINOR_FLAG" HeaderText="Minor?" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PHONE" HeaderText="Phone" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EMAIL" HeaderText="Email" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DOB" HeaderText="DateOfBirth" DataType="System.String">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GENDER" HeaderText="Gender" DataType="System.String">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

        <ClientSettings AllowKeyboardNavigation="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
                      </div>
            </div>
            <!-- END WIDGET WIZARD -->

        </div>
        <!-- /main-content -->
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ExtraJavaScripts" runat="server">
</asp:Content>
