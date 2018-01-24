<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndividualCustomer.aspx.cs" Inherits="Cdma.Web.CustomerInfo.IndividualCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   



    <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Individual Customer</h3>
        </div>
        <div class="widget-content">
            <ul class="nav nav-tabs" role="tablist" id="myTabs">
                <li id="Indiv1" runat="server"><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="true"><i class="fa fa-user"></i>Biodata Information</a></li>
                <li id="Indiv7" runat="server"><a href="#tab7" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-money"></i>Account Information</a></li>
                <li id="Indiv2" runat="server"><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-money"></i>Customer Income</a></li>
                <li id="Indiv4" runat="server"><a href="#tab4" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-user-plus"></i>Next of Kin</a></li>
                <li id="Indiv5" runat="server"><a href="#tab5" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-plane"></i>Foreigner Details</a></li>
                <li id="Indiv6" runat="server"><a href="#tab6" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-legal"></i>Jurat</a></li>
                <li id="Indiv8" runat="server"><a href="#tab8" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-suitcase"></i>Employee Info</a></li>
                <li id="Indiv3" runat="server"><a href="#tab3" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-bank"></i>Trusts/Client Accounts</a></li>
                <li id="Indiv9" runat="server"><a href="#tab9" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-suitcase"></i>Authentification for Financial Inclustion</a></li>
                <li id="Indiv10" runat="server"><a href="#tab10" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-bank"></i>Additional Information</a></li>
            </ul>

            <div class="tab-content">
                <div class="tab-pane fade active in" id="tab1">

                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblstatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">Customer No</td>
                            <td style="width: 211px">

                                <div class="input-group input-group-lg">
                                    <asp:TextBox ID="txtCustInfoID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary fa fa-search" OnClick="btnSearch_Click" />
                                    </span>
                                </div>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>BIODATA</legend></td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*Surname</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" Height="40px" Width="200px" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">*Title</td>
                            <td>

                                <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control" Width="200px">
                                   <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            
                                    <asp:ListItem Value="MR">MR</asp:ListItem>
<asp:ListItem Value="MRS">MRS</asp:ListItem>
<asp:ListItem Value="MISS">MISS</asp:ListItem>
<asp:ListItem Value="MS.">MS.</asp:ListItem>                                   
<asp:ListItem Value="DR.">DR.</asp:ListItem>
<asp:ListItem Value="MASTER">MASTER</asp:ListItem>
<asp:ListItem Value="PROF.">PROF.</asp:ListItem>
<asp:ListItem Value="REV.">REV.</asp:ListItem>
<asp:ListItem Value="LADY">LADY</asp:ListItem>
<asp:ListItem Value="SIR">SIR</asp:ListItem>
<asp:ListItem Value="ALHAJA">ALHAJA</asp:ListItem>
<asp:ListItem Value="ALHAJI">ALHAJI</asp:ListItem>
<asp:ListItem Value="CAPT.">CAPT.</asp:ListItem>
<asp:ListItem Value="MAJOR">MAJOR</asp:ListItem>

