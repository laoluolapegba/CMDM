<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BulkUploadDetails.aspx.cs" Inherits="Cdma.Web.CustomerInfo.BulkUploadDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget widget-table">
        <div>


            <asp:Label ID="lblMsg" runat="server"></asp:Label>


        </div>
											<div class="widget-header">
												<h3><i class="fa fa-table"></i>Bulk Upload Details</h3>
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                


                                                         <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderWidth="0px" CellPadding="0"
                    CssClass="table" GridLines="None"  >
                                                         </asp:GridView>
                


            </div>
                                                    <div>
                                                        <asp:Button ID="btnBack" runat="server" Text="<< Back" CssClass="btn btn-primary" OnClick="btnBack_Click" />
                                                        
                                                    </div>
												</div>
											</div>
										</div>
</asp:Content>
