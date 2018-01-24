<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CorporateCustInfo.aspx.cs" Inherits="Cdma.Web.CustomerInfo.CorporateCustInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            var tab = document.getElementById('<%= hidTAB.ClientID%>').value;
            $('#myTabs a[href="' + tab + '"]').tab('show');
            //alert(tab);
        });
</script>

    <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Corporate Customer</h3>
        </div>
        <div class="widget-content">
            <ul class="nav nav-tabs" role="tablist" id="myTabs">
                <li id="Corp1" runat="server"><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="true"><i class="fa fa-building"></i>Company Information</a></li>
                <li id="Corp2" runat="server"><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-building-o"></i>Company Details</a></li>
                <li id="Corp10" runat="server"><a href="#tab10" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-plus"></i>Company Additional Information</a></li>
                <li id="Corp3" runat="server"><a href="#tab3" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-archive"></i>Account Information</a></li>
                <li id="Corp4" runat="server"><a href="#tab4" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-legal"></i>Power of Attorney</a></li>
                <li id="Corp5" runat="server"><a href="#tab5" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-bank"></i>Account held with other banks</a></li>
                <li id="Corp6" runat="server"><a href="#tab6" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-file-text"></i>Management Biodata</a></li>
                <li id="Corp11" runat="server"><a href="#tab11" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-car"></i>Management Address</a></li>
                <li id="Corp7" runat="server"><a href="#tab7" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-photo"></i>Management Identification</a></li>
                <li id="Corp8" runat="server"><a href="#tab8" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-paper-plane"></i>Management Foriegn Details</a></li>
                <li id="Corp9" runat="server"><a href="#tab9" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-user-plus"></i>Management Next of Kin</a></li>
               
            </ul>

            <div class="tab-content">
                <div class="tab-pane fade active in" id="tab1">

                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg_CompanyInfo" runat="server" Style="color: #0033cc; font-weight: bold;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Customer No</td>
                            <td style="width: 211px">

                                <div class="input-group input-group-lg">
                                    <asp:TextBox ID="txtCustomerNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSearchCompInfo" runat="server" Text="Search" CssClass="btn btn-primary fa fa-search" OnClick="btnSearchCompInfo_Click" />
                                    </span>
                                </div>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>COMPANY INFORMATION</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Company Name</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCopmanyname" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>
                            </td>
                            <td>


                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 65px;">*Date Of Incorporation/ Registration</td>
                            <td style="width: 211px; height: 65px;">

                                <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtIncopDate" runat="server" Width="180" CssClass='form-control'   ClientIDMode="Static"  placeholder="DD/MM/YYYY"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtIncopDate.ClientID%>');
                                        dp.datepicker({
                                            changeMonth: true,
                                            changeYear: true,
                                            format: "dd/mm/yyyy",
                                            language: "tr"
                                        }).on('changeDate', function (ev) {
                                            $(this).blur();
                                            $(this).datepicker('hide');
                                        });
                                    });
                                </script>

                            </td>
                            <td style="height: 65px">


                            </td>
                            <td style="height: 65px"></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer Type</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="form-control">
                                    
                                </asp:DropDownList>

                            </td>
                            <td>


                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 66px;">*Registered Address</td>
                            <td style="width: 211px; height: 66px;">
                                

                                
                                <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control" Height="80px" Width="250px" TextMode="MultiLine"></asp:TextBox>

                            
                                
                            <td style="height: 66px">


                                &nbsp;</td>
                            <td style="height: 66px"></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Category of Business</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCatOfBiz" runat="server" CssClass="form-control">
                                    
                                </asp:DropDownList>

                            </td>

                            
                                
                            <td>


                                &nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px"> <asp:HiddenField ID="hidTAB" runat="server" Value="#tab1" /></td>
                            <td style="width: 211px">
                                <asp:Button ID="btnSaveCompInfo" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnSave_CompInfo" />
                            </td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                          
                    <div class="gridpanel"  max-height: 500px; style="overflow-x:auto;width:1000px">
                          <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0"
                                        CssClass="table" DataSourceID="sqCompanyInfo" GridLines="None" PageSize="20" 
                               EmptyDataText="No Record(s) found!">
                              <%--OnDataBinding="OnGridDataBinding"
                                        OnDataBound="OnGridDataBound" OnPageIndexChanging="OnGridPageIndexChanging" OnSelectedIndexChanged="OnGridSelectedIndexChanged"
                                        OnSorted="OnGridSorted" OnSorting="OnGridSorting" ">--%>
                                        <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                        <Columns>
                                            <asp:TemplateField>
                                            <HeaderTemplate>
                                                EDIT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_CompInfo">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                            <asp:BoundField DataField="COMPANY_NAME" HeaderText="COMPANY_NAME" SortExpression="COMPANY_NAME" />
                                            <asp:BoundField DataField="DATE_OF_INCORP_REGISTRATION" HeaderText="DATE_OF_INCORP_REGISTRATION" SortExpression="DATE_OF_INCORP_REGISTRATION" />
                                            
                                            <asp:BoundField DataField="REGISTERED_ADDRESS" HeaderText="REGISTERED_ADDRESS" SortExpression="REGISTERED_ADDRESS" />
                                            <asp:BoundField DataField="CUSTOMER_TYPE" HeaderText="CUSTOMER_TYPE" SortExpression="CUSTOMER_TYPE" />
                                            <asp:BoundField DataField="CATEGORY_OF_BUSINESS" HeaderText="CATEGORY_OF_BUSINESS" SortExpression="CATEGORY_OF_BUSINESS" />
                                            <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                            <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                            <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                            <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                        </Columns>
                                        <HeaderStyle BackColor="White" BorderColor="White" CssClass="rptheader" Font-Bold="True"
                                            ForeColor="Black" Wrap="False" />
                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                        <PagerStyle CssClass="pagerheader" />
                                        <RowStyle BackColor="#E9F1F6" CssClass="rptitem" ForeColor="#4A3C8C" HorizontalAlign="Left"
                                            Wrap="False" />
                                        <SelectedRowStyle BackColor="#738A9C" CssClass="rptitem" Font-Bold="True" ForeColor="#F7F7F7"
                                            Wrap="False" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="sqCompanyInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;COMPANY_NAME&quot;, &quot;DATE_OF_INCORP_REGISTRATION&quot;, &quot;REGISTERED_ADDRESS&quot;, &quot;CUSTOMER_TYPE&quot;, &quot;CATEGORY_OF_BUSINESS&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_COMPANY_INFORMATION&quot; WHERE ROWNUM <= 1000">
                                    </asp:SqlDataSource>

                       </div>
            </div>

                                        </div>
                                    </div>
                                
                                </td>
                        </tr>
                    </table>

                </div> 
                                  
                <div class="tab-pane fade" id="tab2">

                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblCompDetails" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Customer No</td>
                            <td style="width: 211px">

                                <div class="input-group input-group-lg">
                                    <asp:TextBox ID="txtCustomerNoCompDetails" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <%--<span class="input-group-btn">
                                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary fa fa-search" Visible="false"  />
                                    </span>--%>
                                </div>
                            </td>
                            <td style="width: 206px">

                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>COMPANY DETAILS</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Cert. Of Incorp./Reg. No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCompDRegNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px"></td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Jurisdiction Of Incorporation/ Registration</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCompDJurOfInc" runat="server" CssClass="form-control" Width="200px">
                                   <asp:ListItem Value="">--SELECT--</asp:ListItem>
                                                        
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">&nbsp;</td>
                            <td>

                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Special Control Unit Against Money Laundering (Scuml)No</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCompDSCUMLNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">&nbsp;</td>
                            <td>


                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Which Gender Owns And Controls 51% Or More Of The Business</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtCompDGender" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">&nbsp;</td>
                            <td>

                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Sector/Industry</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCompDSector" runat="server" CssClass="form-control" Width="200px">
                                  
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">&nbsp;</td>
                            <td>

                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                     
                       
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>BUSINESS</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Operating Business 1</td>
                            <td style="width: 211px">



                   <asp:DropDownList ID="ddlCompDOpBiz1" runat="server" CssClass="form-control" Width="200px" AutoPostBack="True" >
                                                
                                </asp:DropDownList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">Operating Business 2</td>
                            <td>
                                <asp:DropDownList ID="ddlCompDOpBiz2" runat="server" CssClass="form-control" Width="200px">
                                   
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*City</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCompDCity1" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">City</td>
                            <td>
                                <asp:TextBox ID="txtCompDCity2" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Country</td>
                            <td style="width: 211px">
                                <asp:DropDownList ID="ddlCompDCountry1" runat="server" CssClass="form-control" Width="200px">
                                   
                                </asp:DropDownList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">Country</td>
                            <td>
                                <asp:DropDownList ID="ddlCompDCountry2" runat="server" CssClass="form-control" Width="200px">
                                    
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Zip Code</td>
                            <td style="width: 211px">

                                

                                <asp:TextBox ID="txtCompDZipCode1" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                                

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">Zip Code</td>
                            <td>
                                
                                <asp:TextBox ID="txtCompDZipCode2" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                                
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Business Address/Registered Office</td>
                            <td style="width: 211px">

                                

                                <asp:TextBox ID="txtCompDBizAddy1" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                                

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">Business Address/Registered Office</td>
                            <td>
                                
                                <asp:TextBox ID="txtCompDBizAddy2" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                                
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">

                                

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">&nbsp;</td>
                            <td>
                                
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="4">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Company Email Address</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCompDCompEmailAddy" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">Crmb No/Borrower&#39;S Code</td>
                            <td>
                                <asp:TextBox ID="txtCompDBorrwerCode" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Website</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCompDWebsite" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">Expected Annual Turnover</td>
                            <td>

                                <asp:TextBox ID="txtCompDAnnTurnover" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Office land or mobile number</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCompDOfficeNo" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">Is Your Company Quoted On The Stock Exchange</td>
                            <td>
                                <asp:RadioButtonList ID="rbtCompDOnStckExnge" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Mobile Number</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCompDMobineNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">If Yes, Indicate Stock Exchange</td>
                            <td>

                                <asp:DropDownList ID="ddlCompDStkExhange" runat="server" CssClass="form-control" Width="200px">
                                   
                                </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*TIN</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCompDTIN" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">&nbsp;</td>
                            <td>

                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                         <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">
                                <asp:Button ID="btnSaveCompDetail" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_CompDetail" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                          <div class="reportspan2">
                    <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                        



                         <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0"
                                    CssClass="table" DataSourceID="dsCostomerDetails" EmptyDataText="No Record(s) found!" GridLines="None"  PageSize="20">
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
                                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_CompDetail">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                        <asp:BoundField DataField="CERT_OF_INCORP_REG_NO" HeaderText="CERT_OF_INCORP_REG_NO" SortExpression="CERT_OF_INCORP_REG_NO" />
                                        <asp:BoundField DataField="SECTOR_OR_INDUSTRY" HeaderText="SECTOR_OR_INDUSTRY" SortExpression="SECTOR_OR_INDUSTRY" />
                                        <asp:BoundField DataField="OPERATING_BUSINESS_1" HeaderText="OPERATING_BUSINESS_1" SortExpression="OPERATING_BUSINESS_1" />
                                        <asp:BoundField DataField="COUNTRY_1" HeaderText="COUNTRY_1" SortExpression="COUNTRY_1" />
                                        <asp:BoundField DataField="MOBILE_NUMBER" HeaderText="MOBILE_NUMBER" SortExpression="MOBILE_NUMBER" />
                                            <asp:BoundField DataField="TIN" HeaderText="TIN" SortExpression="TIN" />
                                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                    </Columns>
                                    <HeaderStyle BackColor="White" BorderColor="White" CssClass="rptheader" Font-Bold="True"
                                        ForeColor="Black" Wrap="False" />
                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                    <PagerStyle CssClass="pagerheader" />
                                    <RowStyle BackColor="#E9F1F6" CssClass="rptitem" ForeColor="#4A3C8C" HorizontalAlign="Left"
                                        Wrap="False" />
                                    <SelectedRowStyle BackColor="#738A9C" CssClass="rptitem" Font-Bold="True" ForeColor="#F7F7F7"
                                        Wrap="False" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="dsCostomerDetails" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;CERT_OF_INCORP_REG_NO&quot;, &quot;SECTOR_OR_INDUSTRY&quot;, &quot;OPERATING_BUSINESS_1&quot;, &quot;COUNTRY_1&quot;, &quot;MOBILE_NUMBER&quot;, &quot;TIN&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_COMPANY_DETAILS&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                                <SelectParameters>
                         <asp:ControlParameter ControlID="txtCustomerNoCompDetails" Name="CUSTOMER_NO" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    </div>
                </div>
                                        </div>
                                    </div>
                                </div>
                                </td>
                        </tr>
                    </table>


                </div>

                <div class="tab-pane fade" id="tab10">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblCompAddInfoMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>COMPANY ADDITIONAL INFORMATION</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtAddInfoCustID" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Name Of Affiliate Company/Body</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtAddInfoAffltdCompName" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Parent Company&#39;s Country Of Incorporation</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlAddInfoCountry" runat="server" CssClass="form-control" Width="200px" >
                                    
                            
                                                        
                                </asp:DropDownList></td>
                            <td>
