<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataCatalog.aspx.cs" Inherits="Cdma.Web.DQI.DataCatalog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script type="text/javascript">
        $(document).ready(function () {
            var tab = document.getElementById('<%= hidTAB.ClientID%>').value;
            $('#myTabs a[href="' + tab + '"]').tab('show');
        });
    </script>--%>
    <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist" id="myTabs">
        <li id="Weight" runat="server"><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-archive"></i>Add New Catalog</a></li>
    </ul>

    <div class="tab-content">

        <div class="tab-pane fade active in" id="tab2">
            <div class="widget">
                <div class="widget-header">
                    <h3><i class="fa fa-edit"></i>Add New Catalog</h3>
                </div>
                <div class="widget-content">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                                <asp:Label ID="lblmsgs" runat="server"></asp:Label></td>

                        </tr>
                    </table>
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                            <telerik:AjaxSetting AjaxControlID="btnSave">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="lblmsgs" UpdatePanelCssClass="" />
                                    <telerik:AjaxUpdatedControl ControlID="gridCat" UpdatePanelCssClass="" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                        </AjaxSettings>
                    </telerik:RadAjaxManager>
                    <table class="nav-justified">

                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Tables"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Columns"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBoxSource" Height="200px" Width="300px"
                                    AutoPostBack="true"
                                    ButtonSettings-AreaWidth="35px" DataSourceID="dsTabs" DataKeyField="TABLE_NAME" DataValueField="TABLE_NAME" DataTextField="TABLE_NAME" OnSelectedIndexChanged="RadListBoxSource_SelectedIndexChanged">
                                </telerik:RadListBox>
                            </td>
                            <td>
                                <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBoxDestination" Height="200px"
                                    DataSourceID="dsCols" DataKeyField="column_id" DataValueField="column_id" DataTextField="column_name"
                                    CheckBoxes="true" ShowCheckAll="true" Width="300">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                        <%-- <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Views"></asp:Label>
                                </td>
                                <td></td>
                            </tr>--%>
                        <%--<tr>
                                <td>
                                    <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListSrc2" Height="200px" Width="300px"
                                        ButtonSettings-AreaWidth="35px" DataSourceID="dsViews" DataValueField="VIEW_NAME" DataTextField="VIEW_NAME" OnSelectedIndexChanged="RadListSrc2_SelectedIndexChanged">
                                    </telerik:RadListBox>
                                </td>
                                <td></td>
                            </tr>--%>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Add Catalog" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </td>

                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:SqlDataSource ID="dsTabs" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                                    SelectCommand="select table_name from (select table_name table_name  from user_tables 
                                        union select view_name table_name from user_views) "></asp:SqlDataSource>
                                <asp:SqlDataSource ID="dsCat" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>" ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                                    SelectCommand=" select distinct(table_names) catalog_name, table_desc catalog_desc,
                                         count(column_names) col_count  from MDM_DQI_PARAMETERS  group by table_names,table_desc"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="dsCols" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>" ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="select column_id, column_name ,table_name from user_tab_cols where table_name=:TABLE_NAME ">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="RadListBoxSource" Name="TABLE_NAME" PropertyName="SelectedValue"
                                            Type="String"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>Available Catalogues</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            <div class="gridpanel" style="overflow-x: auto; width: 1000px">
                                                <telerik:RadGrid ID="gridCat" RenderMode="Lightweight" runat="server" AllowPaging="True" DataSourceID="dsCat" AllowSorting="True"
                                                    PageSize="5" AutoGenerateColumns="False">
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                    <MasterTableView DataSourceID="dsCat" DataKeyNames="CATALOG_NAME">
                                                        <PagerStyle Mode="NumericPages" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="CATALOG_NAME" HeaderText="Name" DataType="System.String">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CATALOG_DESC" HeaderText="Description" DataType="System.String">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="COL_COUNT" HeaderText="# of Columns" DataType="System.Int32">
                                                            </telerik:GridBoundColumn>

                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>

                                            </div>
                                        </div>
                                        <asp:SqlDataSource ID="dsMast" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                            ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                                            SelectCommand=" select distinct(table_names) catalog_name, table_desc catalog_desc, count(column_names) col_count
                                                                 from MDM_DQI_PARAMETERS
                                                                 group by table_names,table_desc "></asp:SqlDataSource>
                                        <asp:SqlDataSource ID="dsdetail" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                            ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                                            SelectCommand=" select distinct(table_names) catalog_name, table_desc catalog_desc, count(column_names) col_count
                                                                 from MDM_DQI_PARAMETERS
                                                                 group by table_names,table_desc ">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="RadListBoxSource" Name="TABLE_NAME" PropertyName="SelectedValue"
                                                    Type="String"></asp:ControlParameter>
                                            </SelectParameters>
                                        </asp:SqlDataSource>

                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
    </div>



</asp:Content>
