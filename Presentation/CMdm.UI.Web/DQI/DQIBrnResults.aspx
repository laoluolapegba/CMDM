<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DQIBrnResults.aspx.cs" Inherits="Cdma.Web.DQI.DQIBrnResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="main-header">
            <h2>Data Quality Report </h2>
            <em>Branch Summary </em>
        </div>

        <div class="main-content">
            <!-- WIDGET WIZARD -->
            <div class="widget">
                <div class="widget-header">
                    <h3><i class="fa fa-hand"></i>Branch Summary</h3>
                </div>
                <div class="widget-content">
                    <telerik:RadGrid ID="gridSummary" runat="server" DataSourceID="dsdqiresult"  ShowStatusBar="true"
                        AllowMultiRowSelection="true" AllowPaging="True"  AutoGenerateColumns="False" AllowFilteringByColumn="true">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" />
                        <ClientSettings Selecting-AllowRowSelect="true">
                        </ClientSettings>
                        <MasterTableView DataSourceID="dsdqiresult" DataKeyNames="branch_code">
                            <PagerStyle Mode="NumericPages" />
                            <Columns>


                                <telerik:GridBoundColumn DataField="branch_code" HeaderText="Branch" DataType="System.Int32" Visible="false">
                                </telerik:GridBoundColumn>

                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />
                                
                                <telerik:GridBoundColumn DataField="branch_name" HeaderText="Branch Name" DataType="System.String">
                                </telerik:GridBoundColumn>
                               
                                <telerik:GridTemplateColumn HeaderText="DQI">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/DQI/DQICatResult.aspx.aspx?branchcode={0}",
                    HttpUtility.UrlEncode(Eval("branch_code").ToString())) %>'
                                            Text='<%#Eval("dqi_result") %>' />
                                    </ItemTemplate>

                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="previous_result" HeaderText="Preious DQI" DataType="System.Decimal">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="dat_last_run" HeaderText="Last Run" DataType="System.DateTime" AllowFiltering="false">
                                </telerik:GridBoundColumn>


                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="dsdqiresult" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                        SelectCommand="select a.branch_code , branch_name , avg(dqi_result)  dqi_result,  avg(previous_dqi_result) previous_result, dat_last_run
from cdma_dqi_processing_result a, SRC_STTM_BRANCH b, mdm_catalog c
where a. branch_code = b.branch_code and a.mdm_catalog_id = c. catalog_id
group by a.branch_code , branch_name, dat_last_run"></asp:SqlDataSource>

                </div>
            </div>
            <%--<asp:Button ID="btnAccept" runat="server" Text="Accept Selected Records" CssClass="btn btn-primary" OnClick="btnAccept_Click" />--%>
            <!-- END WIDGET WIZARD Jaro-Winkler and Levenshtein edit distance algorithms -->

        </div>
        <!-- /main-content -->
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ExtraJavaScripts" runat="server">
</asp:Content>
