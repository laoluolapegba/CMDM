<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GoldenRecord2.aspx.cs" Inherits="Cdma.Web.DQI.GoldenRecord2" %>

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
                    <h3><i class="fa fa-magic"></i>Auto Merge Result</h3>
                </div>
                <div class="widget-content">
                    <telerik:RadGrid ID="gridSummary" runat="server" DataSourceID="dsgoldenrec"  AllowPaging="True" AutoGenerateColumns="False">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <MasterTableView DataSourceID="dsgoldenrec" DataKeyNames="BVN_KEY">
                            <PagerStyle Mode="NumericPages" />
                            <Columns>
                                
                                <telerik:GridTemplateColumn HeaderText="MergeCount">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/DQI/GRRawData.aspx.aspx?Key={0}",
                    HttpUtility.UrlEncode(Eval("BVN_KEY").ToString())) %>'
                                            Text='<%#Eval("KOUNT") %>' />
                                    </ItemTemplate>

                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn DataField="BVN_KEY" HeaderText="BVN_KEY" DataType="System.Int32" Visible="false">
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
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="dsgoldenrec" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                        SelectCommand=" select BVN_KEY,BVN,CUST_FIRST_NAME,CUST_MIDDLE_NAME,CUST_LAST_NAME,MINOR_FLAG,PHONE,EMAIL,DOB,GENDER,KOUNT from uba_golden_view_bvn "></asp:SqlDataSource>
                    
                </div>
            </div>
            <!-- END WIDGET WIZARD Jaro-Winkler and Levenshtein edit distance algorithms -->

        </div>
        <!-- /main-content -->
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ExtraJavaScripts" runat="server">
</asp:Content>