</td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnAddInfoSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_CompAddInfo"/>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                &nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                     <tr>
                            <td colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            
                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">

                    <asp:GridView ID="GridView10" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataSourceID="sdCORPAddInfo" PageSize="20" Width="1057px" 
                         GridLines="None" EmptyDataText="No Record(s) found!"  CssClass="table" >
                         <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                    <Columns>
                                                 <asp:TemplateField>
                                            <HeaderTemplate>
                                                EDIT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_CompAddInfo">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                        <asp:BoundField DataField="AFFILIATE_COMPANY_BODY" HeaderText="AFFILIATE_COMPANY_BODY" SortExpression="AFFILIATE_COMPANY_BODY" />
                                        <asp:BoundField DataField="PARENT_COMPANY_CTRY_INCORP" HeaderText="PARENT_COMPANY_CTRY_INCORP" SortExpression="PARENT_COMPANY_CTRY_INCORP" />
                                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdCORPAddInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;AFFILIATE_COMPANY_BODY&quot;, &quot;PARENT_COMPANY_CTRY_INCORP&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_CORP_ADDITIONAL_DETAILS&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                                <SelectParameters>
                         <asp:ControlParameter ControlID="txtAddInfoCustID" Name="CUSTOMER_NO" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

                                        </div>
                                    </div>
                                </div>
                                </td>
                        </tr>
                    </table>

                </div>
                
                <div class="tab-pane fade" id="tab3">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblAccountInfo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>ACCOUNT INFOMATION</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNoAcctInfo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Account Type</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlAcctInfoAccttype" runat="server" CssClass="form-control" Width="200px" >
                                                                                            
                                </asp:DropDownList>

                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Domicile Branch</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlAcctInfoDomBranch" runat="server" CssClass="form-control" Width="200px">
                                   
                                    </asp:DropDownList>

                            </td>
                            <td>
