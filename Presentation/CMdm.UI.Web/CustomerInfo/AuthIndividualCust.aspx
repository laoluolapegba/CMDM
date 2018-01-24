<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuthIndividualCust.aspx.cs" Inherits="Cdma.Web.CustomerInfo.AuthIndividualCust" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Content/assets/js/jquery/jquery.min.js"></script>
    <script src="../Content/assets/js/jquery-ui/jquery-ui.js"></script>
    <link href="../Content/assets/css/jquery-ui.css" rel="stylesheet" />


    <script type="text/javascript">
        $(document).ready(function () {
            var tab = document.getElementById('<%= hidTAB.ClientID%>').value;
            $('#myTabs a[href="' + tab + '"]').tab('show');
        });
</script>

    <script type="text/javascript">
        $("[id*=btnRejectCustInfo]").live("click", function () {
            $("#modal_dialog").dialog({
                title: "Checker's rejection comment",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    },
                    Submit: function SubmitComment() {
                        $(this).dialog('close');
                        $("[id*=HidCustInfoComment]").val($("[id*=txtCustInfoComment]").val());
                        $("#ButtonID1").click();             
                   }
                },
                modal: true
            });
            return false;
        });
/////////////////////////////////////////////////////////////
        $("[id*=btnRejectAI]").live("click", function () {
            $("#modal_dialog2").dialog({
                title: "Checker's rejection comment",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    },
                    Submit: function SubmitComment() {
                        $(this).dialog('close');
                        $("[id*=HidCustAIComment]").val($("[id*=txtCustAIComment]").val());
                        $("#ButtonID2").click();
                    }
                },
                modal: true
            });
            return false;
        });
/////////////////////////////////////////////////////////////////
        $("[id*=btnRejectIncome]").live("click", function () {
            $("#modal_dialog3").dialog({
                title: "Checker's rejection comment",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    },
                    Submit: function SubmitComment() {
                        $(this).dialog('close');
                        $("[id*=HidCustIncomeComment]").val($("[id*=txtCustIncomeComment]").val());
                        $("#ButtonID3").click();
                    }
                },
                modal: true
            });
            return false;
        });
        /////////////////////////////////////////////////////////////////
        $("[id*=btnRejectNOK]").live("click", function () {
            $("#modal_dialog4").dialog({
                title: "Checker's rejection comment",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    },
                    Submit: function SubmitComment() {
                        $(this).dialog('close');
                        $("[id*=HidCustNOKComment]").val($("[id*=txtCustNOKComment]").val());
                        $("#ButtonID4").click();
                    }
                },
                modal: true
            });
            return false;
        });
        /////////////////////////////////////////////////////////////////
        $("[id*=btnRejectForDet]").live("click", function () {
            $("#modal_dialog5").dialog({
                title: "Checker's rejection comment",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    },
                    Submit: function SubmitComment() {
                        $(this).dialog('close');
                        $("[id*=HidCustForDetComment]").val($("[id*=txtCustForDetComment]").val());
                        $("#ButtonID5").click();
                    }
                },
                modal: true
            });
            return false;
        });
        /////////////////////////////////////////////////////////////////
        $("[id*=btnRejectJurat]").live("click", function () {
            $("#modal_dialog6").dialog({
                title: "Checker's rejection comment",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    },
                    Submit: function SubmitComment() {
                        $(this).dialog('close');
                        $("[id*=HidCustJuratComment]").val($("[id*=txtCustJuratComment]").val());
                        $("#ButtonID6").click();
                    }
                },
                modal: true
            });
            return false;
        });
        /////////////////////////////////////////////////////////////////
        $("[id*=btnRejectEI]").live("click", function () {
            $("#modal_dialog7").dialog({
                title: "Checker's rejection comment",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    },
                    Submit: function SubmitComment() {
                        $(this).dialog('close');
                        $("[id*=HidCustEIComment]").val($("[id*=txtCustEIComment]").val());
                        $("#ButtonID7").click();
                    }
                },
                modal: true
            });
            return false;
        });
        /////////////////////////////////////////////////////////////////
        $("[id*=btnRejectTCAcc]").live("click", function () {
            $("#modal_dialog8").dialog({
                title: "Checker's rejection comment",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    },
                    Submit: function SubmitComment() {
                        $(this).dialog('close');
                        $("[id*=HidCustTCAcctComment]").val($("[id*=txtCustTCAcctComment]").val());
                        $("#ButtonID8").click();
                    }
                },
                modal: true
            });
            return false;
        });
        /////////////////////////////////////////////////////////////////
        $("[id*=btnRejectFinInc]").live("click", function () {
            $("#modal_dialog9").dialog({
                title: "Checker's rejection comment",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    },
                    Submit: function SubmitComment() {
                        $(this).dialog('close');
                        $("[id*=HidCustFinIncComment]").val($("[id*=txtCustFinIncComment]").val());
                        $("#ButtonID9").click();
                    }
                },
                modal: true
            });
            return false;
        });
        /////////////////////////////////////////////////////////////////
        $("[id*=btnRejectAddInfo]").live("click", function () {
            $("#modal_dialog10").dialog({
                title: "Checker's rejection comment",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    },
                    Submit: function SubmitComment() {
                        $(this).dialog('close');
                        $("[id*=HidCustAddInfoComment]").val($("[id*=txtCustAddInfoComment]").val());
                        $("#ButtonID10").click();
                    }
                },
                modal: true
            });
            return false;
        });
