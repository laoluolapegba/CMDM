<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BasicCustomerInfo.aspx.cs" Inherits="Cdma.Web.CustomerInfo.BasicCustomerInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Basic Customer Information</h3>
        </div>
        <div class="widget-content">
            <table class="nav-justified">
                <tr>
                    <td colspan="5"><asp:Label ID="lblstatus" runat="server"></asp:Label></td>
               
                </tr>
                <tr>
                    <td class="col-sm-3 control-label" style="width: 144px">Customer ID</td>
                    <td style="width: 211px">

                        <div class="input-group input-group-lg">
                            <asp:TextBox ID="txtCustID" runat="server" CssClass="form-control"></asp:TextBox>
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
                    <td class="datepicker-inline" colspan="2"><legend>CUSTOMER INFORMATION</legend></td>
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
                    <td class="col-sm-3 control-label" style="width: 144px">*Full Name</td>
                    <td style="width: 211px">
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
                    </td>
                    <td style="width: 206px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="col-sm-3 control-label" style="width: 144px">*Customer Code</td>
                    <td style="width: 211px">
                        <asp:TextBox ID="txtCustCode" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
                    </td>
                    <td style="width: 206px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="col-sm-3 control-label" style="width: 144px">*Customer Type</td>
                    <td style="width: 211px">
                        <asp:TextBox ID="txtCustType" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
                    </td>
                    <td style="width: 206px">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="col-sm-3 control-label" style="width: 144px">*Secret Question</td>
                    <td style="width: 211px">

                        <asp:DropDownList ID="ddlSecretQestion" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Nill">--SELECT---</asp:ListItem>
                    <asp:ListItem>Where did you spend your childhood summer?</asp:ListItem>
                    <asp:ListItem>What was the last name of your favorite teacher?</asp:ListItem>
                    <asp:ListItem>What was the last name of your best childhood friend?</asp:ListItem>
                    <asp:ListItem>What was your favorite food as a child?</asp:ListItem>
                    <asp:ListItem>What was the last name of your first boss?</asp:ListItem>
                    <asp:ListItem>What is the name of the hospital where you were born?</asp:ListItem>
                    <asp:ListItem>What is your main frequent flier number?</asp:ListItem>
                    <asp:ListItem>What is the name of the street on which you grew up?</asp:ListItem>
                    <asp:ListItem>What is the name of your favorite sports team?</asp:ListItem>
                    <asp:ListItem>What was your Pet name?</asp:ListItem>
                    <asp:ListItem>What is the last name of your best man at your wedding?</asp:ListItem>
                    <asp:ListItem>What is the last name of your maid of honor at your wedding?</asp:ListItem>
                    <asp:ListItem>What is the name of your favorite book?</asp:ListItem>
                    <asp:ListItem>What is the last name of your favorite musician?</asp:ListItem>
                    <asp:ListItem>Who is your all-time favorite movie character?</asp:ListItem>
                    <asp:ListItem>What was the make of your first car?</asp:ListItem>
                    <asp:ListItem>Who is your favorite author?</asp:ListItem>
                    <asp:ListItem>What was the make of your first motorcycle?</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td class="col-sm-3 control-label" style="width: 206px">Secret Answer</td>
                    <td>
                        <asp:TextBox ID="txtSecretAnswer" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="col-sm-3 control-label" style="width: 144px">*Politically Exposed?</td>
                    <td style="width: 211px">
                        <asp:TextBox ID="txtPolExposed" runat="server" CssClass="form-control" Height="40px" Width="211px"></asp:TextBox>
                    </td>
                    <td class="col-sm-3 control-label" style="width: 206px">*Financially Exposed?</td>
                    <td>
                        <asp:TextBox ID="txtFinExposed" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="datepicker-inline" style="width: 144px; height: 18px;"></td>
                    <td style="width: 211px; height: 18px;"></td>
                    <td style="width: 206px; height: 18px;"></td>
                    <td style="height: 18px"></td>
                    <td style="height: 18px"></td>
                </tr>
                <tr>
                    <td class="datepicker-inline" colspan="2"><legend>ANNIVERSARY DETAILS</td>
                    <td style="width: 206px">
                        
                        

                    </td>
                    <td>
                     
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
                    <td class="col-sm-3 control-label" style="width: 144px">*Type 1</td>
                    <td style="width: 211px">

                        <asp:TextBox ID="txtType1" runat="server" CssClass="form-control" Height="40px" Width="211px" Enabled="False"></asp:TextBox>

                    </td>
                    <td class="col-sm-3 control-label" style="width: 144px">*Date 1</td>
                    <td>
                        <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate1" runat="server" 
            CssClass='form-control' ClientIDMode="Static" Width="175" Enabled="false"></asp:TextBox>
        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtDate1.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd.mm.yyyy",
                //language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        });
    </script>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="col-sm-3 control-label" style="width: 144px">Type 2</td>
                    <td style="width: 211px">

                        <asp:DropDownList ID="ddlType2" runat="server" CssClass="form-control" AutoPostBack="true">
                            <asp:ListItem Value="Nill">--SELECT---</asp:ListItem>
                    <asp:ListItem>BUSINESS YEAR END</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td class="col-sm-3 control-label" style="width: 144px">Date 2</td>
                    <td>
                        <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate2" runat="server" 
            CssClass='form-control' placeholder="YYYY-MM-DD" type="date" ClientIDMode="Static"></asp:TextBox>
        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtDate2.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd.mm.yyyy",
                //language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        });
    </script>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="col-sm-3 control-label" style="width: 144px">Type 3</td>
                    <td style="width: 211px">

                        <asp:DropDownList ID="ddlType3" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Nill">--SELECT--</asp:ListItem>
                            <asp:ListItem>Nill</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td class="col-sm-3 control-label" style="width: 144px">Date 3</td>
                    <td>
                        <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate3" runat="server" type="date"
            CssClass='form-control' placeholder="YYYY-MM-DD" ClientIDMode="Static"></asp:TextBox>
        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtDate3.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd.mm.yyyy",
                //language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        });
    </script>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="col-sm-3 control-label" style="width: 144px">Type 4</td>
                    <td style="width: 211px">

                        <asp:DropDownList ID="ddlType4" runat="server" CssClass="form-control" AutoPostBack="True">
                          
                   <asp:ListItem Value="Nill">--SELECT---</asp:ListItem>
                    <asp:ListItem>XMAS</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td class="col-sm-3 control-label" style="width: 144px">Date 4</td>
                    <td>
                       <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate4" runat="server" type="date"
            CssClass='form-control' placeholder="YYYY-MM-DD" ClientIDMode="Static"></asp:TextBox>
        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtDate4.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd.mm.yyyy",
                // language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        });
    </script>
                    </td>
                    <td>&nbsp;</td>
                </tr>
        <%--</div>--%>
    <%--</div>--%>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Type 5</td>
        <td style="width: 211px">

            <asp:DropDownList ID="ddlType5" runat="server" CssClass="form-control" AutoPostBack="True">
              
                   <asp:ListItem Value="Nill">--SELECT---</asp:ListItem>
                    <asp:ListItem>SALLAH</asp:ListItem>
            </asp:DropDownList>

        </td>
        <td class="col-sm-3 control-label" style="width: 144px">Date 5</td>
        <td>
           <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate5" runat="server" type="date"
            CssClass='form-control' placeholder="YYYY-MM-DD" ClientIDMode="Static"></asp:TextBox>
        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtDate5.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd.mm.yyyy",
                // language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        });
    </script>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Type 6</td>
        <td style="width: 211px">

            <asp:DropDownList ID="ddlType6" runat="server" CssClass="form-control">
                <asp:ListItem Value="Nill">--SELECT---</asp:ListItem>
                <asp:ListItem>Wedding</asp:ListItem>
            </asp:DropDownList>

        </td>
        <td class="col-sm-3 control-label" style="width: 144px">Date 6</td>
        <td>
            <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate6" runat="server" type="date"
            CssClass='form-control' placeholder="YYYY-MM-DD" ClientIDMode="Static"></asp:TextBox>
        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtDate6.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd.mm.yyyy",
                // language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        });
    </script>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Type 7 (Specify if Applicable):</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtType7" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 144px">Date 7</td>
        <td>
           <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate7" runat="server" type="date"
            CssClass='form-control' placeholder="YYYY-MM-DD" ClientIDMode="Static"></asp:TextBox>
        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtDate7.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd.mm.yyyy",
                // language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        });
    </script>
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
        <td class="datepicker-inline" colspan="2"><legend>ADDRESS DETAILS</td>
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
        <td class="col-sm-3 control-label" style="width: 144px">Email Address</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtEmailAdd" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Alternative E-mail Address</td>
        <td>
            <asp:TextBox ID="txtAltEmailAdd" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">*Address Type</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtAddressType" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">*Home Identifier</td>
        <td>
            <asp:TextBox ID="txtHomeIdentifier" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Address Line 1</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtAddLine1" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Address Line 2</td>
        <td>
            <asp:TextBox ID="txtAddLine2" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Administrative Area</td>
        <td style="width: 211px">

            <asp:TextBox ID="txtAdminArea" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>

        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Locality</td>
        <td>
            <asp:TextBox ID="txtLocality" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Location Coordinates</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtLocCordnts" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Postal Code</td>
        <td>
            <asp:TextBox ID="txtPostalCod" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">P.O. Box</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtPOBox" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">*Country</td>
        <td>
            <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">State</td>
        <td style="width: 211px">

            <asp:TextBox ID="txtState" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>

        </td>
        <td class="col-sm-3 control-label" style="width: 206px">City</td>
        <td>
            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">*Country of Residence</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtCountryOfResidence" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Web Address</td>
        <td>
            <asp:TextBox ID="txtWebAdd" runat="server" CssClass="form-control" Height="40px" Width="200px"></asp:TextBox>
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
        <td class="datepicker-inline" colspan="2"><legend>CONTACT DETAILS</td>
        <td style="width: 206px">&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">`Phone Category</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtPhoneCat" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Area Code</td>
        <td>
            
                        <asp:DropDownList ID="ddlAreaCod" runat="server" CssClass="form-control" Width="200px">
                            <asp:ListItem>--SELECT--</asp:ListItem>
            <asp:ListItem Value='082'>Aba (082)</asp:ListItem>
            <asp:ListItem Value="043">Abakaliki (043)</asp:ListItem>
            <asp:ListItem Value="039">Abeokuta (039)</asp:ListItem>
            <asp:ListItem Value="09">Abuja (09)</asp:ListItem>
            <asp:ListItem Value="030">Ado-Ekiti (030)</asp:ListItem>
            <asp:ListItem Value="078">Agashua (078)</asp:ListItem>
            <asp:ListItem Value="055">Agbor (055)</asp:ListItem>
            <asp:ListItem Value="058">Ajaokuta (058)</asp:ListItem>
            <asp:ListItem Value="034">Akure (034)</asp:ListItem>
            <asp:ListItem Value="047">Akwanga (047)</asp:ListItem>
            <asp:ListItem Value="046">Asaba (046)</asp:ListItem>
            <asp:ListItem Value="057">Auchi (057)</asp:ListItem>
            <asp:ListItem Value="046">Awka (046)</asp:ListItem>
            <asp:ListItem Value="071">Azare (071)</asp:ListItem>
            <asp:ListItem Value="077">Bauchi (077)</asp:ListItem>
            <asp:ListItem Value="052">Benin (052)</asp:ListItem>
            <asp:ListItem Value="066">Bida (066)</asp:ListItem>
            <asp:ListItem Value="073">Bukuru (073)</asp:ListItem>
            <asp:ListItem Value="053">Burutu (053)</asp:ListItem>
            <asp:ListItem Value="087">Calabar (087)</asp:ListItem>
            <asp:ListItem Value="065">Daura (065)</asp:ListItem>
            <asp:ListItem Value="035">Ede (035)</asp:ListItem>
            <asp:ListItem Value="055">Ekpoma (055)</asp:ListItem>
            <asp:ListItem Value="054">Eku (054)</asp:ListItem>
            <asp:ListItem Value="042">Enugu (042)</asp:ListItem>
            <asp:ListItem Value="064">Funtua (064)</asp:ListItem>
            <asp:ListItem Value="063">Gusau (063)</asp:ListItem>
            <asp:ListItem Value="02">Ibadan (02)</asp:ListItem>
            <asp:ListItem Value="051">Ifon (051)</asp:ListItem>
            <asp:ListItem Value="057">Igarra (057)</asp:ListItem>
            <asp:ListItem Value="052">Iguoba Zuwa (052)</asp:ListItem>
            <asp:ListItem Value="037">Ijebu-ode (037)</asp:ListItem>
            <asp:ListItem Value="030">Ikole-Ekiti (030)</asp:ListItem>
            <asp:ListItem Value="085">Ikot-Ekpene (085)</asp:ListItem>
            <asp:ListItem Value="039">Ilaro (039)</asp:ListItem>
            <asp:ListItem Value="031">Ile-Ife/Ilesha (031)</asp:ListItem>
            <asp:ListItem Value="038">Iseyin (038)</asp:ListItem>
            <asp:ListItem Value="062">Kaduna (062)</asp:ListItem>
            <asp:ListItem Value="065">Katsina (065)</asp:ListItem>
            <asp:ListItem Value="047">Keffi (047)</asp:ListItem>
            <asp:ListItem Value="067">Kontagora (067)</asp:ListItem>
            <asp:ListItem Value="054">Koko (054)</asp:ListItem>
            <asp:ListItem Value="053">Kwara (053)</asp:ListItem>
            <asp:ListItem Value="01">Lagos(01)</asp:ListItem>
            <asp:ListItem Value="058">Lokoja (058)</asp:ListItem>
            <asp:ListItem Value="076">Maidugiri (076)</asp:ListItem>
            <asp:ListItem Value="044">Makurdi (044)</asp:ListItem>
            <asp:ListItem Value="066">Minna (066)</asp:ListItem>
            <asp:ListItem Value="047">Nassarawa (047)</asp:ListItem>
            <asp:ListItem Value="078">Nguru (078)</asp:ListItem>
            <asp:ListItem Value="046">Nnewi (046)</asp:ListItem>
            <asp:ListItem Value="042">Nsukka (042)</asp:ListItem>
            <asp:ListItem Value="054">Obiaruku (054)</asp:ListItem>
            <asp:ListItem Value="038">Ogbomosho (038)</asp:ListItem>
            <asp:ListItem Value="046">Ogidi (046)</asp:ListItem>
            <asp:ListItem Value="059">Okitipupa (059)</asp:ListItem>
            <asp:ListItem Value="043">Ondo (043)</asp:ListItem>
            <asp:ListItem Value="046">Onitsha (046)</asp:ListItem>
            <asp:ListItem Value="083">Orlu (083)</asp:ListItem>
            <asp:ListItem Value="035">Oshogbo (035)</asp:ListItem>
            <asp:ListItem Value="083">Owerri (083)</asp:ListItem>
            <asp:ListItem Value="051">Owo (051)</asp:ListItem>
            <asp:ListItem Value="038">Oyo (038)</asp:ListItem>
            <asp:ListItem Value="084">Port-Harcourt (084)</asp:ListItem>
            <asp:ListItem Value="054">Sapele (054)</asp:ListItem>
            <asp:ListItem Value="034">Shagamu (034)</asp:ListItem>
            <asp:ListItem Value="057">Sabongida-Ora (057)</asp:ListItem>
            <asp:ListItem Value="060">Sokoto (060)</asp:ListItem>
            <asp:ListItem Value="066">Suleja (066)</asp:ListItem>
            <asp:ListItem Value="053">Ughelli (053)</asp:ListItem>
            <asp:ListItem Value="088">Umuahia (088)</asp:ListItem>
            <asp:ListItem Value="055">Uromi (055)</asp:ListItem>
            <asp:ListItem Value="075">Yola (075)</asp:ListItem>
            <asp:ListItem Value="069">Zaria (069)</asp:ListItem>
            <asp:ListItem Value="067">Zuru (067)</asp:ListItem>
                        </asp:DropDownList>

                    </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Country Code</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtCountryCod" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Phone Number</td>
        <td>
            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Extension No</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtExtNo" runat="server" CssClass="form-control" Height="40px"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">*Phone Type</td>
        <td>
            <asp:TextBox ID="txtPhoneType" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">*Channel Supported</td>
        <td style="width: 211px">

            <asp:TextBox ID="txtChannelSupport" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>

        </td>
        <td class="col-sm-3 control-label" style="width: 206px">*Reachable Hour</td>
        <td>
            <asp:TextBox ID="txtReachableHr" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Created By</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Created Date</td>
        <td>
            <asp:TextBox ID="txtCreatedDate" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Modified By</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtModiedBy" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Modified Date</td>
        <td>
            <asp:TextBox ID="txtModifiedDate" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Authorized By</td>
        <td style="width: 211px">

            <asp:TextBox ID="txtAuthorizedBy" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>

        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Authorized Date</td>
        <td>
            <asp:TextBox ID="txtAuthorizedDate" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 211px">&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="datepicker-inline" style="width: 144px">&nbsp;</td>
        <td style="width: 211px">&nbsp;</td>
        <td style="width: 206px">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
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
												<h3><i class="fa fa-table"></i> View Basic Customer Information</h3>
											</div>
											<div class="widget-content">
												<div class="table-responsive">
													 <div class="gridpanel"  max-height: 500px;" style="overflow-x:auto;width:1000px">
                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BackColor="White" BorderWidth="0px" CellPadding="0" 
                    CssClass="table" DataSourceID="SqlDataSource1" GridLines="None"  PageSize="20" DataKeyNames="CUSTOMER_ID" EmptyDataText="No Record(s) found!">
                    <AlternatingRowStyle BackColor="White" CssClass="rptitem" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                EDIT
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="white-space: nowrap; clear: none; width: auto; display: inline;">
                                    <asp:LinkButton ID="lnkEdit" runat="server" RecId='<%#Eval("CUSTOMER_ID")%>' Style="margin: 0px 0px 0px 5px;
                                        white-space: nowrap; clear: none; display: inline;" OnClick="OnEdit_Customer" >
                                        <asp:Image ID="imgEdit" ImageUrl="../Content/assets/img/ico-edit.png" runat="server" />
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER ID" SortExpression="CUSTOMER_ID"
                            ReadOnly="True" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER NAME" SortExpression="CUSTOMER_NAME" />
                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER CODE" SortExpression="CUSTOMER_CODE" />
                        <asp:BoundField DataField="CUSTOMER_TYPE" HeaderText="CUSTOMER TYPE" SortExpression="CUSTOMER_TYPE" />
                        <asp:BoundField DataField="LAST_MODIFIED_BY" HeaderText="LAST_MODIFIED_BY" SortExpression="LAST_MODIFIED_BY" />
                        <asp:BoundField DataField="LAST_MODIFIED_DATE" HeaderText="LAST_MODIFIED_DATE" SortExpression="LAST_MODIFIED_DATE" />
                        <asp:BoundField DataField="AUTHORISED_BY" HeaderText="AUTHORISED_BY" SortExpression="AUTHORISED_BY" />
                        <asp:BoundField DataField="AUTHORISED_DATE" HeaderText="AUTHORISED_DATE" SortExpression="AUTHORISED_DATE" />
                        
                    </Columns>
                    <HeaderStyle BackColor="White" BorderColor="White" CssClass="rptheader" Font-Bold="True"
                        ForeColor="Black" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle CssClass="pagerheader" />
                    <RowStyle BackColor="#E9F1F6" CssClass="rptitem" ForeColor="#4A3C8C" HorizontalAlign="Left" Wrap="False" />
                    <SelectedRowStyle BackColor="#738A9C" CssClass="rptitem" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringCDMA %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT CUSTOMER_ID, 
                    CUSTOMER_NAME, CUSTOMER_CODE, CUSTOMER_TYPE, AUTHORISED_BY, AUTHORISED_DATE, 
                    LAST_MODIFIED_BY, LAST_MODIFIED_DATE FROM CUSTOMER WHERE (AUTHORISED_BY IS NULL) AND (ROWNUM <= 1000)">
                </asp:SqlDataSource>
            </div>
												</div>
											</div>
										</div>			
    </tr>
    </table>
            </div>
        </div>
</asp:Content>
