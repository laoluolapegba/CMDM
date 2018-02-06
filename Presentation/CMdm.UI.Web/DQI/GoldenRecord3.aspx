<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GoldenRecord3.aspx.cs" Inherits="Cdma.Web.DQI.GoldenRecord3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="main-header">
            <h2>Golden Record</h2>
            <em>Golden Record Step 2 </em>
        </div>

        <div class="main-content">
            <!-- WIDGET WIZARD -->
            <div class="widget">
                <div class="widget-header">
                    <h3><i class="fa fa-hand"></i>Manual Merge</h3>
                </div>
                <div class="widget-content">
                    <telerik:RadGrid ID="gridSummary" runat="server" DataSourceID="dsgoldenrec"  ShowStatusBar="true"
                        AllowMultiRowSelection="true" AllowPaging="True"  AutoGenerateColumns="False" AllowFilteringByColumn="true">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" />
                        <ClientSettings Selecting-AllowRowSelect="true">
                        </ClientSettings>
                        <MasterTableView DataSourceID="dsgoldenrec" DataKeyNames="RECORD_ID">
                            <PagerStyle Mode="NumericPages" />
                            <Columns>


                                <telerik:GridBoundColumn DataField="BVN_KEY" HeaderText="BVN_KEY" DataType="System.Int32" Visible="false">
                                </telerik:GridBoundColumn>

                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />
                                <telerik:GridBoundColumn DataField="BVN" HeaderText="BVN" DataType="System.String" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CUST_FIRST_NAME" HeaderText="Firstname" DataType="System.String">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CUST_MIDDLE_NAME" HeaderText="Middlename" DataType="System.String">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CUST_LAST_NAME" HeaderText="Lastname" DataType="System.String">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MINOR_FLAG" HeaderText="Minor?" DataType="System.String" AllowFiltering="false">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="PHONE" HeaderText="Phone" DataType="System.String">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EMAIL" HeaderText="Email" DataType="System.String">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DOB" HeaderText="DateOfBirth" DataType="System.String">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="GENDER" HeaderText="Gender" DataType="System.String" AllowFiltering="false">
                                </telerik:GridBoundColumn>


                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="dsgoldenrec" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                        SelectCommand=" select BVN_KEY,BVN,CUST_FIRST_NAME,CUST_MIDDLE_NAME,CUST_LAST_NAME,MINOR_FLAG,PHONE,EMAIL,DOB,GENDER, RECORD_ID from new_uba_cdma where bvn_key is null and rownum < 2000"></asp:SqlDataSource>

                </div>
            </div>
            <asp:Button ID="btnAccept" runat="server" Text="Accept Selected Records" CssClass="btn btn-primary" OnClick="btnAccept_Click" />
            <asp:Button ID="btnReject" runat="server" Text="Reject Selected Records" CssClass="btn btn-primary" />
            <!-- END WIDGET WIZARD Jaro-Winkler and Levenshtein edit distance algorithms -->

        </div>
        <!-- /main-content -->
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ExtraJavaScripts" runat="server">
</asp:Content>