</script>

    <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Authorize Individual Customer</h3>
        </div>
        <div class="widget-content">
            <ul class="nav nav-tabs" role="tablist" id="myTabs">
                <li id="Corp1" runat="server"><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="true"><i class="fa fa-user"></i>Biodata Information</a></li>
                <li id="Corp7" runat="server"><a href="#tab7" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-money"></i>Account Information</a></li>
                <li id="Corp2" runat="server"><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-money"></i>Customer Income</a></li>
                <li id="Corp4" runat="server"><a href="#tab4" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-user-plus"></i>Next of Kin</a></li>
                <li id="Corp5" runat="server"><a href="#tab5" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-plane"></i>Foreigner Details</a></li>
                <li id="Corp6" runat="server"><a href="#tab6" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-legal"></i>Jurat</a></li>
                <li id="Corp8" runat="server"><a href="#tab8" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-suitcase"></i>Employee Info</a></li>
                <li id="Corp3" runat="server"><a href="#tab3" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-bank"></i>Trusts/Client Accounts</a></li>
                <li id="Corp9" runat="server"><a href="#tab9" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-suitcase"></i>Authentification for Financial Inclustion</a></li>
                <li id="Corp10" runat="server"><a href="#tab10" role="tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-bank"></i>Additional Information</a></li>
            </ul>

            <div class="tab-content">
                <div class="tab-pane fade  active in"    id="tab1">

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
                                    <asp:TextBox ID="txtCustInfoID" runat="server" CssClass="form-control" ></asp:TextBox>
                                    <span class="input-group-btn">
                                        <%--<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary fa fa-search" OnClick="btnSearch_Click" />--%>
                                    </span>
                                </div>
                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px">&nbsp;</td>
                            <td style="width: 211px"><asp:TextBox ID="txtCurrentUser" runat="server" CssClass="form-control" Visible="false" ></asp:TextBox></td>
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

                                <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">*Title</td>
                            <td>

                                
<asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Height="40px" Width="200px"  ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*First Name</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">Other Name</td>
                            <td>
                                <asp:TextBox ID="txtOtherName" runat="server" CssClass="form-control" Height="40px" Width="200px"  ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">Nickname/Alias</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtNickname" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Sex</td>
                            <td>
<asp:TextBox ID="txtSex" runat="server" CssClass="form-control" Height="40px" Width="200px"  ReadOnly="true" ></asp:TextBox>

                                

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*Date of Birth</td>
                            <td style="width: 211px">

                                

                                    <asp:TextBox ID="txtDateOfBirth" runat="server" Width="180"  CssClass="form-control"  ReadOnly="true" ></asp:TextBox>
                                    
                               

                            <td class="col-sm-3 control-label" style="width: 206px">&nbsp;*Age</td>
                            <td>

                                <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*Place of Birth</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtPlacefBirth" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">*Country of Birth</td>
                            <td>

                               
                                <asp:TextBox ID="txtCountryofBirth" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px; height: 66px;">*Nationality</td>
                            <td style="width: 211px; height: 66px;">
                                
                                <asp:TextBox ID="txtNationality" runat="server" CssClass="form-control" Height="40px" Width="200px"  ReadOnly="true" ></asp:TextBox>
                                


                               </td> 
                            <td class="col-sm-3 control-label" style="width: 144px; height: 66px;">*State of Origin</td>
                            <td style="height: 66px">
                        

                                <asp:TextBox ID="txtStateOfOrigin" runat="server" CssClass="form-control" Height="40px"  Width="200px" ReadOnly="true" ></asp:TextBox>
                     



                            </td>
                            <td style="height: 66px"></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*Marital Status</td>
                            <td style="width: 211px">
<asp:TextBox ID="txtMaritalStatus" runat="server" CssClass="form-control" Height="40px"   Width="200px" ReadOnly="true" ></asp:TextBox>
                                

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Mothers Maiden Name</td>
                            <td>

                                <asp:TextBox ID="txtMothersMaidenName" runat="server" CssClass="form-control" Height="40px"   Width="200px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">No of Children</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtNoOfChildren" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Religion</td>
                            <td>
<asp:TextBox ID="txtReligion" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
                                

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">Complexion</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtComplexion" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 200px" >*Disablility</td>
                            <td>
