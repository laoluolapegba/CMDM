using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMdm.Data;
using CMdm.Entities.Domain.Customer;
using CMdm.UI.Web.Models.Customer;
using CMdm.Framework.Controllers;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Services.DqQue;
using CMdm.UI.Web.Controllers;

namespace CMdm.UI.Web.Models.Customer
{
    public class AccInfoContextController : BaseController
    {
        private AppDbContext db = new AppDbContext();
        private IDqQueService _dqQueService;

        public AccInfoContextController()
        {
            _dqQueService = new DqQueService();
        }

        public ActionResult Authorize(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            var model = new AccInfoCtxModel();

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }
            //get all changed columns

            var changeId = db.CDMA_CHANGE_LOGS.Where(a => a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();
            if (changeId != null)
            {
                var changedSet = db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId.CHANGEID); //.Select(a=>a.PROPERTYNAME);

                var accountInfo = (from c in db.CDMA_ACCOUNT_INFO
                                   where c.CUSTOMER_NO == id
                                   select new AccInfoModel
                                   {
                                       CUSTOMER_NO = c.CUSTOMER_NO,
                                       ACCOUNT_HOLDER = c.ACCOUNT_HOLDER,
                                       TYPE_OF_ACCOUNT = c.TYPE_OF_ACCOUNT,
                                       ACCOUNT_NUMBER = c.ACCOUNT_NUMBER,
                                       ACCOUNT_OFFICER = c.ACCOUNT_OFFICER,
                                       ACCOUNT_TITLE = c.ACCOUNT_TITLE,
                                       BRANCH = c.BRANCH,
                                       BRANCH_CLASS = c.BRANCH_CLASS,
                                       BUSINESS_DIVISION = c.BUSINESS_DIVISION,
                                       BUSINESS_SEGMENT = c.BUSINESS_SEGMENT,
                                       BUSINESS_SIZE = c.BUSINESS_SIZE,
                                       BVN_NUMBER = c.BVN_NUMBER,
                                       CAV_REQUIRED = c.CAV_REQUIRED,
                                       CUSTOMER_IC = c.CUSTOMER_IC,
                                       CUSTOMER_SEGMENT = c.CUSTOMER_SEGMENT,
                                       CUSTOMER_TYPE = c.CUSTOMER_TYPE,
                                       OPERATING_INSTRUCTION = c.OPERATING_INSTRUCTION,
                                       ORIGINATING_BRANCH = c.ORIGINATING_BRANCH,
                                       LastUpdatedby = c.LAST_MODIFIED_BY,
                                       LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                       LastAuthdby = c.AUTHORISED_BY,
                                       LastAuthDate = c.AUTHORISED_DATE,
                                       ExceptionId = querecord.EXCEPTION_ID
                                   }).FirstOrDefault();

                var accountService = (from c in db.CDMA_ACCT_SERVICES_REQUIRED
                                      where c.CUSTOMER_NO == id
                                      select new AccServicesModel
                                      {
                                          CUSTOMER_NO = c.CUSTOMER_NO,
                                          ACCOUNT_NUMBER = c.ACCOUNT_NUMBER,
                                          CARD_PREFERENCE = c.CARD_PREFERENCE,
                                          ELECTRONIC_BANKING_PREFERENCE = c.ELECTRONIC_BANKING_PREFERENCE,
                                          STATEMENT_PREFERENCES = c.STATEMENT_PREFERENCES,
                                          TRANSACTION_ALERT_PREFERENCE = c.TRANSACTION_ALERT_PREFERENCE,
                                          STATEMENT_FREQUENCY = c.STATEMENT_FREQUENCY,
                                          CHEQUE_BOOK_REQUISITION = c.CHEQUE_BOOK_REQUISITION,
                                          CHEQUE_LEAVES_REQUIRED = c.CHEQUE_LEAVES_REQUIRED,
                                          CHEQUE_CONFIRMATION = c.CHEQUE_CONFIRMATION,
                                          CHEQUE_CONFIRMATION_THRESHOLD = c.CHEQUE_CONFIRMATION_THRESHOLD,
                                          CHEQUE_CONFIRM_THRESHOLD_RANGE = c.CHEQUE_CONFIRM_THRESHOLD_RANGE,
                                          ONLINE_TRANSFER_LIMIT = c.ONLINE_TRANSFER_LIMIT,
                                          ONLINE_TRANSFER_LIMIT_RANGE = c.ONLINE_TRANSFER_LIMIT_RANGE,
                                          TOKEN = c.TOKEN,
                                          ACCOUNT_SIGNATORY = c.ACCOUNT_SIGNATORY,
                                          SECOND_SIGNATORY = c.SECOND_SIGNATORY,
                                          LastUpdatedby = c.LAST_MODIFIED_BY,
                                          LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                          LastAuthdby = c.AUTHORISED_BY,
                                          LastAuthDate = c.AUTHORISED_DATE,
                                          ExceptionId = querecord.EXCEPTION_ID
                                      }).FirstOrDefault();

                model.AccInfoModel = accountInfo;
                model.AccServicesModel = accountService;

                foreach (var item in model.AccInfoModel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }
                }

