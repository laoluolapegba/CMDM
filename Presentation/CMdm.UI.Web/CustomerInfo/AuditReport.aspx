<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuditReport.aspx.cs" Inherits="Cdma.Web.CustomerInfo.AuditReport" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    
            <div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i>Audit Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h3>
                                                
                                                <asp:Button ID="btnDownloadExcel" runat="server" Text="Export to Excel" CssClass="btn-custom-secondary" OnClick="btnDownloadExcelAudittrail_Click" />
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0"
                    CssClass="table" GridLines="None"  PageSize="20" EmptyDataText="No Record(s) found!"
                   DataSourceID="SqlDataSource2">
                    <AlternatingRowStyle BackColor="White" CssClass="rptitem" />

                    <Columns>

                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                        <asp:BoundField DataField="USER_NAME" HeaderText="USER NAME" SortExpression="USER_NAME" />
                        <asp:BoundField DataField="RECORD_MAINTENANCE_DATE" HeaderText="RECORD MAINTENANCE DATE" SortExpression="RECORD_MAINTENANCE_DATE" />
                        
                        <asp:BoundField DataField="ACTIVITY_TYPE" HeaderText="ACTIVITY PERFORMED" SortExpression="ACTIVITY_TYPE" />
                        <asp:BoundField DataField="TABLE_NAME" HeaderText="TABLE_NAME" SortExpression="TABLE_NAME" />
                        <asp:BoundField DataField="CUSTOMER_NO_MAINTAINED" HeaderText="CUSTOMER NO" SortExpression="CUSTOMER_NO_MAINTAINED" />
                        <asp:BoundField DataField="IP_ADDRESS" HeaderText="IP ADDRESS" SortExpression="IP_ADDRESS" />
                        
                    </Columns>
                    <HeaderStyle BackColor="White" BorderColor="White" CssClass="rptheader" Font-Bold="True"
                        ForeColor="Black" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle CssClass="pagerheader" />
                    <RowStyle BackColor="#E9F1F6" CssClass="rptitem" ForeColor="#4A3C8C" HorizontalAlign="Left" Wrap="False" />
                    <SelectedRowStyle BackColor="#738A9C" CssClass="rptitem" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                    SelectCommand="SELECT &quot;ID&quot;, &quot;USER_NAME&quot;, &quot;RECORD_MAINTENANCE_DATE&quot;, &quot;ACTIVITY_TYPE&quot;, &quot;TABLE_NAME&quot;, &quot;CUSTOMER_NO_MAINTAINED&quot;, &quot;IP_ADDRESS&quot; FROM &quot;CDMA_AUDIT_TRAIL&quot; order by &quot;RECORD_MAINTENANCE_DATE&quot; desc">
                   
                </asp:SqlDataSource>
            </div>
												</div>
											</div>
										</div>			
                    </asp:Content>