<asp:TextBox ID="txtDisability" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
                               

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
 <asp:TextBox ID="txtCountryofResidence" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
                               



                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">*State of Residence</td>
                            <td>

                           
                                <asp:TextBox ID="txtStateOfResidence" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>



                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px">*LGA of Residence</td>
                            <td style="width: 211px">

                                
                                <asp:TextBox ID="txtLGAOfResidence" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>



                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">*City/Town of Residence</td>
                            <td>

                                <asp:TextBox ID="txtCityofResidence" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px" rowspan="2">*Residential Address</td>
                            <td style="width: 211px" rowspan="2">
                                <asp:TextBox ID="txtResidentialAddy" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px" rowspan="2">
                                 *Nearest bus stop/Landmark
                            </td>
                            <td rowspan="2">
                                <asp:TextBox ID="txtNearestBusStop" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 208px"> Residence owned or rented&nbsp;</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList Enabled="false"  ID="rbtOwnedorRented" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">
                                Zip Postal Code</td>
                            <td>
                                <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
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
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
                                
                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px; height: 18px; top: -935px; left: -42px;">Email</td>
                            <td style="height: 18px">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
                                
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 206px">Mailing Address</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtMailingAddy" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
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
                                <asp:TextBox ID="txtIDType" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly ="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 206px">*Identification No</td>
                            <td>
                                <asp:TextBox ID="txtIDNo" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 206px">*ID Issue Date</td>
                           

                               <td>     <asp:TextBox ID="txtIDIssueDate" runat="server" Width="180"  CssClass='form-control'   ReadOnly="true" ></asp:TextBox>
                                   
                                
                                            </td>
                            
                            <td class="col-sm-3 control-label" style="width: 206px">*ID Expiry Date</td>
                            <td> 

                                    <asp:TextBox ID="txtIDExpiryDate" runat="server" Width="180" CssClass='form-control' ReadOnly="true" ></asp:TextBox>
                                  
                              

                               </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 206px">*Place of Issuance</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtPlaceOfIssue" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
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
                                <asp:TextBox ID="txtTINNo" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true" ></asp:TextBox>
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
                            <td class="datepicker-inline" style="width: 208px"> &nbsp;</td>
                            <td style="width: 211px">
                                <asp:Button ID="btnApproveCustInfo" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnApproveCustInfo_Click" />
                                <asp:Button ID="btnRejectCustInfo" runat="server" Text="Reject" CssClass="btn btn-default" />
                                <asp:Button ID="ButtonID1" runat="server" OnClick="btnRejectCustInfo_Click" style="visibility: hidden; display: none;" ClientIDMode="Static" />
                            </td><%--OnClientClick ="Comment()" --%>

                            <td style="width: 206px">    
                               <div id="modal_dialog" style="display: none">
                                                
                                  <asp:TextBox ID="txtCustInfoComment" runat="server" TextMode="MultiLine" Width="270px" Height="80px" ></asp:TextBox>
                            </div>
                                <asp:HiddenField ID="HidCustInfoComment" runat="server" />
                            </td>
                            
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="datepicker-inline" style="width: 208px"> <asp:HiddenField ID="hidTAB" runat="server" Value="#tab1" /></td>
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
                            ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;SURNAME&quot;, &quot;FIRST_NAME&quot;, &quot;OTHER_NAME&quot;, &quot;DATE_OF_BIRTH&quot;, &quot;STATE_OF_ORIGIN&quot;, &quot;NATIONALITY&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; 
                            FROM &quot;TMP_INDIVIDUAL_BIO_DATA&quot; where last_modified_by IN (select p.USER_ID from CM_USER_PROFILE p , 
(select * from CM_MAKER_CHECKER_XREF where  CHECKER_ID in (select PROFILE_ID from CM_USER_PROFILE p where p.USER_ID = :CurrentUser))x 
where (x.MAKER_ID = p.profile_id )) AND ROWNUM <= 1000">

                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCurrentUser" Name="CurrentUser" Type="String" />
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

                                <asp:TextBox ID="txtCustNoAI" runat="server" CssClass="form-control" Height="40px" ReadOnly="True"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*BVN Number</td>
                            <td>

                                <asp:TextBox ID="txtCustAIBVNNo" runat="server" CssClass="form-control" Height="40px" MaxLength="11" ReadOnly="true" ></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Account Holder</td>
                            <td style="width: 243px">
                                <asp:RadioButtonList Enabled="false"  ID="rbtCustAIAccHolders" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*CAV Required</td>
                            <td>
                            <asp:RadioButtonList Enabled="false" ID="rblCAVRequired" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                                </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Type of Account</td>
                            <td style="width: 243px">
                                <asp:TextBox ID="txtCustAITypeOfAcc" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                                

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer Ic</td>
                            <td>

                                <asp:TextBox ID="txtCustAICustIc" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Account Number</td>
                            <td style="width: 243px">
                                <asp:TextBox ID="txtCustAIAccNo" runat="server" CssClass="form-control" Height="40px" Enabled="True" ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer Segment</td>
                            <td>

                                <asp:TextBox ID="txtCustAICustSeg" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                              
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Account Officer</td>
                            <td style="width: 243px">

                               <asp:TextBox ID="txtCustAIAccOfficer" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>


                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Customer Type</td>
                            <td>
                                <asp:TextBox ID="txtCustAICusType" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Account Title</td>
                            <td style="width: 243px">

                                <asp:TextBox ID="txtCustAIAccTitle" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 408px; left: 1028px;">*Online Transfer Limit </td>
                            <td>

                                <asp:RadioButtonList Enabled="false" ID="rbtASROnlineTraxLimit" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                                
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Branch</td>
                            <td style="width: 243px">
                                <asp:TextBox ID="txtCustAIBranch" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                                

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 53px;">*Online Transfer Limit Range</td>
                            <td>
                                <asp:TextBox ID="txtCustAIOnlineTrnsfLimit" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                                

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Branch Class</td>
                            <td style="width: 243px">
                                <asp:TextBox ID="txtCustAIBranchClass" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                               

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 408px; left: 1028px;">*Operating Instruction</td>
                            <td>

                                <asp:TextBox ID="txtCustAIOpInsttn" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px; height: 53px;">*Business Division</td>
                            <td style="width: 243px; height: 53px;">
                                <asp:TextBox ID="txtCustAIBizDiv" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                               

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px; top: 408px; left: 1028px;">Originating Branch</td>
                            <td>
                                <asp:TextBox ID="txtCustAIOriginatingBranch" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                               
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Business Segment</td>
                            <td style="width: 243px">
                                <asp:TextBox ID="txtCustBizSeg" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                                </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Business Size</td>
                            <td style="width: 243px">
                                <asp:TextBox ID="txtCustBizSize" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                                
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
                                <asp:RadioButtonList Enabled="false" ID="rbtASRCardRef" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Cheque Confirmation</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList Enabled="false" ID="rbtASRChequeConfmtn" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Electronic Banking Preference</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rbtASREBP" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Cheque Confirmation Threshold</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rbtASRChequeConfmtnThreshold" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Statement Preferences</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rbtASRStatementPref" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Cheque Confirmation Threshold Range</td>
                            <td style="width: 211px">
                              <asp:TextBox ID="txtASRChequeConfmtnThresholdRange" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>   
                            
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Transaction Alert Preference</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rbtASRTransAlertPref" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Token</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rbtASRToken" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Statement Frequency</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rbtASRStatementFreq" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Account Signatory</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtASRAcctSignitory" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Cheque Book Requisition</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rbtASRChequeBookReqtn" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Second Signatory</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtASR2ndAcctSignitory" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Cheque Leaves Required</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rbtASRChequeLeaveReq" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
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
                                <%--<asp:Button ID="btnCustAISave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnAccountInfo_Click" />--%>
                                 <asp:Button ID="btnApproveAI" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnApproveAI_Click" />
                                <asp:Button ID="btnRejectAI" runat="server" Text="Reject" CssClass="btn btn-default"  />
                            <asp:Button ID="ButtonID2" runat="server" OnClick="btnRejectAI_Click" style="visibility: hidden; display: none;" ClientIDMode="Static" />
                            </td>
                            <td style="width: 206px">    
                               <div id="modal_dialog2" style="display: none"  >                                     
                                  <asp:TextBox ID="txtCustAIComment" runat="server" TextMode="MultiLine" Width="270px" Height="80px" ></asp:TextBox>
                            </div>
                                <asp:HiddenField ID="HidCustAIComment" runat="server" />
 </td>
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
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                        SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;TYPE_OF_ACCOUNT&quot;, &quot;ACCOUNT_NUMBER&quot;, &quot;ACCOUNT_TITLE&quot;, &quot;BRANCH&quot;, &quot;BVN_NUMBER&quot;, &quot;CUSTOMER_TYPE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot;, &quot;LAST_MODIFIED_DATE&quot; 
                        FROM &quot;TMP_ACCOUNT_INFO&quot; where last_modified_by IN (select p.USER_ID from CM_USER_PROFILE p , 
(select * from CM_MAKER_CHECKER_XREF where  CHECKER_ID in (select PROFILE_ID from CM_USER_PROFILE p where p.USER_ID = :CurrentUser))x 
where (x.MAKER_ID = p.profile_id ))">

                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCurrentUser" Name="CurrentUser" Type="String" />
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
                                <asp:TextBox ID="txtCustIncomeBand" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                               
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Initial Deposit</td>
                            <td style="width: 211px">
