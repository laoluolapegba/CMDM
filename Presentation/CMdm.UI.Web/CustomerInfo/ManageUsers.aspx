<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="Cdma.Web.CustomerInfo.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
        var tab = document.getElementById('<%= hidTAB.ClientID%>').value;
            $('#myTabs a[href="' + tab + '"]').tab('show');
        });
</script>


    <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist" id="myTabs">
        <li id="Usrs" runat="server" ><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-user"></i>Users</a></li>
        <li id="Rols" runat="server"><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-cogs"></i>Roles</a></li>
        <%--<li id="Permssn" runat="server"><a href="#tab3" role="tab" data-toggle="tab" aria-expanded="true"><i class="fa fa-check-square-o"></i>Permissions</a></li> uncomment me whenever u want to create new page--%>
        <li id="AssignPermssns" runat="server"><a href="#tab4" role="tab" data-toggle="tab" aria-expanded="true"><i class="fa fa-unlock"></i>Assign Permissions</a></li>
        <li id="AssignUsrs" runat="server"><a href="#tab5" role="tab" data-toggle="tab" aria-expanded="true"><i class="fa fa-unlock"></i>Assign Users</a></li>
  
    </ul>

   <%--<asp:TemplateField>
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
                        </asp:TemplateField>--%>


    

    <div class="tab-content">
        <div class="tab-pane fade  active in" id="tab1">
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
                                <asp:Label ID="lblUsrMsg" runat="server"></asp:Label>
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

                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Height="40px" TextMode="Password" ReadOnly="true" Text="password"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
