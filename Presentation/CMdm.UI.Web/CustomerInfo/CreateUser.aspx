<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="Cdma.Web.CustomerInfo.CreateUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Create User</h3>
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
                            <td class="col-sm-3 control-label" style="width: 144px">*User Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>

                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Password</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Height="40px" TextMode="Password"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Confirm Password</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtConfirmPasswd" runat="server" CssClass="form-control" Height="40px" TextMode="Password"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Email</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*User Role</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlRoleName" runat="server" CssClass="form-control" >
                                </asp:DropDownList>

                                
                            </td>
                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*First Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtfName" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Last Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtlName" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
            <asp:Button ID="btnCreateUser" runat="server" Text="Create User" CssClass="btn btn-primary" OnClick="btnCreateUser_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
           
            </div>
        </div>
</asp:Content>