<asp:TextBox ID="txtCustIncomeInitDeposit" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                                

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
                                <%--<asp:Button ID="btnCustIncomeSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnCustIncome_Click"  />--%>
                                 <asp:Button ID="btnApproveIncome" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnApproveIncome_Click" />
                                <asp:Button ID="btnRejectIncome" runat="server" Text="Reject" CssClass="btn btn-default"  />
                                <asp:Button ID="ButtonID3" runat="server" OnClick="btnRejectIncome_Click" style="visibility: hidden; display: none;" ClientIDMode="Static" />
                            </td>
                            <td style="width: 206px">    
                               <div id="modal_dialog3" style="display: none">                                       
                                  <asp:TextBox ID="txtCustIncomeComment" runat="server" TextMode="MultiLine" Width="270px" Height="80px" ></asp:TextBox>
                            </div>
                                <asp:HiddenField ID="HidCustIncomeComment" runat="server" />
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
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                        SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;INCOME_BAND&quot;, &quot;INITIAL_DEPOSIT&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; 
                        FROM &quot;TMP_CUSTOMER_INCOME&quot; where last_modified_by IN (select p.USER_ID from CM_USER_PROFILE p , 
(select * from CM_MAKER_CHECKER_XREF where  CHECKER_ID in (select PROFILE_ID from CM_USER_PROFILE p where p.USER_ID = :CurrentUser))x 
where (x.MAKER_ID = p.profile_id ))">

                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCurrentUser" Name="CurrentUser" Type="String" />
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

                                <asp:TextBox ID="txtCustNoNOK" ReadOnly="True" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Identification Type</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCustNOKIdType" ReadOnly="True" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
                                   

                               

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Title</td>
                            <td style="width: 211px">
                                 <asp:TextBox ID="txtCustNOKTitle" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                                
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*ID Issue Date</td>
                            <td style="width: 211px">
                               

                                    <asp:TextBox ID="txtCustNOKIssuedDate" runat="server"  CssClass='form-control' ReadOnly="true" ></asp:TextBox>
                                    
                                
