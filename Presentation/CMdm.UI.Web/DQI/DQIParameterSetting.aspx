<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DQIParameterSetting.aspx.cs" Inherits="Cdma.Web.DQI.DQIParameterSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            var tab = document.getElementById('<%= hidTAB.ClientID%>').value;
            $('#myTabs a[href="' + tab + '"]').tab('show');
        });
</script>


    <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist" id="myTabs">
        <li id="DQIParam" runat="server" ><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-columns"></i>DQI Parameter Setting</a></li>
        <li id="Weight" runat="server"><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-archive"></i>Weight Setting</a></li>
        <%--<li id="DQIParama" runat="server"><a href="#tab3" role="tab" data-toggle="tab" aria-expanded="true"><i class="fa fa-support"></i>Metric Setting</a></li>
        <li id="xxxx" runat="server"><a href="#tab4" role="tab" data-toggle="tab" aria-expanded="true"><i class="fa fa-unlock"></i>XXXXX</a></li>--%>
  
    </ul>

   <%-- <ul class="nav nav-tabs" id="myTabs">
  <li><a href="#tab1" data-toggle="tab">Home page</a></li>
  <li><a href="#tab2" data-toggle="tab">another page</a></li>
</ul>--%>


    

    <div class="tab-content">
        <div class="tab-pane fade   active in" id="tab1">
            <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Data Quality Index Parameter Setting</h3>
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
                                <asp:Label ID="lblDQIParamMsg" runat="server"></asp:Label>
                            </td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Catalog:</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="DDLTblCat" runat="server" CssClass="form-control" Widht="50px"  AutoPostBack="True" onselectedindexchanged="ddlDQITblCat_SelectedIndexChanged">
                                </asp:DropDownList>

                            </td>

                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">

                                &nbsp;</td>
                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>