<asp:ListItem Value="BISHOP">BISHOP</asp:ListItem>
<asp:ListItem Value="HIS ROYAL MAJESTY">HIS ROYAL MAJESTY</asp:ListItem>
<asp:ListItem Value="HER ROYAL MAJESTY">HER ROYAL MAJESTY</asp:ListItem>
<asp:ListItem Value="HIS Royal HIGHNESS">HIS Royal HIGHNESS</asp:ListItem>
<asp:ListItem Value="HER Royal HIGHNESS">HER Royal HIGHNESS</asp:ListItem>
<asp:ListItem Value="HIS EMINENCE">HIS EMINENCE</asp:ListItem>
<asp:ListItem Value="SHEIKH">SHEIKH</asp:ListItem>
<asp:ListItem Value="HIS EXCELLENCY">HIS EXCELLENCY</asp:ListItem>
<asp:ListItem Value="HER EXCELLENCY">HER EXCELLENCY</asp:ListItem>
<asp:ListItem Value="ChIEF">ChIEF</asp:ListItem>
<asp:ListItem Value="HIGH ChIEF">HIGH ChIEF</asp:ListItem>
<asp:ListItem Value="ELDER">ELDER</asp:ListItem>
<asp:ListItem Value="MONSIGNOR">MONSIGNOR</asp:ListItem>
<asp:ListItem Value="DEACON">DEACON</asp:ListItem>
<asp:ListItem Value="DEACONESS">DEACONESS</asp:ListItem>
<asp:ListItem Value="LOLO.">LOLO.</asp:ListItem>
<asp:ListItem Value="BARRISTER">BARRISTER</asp:ListItem>
<asp:ListItem Value="ENGR.">ENGR.</asp:ListItem>
<asp:ListItem Value="ARCH.">ARCH.</asp:ListItem>
<asp:ListItem Value="SURV.">SURV.</asp:ListItem>
<asp:ListItem Value="AMBASSADOR">AMBASSADOR</asp:ListItem>
<asp:ListItem Value="SENATOR">SENATOR</asp:ListItem>
<asp:ListItem Value="LT.-COL.">LT.-COL.</asp:ListItem>
<asp:ListItem Value="COL.">COL.</asp:ListItem>
<asp:ListItem Value="LT.-CMDR.">LT.-CMDR.</asp:ListItem>
<asp:ListItem Value="THE HON.">THE HON.</asp:ListItem>
<asp:ListItem Value="CMDR.">CMDR.</asp:ListItem>
<asp:ListItem Value="FLT. LT.">FLT. LT.</asp:ListItem>
<asp:ListItem Value="BRGDR.">BRGDR.</asp:ListItem>
<asp:ListItem Value="JUDGE">JUDGE</asp:ListItem>
<asp:ListItem Value="LORD">LORD</asp:ListItem>
<asp:ListItem Value="THE HON. MRS">THE HON. MRS</asp:ListItem>
<asp:ListItem Value="WNG. CMDR.">WNG. CMDR.</asp:ListItem>
<asp:ListItem Value="GROUP CAPT.">GROUP CAPT.</asp:ListItem>
<asp:ListItem Value="RT. HON. LORD">RT. HON. LORD</asp:ListItem>
<asp:ListItem Value="REVD. FATHER">REVD. FATHER</asp:ListItem>
<asp:ListItem Value="REVD. CANON">REVD. CANON</asp:ListItem>
<asp:ListItem Value="PASTOR">PASTOR</asp:ListItem>
<asp:ListItem Value="MAJ.-GEN.">MAJ.-GEN.</asp:ListItem>
<asp:ListItem Value="AIR CDRE.">AIR CDRE.</asp:ListItem>
<asp:ListItem Value="VISCOUNT">VISCOUNT</asp:ListItem>
<asp:ListItem Value="DAME">DAME</asp:ListItem>
<asp:ListItem Value="REAR ADMRL.">REAR ADMRL.</asp:ListItem>
                                    <asp:ListItem Value="Others">Others</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*First Name</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">Other Name</td>
                            <td>
                                <asp:TextBox ID="txtOtherName" runat="server" CssClass="form-control" Height="40px" Width="200px"  ></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">Nickname/Alias</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtNickname" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Sex</td>
                            <td>


                                <asp:DropDownList ID="ddlSex" runat="server" CssClass="form-control" Width="200px">
                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="MALE">MALE</asp:ListItem>
                                    <asp:ListItem Value="FEMALE">FEMALE</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*Date of Birth</td>
                            <td style="width: 211px">

                                <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtDateOfBirth" runat="server" Width="180" CssClass="form-control" ClientIDMode="Static" placeholder="DD/MM/YYYY" AutoPostBack="true"   OnTextChanged="txtDateOfBirth_TextChanged" OnPreRender="txtDateOfBirth_OnPreRender"  ></asp:TextBox><%--OnDataBinding="txtDateOfBirth_DataBinding"--%>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                               <%-- <div class="container">--%>
   
                                  <script type="text/javascript">
                                      $(document).ready(function () {
                                          var dp = $('#<%=txtDateOfBirth.ClientID%>');
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

                            <td class="col-sm-3 control-label" style="width: 206px">&nbsp;*Age</td>
                            <td>

                                <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*Place of Birth</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtPlacefBirth" runat="server" CssClass="form-control" Height="40px" Width="200px" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">*Country of Birth</td>
                            <td>

                                <asp:DropDownList ID="ddlCountryOfBirth" runat="server" CssClass="form-control" Width="200px"  >
                                  
                           
                                </asp:DropDownList>

                               
                                <asp:TextBox ID="txtCountryofBirth" runat="server" CssClass="form-control" Height="40px" Width="200px" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px; height: 66px;">*Nationality</td>
                            <td style="width: 211px; height: 66px;">
                                

                                <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlNationality_SelectedIndexChanged"  AutoPostBack="true">
                                     <%--OnDataBound="ddlNationality_OnDataBound" OnPreRender="ddlNationality_OnPreRender" --%>
                                </asp:DropDownList>


                               </td> 
                            <td class="col-sm-3 control-label" style="width: 144px; height: 66px;">*State of Origin</td>
                            <td style="height: 66px">

                                <asp:DropDownList ID="ddlStateOfOrigin" runat="server" CssClass="form-control" Width="200px" >
                                   <%-- <asp:ListItem Value="Nill">--SELECT--</asp:ListItem>--%>
                            
                                </asp:DropDownList>
                 <asp:TextBox ID="txtStateOfOrigin" runat="server" CssClass="form-control" Height="40px"  Width="200px"></asp:TextBox>


                            </td>
                            <td style="height: 66px"></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*Marital Status</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="MARRIED">MARRIED</asp:ListItem>
                            <asp:ListItem Value="SINGLE">SINGLE</asp:ListItem>
                            <asp:ListItem Value="DIVORCED">DIVORCED</asp:ListItem>
                                    <asp:ListItem Value="WIDOWED">WIDOWED</asp:ListItem>
                                    <asp:ListItem Value="SEPARATED">SEPARATED</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Mothers Maiden Name</td>
                            <td>

                                <asp:TextBox ID="txtMothersMaidenName" runat="server" CssClass="form-control" Height="40px"   Width="200px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">No of Children</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtNoOfChildren" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Religion</td>
                            <td>

                                <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control" Width="200px">
                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="CHRISTIANITY">CHRISTIANITY</asp:ListItem>
                            <asp:ListItem Value="ISLAM">ISLAM</asp:ListItem>
                            <asp:ListItem Value="TRADITIONAL">TRADITIONAL</asp:ListItem>
                                    <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">Complexion</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtComplexion" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Disablility</td>
                            <td>

                                <asp:DropDownList ID="ddlDisability" runat="server" CssClass="form-control" Width="200px">
                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            
                                </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    
                        <tr>
                            <td class="datepicker-inline" style="width: 208px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>ADDRESS DETAILS</td>
                            <td style="width: 206px">&nbsp;&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*Country of Residence</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCountryofResidence" runat="server" CssClass="form-control" Width="200px" OnSelectedIndexChanged="ddlCountryofResidence_SelectedIndexChanged"  AutoPostBack="true">
                                  </asp:DropDownList><%--OnPreRender="ddlCountryofResidence_OnPreRender"--%>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">*State of Residence</td>
                            <td>

                                <asp:DropDownList ID="ddlStateOfResidence" runat="server" CssClass="form-control" Width="200px" OnSelectedIndexChanged="ddlStateOfResidence_SelectedIndexChanged"  AutoPostBack="true">
                                    
                           <%--OnPreRender="ddlStateOfResidence_OnPreRender"--%>

                                </asp:DropDownList>



                                <asp:TextBox ID="txtStateOfResidence" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>



                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*LGA of Residence</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlLGAofResidence" runat="server" CssClass="form-control" Width="200px" 
                                     AutoPostBack="true">
                                   
                            <%--OnSelectedIndexChanged="ddlLGAofResidence_SelectedIndexChanged"--%>
                                </asp:DropDownList>



                                <asp:TextBox ID="txtLGAOfResidence" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>



                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">*City/Town of Residence</td>
                            <td>

                                <asp:TextBox ID="txtCityofResidence" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px" rowspan="2">*Residential Address</td>
                            <td style="width: 211px" rowspan="2">
                                <asp:TextBox ID="txtResidentialAddy" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px" rowspan="2">
                                 *Nearest bus stop/Landmark
                            </td>
                            <td rowspan="2">
                                <asp:TextBox ID="txtNearestBusStop" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px"> Residence owned or rented&nbsp;</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList ID="rbtOwnedorRented" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">
                                Zip Postal Code</td>
                            <td>
                                <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px"> &nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="height: 18px;" colspan="2"><legend> CONTACT DETAILS</td>
                            <td style="width: 206px; height: 18px;">
                                </td>
                            <td style="height: 18px"></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 185px; top: 966px; left: 0px; height: 18px;">*Mobile No</td>
                            <td style="width: 211px; height: 18px;">
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox><%--(\([0-9]\d{4}\)|[0-9]\d{4})[- .]?\d{3}[- .]?\d{4}$--%>
                                <asp:RegularExpressionValidator ID="REmobile" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="This mobile no is Invalid" ForeColor="Red" ValidationGroup="Biodata" 
                                    ValidationExpression="^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$"></asp:RegularExpressionValidator>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px; height: 18px; top: -935px; left: -42px;">Email</td>
                            <td style="height: 18px">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="REemail" runat="server" ControlToValidate="txtEmail" ErrorMessage="This email address is Invalid" ForeColor="Red" ValidationGroup="Biodata"
                                     ValidationExpression="^(?(&quot;&quot;)(&quot;&quot;.+?&quot;&quot;@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&amp;'\*\+/=\?\^`\{\}\|~\w])*)(?&lt;=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"></asp:RegularExpressionValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 206px">Mailing Address</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtMailingAddy" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px"> &nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend> IDENTIFICATION DETAILS</td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px"> &nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 206px">*Identification Type</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlIDType" runat="server" CssClass="form-control" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlIDType_SelectedIndexChanged">
                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="DRIVING LICENSE">DRIVING LICENSE</asp:ListItem>
                            <asp:ListItem Value="INTERNATIONAL PASSPORT">INTERNATIONAL PASSPORT</asp:ListItem>
                            <asp:ListItem Value="NATIONAL ID CARD">NATIONAL ID CARD</asp:ListItem>
                            <asp:ListItem Value="EMPLOYMENT ID">EMPLOYMENT ID</asp:ListItem>
                            <asp:ListItem Value="PENSION ID">PENSION ID</asp:ListItem>
                                    <asp:ListItem Value="VOTED CARD">VOTED CARD</asp:ListItem>
                            <asp:ListItem Value="BVN CARD">BVN CARD</asp:ListItem>
                                    <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                                </asp:DropDownList>



                                <asp:TextBox ID="txtIDType" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>



                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">*Identification No</td>
                            <td>
                                <asp:TextBox ID="txtIDNo" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 206px">*ID Issue Date</td>
                            <td style="width: 211px"> <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtIDIssueDate" runat="server" Width="180" placeholder="DD/MM/YYYY" 
                                        CssClass='form-control'   ClientIDMode="Static" ></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtIDIssueDate.ClientID%>');
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
                            <td class="col-sm-3 control-label" style="width: 206px">*ID Expiry Date</td>
                            <td> <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtIDExpiryDate" runat="server" Width="180" placeholder="DD/MM/YYYY" 
                                        CssClass='form-control'   ClientIDMode="Static" ></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtIDExpiryDate.ClientID%>');
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
                            <td class="col-sm-3 control-label" style="width: 206px">*Place of Issuance</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtPlaceOfIssue" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px"> &nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend> OTHER DETAILS</td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px"> &nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 206px">TIN No</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtTINNo" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px"> &nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr><%-- --%>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px"> <asp:HiddenField ID="hidTAB" runat="server" Value="#tab1"/></td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">
                                <asp:Button ID="btnCustInfoUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnCustInfoUpdate_Click" ValidationGroup="Biodata" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px"> &nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">
                                &nbsp;</td>
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
                        <asp:GridView ID="GridView0" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0"
                            CssClass="table" DataSourceID="DSBiodata" GridLines="None"  PageSize="20" EmptyDataText="No Record(s) found!">
                            <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        EDIT
                                    </HeaderTemplate>
                                   <ItemTemplate>
                                      <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                            <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                                white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_CustomerInfo">
                                                <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                                <asp:BoundField DataField="SURNAME" HeaderText="SURNAME" SortExpression="SURNAME" />
                                <asp:BoundField DataField="FIRST_NAME" HeaderText="FIRST_NAME" SortExpression="FIRST_NAME" />
                                <asp:BoundField DataField="OTHER_NAME" HeaderText="OTHER_NAME" SortExpression="OTHER_NAME" />
                                <asp:BoundField DataField="DATE_OF_BIRTH" HeaderText="DATE_OF_BIRTH" SortExpression="DATE_OF_BIRTH" />
                                <asp:BoundField DataField="STATE_OF_ORIGIN" HeaderText="STATE_OF_ORIGIN" SortExpression="STATE_OF_ORIGIN" />
                                <asp:BoundField DataField="NATIONALITY" HeaderText="NATIONALITY" SortExpression="NATIONALITY" />
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
                        <asp:SqlDataSource ID="DSBiodata" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                            ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;SURNAME&quot;, &quot;FIRST_NAME&quot;, &quot;OTHER_NAME&quot;, &quot;DATE_OF_BIRTH&quot;, &quot;STATE_OF_ORIGIN&quot;, &quot;NATIONALITY&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_INDIVIDUAL_BIO_DATA&quot; WHERE ROWNUM <= 1000"><%--(&quot;CUSTOME
                                _NO&quot; = :CUSTOMER_NO)--%>
                         <%--    <SelectParameters>
                         <asp:ControlParameter ControlID="txtCustInfoID" Name="CUSTOMER_NO" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>--%>
                        </asp:SqlDataSource>
                        <%--SELECT "CUSTOMER_NO", "TITLE", "SURNAME", "FIRST_NAME", "DATE_OF_BIRTH", "LAST_MODIFIED_DATE", "LAST_MODIFIED_BY", "AUTHORISED_BY", "AUTHORISED_DATE" FROM "CDMA_INDIVIDUAL_BIO_DATA"--%>
                    </div>
                </div>
                                        </div>
                                    </div>
                                </div>
                                </td>
                        </tr>
                    </table>


                </div>

                <div class="tab-pane fade" id="tab7" >
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblstatusAI" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>ACCOUNT INFORMATION</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 243px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Customer No</td>
                            <td style="width: 243px">

                                <asp:TextBox ID="txtCustNoAI" runat="server" CssClass="form-control" Height="40px" ReadOnly="True" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*BVN Number</td>
                            <td>

                                <asp:TextBox ID="txtCustAIBVNNo" runat="server" CssClass="form-control" Height="40px" MaxLength="11" ></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Account Holder</td>
                            <td style="width: 243px">
                                <asp:RadioButtonList ID="rbtCustAIAccHolders" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="rbtCustAIAccHolders_SelectedIndexChanged">
                                    <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*CAV Required</td>
                            <td>
                            <asp:RadioButtonList ID="rblCAVRequired" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                                </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Type of Account</td>
                            <td style="width: 243px">

                                <asp:DropDownList ID="ddlCustAITypeOfAcc" runat="server" CssClass="form-control" Width="200px">
                                  
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer Ic</td>
                            <td>

                                <asp:TextBox ID="txtCustAICustIc" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Account Number</td>
                            <td style="width: 243px">
                                <asp:TextBox ID="txtCustAIAccNo" runat="server" CssClass="form-control" Height="40px" Enabled="True"></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer Segment</td>
                            <td>

                                <%--<asp:TextBox ID="txtCustAICustId" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>--%>

                                <asp:DropDownList ID="ddlCustAICustSeg" runat="server" CssClass="form-control" Width="200px">
                                     <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="Mass market">Mass market</asp:ListItem>
                            <asp:ListItem Value="Youth and Sch">Youth and Sch</asp:ListItem>
                            <asp:ListItem Value=" MSME"> MSME</asp:ListItem>
                            <asp:ListItem Value="Affluent">Affluent</asp:ListItem>
                           
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Account Officer</td>
                            <td style="width: 243px">

                               <asp:TextBox ID="txtCustAIAccOfficer" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>


                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer Type</td>
                            <td>

                                <asp:DropDownList ID="ddlCustAICusType" runat="server" CssClass="form-control" Width="200px">
                                    
                           
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Account Title</td>
                            <td style="width: 243px">

                                <asp:TextBox ID="txtCustAIAccTitle" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 408px; left: 1028px;">*Online Transfer Limit </td>
                            <td>

                               <%-- <asp:RadioButtonList ID="rbtASROnlineTraxLimit" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="rbtASROnlineTraxLimit_SelectedIndexChanged">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>--%>
                                <asp:RadioButtonList ID="rbtASROnlineTraxLimit" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="rbtCustAIAccHolders_SelectedIndexChanged" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem><%----%>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                                
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Branch</td>
                            <td style="width: 243px">

                                <asp:DropDownList ID="ddlCustAIBranch" runat="server" CssClass="form-control" Width="200px">
                                   
                            
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 53px;">*Online Transfer Limit Range</td>
                            <td>

                                <asp:DropDownList ID="ddlCustAIOnlineTrnsfLimit" runat="server" CssClass="form-control" Width="200px">
                                     
                           
                                </asp:DropDownList>

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Branch Class</td>
                            <td style="width: 243px">

                                <asp:DropDownList ID="ddlCustAIBranchClass" runat="server" CssClass="form-control" Width="200px">
                                   
                            
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 408px; left: 1028px;">*Operating Instruction</td>
                            <td>

                                <asp:TextBox ID="txtCustAIOpInsttn" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px; height: 53px;">*Business Division</td>
                            <td style="width: 243px; height: 53px;">

                                <asp:DropDownList ID="ddlCustAIBizDiv" runat="server" CssClass="form-control" Width="200px">
                                  
                           
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 408px; left: 1028px;">Originating Branch</td>
                            <td>

                                <asp:DropDownList ID="ddlCustAIOriginatingBranch" runat="server" CssClass="form-control" Width="200px">
                                    
                           
                                </asp:DropDownList>

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Business Segment</td>
                            <td style="width: 243px">

                                <asp:DropDownList ID="ddlCustBizSeg" runat="server" CssClass="form-control" Width="200px">
                                    
                           
                                </asp:DropDownList>

                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Business Size</td>
                            <td style="width: 243px">

                                <asp:DropDownList ID="ddlCustBizSize" runat="server" CssClass="form-control" Width="200px">
                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                                    <asp:ListItem Value="Retail Banking - Monthly Turnover below 40M">Retail Banking - Monthly Turnover below 40M</asp:ListItem>
                                    <asp:ListItem Value="Business Banking - Monthly Turnover > 40M but < less than 1B NGN">Business Banking - Monthly Turnover > 40M but < less than 1B NGN</asp:ListItem>
                                    <asp:ListItem Value="Corporate Banking - Monthly Turnover > 500M but < less than 2B NGN">Corporate Banking - Monthly Turnover > 500M but < less than 2B NGN</asp:ListItem>
                                    <asp:ListItem Value="Corporate Banking - Monthly Turnover 2B and Above">Corporate Banking - Monthly Turnover 2B and Above</asp:ListItem>
                                    
                            </asp:DropDownList>

                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 243px">&nbsp;</td>
                            <td style="width: 206px; height: 19px;"></td>
                            <td style="height: 19px"></td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>ACCOUNT SERVICES REQUIRED</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px; height: 19px;"></td>
                            <td style="width: 243px; height: 19px;"></td>
                            <td class="col-sm-3 control-label" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                &nbsp;</td>
                            <td style="height: 19px"></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Card Preference</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList ID="rbtASRCardRef" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Cheque Confirmation</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList ID="rbtASRChequeConfmtn" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Electronic Banking Preference</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtASREBP" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Cheque Confirmation Threshold</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtASRChequeConfmtnThreshold" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="rbtASRChequeConfmtnThreshold_SelectedIndexChanged">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem><%-- --%>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Statement Preferences</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtASRStatementPref" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Cheque Confirmation Threshold Range</td>
                            <td style="width: 211px">
                                
                            <asp:DropDownList ID="ddlASRChequeConfmtnThresholdRange" runat="server" CssClass="form-control" Width="200px">
                                    
                                    
                            </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Transaction Alert Preference</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtASRTransAlertPref" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Token</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtASRToken" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Statement Frequency</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtASRStatementFreq" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Account Signatory</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtASRAcctSignitory" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Cheque Book Requisition</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtASRChequeBookReqtn" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Second Signatory</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtASR2ndAcctSignitory" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Cheque Leaves Required</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtASRChequeLeaveReq" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">

                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 243px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 243px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 243px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 243px">
                                <asp:Button ID="btnCustAISave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnAccountInfo_Click" />
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 243px">&nbsp;</td>
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

                    <asp:GridView ID="GridView6" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderWidth="0px" CellPadding="0"
                        AutoGenerateColumns="False" DataSourceID="dsAccount" GridLines="None" CssClass="table" Width="1528px" EmptyDataText="No Record(s) found!">
                        <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    EDIT
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("CUSTOMER_NO") + ";" + Eval("ACCOUNT_NUMBER")%>' Style="margin: 0px 0px 0px 5px;
                                            white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_AccountInfo">
                                            <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png"  runat="server" />
                                        </asp:LinkButton><%--RecId='<%#Eval("CUSTOMER_NO")%>'--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                            <asp:BoundField DataField="TYPE_OF_ACCOUNT" HeaderText="TYPE_OF_ACCOUNT" SortExpression="TYPE_OF_ACCOUNT" />
                            <asp:BoundField DataField="ACCOUNT_NUMBER" HeaderText="ACCOUNT_NUMBER" SortExpression="ACCOUNT_NUMBER" />
                            <asp:BoundField DataField="ACCOUNT_TITLE" HeaderText="ACCOUNT_TITLE" SortExpression="ACCOUNT_TITLE" />
                            <asp:BoundField DataField="BRANCH" HeaderText="BRANCH" SortExpression="BRANCH" />
                            <asp:BoundField DataField="BVN_NUMBER" HeaderText="BVN_NUMBER" SortExpression="BVN_NUMBER" />
                            <asp:BoundField DataField="CUSTOMER_TYPE" HeaderText="CUSTOMER_TYPE" SortExpression="CUSTOMER_TYPE" />
                            <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                            <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                            
                            <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                            <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsAccount" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;TYPE_OF_ACCOUNT&quot;, &quot;ACCOUNT_NUMBER&quot;, &quot;ACCOUNT_TITLE&quot;, &quot;BRANCH&quot;, &quot;BVN_NUMBER&quot;, &quot;CUSTOMER_TYPE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot;, &quot;LAST_MODIFIED_DATE&quot; FROM &quot;CDMA_ACCOUNT_INFO&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO) ">
                        <SelectParameters>
                         <asp:ControlParameter ControlID="txtCustNoAI" Name="CUSTOMER_NO" PropertyName="Text"
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

                <div class="tab-pane fade" id="tab2">
                    <table class="nav-justified">
                        <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblstatusCI" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>CUSTOMER INCOME</td>
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

                                <asp:TextBox ID="txtCustNoIncome" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Income Band</td>
                            <td style="width: 211px">
                                <%--<asp:TextBox ID="txtCustIncomeBand" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlCustIncomeBand" runat="server" CssClass="form-control" Width="200px" >
                                   <%-- <asp:ListItem Value="Nill">--SELECT--</asp:ListItem>--%>
                            
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Initial Deposit</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCustIncomeInitDeposit" runat="server" CssClass="form-control" Width="200px">
                                     
                            
                                </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnCustIncomeSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnCustIncome_Click"  />
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
                            <td  colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                             
                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderWidth="0px" CellPadding="0"
                        AutoGenerateColumns="False" DataSourceID="dsCustIncome" PageSize="20" GridLines="None" Width="1057px" EmptyDataText="No Record(s) found!"  CssClass="table">
                        <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                        <Columns>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    EDIT
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                        <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                            white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_Income">
                                            <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                            <asp:BoundField DataField="INCOME_BAND" HeaderText="INCOME_BAND" SortExpression="INCOME_BAND" />
                            <asp:BoundField DataField="INITIAL_DEPOSIT" HeaderText="INITIAL_DEPOSIT" SortExpression="INITIAL_DEPOSIT" />
                            <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                            <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                            
                            <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                            <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsCustIncome" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;INCOME_BAND&quot;, &quot;INITIAL_DEPOSIT&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_CUSTOMER_INCOME&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                        <SelectParameters>
   <asp:ControlParameter ControlID="txtCustNoIncome" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
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
                           
                             <asp:Label ID="lblstatusNOK" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>NEXT OF KIN</td>
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

                                <asp:TextBox ID="txtCustNoNOK" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Identification Type</td>
                            <td style="width: 211px">

                                   <asp:DropDownList ID="ddlCustNOKIdType" runat="server" CssClass="form-control" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlCustNOKIdType_SelectedIndexChanged">
                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="DRIVING LICENSE">DRIVING LICENSE</asp:ListItem>
                            <asp:ListItem Value="INTERNATIONAL PASSPORT">INTERNATIONAL PASSPORT</asp:ListItem>
                            <asp:ListItem Value="NATIONAL ID CARD">NATIONAL ID CARD</asp:ListItem>
                            <asp:ListItem Value="EMPLOYMENT ID">EMPLOYMENT ID</asp:ListItem>
                            <asp:ListItem Value="PENSION ID">PENSION ID</asp:ListItem>
                                    <asp:ListItem Value="VOTED CARD">VOTED CARD</asp:ListItem>
                            <asp:ListItem Value="BVN CARD">BVN CARD</asp:ListItem>
                                    <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                                </asp:DropDownList>

                                

                                <asp:TextBox ID="txtCustNOKIdType" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Title</td>
                            <td style="width: 211px">
                                <asp:DropDownList ID="DDLCustNOKTitle" runat="server" CssClass="form-control" Width="200px">
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
                            <td class="col-sm-3 control-label" style="width: 144px">*ID Issue Date</td>
                            <td style="width: 211px">
                                <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtCustNOKIssuedDate" runat="server" 
                                        CssClass='form-control' placeholder="DD/MM/YYYY" ClientIDMode="Static" Width="200px"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtCustNOKIssuedDate.ClientID%>');
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
                            <td class="col-sm-3 control-label" style="width: 144px">*Surname</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKSurname" runat="server" CssClass="form-control" Height="40px" Enabled="True" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*ID Expiry Date</td>
                            <td style="width: 211px">

                                <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtCustNOKExpiryDate" runat="server" 
                                        CssClass='form-control' placeholder="DD/MM/YYYY" ClientIDMode="Static" Width="200px"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtCustNOKExpiryDate.ClientID%>');
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
                            <td class="col-sm-3 control-label" style="width: 144px">*First Name</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCustNOKfirstName" runat="server" CssClass="form-control" Height="40px" Enabled="True"  ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Passport/Resident Permit No</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCustNOKPermitNo" runat="server" CssClass="form-control" Height="40px" Enabled="True"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Other Names</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCustNOKOtherName" runat="server" CssClass="form-control" Height="40px" Enabled="True" ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Place of Issue</td>
                            <td style="width: 211px">
                      
                                <asp:TextBox ID="txtCustNOKPlaceOfIssue" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Date of Birth</td>
                            <td style="width: 211px">

                                <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtCustNOKDateOfBirth" runat="server" 
                                        CssClass='form-control' placeholder="DD/MM/YYYY"  ClientIDMode="Static" Width="200px"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtCustNOKDateOfBirth.ClientID%>');
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
                            <td class="col-sm-3 control-label" style="width: 144px">*Street Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKStreetName" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Sex</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCustNOKSex" runat="server" CssClass="form-control" Width="200px">
                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="MALE">MALE</asp:ListItem>
                            <asp:ListItem Value="FEMALE">FEMALE</asp:ListItem>
                                                            </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Nearest Busstop/Landmark</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKBusstop" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Relationship</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCustNOKReltnship" runat="server" CssClass="form-control" Width="200px">
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
                            <td class="col-sm-3 control-label" style="width: 144px">Zip/Postal Code</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKZipCode" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Office Phone No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKOfficeNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Country</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCustNOKCountry" runat="server" CssClass="form-control" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlCustNOKCountry_SelectedIndexChanged" >
                                     
                           
                                </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Mobile No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKMobileNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*State</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCustNOKState" runat="server" CssClass="form-control" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlCustNOKState_SelectedIndexChanged">
                                     
                            
                                </asp:DropDownList>

                                <asp:TextBox ID="txtCustNOKState" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Email Address</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKEmail" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCustNOKEmail" ErrorMessage="This email address is Invalid" ForeColor="Red" ValidationGroup="NOK" 
    ValidationExpression="^(?(&quot;&quot;)(&quot;&quot;.+?&quot;&quot;@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&amp;'\*\+/=\?\^`\{\}\|~\w])*)(?&lt;=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"></asp:RegularExpressionValidator>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*L.G.A</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlCustNOKLGA" runat="server" CssClass="form-control" Width="200px" >
                                     
                            
                                </asp:DropDownList>

                                <asp:TextBox ID="txtCustNOKLGA" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*House No</td>
                            <td style="width: 211px">

                                    <asp:TextBox ID="txtCustNOKHouseNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*City/Town</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKCity" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label"></td>
                            <td style="width: 211px">

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
                            <td style="width: 211px">
                                <asp:Button ID="btnCustNOKSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnNextofKin_Click" ValidationGroup="NOK" />
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
                            <td  colspan="5">
                                <div class="widget widget-table">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-table"></i>View Customer Information</h3>
                                    </div>
                                    <div class="widget-content">
                                        <div class="table-responsive">
                                            

                                            <div class="reportspan2">
                <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                    <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderWidth="0px" CellPadding="0"
                        AutoGenerateColumns="False" DataSourceID="dsKin" Width="1513px" GridLines="None" CssClass="table" EmptyDataText="No Record(s) found!">
                        <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                        <Columns>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    EDIT
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                        <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                            white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_NextOfKin">
                                            <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                            <asp:BoundField DataField="TITLE" HeaderText="TITLE" SortExpression="TITLE" />
                            <asp:BoundField DataField="SURNAME" HeaderText="SURNAME" SortExpression="SURNAME" />
                            <asp:BoundField DataField="FIRST_NAME" HeaderText="FIRST_NAME" SortExpression="FIRST_NAME" />
                            <asp:BoundField DataField="DATE_OF_BIRTH" HeaderText="DATE_OF_BIRTH" SortExpression="DATE_OF_BIRTH" />
                            <asp:BoundField DataField="RELATIONSHIP" HeaderText="RELATIONSHIP" SortExpression="RELATIONSHIP" />
                            <asp:BoundField DataField="MOBILE_NO" HeaderText="MOBILE_NO" SortExpression="MOBILE_NO" />
                            <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                            
                            <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                            <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                            <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsKin" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;TITLE&quot;, &quot;SURNAME&quot;, &quot;FIRST_NAME&quot;, &quot;DATE_OF_BIRTH&quot;, &quot;RELATIONSHIP&quot;, &quot;MOBILE_NO&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_INDIVIDUAL_NEXT_OF_KIN&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                         <SelectParameters>
   <asp:ControlParameter ControlID="txtCustNoNOK" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
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
                            <td colspan="5">
                           
                             <asp:Label ID="lblstatusFD" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>FOREIGNER DETAILS</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 73px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; ">*Customer No</td>
                            <td style="width: 73px">

                                <asp:TextBox ID="txtCustNoForgner" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" Width="200px" ></asp:TextBox>

                            </td>
                            <td rowspan="2">&nbsp;</td>
                            <td rowspan="2">&nbsp;</td>
                            <td rowspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Foreigner</td>
                            <td style="width: 73px">
                                <asp:RadioButtonList ID="rbtForeigner" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="rbtForeigner_SelectedIndexChanged">
                                    <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem><%----%>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Multiple Citizenship</td>
                            <td style="height: 54px; width: 73px;">

                                <asp:RadioButtonList ID="rbtMultipleCitizenship" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label">&nbsp;</td>
                            <td style="height: 54px">

                                &nbsp;</td>
                            <td style="height: 54px"></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Passport / Residence Permit No</td>
                            <td style="width: 73px">
                                <asp:TextBox ID="txtCustFPassPermit" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label">*Foreign Phone No</td>
                            <td>

                                <asp:TextBox ID="txtCustfForeignPhoneNo" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Passport / Permit Issue Date</td>
                            <td style="width: 73px">

                                <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtCustFIssueDate" runat="server" 
                                        CssClass='form-control' placeholder="DD/MM/YYYY" ClientIDMode="Static" Width="200px"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                                               
                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtCustFIssueDate.ClientID%>');
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
                            <td class="col-sm-3 control-label" style="top: 162px; left: 1064px;">Zip Postal Code</td>
                            <td>

                                
                                <asp:TextBox ID="txtCustfZipPostalCode" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                                
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Passport / Permit Expiry Date</td>
                            <td style="width: 73px">

                                <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtCustFExpiryDate" runat="server" 
                                        CssClass='form-control' placeholder="DD/MM/YYYY" ClientIDMode="Static" Width="200px"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>


                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtCustFExpiryDate.ClientID%>');
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
                            <td class="col-sm-3 control-label" style="top: 162px; left: 1064px;">*Purpose of Account</td>
                            <td>

                                
                                <asp:TextBox ID="txtCustfPurposeOfAcc" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                                
                               </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Foreign Address</td>
                            <td style="width: 73px">

                                <asp:TextBox ID="txtCustfForeignAddy" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*City</td>
                            <td style="width: 73px">

                                <asp:TextBox ID="txtCustfCity" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                                </td>
                            <td class="col-sm-3 control-label">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" >*Country</td>
                            <td style="height: 54px; width: 73px;">

                                <asp:DropDownList ID="ddlCustfCountry" runat="server" CssClass="form-control" Enabled="True" Width="200px">
                                   
                            
                                </asp:DropDownList>

                            </td>
                            <td class="col-sm-3 control-label" style="height: 54px;">&nbsp;</td>
                            <td style="height: 54px">

                                &nbsp;</td>
                            <td style="height: 54px"></td>
                        </tr>
                        
                        <tr>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 73px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 73px">
                                <asp:Button ID="btnCustfSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnForeDet_Click" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 73px">&nbsp;</td>
                            <td>&nbsp;</td>
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
                    <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderWidth="0px" CellPadding="0"
                        AutoGenerateColumns="False" DataSourceID="dsForeigner" GridLines="None" Width="2164px"  CssClass="table" EmptyDataText="No Record(s) found!">
                        <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    EDIT
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                        <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                            white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_Foreigner">
                                            <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                                </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                            <asp:BoundField DataField="PASSPORT_RESIDENCE_PERMIT" HeaderText="PASSPORT_RESIDENCE_PERMIT" SortExpression="PASSPORT_RESIDENCE_PERMIT" />
                            <asp:BoundField DataField="PERMIT_ISSUE_DATE" HeaderText="PERMIT_ISSUE_DATE" SortExpression="PERMIT_ISSUE_DATE" />
                            <asp:BoundField DataField="PERMIT_EXPIRY_DATE" HeaderText="PERMIT_EXPIRY_DATE" SortExpression="PERMIT_EXPIRY_DATE" />
                            <asp:BoundField DataField="FOREIGN_ADDRESS" HeaderText="FOREIGN_ADDRESS" SortExpression="FOREIGN_ADDRESS" />
                            <asp:BoundField DataField="COUNTRY" HeaderText="COUNTRY" SortExpression="COUNTRY" />
                            <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                            <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY"
                                SortExpression="LAST_MODIFIED_BY" />
                            <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                            <asp:BoundField DataField="IP_ADDRESS" HeaderText="IP_ADDRESS"
                                SortExpression="IP_ADDRESS" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsForeigner" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;PASSPORT_RESIDENCE_PERMIT&quot;, &quot;PERMIT_ISSUE_DATE&quot;, &quot;PERMIT_EXPIRY_DATE&quot;, &quot;FOREIGN_ADDRESS&quot;, &quot;COUNTRY&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_DATE&quot;, &quot;IP_ADDRESS&quot; FROM &quot;CDMA_FOREIGN_DETAILS&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                        <SelectParameters>
   <asp:ControlParameter ControlID="txtCustNoForgner" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
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

                <div class="tab-pane fade" id="tab6">
                    <table class="nav-justified">
                       <tr>
                            <td colspan="5">
                           
                             <asp:Label ID="lblstatusJurat" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>JURAT DETAILS</td>
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
                            <td class="col-sm-3 control-label" style="width: 250px">Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNoJurat" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td>

                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Date Of Oath</td>
                            <td style="width: 211px">
                               <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtCustJDateOfOath" runat="server" 
                                        CssClass='form-control' placeholder="DD/MM/YYYY" ClientIDMode="Static" Width="200"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtCustJDateOfOath.ClientID%>');
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
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Name Of Interpreter</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustJNameOfInerpreter" runat="server" CssClass="form-control" Height="40px" Width="300" ></asp:TextBox>

                            </td>
                            <td>
</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Address Of Interpreter</td>
                            <td style="width: 211px">

                               

                                <asp:TextBox ID="txtCustJAddyOfInterperter" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                               

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Telephone No </td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustJPhoneNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Language Of Interpretation</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustJLangOfInterpretation" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

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
                            <td style="width: 211px">
                                <asp:Button ID="btnCustJSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnJurat_Click" />
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
                    <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderWidth="0px" CellPadding="0"
                        AutoGenerateColumns="False" DataSourceID="dsjurat"  GridLines="None" Width="1280px"   CssClass="table" EmptyDataText="No Record(s) found!">
                        <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                        <Columns>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    EDIT
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                        <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                            white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_jurat">
                                            <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                            <asp:BoundField DataField="DATE_OF_OATH" HeaderText="DATE_OF_OATH" SortExpression="DATE_OF_OATH" />
                            <asp:BoundField DataField="NAME_OF_INTERPRETER" HeaderText="NAME_OF_INTERPRETER" SortExpression="NAME_OF_INTERPRETER" />
                            <asp:BoundField DataField="ADDRESS_OF_INTERPRETER" HeaderText="ADDRESS_OF_INTERPRETER" SortExpression="ADDRESS_OF_INTERPRETER" />
                            <asp:BoundField DataField="TELEPHONE_NO" HeaderText="TELEPHONE_NO" SortExpression="TELEPHONE_NO" />
                           
                            <asp:BoundField DataField="LANGUAGE_OF_INTERPRETATION" HeaderText="LANGUAGE_OF_INTERPRETATION" SortExpression="LANGUAGE_OF_INTERPRETATION" />
                            <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                            <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                            <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsjurat" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;DATE_OF_OATH&quot;, &quot;NAME_OF_INTERPRETER&quot;, &quot;ADDRESS_OF_INTERPRETER&quot;, &quot;TELEPHONE_NO&quot;, &quot;LANGUAGE_OF_INTERPRETATION&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_JURAT&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                        <SelectParameters>
   <asp:ControlParameter ControlID="txtCustNoJurat" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
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
                           
                             <asp:Label ID="lblstatusEI" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>EMPLOYEE INFORMATION</td>
                            <td>&nbsp;&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Customer No</td>
                            
                            <td>

                                <asp:TextBox ID="txtCustNoEmpInfo" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" Width="200px" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Employment Status</td>
                            <td>

                            <asp:DropDownList ID="ddlCustEIEmpStatus" runat="server" CssClass="form-control" Enabled="True"  Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlCustEIEmpStatus_SelectedIndexChanged">
                            <asp:ListItem Value="">--SELECT--</asp:ListItem>
                            <asp:ListItem>EMPLOYED</asp:ListItem>
                            <asp:ListItem>SELF EMPLOYED</asp:ListItem>
                            <asp:ListItem>ASSISTED</asp:ListItem>
                            </asp:DropDownList>

                            </td>
                            <td>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Employer Name/Institution Name</td>
                            <td>

                                <asp:TextBox ID="txtCustEIEmployerName" runat="server" CssClass="form-control" Height="40px"  Width="200px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Date of Employment</td>
                            <td>

                                <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtCustEIDateOfEmp" runat="server" 
                                        CssClass='form-control' placeholder="DD/MM/YYYY" ClientIDMode="Static" Width="200"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                               

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtCustEIDateOfEmp.ClientID%>');
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
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Sector Class</td>
                            <td>

                            <asp:DropDownList ID="ddlCustEISectorClass" runat="server" CssClass="form-control" Enabled="True"  Width="200px">
                            
                            </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Sub Sector</td>
                            <td>

                            <asp:DropDownList ID="ddlCustEISubSector" runat="server" CssClass="form-control" Enabled="True"  Width="200px">
                            
                            </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Nature of Business</td>
                            <td>

                            <asp:DropDownList ID="ddlCustEINatureOfBiz" runat="server" CssClass="form-control" Enabled="True"  Width="200px" OnSelectedIndexChanged="ddlCustEINatureOfBiz_SelectedIndexChanged" AutoPostBack="true" >
                            
                            </asp:DropDownList>

                                <asp:TextBox ID="txtCustEINatureOfBiz" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Industry Segment</td>
                            <td>

                            <asp:DropDownList ID="ddlCustEIIndustrySeg" runat="server" CssClass="form-control" Enabled="True"  Width="200px">
                           
                            </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="btnCustEISave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnEmpInfo_Click"/>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
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
                    <asp:GridView ID="GridView7" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderWidth="0px" CellPadding="0"
                        AutoGenerateColumns="False" DataSourceID="dsEmplInfo" GridLines="None" CssClass="table" Width="800px" EmptyDataText="No Record(s) found!">
                        <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                        <Columns>
                            <asp:TemplateField>
                            <HeaderTemplate>
                                EDIT
                            </HeaderTemplate>
                            <ItemTemplate>
                            <div style="white-space: nowrap; clear: none; width:auto; display: inline;">
                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_Employment">
                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png"  runat="server"  />
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                            <asp:BoundField DataField="EMPLOYMENT_STATUS" HeaderText="EMPLOYMENT_STATUS" SortExpression="EMPLOYMENT_STATUS" />
                            <asp:BoundField DataField="EMPLOYER_INSTITUTION_NAME" HeaderText="EMPLOYER_INSTITUTION_NAME" SortExpression="EMPLOYER_INSTITUTION_NAME" />
                            <asp:BoundField DataField="DATE_OF_EMPLOYMENT" HeaderText="DATE_OF_EMPLOYMENT"
                                SortExpression="DATE_OF_EMPLOYMENT" />
                            <asp:BoundField DataField="SECTOR_CLASS" HeaderText="SECTOR_CLASS"
                                SortExpression="SECTOR_CLASS" />
                            <asp:BoundField DataField="SUB_SECTOR" HeaderText="SUB_SECTOR"
                                SortExpression="SUB_SECTOR" />
                            <asp:BoundField DataField="NATURE_OF_BUSINESS_OCCUPATION" HeaderText="NATURE_OF_BUSINESS_OCCUPATION" SortExpression="NATURE_OF_BUSINESS_OCCUPATION" />
                            <asp:BoundField DataField="INDUSTRY_SEGMENT" HeaderText="INDUSTRY_SEGMENT"
                                SortExpression="INDUSTRY_SEGMENT" />
                            <asp:BoundField DataField="CREATED_DATE" HeaderText="CREATED_DATE"
                                SortExpression="CREATED_DATE" />
                            <asp:BoundField DataField="CREATED_BY" HeaderText="CREATED_BY"
                                SortExpression="CREATED_BY" />
                            <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY"
                                SortExpression="AUTHORISED_BY" />
                            <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE"
                                SortExpression="AUTHORISED_DATE" />
                            
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsEmplInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;EMPLOYMENT_STATUS&quot;, &quot;EMPLOYER_INSTITUTION_NAME&quot;, &quot;DATE_OF_EMPLOYMENT&quot;, &quot;SECTOR_CLASS&quot;, &quot;SUB_SECTOR&quot;, &quot;NATURE_OF_BUSINESS_OCCUPATION&quot;, &quot;INDUSTRY_SEGMENT&quot;, &quot;CREATED_DATE&quot;, &quot;CREATED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_EMPLOYMENT_DETAILS&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                        <SelectParameters>
   <asp:ControlParameter ControlID="txtCustNoEmpInfo" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
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
                           
                             <asp:Label ID="lblstatusTCAcct" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>TRUST CLIENT ACCOUNT</td>
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
                            <td class="col-sm-3 control-label" style="width: 250px">Trusts, Client Accounts</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList ID="rblTCAcct" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="rblTCAcct_SelectedIndexChanged" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem><%----%>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNoTCAcct" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Insider Relation </td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rblTCAcctInsiderRelation" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Name Of Beneficial Owner(S)</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustTCAcctBeneficialName" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Is The Applicant A Politically Exposed Person</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList ID="rblTCAcctPolExposed" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Spouse’s Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustTCAcctSpouseName" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Power Of Attorney</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rtlTCAcctPowerOfAttony" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Spouse’s Date Of Birth</td>
                            <td style="width: 211px">

                                <div class="input-group col-md-2">

                                    <asp:TextBox ID="txtCustTCAcctDOB" runat="server" 
                                        CssClass='form-control' placeholder="DD/MM/YYYY" ClientIDMode="Static" Width="200"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>

                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        var dp = $('#<%=txtCustTCAcctDOB.ClientID%>');
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
                            <td class="col-sm-3 control-label" style="width: 144px">Holder Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtTCAcctHoldersName" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Spouse’s Occupation</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustTCAcctOccptn" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Address</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtTCAcctAddress" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Sources Of Fund To The Account</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustTCAcctSrcOfFund" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Country</td>
                            <td style="width: 211px">
                                <asp:DropDownList ID="ddlTCAcctCountry" runat="server" CssClass="form-control" Width="200px" >
                                    
                                </asp:DropDownList></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px;">Expected Annual Income From Other Sources</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustTCAcctOtherScrIncome" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Nationality</td>
                            <td style="width: 211px">

                                <asp:DropDownList ID="ddlTCAcctNationality" runat="server" CssClass="form-control" Width="200px" >
                                    
                                </asp:DropDownList></td>
                            <td>

                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px;">Name Of Associated Business(Es)</td>
                            <td style="width: 211px; height: 54px;">

                                <asp:TextBox ID="txtCustTCAcctNameOfAssBiz" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Telephone Number</td>
                            <td>

                                <asp:TextBox ID="txtTCAcctTelPhone" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px;">Frequent International Traveler</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList ID="rblFreqIntTraveler" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 206px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 270px; left: 0px;">&nbsp;</td>
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
                                <asp:Button ID="btnCustTCAcctSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnCustTCAcctSave_Click"/>
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
                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderWidth="0px" CellPadding="0"
                        AutoGenerateColumns="False" DataSourceID="dsTCAcct" GridLines="None" PageSize="20" Width="1256px" EmptyDataText="No Record(s) found!"  CssClass="table" >
                         <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                        <Columns>

                             
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    EDIT
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                        <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                            white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_trustClientAcct">
                                            <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                            <asp:BoundField DataField="NAME_OF_BENEFICIAL_OWNER" HeaderText="NAME_OF_BENEFICIAL_OWNER" SortExpression="NAME_OF_BENEFICIAL_OWNER" />
                            <asp:BoundField DataField="SPOUSE_NAME" HeaderText="SPOUSE_NAME" SortExpression="SPOUSE_NAME" />
                            <asp:BoundField DataField="FREQ_INTERNATIONAL_TRAVELER" HeaderText="FREQ_INTERNATIONAL_TRAVELER" SortExpression="FREQ_INTERNATIONAL_TRAVELER" />
                            <asp:BoundField DataField="INSIDER_RELATION" HeaderText="INSIDER_RELATION" SortExpression="INSIDER_RELATION" />
                            <asp:BoundField DataField="POLITICALLY_EXPOSED_PERSON" HeaderText="POLITICALLY_EXPOSED_PERSON" SortExpression="POLITICALLY_EXPOSED_PERSON" />
                            <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                            <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                            <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                            <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsTCAcct" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;NAME_OF_BENEFICIAL_OWNER&quot;, &quot;SPOUSE_NAME&quot;, &quot;FREQ_INTERNATIONAL_TRAVELER&quot;, &quot;INSIDER_RELATION&quot;, &quot;POLITICALLY_EXPOSED_PERSON&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_TRUSTS_CLIENT_ACCOUNTS&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                        
<SelectParameters>
   <asp:ControlParameter ControlID="txtCustNoTCAcct" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
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
                           
                             <asp:Label ID="lblCustNoA4FinInc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="5"><legend>AUTHENTIFICATION FOR FINANCIAL INCLUSION</td>
                        </tr>
                        <tr>
                            <td style="width: 500px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px">*Customer No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNoA4FinInc" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px" >Is The Customer Socially Or Financially Disadvantaged?</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtA4FinIncSocFinDisadv" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="rbtA4FinIncSocFinDisadv_SelectedIndexChanged">
                                    <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td>

                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px">If Answer To The (I)Above Is Yes, State Other Documents Obtained In Line With The Bank’S Policy On Socially/ Financially Disadvantaged Customer Incompliance With Regulation 77 (4) Of Aml/Cft Regulation, 2013<br />
                            </td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtA4FinIncSocFinDoc" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                               </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px">Does The Customer Enjoy Tiered KYC Requirements?</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList ID="rbtA4FinIncEnjoyKYC" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="rbtA4FinIncEnjoyKYC_SelectedIndexChanged">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td>
</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px">If Answer To Question Above Is Yes, Identify The Customer Risk Category:</td>
                            <td style="width: 211px">

                                                                

                                <asp:DropDownList ID="ddlA4FinIncRiskCat" runat="server" CssClass="form-control" Width="200px">
                                    <asp:ListItem Value="">--SELECT--</asp:ListItem>
                                    <asp:ListItem Value="Low">Low</asp:ListItem>
                                    <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                    <asp:ListItem Value="High">High</asp:ListItem>
                                </asp:DropDownList>

                               

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px">Mandate Authorization/Combination Rule</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtA4FinIncMandRule" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px">Account Held With Other Banks</td>
                            <td colspan="4">

                               <%-- <asp:DropDownList ID="ddlA4FinIncAcctWithOtherBanks" runat="server" CssClass="form-control" Width="200px">
                                    
                                    
                                </asp:DropDownList>--%>
                                <div>
                                <asp:CheckBoxList ID="cblA4FinIncAcctWithOtherBanks" runat="server" Width="500px" CssClass="FormText" 
  RepeatDirection="Horizontal" RepeatColumns="2" BorderWidth="0" >
                                </asp:CheckBoxList>
</div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 500px">&nbsp;</td>
                            <td style="width: 211px">&nbsp;</td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 500px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnA4FinInc" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnA4FinInc_Click" />
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 500px">&nbsp;</td>
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
                    <asp:GridView ID="GridView8" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderWidth="0px" CellPadding="0"
                        AutoGenerateColumns="False" DataSourceID="sqlbtnA4FinInc"  GridLines="None" Width="1280px"   CssClass="table" EmptyDataText="No Record(s) found!">
                        <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    EDIT
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                        <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                            white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_A4FinInc">
                                            <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                            <asp:BoundField DataField="SOCIAL_FINANCIAL_DISADVTAGE" HeaderText="SOCIAL_FINANCIAL_DISADVTAGE" SortExpression="SOCIAL_FINANCIAL_DISADVTAGE" />
                            <asp:BoundField DataField="SOCIAL_FINANCIAL_DOCUMENTS" HeaderText="SOCIAL_FINANCIAL_DOCUMENTS" SortExpression="SOCIAL_FINANCIAL_DOCUMENTS" />
                            <asp:BoundField DataField="ENJOYED_TIERED_KYC" HeaderText="ENJOYED_TIERED_KYC" SortExpression="ENJOYED_TIERED_KYC" />
                            <asp:BoundField DataField="RISK_CATEGORY" HeaderText="RISK_CATEGORY" SortExpression="RISK_CATEGORY" />
                           
                            <asp:BoundField DataField="MANDATE_AUTH_COMBINE_RULE" HeaderText="MANDATE_AUTH_COMBINE_RULE" SortExpression="MANDATE_AUTH_COMBINE_RULE" />
                            <asp:BoundField DataField="ACCOUNT_WITH_OTHER_BANKS" HeaderText="ACCOUNT_WITH_OTHER_BANKS" SortExpression="ACCOUNT_WITH_OTHER_BANKS" />
                            <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                            <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                            <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlbtnA4FinInc" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;SOCIAL_FINANCIAL_DISADVTAGE&quot;, &quot;SOCIAL_FINANCIAL_DOCUMENTS&quot;, &quot;ENJOYED_TIERED_KYC&quot;, &quot;RISK_CATEGORY&quot;, &quot;MANDATE_AUTH_COMBINE_RULE&quot;, &quot;ACCOUNT_WITH_OTHER_BANKS&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_AUTH_FINANCE_INCLUSION&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                        <SelectParameters>
   <asp:ControlParameter ControlID="txtCustNoA4FinInc" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
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
                           
                             <asp:Label ID="lblCustAddtnalInfo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" colspan="2"><legend>ADDITIONAL INFORMATION</td>
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

                                <asp:TextBox ID="txtCustNoAddInfo" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" ></asp:TextBox>

                            </td>
                            <td>

                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Annual Salary/ Expected Annual Income</td>
                            <td style="width: 211px">
                                <asp:DropDownList ID="ddlAddInfoAnnualSalary" runat="server" CssClass="form-control" Width="200px" >
                                    
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtAddInfoAnnualSalary" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>--%>

                            </td>
                            <td>
</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Fax Number</td>
                            <td style="width: 211px">

                               

                                <asp:TextBox ID="txtAddInfoFaxNumber" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                               

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
                            <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnAdditionalInfo" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnAdditionalInfo_Click" />
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
                    <asp:GridView ID="GridView9" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderWidth="0px" CellPadding="0"
                        AutoGenerateColumns="False" DataSourceID="SqlAddtnalInfo"  GridLines="None" Width="1280px"   CssClass="table" EmptyDataText="No Record(s) found!">
                        <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    EDIT
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                        <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_NO")%>' Style="margin: 0px 0px 0px 5px;
                                            white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_additionalInfo">
                                            <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" SortExpression="CUSTOMER_NO" />
                            <asp:BoundField DataField="ANNUAL_SALARY_EXPECTED_INC" HeaderText="ANNUAL_SALARY_EXPECTED_INC" SortExpression="ANNUAL_SALARY_EXPECTED_INC" />
                            <asp:BoundField DataField="FAX_NUMBER" HeaderText="FAX_NUMBER" SortExpression="FAX_NUMBER" />
                            <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                            <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                           
                            <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                            <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlAddtnalInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;ANNUAL_SALARY_EXPECTED_INC&quot;, &quot;FAX_NUMBER&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; FROM &quot;CDMA_ADDITIONAL_INFORMATION&quot; WHERE (&quot;CUSTOMER_NO&quot; = :CUSTOMER_NO)">
                        <SelectParameters>
   <asp:ControlParameter ControlID="txtCustNoAddInfo" Name="CUSTOMER_NO" PropertyName="Text" Type="String" />
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


     <script type="text/javascript">
         $(document).ready(function () {
             var tab = document.getElementById('<%=hidTAB.ClientID%>').value;    //"#tab2";
             $('#myTabs a[href="' + tab + '"]').tab('show');
             //alert(tab);
             // alert($('#myTabs a[href="' + tab + '"]').tab('show'));
         });
</script>
</asp:Content>
