<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DQICatResult.aspx.cs" Inherits="Cdma.Web.DQI.DQICatResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="main-header">
            <h2>Data Quality Reports </h2>
            <em>Catalog Summary </em>
        </div>

        <div class="main-content">
            <!-- WIDGET WIZARD -->
            <div class="widget">
                <div class="widget-header">
                    <h3><i class="fa fa-hand"></i>Catalog Summary</h3>
                </div>
                <div class="widget-content">
                    <telerik:RadGrid ID="gridSummary" runat="server"  OnNeedDataSource="gridSummary_NeedDataSource"  ShowStatusBar="true"
                        AllowMultiRowSelection="true" AllowPaging="True"  AutoGenerateColumns="False" AllowFilteringByColumn="true">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" />
                        <ClientSettings Selecting-AllowRowSelect="true">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="process_id">
                            <PagerStyle Mode="NumericPages" />
                            <Columns>


                                <telerik:GridBoundColumn DataField="branch_code" HeaderText="Branch" DataType="System.Int32" Visible="false">
                                </telerik:GridBoundColumn>

                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />
                                
                                <telerik:GridBoundColumn DataField="catalog_name" HeaderText="Catalog Name" DataType="System.String">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="dqi_result" HeaderText="DQI" DataType="System.Decimal">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="previous_result" HeaderText="Preious DQI" DataType="System.Decimal">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="dat_last_run" HeaderText="Last Run" DataType="System.DateTime" AllowFiltering="false">
                                </telerik:GridBoundColumn>


                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    

                </div>
            </div>
           
            <!-- END WIDGET WIZARD Jaro-Winkler and Levenshtein edit distance algorithms -->

        </div>
        <!-- /main-content -->
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ExtraJavaScripts" runat="server">
</asp:Content>
