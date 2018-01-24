<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DQWizard1.aspx.cs" Inherits="Cdma.Web.DQWizard1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Dashboard | CDMA - Admin Dashboard</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <meta name="description" content="CDMA Admin Dashboard"/>
    <meta name="author" content="Bluechip Technologies Ltd"/>

    <!-- CSS -->
    <link href="/Content/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
    <link href="/Content/assets/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
    <link href="/Content/assets/css/main.css" rel="stylesheet" type="text/css"/>
    <link href="/Content/assets/css/my-custom-styles.css" rel="stylesheet" type="text/css"/>

    <!--[if lte IE 9]>
		<link href="assets/css/main-ie.css" rel="stylesheet" type="text/css"/>
		<link href="assets/css/main-ie-part2.css" rel="stylesheet" type="text/css"/>
	<![endif]-->


    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="/Content/assets/ico/kingadmin-favicon144x144.png"/>
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="/Content/assets/ico/kingadmin-favicon114x114.png"/>
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="/Content/assets/ico/kingadmin-favicon72x72.png"/>
    <link rel="apple-touch-icon-precomposed" sizes="57x57" href="/Content/assets/ico/kingadmin-favicon57x57.png"/>
    <link rel="shortcut icon" href="/Content/assets/ico/favicon.png"/>
</head>