</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Referral Code</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtAcctInfoReferralCode" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Account No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtAcctInfoAcctNo" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                                </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Account Name </td>
                            <td style="width: 211px; height: 54px;">

                                <asp:TextBox ID="txtAcctInfoAcctName" runat="server" CssClass="form-control" Height="40px" Width="250px" ></asp:TextBox>

                            </td>
                            <td style="height: 54px"></td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>ACCOUNT SERVICES REQUIRED</legend></td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Card Preference </td>
                            <td style="width: 211px">
                                <asp:RadioButtonList ID="rbtAcctInfoCrdPref" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Statement Frequency</td>
                            <td>
                                <asp:RadioButtonList ID="rbtAcctInfoStatmntFreq" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Electronic Banking Preference </td>
                            <td style="width: 211px; height: 36px;">
                                <asp:RadioButtonList ID="rbtAcctInfoEBankingPref" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Cheque Book Requisition</td>
                            <td style="height: 36px">
                                <asp:RadioButtonList ID="rbtAcctInfoChequeConfmtnReq" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="height: 36px"></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Statement Preferences</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList ID="rbtAcctInfoStatmntPref" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Cheque Confirmation</td>
                            <td>
                                <asp:RadioButtonList ID="rbtAcctInfoChequeConfmtn" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Transaction Alert Preference </td>
                            <td style="width: 211px">
                                <asp:RadioButtonList ID="rbtTranxAlertPref" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Cheque Confirmation Threshold </td>
                            <td>
                                <asp:RadioButtonList ID="rbtAcctInfoChequeConfmtnThreshold" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnAccountInfo" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_AccountInfo"/>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                &nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                     <tr>
                            <td colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            
                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">

                    <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataSourceID="dsAcctInfo" PageSize="20" Width="1057px" 
                         GridLines="None" EmptyDataText="No Record(s) found!"  CssClass="table" >
                         <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                EDIT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_AccountInfo">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                        <asp:BoundField DataField="ACCOUNT_NUMBER" HeaderText="ACCOUNT_NUMBER" SortExpression="ACCOUNT_NUMBER" />
                                        <asp:BoundField DataField="ACCOUNT_NAME" HeaderText="ACCOUNT_NAME" SortExpression="ACCOUNT_NAME" />
                                        <asp:BoundField DataField="DOMICILE_BRANCH" HeaderText="DOMICILE_BRANCH" SortExpression="DOMICILE_BRANCH" />
                                        <asp:BoundField DataField="CARD_PREFERENCE" HeaderText="CARD_PREFERENCE" SortExpression="CARD_PREFERENCE" />
                                        <asp:BoundField DataField="ELECTRONIC_BANKING_PREFERENCE" HeaderText="ELECTRONIC_BANKING_PREFERENCE" SortExpression="ELECTRONIC_BANKING_PREFERENCE" />
                                        <asp:BoundField DataField="STATEMENT_PREFERENCES" HeaderText="STATEMENT_PREFERENCES" SortExpression="STATEMENT_PREFERENCES" />
                                        
                                        <asp:BoundField DataField="TRANSACTION_ALERT_PREFERENCE" HeaderText="TRANSACTION_ALERT_PREFERENCE" SortExpression="TRANSACTION_ALERT_PREFERENCE" />
                                        <asp:BoundField DataField="CHEQUE_BOOK_REQUISITION" HeaderText="CHEQUE_BOOK_REQUISITION" SortExpression="CHEQUE_BOOK_REQUISITION" />
                                        <asp:BoundField DataField="CHEQUE_CONFIRMATION" HeaderText="CHEQUE_CONFIRMATION" SortExpression="CHEQUE_CONFIRMATION" />
                                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="dsAcctInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;ACCOUNT_NUMBER&quot;, &quot;ACCOUNT_NAME&quot;, &quot;DOMICILE_BRANCH&quot;, &quot;CARD_PREFERENCE&quot;, &quot;ELECTRONIC_BANKING_PREFERENCE&quot;, &quot;STATEMENT_PREFERENCES&quot;, &quot;TRANSACTION_ALERT_PREFERENCE&quot;, &quot;CHEQUE_BOOK_REQUISITION&quot;, &quot;CHEQUE_CONFIRMATION&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_CORP_ACCT_SERV_REQUIRED&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                                <SelectParameters>
                         <asp:ControlParameter ControlID="txtCustNoAcctInfo" Name="CUSTOMER_NO" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

                                        </div>
                                    </div>
                                </div>
                                </td>
                        </tr>
                    </table>

                </div>

                <div class="tab-pane fade" id="tab4">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblPOAMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                       <tr>
                            <td class="datepicker-inline" colspan="2"><legend>POWER OF ATTORNEY</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Account has power of attorney?</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList ID="rblPOAttorney" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="rblPOAttorney_SelectedIndexChanged" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem><%----%>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtPOACustNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Account No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtPOAAccountNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Holder Name </td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtPOAHolderName" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Address</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtPOAAddy" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                            </td>
                            <td>
</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Country</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlPOACountry" runat="server" CssClass="form-control" Width="200px" >
                                                                                        
                                </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Nationality</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlPOANationality" runat="server" CssClass="form-control" Width="200px">
                                              
                                                       
                                    </asp:DropDownList>

                                </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Telephone Number</td>
                            <td style="width: 211px; height: 54px;">

                                <asp:TextBox ID="txtPOAPhoneNo" runat="server" CssClass="form-control" Height="40px" Width="250px" ></asp:TextBox>

                            </td>
                            <td style="height: 54px"></td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnPOASave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_POAInfo"/>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                &nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                     <tr>
                            <td colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            
                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">

                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataSourceID="dsPOA" PageSize="20" Width="1057px" 
                         GridLines="None" EmptyDataText="No Record(s) found!"  CssClass="table" >
                         <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                EDIT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("CUSTOMER_NO") + ";" + Eval("ACCOUNT_NUMBER")%>' Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_POAInfo">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton><%--RecId='<%#Eval("CUSTOMER_NO")%>'--%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                        <asp:BoundField DataField="ACCOUNT_NUMBER" HeaderText="ACCOUNT_NUMBER" SortExpression="ACCOUNT_NUMBER" />
                                        <asp:BoundField DataField="HOLDER_NAME" HeaderText="HOLDER_NAME" SortExpression="HOLDER_NAME" />
                                        <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" SortExpression="ADDRESS" />
                                        <asp:BoundField DataField="COUNTRY" HeaderText="COUNTRY" SortExpression="COUNTRY" />
                                        <asp:BoundField DataField="NATIONALITY" HeaderText="NATIONALITY" SortExpression="NATIONALITY" />
                                        <asp:BoundField DataField="TELEPHONE_NUMBER" HeaderText="TELEPHONE_NUMBER" SortExpression="TELEPHONE_NUMBER" />
                                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                        
                                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="dsPOA" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;,&quot;ACCOUNT_NUMBER&quot;, &quot;HOLDER_NAME&quot;, &quot;ADDRESS&quot;, &quot;COUNTRY&quot;, &quot;NATIONALITY&quot;, &quot;TELEPHONE_NUMBER&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_POWER_OF_ATTORNEY&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                                <SelectParameters>
                         <asp:ControlParameter ControlID="txtPOACustNo" Name="CUSTOMER_NO" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

                                        </div>
                                    </div>
                                </div>
                                </td>
                        </tr>
                    </table>

                </div>

                <div class="tab-pane fade" id="tab5">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5" style="height: 28px">
                           
                             <asp:Label ID="lblAWOBmsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>ACCOUNT HELD WITH OTHER BANKS</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtAWOBCustNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Have Accounts with other Banks?</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtAWOB" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="rbtAWOB_SelectedIndexChanged">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Bank Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtAWOBBankName" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                            </td>
                            <td>