<%--                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Confirm Password</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtConfirmPasswd" runat="server" CssClass="form-control" Height="40px" TextMode="Password"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>--%>
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
                    <td>
                        <asp:HiddenField ID="hidUerID" runat="server" />
                        <%--//<asp:HiddenField ID="hidTAB" runat="server" Value="Users" />--%>
                            <asp:HiddenField ID="hidTAB" runat="server" Value="#tab1" />
                    </td>
                    <td>
            <asp:Button ID="btnCreateUser" runat="server" Text="Create User" CssClass="btn btn-primary" OnClick="btnCreateUser_Click" />
                        <asp:Button ID="btnUpdateUser" runat="server" Text="Update User" CssClass="btn btn-default" OnClick="btnUpdateUser_Click" />
              
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
												<h3><i class="fa fa-table"></i> View Users</h3>
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0"
                    CssClass="table" GridLines="None"  PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" 
                    OnClientClick="javascript:return confirm('Are you sure you want to delete this record?')" EmptyDataText="No Record(s) found!">
                    <AlternatingRowStyle BackColor="White" CssClass="rptitem" /><%--OnDataBinding="OnGridDataBinding"
                    OnDataBound="OnGridDataBound" OnPageIndexChanging="OnGridPageIndexChanging" OnSelectedIndexChanged="OnGridSelectedIndexChanged"
                    OnSorted="OnGridSorted" OnSorting="OnGridSorting"--%>
                    <Columns>
                        <asp:TemplateField>
                        <HeaderTemplate>
                                EDIT
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("PROFILE_ID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_User" >
                                        <asp:Image ID="imgEdit" ImageUrl="../Content/images/ico-edit.png" runat="server" />
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
                                    <asp:LinkButton ID="LinkButton1" runat="server" RecId='<%#Eval("PROFILE_ID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnDelete_User" >
                                        <asp:Image ID="Image1" ImageUrl="../Content/images/ico-delete.png" runat="server" />
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PROFILE_ID" HeaderText="PROFILE_ID" SortExpression="PROFILE_ID" Visible="false" />
                        <asp:BoundField DataField="FIRSTNAME" HeaderText="FIRSTNAME" SortExpression="FIRSTNAME" />
                        <asp:BoundField DataField="LASTNAME" HeaderText="LASTNAME" SortExpression="LASTNAME" />
                        
                        <asp:BoundField DataField="USER_ID" HeaderText="USER ID" SortExpression="USER_ID" />
                        <asp:BoundField DataField="ROLE_NAME" HeaderText="ROLE NAME" SortExpression="ROLE_ID" />
                        <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="EMAIL ADDRESS" SortExpression="EMAIL_ADDRESS" />
                        <asp:BoundField DataField="ISAPPROVED" HeaderText="IS APPROVED" SortExpression="ISAPPROVED" />
                        <asp:BoundField DataField="CREATED_DATE" HeaderText="CREATED DATE" SortExpression="CREATED_DATE" />
                        
                    </Columns>
                    <HeaderStyle BackColor="White" BorderColor="White" CssClass="rptheader" Font-Bold="True"
                        ForeColor="Black" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle CssClass="pagerheader" />
                    <RowStyle BackColor="#E9F1F6" CssClass="rptitem" ForeColor="#4A3C8C" HorizontalAlign="Left" Wrap="False" />
                    <SelectedRowStyle BackColor="#738A9C" CssClass="rptitem" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                </asp:GridView>
                <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                    SelectCommand="SELECT &quot;PROFILE_ID&quot;, &quot;FIRSTNAME&quot;, &quot;LASTNAME&quot;, &quot;USER_ID&quot;, &quot;ROLE_ID&quot;, &quot;EMAIL_ADDRESS&quot;, &quot;ISAPPROVED&quot;, &quot;CREATED_DATE&quot; FROM &quot;CM_USER_PROFILE&quot;" 
                    OldValuesParameterFormatString="original_{0}"
                </asp:SqlDataSource>--%>
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
        <div class="tab-pane fade " id="tab2">
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
                                <asp:Label ID="lblRolMsg" runat="server"></asp:Label>
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
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0" OnPageIndexChanging="GridView2_PageIndexChanging" 
                    CssClass="table" DataSourceID="SqlDataSource1" GridLines="None"  PageSize="5" DataKeyNames="ROLE_ID" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" EmptyDataText="No Record(s) found!">
                    <AlternatingRowStyle BackColor="White" CssClass="rptitem"  /><%--OnDataBinding="OnGridDataBinding"
                    OnDataBound="OnGridDataBound" OnPageIndexChanging="OnGridPageIndexChanging" OnSelectedIndexChanged="OnGridSelectedIndexChanged"
                    OnSorted="OnGridSorted" OnSorting="OnGridSorting"--%>
                    <Columns>
                        <asp:BoundField DataField="ROLE_ID" HeaderText="ROLE_ID" ReadOnly="True" SortExpression="ROLE_ID" Visible="false" />
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
                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                    SelectCommand="SELECT &quot;ROLE_ID&quot;, &quot;ROLE_NAME&quot; FROM &quot;CM_USER_ROLES&quot;" 
                    ConflictDetection="CompareAllValues" 
                    DeleteCommand="DELETE FROM &quot;CM_USER_ROLES&quot; WHERE &quot;ROLE_ID&quot; = :original_ROLE_ID AND &quot;ROLE_NAME&quot; = :original_ROLE_NAME" 
                    InsertCommand="INSERT INTO &quot;CM_USER_ROLES&quot; (&quot;ROLE_ID&quot;, &quot;ROLE_NAME&quot;) VALUES (:ROLE_ID, :ROLE_NAME)" 
                    OldValuesParameterFormatString="original_{0}" 
                    UpdateCommand="UPDATE &quot;CM_USER_ROLES&quot; SET &quot;ROLE_NAME&quot; = :ROLE_NAME WHERE &quot;ROLE_ID&quot; = :original_ROLE_ID AND &quot;ROLE_NAME&quot; = :ROLE_NAME">
                    <DeleteParameters>
                        <asp:Parameter Name="original_ROLE_ID" Type="Decimal" />
                        <asp:Parameter Name="original_ROLE_NAME" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ROLE_ID" Type="Decimal" />
                        <asp:Parameter Name="ROLE_NAME" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="ROLE_NAME" Type="String" />
                        <asp:Parameter Name="original_ROLE_ID" Type="Decimal" />
                        <asp:Parameter Name="original_ROLE_NAME" Type="String" />
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
        <div class="tab-pane fade" id="tab3">
            <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Create Permissions</h3>
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
            <asp:Button ID="btnPermission" runat="server" Text="Create Permission" CssClass="btn btn-primary" OnClick="btnPermission_Click" />
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
                    CssClass="table" GridLines="None"  PageSize="10" OnPageIndexChanging="GridView3_PageIndexChanging" 
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
                                        <asp:Image ID="imgEdit" ImageUrl="../Content/images/ico-edit.png" runat="server" />
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
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnDelete_Permission" >
                                        <asp:Image ID="Image1" ImageUrl="../Content/images/ico-delete.png" runat="server" />
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
            </div>
        <div class="tab-pane fade" id="tab4">
            <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Assign Permissions</h3>
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
                                        <asp:Image ID="imgEdit" ImageUrl="../Content/images/ico-edit.png" runat="server" />
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
                                        <asp:Image ID="Image1" ImageUrl="../Content/images/ico-delete.png" runat="server" />
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
            </div>
        <div class="tab-pane fade" id="tab5">
            <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Assign Users</h3>
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
                                <asp:Label ID="lblAssgnUserMsg" runat="server"></asp:Label>
                            </td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Assign</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlMakers" runat="server" CssClass="form-control" >
                                </asp:DropDownList>

                                
                            </td>

                            <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Assign to</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlcheckers" runat="server" CssClass="form-control" >
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
            <asp:Button ID="Button1" runat="server" Text="Assign User" CssClass="btn btn-primary" OnClick="btnAssignUser_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:SqlDataSource ID="dsMCX" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="select maker_id ig_no,b.USER_ID MAKER_NAME ,CHECKER_ID,C.USER_ID CHECKER_NAME,RECORD_ID 
                        from CM_MAKER_CHECKER_XREF a,CM_USER_PROFILE b,CM_USER_PROFILE C where a.maker_id = b.PROFILE_ID AND a.CHECKER_ID = C.PROFILE_ID">
                         <%--SELECT &quot;MAKER_ID&quot;, &quot;CHECKER_ID&quot; , &quot;RECORD_ID&quot; FROM &quot;CM_MAKER_CHECKER_XREF&quot;--%>
                    </asp:SqlDataSource></td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4"><div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View Assigned Users</h3>
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="dsMCX"
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0" DataKeyNames="RECORD_ID"
                    CssClass="table" GridLines="None"  PageSize="15" OnPageIndexChanging="GridView5_PageIndexChanging"  
                    OnClientClick="javascript:return confirm('Are you sure you want to delete this record?')" EmptyDataText="No Record(s) found!">
                    <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                    <Columns>
                        <%--<asp:TemplateField>
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
                        </asp:TemplateField>--%>
                        <asp:TemplateField>
                        <HeaderTemplate>
                                DELETE
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" RecId='<%#Eval("RECORD_ID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnDelete_AssignedUser" >
                                        <asp:Image ID="Image1" ImageUrl="../Content/images/ico-delete.png" runat="server" />
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RECORD_ID" HeaderText="recordID" SortExpression="recordID" Visible="false" />
                        <asp:BoundField DataField="MAKER_NAME" HeaderText="MAKER" SortExpression="Maker" />
                        <asp:BoundField DataField="CHECKER_NAME" HeaderText="CHECKER" SortExpression="Checker" />
                        
                       
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
            </div>
    </div>



</asp:Content>
