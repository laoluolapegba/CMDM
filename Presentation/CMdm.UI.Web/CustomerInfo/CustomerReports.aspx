<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerReports.aspx.cs" Inherits="Cdma.Web.CustomerInfo.CustomerReports" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <script type="text/javascript">
       $(document).ready(function () {
           var tab = document.getElementById('<%= hidTAB.ClientID%>').value;
           $('#myTabs a[href="' + tab + '"]').tab('show');
       });
</script>


    <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist" id="myTabs">
        <li id="Report1" runat="server" ><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-user"></i>Customer DQI Report Before BVN(by Branch)</a></li>
        <li id="Report2" runat="server"><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-binoculars"></i>Customer DQI Report after BVN(by Branch)</a></li>
        <li id="Report3" runat="server"><a href="#tab3" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-dashboard"></i>Customer DQI Report at Enterprise level</a></li>
      <%--  <li id="Report4" runat="server"><a href="#tab4" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-unlock"></i>Report4</a></li>
        <li id="Report5" runat="server" ><a href="#tab5" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-user"></i>Report5</a></li>
        <li id="Report6" runat="server"><a href="#tab6" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-cogs"></i>Report6</a></li>
        <li id="Report7" runat="server"><a href="#tab7" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-check-square-o"></i>Report7</a></li>--%>
    </ul>


   
   
    <div class="tab-content">
        <div class="tab-pane fade" id="tab1">
            <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Customer Report</h3>
        </div>
        <div class="widget-content">

    <table class="nav-justified">
        <tr>
            
            <td colspan="12">
            <div>    <asp:Label ID="lblMsg" runat="server"></asp:Label> </div>
            </td>

           
        </tr>
         <tr>
            <td colspan="12">
              <div>

                  <table class="nav-justified">
                      <tr>
                          <td class="col-sm-3 control-label" style="width: 93px">Category:</td>
                          <td style="width: 100px">
                              <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" Width="200px">
                                  <asp:ListItem>INDIVIDUAL</asp:ListItem>
                                  <asp:ListItem>CORPORATE</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                          <td class="col-sm-3 control-label" style="width: 59px">Branch:</td>
                          <td class="modal-sm" style="width: 331px">
                              <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" Width="200px">
                              </asp:DropDownList>
                          </td>
                          <td>
                              <asp:Button ID="btnSearch" runat="server" Text="Submit" CssClass="btn btn-primary fa fa-search-plus" OnClick="btnSearch_Click" />
                              
                          </td>
                      </tr>
                  </table>

              </div>
            </td>
        </tr>
        
        <tr>
            <td><asp:HiddenField ID="hidTAB" runat="server" Value="#tab1" /></td>
            <td class="datepicker-inline" style="width: 285px" colspan="11">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="12">
            <div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View Completeness Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                 <asp:Button ID="btnIndivCompReportDownloadExcel" runat="server" Text="Export to Excel" CssClass="btn-custom-secondary" OnClick="btnDownloadExcelIndivCompReport_Click" />
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                 <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderWidth="0px" CellPadding="0"  CssClass="table" AllowPaging="True" AutoGenerateColumns="true" 
                     PageSize="20" GridLines="Both" OnPageIndexChanging="GridView1_PageIndexChanging" >
                  <AlternatingRowStyle BackColor="#99ccff" CssClass="rptitem" Wrap="False"/>
                  <HeaderStyle BackColor="White" BorderColor="Black" CssClass="rptheader" Font-Bold="True" BorderStyle="Outset" ForeColor="Black" Wrap="False"  />
                   <PagerSettings Position="TopAndBottom" />
                   </asp:GridView>

            </div>
												</div>
											</div>
										</div>			
            </td>
        </tr>
        <tr>
            <td colspan="12">
            <div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View Correctness Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                 <asp:Button ID="btnIndivCorrReportDownloadExcel" runat="server" Text="Export to Excel" CssClass="btn-custom-secondary" OnClick="btnDownloadExcelIndivCorrReport_Click" />
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
       <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderWidth="0px" CellPadding="0" AutoGenerateColumns="true" CssClass="table" GridLines="Both"
           AllowPaging="True"  PageSize="20" OnPageIndexChanging="GridView2_PageIndexChanging" >
                  <AlternatingRowStyle BackColor="#99ccff" CssClass="rptitem" Wrap="False"/>
             
                  <HeaderStyle BackColor="White" BorderColor="Black" CssClass="rptheader" Font-Bold="True" BorderStyle="Outset" ForeColor="Black" Wrap="False"  />
                   <PagerSettings Position="TopAndBottom" />
                   </asp:GridView>
                                                         
            </div>
												</div>
											</div>
										</div>			
            </td>
        </tr>
    </table>

       
            </div>
        </div>

            </div>
        <div class="tab-pane fade active in" id="tab2">
            <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Customer Report</h3>
        </div>
        <div class="widget-content">
            
           <table class="nav-justified">
    <tr>
            <td colspan="12">
              <div>

                  <table class="nav-justified">
                      <tr>
                          <td class="col-sm-3 control-label" style="width: 93px">Category:</td>
                          <td style="width: 100px">
                              <asp:DropDownList ID="ddlCat2" runat="server" CssClass="form-control" Width="200px">
                                  <asp:ListItem>INDIVIDUAL</asp:ListItem>
                                  <asp:ListItem>CORPORATE</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                          <td class="col-sm-3 control-label" style="width: 59px">Branch:</td>
                          <td class="modal-sm" style="width: 331px">
                              <asp:DropDownList ID="ddlbranch2" runat="server" CssClass="form-control" Width="200px">
                              </asp:DropDownList>
                          </td>
                          <td>
                              <asp:Button ID="Search2" runat="server" Text="Submit" CssClass="btn btn-primary fa fa-search-plus" OnClick="btnSearch2_Click" />
                              
                          </td>
                      </tr>
                  </table>

              </div>
            </td>
        </tr>
        
        <tr>
            <td>&nbsp;&nbsp;&nbsp;</td>
            
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="datepicker-inline" style="width: 285px" colspan="11">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="12">
            <div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View Completeness Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                 <asp:Button ID="btnCorpCompReportDownloadExcel" runat="server" Text="Export to Excel" CssClass="btn-custom-secondary" OnClick="btnDownloadExcelCorpCompReport_Click" />
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                


              <asp:GridView ID="GridView3" runat="server" BackColor="White" BorderWidth="0px" CellPadding="0"
                    CssClass="table" AllowPaging="True"  PageSize="20" OnPageIndexChanging="GridView3_PageIndexChanging" AutoGenerateColumns="true" GridLines="Both">


                 <AlternatingRowStyle BackColor="#99ccff" CssClass="rptitem" Wrap="False"/>
                  <%--  <Columns>
                     <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                      <asp:BoundField DataField="TABLE_CATEGORY" HeaderText="TABLE CATEGORY" SortExpression="TABLE_CATEGORY" />
                      <asp:BoundField DataField="TABLE_NAME" HeaderText="TABLE NAME" SortExpression="TABLE_NAME" />
                      <asp:BoundField DataField="COLUMN_NAME" HeaderText="COLUMN NAME" SortExpression="COLUMN_NAME" />
                      <asp:BoundField DataField="FAILED_PCT" HeaderText="FAILED PCT" SortExpression="FAILED_PCT" />
                  </Columns>--%>
                  <HeaderStyle BackColor="White" BorderColor="Black" CssClass="rptheader" Font-Bold="True" BorderStyle="Outset" ForeColor="Black" Wrap="False"  />
                                                         <PagerSettings Position="TopAndBottom" />


                                                         </asp:GridView>
                                                         <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>" ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;ID&quot;, &quot;TABLE_CATEGORY&quot;, &quot;TABLE_NAME&quot;, &quot;COLUMN_NAME&quot;, &quot;FAILED_PCT&quot; FROM &quot;DQI_PROFILE_RESULT&quot; WHERE (&quot;TABLE_CATEGORY&quot; = 'CORPORATE')"></asp:SqlDataSource>--%>
            </div>
												</div>
											</div>
										</div>			
            </td>
        </tr>
               <tr>
            <td colspan="12">
            <div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View Correctness Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                 <asp:Button ID="btnCorpCorrReportDownloadExcel" runat="server" Text="Export to Excel" CssClass="btn-custom-secondary" OnClick="btnDownloadExcelCorpCorrReport_Click" /> 
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                


              <asp:GridView ID="GridView4" runat="server" BackColor="White" BorderWidth="0px" CellPadding="0" 
                    CssClass="table" GridLines="Both" AllowPaging="True"  PageSize="20" OnPageIndexChanging="GridView4_PageIndexChanging" AutoGenerateColumns="true" >
                  <AlternatingRowStyle BackColor="#99ccff" CssClass="rptitem" Wrap="False"/>
                  <%--  <Columns>
                      <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                      <asp:BoundField DataField="TABLE_CATEGORY" HeaderText="TABLE CATEGORY" SortExpression="TABLE_CATEGORY" />
                      <asp:BoundField DataField="TABLE_NAME" HeaderText="TABLE NAME" SortExpression="TABLE_NAME" />
                      <asp:BoundField DataField="COLUMN_NAME" HeaderText="COLUMN NAME" SortExpression="COLUMN_NAME" />
                      <asp:BoundField DataField="FAILED_PCT" HeaderText="FAILED PCT" SortExpression="FAILED_PCT" />
                  </Columns>--%>
                  <HeaderStyle BackColor="White" BorderColor="Black" CssClass="rptheader" Font-Bold="True" BorderStyle="Outset" 
                        ForeColor="Black" Wrap="False"  />
                                                         <PagerSettings Position="TopAndBottom" />
                                                         </asp:GridView>
            </div>
												</div>
											</div>
										</div>			
            </td>
        </tr>
    </table>
            
           
            </div>
        </div>
            </div>
        <div class="tab-pane fade" id="tab3">
            <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Customer Information Report</h3>
        </div>
        <div class="widget-content">
           
           <table class="nav-justified">
        <tr>
            <td colspan="12">
                <asp:Label ID="lblCustInfoMsg" runat="server"></asp:Label>
            </td>
        </tr>
               <tr>
            <td>&nbsp;&nbsp;&nbsp;</td>
            
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="datepicker-inline" style="width: 285px" colspan="11">
                &nbsp;</td>
        </tr>
        
        
        
        <tr>
            <td colspan="12">
            <div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View DQI Report before BVN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                 <asp:Button ID="Button1" runat="server" Text="Export to Excel" CssClass="btn-custom-secondary" OnClick="btnDownloadExcelDQIBE4BVNReport_Click" /> 
											
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                


              <asp:GridView ID="GridView5" runat="server" BackColor="White" BorderWidth="0px" CellPadding="0" AutoGenerateColumns="true"
                    CssClass="table" GridLines="Both" AllowPaging="True"  PageSize="20" OnPageIndexChanging="GridView5_PageIndexChanging" >
                  <AlternatingRowStyle BackColor="#99ccff" CssClass="rptitem" Wrap="False"/>
                  <HeaderStyle BackColor="White" BorderColor="Black" CssClass="rptheader" Font-Bold="True" BorderStyle="Outset" 
                        ForeColor="Black" Wrap="False"  />
                                                         <PagerSettings Position="TopAndBottom" />
                                                         </asp:GridView>
            </div>
												</div>
											</div>
										</div>			
            </td>
        </tr>
               <tr>
            <td colspan="12">
            <div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View DQI Report after BVN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                 <asp:Button ID="Button2" runat="server" Text="Export to Excel" CssClass="btn-custom-secondary" OnClick="btnDownloadExcelDQIAfterBVNReport_Click" /> 
											
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                


              <asp:GridView ID="GridView6" runat="server" BackColor="White" BorderWidth="0px" CellPadding="0" AutoGenerateColumns="true"
                    CssClass="table" GridLines="Both" AllowPaging="True"  PageSize="20" OnPageIndexChanging="GridView6_PageIndexChanging">
                  <AlternatingRowStyle BackColor="#99ccff" CssClass="rptitem" Wrap="False"/>
                  <HeaderStyle BackColor="White" BorderColor="Black" CssClass="rptheader" Font-Bold="True" BorderStyle="Outset" 
                        ForeColor="Black" Wrap="False"  />
                                                         <PagerSettings Position="TopAndBottom" />
                                                         </asp:GridView>
            </div>
												</div>
											</div>
										</div>			
            </td>
        </tr>
    </table> 
           
            </div>
        </div>
            </div>
   </div>
</asp:Content>