</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Surname</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKSurname" runat="server" CssClass="form-control" Height="40px" Enabled="True" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*ID Expiry Date</td>
                            <td style="width: 211px">
 <asp:TextBox ID="txtCustNOKExpiryDate" runat="server" CssClass='form-control' ReadOnly="true" ></asp:TextBox>
 
                                </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*First Name</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCustNOKfirstName" runat="server" CssClass="form-control" Height="40px" Enabled="True"  ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Passport/Resident Permit No</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCustNOKPermitNo" runat="server" CssClass="form-control" Height="40px" Enabled="True" ReadOnly="true"  ></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Other Names</td>
                            <td style="width: 211px">

                               


                                <asp:TextBox ID="txtCustNOKOtherName" runat="server" CssClass="form-control" Height="40px" Enabled="True" ReadOnly="true" ></asp:TextBox>

                               


                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Place of Issue</td>
                            <td style="width: 211px">

                                
                                <asp:TextBox ID="txtCustNOKPlaceOfIssue" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>


                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Date of Birth</td>
                            <td style="width: 211px">

                           

                                    <asp:TextBox ID="txtCustNOKDateOfBirth" runat="server" CssClass='form-control'  ReadOnly="true" ></asp:TextBox>
                      
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Street Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKStreetName" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Sex</td>
                            <td style="width: 211px">
<asp:TextBox ID="txtCustNOKSex" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>
                                
                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Nearest Busstop/Landmark</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKBusstop" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Relationship</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtCustNOKReltnship" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>
                                

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Zip/Postal Code</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKZipCode" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Office Phone No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKOfficeNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Country</td>
                            <td style="width: 211px">
<asp:TextBox ID="txtCustNOKCountry" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>
                               

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Mobile No</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKMobileNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*State</td>
                            <td style="width: 211px">
                                

                                <asp:TextBox ID="txtCustNOKState" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Email Address</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKEmail" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*L.G.A</td>
                            <td style="width: 211px">
                                

                                <asp:TextBox ID="txtCustNOKLGA" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*House No</td>
                            <td style="width: 211px">

                                    <asp:TextBox ID="txtCustNOKHouseNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*City/Town</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustNOKCity" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

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
                                <%--<asp:Button ID="btnCustNOKSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnNextofKin_Click" />--%>
                                 <asp:Button ID="btnApproveNOK" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnApproveNOK_Click" />
                                <asp:Button ID="btnRejectNOK" runat="server" Text="Reject" CssClass="btn btn-default"  />
                             <asp:Button ID="ButtonID4" runat="server" OnClick="btnRejectNOK_Click" style="visibility: hidden; display: none;" ClientIDMode="Static" />
                            </td>
                            <td style="width: 206px">    
                               <div id="modal_dialog4" style="display: none"   >                                    
                                  <asp:TextBox ID="txtCustNOKComment" runat="server" TextMode="MultiLine" Width="270px" Height="80px" ></asp:TextBox>
                            </div>
                                <asp:HiddenField ID="HidCustNOKComment" runat="server" />
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
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                        SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;TITLE&quot;, &quot;SURNAME&quot;, &quot;FIRST_NAME&quot;, &quot;DATE_OF_BIRTH&quot;, &quot;RELATIONSHIP&quot;, &quot;MOBILE_NO&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; 
                        FROM &quot;TMP_INDIVIDUAL_NEXT_OF_KIN&quot; where last_modified_by IN (select p.USER_ID from CM_USER_PROFILE p , 
(select * from CM_MAKER_CHECKER_XREF where  CHECKER_ID in (select PROFILE_ID from CM_USER_PROFILE p where p.USER_ID = :CurrentUser))x 
where (x.MAKER_ID = p.profile_id ))">

                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCurrentUser" Name="CurrentUser" Type="String" />
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

                                <asp:TextBox ID="txtCustNoForgner" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" Width="200px"  ></asp:TextBox>

                            </td>
                            <td rowspan="2">&nbsp;</td>
                            <td rowspan="2">&nbsp;</td>
                            <td rowspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Foreigner</td>
                            <td style="width: 73px">
                                <asp:RadioButtonList Enabled="false" ID="rbtForeigner" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px; height: 54px;">*Multiple Citizenship</td>
                            <td style="height: 54px; width: 73px;">

                                <asp:RadioButtonList Enabled="false" ID="rbtMultipleCitizenship" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
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
                                <asp:TextBox ID="txtCustFPassPermit" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>
                            </td>
                            <td class="col-sm-3 control-label">*Foreign Phone No</td>
                            <td>

                                <asp:TextBox ID="txtCustfForeignPhoneNo" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Passport / Permit Issue Date</td>
                            <td style="width: 73px">
            <asp:TextBox ID="txtCustFIssueDate" runat="server" CssClass='form-control' Width="200px" ReadOnly="true"  ></asp:TextBox>
                           </td>
                            <td class="col-sm-3 control-label" style="top: 162px; left: 1064px;">Zip Postal Code</td>
                            <td>

                                
                                <asp:TextBox ID="txtCustfZipPostalCode" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>

                                
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*Passport / Permit Expiry Date</td>
                            <td style="width: 73px">
    <asp:TextBox ID="txtCustFExpiryDate" runat="server" CssClass='form-control' Width="200px" ReadOnly="true"  ></asp:TextBox>
                                                          </td>
                            <td class="col-sm-3 control-label" style="top: 162px; left: 1064px;">*Purpose of Account</td>
                            <td>

                                
                                <asp:TextBox ID="txtCustfPurposeOfAcc" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>

                                
                               </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">Foreign Address</td>
                            <td style="width: 73px">

                                <asp:TextBox ID="txtCustfForeignAddy" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 144px">*City</td>
                            <td style="width: 73px">

                                <asp:TextBox ID="txtCustfCity" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>

                                </td>
                            <td class="col-sm-3 control-label">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" >*Country</td>
                            <td style="height: 54px; width: 73px;">
