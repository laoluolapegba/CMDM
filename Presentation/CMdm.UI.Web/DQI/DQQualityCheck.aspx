<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DQQualityCheck.aspx.cs" Inherits="Cdma.Web.DQI.DQQualityCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
            <div class="row">
									<div class="col-md-6">
										<!-- SUPPOR TICKET FORM -->
										<div class="widget">
											<div class="widget-header">
												<h3><i class="fa fa-edit"></i> Submit a Ticket</h3>
											</div>
											<div class="widget-content">
												<form class="form-horizontal" role="form">
													<fieldset>
														<legend>General Information</legend>
														<div class="form-group">
															<label for="ticket-type" class="col-sm-3 control-label">Type</label>
															<div class="col-sm-9">
																<select id="ticket-type" name="ticket-type" class="select-ticket-type">
																	<option value="technical-support">Technical Support</option>
																	<option value="general-info">General Info</option>
																	<option value="sales-inquiry">Sales Inquiry</option>
																	<option value="billing">Billing</option>
																</select>
															</div>
														</div>
														<div class="form-group">
															<label for="ticket-name" class="col-sm-3 control-label">Name</label>
															<div class="col-sm-9">
																<input type="text" class="form-control" id="ticket-name" placeholder="Name">
															</div>
														</div>
														<div class="form-group">
															<label for="ticket-email" class="col-sm-3 control-label">Email</label>
															<div class="col-sm-9">
																<input type="email" class="form-control" id="ticket-email" placeholder="Email">
															</div>
														</div>
														<div class="form-group">
															<label for="ticket-priority" class="col-sm-3 control-label">Priority</label>
															<div class="col-sm-9">
																<select id="ticket-priority" name="ticket-priority" class="select-ticket-priority">
																	<option value="low">Low</option>
																	<option value="medium">Medium</option>
																	<option value="high">High</option>
																	<option value="urgent">Urgent</option>
																	<option value="emergency">Emergency</option>
																	<option value="critical">Critical</option>
																</select>
															</div>
														</div>
													</fieldset>
													<fieldset>
														<legend>Your Message</legend>
														<div class="form-group">
															<label for="ticket-subject" class="col-sm-3 control-label">Subject</label>
															<div class="col-sm-9">
																<input type="text" class="form-control" name="ticket-subject" id="ticket-subject" placeholder="Subject">
															</div>
														</div>
														<div class="form-group">
															<label for="ticket-message" class="col-sm-3 control-label">Message</label>
															<div class="col-sm-9">
																<textarea class="form-control" name="ticket-message" id="ticket-message" rows="5" cols="30" placeholder="Message"></textarea>
															</div>
														</div>
														<div class="form-group">
															<label for="ticket-attachment" class="col-sm-3 control-label">Attach File</label>
															<div class="col-md-9">
																<input type="file" id="ticket-attachment">
																<p class="help-block"><em>Valid file type: .jpg, .png, .txt, .pdf. File size max: 1 MB</em></p>
															</div>
														</div>
														<div class="form-group">
															<div class="col-sm-offset-3 col-sm-9">
																<button type="submit" class="btn btn-primary btn-block">Submit Message</button>
															</div>
														</div>
													</fieldset>
												</form>
											</div>
										</div>

        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ExtraJavaScripts" runat="server">
    <script src="/Content/assets/js/king-form-layouts.js"></script>
</asp:Content>
