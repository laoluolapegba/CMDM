<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateRoles.aspx.cs" Inherits="Cdma.Web.CustomerInfo.CreateRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Create Role</h3>
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
                            <td class="col-sm-3 control-label" style="width: 144px">*Role Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtRole" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

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
            <asp:Button ID="btnCreateRole" runat="server" Text="Create Role" CssClass="btn btn-primary" OnClick="btnCreateRole_Click" />
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
                    <td colspan="4">
            <div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View Roles</h3>
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0"
                    CssClass="gridstyle" DataSourceID="SqlDataSource1" GridLines="None"  PageSize="5" DataKeyNames="ROLE_ID">
                    <AlternatingRowStyle BackColor="White" CssClass="rptitem" /><%--OnDataBinding="OnGridDataBinding"
                    OnDataBound="OnGridDataBound" OnPageIndexChanging="OnGridPageIndexChanging" OnSelectedIndexChanged="OnGridSelectedIndexChanged"
                    OnSorted="OnGridSorted" OnSorting="OnGridSorting"--%>
                    <Columns>
                        <asp:BoundField DataField="ROLE_ID" HeaderText="ROLE_ID" SortExpression="ROLE_ID"
                            ReadOnly="True" />
                        <asp:BoundField DataField="ROLE_NAME" HeaderText="ROLE_NAME" SortExpression="ROLE_NAME" />
                        
                    </Columns>
                    <HeaderStyle BackColor="White" BorderColor="White" CssClass="rptheader" Font-Bold="True"
                        ForeColor="Black" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle CssClass="pagerheader" />
                    <RowStyle BackColor="#E9F1F6" CssClass="rptitem" ForeColor="#4A3C8C" HorizontalAlign="Left" Wrap="False" />
                    <SelectedRowStyle BackColor="#738A9C" CssClass="rptitem" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;ROLE_ID&quot;, &quot;ROLE_NAME&quot; FROM &quot;CM_USER_ROLES&quot;">
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
</asp:Content>