</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Bank Address</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtAWOBBankAddy" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Account No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtAWOBAcctNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                                </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Account Name </td>
                            <td style="width: 211px; height: 54px;">

                                <asp:TextBox ID="txtAWOBAcctName" runat="server" CssClass="form-control" Height="40px" Width="250px" ></asp:TextBox>

                            </td>
                            <td style="height: 54px"></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Status </td>
                            <td style="width: 211px; height: 54px;">

                                <asp:TextBox ID="txtAWOBStatus" runat="server" CssClass="form-control" Height="40px" Width="250px" ></asp:TextBox>

                            </td>
                            <td style="height: 54px"></td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        

                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnSaveAWOB" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_AWOBInfo"/>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                &nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                     <tr>
                            <td colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            
                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">

                    <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataSourceID="dsAWOB" PageSize="20" Width="1057px" 
                         GridLines="None" EmptyDataText="No Record(s) found!"  CssClass="table" >
                         <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                EDIT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_AWOB">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                        <asp:BoundField DataField="BANK_NAME" HeaderText="BANK_NAME" SortExpression="BANK_NAME" />
                                        <asp:BoundField DataField="BANK_ADDRESS_OR_BRANCH" HeaderText="BANK_ADDRESS_OR_BRANCH" SortExpression="BANK_ADDRESS_OR_BRANCH" />
                                        <asp:BoundField DataField="ACCOUNT_NAME" HeaderText="ACCOUNT_NAME" SortExpression="ACCOUNT_NAME" />
                                        <asp:BoundField DataField="ACCOUNT_NUMBER" HeaderText="ACCOUNT_NUMBER" SortExpression="ACCOUNT_NUMBER" />
                                        <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" />
                                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                        
                                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="dsAWOB" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;BANK_NAME&quot;, &quot;BANK_ADDRESS_OR_BRANCH&quot;, &quot;ACCOUNT_NAME&quot;, &quot;ACCOUNT_NUMBER&quot;, &quot;STATUS&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_ACCT_HELD_WITH_OTHER_BANK&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                                <SelectParameters>
                         <asp:ControlParameter ControlID="txtAWOBCustNo" Name="CUSTOMER_NO" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

                                        </div>
                                    </div>
                                </div>
                                </td>
                        </tr>
                    </table>

                </div>

                <div class="tab-pane fade " id="tab6">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblDIRMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>MANAGEMENT BIODATA</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRCustNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Management ID</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRMngtNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Management Type</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlDIRType" runat="server" CssClass="form-control" Width="200px" >
                                     <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="Directors">Directors</asp:ListItem>
                                    <asp:ListItem Value="Executives">Executives</asp:ListItem>
                                    <asp:ListItem Value="TrusteesTrustees">TrusteesTrustees</asp:ListItem>
                                    <asp:ListItem Value="Promoters">Promoters</asp:ListItem>
                                    <asp:ListItem Value="Executors">Executors</asp:ListItem>
                                    <asp:ListItem Value="Administrators">Administrators</asp:ListItem>
                                    <asp:ListItem Value="Principal Officers">Principal Officers</asp:ListItem>
                                    <asp:ListItem Value="Sole Proprietor">Sole Proprietor</asp:ListItem>
                                                        
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Place Of Birth</td>
                            <td>

                                <asp:TextBox ID="txtDIRPlaceOfBirth" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                                </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Title</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlDIRTitle" runat="server" CssClass="form-control" Width="200px" >
                                     <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            
                                    <asp:ListItem Value="Mr">Mr</asp:ListItem>
<asp:ListItem Value="Mrs">Mrs</asp:ListItem>
<asp:ListItem Value="Miss">Miss</asp:ListItem>
<asp:ListItem Value="Ms.">Ms.</asp:ListItem>                                   
<asp:ListItem Value="Dr.">Dr.</asp:ListItem>
<asp:ListItem Value="MASTER">MASTER</asp:ListItem>
<asp:ListItem Value="Prof.">Prof.</asp:ListItem>
<asp:ListItem Value="Rev.">Rev.</asp:ListItem>
<asp:ListItem Value="Lady">Lady</asp:ListItem>
<asp:ListItem Value="Sir">Sir</asp:ListItem>
<asp:ListItem Value="Alhaja">Alhaja</asp:ListItem>
<asp:ListItem Value="Alhaji">Alhaji</asp:ListItem>
<asp:ListItem Value="Capt.">Capt.</asp:ListItem>
<asp:ListItem Value="Major">Major</asp:ListItem>

<asp:ListItem Value="Bishop">Bishop</asp:ListItem>
<asp:ListItem Value="His Royal Majesty">His Royal Majesty</asp:ListItem>
<asp:ListItem Value="Her Royal Majesty">Her Royal Majesty</asp:ListItem>
<asp:ListItem Value="His Royal Highness">His Royal Highness</asp:ListItem>
<asp:ListItem Value="Her Royal Highness">Her Royal Highness</asp:ListItem>
<asp:ListItem Value="His Eminence">His Eminence</asp:ListItem>
<asp:ListItem Value="Sheikh">Sheikh</asp:ListItem>
<asp:ListItem Value="His Excellency">His Excellency</asp:ListItem>
<asp:ListItem Value="Her Excellency">Her Excellency</asp:ListItem>
<asp:ListItem Value="Chief">Chief</asp:ListItem>
<asp:ListItem Value="High Chief">High Chief</asp:ListItem>
<asp:ListItem Value="Elder">Elder</asp:ListItem>
<asp:ListItem Value="Monsignor">Monsignor</asp:ListItem>
<asp:ListItem Value="Deacon">Deacon</asp:ListItem>
<asp:ListItem Value="Deaconess">Deaconess</asp:ListItem>
<asp:ListItem Value="Lolo.">Lolo.</asp:ListItem>
<asp:ListItem Value="Barrister">Barrister</asp:ListItem>
<asp:ListItem Value="Engr.">Engr.</asp:ListItem>
<asp:ListItem Value="Arch.">Arch.</asp:ListItem>
<asp:ListItem Value="Surv.">Surv.</asp:ListItem>
<asp:ListItem Value="Ambassador">Ambassador</asp:ListItem>
<asp:ListItem Value="Senator">Senator</asp:ListItem>

<asp:ListItem Value="Lt.-Col.">Lt.-Col.</asp:ListItem>
<asp:ListItem Value="Col.">Col.</asp:ListItem>

