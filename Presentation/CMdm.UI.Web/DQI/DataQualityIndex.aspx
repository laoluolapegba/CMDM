<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataQualityIndex.aspx.cs" Inherits="Cdma.Web.DQI.DataQualityIndex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- BASIC INPUT -->
    <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Data Quality Index Set Up</h3>
        </div>
        <div class="widget-content">
            <div class="form-horizontal">
                <%--<div class="form-group">
                    &nbsp;<div class="col-md-10">--%>
                        <asp:Label ID="lblDQIMsg" runat="server" Text=""></asp:Label>

                  <%--  </div>
                </div>--%>


                <div class="form-group">
                    <label class="col-md-2 control-label"><strong>DQI Name:</strong></label>
                    <div class="col-md-10">
                        <%--<input type="text" class="form-control" placeholder="text field">--%>
                        <asp:TextBox ID="txtDQIName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label"><strong>DQI Description:</strong></label>
                    <div class="col-md-10">
                        <%--<input type="password" class="form-control" value="asecret">--%>
                        <asp:TextBox ID="txtDQLDesc" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-2 control-label"><strong>DQI Script:</strong></label>
                    <div class="col-md-10">
                        <%--<textarea class="form-control" placeholder="textarea" rows="4"></textarea>--%>
                        <asp:TextBox ID="txtDQIScript" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    &nbsp;<div class="col-md-10">
                        &nbsp;
                    </div>
                </div>

                <div class="form-group">
                    &nbsp;<div class="col-md-10">
                        &emsp;&emsp;&emsp;&emsp; &emsp;&emsp;&emsp;&emsp; &emsp;&emsp;&emsp;&emsp; &emsp; &emsp;<asp:Button ID="btnCreateDQIScript" runat="server" Text="Create Script" CssClass="btn btn-primary" OnClick="btnSave_DQIScript" />
                        <asp:Button ID="btnUpdateDQIScript" runat="server" Text="Update Script" CssClass="btn btn-primary" OnClick="btnUpdate_DQIScript" />
                        &nbsp;
                        <asp:HiddenField ID="HidID" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    &nbsp;<div class="col-md-10">
                    </div>
                </div>
                <div class="form-group">
                    &nbsp;
                </div>
               <div class="widget widget-table">
											<div class="widget-header">
												<h3><i class="fa fa-table"></i> View Scripts</h3>
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0"
                    CssClass="table" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" 
                    OnClientClick="javascript:return confirm('Are you sure you want to delete this record?')" EmptyDataText="No Record(s) found!" DataSourceID="DQIDataSource" >
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
                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("DQI_ID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_DQI" >
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
                                    <asp:LinkButton ID="LinkButton1" runat="server" RecId='<%#Eval("DQI_ID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnDelete_DQI" >
                                        <asp:Image ID="Image1" ImageUrl="../Content/assets/img/ico-delete.png" runat="server" />
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DQI_NAME" HeaderText="DQI_NAME" SortExpression="DQI_NAME" />
                        <asp:BoundField DataField="DQI_DESC" HeaderText="DQI_DESC" SortExpression="DQI_DESC" />
                        <asp:BoundField DataField="DQI_SCRIPT" HeaderText="DQI_SCRIPT" SortExpression="DQI_SCRIPT" />
                        
                        <asp:BoundField DataField="DQI_ID" HeaderText="DQI_ID" SortExpression="DQI_ID" Visible="false" />
                        
                    </Columns>
                    <HeaderStyle BackColor="White" BorderColor="White" CssClass="rptheader" Font-Bold="True"
                        ForeColor="Black" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle CssClass="pagerheader" />
                    <RowStyle BackColor="#E9F1F6" CssClass="rptitem" ForeColor="#4A3C8C" HorizontalAlign="Left" Wrap="False" />
                    <SelectedRowStyle BackColor="#738A9C" CssClass="rptitem" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                </asp:GridView>
                                                         <asp:SqlDataSource ID="DQIDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>" ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;DQI_NAME&quot;, &quot;DQI_DESC&quot;, &quot;DQI_SCRIPT&quot;, &quot;DQI_ID&quot; FROM &quot;MDM_DQI_SETTING&quot;"></asp:SqlDataSource>
                <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                    SelectCommand="SELECT &quot;PROFILE_ID&quot;, &quot;FIRSTNAME&quot;, &quot;LASTNAME&quot;, &quot;USER_ID&quot;, &quot;ROLE_ID&quot;, &quot;EMAIL_ADDRESS&quot;, &quot;ISAPPROVED&quot;, &quot;CREATED_DATE&quot; FROM &quot;CM_USER_PROFILE&quot;" 
                    OldValuesParameterFormatString="original_{0}"
                </asp:SqlDataSource>--%>
            </div>
												</div>
											</div>
										</div>
            </div>
        </div>
    </div>
    <!-- END BASIC INPUT -->

</asp:Content>
