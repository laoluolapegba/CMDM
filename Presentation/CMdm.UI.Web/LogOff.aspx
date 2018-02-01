<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOff.aspx.cs" Inherits="CMdm.UI.Web.LogOff" %>

<%@ Import Namespace="CMdm.UI.Web.Helpers.CrossCutting.Security" %>
<%@ Import Namespace="System.Security" %>
<%@ Import Namespace="CMdm.Data.Rbac" %>
<%@ Import Namespace="Microsoft.AspNet.Identity" %>
<%@ Import Namespace="CMdm.Data" %>
<%@ Import Namespace="CMdm.UI.Web.Extensions" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Views/Shared/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Bluespace MDM</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">


    <asp:PlaceHolder runat="server">
        <%:Styles.Render("~/AdminLTE/plugins/font-awesome/css") %>
        <%:Styles.Render("~/AdminLTE/plugins/ionicons/css") %>
        <%:Styles.Render("~/AdminLTE/bootstrap/css") %>
        <%:Styles.Render("~/AdminLTE/dist/css") %>
        <%:Styles.Render("~/AdminLTE/dist/css/skins") %>
    </asp:PlaceHolder>

    <style>
        .main-header .logo i {
            width: 50px;
            height: 50px;
            float: left;
            background: url(../../Content/images/logo-48x48.png) no-repeat center center;
            animation: fa-spin 5s infinite linear reverse;
        }
    </style>

</head>
<body class="hold-transition skin-blue sidebar-mini">
    <form runat="server">
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>
        <h2>...You have successfully logged out.</h2>
    <p>Please click
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx">here</asp:HyperLink>
&nbsp;to log in again.</p>
    </form>

    <asp:PlaceHolder runat="server">
        <%:Scripts.Render("~/AdminLTE/plugins/jquery/js") %>
        <%:Scripts.Render("~/AdminLTE/bootstrap/js") %>
        <%:Scripts.Render("~/AdminLTE/plugins/slimscroll/js") %>
        <%:Scripts.Render("~/AdminLTE/plugins/fastclick/js") %>
        <%:Scripts.Render("~/Scripts/Shared/_Layout") %>
        <%:Scripts.Render("~/AdminLTE/dist/js") %>     
    </asp:PlaceHolder>

</body>
</html>