<td class="col-sm-3 control-label" style="top: 96px; left: 0px;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;
                        						<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">

          <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderWidth="0px" CellPadding="0" CssClass="table" GridLines="None"
           EmptyDataText="No Record(s) found!" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" EnableViewState="false">

                        <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                        <Columns>
                            <asp:TemplateField HeaderText="S/N">
                                <ItemTemplate>
                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="COLUMNID" Visible="false">
                            </asp:BoundField>--%>
                            <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:label ID="lblColID" Text='<%# Convert.ToInt16 (Eval("COLUMNID")) %>'  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:BoundField DataField="COLUMN_DESC" HeaderText="ATTRIBUTE"  >
                            <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                                </asp:BoundField>
                            <%--<asp:BoundField DataField="IMPORTANCE_LEVEL" HeaderText="IMPORTANCE LEVEL" ItemStyle-HorizontalAlign = "Center" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                               <%-- <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                         
                        <asp:TemplateField HeaderText="REQUIRED?">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRequired"  runat="server" Checked ='<%# Convert.ToBoolean (Eval("COLMN_REQUIRED")) %>' ItemStyle-HorizontalAlign = "Center" />
                            </ItemTemplate>
                        </asp:TemplateField>

        <asp:TemplateField HeaderText="WEIGHT">
            <ItemTemplate>
                <asp:DropDownList ID="ddlCWeight" runat="server" SelectedValue='<%# Eval("COLUMN_WEIGHT") %>' DataSourceID="DSCWeight" DataTextField="WEIGHT_DESC" DataValueField="WEIGHT_VALUE" Width="120" ItemStyle-HorizontalAlign = "Center">
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>

      <asp:TemplateField HeaderText="REGULAR EXPRESSION" >
            <ItemTemplate>
                <asp:DropDownList ID="ddlRegex" runat="server" SelectedValue='<%# Eval("REGEX") %>' DataSourceID="DSRegex" DataTextField="REGEX_NAME" DataValueField="REGEX_ID" Width="180" ItemStyle-HorizontalAlign = "Center">
                </asp:DropDownList>
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
											</td>


                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hidTblName" runat="server" />
                        <%--//<asp:HiddenField ID="hidTAB" runat="server" Value="Users" />--%>
                            <asp:HiddenField ID="hidTAB" runat="server" Value="#tab1" />
                    </td>
                    <td>
            <asp:Button ID="btnUpdateDQIParam" runat="server" Text="Update Parameter" CssClass="btn btn-primary" OnClick="btnUpdateDQIParam_Click" />
                        &nbsp;</td>
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
                        <asp:SqlDataSource ID="DSCWeight" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>" 
                            ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                            SelectCommand="SELECT &quot;WEIGHT_ID&quot;, &quot;WEIGHT_VALUE&quot;, &quot;WEIGHT_DESC&quot; FROM &quot;MDM_WEIGHTS&quot; ORDER BY &quot;WEIGHT_VALUE&quot;">

                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="DSRegex" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>" 
                            ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                            SelectCommand="SELECT &quot;REGEX_ID&quot;, &quot;REGEX_NAME&quot;, &quot;REGEX_DESC&quot; FROM &quot;MDM_REGEX&quot; ORDER BY &quot;REGEX_NAME&quot;"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
           
            </div>
        </div>

            </div>
        <div class="tab-pane fade" id="tab2">
            <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Weight Setup</h3>
        </div>
        <div class="widget-content">
            <table class="nav-justified">
                <tr>
                    <td colspan="5"><asp:Label ID="lblWeightstatus" runat="server"></asp:Label></td>
               
                </tr>
                </table>
           
            <table class="nav-justified">
                <tr>
                            <td colspan="4">
                                <asp:Label ID="lblWeightMsg" runat="server"></asp:Label>
                            </td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Weight Value</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtWeightValue" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>

                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    
                </tr>
                <tr>
                    <td class="col-sm-3 control-label" style="width: 144px">*Weight Description</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtWeightDesc" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

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
            <asp:Button ID="btnSaveWeight" runat="server" Text="Save Weight" CssClass="btn btn-primary" OnClick="btnSaveWeight_Click" />
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
												<h3><i class="fa fa-table"></i> View Weight Values</h3>
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0" OnPageIndexChanging="GridView2_PageIndexChanging" 
                    CssClass="table" DataSourceID="SqlDataSource1" GridLines="None"  PageSize="5" DataKeyNames="WEIGHT_ID" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" EmptyDataText="No Record(s) found!">
                    <AlternatingRowStyle BackColor="White" CssClass="rptitem" /><%--OnDataBinding="OnGridDataBinding"
                    OnDataBound="OnGridDataBound" OnPageIndexChanging="OnGridPageIndexChanging" OnSelectedIndexChanged="OnGridSelectedIndexChanged"
                    OnSorted="OnGridSorted" OnSorting="OnGridSorting"--%>
                    <Columns>
                        <asp:BoundField DataField="WEIGHT_VALUE" HeaderText="WEIGHT VALUE" SortExpression="WEIGHT_VALUE" />
                        <asp:BoundField DataField="WEIGHT_DESC" HeaderText="WEIGHT DESCRIPTION" SortExpression="WEIGHT_DESC" />
                        <asp:BoundField DataField="WEIGHT_ID" HeaderText="WEIGHT_ID" ReadOnly="True" SortExpression="WEIGHT_ID" Visible="false"/>
                        
                    </Columns>
                    <HeaderStyle BackColor="White" BorderColor="White" CssClass="rptheader" Font-Bold="True"
                        ForeColor="Black" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle CssClass="pagerheader" />
                    <RowStyle BackColor="#E9F1F6" CssClass="rptitem" ForeColor="#4A3C8C" HorizontalAlign="Left" Wrap="False" />
                    <SelectedRowStyle BackColor="#738A9C" CssClass="rptitem" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                    SelectCommand="SELECT &quot;WEIGHT_ID&quot;, &quot;WEIGHT_VALUE&quot;, &quot;WEIGHT_DESC&quot; FROM &quot;MDM_WEIGHTS&quot;" 
                    DeleteCommand="DELETE FROM &quot;MDM_WEIGHTS&quot; WHERE &quot;WEIGHT_ID&quot; = ?" 
                    InsertCommand="INSERT INTO &quot;MDM_WEIGHTS&quot; (&quot;WEIGHT_ID&quot;, &quot;WEIGHT_VALUE&quot;) VALUES (?, ?)" 
                    OldValuesParameterFormatString="original_{0}" 
                    UpdateCommand="UPDATE &quot;MDM_WEIGHTS&quot; SET &quot;WEIGHT_VALUE&quot; = ? WHERE &quot;WEIGHT_ID&quot; = ?">
                    <DeleteParameters>
                        <asp:Parameter Name="original_WEIGHT_ID" Type="Decimal" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="WEIGHT_ID" Type="Decimal" />
                        <asp:Parameter Name="WEIGHT_VALUE" Type="Decimal" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="WEIGHT_VALUE" Type="Decimal" />
                        <asp:Parameter Name="original_WEIGHT_ID" Type="Decimal" />
                    </UpdateParameters>
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
            </div>
        <%--<div class="tab-pane fade" id="tab3">
            <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Metric Setting</h3>
        </div>
        <div class="widget-content">
            <table class="nav-justified">
                <tr>
                    <td colspan="5"><asp:Label ID="lblstatus" runat="server"></asp:Label></td>
               
                </tr>
                </table>
           
            <table class="nav-justified">
                <tr>
                            <td colspan="4">
                                <asp:Label ID="lblPermsnMsg" runat="server"></asp:Label>
                            </td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Permission Description</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtPermDesc" runat="server" CssClass="form-control" Height="40px" MaxLength="99"></asp:TextBox>

                            </td>

                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Form URL</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtFormURL" runat="server" CssClass="form-control" Height="40px" MaxLength="99"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Action</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtAction" runat="server" CssClass="form-control" Height="40px" MaxLength="24"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Controller</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtController" runat="server" CssClass="form-control" Height="40px" MaxLength="24"></asp:TextBox>

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
            <asp:Button ID="btnSaveMetrics" runat="server" Text="Save Weight" CssClass="btn btn-primary" OnClick="btnSaveMetrics_Click" />
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
                    <td colspan="4"><div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View Premissions</h3>
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0"
                    CssClass="table" GridLines="None"  PageSize="10"  
                    OnClientClick="javascript:return confirm('Are you sure you want to delete this record?')" EmptyDataText="No Record(s) found!">
                    <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                    <Columns>
                        <asp:TemplateField>
                        <HeaderTemplate>
                                EDIT
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("PermID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;"  >
                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                        <HeaderTemplate>
                                DELETE
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" RecId='<%#Eval("PermID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline; " OnClick="OnDelete_Permission">
                                        <asp:Image ID="Image1" ImageUrl="../Content/assets/img/ico-delete.png" runat="server" />
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PermID" HeaderText="PermID" SortExpression="PermID" Visible="false" />
                        <asp:BoundField DataField="Desc" HeaderText="Desc" SortExpression="Desc" />
                        <asp:BoundField DataField="FormURL" HeaderText="Form URL" SortExpression="FormURL" />
                        <asp:BoundField DataField="Controller" HeaderText="Controller" SortExpression="Controller" />
                        <asp:BoundField DataField="Action" HeaderText="Action" SortExpression="Action" />
                        
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
										</div>	</td>
                </tr>
            </table>
           
            </div>
        </div>
            </div>--%>
        <%--<div class="tab-pane fade" id="tab4">
            <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>yyy</h3>
        </div>
        <div class="widget-content">
            <table class="nav-justified">
                <tr>
                    <td colspan="5"><asp:Label ID="lblstatus" runat="server"></asp:Label></td>
               
                </tr>
                </table>
           
            <table class="nav-justified">
                <tr>
                            <td colspan="4">
                                <asp:Label ID="lblAssgnPermsnMsg" runat="server"></asp:Label>
                            </td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Role</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlRoleforPerm" runat="server" CssClass="form-control" >
                                </asp:DropDownList>

                                
                            </td>

                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Permission</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlPermforPerm" runat="server" CssClass="form-control" >
                                </asp:DropDownList>

                                
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
            <asp:Button ID="btnAssgnPerm" runat="server" Text="Assign Permission" CssClass="btn btn-primary" OnClick="btnAssignPermission_Click" />
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
                    <td colspan="4"><div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View Assigned Permissions</h3>
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0"
                    CssClass="table" GridLines="None"  PageSize="15" OnPageIndexChanging="GridView4_PageIndexChanging"  
                    OnClientClick="javascript:return confirm('Are you sure you want to delete this record?')" EmptyDataText="No Record(s) found!">
                    <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                    <Columns>
                        <asp:TemplateField>
                        <HeaderTemplate>
                                EDIT
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("recordID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;"  >
                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                        <HeaderTemplate>
                                DELETE
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" RecId='<%#Eval("recordID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnDelete_AssignedPermission" >
                                        <asp:Image ID="Image1" ImageUrl="../Content/assets/img/ico-delete.png" runat="server" />
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="recordID" HeaderText="recordID" SortExpression="recordID" Visible="false" />
                        <asp:BoundField DataField="Role" HeaderText="ROLE NAME" SortExpression="Role" />
                        <asp:BoundField DataField="Permission" HeaderText="PERMISSION" SortExpression="Permission" />
                        <asp:BoundField DataField="Createdby" HeaderText="CREATED BY" SortExpression="Createdby" />
                        <asp:BoundField DataField="DateCreated" HeaderText="DATE CREATED" SortExpression="DateCreated" />
                       
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
										</div>	</td>
                </tr>
            </table>
           
            </div>
        </div>
            </div>--%>
    </div>



</asp:Content>
