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
using CMdm.Services.Messaging;
using CMdm.Entities.ViewModels;

namespace CMdm.UI.Web.Controllers
{
    public class AccInfoContextController : BaseController
    {
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        private IMessagingService _messageService;

        public AccInfoContextController()
        {
            _dqQueService = new DqQueService();
            _messageService = new MessagingService();
        }

        public ActionResult Authorize(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            //var model = new AccInfoCtxModel();

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }
            //get all changed columns

            AccInfoCtxModel model = new AccInfoCtxModel();

            var changeId = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_ACCOUNT_INFO" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId); //.Select(a=>a.PROPERTYNAME);

            var changeId2 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_ACCT_SERVICES_REQUIRED" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet2 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId2); //.Select(a=>a.PROPERTYNAME);

            AccInfoModel accountInfo = (from c in _db.CDMA_ACCOUNT_INFO
                               where c.CUSTOMER_NO == id
                               where c.AUTHORISED == "U"
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

            AccServicesModel accountService = (from c in _db.CDMA_ACCT_SERVICES_REQUIRED
                                    where c.CUSTOMER_NO == id
                                  where c.AUTHORISED == "U"
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

                if (model.AccInfoModel != null)
                {
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
                }
                
                if(model.AccServicesModel != null)
                {
                    foreach (var item in model.AccServicesModel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in changedSet2)
                        {
                            if (item2.PROPERTYNAME == item.Name)
                            {
                                ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                            }
                        }
                    }
                }
                
                model.AccInfoModel.ReadOnlyForm = "True";
                model.AccServicesModel.ReadOnlyForm = "True";

            PrepareAccountInfoModel(model.AccInfoModel);
            PrepareAccountServiceModel(model.AccServicesModel);
            //PrepareModel(model);
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }
            int records = _db.CDMA_ACCOUNT_INFO.Count(o => o.CUSTOMER_NO == id);
            int records2 = _db.CDMA_ACCT_SERVICES_REQUIRED.Count(o => o.CUSTOMER_NO == id);