<asp:ListItem Value="Lt.-Cmdr.">Lt.-Cmdr.</asp:ListItem>
<asp:ListItem Value="The Hon.">The Hon.</asp:ListItem>
<asp:ListItem Value="Cmdr.">Cmdr.</asp:ListItem>
<asp:ListItem Value="Flt. Lt.">Flt. Lt.</asp:ListItem>
<asp:ListItem Value="Brgdr.">Brgdr.</asp:ListItem>
<asp:ListItem Value="Judge">Judge</asp:ListItem>
<asp:ListItem Value="Lord">Lord</asp:ListItem>
<asp:ListItem Value="The Hon. Mrs">The Hon. Mrs</asp:ListItem>
<asp:ListItem Value="Wng. Cmdr.">Wng. Cmdr.</asp:ListItem>
<asp:ListItem Value="Group Capt.">Group Capt.</asp:ListItem>
<asp:ListItem Value="Rt. Hon. Lord">Rt. Hon. Lord</asp:ListItem>
<asp:ListItem Value="Revd. Father">Revd. Father</asp:ListItem>
<asp:ListItem Value="Revd. Canon">Revd. Canon</asp:ListItem>
<asp:ListItem Value="Pastor">Pastor</asp:ListItem>
<asp:ListItem Value="Maj.-Gen.">Maj.-Gen.</asp:ListItem>
<asp:ListItem Value="Air Cdre.">Air Cdre.</asp:ListItem>
<asp:ListItem Value="Viscount">Viscount</asp:ListItem>
<asp:ListItem Value="Dame">Dame</asp:ListItem>
<asp:ListItem Value="Rear Admrl.">Rear Admrl.</asp:ListItem>
                                    <asp:ListItem Value="Others">Others</asp:ListItem>

                            
                                                        
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Date Of Birth</td>
                            <td style="width: 211px"> <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtDIRDOB" runat="server" Width="180px" placeholder="DD/MM/YYYY" 
                                        CssClass='form-control'   ClientIDMode="Static" ></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtDIRDOB.ClientID%>');
                                        dp.datepicker({
                                            changeMonth: true,
                                            changeYear: true,
                                            format: "dd/mm/yyyy",
                                            language: "tr"
                                        }).on('changeDate', function (ev) {
                                            $(this).blur();
                                            $(this).datepicker('hide');
                                        });
                                    });
                                </script></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Surname</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRSurname" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Sex</td>
                            <td>

                                <asp:DropDownList ID="ddlDIRSex" runat="server" CssClass="form-control" Width="200px" >
                                      <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="MALE">MALE</asp:ListItem>
                            <asp:ListItem Value="FEMALE">FEMALE</asp:ListItem>
                                                            </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*First Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRFirstName" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Marital Status</td>
                            <td>

                                <asp:DropDownList ID="ddlDIRMStatus" runat="server" CssClass="form-control" Width="200px" >
                                             <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="MARRIED">MARRIED</asp:ListItem>
                            <asp:ListItem Value="SINGLE">SINGLE</asp:ListItem>
                            <asp:ListItem Value="DIVORCED">DIVORCED</asp:ListItem>
                                    <asp:ListItem Value="WIDOWED">WIDOWED</asp:ListItem>
                                    <asp:ListItem Value="SEPARATED">SEPARATED</asp:ListItem>
                                    
                            
                                                        
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Other Names</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIROtherName" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                                </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Nationality </td>
                            <td>

                                <asp:DropDownList ID="ddlDIRNationality" runat="server" CssClass="form-control" Width="200px" >
                                     
                            
                                                        
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 360px; left: 0px;">Mother’s Maiden Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRMMName" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                                </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Occupation</td>
                            <td>
                                <asp:TextBox ID="txtDIROccupation" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>
                               
                            </td>
                        </tr>
                       <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">Class Of Signatory</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlDIRClassOfSign" runat="server" CssClass="form-control" Width="200px" >
                                     <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="Primary">Primary</asp:ListItem>
                                    <asp:ListItem Value="Secondary">Secondary</asp:ListItem>
                                    <asp:ListItem Value="Sole">Sole</asp:ListItem>
                                                        
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Status/Job Title</td>
                            <td>
                                 <asp:TextBox ID="txtDIRJobTitle" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>
                               

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                                                
                         <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnDIRBioSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_DIRBioInfo"/>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                &nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                     <tr>
                            <td colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            
                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">

                    <asp:GridView ID="GridView6" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataSourceID="sdDIRBiodata" PageSize="20" Width="1057px" 
                         GridLines="None" EmptyDataText="No Record(s) found!"  CssClass="table"  >
                         <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                    <Columns><%--DataKeyNames="MANAGEMENT_ID"--%>
                                                 <asp:TemplateField>
                                            <HeaderTemplate>
                                                EDIT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server"  CommandArgument='<%#Eval("CUSTOMER_NO") + ";" + Eval("MANAGEMENT_ID")%>' Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_DIRBioInfo">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton><%--RecId='<%#Eval("CUSTOMER_NO")%>'--%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                        <asp:BoundField DataField="MANAGEMENT_ID" HeaderText="MANAGEMENT_ID" SortExpression="MANAGEMENT_ID" ReadOnly="True" />
                                        <asp:BoundField DataField="MANAGEMENT_TYPE" HeaderText="MANAGEMENT_TYPE" SortExpression="MANAGEMENT_TYPE" />
                                        <asp:BoundField DataField="SURNAME" HeaderText="SURNAME" SortExpression="SURNAME" />
                                        <asp:BoundField DataField="FIRST_NAME" HeaderText="FIRST_NAME" SortExpression="FIRST_NAME" />
                                        <asp:BoundField DataField="OTHER_NAMES" HeaderText="OTHER_NAMES" SortExpression="OTHER_NAMES" />
                                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                        
                                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdDIRBiodata" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;MANAGEMENT_ID&quot;, &quot;MANAGEMENT_TYPE&quot;, &quot;SURNAME&quot;, &quot;FIRST_NAME&quot;, &quot;OTHER_NAMES&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_MANAGEMENT_BIO_DATA&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                                <SelectParameters>
                         <asp:ControlParameter ControlID="txtDIRCustNo" Name="CUSTOMER_NO" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </div>
            </div>

                                        </div>
                                    </div>
                                </div>
                                </td>
                        </tr>
                    </table>

                </div>

                <div class="tab-pane fade" id="tab11">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblDIRAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>MANAGEMENT ADDRESS</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRAddyCustNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Management ID</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRAddyManID" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">*House Number</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRAddyHouseNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                           <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">*Country</td>
                            <td>

                                <asp:DropDownList ID="ddlDIRAddyCountry" runat="server" CssClass="form-control" Width="200px" OnSelectedIndexChanged="ddlDIRAddyCountry_SelectedIndexChanged" AutoPostBack="true">
                                                        
                                </asp:DropDownList>

                             </td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">*Street Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRAddyStreetName" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                             </td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">*State</td>
                            <td>

                                <asp:DropDownList ID="ddlDIRAddyState" runat="server" CssClass="form-control" Width="200px" OnSelectedIndexChanged="ddlDIRAddyState_SelectedIndexChanged" AutoPostBack="true">
                                    
                            
                                                        
                                </asp:DropDownList>

                                <asp:TextBox ID="txtDIRAddyState" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                             </td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">Nearest Bus Stop/Landmark</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRAddyBStop" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                                </td>
                              <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">*L.G.A (Residential)</td>
                            <td>

                                <asp:DropDownList ID="ddlDIRAddyLGA" runat="server" CssClass="form-control" Width="200px" >
                                                                                            
                                </asp:DropDownList>
                                <asp:TextBox ID="txtDIRAddyLGA" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">*City/Town</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRAddyCity" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                                </td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">Email Address</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRAddyEmailAddy" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDIRAddyEmailAddy" ErrorMessage="This email address is Invalid" ForeColor="Red" ValidationGroup="MngtAddy"
                                    ValidationExpression="^(?(&quot;&quot;)(&quot;&quot;.+?&quot;&quot;@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&amp;'\*\+/=\?\^`\{\}\|~\w])*)(?&lt;=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"></asp:RegularExpressionValidator>

                                </td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">Office Number</td>
                            <td>

                                <asp:TextBox ID="txtDIRAddyOfficeNo" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">*Mobile Number</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRAddyMobileNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="REmobile" runat="server" ControlToValidate="txtDIRAddyMobileNo" ErrorMessage="This mobile no is Invalid" ForeColor="Red" ValidationGroup="MngtAddy" ValidationExpression="^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$"></asp:RegularExpressionValidator>


                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 414px; left: 0px;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnDIRAddressSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_DIRAddressInfo" ValidationGroup="MngtAddy"/>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                &nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                     <tr>
                            <td colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            
                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">

                    <asp:GridView ID="GridView11" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataSourceID="sdDIRAddy" PageSize="20" Width="1057px" 
                         GridLines="None" EmptyDataText="No Record(s) found!"  CssClass="table" >
                         <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                    <Columns>
                                                 <asp:TemplateField>
                                            <HeaderTemplate>
                                                EDIT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server"  CommandArgument='<%#Eval("CUSTOMER_NO") + ";" + Eval("MANAGEMENT_ID")%>' Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_DIRAddressInfo">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton><%--RecId='<%#Eval("CUSTOMER_NO")%>'--%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                        <asp:BoundField DataField="MANAGEMENT_ID" HeaderText="MANAGEMENT_ID" SortExpression="MANAGEMENT_ID" />
                                        <asp:BoundField DataField="HOUSE_NUMBER" HeaderText="HOUSE_NUMBER" SortExpression="HOUSE_NUMBER" />
                                        <asp:BoundField DataField="STREET_NAME" HeaderText="STREET_NAME" SortExpression="STREET_NAME" />
                                        <asp:BoundField DataField="STATE" HeaderText="STATE" SortExpression="STATE" />
                                        <asp:BoundField DataField="COUNTRY" HeaderText="COUNTRY" SortExpression="COUNTRY" />
                                        <asp:BoundField DataField="MOBILE_NUMBER" HeaderText="MOBILE_NUMBER" SortExpression="MOBILE_NUMBER" />
                                        
                                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdDIRAddy" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;MANAGEMENT_ID&quot;, &quot;HOUSE_NUMBER&quot;, &quot;STREET_NAME&quot;, &quot;STATE&quot;, &quot;COUNTRY&quot;, &quot;MOBILE_NUMBER&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;,
                                     &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_MANAGEMENT_ADDRESS&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO AND &quot;MANAGEMENT_ID&quot; = :MANAGEMENT_ID)">
                                <SelectParameters>
                         <asp:ControlParameter ControlID="txtDIRAddyCustNo" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
                         <asp:ControlParameter ControlID="txtDIRAddyManID" Name="MANAGEMENT_ID" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

                                        </div>
                                    </div>
                                </div>
                                </td>
                        </tr>
                    </table>

                </div>

                <div class="tab-pane fade" id="tab7">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblDIRIDMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>IDENTIFICATION INFORMATION</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRDICustNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Management ID</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRDIManID" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Type Of Identification </td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlDIRIDTypeOfID" runat="server" CssClass="form-control" Width="200px">
                                      <asp:ListItem Value="">--SELECT--</asp:ListItem><%-- AutoPostBack="True" OnSelectedIndexChanged="ddlDIRIDTypeOfID_SelectedIndexChanged" --%>
                            <asp:ListItem Value="DRIVING LICENSE">DRIVING LICENSE</asp:ListItem>
                            <asp:ListItem Value="INTERNATIONAL PASSPORT">INTERNATIONAL PASSPORT</asp:ListItem>
                            <asp:ListItem Value="NATIONAL ID CARD">NATIONAL ID CARD</asp:ListItem>
                            <asp:ListItem Value="EMPLOYMENT ID">EMPLOYMENT ID</asp:ListItem>
                            <asp:ListItem Value="PENSION ID">PENSION ID</asp:ListItem>
                                    <asp:ListItem Value="VOTED CARD">VOTED CARD</asp:ListItem>
                            <asp:ListItem Value="BVN CARD">BVN CARD</asp:ListItem>
                                    <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                                </asp:DropDownList>



                                <asp:TextBox ID="txtDIRIDTypeOfID" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">&nbsp;</td>
                            
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Identification No.</td>
                            <td style="width: 211px; height: 36px;">

                                <asp:TextBox ID="txtDIRIDNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">&nbsp;</td>
                            
                            <td style="height: 36px"></td>
                        </tr>
                        
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*ID Issue Date</td>
                            <td style="width: 211px"> <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtDIRIDIssueDate" runat="server" Width="180" placeholder="DD/MM/YYYY" 
                                        CssClass='form-control'   ClientIDMode="Static" ></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtDIRIDIssueDate.ClientID%>');
                                        dp.datepicker({
                                            changeMonth: true,
                                            changeYear: true,
                                            format: "dd/mm/yyyy",
                                            language: "tr"
                                        }).on('changeDate', function (ev) {
                                            $(this).blur();
                                            $(this).datepicker('hide');
                                        });
                                    });
                                </script></td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*ID Expiry Date</td>
                            <td style="width: 211px"> <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtDIRIDExpiryDate" runat="server" Width="180" placeholder="DD/MM/YYYY" 
                                        CssClass='form-control'   ClientIDMode="Static" ></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtDIRIDExpiryDate.ClientID%>');
                                        dp.datepicker({
                                            changeMonth: true,
                                            changeYear: true,
                                            format: "dd/mm/yyyy",
                                            language: "tr"
                                        }).on('changeDate', function (ev) {
                                            $(this).blur();
                                            $(this).datepicker('hide');
                                        });
                                    });
                                </script></td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">BVN ID</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRIDBVNID" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">TIN</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRIDTIN" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                &nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnDIRIDSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_DIRIDInfo"/>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                     <tr>
                            <td colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            
                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">

                    <asp:GridView ID="GridView7" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataSourceID="sdDIRID" PageSize="20" Width="1057px" 
                         GridLines="None" EmptyDataText="No Record(s) found!"  CssClass="table" >
                         <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                    <Columns>
                                                 <asp:TemplateField>
                                            <HeaderTemplate>
                                                EDIT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server"  CommandArgument='<%#Eval("CUSTOMER_NO") + ";" + Eval("MANAGEMENT_ID")%>'  Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_DIRIDInfo">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton><%--RecId='<%#Eval("CUSTOMER_NO")%>'--%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                        <asp:BoundField DataField="MANAGEMENT_ID" HeaderText="MANAGEMENT_ID" SortExpression="MANAGEMENT_ID" />
                                        <asp:BoundField DataField="TYPE_OF_IDENTIFICATION" HeaderText="TYPE_OF_IDENTIFICATION" SortExpression="TYPE_OF_IDENTIFICATION" />
                                        <asp:BoundField DataField="ID_NO" HeaderText="ID_NO" SortExpression="ID_NO" />
                                        <asp:BoundField DataField="ID_ISSUE_DATE" HeaderText="ID_ISSUE_DATE" SortExpression="ID_ISSUE_DATE" />
                                        <asp:BoundField DataField="ID_EXPIRY_DATE" HeaderText="ID_EXPIRY_DATE" SortExpression="ID_EXPIRY_DATE" />
                                        <asp:BoundField DataField="BVN_ID" HeaderText="BVN_ID" SortExpression="BVN_ID" />
                                        
                                        <asp:BoundField DataField="TIN" HeaderText="TIN" SortExpression="TIN" />
                                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdDIRID" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;MANAGEMENT_ID&quot;, &quot;TYPE_OF_IDENTIFICATION&quot;, &quot;ID_NO&quot;, &quot;ID_ISSUE_DATE&quot;, 
                                    &quot;ID_EXPIRY_DATE&quot;, &quot;BVN_ID&quot;, &quot;TIN&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_MANAGEMENT_IDENTIFIER&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO AND &quot;MANAGEMENT_ID&quot; = :MANAGEMENT_ID)">
                                <SelectParameters>
                         <asp:ControlParameter ControlID="txtDIRDICustNo" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
                         <asp:ControlParameter ControlID="txtDIRDIManID" Name="MANAGEMENT_ID" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

                                        </div>
                                    </div>
                                </div>
                                </td>
                        </tr>
                    </table>

                </div>

                <div class="tab-pane fade" id="tab8">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblDIRForeignerMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>MANAGEMENT FOREIGN DETAILS</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                   
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRForeginCustNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                            <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Foreigner?</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtDIRForeigner" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="rbtDIRForeigner_SelectedIndexChanged">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Management ID</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRFMngtID" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Nationality</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlDIRFNationality" runat="server" CssClass="form-control" Width="200px" >
                                                     
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Permit Issue Date</td>
                            <td style="width: 211px"> <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtDIRFPIssueDate" runat="server" Width="180" placeholder="DD/MM/YYYY" 
                                        CssClass='form-control'   ClientIDMode="Static" ></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtDIRFPIssueDate.ClientID%>');
                                        dp.datepicker({
                                            changeMonth: true,
                                            changeYear: true,
                                            format: "dd/mm/yyyy",
                                            language: "tr"
                                        }).on('changeDate', function (ev) {
                                            $(this).blur();
                                            $(this).datepicker('hide');
                                        });
                                    });
                                </script></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Residence Permit Number (For Foreigners)</td>
                            <td style="width: 211px; height: 36px;">

                                <asp:TextBox ID="txtDIRFPermitNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Permit Expiry Date</td>
                            <td style="width: 211px"> <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtDIRFPExpiryDate" runat="server" Width="180" placeholder="DD/MM/YYYY" 
                                        CssClass='form-control'   ClientIDMode="Static" ></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtDIRFPExpiryDate.ClientID%>');
                                        dp.datepicker({
                                            changeMonth: true,
                                            changeYear: true,
                                            format: "dd/mm/yyyy",
                                            language: "tr"
                                        }).on('changeDate', function (ev) {
                                            $(this).blur();
                                            $(this).datepicker('hide');
                                        });
                                    });
                                </script></td>
                            <td style="height: 36px"></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">&nbsp;</td>
                            <td style="width: 211px">

                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px; right: 1856px;">Multiple Citizenship</td>
                            <td style="height: 54px">
                                <asp:RadioButtonList ID="rbtDIRMultipleCitizenship" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="rbtDIRMultipleCitizenship_SelectedIndexChanged">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="height: 54px"></td>
                        </tr>
                        
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Foreign Telephone Number</td>
                            <td style="width: 206px">

                                <asp:TextBox ID="txtDIRMFTelephoneNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                                </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">City</td>
                            <td>

                                <asp:TextBox ID="txtDIRMCity" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                        </tr>
                          <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Passport/Resident Permit Number</td>
                            <td style="width: 206px">

                                <asp:TextBox ID="txtDIRMResPermitNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                              </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Country</td>
                            <td>

                                <asp:DropDownList ID="ddlDIRMCountry" runat="server" CssClass="form-control" Width="200px" >
                                 
                            
                                                        
                                </asp:DropDownList>

                              </td>
                        </tr>
                          <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Foreign Address</td>
                            <td style="width: 206px">

                                <asp:TextBox ID="txtDIRMForeignAddy" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                              </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Zip/Postal Code</td>
                            <td>

                                <asp:TextBox ID="txtDIRMZipCode" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                              </td>
                        </tr>
                          <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">&nbsp;</td>
                            <td style="width: 206px">

                                &nbsp;</td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnSaveForeigner" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_ForeignerInfo"/>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                &nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                     <tr>
                            <td colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            
                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">

                    <asp:GridView ID="GridView8" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataSourceID="sdForeigner" PageSize="20" Width="1057px" 
                         GridLines="None" EmptyDataText="No Record(s) found!"  CssClass="table" >
                         <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                    <Columns>
                                                 <asp:TemplateField>
                                            <HeaderTemplate>
                                                EDIT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server"  CommandArgument='<%#Eval("CUSTOMER_NO") + ";" + Eval("MANAGEMENT_ID")%>' Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_ForeignerInfo">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton><%--RecId='<%#Eval("CUSTOMER_NO")%>'--%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                        <asp:BoundField DataField="MANAGEMENT_ID" HeaderText="MANAGEMENT_ID" SortExpression="MANAGEMENT_ID" />
                                        <asp:BoundField DataField="RESIDENCE_PERMIT_NUMBER" HeaderText="RESIDENCE_PERMIT_NUMBER" SortExpression="RESIDENCE_PERMIT_NUMBER" />
                                        <asp:BoundField DataField="PERMIT_ISSUE_DATE" HeaderText="PERMIT_ISSUE_DATE" SortExpression="PERMIT_ISSUE_DATE" />
                                        <asp:BoundField DataField="PERMIT_EXPIRY_DATE" HeaderText="PERMIT_EXPIRY_DATE" SortExpression="PERMIT_EXPIRY_DATE" />
                                        <asp:BoundField DataField="FOREIGN_TEL_NUMBER" HeaderText="FOREIGN_TEL_NUMBER" SortExpression="FOREIGN_TEL_NUMBER" />
                                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                        
                                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdForeigner" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;MANAGEMENT_ID&quot;, &quot;RESIDENCE_PERMIT_NUMBER&quot;, &quot;PERMIT_ISSUE_DATE&quot;, 
                                    &quot;PERMIT_EXPIRY_DATE&quot;, &quot;FOREIGN_TEL_NUMBER&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_MANAGEMENT_FOREIGNER&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO AND &quot;MANAGEMENT_ID&quot; = :MANAGEMENT_ID)">
                                <SelectParameters>
                         <asp:ControlParameter ControlID="txtDIRForeginCustNo" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
                         <asp:ControlParameter ControlID="txtDIRFMngtID" Name="MANAGEMENT_ID" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

                                        </div>
                                    </div>
                                </div>
                                </td>
                        </tr>
                    </table>

                </div>

                <div class="tab-pane fade" id="tab9">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblDIRNOKmsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>MANAGEMENT NEXT OF KIN</legend></td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRNOKCustNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Management ID</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRNOKMngtID" runat="server" CssClass="form-control" Height="40px"  ReadOnly="true"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Surname</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRNOKSurame" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Title</td>
                            <td>

                                <asp:DropDownList ID="ddlDIRNOKTitle" runat="server" CssClass="form-control" Width="200px" >
                                   <asp:ListItem Value="">--SELECT--</asp:ListItem>
                                  <asp:ListItem Value="Mr">Mr</asp:ListItem>