                foreach (var item in model.AccServicesModel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }
                }

                model.AccInfoModel.ReadOnlyForm = "True";
                model.AccServicesModel.ReadOnlyForm = "True";
            }

            PrepareModel(model);
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }

            var accountInfo = (from c in db.CDMA_ACCOUNT_INFO
                               where c.CUSTOMER_NO == id
                               select new AccInfoModel {
                                   CUSTOMER_NO = c.CUSTOMER_NO,
                                   ACCOUNT_HOLDER = c.ACCOUNT_HOLDER,
                                   TYPE_OF_ACCOUNT = c.TYPE_OF_ACCOUNT,
                                   ACCOUNT_NUMBER = c.ACCOUNT_NUMBER,
                                   ACCOUNT_OFFICER = c.ACCOUNT_OFFICER,
                                   ACCOUNT_TITLE = c.ACCOUNT_TITLE,
                                   BRANCH = c.BRANCH,
                                   BRANCH_CLASS = c.BRANCH_CLASS,
                                   BUSINESS_DIVISION = c.BUSINESS_DIVISION,
                                   BUSINESS_SEGMENT = c.BUSINESS_SEGMENT,
                                   BUSINESS_SIZE = c.BUSINESS_SIZE,
                                   BVN_NUMBER = c.BVN_NUMBER,
                                   CAV_REQUIRED = c.CAV_REQUIRED,
                                   CUSTOMER_IC = c.CUSTOMER_IC,
                                   CUSTOMER_SEGMENT = c.CUSTOMER_SEGMENT,
                                   CUSTOMER_TYPE = c.CUSTOMER_TYPE,
                                   OPERATING_INSTRUCTION = c.OPERATING_INSTRUCTION,
                                   ORIGINATING_BRANCH = c.ORIGINATING_BRANCH,
                               }).FirstOrDefault();
            PrepareAccountInfoModel(accountInfo);

            var accountService = (from c in db.CDMA_ACCT_SERVICES_REQUIRED
                                  where c.CUSTOMER_NO == id
                                  select new AccServicesModel
                                  {
                                      CUSTOMER_NO = c.CUSTOMER_NO,
                                      ACCOUNT_NUMBER = c.ACCOUNT_NUMBER,
                                      CARD_PREFERENCE = c.CARD_PREFERENCE,
                                      ELECTRONIC_BANKING_PREFERENCE = c.ELECTRONIC_BANKING_PREFERENCE,
                                      STATEMENT_PREFERENCES = c.STATEMENT_PREFERENCES,
                                      TRANSACTION_ALERT_PREFERENCE = c.TRANSACTION_ALERT_PREFERENCE,
                                      STATEMENT_FREQUENCY = c.STATEMENT_FREQUENCY,
                                      CHEQUE_BOOK_REQUISITION = c.CHEQUE_BOOK_REQUISITION,
                                      CHEQUE_LEAVES_REQUIRED = c.CHEQUE_LEAVES_REQUIRED,
                                      CHEQUE_CONFIRMATION = c.CHEQUE_CONFIRMATION,
                                      CHEQUE_CONFIRMATION_THRESHOLD = c.CHEQUE_CONFIRMATION_THRESHOLD,
                                      CHEQUE_CONFIRM_THRESHOLD_RANGE = c.CHEQUE_CONFIRM_THRESHOLD_RANGE,
                                      ONLINE_TRANSFER_LIMIT = c.ONLINE_TRANSFER_LIMIT,
                                      ONLINE_TRANSFER_LIMIT_RANGE = c.ONLINE_TRANSFER_LIMIT_RANGE,
                                      TOKEN = c.TOKEN,
                                      ACCOUNT_SIGNATORY = c.ACCOUNT_SIGNATORY,
                                      SECOND_SIGNATORY = c.SECOND_SIGNATORY,
                                  }).FirstOrDefault();
            PrepareAccountServiceModel(accountService);

            var model = new AccInfoCtxModel();
            model.AccInfoModel = accountInfo;
            model.AccServicesModel = accountService;

            if(model == null)
            {
                return HttpNotFound();
            }

            //PrepareModel(model);
            return View(model);
        }

        // POST: AccInfoContext/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccInfoCtxModel actxmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_ACCOUNT_INFO.FirstOrDefault(o => o.CUSTOMER_NO == actxmodel.AccInfoModel.CUSTOMER_NO);
                    var entity2 = db.CDMA_ACCT_SERVICES_REQUIRED.FirstOrDefault(o => o.CUSTOMER_NO == actxmodel.AccInfoModel.CUSTOMER_NO);

                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", actxmodel.AccInfoModel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.ACCOUNT_HOLDER = actxmodel.AccInfoModel.ACCOUNT_HOLDER;
                        entity.TYPE_OF_ACCOUNT = actxmodel.AccInfoModel.TYPE_OF_ACCOUNT;
                        entity.ACCOUNT_NUMBER = actxmodel.AccInfoModel.ACCOUNT_NUMBER;
                        entity.ACCOUNT_OFFICER = actxmodel.AccInfoModel.ACCOUNT_OFFICER;
                        entity.ACCOUNT_TITLE = actxmodel.AccInfoModel.ACCOUNT_TITLE;
                        entity.BRANCH = actxmodel.AccInfoModel.BRANCH;
                        entity.BRANCH_CLASS = actxmodel.AccInfoModel.BRANCH_CLASS;
                        entity.BUSINESS_DIVISION = actxmodel.AccInfoModel.BUSINESS_DIVISION;
                        entity.BUSINESS_SEGMENT = actxmodel.AccInfoModel.BUSINESS_SEGMENT;
                        entity.BUSINESS_SIZE = actxmodel.AccInfoModel.BUSINESS_SIZE;
                        entity.BVN_NUMBER = actxmodel.AccInfoModel.BVN_NUMBER;
                        entity.CAV_REQUIRED = actxmodel.AccInfoModel.CAV_REQUIRED;
                        entity.CUSTOMER_IC = actxmodel.AccInfoModel.CUSTOMER_IC;
                        entity.CUSTOMER_SEGMENT = actxmodel.AccInfoModel.CUSTOMER_SEGMENT;
                        entity.CUSTOMER_TYPE = actxmodel.AccInfoModel.CUSTOMER_TYPE;
                        entity.OPERATING_INSTRUCTION = actxmodel.AccInfoModel.OPERATING_INSTRUCTION;
                        entity.ORIGINATING_BRANCH = actxmodel.AccInfoModel.ORIGINATING_BRANCH;
                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";

                        db.CDMA_ACCOUNT_INFO.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges(identity.ProfileId.ToString(), actxmodel.AccInfoModel.CUSTOMER_NO);
                    }
                    if(entity2 == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", actxmodel.AccServicesModel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity2.ACCOUNT_NUMBER = actxmodel.AccServicesModel.ACCOUNT_NUMBER;
                        entity2.CARD_PREFERENCE = actxmodel.AccServicesModel.CARD_PREFERENCE;
                        entity2.ELECTRONIC_BANKING_PREFERENCE = actxmodel.AccServicesModel.ELECTRONIC_BANKING_PREFERENCE;
                        entity2.STATEMENT_PREFERENCES = actxmodel.AccServicesModel.STATEMENT_PREFERENCES;
                        entity2.TRANSACTION_ALERT_PREFERENCE = actxmodel.AccServicesModel.TRANSACTION_ALERT_PREFERENCE;
                        entity2.STATEMENT_FREQUENCY = actxmodel.AccServicesModel.STATEMENT_FREQUENCY;
                        entity2.CHEQUE_BOOK_REQUISITION = actxmodel.AccServicesModel.CHEQUE_BOOK_REQUISITION;
                        entity2.CHEQUE_LEAVES_REQUIRED = actxmodel.AccServicesModel.CHEQUE_LEAVES_REQUIRED;
                        entity2.CHEQUE_CONFIRMATION = actxmodel.AccServicesModel.CHEQUE_CONFIRMATION;
                        entity2.CHEQUE_CONFIRMATION_THRESHOLD = actxmodel.AccServicesModel.CHEQUE_CONFIRMATION_THRESHOLD;
                        entity2.CHEQUE_CONFIRM_THRESHOLD_RANGE = actxmodel.AccServicesModel.CHEQUE_CONFIRM_THRESHOLD_RANGE;
                        entity2.ONLINE_TRANSFER_LIMIT = actxmodel.AccServicesModel.ONLINE_TRANSFER_LIMIT;
                        entity2.ONLINE_TRANSFER_LIMIT_RANGE = actxmodel.AccServicesModel.ONLINE_TRANSFER_LIMIT_RANGE;
                        entity2.TOKEN = actxmodel.AccServicesModel.TOKEN;
                        entity2.ACCOUNT_SIGNATORY = actxmodel.AccServicesModel.ACCOUNT_SIGNATORY;
                        entity2.SECOND_SIGNATORY = actxmodel.AccServicesModel.SECOND_SIGNATORY;
                        entity2.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity2.LAST_MODIFIED_DATE = DateTime.Now;
                        entity2.AUTHORISED = "U";

                        db.CDMA_ACCT_SERVICES_REQUIRED.Attach(entity2);
                        db.Entry(entity2).State = EntityState.Modified;
                        db.SaveChanges(identity.ProfileId.ToString(), actxmodel.AccServicesModel.CUSTOMER_NO);
                    }
                }

                SuccessNotification("ACCOUNT INFO Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = actxmodel.AccInfoModel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }
            PrepareModel(actxmodel);
            return View(actxmodel);
        }

        public ActionResult Create()
        {
            var accountInfoModel = new AccInfoModel();
            var accountServiceModel = new AccServicesModel();
            var model = new AccInfoCtxModel();

            model.AccInfoModel = accountInfoModel;
            model.AccServicesModel = accountServiceModel;

            PrepareModel(model);
            return View(model);
        }

        // POST: AccInfoContext/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccInfoCtxModel actxmodel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string ip_address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (ModelState.IsValid)
            {
                CDMA_ACCOUNT_INFO accInfo = new CDMA_ACCOUNT_INFO
                {
                    CUSTOMER_NO = actxmodel.AccInfoModel.CUSTOMER_NO,
                    ACCOUNT_HOLDER = actxmodel.AccInfoModel.ACCOUNT_HOLDER,
                    TYPE_OF_ACCOUNT = actxmodel.AccInfoModel.TYPE_OF_ACCOUNT,
                    ACCOUNT_NUMBER = actxmodel.AccInfoModel.ACCOUNT_NUMBER,
                    ACCOUNT_OFFICER = actxmodel.AccInfoModel.ACCOUNT_OFFICER,
                    ACCOUNT_TITLE = actxmodel.AccInfoModel.ACCOUNT_TITLE,
                    BRANCH = actxmodel.AccInfoModel.BRANCH,
                    BRANCH_CLASS = actxmodel.AccInfoModel.BRANCH_CLASS,
                    BUSINESS_DIVISION = actxmodel.AccInfoModel.BUSINESS_DIVISION,
                    BUSINESS_SEGMENT = actxmodel.AccInfoModel.BUSINESS_SEGMENT,
                    BUSINESS_SIZE = actxmodel.AccInfoModel.BUSINESS_SIZE,
                    BVN_NUMBER = actxmodel.AccInfoModel.BVN_NUMBER,
                    CAV_REQUIRED = actxmodel.AccInfoModel.CAV_REQUIRED,
                    CUSTOMER_IC = actxmodel.AccInfoModel.CUSTOMER_IC,
                    CUSTOMER_SEGMENT = actxmodel.AccInfoModel.CUSTOMER_SEGMENT,
                    CUSTOMER_TYPE = actxmodel.AccInfoModel.CUSTOMER_TYPE,
                    OPERATING_INSTRUCTION = actxmodel.AccInfoModel.OPERATING_INSTRUCTION,
                    ORIGINATING_BRANCH = actxmodel.AccInfoModel.ORIGINATING_BRANCH,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                db.CDMA_ACCOUNT_INFO.Add(accInfo);
                db.SaveChanges();

                CDMA_ACCT_SERVICES_REQUIRED accServices = new CDMA_ACCT_SERVICES_REQUIRED
                {
                    CUSTOMER_NO = actxmodel.AccInfoModel.CUSTOMER_NO,
                    ACCOUNT_NUMBER = actxmodel.AccServicesModel.ACCOUNT_NUMBER,
                    CARD_PREFERENCE = actxmodel.AccServicesModel.CARD_PREFERENCE,
                    ELECTRONIC_BANKING_PREFERENCE = actxmodel.AccServicesModel.ELECTRONIC_BANKING_PREFERENCE,
                    STATEMENT_PREFERENCES = actxmodel.AccServicesModel.STATEMENT_PREFERENCES,
                    TRANSACTION_ALERT_PREFERENCE = actxmodel.AccServicesModel.TRANSACTION_ALERT_PREFERENCE,
                    STATEMENT_FREQUENCY = actxmodel.AccServicesModel.STATEMENT_FREQUENCY,
                    CHEQUE_BOOK_REQUISITION = actxmodel.AccServicesModel.CHEQUE_BOOK_REQUISITION,
                    CHEQUE_LEAVES_REQUIRED = actxmodel.AccServicesModel.CHEQUE_LEAVES_REQUIRED,
                    CHEQUE_CONFIRMATION = actxmodel.AccServicesModel.CHEQUE_CONFIRMATION,
                    CHEQUE_CONFIRMATION_THRESHOLD = actxmodel.AccServicesModel.CHEQUE_CONFIRMATION_THRESHOLD,
                    CHEQUE_CONFIRM_THRESHOLD_RANGE = actxmodel.AccServicesModel.CHEQUE_CONFIRM_THRESHOLD_RANGE,
                    ONLINE_TRANSFER_LIMIT = actxmodel.AccServicesModel.ONLINE_TRANSFER_LIMIT,
                    ONLINE_TRANSFER_LIMIT_RANGE = actxmodel.AccServicesModel.ONLINE_TRANSFER_LIMIT_RANGE,
                    TOKEN = actxmodel.AccServicesModel.TOKEN,
                    ACCOUNT_SIGNATORY = actxmodel.AccServicesModel.ACCOUNT_SIGNATORY,
                    SECOND_SIGNATORY = actxmodel.AccServicesModel.SECOND_SIGNATORY,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                db.CDMA_ACCT_SERVICES_REQUIRED.Add(accServices);
                db.SaveChanges();

                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New Account Info has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = actxmodel.AccInfoModel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }
            PrepareModel(actxmodel);
            return View(actxmodel);
        }

        [NonAction]
        protected virtual void PrepareModel(AccInfoCtxModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");


            var accountInfo = new AccInfoModel();
            var accountService = new AccServicesModel();

            accountInfo.AccountHolder.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountInfo.AccountHolder.Add(new SelectListItem { Text = "No", Value = "N" });
            accountInfo.CavRequired.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountInfo.CavRequired.Add(new SelectListItem { Text = "No", Value = "N" });
            accountInfo.TypesOfAccount = new SelectList(db.CDMA_ACCOUNT_TYPE, "ACCOUNT_ID", "ACCOUNT_NAME").ToList();
            accountInfo.Branches = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
            accountInfo.BranchClasses = new SelectList(db.CDMA_BRANCH_CLASS, "ID", "CLASS").ToList();
            accountInfo.BusinessDivisions = new SelectList(db.CDMA_BUSINESS_DIVISION, "ID", "DIVISION").ToList();
            accountInfo.BusinessSegments = new SelectList(db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT").ToList();
            accountInfo.BusinessSizes = new SelectList(db.CDMA_BUSINESS_SIZE, "SIZE_ID", "SIZE_RANGE").ToList();
            accountInfo.CustomerSegments = new SelectList(db.CDMA_CUSTOMER_SEGMENT, "ID", "SEGMENT").ToList();
            accountInfo.CustomerTypes = new SelectList(db.CDMA_CUSTOMER_TYPE, "TYPE_ID", "CUSTOMER_TYPE").ToList();
            accountInfo.OriginatingBranch = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();

            accountService.OnlineTransferLimit.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.OnlineTransferLimit.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.CardPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.CardPreference.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.ElectronicBankingPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.ElectronicBankingPreference.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.StatementPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.StatementPreference.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.TransactionAlertPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.TransactionAlertPreference.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.StatementFrequency.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.StatementFrequency.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.ChequeBookRequisition.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.ChequeBookRequisition.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.ChequeLeavesRequired.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.ChequeLeavesRequired.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.ChequeConfirmation.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.ChequeConfirmation.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.ChequeConfirmationThreshold.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.ChequeConfirmationThreshold.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.Tokens.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            accountService.Tokens.Add(new SelectListItem { Text = "No", Value = "N" });
            accountService.OnlineTransferLimitRange = new SelectList(db.CDMA_ONLINE_TRANSFER_LIMIT, "LIMIT_ID", "DESCRIPTION").ToList();
            accountService.ChequeConfirmationThresholdRange = new SelectList(db.CDMA_CHEQUE_CONFIRM_THRESHOLD, "INCOME_ID", "EXPECTED_INCOME_BAND").ToList();

            model.AccInfoModel = accountInfo;
            model.AccServicesModel = accountService;
        }

        [NonAction]
        public virtual void PrepareAccountInfoModel(AccInfoModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AccountHolder.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.AccountHolder.Add(new SelectListItem { Text = "No", Value = "N" });
            model.CavRequired.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.CavRequired.Add(new SelectListItem { Text = "No", Value = "N" });
            model.TypesOfAccount = new SelectList(db.CDMA_ACCOUNT_TYPE, "ACCOUNT_ID", "ACCOUNT_NAME").ToList();
            model.Branches = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
            model.BranchClasses = new SelectList(db.CDMA_BRANCH_CLASS, "ID", "CLASS").ToList();
            model.BusinessDivisions = new SelectList(db.CDMA_BUSINESS_DIVISION, "ID", "DIVISION").ToList();
            model.BusinessSegments = new SelectList(db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT").ToList();
            model.BusinessSizes = new SelectList(db.CDMA_BUSINESS_SIZE, "SIZE_ID", "SIZE_RANGE").ToList();
            model.CustomerSegments = new SelectList(db.CDMA_CUSTOMER_SEGMENT, "ID", "SEGMENT").ToList();
            model.CustomerTypes = new SelectList(db.CDMA_CUSTOMER_TYPE, "TYPE_ID", "CUSTOMER_TYPE").ToList();
            model.OriginatingBranch = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
        }

        [NonAction]
        public virtual void PrepareAccountServiceModel(AccServicesModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.OnlineTransferLimit.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.OnlineTransferLimit.Add(new SelectListItem { Text = "No", Value = "N" });
            model.CardPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.CardPreference.Add(new SelectListItem { Text = "No", Value = "N" });
            model.ElectronicBankingPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.ElectronicBankingPreference.Add(new SelectListItem { Text = "No", Value = "N" });
            model.StatementPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.StatementPreference.Add(new SelectListItem { Text = "No", Value = "N" });
            model.TransactionAlertPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.TransactionAlertPreference.Add(new SelectListItem { Text = "No", Value = "N" });
            model.StatementFrequency.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.StatementFrequency.Add(new SelectListItem { Text = "No", Value = "N" });
            model.ChequeBookRequisition.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.ChequeBookRequisition.Add(new SelectListItem { Text = "No", Value = "N" });
            model.ChequeLeavesRequired.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.ChequeLeavesRequired.Add(new SelectListItem { Text = "No", Value = "N" });
            model.ChequeConfirmation.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.ChequeConfirmation.Add(new SelectListItem { Text = "No", Value = "N" });
            model.ChequeConfirmationThreshold.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.ChequeConfirmationThreshold.Add(new SelectListItem { Text = "No", Value = "N" });
            model.Tokens.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.Tokens.Add(new SelectListItem { Text = "No", Value = "N" });
            model.OnlineTransferLimitRange = new SelectList(db.CDMA_ONLINE_TRANSFER_LIMIT, "LIMIT_ID", "DESCRIPTION").ToList();
            model.ChequeConfirmationThresholdRange = new SelectList(db.CDMA_CHEQUE_CONFIRM_THRESHOLD, "INCOME_ID", "EXPECTED_INCOME_BAND").ToList();
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("approve")]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(AccInfoCtxModel actxmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {

                _dqQueService.ApproveExceptionQueItems(actxmodel.AccInfoModel.ExceptionId.ToString());
                _dqQueService.ApproveExceptionQueItems(actxmodel.AccServicesModel.ExceptionId.ToString());
                //using (var db = new AppDbContext())
                //{
                //    var entity = db.CDMA_INDIVIDUAL_NEXT_OF_KIN.FirstOrDefault(o => o.CUSTOMER_NO == nokmodel.CUSTOMER_NO);
                //    if (entity == null)
                //    {
                //        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", nokmodel.CUSTOMER_NO);
                //        ModelState.AddModelError("", errorMessage);
                //    }
                //    else
                //    {                       
                //        entity.AUTHORISED = "A";
                //        db.CDMA_INDIVIDUAL_NEXT_OF_KIN.Attach(entity);
                //        db.Entry(entity).State = EntityState.Modified;
                //        db.SaveChanges();

                //    }
                //}

                SuccessNotification("Account Info Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = actxmodel.AccInfoModel.CUSTOMER_NO }) : RedirectToAction("Authorize", "AccInfoContext");
                //return RedirectToAction("Index");
            }
            PrepareModel(actxmodel);
            return View(actxmodel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove(AccInfoCtxModel actxmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_ACCOUNT_INFO.FirstOrDefault(o => o.CUSTOMER_NO == actxmodel.AccInfoModel.CUSTOMER_NO);
                    var entity2 = db.CDMA_ACCT_SERVICES_REQUIRED.FirstOrDefault(o => o.CUSTOMER_NO == actxmodel.AccServicesModel.CUSTOMER_NO);
                    if (entity == null || entity2 == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", actxmodel.AccInfoModel.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
                    {
                        entity.AUTHORISED = "N";
                        db.CDMA_ACCOUNT_INFO.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;

                        entity2.AUTHORISED = "N";
                        db.CDMA_ACCT_SERVICES_REQUIRED.Attach(entity2);
                        db.Entry(entity2).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                SuccessNotification("Account Info Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = actxmodel.AccInfoModel.CUSTOMER_NO }) : RedirectToAction("Authorize", "AccInfoContext");
                //return RedirectToAction("Index");
            }
            PrepareModel(actxmodel);
            return View(actxmodel);
        }

    }
}