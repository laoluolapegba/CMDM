<%@ Page Title="" Language="C#" MasterPageFile="~/Wiz.Master" AutoEventWireup="true" CodeBehind="DQWizard.aspx.cs" Inherits="Cdma.Web.DQI.DQWizard" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/wizstyle.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="WizContent" runat="server">
    <div class="content">
        <div class="main-header">
            <h2>Data Quality</h2>
            <em>Data Quality Process Setup</em>
        </div>

        <div class="main-content">
            <!-- WIDGET WIZARD -->
            <div class="widget">
                <div class="widget-header">
                    <h3><i class="fa fa-magic"></i>DQ Process</h3>
                </div>
                <div class="widget-content">
                    <telerik:RadWizard RenderMode="Lightweight" ID="RadWizard1" runat="server" Height="420px" OnClientLoad="OnClientLoad" OnClientButtonClicking="OnClientButtonClicking">
                        <WizardSteps>
                            <telerik:RadWizardStep ID="RadWizardStep1" Title="Add Catalog" runat="server" StepType="Start" ValidationGroup="accountInfo" CausesValidation="true" SpriteCssClass="accountInfo">
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
                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep Title="Configure" runat="server" StepType="Step" ValidationGroup="personalInfo" SpriteCssClass="personalInfo">
                                <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist" id="myTabs">
                                    <li id="DQIParam" runat="server"><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-columns"></i>DQI Parameter Setting</a></li>
                                    <li id="Weight" runat="server"><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-archive"></i>Weight Setting</a></li>
                                </ul>

                                <div class="tab-content">
                                    <div class="tab-pane fade   active in" id="tab1">
                                        <div class="widget">
                                            <div class="widget-header">
                                                <h3><i class="fa fa-edit"></i>Data Quality Index Parameter Setting</h3>
                                            </div>
                                            <div class="widget-content">

                                                <table class="nav-justified">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Label ID="lblDQIParamMsg" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col-sm-3 control-label" style="width: 144px">*Catalog:</td>
                                                        <td style="width: 211px">

                                                            <asp:DropDownList ID="DDLTblCat" runat="server" CssClass="form-control" Widht="50px" AutoPostBack="True" OnSelectedIndexChanged="ddlDQITblCat_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <%--<telerik:RadDropDownList ID="DDLTblCat" runat="server" DataSourceID="dsCat" DataTextField="catalog_name" DataValueField="catalog_name" AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="DDLTblCat_SelectedIndexChanged">

                                                            </telerik:RadDropDownList>--%>
                                                        </td>

                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col-sm-3 control-label" style="width: 144px">&nbsp;</td>
                                                        <td style="width: 211px">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>



                                                        <td class="col-sm-3 control-label" style="top: 96px; left: 0px;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;
                        						<div class="table-responsive">
                                                    <div class="gridpanel" style="overflow-x: auto; width: 1000px">

                                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderWidth="0px" CellPadding="0" CssClass="table" GridLines="None"
                                                            EmptyDataText="No Record(s) found!" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">

                                                            <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S/N">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:BoundField DataField="COLUMNID" Visible="false">
                            </asp:BoundField>--%>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColID" Text='<%# Convert.ToInt16 (Eval("COLUMNID")) %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="COLUMN_DESC" HeaderText="ATTRIBUTE">
                                                                    <HeaderStyle Wrap="False" />
                                                                    <ItemStyle Wrap="False" />
                                                                </asp:BoundField>


                                                                <asp:TemplateField HeaderText="REQUIRED?">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRequired" runat="server" Checked='<%# Convert.ToBoolean (Eval("COLMN_REQUIRED")) %>' ItemStyle-HorizontalAlign="Center" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="WEIGHT">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlCWeight" runat="server" SelectedValue='<%# Eval("COLUMN_WEIGHT") %>' DataSourceID="DSCWeight" DataTextField="WEIGHT_DESC" DataValueField="WEIGHT_VALUE" Width="120" ItemStyle-HorizontalAlign="Center">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                            </Columns>

                                                            <HeaderStyle BackColor="White" BorderColor="White" CssClass="rptheader" Font-Bold="True"
                                                                ForeColor="Black" Wrap="False" />
                                                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                                            <PagerStyle CssClass="pagerheader" />
                                                            <RowStyle BackColor="#E9F1F6" CssClass="rptitem" ForeColor="#4A3C8C" HorizontalAlign="Left" Wrap="False" />
                                                            <SelectedRowStyle BackColor="#738A9C" CssClass="rptitem" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />

                                                        </asp:GridView>

                                                    </div>

                                                </div>
                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hidTblName" runat="server" />
                                                            <%--//<asp:HiddenField ID="hidTAB" runat="server" Value="Users" />--%>
                                                            <asp:HiddenField ID="hidTAB" runat="server" Value="#tab1" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnUpdateDQIParam" runat="server" Text="Update Parameter" CssClass="btn btn-primary" OnClick="btnUpdateDQIParam_Click" />
                                                            &nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:SqlDataSource ID="DSCWeight" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                                                ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                                                                SelectCommand="SELECT &quot;WEIGHT_ID&quot;, &quot;WEIGHT_VALUE&quot;, &quot;WEIGHT_DESC&quot; FROM &quot;MDM_WEIGHTS&quot; ORDER BY &quot;WEIGHT_VALUE&quot;"></asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="tab-pane fade" id="tab2">
                                        <div class="widget">
                                            <div class="widget-header">
                                                <h3><i class="fa fa-edit"></i>Weight Setup</h3>
                                            </div>
                                            <div class="widget-content">
                                                <table class="nav-justified">
                                                    <tr>
                                                        <td colspan="5">
                                                            <asp:Label ID="lblWeightstatus" runat="server"></asp:Label></td>

                                                    </tr>
                                                </table>

                                                <table class="nav-justified">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Label ID="lblWeightMsg" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="col-sm-3 control-label" style="width: 144px">*Weight Value</td>
                                                        <td style="width: 211px">

                                                            <asp:TextBox ID="txtWeightValue" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                                                        </td>

                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>

                                                    </tr>
                                                    <tr>
                                                        <td class="col-sm-3 control-label" style="width: 144px">*Weight Description</td>
                                                        <td style="width: 211px">

                                                            <asp:TextBox ID="txtWeightDesc" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                                                        </td>

                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:Button ID="btnSaveWeight" runat="server" Text="Save Weight" CssClass="btn btn-primary" OnClick="btnSaveWeight_Click" />
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <div class="widget widget-table">
                                                                <div class="widget-header">
                                                                    <h3><i class="fa fa-table"></i>View Weight Values</h3>
                                                                </div>
                                                                <div class="widget-content">
                                                                    <div class="table-responsive">
                                                                        <div class="gridpanel" style="overflow-x: auto; width: 1000px">
                                                                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0" OnPageIndexChanging="GridView2_PageIndexChanging"
                                                                                CssClass="table" DataSourceID="SqlDataSource1" GridLines="None" PageSize="5" DataKeyNames="WEIGHT_ID" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" EmptyDataText="No Record(s) found!">
                                                                                <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                                                                <%--OnDataBinding="OnGridDataBinding"
                    OnDataBound="OnGridDataBound" OnPageIndexChanging="OnGridPageIndexChanging" OnSelectedIndexChanged="OnGridSelectedIndexChanged"
                    OnSorted="OnGridSorted" OnSorting="OnGridSorting"--%>
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="WEIGHT_VALUE" HeaderText="WEIGHT VALUE" SortExpression="WEIGHT_VALUE" />
                                                                                    <asp:BoundField DataField="WEIGHT_DESC" HeaderText="WEIGHT DESCRIPTION" SortExpression="WEIGHT_DESC" />
                                                                                    <asp:BoundField DataField="WEIGHT_ID" HeaderText="WEIGHT_ID" ReadOnly="True" SortExpression="WEIGHT_ID" Visible="false" />

                                                                                </Columns>
                                                                                <HeaderStyle BackColor="White" BorderColor="White" CssClass="rptheader" Font-Bold="True"
                                                                                    ForeColor="Black" Wrap="False" />
                                                                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                                                                <PagerStyle CssClass="pagerheader" />
                                                                                <RowStyle BackColor="#E9F1F6" CssClass="rptitem" ForeColor="#4A3C8C" HorizontalAlign="Left" Wrap="False" />
                                                                                <SelectedRowStyle BackColor="#738A9C" CssClass="rptitem" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                                                                            </asp:GridView>
                                                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                                                                ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                                                                                SelectCommand="SELECT &quot;WEIGHT_ID&quot;, &quot;WEIGHT_VALUE&quot;, &quot;WEIGHT_DESC&quot; FROM &quot;MDM_WEIGHTS&quot;"
                                                                                DeleteCommand="DELETE FROM &quot;MDM_WEIGHTS&quot; WHERE &quot;WEIGHT_ID&quot; = ?"
                                                                                InsertCommand="INSERT INTO &quot;MDM_WEIGHTS&quot; (&quot;WEIGHT_ID&quot;, &quot;WEIGHT_VALUE&quot;) VALUES (?, ?)"
                                                                                OldValuesParameterFormatString="original_{0}"
                                                                                UpdateCommand="UPDATE &quot;MDM_WEIGHTS&quot; SET &quot;WEIGHT_VALUE&quot; = ? WHERE &quot;WEIGHT_ID&quot; = ?">
                                                                                <DeleteParameters>
                                                                                    <asp:Parameter Name="original_WEIGHT_ID" Type="Decimal" />
                                                                                </DeleteParameters>
                                                                                <InsertParameters>
                                                                                    <asp:Parameter Name="WEIGHT_ID" Type="Decimal" />
                                                                                    <asp:Parameter Name="WEIGHT_VALUE" Type="Decimal" />
                                                                                </InsertParameters>
                                                                                <UpdateParameters>
                                                                                    <asp:Parameter Name="WEIGHT_VALUE" Type="Decimal" />
                                                                                    <asp:Parameter Name="original_WEIGHT_ID" Type="Decimal" />
                                                                                </UpdateParameters>
                                                                            </asp:SqlDataSource>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </div>
                                        </div>
                                    </div>


                                </div>


                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep Title="DQI Profiling" runat="server" StepType="Step" ValidationGroup="ContactDetails" SpriteCssClass="contactDetails">
                                <asp:Panel ID="tvScreen" runat="server" CssClass="tvContent">                                     
                                    <asp:Button ID="xmlBtn" runat="server" Text="Start Profiling" OnClientClick="return startXml();" />
                                    <telerik:RadXmlHttpPanel ID="XmlHttpPanel" runat="server" OnClientResponseEnding="ajax_end" OnServiceRequest="XmlHttp_Request" />
                                    <telerik:RadProgressBar ID="RadProgressBar1" runat="server" />
                                   
                                </asp:Panel>
                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep StepType="Finish" Title="Review" ValidationGroup="Confirmation" SpriteCssClass="confirmation">

                                <telerik:RadLinkButton ID="lnkReview" runat="server" Text="Click Finish to Review"></telerik:RadLinkButton>
                                <p class="anti-spam-policy">

                                    <asp:CustomValidator ID="AcceptTermsCheckBoxCustomValidator" runat="server"
                                        EnableClientScript="true" ClientValidationFunction="AcceptTermsCheckBoxValidation" ValidationGroup="Confirmation"
                                        ErrorMessage="Please agree to the anti-spam policy" Display="Dynamic"
                                        CssClass="checkbox-validator" ForeColor="Red" />
                                </p>
                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep runat="server" StepType="Complete" CssClass="complete">
                                <p>Completed successfully!</p>
                                <telerik:RadButton RenderMode="Lightweight" ID="NewRegistrationButton" runat="server" OnClick="NewRegistrationButton_Click" Text="Add a new Catalog"></telerik:RadButton>
                            </telerik:RadWizardStep>

                        </WizardSteps>
                    </telerik:RadWizard>
                </div>
            </div>
            <!-- END WIDGET WIZARD -->

        </div>
        <!-- /main-content -->
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var tab = document.getElementById('<%= hidTAB.ClientID%>').value;
            $('#myTabs a[href="' + tab + '"]').tab('show');
        });
        (function () {

            window.pageLoad = function () {
                var $ = $telerik.$;
                var cssSelectors = ["accountInfo", "personalInfo", "contactDetails", "confirmation"];
                var breadCrumbButtons = $(".rwzBreadCrumb .rwzLI");

                for (var i = 0; i < cssSelectors.length; i++) {
                    $(breadCrumbButtons[i]).addClass(cssSelectors[i]);
                }
            }

            window.OnClientLoad = function (sender, args) {
                for (var i = 1; i < sender.get_wizardSteps().get_count() ; i++) {
                    sender.get_wizardSteps().getWizardStep(i).set_enabled(false);
                }
            }

            window.OnClientButtonClicking = function (sender, args) {
                if (!args.get_nextActiveStep().get_enabled()) {
                    args.get_nextActiveStep().set_enabled(true);
                }
            }

            window.AcceptTermsCheckBoxValidation = function (source, args) {
                var termsChecked = $telerik.$("input[id*='AcceptTermsCheckBox']")[0].checked;
                args.IsValid = termsChecked;
            }

            window.UserNameLenghthValidation = function (source, args) {
                var userNameConditions = $telerik.$(".conditions")[0];
                var isValid = (args.Value.length >= 4 && args.Value.length <= 15);
                args.IsValid = isValid;
                $telerik.$(userNameConditions).toggleClass("redColor", !isValid);

            }

            window.PasswordLenghthValidation = function (source, args) {
                var passwordConditions = $telerik.$(".conditions")[1];
                var isValid = args.Value.length >= 6;
                args.IsValid = isValid;
                $telerik.$(passwordConditions).toggleClass("redColor", !isValid);
            }
        })();

    </script>
    <script src="/Content/js/wizprogressbar.js"></script>
    <script type="text/javascript">
        (function (global, undefined) {
            var interval;

            function startXml(sender, args) {
                var xml = $find("<%= XmlHttpPanel.ClientID %>");

                interval = setInterval(function () {
                    xml.set_value(getProgressBar().get_value());
                }, 900);
                return false;
            }

            function ajax_end(sender, args) {
                var progressBar = getProgressBar(),
                    value = parseInt(args.get_content(), 10);

                progressBar.set_value(value);
                if (value >= 100) {
                    clearInterval(interval);
                }
            }

            function getProgressBar() { return $find("<%= RadProgressBar1.ClientID %>"); }

			global.startXml = startXml;
			global.ajax_end = ajax_end;
        })(window);
    </script>
</asp:Content>