<asp:TextBox ID="txtCustfCountry" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>
                                

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
                                <%--<asp:Button ID="btnCustfSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnForeDet_Click" />--%>
                                 <asp:Button ID="btnApproveForDet" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnApproveForDet_Click" />
                                <asp:Button ID="btnRejectForDet" runat="server" Text="Reject" CssClass="btn btn-default"  />
                            <asp:Button ID="ButtonID5" runat="server" OnClick="btnRejectForDet_Click" style="visibility: hidden; display: none;" ClientIDMode="Static" />
                            </td>
                            <td style="width: 206px">    
                               <div id="modal_dialog5" style="display: none" >                                      
                                  <asp:TextBox ID="txtCustForDetComment" runat="server" TextMode="MultiLine" Width="270px" Height="80px" ></asp:TextBox>
                            </div>
                                <asp:HiddenField ID="HidCustForDetComment" runat="server" />
 </td>
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
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                        SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;PASSPORT_RESIDENCE_PERMIT&quot;, &quot;PERMIT_ISSUE_DATE&quot;, &quot;PERMIT_EXPIRY_DATE&quot;, &quot;FOREIGN_ADDRESS&quot;, &quot;COUNTRY&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_DATE&quot;, &quot;IP_ADDRESS&quot; 
                        FROM &quot;TMP_FOREIGN_DETAILS&quot; where last_modified_by IN (select p.USER_ID from CM_USER_PROFILE p , 
(select * from CM_MAKER_CHECKER_XREF where  CHECKER_ID in (select PROFILE_ID from CM_USER_PROFILE p where p.USER_ID = :CurrentUser))x 
where (x.MAKER_ID = p.profile_id ))">

                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCurrentUser" Name="CurrentUser" Type="String" />
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
                               
                                    <asp:TextBox ID="txtCustJDateOfOath" runat="server" CssClass='form-control'  Width="200" ReadOnly="true"  ></asp:TextBox>
                              
                                </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Name Of Interpreter</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustJNameOfInerpreter" runat="server" CssClass="form-control" Height="40px" Width="300" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td>
</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Address Of Interpreter</td>
                            <td style="width: 211px">

                               

                                <asp:TextBox ID="txtCustJAddyOfInterperter" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                               

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Telephone No </td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustJPhoneNo" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Language Of Interpretation</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustJLangOfInterpretation" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

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
                                
                                <asp:Button ID="btnApproveJurat" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnApproveJurat_Click" />
                                <asp:Button ID="btnRejectJurat" runat="server" Text="Reject" CssClass="btn btn-default" />
                                <asp:Button ID="ButtonID6" runat="server" OnClick="btnRejectJurat_Click" style="visibility: hidden; display: none;" ClientIDMode="Static" />
                            </td>
                            <td style="width: 206px">    
                               <div id="modal_dialog6" style="display: none"  >                                     
                                  <asp:TextBox ID="txtCustJuratComment" runat="server" TextMode="MultiLine" Width="270px" Height="80px" ></asp:TextBox>
                            </div>
                                <asp:HiddenField ID="HidCustJuratComment" runat="server" />
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
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                        SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;DATE_OF_OATH&quot;, &quot;NAME_OF_INTERPRETER&quot;, &quot;ADDRESS_OF_INTERPRETER&quot;, &quot;TELEPHONE_NO&quot;, &quot;LANGUAGE_OF_INTERPRETATION&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; 
                        FROM &quot;TMP_JURAT&quot; where last_modified_by IN (select p.USER_ID from CM_USER_PROFILE p , 
(select * from CM_MAKER_CHECKER_XREF where  CHECKER_ID in (select PROFILE_ID from CM_USER_PROFILE p where p.USER_ID = :CurrentUser))x 
where (x.MAKER_ID = p.profile_id ))">

                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCurrentUser" Name="CurrentUser" Type="String" />
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
                <div class="tab-pane fade"  id="tab8">
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

                                <asp:TextBox ID="txtCustNoEmpInfo" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Employment Status</td>
                            <td>
 <asp:TextBox ID="txtCustEIEmpStatus" ReadOnly="True" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                            
                            </td>
                            <td>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Employer Name/Institution Name</td>
                            <td>

                                <asp:TextBox ID="txtCustEIEmployerName" runat="server" CssClass="form-control" Height="40px"  Width="200px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Date of Employment</td>
                            <td>

                                    <asp:TextBox ID="txtCustEIDateOfEmp" runat="server" CssClass='form-control'  Width="200" ReadOnly="true"  ></asp:TextBox>
                            
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Sector Class</td>
                            <td>
<asp:TextBox ID="txtCustEISectorClass" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>
                            

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Sub Sector</td>
                            <td>
 <asp:TextBox ID="txtCustEISubSector" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>
                            

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Nature of Business</td>
                            <td>


                                <asp:TextBox ID="txtCustEINatureOfBiz" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Industry Segment</td>
                            <td>
