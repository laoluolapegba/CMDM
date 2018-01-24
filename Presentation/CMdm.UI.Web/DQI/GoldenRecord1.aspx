<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GoldenRecord1.aspx.cs" Inherits="Cdma.Web.DQI.GoldenRecord1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="main-header">
            <h2>Golden Record</h2>
            <em>Golden Record Step 1 </em>
        </div>

        <div class="main-content">
            <!-- WIDGET WIZARD -->
            <div class="widget">
                <div class="widget-header">
                    <h3><i class="fa fa-magic"></i>Rule Definition </h3>
                </div>
                <div class="widget-content">
                    <telerik:RadGrid ID="gridRules" runat="server" DataSourceID="dsrules" AllowPaging="True" AutoGenerateColumns="False">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <MasterTableView DataSourceID="dsrules" DataKeyNames="rule_id">
                            <PagerStyle Mode="NumericPages" />
                            <Columns>

                                <telerik:GridBoundColumn DataField="rule_id" HeaderText="ID" DataType="System.Int32" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="rule_name" HeaderText="Rule" DataType="System.String">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="rule_description" HeaderText="Rule Description" DataType="System.String">
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridCheckBoxColumn DataField="rule_status" HeaderText="Status"  DataType="System.String">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn UniqueName="rule_status" HeaderText="Status"></telerik:GridCheckBoxColumn>
                                --%>
                                <telerik:GridTemplateColumn HeaderText="Enabled" UniqueName="TemplateColumn">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server"
                                            Checked='<%#  Convert.ToInt32(Eval("rule_status")) == 1 %>'
                                            OnCheckedChanged="CheckBox2_CheckedChanged" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Convert.ToInt32(Eval("rule_status")) == 1 %>'
                                            Enabled="False" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="dsrules" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                        SelectCommand="select rule_id,rule_name,rule_description,rule_status from cdma_golden_record_rules"></asp:SqlDataSource>


                </div>
            </div>
            <!-- END WIDGET WIZARD -->

        </div>
        <!-- /main-content -->
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ExtraJavaScripts" runat="server">
</asp:Content>