<asp:ListItem Value="Mrs">Mrs</asp:ListItem>
<asp:ListItem Value="Miss">Miss</asp:ListItem>
<asp:ListItem Value="Ms.">Ms.</asp:ListItem>                                   
<asp:ListItem Value="Dr.">Dr.</asp:ListItem>
<asp:ListItem Value="MASTER">MASTER</asp:ListItem>
<asp:ListItem Value="Prof.">Prof.</asp:ListItem>
<asp:ListItem Value="Rev.">Rev.</asp:ListItem>
<asp:ListItem Value="Lady">Lady</asp:ListItem>
<asp:ListItem Value="Sir">Sir</asp:ListItem>
<asp:ListItem Value="Alhaja">Alhaja</asp:ListItem>
<asp:ListItem Value="Alhaji">Alhaji</asp:ListItem>
<asp:ListItem Value="Capt.">Capt.</asp:ListItem>
<asp:ListItem Value="Major">Major</asp:ListItem>
<asp:ListItem Value="Bishop">Bishop</asp:ListItem>
<asp:ListItem Value="His Royal Majesty">His Royal Majesty</asp:ListItem>
<asp:ListItem Value="Her Royal Majesty">Her Royal Majesty</asp:ListItem>
<asp:ListItem Value="His Royal Highness">His Royal Highness</asp:ListItem>
<asp:ListItem Value="Her Royal Highness">Her Royal Highness</asp:ListItem>
<asp:ListItem Value="His Eminence">His Eminence</asp:ListItem>
<asp:ListItem Value="Sheikh">Sheikh</asp:ListItem>
<asp:ListItem Value="His Excellency">His Excellency</asp:ListItem>
<asp:ListItem Value="Her Excellency">Her Excellency</asp:ListItem>
<asp:ListItem Value="Chief">Chief</asp:ListItem>
<asp:ListItem Value="High Chief">High Chief</asp:ListItem>
<asp:ListItem Value="Elder">Elder</asp:ListItem>
<asp:ListItem Value="Monsignor">Monsignor</asp:ListItem>
<asp:ListItem Value="Deacon">Deacon</asp:ListItem>
<asp:ListItem Value="Deaconess">Deaconess</asp:ListItem>
<asp:ListItem Value="Lolo.">Lolo.</asp:ListItem>
<asp:ListItem Value="Barrister">Barrister</asp:ListItem>
<asp:ListItem Value="Engr.">Engr.</asp:ListItem>
<asp:ListItem Value="Arch.">Arch.</asp:ListItem>
<asp:ListItem Value="Surv.">Surv.</asp:ListItem>
<asp:ListItem Value="Ambassador">Ambassador</asp:ListItem>
<asp:ListItem Value="Senator">Senator</asp:ListItem>
<asp:ListItem Value="Lt.-Col.">Lt.-Col.</asp:ListItem>
<asp:ListItem Value="Col.">Col.</asp:ListItem>
<asp:ListItem Value="Lt.-Cmdr.">Lt.-Cmdr.</asp:ListItem>
<asp:ListItem Value="The Hon.">The Hon.</asp:ListItem>
<asp:ListItem Value="Cmdr.">Cmdr.</asp:ListItem>
<asp:ListItem Value="Flt. Lt.">Flt. Lt.</asp:ListItem>
<asp:ListItem Value="Brgdr.">Brgdr.</asp:ListItem>
<asp:ListItem Value="Judge">Judge</asp:ListItem>
<asp:ListItem Value="Lord">Lord</asp:ListItem>
<asp:ListItem Value="The Hon. Mrs">The Hon. Mrs</asp:ListItem>
<asp:ListItem Value="Wng. Cmdr.">Wng. Cmdr.</asp:ListItem>
<asp:ListItem Value="Group Capt.">Group Capt.</asp:ListItem>
<asp:ListItem Value="Rt. Hon. Lord">Rt. Hon. Lord</asp:ListItem>
<asp:ListItem Value="Revd. Father">Revd. Father</asp:ListItem>
<asp:ListItem Value="Revd. Canon">Revd. Canon</asp:ListItem>
<asp:ListItem Value="Pastor">Pastor</asp:ListItem>
<asp:ListItem Value="Maj.-Gen.">Maj.-Gen.</asp:ListItem>
<asp:ListItem Value="Air Cdre.">Air Cdre.</asp:ListItem>
<asp:ListItem Value="Viscount">Viscount</asp:ListItem>
<asp:ListItem Value="Dame">Dame</asp:ListItem>
<asp:ListItem Value="Rear Admrl.">Rear Admrl.</asp:ListItem>
<asp:ListItem Value="Others">Others</asp:ListItem>                            
  
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*First Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRNOKFirstName" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Relationship</td>
                            <td>

                                <asp:DropDownList ID="ddlDIRNOKRelationship" runat="server" CssClass="form-control" Width="200px" >
                                     <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem>BROTHER</asp:ListItem>
                            <asp:ListItem>CHILDREN</asp:ListItem>
                            <asp:ListItem>COUSIN</asp:ListItem>
                            <asp:ListItem>DAUGHTER</asp:ListItem>
                            <asp:ListItem>FATHER</asp:ListItem>
                            <asp:ListItem>GRANDCHILDREN</asp:ListItem>
                            <asp:ListItem>GUARDIAN</asp:ListItem>
                            <asp:ListItem>MOTHER</asp:ListItem>
                            <asp:ListItem>NEPHEW</asp:ListItem>
                            <asp:ListItem>NIECE</asp:ListItem>
                            <asp:ListItem>SISTER</asp:ListItem>
                            <asp:ListItem>SON</asp:ListItem>
                            <asp:ListItem>SPOUSE</asp:ListItem>
                            <asp:ListItem>UNCLE</asp:ListItem>
                                    <asp:ListItem>AUNT</asp:ListItem>
                                    <asp:ListItem>OTHERS</asp:ListItem>
                            
                                                        
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Other Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRNOKOtherName" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Office No</td>
                            <td>

                                <asp:TextBox ID="txtDIRNOKOfficeNo" runat="server" CssClass="form-control" Height="40px"  Width="200px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Date Of Birth</td>
                            <td style="width: 211px"> <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtDIRNOKDOB" runat="server" Width="180" placeholder="DD/MM/YYYY" 
                                        CssClass='form-control'   ClientIDMode="Static" ></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtDIRNOKDOB.ClientID%>');
                                        dp.datepicker({
                                            changeMonth: true,
                                            changeYear: true,
                                            format: "dd/mm/yyyy",
                                            language: "tr"
                                        }).on('changeDate', function (ev) {
                                            $(this).blur();
                                            $(this).datepicker('hide');
                                        });
                                    });
                                </script></td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Mobile No</td>
                            <td>

                                <asp:TextBox ID="txtDIRNOKMobileNo" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Sex </td>
                            <td style="width: 211px; height: 54px;">

                                <asp:DropDownList ID="ddlDIRNOKSex" runat="server" CssClass="form-control" Width="200px" >
                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="MALE">MALE</asp:ListItem>
                            <asp:ListItem Value="FEMALE">FEMALE</asp:ListItem>
                            
                                                        
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Email Address</td>
                            <td>

                                <asp:TextBox ID="txtDIRNOKEmailAddy" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="REemail" runat="server" ControlToValidate="txtDIRNOKEmailAddy" ErrorMessage="This email address is Invalid" ForeColor="Red" ValidationGroup="MngtNOK" ValidationExpression="^(?(&quot;&quot;)(&quot;&quot;.+?&quot;&quot;@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&amp;'\*\+/=\?\^`\{\}\|~\w])*)(?&lt;=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"></asp:RegularExpressionValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>NEXT OF KIN ADDRESS INFORMATION</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*House Number</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRNOKHouseNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Zip/Postal Code</td>
                            <td>

                                <asp:TextBox ID="txtDIRNOKZip" runat="server" CssClass="form-control" Height="40px" Width="250px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Street Name</td>
                            <td style="width: 211px; height: 36px;">

                                <asp:TextBox ID="txtDIRNOKStrName" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Country</td>
                            <td style="height: 36px">

                                <asp:DropDownList ID="ddlDIRNOKCountry" runat="server" CssClass="form-control" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlDIRNOKCountry_SelectedIndexChanged">
                                                             
                                                        
                                </asp:DropDownList>

                            </td>
                            <td style="height: 36px"></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">Nearest Bus Stop/Landmark</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRNOKBusStop" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*State</td>
                            <td>

                                <asp:DropDownList ID="ddlDIRNOKState" runat="server" CssClass="form-control" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlDIRNOKState_SelectedIndexChanged">
                                    
                            
                                                        
                                </asp:DropDownList>
                                <asp:TextBox ID="txtDIRNOKState" runat="server" CssClass="form-control" Height="40px" Width="200px" ></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*City/ Town</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtDIRNOKCity" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*L.G.A</td>
                            <td>

                                <asp:DropDownList ID="ddlDIRNOKLGA" runat="server" CssClass="form-control" Width="200px" >
                                    
                            
                                                        
                                </asp:DropDownList>
                                 <asp:TextBox ID="txtDIRNOKLGA" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnDIRNOKSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_DIRNOKInfo" ValidationGroup="MngtNOK"/>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                &nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                     <tr>
                            <td colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            
                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">

                    <asp:GridView ID="GridView9" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataSourceID="sdDIRNOK" PageSize="20" Width="1057px" 
                         GridLines="None" EmptyDataText="No Record(s) found!"  CssClass="table" >
                         <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                                    <Columns>
                                                 <asp:TemplateField>
                                            <HeaderTemplate>
                                                EDIT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("CUSTOMER_NO") + ";" + Eval("MANAGEMENT_ID")%>' Style="margin: 0px 0px 0px 5px;
                                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_DIRNOKInfo">
                                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                                    </asp:LinkButton><%--RecId='<%#Eval("CUSTOMER_NO")%>'--%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                        <asp:BoundField DataField="MANAGEMENT_ID" HeaderText="MANAGEMENT_ID" SortExpression="MANAGEMENT_ID" />
                                        <asp:BoundField DataField="TITLE" HeaderText="TITLE" SortExpression="TITLE" />
                                        <asp:BoundField DataField="SURNAME" HeaderText="SURNAME" SortExpression="SURNAME" />
                                        <asp:BoundField DataField="FIRST_NAME" HeaderText="FIRST_NAME" SortExpression="FIRST_NAME" />
                                        <asp:BoundField DataField="RELATIONSHIP" HeaderText="RELATIONSHIP" SortExpression="RELATIONSHIP" />
                                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                                        
                                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdDIRNOK" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;MANAGEMENT_ID&quot;, &quot;TITLE&quot;, &quot;SURNAME&quot;, &quot;FIRST_NAME&quot;, &quot;RELATIONSHIP&quot;, &quot;LAST_MODIFIED_DATE&quot;, 
                                    &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_MANAGEMENT_NEXT_OF_KIN&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO AND &quot;MANAGEMENT_ID&quot; = :MANAGEMENT_ID)">
                                <SelectParameters>
                         <asp:ControlParameter ControlID="txtDIRNOKCustNo" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
                         <asp:ControlParameter ControlID="txtDIRNOKMngtID" Name="MANAGEMENT_ID" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
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