<asp:TextBox ID="txtCustEIIndustrySeg" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>
                            

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
                               <%-- <asp:Button ID="btnCustEISave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnEmpInfo_Click"/>--%>
                                <asp:Button ID="btnApproveEI" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnApproveEI_Click" />
                                <asp:Button ID="btnRejectEI" runat="server" Text="Reject" CssClass="btn btn-default" />
                             <asp:Button ID="ButtonID7" runat="server"  OnClick="btnRejectEI_Click" style="visibility: hidden; display: none;" ClientIDMode="Static" />
                            </td>
                            <td style="width: 206px">    
                               <div id="modal_dialog7" style="display: none">                                     
                                  <asp:TextBox ID="txtCustEIComment" runat="server" TextMode="MultiLine" Width="270px" Height="80px" ></asp:TextBox>
                            </div>
                                <asp:HiddenField ID="HidCustEIComment" runat="server" />
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
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                        SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;EMPLOYMENT_STATUS&quot;, &quot;EMPLOYER_INSTITUTION_NAME&quot;, &quot;DATE_OF_EMPLOYMENT&quot;, &quot;SECTOR_CLASS&quot;, &quot;SUB_SECTOR&quot;, &quot;NATURE_OF_BUSINESS_OCCUPATION&quot;, &quot;INDUSTRY_SEGMENT&quot;, &quot;CREATED_DATE&quot;, &quot;CREATED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; 
                        FROM &quot;TMP_EMPLOYMENT_DETAILS&quot; where last_modified_by IN (select p.USER_ID from CM_USER_PROFILE p , 
(select * from CM_MAKER_CHECKER_XREF where  CHECKER_ID in (select PROFILE_ID from CM_USER_PROFILE p where p.USER_ID = :CurrentUser))x 
where (x.MAKER_ID = p.profile_id ))">

                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCurrentUser" Name="CurrentUser" Type="String" />
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
                                <asp:RadioButtonList Enabled="false" ID="rblTCAcct" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
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

                                <asp:RadioButtonList Enabled="false" ID="rblTCAcctInsiderRelation" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Name Of Beneficial Owner(S)</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustTCAcctBeneficialName" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">*Is The Applicant A Politically Exposed Person</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList Enabled="false" ID="rblTCAcctPolExposed" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Spouse’s Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustTCAcctSpouseName" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Power Of Attorney</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rtlTCAcctPowerOfAttony" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Spouse’s Date Of Birth</td>
                            <td style="width: 211px">

                                    <asp:TextBox ID="txtCustTCAcctDOB" runat="server" CssClass='form-control' Width="200" ReadOnly="true"  ></asp:TextBox>
                                
                                </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Holder Name</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtTCAcctHoldersName" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Spouse’s Occupation</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustTCAcctOccptn" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Address</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtTCAcctAddress" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Sources Of Fund To The Account</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustTCAcctSrcOfFund" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Country</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtTCAcctCountry" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                                
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px;">Expected Annual Income From Other Sources</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtCustTCAcctOtherScrIncome" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Nationality</td>
                            <td style="width: 211px">
                                <asp:TextBox ID="txtTCAcctNationality" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>
                               </td>
                            <td>

                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px;">Name Of Associated Business(Es)</td>
                            <td style="width: 211px; height: 54px;">

                                <asp:TextBox ID="txtCustTCAcctNameOfAssBiz" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                            </td>
                            <td class="col-sm-3 control-label" style="width: 144px">Telephone Number</td>
                            <td>

                                <asp:TextBox ID="txtTCAcctTelPhone" runat="server" CssClass="form-control" Height="40px" Width="200px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px;">Frequent International Traveler</td>
                            <td style="width: 211px">
                                <asp:RadioButtonList Enabled="false" ID="rblFreqIntTraveler" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
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
                                <%--<asp:Button ID="btnCustTCAcctSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnCustTCAcctSave_Click"/>--%>
                                <asp:Button ID="btnAproveTCAcct" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnAproveTCAcct_Click" />
                                <asp:Button ID="btnRejectTCAcct" runat="server" Text="Reject" CssClass="btn btn-default" />
                            <asp:Button ID="ButtonID8" runat="server" OnClick="btnRejectTCAcct_Click"  style="visibility: hidden; display: none;" ClientIDMode="Static" />
                            </td>
                            <td style="width: 206px">    
                               <div id="modal_dialog8" style="display: none" >                                      
                                  <asp:TextBox ID="txtCustTCAcctComment" runat="server" TextMode="MultiLine" Width="270px" Height="80px" ></asp:TextBox>
                            </div>
                                <asp:HiddenField ID="HidCustTCAcctComment" runat="server" />
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
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                        SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;NAME_OF_BENEFICIAL_OWNER&quot;, &quot;SPOUSE_NAME&quot;, &quot;FREQ_INTERNATIONAL_TRAVELER&quot;, &quot;INSIDER_RELATION&quot;, &quot;POLITICALLY_EXPOSED_PERSON&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; 
                        FROM &quot;TMP_TRUSTS_CLIENT_ACCOUNTS&quot; where last_modified_by IN (select p.USER_ID from CM_USER_PROFILE p , 
(select * from CM_MAKER_CHECKER_XREF where  CHECKER_ID in (select PROFILE_ID from CM_USER_PROFILE p where p.USER_ID = :CurrentUser))x 
where (x.MAKER_ID = p.profile_id ))">

                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCurrentUser" Name="CurrentUser" Type="String" />
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

                                <asp:TextBox ID="txtCustNoA4FinInc" ReadOnly="True" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

                            </td>
                            <td style="width: 206px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px" >Is The Customer Socially Or Financially Disadvantaged?</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rbtA4FinIncSocFinDisadv" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
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

                                <asp:TextBox ID="txtA4FinIncSocFinDoc" runat="server" CssClass="form-control" Height="40px" ReadOnly="true" ></asp:TextBox>

                               </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px">Does The Customer Enjoy Tiered KYC Requirements?</td>
                            <td style="width: 211px">

                                <asp:RadioButtonList Enabled="false" ID="rbtA4FinIncEnjoyKYC" runat="server" Height="25px" RepeatDirection="Horizontal" Width="200px" >
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                            <td>
</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px">If Answer To Question Above Is Yes, Identify The Customer Risk Category:</td>
                            <td style="width: 211px">

                                 <asp:TextBox ID="txtA4FinIncRiskCat" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>                               
                             

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px">Mandate Authorization/Combination Rule</td>
                            <td style="width: 211px">

                                <asp:TextBox ID="txtA4FinIncMandRule" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 500px">Account Held With Other Banks</td>
                            <td colspan="4">

                                <asp:TextBox ID="txtA4FinIncAcctWithOtherBanks" runat="server" CssClass="form-control" Height="40px" Width="300px" ReadOnly="true"  ></asp:TextBox>
         
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
                                <%--<asp:Button ID="btnA4FinInc" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnA4FinInc_Click" />--%>
                                 <asp:Button ID="btnApproveFinInc" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnApproveFinInc_Click" />
                                <asp:Button ID="btnRejectFinInc" runat="server" Text="Reject" CssClass="btn btn-default"  />
                           <asp:Button ID="ButtonID9" runat="server" OnClick="btnRejectFinInc_Click"  style="visibility: hidden; display: none;" ClientIDMode="Static" />
                            </td>
                            <td style="width: 206px">    
                               <div id="modal_dialog9" style="display: none" >                                      
                                  <asp:TextBox ID="txtCustFinIncComment" runat="server" TextMode="MultiLine" Width="270px" Height="80px" ></asp:TextBox>
                            </div>
                                <asp:HiddenField ID="HidCustFinIncComment" runat="server" />
 </td>
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
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                        SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;SOCIAL_FINANCIAL_DISADVTAGE&quot;, &quot;SOCIAL_FINANCIAL_DOCUMENTS&quot;, &quot;ENJOYED_TIERED_KYC&quot;, &quot;RISK_CATEGORY&quot;, &quot;MANDATE_AUTH_COMBINE_RULE&quot;, &quot;ACCOUNT_WITH_OTHER_BANKS&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; 
                        FROM &quot;TMP_AUTH_FINANCE_INCLUSION&quot; where last_modified_by IN (select p.USER_ID from CM_USER_PROFILE p , 
(select * from CM_MAKER_CHECKER_XREF where  CHECKER_ID in (select PROFILE_ID from CM_USER_PROFILE p where p.USER_ID = :CurrentUser))x 
where (x.MAKER_ID = p.profile_id ))">

                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCurrentUser" Name="CurrentUser" Type="String" />
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

                                <asp:TextBox ID="txtCustNoAddInfo" ReadOnly="True" runat="server" CssClass="form-control" Height="40px"  ></asp:TextBox>

                            </td>
                            <td>

                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">*Annual Salary/ Expected Annual Income</td>
                            <td style="width: 211px">
                               
                                <asp:TextBox ID="txtAddInfoAnnualSalary" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                            </td>
                            <td>
