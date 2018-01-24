<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BulkIndividualCustInfo.aspx.cs" Inherits="Cdma.Web.CustomerInfo.BulkIndividualCustInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="widget">
        <div class="widget-header">
            <h3>Bulk Individual Customer Upload</h3>
        </div>
        <div class="widget-content">
            <%--<table class="nav-justified">
                <tr>
                    <td colspan="5"><asp:Label ID="lblstatus" runat="server"></asp:Label></td>
               
                </tr>
                </table>--%>
           
            <table class="nav-justified">
                <tr>
                            <td colspan="4">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Upload File</td>
                            <td style="width: 285px" class="datepicker-inline">

                                <asp:FileUpload ID="fUpload" runat="server"  CssClass="form-control"/>

                            </td>

                            <td>Select a file (.xls, .xlsx)</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                    <td class="datepicker-inline" style="width: 285px">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="datepicker-inline" style="width: 285px">
            <asp:Button ID="btnBulkCustInfoUpload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnBulkCustInfoUpload_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
           
            </div>
        </div>
</asp:Content>
