<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuthCustBasicInfo.aspx.cs" Inherits="Cdma.Web.CustomerInfo.AuthCustBasicInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget">
        <div class="widget-header">
            <h3><i class="fa fa-edit"></i>Authorise Basic Customer Information</h3>
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
                    <td class="datepicker-inline" colspan="2"><legend>CUSTOMER INFROMATION</legend></td>
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

                        <asp:TextBox ID="txtSecurityQuestn" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>

                    </td>
                    <td class="col-sm-3 control-label" style="width: 206px">Secret Answer</td>
                    <td>
                        <asp:TextBox ID="txtSecretAnswer" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="col-sm-3 control-label" style="width: 144px">*Politically Exposed?</td>
                    <td style="width: 211px">
                        <asp:TextBox ID="txtPolExposed" runat="server" CssClass="form-control" Height="40px" Width="211px" Enabled="False"></asp:TextBox>
                    </td>
                    <td class="col-sm-3 control-label" style="width: 206px">*Financially Exposed?</td>
                    <td>
                        <asp:TextBox ID="txtFinExposed" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
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

                        <asp:TextBox ID="txtType2" runat="server" CssClass="form-control" Height="40px" Width="211px" Enabled="False"></asp:TextBox>

                    </td>
                    <td class="col-sm-3 control-label" style="width: 144px">Date 2</td>
                    <td>
                        <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate2" runat="server" Enabled="False"
            CssClass='form-control' Width="180"  ClientIDMode="Static"></asp:TextBox>
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

                        <asp:TextBox ID="txtType3" runat="server" CssClass="form-control" Height="40px" Width="211px" Enabled="False"></asp:TextBox>

                    </td>
                    <td class="col-sm-3 control-label" style="width: 144px">Date 3</td>
                    <td>
                        <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate3" runat="server" Enabled="False"
            CssClass='form-control' Width="180"  ClientIDMode="Static"></asp:TextBox>
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

                        <asp:TextBox ID="txtType4" runat="server" CssClass="form-control" Height="40px" Width="211px" Enabled="False"></asp:TextBox>

                    </td>
                    <td class="col-sm-3 control-label" style="width: 144px">Date 4</td>
                    <td>
                       <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate4" runat="server" Enabled="False"
            CssClass='form-control' Width="180"  ClientIDMode="Static"></asp:TextBox>
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

                        <asp:TextBox ID="txtType5" runat="server" CssClass="form-control" Height="40px" Width="211px" Enabled="False"></asp:TextBox>

        </td>
        <td class="col-sm-3 control-label" style="width: 144px">Date 5</td>
        <td>
           <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate5" runat="server" Enabled="False"
            CssClass='form-control' Width="180" ClientIDMode="Static"></asp:TextBox>
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

                        <asp:TextBox ID="txtType6" runat="server" CssClass="form-control" Height="40px" Width="211px" Enabled="False"></asp:TextBox>

        </td>
        <td class="col-sm-3 control-label" style="width: 144px">Date 6</td>
        <td>
            <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate6" runat="server" Enabled="False"
            CssClass='form-control' Width="180"  ClientIDMode="Static"></asp:TextBox>
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
            <asp:TextBox ID="txtType7" runat="server" CssClass="form-control" Height="40px" Enabled="False" Width="211px"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 144px">Date 7</td>
        <td>
           <div class="input-group col-md-2">
       
        <asp:TextBox ID="txtDate7" runat="server" Enabled="False"
            CssClass='form-control' Width="180"  ClientIDMode="Static"></asp:TextBox>
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
            <asp:TextBox ID="txtAltEmailAdd" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
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
            <asp:TextBox ID="txtAddLine2" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Administrative Area</td>
        <td style="width: 211px">

            <asp:TextBox ID="txtAdminArea" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>

        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Locality</td>
        <td>
            <asp:TextBox ID="txtLocality" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">Location Coordinates</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtLocCordnts" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
        </td>
        <td class="col-sm-3 control-label" style="width: 206px">Postal Code</td>
        <td>
            <asp:TextBox ID="txtPostalCod" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col-sm-3 control-label" style="width: 144px">P.O. Box</td>
        <td style="width: 211px">
            <asp:TextBox ID="txtPOBox" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
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
            <asp:TextBox ID="txtWebAdd" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>
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
            
            <asp:TextBox ID="txtAreaCode" runat="server" CssClass="form-control" Height="40px" Width="200px" Enabled="False"></asp:TextBox>

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
            <asp:TextBox ID="txtExtNo" runat="server" CssClass="form-control" Height="40px" Enabled="False"></asp:TextBox>
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
        <td style="width: 211px">
            <asp:Button ID="btnAuthorise" runat="server" Text="Authorise" CssClass="btn btn-primary" OnClick="btnAuthorise_Click" />
        </td>
        <td style="width: 206px">
            <asp:Button ID="btnDeny" runat="server" Text="Deny/Cancel" CssClass="btn btn-primary" OnClick="btnDeny_Click" />
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
                    ProviderName="<%$ ConnectionStrings:ConnectionStringCDMA.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_ID&quot;, 
                    &quot;CUSTOMER_NAME&quot;, &quot;CUSTOMER_CODE&quot;, &quot;CUSTOMER_TYPE&quot;, &quot;AUTHORISED_BY&quot;, &quot;AUTHORISED_DATE&quot;, 
                    &quot;LAST_MODIFIED_BY&quot;, &quot;LAST_MODIFIED_DATE&quot; FROM &quot;TMP_CUSTOMER_BASIC&quot; ">
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
