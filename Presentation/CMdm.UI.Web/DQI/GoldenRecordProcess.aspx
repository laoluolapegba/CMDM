<%@ Page Title="" Language="C#" MasterPageFile="~/Wiz.Master" AutoEventWireup="true" CodeBehind="GoldenRecordProcess.aspx.cs" Inherits="Cdma.Web.DQI.GoldenRecordProcess" %>
<%@ MasterType VirtualPath="~/Wiz.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/wizstyle.css" rel="stylesheet" type="text/css">
    <link href="/Content/summarystyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="WizContent" runat="server">
 <div class="content">
        <div class="main-header">
            <h2>Golden Record</h2>
            <em>Golden Record Setup</em>
        </div>

        <div class="main-content">
            <!-- WIDGET WIZARD -->
            <div class="widget">
                <div class="widget-header">
                    <h3><i class="fa fa-magic"></i>Golden Record Process</h3>
                </div>
                <div class="widget-content">

                    <telerik:RadWizard RenderMode="Lightweight" ID="RadWizard1" runat="server" Height="420px" OnClientLoad="OnClientLoad" OnClientButtonClicking="OnClientButtonClicking">
                        <WizardSteps>
                            <telerik:RadWizardStep ID="RadWizardStep1" Title="Select Entity" runat="server" StepType="Start" ValidationGroup="accountInfo" CausesValidation="true" SpriteCssClass="accountInfo">
                                <div class="widget-content">
                                    <table class="nav-justified">
                                        <tr>
                                            <td colspan="5">
                                                <asp:Label ID="lblmsgs" runat="server"></asp:Label></td>

                                        </tr>
                                    </table>

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
                                                    SelectCommand="select table_name from (select table_name table_name  
                                                    from user_tables where table_name like '%CDMA%'
                                        union select view_name table_name from user_views where view_name like '%CDMA%') "></asp:SqlDataSource>
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
                                                        <h3><i class="fa fa-table"></i>Available Entities</h3>
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

                            <telerik:RadWizardStep Title="DQI Profiling" runat="server" StepType="Step" ValidationGroup="personalInfo" SpriteCssClass="personalInfo">
                                <asp:Panel ID="tvScreen" runat="server" CssClass="tvContent">
                                    <asp:Button ID="btnRun" runat="server" Text="Run" OnClick="btnRun_Click" />
                                    <asp:Button ID="xmlBtn" runat="server" Text="Start Profiling" OnClientClick="return startXml();" OnClick="xmlBtn_Click" />

                                    <telerik:RadXmlHttpPanel ID="XmlHttpPanel" runat="server" OnClientResponseEnding="ajax_end" OnServiceRequest="XmlHttp_Request" />
                                    <telerik:RadProgressBar ID="RadProgressBar1" runat="server" />

                                </asp:Panel>

                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep Title="DQI Summary" runat="server" StepType="Step" ValidationGroup="ContactDetails" SpriteCssClass="contactDetails">
                                <%--<telerik:RadAjaxManager ID="RadAjaxManagerx" runat="server"></telerik:RadAjaxManager>--%>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>Click to View Summary</td>
                                        <td>
                                            <asp:Button ID="btnViewSummary" runat="server" Text="Get Summary" OnClick="btnViewSummary_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td>Customer Count:</td>
                                        <td>&nbsp;<asp:Label ID="lblCustCount" runat="server" Text=""></asp:Label></td>

                                    </tr>

                                    <tr>

                                        <td>Process DQI:</td>
                                        <td>
                                            <asp:Label ID="lblDQI" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                                <telerik:RadGrid ID="gridSummary" runat="server" DataSourceID="dsResult1" AllowFilteringByColumn="True" AllowPaging="True" AutoGenerateColumns="False">
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                    <MasterTableView DataSourceID="dsResult1" DataKeyNames="ID">
                                        <PagerStyle Mode="NumericPages" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="Id" DataType="System.Int32" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TABLE_CATEGORY" HeaderText="Category" DataType="System.String">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TABLE_NAME" HeaderText="Table" DataType="System.String">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COLUMN_NAME" HeaderText="Column" DataType="System.String">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="FAILED_PCT" HeaderText="Correctness failed %" DataType="System.String">
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridTemplateColumn HeaderText="Correctness failed %">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/DQI/CorrectnessDetails.aspx?Id={0}&Column={1}",
                    HttpUtility.UrlEncode(Eval("Id").ToString()), HttpUtility.UrlEncode(Eval("COLUMN_NAME").ToString())) %>'
                                                        Text='<%#Eval("FAILED_PCT") %>' />
                                                </ItemTemplate>

                                            </telerik:GridTemplateColumn>--%>

                                            <telerik:GridTemplateColumn HeaderText="Validity failed %">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/DQI/ValidityDetails.aspx.aspx?Id={0}&Column={1}",
                    HttpUtility.UrlEncode(Eval("Id").ToString()), HttpUtility.UrlEncode(Eval("COLUMN_NAME").ToString())) %>'
                                                        Text='<%#Eval("VALIDITY_FAILED_PCT") %>' />
                                                </ItemTemplate>

                                            </telerik:GridTemplateColumn>


                                            <telerik:GridBoundColumn DataField="RUN_DATE" HeaderText="Date" DataType="System.DateTime">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <asp:SqlDataSource ID="dsResult1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>"
                                    SelectCommand=" select id, table_Category,table_name,column_name,failed_pct,
                                    validity_failed_pct,after_bvn_failed_pct,run_date,branch_code      from DQI_PROFILE_RESULT23 "></asp:SqlDataSource>
                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep StepType="Finish" Title="Review" runat="server" ValidationGroup="Confirmation" SpriteCssClass="confirmation">

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

