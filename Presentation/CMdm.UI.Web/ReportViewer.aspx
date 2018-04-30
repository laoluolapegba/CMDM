<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="CMdm.UI.Web.ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
         <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Home">Go Home</asp:HyperLink>
    <div>
        <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" OnLoad="rptViewer_Load" ></rsweb:ReportViewer>
    </div>
    <script type="text/javascript">
    $(document).ready(function () {
        hideExportOptions();
    });

    function hideExportOptions() {
        $("a[title='Word']").parent().hide();  // Remove from export dropdown.
        $("a[title='Excel']").parent().hide();
      
    }

</script> 
       
    </form>
    </body>
</html>
