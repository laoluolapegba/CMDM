<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataQualityQueue.aspx.cs" Inherits="Cdma.Web.DQI.DataQualityQueue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-content">
        <div class="widget">
            <div class="widget-header">
                <h3><i class="fa fa-certificate"></i>Data Quality Queue</h3>
            </div>
            <div class="widget-content">
                <!-- INBOX -->
                <div class="inbox">
                    <div class="row">
                        <div class="col-md-9">
                            </div>
                        <div class="col-md-3">
                            <!-- search box col-lg-10 col-lg-offset-2-->

                            <div class="input-group input-group-sm">
                                <input type="search" class="form-control">
                                <span class="input-group-btn">
                                    <button class="btn btn-primary" type="button"><i class="fa fa-search"></i>Search</button>
                                </span>
                            </div>

                            <!-- end search box -->
                        </div>
                    </div>
                    <br />
                    <div class="top">
                        <div class="row">
                           
                            <div class="col-lg-10">
                                <div class="top-menu">
                                    <label class="control-inline fancy-checkbox fancy-checkbox-all">
                                        <input type="checkbox">
                                        <span>&nbsp;</span>
                                    </label>
                                    <ul class="list-inline top-menu-group1">
                                       
                                        
                                    </ul>

                                    <ul class="list-inline top-menu-group2">
                                        <li class="top-menu-label">
                                        <div class="btn-group">
                                            <button type="button" class="btn dropdown-toggle" data-toggle="dropdown">
                                                <i class="fa fa-tags"></i>Status <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu">
                                                <li><a href="#">Closed</a></li>
                                                <li><a href="#">Open</a></li>
                                                <li><a href="#">In progress</a></li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li class="top-menu-label">
                                        <div class="btn-group">
                                            <button type="button" class="btn dropdown-toggle" data-toggle="dropdown">
                                                <i class="fa fa-tags"></i>Severity <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu">
                                                <li><a href="#">High</a></li>
                                                <li><a href="#">Low</a></li>
                                                <li><a href="#">Medium</a></li>
                                            </ul>
                                        </div>
                                    </li>
                                        <li class="top-menu-label">
                                        <div class="btn-group">
                                            <button type="button" class="btn dropdown-toggle" data-toggle="dropdown">
                                                <i class="fa fa-tags"></i>Impact <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu">
                                                <li><a href="#">Enterprise</a></li>
                                                <li><a href="#">Entity</a></li>
                                                <li><a href="#">Departmental</a></li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li class="top-menu-more">
                                        <div class="btn-group">
                                            <button type="button" class="btn dropdown-toggle" data-toggle="dropdown">
                                                <i class="fa fa-list"></i>MORE <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu">
                                                <li class="mark-all"><a href="#">Mark All</a></li>
                                                <li class="hide mark-read"><a href="#">Mark As Read</a></li>
                                                <li class="hide mark-unread"><a href="#">Mark As Unread</a></li>
                                                <li class="hide add-star"><a href="#">Add Star</a></li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li class="top-menu-more">
                                        <div class="btn-group">
                                            <button type="button" class="btn">
                                                <i class="fa fa-recycle"></i>REFRESH
                                            </button>

                                        </div>
                                    </li>
                                </ul>
                                    <div>
                                        
                                    </div>
                                    <div class="navigation">
                                        
                                      
                                    </div>
                                </div>
                                <!-- /top-menu -->
                            </div>
                        </div>
                        <!-- /row -->
                    </div>
                    <!-- /top -->

                    <div class="bottom">
                        <div class="row">
                            <!-- inbox left menu -->
                            
                            <!-- end inbox left menu -->

                            <!-- right main content, the messages -->
                            <div class="col-xs-12 col-sm-9 col-lg-12"><%--col-xs-12 col-sm-9 col-lg-10--%>
                                <div class="messages">
                                    <table class="table-condensed message-table">
                                        <colgroup>
                                            <col class="col-check">
                                            <col class="col-star">
                                            <col class="col-from">
                                            <col class="col-title">
                                            <col class="col-attachment">
                                            <col class="col-timestamp">
                                        </colgroup>
                                        <tbody >
                                            <tr>
                                                <td>
                                                    <label class="fancy-checkbox">
                                                        <input type="checkbox">
                                                        <span>&nbsp;</span>
                                                    </label>
                                                </td>
                                                <td><i class="fa fa-star-o"></i></td>
                                                <td><span class="from">Last Name</span></td>
                                                <td>
                                                    <span class="message-label label2">Closed</span>
                                                    <span class="title">Duplicate Individual Customer Last Name..</span> <span class="preview">- 120,344 Customer individual customer names appears on the system. </span>
                                                </td>
                                                <td><span class="icon-attachment"><i class="fa fa-user"></i></span></td>
                                                <td><span class="timestamp">tAkintola</span></td>
                                            </tr>
                                            <tr class="unread">
                                                <td>
                                                    <label class="fancy-checkbox">
                                                        <input type="checkbox">
                                                        <span>&nbsp;</span>
                                                    </label>
                                                </td>
                                                <td><i class="fa fa-star-o"></i></td>
                                                <td><span class="from">Address</span></td>
                                                
                                                <td>
                                                    <span class="message-label label3">Open</span>
                                                    <span class="title">Empty Corporate Customer Address</span> <span class="preview">- 1,500,342 Coporate customers have their address column empty</span></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr class="unread">
                                                <td>
                                                    <label class="fancy-checkbox">
                                                        <input type="checkbox">
                                                        <span>&nbsp;</span>
                                                    </label>
                                                </td>
                                                <td><i class="fa fa-star-o"></i></td>
                                                <td><span class="from">Date of Birth</span></td>
                                                <td>
                                                    <span class="message-label label3">Open</span>
                                                    <span class="title">In-correct Customer Date of Birth</span> <span class="preview">-234,567 customers have invalid date of birth                                                               </span>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label class="fancy-checkbox">
                                                        <input type="checkbox">
                                                        <span>&nbsp;</span>
                                                    </label>
                                                </td>
                                                <td><i class="fa fa-star-o"></i></td>
                                                <td><span class="from">Phone No</span></td>
                                                <td>
                                                    <span class="message-label label1">Acknowledged</span>
                                                    <span class="title">Empty Phone no column</span> <span class="preview">- 356,789 Customers have empty Phone No column</span>
                                                </td>
                                                <td><span class="icon-attachment"><i class="fa fa-user"></i></span></td>
                                                <td><span class="timestamp">lOlapegba</span></td>
                                            </tr>
                                            <tr class="unread">
                                                <td>
                                                    <label class="fancy-checkbox">
                                                        <input type="checkbox">
                                                        <span>&nbsp;</span>
                                                    </label>
                                                </td>
                                                <td><i class="fa fa-star-o"></i></td>
                                                <td><span class="from">Account No</span></td>
                                                <td><span class="message-label label3">Open</span><span class="title">Duplicate customer account no</span> <span class="preview">- 10 customers have diplicate account number</span></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label class="fancy-checkbox">
                                                        <input type="checkbox">
                                                        <span>&nbsp;</span>
                                                    </label>
                                                </td>
                                                <td><i class="fa fa-star-o"></i></td>
                                                <td><span class="from">Next of Kin</span></td>
                                                <td>
                                                    <span class="message-label label1">Acknowledged</span>
                                                    <span class="title">Empty Next of Kin Infor</span> <span class="preview">- 239,086 customers Next of Kin column is empty</span>
                                                </td>
                                                <td><span class="icon-attachment"><i class="fa fa-user"></i></span></td>
                                                <td><span class="timestamp">aGbadeyanka</span></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label class="fancy-checkbox">
                                                        <input type="checkbox">
                                                        <span>&nbsp;</span>
                                                    </label>
                                                </td>
                                                <td><i class="fa fa-star-o"></i></td>
                                                <td><span class="from">Gender</span></td>
                                                <td>
                                                    <span class="message-label label1">Acknowledged</span>
                                                    <span class="title">Empty Gender Column</span> <span class="preview">- 50 customers has empty gender column</span>
                                                </td>
                                               <td><span class="icon-attachment"><i class="fa fa-user"></i></span></td>
                                                <td><span class="timestamp">hAdigun</span></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label class="fancy-checkbox">
                                                        <input type="checkbox">
                                                        <span>&nbsp;</span>
                                                    </label>
                                                </td>
                                                <td><i class="fa fa-star-o"></i></td>
                                                <td><span class="from">Gender</span></td>
                                                <td><span class="message-label label2">Closed</span><span class="title">Invalid Gender Entry</span> <span class="preview">- 250 Customers has the gender column empty</span></td>
                                                <td><span class="icon-attachment"><i class="fa fa-user"></i></span></td>
                                                <td><span class="timestamp">aGbadeyanka</span></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label class="fancy-checkbox">
                                                        <input type="checkbox">
                                                        <span>&nbsp;</span>
                                                    </label>
                                                </td>
                                                <td><i class="fa fa-star-o"></i></td>
                                                <td><span class="from">Email</span></td>
                                                <td><span class="message-label label2">Closed</span>
                                                    <span class="title">Invalid Email Adress</span> <span class="preview">- 950,908 have invalid Email addresses</span></td>
                                                <td><span class="icon-attachment"><i class="fa fa-user"></i></span></td>
                                                <td><span class="timestamp">tAkintola</span></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label class="fancy-checkbox">
                                                        <input type="checkbox">
                                                        <span>&nbsp;</span>
                                                    </label>
                                                </td>
                                                <td><i class="fa fa-star-o"></i></td>
                                                <td><span class="from">Title</span></td>
                                                <td>
                                                    <span class="message-label label1">Acknowledged</span>
                                                    <span class="title">Empty Title Column</span> <span class="preview">- 23,567 customer has empty title columns</span>
                                                </td>
                                                <td><span class="icon-attachment"><i class="fa fa-user"></i></span></td>
                                                <td><span class="timestamp">aAfolabi</span></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <!-- end right main content, the messages -->
                        </div>
                    </div>

                </div>
                <!-- END INBOX -->

            </div>
            <!-- /main-content -->
        </div>
    </div>
</asp:Content>
<asp:content contentplaceholderid="ExtraJavaScripts" runat="server">    
    <script src="Content/assets/js/plugins/select2/select2.min.js"></script>
</asp:content>