<body class="comp-wizard">
    <form id="form1" runat="server">
       
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
      
        
        <!-- WRAPPER -->
        <div class="wrapper">

            <!-- TOP BAR -->
            <div class="top-bar">
                <div class="container">
                    <div class="row">
                        <!-- logo -->
                        <div class="col-md-2 logo">
                            <a href="#">
                                <img src="../Content/assets/img/kingadmin-logo-white.png" alt="CDMA Admin - Admin Dashboard" /></a>
                            <h1 class="sr-only">CDMA Admin Dashboard</h1>

                        </div>
                        <!-- end logo -->
                        <div class="col-md-10">
                            <div class="row">
                                <div class="col-md-3">
                                    <!-- search box -->
                                    <div id="tour-searchbox" class="input-group searchbox">
                                        <input type="search" class="form-control" placeholder="enter keyword here...">
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>
                                        </span>
                                    </div>
                                    <!-- end search box -->
                                </div>
                                <div class="col-md-9">
                                    <div class="top-bar-right">
                                        <!-- responsive menu bar icon -->
                                        <a href="#" class="hidden-md hidden-lg main-nav-toggle"><i class="fa fa-bars"></i></a>
                                        <!-- end responsive menu bar icon -->
                                        <button type="button" id="start-tour" class="btn btn-link">
                                            <i class="fa fa-laptop"></i>Role:
                                            <asp:Label ID="lblRole" runat="server" Text=""></asp:Label>
                                        </button>
                                        <button type="button" id="global-volume" class="btn btn-link btn-global-volume"><i class="fa"></i></button>
                                        <div class="notifications">
                                            <ul>
                                                <!-- notification: inbox -->
                                                <li class="notification-item inbox">
                                                    <div class="btn-group">
                                                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                                            <i class="fa fa-envelope"></i><span class="count">0</span>
                                                            <span class="circle"></span>
                                                        </a>

                                                    </div>
                                                </li>
                                                <!-- end notification: inbox -->
                                                <!-- notification: general -->
                                                <li class="notification-item general">
                                                    <div class="btn-group">
                                                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                                            <i class="fa fa-bell"></i><span class="count">0</span>
                                                            <span class="circle"></span>
                                                        </a>

                                                    </div>
                                                </li>
                                                <!-- end notification: general -->
                                            </ul>
                                        </div>
                                       
                                        <!-- logged user and the menu -->
                                        <div class="logged-user">
                                            <div class="btn-group">
                                                <a href="#" class="btn btn-link dropdown-toggle" data-toggle="dropdown">
                                                    <img src="../Content/assets/ico/favicon.png" alt="User Avatar" />
                                                    <span class="name">
                                                        <asp:Label ID="lblProfileName" runat="server" Text=""></asp:Label></span> <span class="caret"></span>
                                                </a>
                                                <ul class="dropdown-menu" role="menu">
                                                    <li>
                                                        <a href="#">
                                                            <i class="fa fa-user"></i>
                                                            <span class="text">Profile</span>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a href="#">
                                                            <i class="fa fa-cog"></i>
                                                            <span class="text">Settings</span>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <i class="fa fa-power-off"></i>
                                                        <span>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Logout.aspx" CssClass="text" ForeColor="Black" Font-Underline="false" Font-Size="Small">Logout</asp:HyperLink></span>
                                                       
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <!-- end logged user and the menu -->
                                    </div>
                                    <!-- /top-bar-right -->
                                </div>
                            </div>
                            <!-- /row -->
                        </div>
                    </div>
                    <!-- /row -->
                </div>
                <!-- /container -->
            </div>
            <!-- /top -->

            <!-- BOTTOM: LEFT NAV AND RIGHT MAIN CONTENT -->
            <div class="bottom">
                <div class="container">
                    <div class="row">
                        <!-- left sidebar -->
                        <div class="col-md-2 left-sidebar">
                            <!-- main-nav -->
                            <nav class="main-nav">
                                <ul class="main-menu">
                                    <li class="active">
                                      
                                    </li>
                                    <li>
                                        <a href="#" class="js-sub-menu-toggle">
                                            <i class="fa fa-bars fa-fw"></i>
                                            <span class="text"><b>CDMA M
                                            <i class="toggle-icon fa fa-angle-down"></i></a>

                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <ul class="sub-menu active open">
                                                    <li><a href="<%# Eval("URL") %>"><%# Eval("MenuDesc") %></a></li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </li>
                                </ul>
                            </nav>
                            <!-- /main-nav -->
                            <div class="sidebar-minified js-toggle-minified">
                                <i class="fa fa-angle-left"></i>
                            </div>

                        </div>
                        <!-- end left sidebar -->
                        <!-- top general alert -->
                        <div class="alert alert-danger top-general-alert">
                            <span>If you  <span>If you <strong>can't see the logo</strong> on the top left, please reset the style on right style switcher (for upgraded theme only).</span>
                                <button type="button" class="close">&times;</button>
                        </div>
                        <!-- end top general alert -->

                        <!-- content-wrapper -->
                        <div class="col-md-10 content-wrapper">
                            <div class="row">
                                <div class="col-lg-4 ">
                                    <ul class="breadcrumb">
                                        <li><i class="fa fa-home"></i><a href="#">Home</a></li>
                                        <li><a href="#">DQI</a></li>
                                        <li class="active">DQ WIzard</li>
                                    </ul>
                                </div>
                                <div class="col-lg-8 ">
                                    <div class="top-content">
                                        <div class="top-content">
                                            <ul class="list-inline mini-stat">
                                                <li>
                                                    <h5>CURRENT USERS<span class="stat-value stat-color-orange"><i class="fa fa-plus-circle"></i><asp:Label ID="lblCurrentUsers" runat="server" Text="Label"></asp:Label></span></h5>
                                                    <span id="mini-bar-chart1" class="mini-bar-chart"></span>
                                                </li>
                                                <li>
                                                    <h5>DATA QUALITY <span class="stat-value stat-color-blue"><i class="fa fa-plus-circle"></i>
                                                        <asp:Label ID="lblDataQuality" runat="server" Text="Label"></asp:Label>%</span></h5>
                                                    <span id="mini-bar-chart2" class="mini-bar-chart"></span>
                                                </li>
                                                <li>
                                                    <h5>CUSTOMERS <span class="stat-value stat-color-seagreen"><i class="fa fa-plus-circle"></i>
                                                        <asp:Label ID="lblNoOfCustomers" runat="server" Text="Label"></asp:Label></span></h5>
                                                    <span id="mini-bar-chart3" class="mini-bar-chart"></span>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- main -->
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
                                            <div class="wizard-wrapper">
                                                <div id="demo-wizard" class="wizard">
                                                    <ul class="steps">
                                                        <li data-target="#step1" class="active"><span class="badge badge-info">1</span>Data Catalog<span class="chevron"></span></li>
                                                        <li data-target="#step2"><span class="badge">2</span>User Account<span class="chevron"></span></li>
                                                        <li data-target="#step3"><span class="badge">3</span>Options<span class="chevron"></span></li>
                                                        <li data-target="#step4" class="last"><span class="badge">4</span>Create Account</li>
                                                    </ul>
                                                </div>
                                                <div class="step-content">
                                                    <div class="step-pane active" id="step1">
                                                        <p>Choose your account type:</p>
                                                        <div class="widget-content">
                                                            <table class="nav-justified">
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <asp:Label ID="lblmsgs" runat="server"></asp:Label></td>

                                                                </tr>
                                                            </table>
                                                            <telerik:radajaxmanager id="RadAjaxManager1" runat="server">
                                                                <AjaxSettings>
                                                                    <telerik:AjaxSetting AjaxControlID="btnSave">
                                                                        <UpdatedControls>
                                                                            <telerik:AjaxUpdatedControl ControlID="lblmsgs" UpdatePanelCssClass="" />
                                                                            <telerik:AjaxUpdatedControl ControlID="gridCat" UpdatePanelCssClass="" />
                                                                        </UpdatedControls>
                                                                    </telerik:AjaxSetting>
                                                                </AjaxSettings>
                                                            </telerik:radajaxmanager>
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
                                                                        <telerik:radlistbox rendermode="Lightweight" runat="server" id="RadListBoxSource" height="200px" width="300px"
                                                                            autopostback="true"
                                                                            buttonsettings-areawidth="35px" datasourceid="dsTabs" datakeyfield="TABLE_NAME" datavaluefield="TABLE_NAME" datatextfield="TABLE_NAME" onselectedindexchanged="RadListBoxSource_SelectedIndexChanged">
                                                                        </telerik:radlistbox>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:radlistbox rendermode="Lightweight" runat="server" id="RadListBoxDestination" height="200px"
                                                                            datasourceid="dsCols" datakeyfield="column_id" datavaluefield="column_id" datatextfield="column_name"
                                                                            checkboxes="true" showcheckall="true" width="300">
                                                                        </telerik:radlistbox>
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
                                                                                        <telerik:radgrid id="gridCat" rendermode="Lightweight" runat="server" allowpaging="True" datasourceid="dsCat" allowsorting="True"
                                                                                            pagesize="5" autogeneratecolumns="False">
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
                                                                                        </telerik:radgrid>

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
                                                        <p id="error-step1"></p>
                                                    </div>
                                                    <div class="step-pane" id="step2">
                                                        <p>
                                                            Please provide email, username and password
														
                                                            <br />
                                                            <em><small>Field marked * is required</small></em>
                                                        </p>
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label for="email">Email *</label>
                                                                    <input type="email" id="email" class="form-control" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="password">Password *</label>
                                                                    <input type="password" id="password" name="password" class="form-control" required>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="password2">Repeat Password *</label>
                                                                    <input type="password" id="password2" name="password2" class="form-control" required data-parsley-equalto="#password" data-parsley-equalto-message="Password doesn't match.">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="step-pane" id="step3">
                                                        <label class="fancy-checkbox">
                                                            <input type="checkbox" name="newsletter">
                                                            <span>Subscribe to monthly newsletter</span>
                                                        </label>
                                                        <label class="fancy-checkbox">
                                                            <input type="checkbox" name="terms">
                                                            <span>I accept the <a href="#">Terms &amp; Agreements</a></span>
                                                        </label>
                                                    </div>
                                                    <div class="step-pane" id="step4">
                                                        <p class="lead"><i class="fa fa-check-circle text-success"></i>All is well! Click "Create My Account" to complete.</p>
                                                    </div>
                                                </div>
                                                <div class="actions">
                                                    <button type="button" class="btn btn-default btn-prev"><i class="fa fa-arrow-left"></i>Prev</button>
                                                    <button type="button" class="btn btn-primary btn-next">Next <i class="fa fa-arrow-right"></i></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- END WIDGET WIZARD -->

                                </div>
                                <!-- /main-content -->
                            </div>
                            <!-- /main -->
                        </div>
                        <!-- /content-wrapper -->
                    </div>
                    <!-- /row -->
                </div>
                <!-- /container -->
            </div>
            <!-- END BOTTOM: LEFT NAV AND RIGHT MAIN CONTENT -->
        </div>
        <!-- /wrapper -->

        


        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <!-- Javascript -->
        <script src="/Content/assets/js/jquery/jquery-2.1.0.min.js"></script>
        <script src="/Content/assets/js/bootstrap/bootstrap.js"></script>
        <script src="/Content/assets/js/plugins/modernizr/modernizr.js"></script>
        <script src="/Content/assets/js/plugins/bootstrap-tour/bootstrap-tour.custom.js"></script>
        <script src="/Content/assets/js/king-common.js"></script>
        <script src="/Content/assets/js/plugins/wizard/wizard.min.js"></script>
        <script src="/Content/assets/js/plugins/parsley-validation/parsley.min.js"></script>
        <script src="/Content/assets/js/king-components.js"></script>
            </telerik:RadScriptBlock>

    </form>

    <!-- FOOTER -->
        <!-- FOOTER -->
        <footer class="footer">

            <p>&copy; <%: DateTime.Now.Year %> - Bluechip Technologies Limited.</p>

        </footer>
        <!-- END FOOTER -->
</body>

</html>