            AccInfoCtxModel model = new AccInfoCtxModel();
            AccInfoModel accountInfo = new AccInfoModel();
            AccServicesModel accountService = new AccServicesModel();
            if(records > 1)
            {
                accountInfo = (from c in _db.CDMA_ACCOUNT_INFO
                               where c.CUSTOMER_NO == id
                               where c.AUTHORISED == "U"
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
                                }).FirstOrDefault();
            }
            else if(records == 1)
            {
                accountInfo = (from c in _db.CDMA_ACCOUNT_INFO
                               where c.CUSTOMER_NO == id
                               where c.AUTHORISED == "A"
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
                               }).FirstOrDefault();
            }
            
            if(records2 > 1)
            {
                accountService = (from c in _db.CDMA_ACCT_SERVICES_REQUIRED
                                  where c.CUSTOMER_NO == id
                                  where c.AUTHORISED == "U"
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
            }
            else if(records2 == 1)
            {
                accountService = (from c in _db.CDMA_ACCT_SERVICES_REQUIRED
                                  where c.CUSTOMER_NO == id
                                  where c.AUTHORISED == "A"
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
                
            }

            PrepareAccountInfoModel(accountInfo);
            PrepareAccountServiceModel(accountService);

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
            bool updateFlag = false;

            if (ModelState.IsValid)
            {
                CDMA_ACCOUNT_INFO originalObject = new CDMA_ACCOUNT_INFO();
                CDMA_ACCT_SERVICES_REQUIRED originalObject2 = new CDMA_ACCT_SERVICES_REQUIRED();

                using (var db = new AppDbContext())
                {
                    int records = db.CDMA_ACCOUNT_INFO.Count(o => o.CUSTOMER_NO == actxmodel.AccInfoModel.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    int records2 = db.CDMA_ACCT_SERVICES_REQUIRED.Count(o => o.CUSTOMER_NO == actxmodel.AccServicesModel.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()

                    if (records > 1)
                    {
                        updateFlag = true;
                        originalObject = _db.CDMA_ACCOUNT_INFO.Where(o => o.CUSTOMER_NO == actxmodel.AccInfoModel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();
                        var entity = db.CDMA_ACCOUNT_INFO.FirstOrDefault(o => o.CUSTOMER_NO == actxmodel.AccInfoModel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity != null)
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
                            //entity.AUTHORISED = "U";

                            db.CDMA_ACCOUNT_INFO.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), actxmodel.AccInfoModel.CUSTOMER_NO, updateFlag, originalObject);
                            _messageService.LogEmailJob(identity.ProfileId, entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if(records == 1)
                    {
                        updateFlag = false;
                        var entity = db.CDMA_ACCOUNT_INFO.FirstOrDefault(o => o.CUSTOMER_NO == actxmodel.AccInfoModel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject = _db.CDMA_ACCOUNT_INFO.Where(o => o.CUSTOMER_NO == actxmodel.AccInfoModel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject != null)
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
                            //entity.AUTHORISED = "U";
                            //

                            db.CDMA_ACCOUNT_INFO.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), actxmodel.AccInfoModel.CUSTOMER_NO, updateFlag, originalObject);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_ACCOUNT_INFO();
                            newentity.ACCOUNT_HOLDER = actxmodel.AccInfoModel.ACCOUNT_HOLDER;
                            newentity.TYPE_OF_ACCOUNT = actxmodel.AccInfoModel.TYPE_OF_ACCOUNT;
                            newentity.ACCOUNT_NUMBER = actxmodel.AccInfoModel.ACCOUNT_NUMBER;
                            newentity.ACCOUNT_OFFICER = actxmodel.AccInfoModel.ACCOUNT_OFFICER;
                            newentity.ACCOUNT_TITLE = actxmodel.AccInfoModel.ACCOUNT_TITLE;
                            newentity.BRANCH = actxmodel.AccInfoModel.BRANCH;
                            newentity.BRANCH_CLASS = actxmodel.AccInfoModel.BRANCH_CLASS;
                            newentity.BUSINESS_DIVISION = actxmodel.AccInfoModel.BUSINESS_DIVISION;
                            newentity.BUSINESS_SEGMENT = actxmodel.AccInfoModel.BUSINESS_SEGMENT;
                            newentity.BUSINESS_SIZE = actxmodel.AccInfoModel.BUSINESS_SIZE;
                            newentity.BVN_NUMBER = actxmodel.AccInfoModel.BVN_NUMBER;
                            newentity.CAV_REQUIRED = actxmodel.AccInfoModel.CAV_REQUIRED;
                            newentity.CUSTOMER_IC = actxmodel.AccInfoModel.CUSTOMER_IC;
                            newentity.CUSTOMER_SEGMENT = actxmodel.AccInfoModel.CUSTOMER_SEGMENT;
                            newentity.CUSTOMER_TYPE = actxmodel.AccInfoModel.CUSTOMER_TYPE;
                            newentity.OPERATING_INSTRUCTION = actxmodel.AccInfoModel.OPERATING_INSTRUCTION;
                            newentity.ORIGINATING_BRANCH = actxmodel.AccInfoModel.ORIGINATING_BRANCH;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = actxmodel.AccInfoModel.CUSTOMER_NO;
                            db.CDMA_ACCOUNT_INFO.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", actxmodel.AccInfoModel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }


                    if (records2 > 1)
                    {
                        updateFlag = true;
                        originalObject2 = _db.CDMA_ACCT_SERVICES_REQUIRED.Where(o => o.CUSTOMER_NO == actxmodel.AccServicesModel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();
                        var entity2 = db.CDMA_ACCT_SERVICES_REQUIRED.FirstOrDefault(o => o.CUSTOMER_NO == actxmodel.AccServicesModel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity2 != null)
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
                            //entity.AUTHORISED = "U";

                            db.CDMA_ACCT_SERVICES_REQUIRED.Attach(entity2);
                            db.Entry(entity2).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), actxmodel.AccServicesModel.CUSTOMER_NO, updateFlag, originalObject2);
                            _messageService.LogEmailJob(identity.ProfileId, entity2.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if (records == 1)
                    {
                        updateFlag = false;
                        var entity2 = db.CDMA_ACCT_SERVICES_REQUIRED.FirstOrDefault(o => o.CUSTOMER_NO == actxmodel.AccServicesModel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject2 = _db.CDMA_ACCT_SERVICES_REQUIRED.Where(o => o.CUSTOMER_NO == actxmodel.AccServicesModel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject2 != null)
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
                            //entity.AUTHORISED = "U";
                            //

                            db.CDMA_ACCT_SERVICES_REQUIRED.Attach(entity2);
                            db.Entry(entity2).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), actxmodel.AccServicesModel.CUSTOMER_NO, updateFlag, originalObject2);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_ACCT_SERVICES_REQUIRED();
                            newentity.ACCOUNT_NUMBER = actxmodel.AccServicesModel.ACCOUNT_NUMBER;
                            newentity.CARD_PREFERENCE = actxmodel.AccServicesModel.CARD_PREFERENCE;
                            newentity.ELECTRONIC_BANKING_PREFERENCE = actxmodel.AccServicesModel.ELECTRONIC_BANKING_PREFERENCE;
                            newentity.STATEMENT_PREFERENCES = actxmodel.AccServicesModel.STATEMENT_PREFERENCES;
                            newentity.TRANSACTION_ALERT_PREFERENCE = actxmodel.AccServicesModel.TRANSACTION_ALERT_PREFERENCE;
                            newentity.STATEMENT_FREQUENCY = actxmodel.AccServicesModel.STATEMENT_FREQUENCY;
                            newentity.CHEQUE_BOOK_REQUISITION = actxmodel.AccServicesModel.CHEQUE_BOOK_REQUISITION;
                            newentity.CHEQUE_LEAVES_REQUIRED = actxmodel.AccServicesModel.CHEQUE_LEAVES_REQUIRED;
                            newentity.CHEQUE_CONFIRMATION = actxmodel.AccServicesModel.CHEQUE_CONFIRMATION;
                            newentity.CHEQUE_CONFIRMATION_THRESHOLD = actxmodel.AccServicesModel.CHEQUE_CONFIRMATION_THRESHOLD;
                            newentity.CHEQUE_CONFIRM_THRESHOLD_RANGE = actxmodel.AccServicesModel.CHEQUE_CONFIRM_THRESHOLD_RANGE;
                            newentity.ONLINE_TRANSFER_LIMIT = actxmodel.AccServicesModel.ONLINE_TRANSFER_LIMIT;
                            newentity.ONLINE_TRANSFER_LIMIT_RANGE = actxmodel.AccServicesModel.ONLINE_TRANSFER_LIMIT_RANGE;
                            newentity.TOKEN = actxmodel.AccServicesModel.TOKEN;
                            newentity.ACCOUNT_SIGNATORY = actxmodel.AccServicesModel.ACCOUNT_SIGNATORY;
                            newentity.SECOND_SIGNATORY = actxmodel.AccServicesModel.SECOND_SIGNATORY;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = actxmodel.AccServicesModel.CUSTOMER_NO;
                            db.CDMA_ACCT_SERVICES_REQUIRED.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", actxmodel.AccServicesModel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                }

                SuccessNotification("ACCOUNT INFO Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = actxmodel.AccInfoModel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }

            PrepareAccountInfoModel(actxmodel.AccInfoModel);
            PrepareAccountServiceModel(actxmodel.AccServicesModel);
            //PrepareModel(actxmodel);
            return View(actxmodel);
        }

        public ActionResult Create()
        {
            AccInfoModel accountInfoModel = new AccInfoModel();
            AccServicesModel accountServiceModel = new AccServicesModel();
            AccInfoCtxModel model = new AccInfoCtxModel();

            model.AccInfoModel = accountInfoModel;
            model.AccServicesModel = accountServiceModel;

            PrepareAccountInfoModel(accountInfoModel);
            PrepareAccountServiceModel(accountServiceModel);
            //PrepareModel(model);
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
                _db.CDMA_ACCOUNT_INFO.Add(accInfo);
                _db.SaveChanges();

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
                _db.CDMA_ACCT_SERVICES_REQUIRED.Add(accServices);
                _db.SaveChanges();

                //_localizationService.GetResource("Admin.Configuration.Stores.Added")
                SuccessNotification("New Account Info has been Added");
                //do activity log
                return continueEditing ? RedirectToAction("Edit", new { id = actxmodel.AccInfoModel.CUSTOMER_NO }) : RedirectToAction("Create");
                //return RedirectToAction("Index");
            }

            PrepareAccountInfoModel(actxmodel.AccInfoModel);
            PrepareAccountServiceModel(actxmodel.AccServicesModel);
            //PrepareModel(actxmodel);
            return View(actxmodel);
        }

        //[NonAction]
        //protected virtual void PrepareModel(AccInfoCtxModel model)
        //{
        //    //if (model == null)
        //    //    throw new ArgumentNullException("model");


        //    AccInfoModel accountInfo = new AccInfoModel();
        //    AccServicesModel accountService = new AccServicesModel();

        //    accountInfo.AccountHolder.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountInfo.AccountHolder.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountInfo.CavRequired.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountInfo.CavRequired.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountInfo.TypesOfAccount = new SelectList(_db.CDMA_ACCOUNT_TYPE, "ACCOUNT_ID", "ACCOUNT_NAME").ToList();
        //    accountInfo.Branches = new SelectList(_db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
        //    accountInfo.BranchClasses = new SelectList(_db.CDMA_BRANCH_CLASS, "ID", "CLASS").ToList();
        //    accountInfo.BusinessDivisions = new SelectList(_db.CDMA_BUSINESS_DIVISION, "ID", "DIVISION").ToList();
        //    accountInfo.BusinessSegments = new SelectList(_db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT").ToList();
        //    accountInfo.BusinessSizes = new SelectList(_db.CDMA_BUSINESS_SIZE, "SIZE_ID", "SIZE_RANGE").ToList();
        //    accountInfo.CustomerSegments = new SelectList(_db.CDMA_CUSTOMER_SEGMENT, "ID", "SEGMENT").ToList();
        //    accountInfo.CustomerTypes = new SelectList(_db.CDMA_CUSTOMER_TYPE, "TYPE_ID", "CUSTOMER_TYPE").ToList();
        //    accountInfo.OriginatingBranch = new SelectList(_db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();

        //    accountService.OnlineTransferLimit.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.OnlineTransferLimit.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.CardPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.CardPreference.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.ElectronicBankingPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.ElectronicBankingPreference.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.StatementPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.StatementPreference.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.TransactionAlertPreference.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.TransactionAlertPreference.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.StatementFrequency.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.StatementFrequency.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.ChequeBookRequisition.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.ChequeBookRequisition.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.ChequeLeavesRequired.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.ChequeLeavesRequired.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.ChequeConfirmation.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.ChequeConfirmation.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.ChequeConfirmationThreshold.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.ChequeConfirmationThreshold.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.Tokens.Add(new SelectListItem { Text = "Yes", Value = "Y" });
        //    accountService.Tokens.Add(new SelectListItem { Text = "No", Value = "N" });
        //    accountService.OnlineTransferLimitRange = new SelectList(_db.CDMA_ONLINE_TRANSFER_LIMIT, "LIMIT_ID", "DESCRIPTION").ToList();
        //    accountService.ChequeConfirmationThresholdRange = new SelectList(_db.CDMA_CHEQUE_CONFIRM_THRESHOLD, "INCOME_ID", "EXPECTED_INCOME_BAND").ToList();

        //    model.AccInfoModel = accountInfo;
        //    model.AccServicesModel = accountService;
        //}

        [NonAction]
        public virtual void PrepareAccountInfoModel(AccInfoModel model)
        {
            //if (model == null)
               // throw new ArgumentNullException("model");
            model.AccountHolder.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.AccountHolder.Add(new SelectListItem { Text = "No", Value = "N" });
            model.CavRequired.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.CavRequired.Add(new SelectListItem { Text = "No", Value = "N" });
            model.TypesOfAccount = new SelectList(_db.CDMA_ACCOUNT_TYPE, "ACCOUNT_ID", "ACCOUNT_NAME").ToList();
            model.Branches = new SelectList(_db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
            model.BranchClasses = new SelectList(_db.CDMA_BRANCH_CLASS, "ID", "CLASS").ToList();
            model.BusinessDivisions = new SelectList(_db.CDMA_BUSINESS_DIVISION, "ID", "DIVISION").ToList();
            model.BusinessSegments = new SelectList(_db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT").ToList();
            model.BusinessSizes = new SelectList(_db.CDMA_BUSINESS_SIZE, "SIZE_ID", "SIZE_RANGE").ToList();
            model.CustomerSegments = new SelectList(_db.CDMA_CUSTOMER_SEGMENT, "ID", "SEGMENT").ToList();
            model.CustomerTypes = new SelectList(_db.CDMA_CUSTOMER_TYPE, "TYPE_ID", "CUSTOMER_TYPE").ToList();
            model.OriginatingBranch = new SelectList(_db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
        }

        [NonAction]
        public virtual void PrepareAccountServiceModel(AccServicesModel model)
        {
            //if (model == null)
            //    throw new ArgumentNullException("model");

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
            model.OnlineTransferLimitRange = new SelectList(_db.CDMA_ONLINE_TRANSFER_LIMIT, "LIMIT_ID", "DESCRIPTION").ToList();
            model.ChequeConfirmationThresholdRange = new SelectList(_db.CDMA_CHEQUE_CONFIRM_THRESHOLD, "INCOME_ID", "EXPECTED_INCOME_BAND").ToList();
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(AccInfoCtxModel actxmodel, bool disapproveRecord)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;

                int exceptionId = 0;
                if (routeValues.ContainsKey("id"))
                    exceptionId = int.Parse((string)routeValues["id"]);
                if (disapproveRecord)
                {
                    _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), actxmodel.AccInfoModel.AuthoriserRemarks);
                    SuccessNotification("Account Info Not Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, actxmodel.AccInfoModel.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(actxmodel.AccInfoModel.LastUpdatedby));
                }

                else
                {
                    _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(), identity.ProfileId);
                    SuccessNotification("Account Info Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, actxmodel.AccInfoModel.CUSTOMER_NO, MessageJobEnum.MailType.Authorize, Convert.ToInt32(actxmodel.AccInfoModel.LastUpdatedby));
                }

                return RedirectToAction("AuthList", "DQQue");
                //return RedirectToAction("Index");
            }

            PrepareAccountInfoModel(actxmodel.AccInfoModel);
            PrepareAccountServiceModel(actxmodel.AccServicesModel);
            //PrepareModel(actxmodel);
            return View(actxmodel);
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove_(AccInfoCtxModel actxmodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                _dqQueService.DisApproveExceptionQueItems(actxmodel.AccInfoModel.ExceptionId.ToString(), actxmodel.AccInfoModel.AuthoriserRemarks);
                _dqQueService.DisApproveExceptionQueItems(actxmodel.AccServicesModel.ExceptionId.ToString(), actxmodel.AccInfoModel.AuthoriserRemarks);

                SuccessNotification("Account Info Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = actxmodel.AccInfoModel.CUSTOMER_NO }) : RedirectToAction("Authorize", "AccInfoContext");
                //return RedirectToAction("Index");
            }

            PrepareAccountInfoModel(actxmodel.AccInfoModel);
            PrepareAccountServiceModel(actxmodel.AccServicesModel);
            //PrepareModel(actxmodel);
            return View(actxmodel);
        }

    }
}