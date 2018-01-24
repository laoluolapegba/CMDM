<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuthBulkCorpCustInfo.aspx.cs" Inherits="Cdma.Web.CustomerInfo.AuthBulkCorpCustInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Authorize Bulk Corporate Customer Information</h3>
        </div>
        <div class="widget-content">
    <table class="nav-justified">
        <tr>
            <td colspan="4">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="col-sm-3 control-label" style="width: 144px">*Maker Names</td>
            <td style="width: 285px" class="datepicker-inline">
                
                <asp:DropDownList ID="DDLMakerList" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="datepicker-inline" style="width: 285px">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="datepicker-inline" style="width: 285px">
                <asp:Button ID="btnSearchMaker" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearchMaker_Click" />
            </td>
            <td>&nbsp;</td>
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
            <td style="height: 18px"></td>
            <td class="datepicker-inline" style="width: 285px; height: 18px;">
            </td>
            <td style="height: 18px"></td>
            <td style="height: 18px"></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="datepicker-inline" style="width: 285px">
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
            <div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View Corporate Customer Information</h3>
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0" 
                    CssClass="table" GridLines="None"  PageSize="20" DataKeyNames="CUSTOMER_ID" EmptyDataText="No Record(s) found!">
                    <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                    <Columns>
                        
                        <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER ID" SortExpression="CUSTOMER_ID"
                            ReadOnly="True" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER NAME" SortExpression="CUSTOMER_NAME" />
                        <asp:HyperLinkField DataNavigateUrlFields="CUSTOMER_ID" DataNavigateUrlFormatString="~/CustomerInfo/BulkUploadDetails.aspx?prc=prc_corporate_approval_url&CUSTOMER_ID={0}" 
                            HeaderText="VIEW DETAILS" Text="Details" SortExpression=""/>
                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                               APPROVE
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                    <asp:LinkButton ID="lnkApprove" runat="server" RecId='<%#Eval("CUSTOMER_ID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnApprove_Click" >
                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/approved.jpg"  Height="25px" Width="25px"  runat="server" />
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                DECLINE
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                    <asp:LinkButton ID="lnkDecline" runat="server" RecId='<%#Eval("CUSTOMER_ID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnDecline_Click" >
                                        <asp:Image ID="Image1" ImageUrl="../Content/assets/img/declined.jpg" Height="25px" Width="25px" runat="server" />
                                    </asp:LinkButton>
                                </div>
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
											</div>
										</div>			
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="datepicker-inline" style="width: 285px">
                <asp:Button ID="btnApproveBulkMaker" runat="server" Text="Approve All" CssClass="btn btn-primary" OnClick="btnApproveBulkMaker_Click" />
            &nbsp;<asp:Button ID="btnDeclineBulkMaker" runat="server" Text="Decline All" CssClass="btn btn-default" OnClick="btnDeclineBulkMaker_Click" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="datepicker-inline" style="width: 285px">
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
            </div>
        </div>
</asp:Content>