</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="col-sm-3 control-label" style="width: 250px">Fax Number</td>
                            <td style="width: 211px">

                               

                                <asp:TextBox ID="txtAddInfoFaxNumber" runat="server" CssClass="form-control" Height="40px" ReadOnly="true"  ></asp:TextBox>

                               

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
                               <%-- <asp:Button ID="btnAdditionalInfo" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnAdditionalInfo_Click" />--%>
                                <asp:Button ID="btnApproveAddInfo" runat="server" Text="Approve" CssClass="btn btn-primary" OnClick="btnApproveAddInfo_Click" />
                                <asp:Button ID="btnRejectAddInfo" runat="server" Text="Reject" CssClass="btn btn-default"  />
                            <asp:Button ID="ButtonID10" runat="server" OnClick="btnRejectAddInfo_Click" style="visibility: hidden; display: none;" ClientIDMode="Static" />
                            </td>
                            <td style="width: 206px">    
                               <div id="modal_dialog10" style="display: none" >                                      
                                  <asp:TextBox ID="txtCustAddInfoComment" runat="server" TextMode="MultiLine" Width="270px" Height="80px" ></asp:TextBox>
                            </div>
                                <asp:HiddenField ID="HidCustAddInfoComment" runat="server" />
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
                        ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" 
                        SelectCommand="SELECT &quot;CUSTOMER_NO&quot;, &quot;ANNUAL_SALARY_EXPECTED_INC&quot;, &quot;FAX_NUMBER&quot;, &quot;LAST_MODIFIED_DATE&quot;, &quot;LAST_MODIFIED_BY&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot; 
                        FROM &quot;TMP_ADDITIONAL_INFORMATION&quot; where last_modified_by IN (select p.USER_ID from CM_USER_PROFILE p , 
(select * from CM_MAKER_CHECKER_XREF where  CHECKER_ID in (select PROFILE_ID from CM_USER_PROFILE p where p.USER_ID = :CurrentUser))x 
where (x.MAKER_ID = p.profile_id ))">

                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCurrentUser" Name="CurrentUser" Type="String" />
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
