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
    public class CorporateCustomerController : BaseController
    {
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        private IMessagingService _messageService;

        public CorporateCustomerController()
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

            CorpCustModel model = new CorpCustModel();

            var changeId = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_COMPANY_DETAILS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId); //.Select(a=>a.PROPERTYNAME);

            var changeId2 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_COMPANY_INFORMATION" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet2 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId2); //.Select(a=>a.PROPERTYNAME);

            var changeId3 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_BENEFICIALOWNERS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet3 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId3); //.Select(a=>a.PROPERTYNAME);

            var changeId4 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_CORP_ADDITIONAL_DETAILS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet4 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId4); //.Select(a=>a.PROPERTYNAME);

            CompDetailsModel compDetails = (from c in _db.CDMA_COMPANY_DETAILS
                                        where c.CUSTOMER_NO == id
                                        where c.AUTHORISED == "U"
                                        select new CompDetailsModel
                                        {
                                            CUSTOMER_NO = c.CUSTOMER_NO,
                                            CERT_OF_INCORP_REG_NO = c.CERT_OF_INCORP_REG_NO,
                                            JURISDICTION_OF_INCORP_REG = c.JURISDICTION_OF_INCORP_REG,
                                            SCUML_NO = c.SCUML_NO,
                                            GENDER_CONTROLLING_51_PERC = c.GENDER_CONTROLLING_51_PERC,
                                            SECTOR_OR_INDUSTRY = c.SECTOR_OR_INDUSTRY,
                                            OPERATING_BUSINESS_1 = c.OPERATING_BUSINESS_1,
                                            CITY_1 = c.CITY_1,
                                            COUNTRY_1 = c.COUNTRY_1,
                                            ZIP_CODE_1 = c.ZIP_CODE_1,
                                            BIZ_ADDRESS_REG_OFFICE_1 = c.BIZ_ADDRESS_REG_OFFICE_1,
                                            OPERATING_BUSINESS_2 = c.OPERATING_BUSINESS_2,
                                            CITY_2 = c.CITY_2,
                                            COUNTRY_2 = c.COUNTRY_2,
                                            ZIP_CODE_2 = c.ZIP_CODE_2,
                                            BIZ_ADDRESS_REG_OFFICE_2 = c.BIZ_ADDRESS_REG_OFFICE_2,
                                            COMPANY_EMAIL_ADDRESS = c.COMPANY_EMAIL_ADDRESS,
                                            WEBSITE = c.WEBSITE,
                                            OFFICE_NUMBER = c.OFFICE_NUMBER,
                                            MOBILE_NUMBER = c.MOBILE_NUMBER,
                                            TIN = c.TIN,
                                            CRMB_NO_BORROWER_CODE = c.CRMB_NO_BORROWER_CODE,
                                            EXPECTED_ANNUAL_TURNOVER = c.EXPECTED_ANNUAL_TURNOVER,
                                            IS_COMPANY_ON_STOCK_EXCH = c.IS_COMPANY_ON_STOCK_EXCH,
                                            STOCK_EXCHANGE_NAME = c.STOCK_EXCHANGE_NAME,
                                            BRANCH_CODE = c.BRANCH_CODE,
                                            LastUpdatedby = c.LAST_MODIFIED_BY,
                                            LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                            LastAuthdby = c.AUTHORISED_BY,
                                            LastAuthDate = c.AUTHORISED_DATE,
                                            ExceptionId = querecord.EXCEPTION_ID
                                        }).FirstOrDefault();

            CompInfoModel compInfo = (from c in _db.CDMA_COMPANY_INFORMATION
                                               where c.CUSTOMER_NO == id
                                               where c.AUTHORISED == "U"
                                               select new CompInfoModel
                                               {
                                                   CUSTOMER_NO = c.CUSTOMER_NO,
                                                   COMPANY_NAME = c.COMPANY_NAME,
                                                   DATE_OF_INCORP_REGISTRATION = c.DATE_OF_INCORP_REGISTRATION,
                                                   CUSTOMER_TYPE = c.CUSTOMER_TYPE,
                                                   REGISTERED_ADDRESS = c.REGISTERED_ADDRESS,
                                                   CATEGORY_OF_BUSINESS = c.CATEGORY_OF_BUSINESS,
                                                   BRANCH_CODE = c.BRANCH_CODE,
                                                   EOC_RISK = c.EOC_RISK,
                                                   CURRENT_LINE_OF_BUSINESS = c.CURRENT_LINE_OF_BUSINESS,
                                                   COMPANY_NETWORTH_SOA = c.COMPANY_NETWORTH_SOA,
                                                   INTRODUCED_BY = c.INTRODUCED_BY,
                                                   BRF_INVESTIGATION_MEDIA_REPORT = c.BRF_INVESTIGATION_MEDIA_REPORT,
                                                   INVESTIGATION_MEDIA_REPORT = c.INVESTIGATION_MEDIA_REPORT,
                                                   ADD_VERIFICATION_STATUS = c.ADD_VERIFICATION_STATUS,
                                                   COUNTERPARTIES_CLIENTS_OF_CUST = c.COUNTERPARTIES_CLIENTS_OF_CUST,
                                                   ANTICIPATED_TRANS_OUTFLOW = c.ANTICIPATED_TRANS_OUTFLOW,
                                                   ANTICIPATED_TRANS_INFLOW = c.ANTICIPATED_TRANS_INFLOW,
                                                   LENGTH_OF_STAY_INBUS = c.LENGTH_OF_STAY_INBUS,
                                                   DATE_OF_COMMENCEMENT = c.DATE_OF_COMMENCEMENT,
                                                   SOURCE_OF_ASSET = c.SOURCE_OF_ASSET,
                                                   HISTORY_OF_CUSTOMER = c.HISTORY_OF_CUSTOMER,
                                                   LastUpdatedby = c.LAST_MODIFIED_BY,
                                                   LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                                   LastAuthdby = c.AUTHORISED_BY,
                                                   LastAuthDate = c.AUTHORISED_DATE,
                                                   ExceptionId = querecord.EXCEPTION_ID
                                               }).FirstOrDefault();

            BenOwnersModel benOwners = (from c in _db.CDMA_BENEFICIALOWNERS
                                      where c.ORGKEY == id
                                      where c.AUTHORISED == "U"
                                      select new BenOwnersModel
                                      {
                                          ORGKEY = c.ORGKEY,
                                          PERCENTAGE_OF_BENEFICIARY = c.PERCENTAGE_OF_BENEFICIARY,
                                          NAMES_OF_BENEFICIARY = c.NAMES_OF_BENEFICIARY,
                                          PRIMARY_SOL_ID = c.PRIMARY_SOL_ID,
                                          LastUpdatedby = c.LAST_MODIFIED_BY,
                                          LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                          LastAuthdby = c.AUTHORISED_BY,
                                          LastAuthDate = c.AUTHORISED_DATE,
                                          ExceptionId = querecord.EXCEPTION_ID
                                      }).FirstOrDefault();

            CorpADDModel corpADD = (from c in _db.CDMA_CORP_ADDITIONAL_DETAILS
                                        where c.CUSTOMER_NO == id
                                        where c.AUTHORISED == "U"
                                        select new CorpADDModel
                                        {
                                            CUSTOMER_NO = c.CUSTOMER_NO,
                                            BRANCH_CODE = c.BRANCH_CODE,
                                            COUNTERPARTIES_CLIENTS_OF_CUST = c.COUNTERPARTIES_CLIENTS_OF_CUST,
                                            PARENT_COMPANY_CTRY_INCORP = c.PARENT_COMPANY_CTRY_INCORP,
                                            LastUpdatedby = c.LAST_MODIFIED_BY,
                                            LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                            LastAuthdby = c.AUTHORISED_BY,
                                            LastAuthDate = c.AUTHORISED_DATE,
                                            ExceptionId = querecord.EXCEPTION_ID
                                        }).FirstOrDefault();

            model.CompDetailsModel = compDetails;
            model.CompInfoModel = compInfo;
            model.BenOwnersModel = benOwners;
            model.CorpADDModel = corpADD;

            if (model.CompDetailsModel != null)
            {
                foreach (var item in model.CompDetailsModel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
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

            if (model.CompInfoModel != null)
            {
                foreach (var item in model.CompInfoModel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
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

            if (model.BenOwnersModel != null)
            {
                foreach (var item in model.BenOwnersModel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet3)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }
                }
            }

            if (model.CorpADDModel != null)
            {
                foreach (var item in model.CorpADDModel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet4)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }
                }
            }

            model.CompDetailsModel.ReadOnlyForm = "True";
            model.CompInfoModel.ReadOnlyForm = "True";
            model.BenOwnersModel.ReadOnlyForm = "True";
            model.CorpADDModel.ReadOnlyForm = "True";

            PrepareCompDetailsModel(model.CompDetailsModel);
            PrepareCompInfoModel(model.CompInfoModel);
            PrepareBenOwnersModel(model.BenOwnersModel);
            PrepareCorpAddModel(model.CorpADDModel);
            //PrepareModel(model);
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }
            int records = _db.CDMA_COMPANY_DETAILS.Count(o => o.CUSTOMER_NO == id);
            int records2 = _db.CDMA_COMPANY_INFORMATION.Count(o => o.CUSTOMER_NO == id);
            int records3 = _db.CDMA_BENEFICIALOWNERS.Count(o => o.ORGKEY == id);
            int records4 = _db.CDMA_CORP_ADDITIONAL_DETAILS.Count(o => o.CUSTOMER_NO == id);

            CorpCustModel model = new CorpCustModel();
            CompDetailsModel compDetails = new CompDetailsModel();
            CompInfoModel compInfo = new CompInfoModel();
            BenOwnersModel benOwners = new BenOwnersModel();
            CorpADDModel corpADD = new CorpADDModel();

            if (records > 1)
            {
                compDetails = (from c in _db.CDMA_COMPANY_DETAILS
                               where c.CUSTOMER_NO == id
                               where c.AUTHORISED == "U"
                               select new CompDetailsModel
                               {
                                   CUSTOMER_NO = c.CUSTOMER_NO,
                                   CERT_OF_INCORP_REG_NO = c.CERT_OF_INCORP_REG_NO,
                                   JURISDICTION_OF_INCORP_REG = c.JURISDICTION_OF_INCORP_REG,
                                   SCUML_NO = c.SCUML_NO,
                                   GENDER_CONTROLLING_51_PERC = c.GENDER_CONTROLLING_51_PERC,
                                   SECTOR_OR_INDUSTRY = c.SECTOR_OR_INDUSTRY,
                                   OPERATING_BUSINESS_1 = c.OPERATING_BUSINESS_1,
                                   CITY_1 = c.CITY_1,
                                   COUNTRY_1 = c.COUNTRY_1,
                                   ZIP_CODE_1 = c.ZIP_CODE_1,
                                   BIZ_ADDRESS_REG_OFFICE_1 = c.BIZ_ADDRESS_REG_OFFICE_1,
                                   OPERATING_BUSINESS_2 = c.OPERATING_BUSINESS_2,
                                   CITY_2 = c.CITY_2,
                                   COUNTRY_2 = c.COUNTRY_2,
                                   ZIP_CODE_2 = c.ZIP_CODE_2,
                                   BIZ_ADDRESS_REG_OFFICE_2 = c.BIZ_ADDRESS_REG_OFFICE_2,
                                   COMPANY_EMAIL_ADDRESS = c.COMPANY_EMAIL_ADDRESS,
                                   WEBSITE = c.WEBSITE,
                                   OFFICE_NUMBER = c.OFFICE_NUMBER,
                                   MOBILE_NUMBER = c.MOBILE_NUMBER,
                                   TIN = c.TIN,
                                   CRMB_NO_BORROWER_CODE = c.CRMB_NO_BORROWER_CODE,
                                   EXPECTED_ANNUAL_TURNOVER = c.EXPECTED_ANNUAL_TURNOVER,
                                   IS_COMPANY_ON_STOCK_EXCH = c.IS_COMPANY_ON_STOCK_EXCH,
                                   STOCK_EXCHANGE_NAME = c.STOCK_EXCHANGE_NAME,
                                   BRANCH_CODE = c.BRANCH_CODE,
                               }).FirstOrDefault();
            }
            else if (records == 1)
            {
                compDetails = (from c in _db.CDMA_COMPANY_DETAILS
                               where c.CUSTOMER_NO == id
                               where c.AUTHORISED == "A"
                               select new CompDetailsModel
                               {
                                   CUSTOMER_NO = c.CUSTOMER_NO,
                                   CERT_OF_INCORP_REG_NO = c.CERT_OF_INCORP_REG_NO,
                                   JURISDICTION_OF_INCORP_REG = c.JURISDICTION_OF_INCORP_REG,
                                   SCUML_NO = c.SCUML_NO,
                                   GENDER_CONTROLLING_51_PERC = c.GENDER_CONTROLLING_51_PERC,
                                   SECTOR_OR_INDUSTRY = c.SECTOR_OR_INDUSTRY,
                                   OPERATING_BUSINESS_1 = c.OPERATING_BUSINESS_1,
                                   CITY_1 = c.CITY_1,
                                   COUNTRY_1 = c.COUNTRY_1,
                                   ZIP_CODE_1 = c.ZIP_CODE_1,
                                   BIZ_ADDRESS_REG_OFFICE_1 = c.BIZ_ADDRESS_REG_OFFICE_1,
                                   OPERATING_BUSINESS_2 = c.OPERATING_BUSINESS_2,
                                   CITY_2 = c.CITY_2,
                                   COUNTRY_2 = c.COUNTRY_2,
                                   ZIP_CODE_2 = c.ZIP_CODE_2,
                                   BIZ_ADDRESS_REG_OFFICE_2 = c.BIZ_ADDRESS_REG_OFFICE_2,
                                   COMPANY_EMAIL_ADDRESS = c.COMPANY_EMAIL_ADDRESS,
                                   WEBSITE = c.WEBSITE,
                                   OFFICE_NUMBER = c.OFFICE_NUMBER,
                                   MOBILE_NUMBER = c.MOBILE_NUMBER,
                                   TIN = c.TIN,
                                   CRMB_NO_BORROWER_CODE = c.CRMB_NO_BORROWER_CODE,
                                   EXPECTED_ANNUAL_TURNOVER = c.EXPECTED_ANNUAL_TURNOVER,
                                   IS_COMPANY_ON_STOCK_EXCH = c.IS_COMPANY_ON_STOCK_EXCH,
                                   STOCK_EXCHANGE_NAME = c.STOCK_EXCHANGE_NAME,
                                   BRANCH_CODE = c.BRANCH_CODE,
                               }).FirstOrDefault();
            }
            if (records2 > 1)
            {
                compInfo = (from c in _db.CDMA_COMPANY_INFORMATION
                                  where c.CUSTOMER_NO == id
                                  where c.AUTHORISED == "U"
                                  select new CompInfoModel
                                  {
                                      CUSTOMER_NO = c.CUSTOMER_NO,
                                      COMPANY_NAME = c.COMPANY_NAME,
                                      DATE_OF_INCORP_REGISTRATION = c.DATE_OF_INCORP_REGISTRATION,
                                      CUSTOMER_TYPE = c.CUSTOMER_TYPE,
                                      REGISTERED_ADDRESS = c.REGISTERED_ADDRESS,
                                      CATEGORY_OF_BUSINESS = c.CATEGORY_OF_BUSINESS,
                                      BRANCH_CODE = c.BRANCH_CODE,
                                      EOC_RISK = c.EOC_RISK,
                                      CURRENT_LINE_OF_BUSINESS = c.CURRENT_LINE_OF_BUSINESS,
                                      COMPANY_NETWORTH_SOA = c.COMPANY_NETWORTH_SOA,
                                      INTRODUCED_BY = c.INTRODUCED_BY,
                                      BRF_INVESTIGATION_MEDIA_REPORT = c.BRF_INVESTIGATION_MEDIA_REPORT,
                                      INVESTIGATION_MEDIA_REPORT = c.INVESTIGATION_MEDIA_REPORT,
                                      ADD_VERIFICATION_STATUS = c.ADD_VERIFICATION_STATUS,
                                      COUNTERPARTIES_CLIENTS_OF_CUST = c.COUNTERPARTIES_CLIENTS_OF_CUST,
                                      ANTICIPATED_TRANS_OUTFLOW = c.ANTICIPATED_TRANS_OUTFLOW,
                                      ANTICIPATED_TRANS_INFLOW = c.ANTICIPATED_TRANS_INFLOW,
                                      LENGTH_OF_STAY_INBUS = c.LENGTH_OF_STAY_INBUS,
                                      DATE_OF_COMMENCEMENT = c.DATE_OF_COMMENCEMENT,
                                      SOURCE_OF_ASSET = c.SOURCE_OF_ASSET,
                                      HISTORY_OF_CUSTOMER = c.HISTORY_OF_CUSTOMER,
                                  }).FirstOrDefault();
            }
            else if (records2 == 1)
            {
                compInfo = (from c in _db.CDMA_COMPANY_INFORMATION
                            where c.CUSTOMER_NO == id
                                  where c.AUTHORISED == "A"
                                  select new CompInfoModel
                                  {
                                      CUSTOMER_NO = c.CUSTOMER_NO,
                                      COMPANY_NAME = c.COMPANY_NAME,
                                      DATE_OF_INCORP_REGISTRATION = c.DATE_OF_INCORP_REGISTRATION,
                                      CUSTOMER_TYPE = c.CUSTOMER_TYPE,
                                      REGISTERED_ADDRESS = c.REGISTERED_ADDRESS,
                                      CATEGORY_OF_BUSINESS = c.CATEGORY_OF_BUSINESS,
                                      BRANCH_CODE = c.BRANCH_CODE,
                                      EOC_RISK = c.EOC_RISK,
                                      CURRENT_LINE_OF_BUSINESS = c.CURRENT_LINE_OF_BUSINESS,
                                      COMPANY_NETWORTH_SOA = c.COMPANY_NETWORTH_SOA,
                                      INTRODUCED_BY = c.INTRODUCED_BY,
                                      BRF_INVESTIGATION_MEDIA_REPORT = c.BRF_INVESTIGATION_MEDIA_REPORT,
                                      INVESTIGATION_MEDIA_REPORT = c.INVESTIGATION_MEDIA_REPORT,
                                      ADD_VERIFICATION_STATUS = c.ADD_VERIFICATION_STATUS,
                                      COUNTERPARTIES_CLIENTS_OF_CUST = c.COUNTERPARTIES_CLIENTS_OF_CUST,
                                      ANTICIPATED_TRANS_OUTFLOW = c.ANTICIPATED_TRANS_OUTFLOW,
                                      ANTICIPATED_TRANS_INFLOW = c.ANTICIPATED_TRANS_INFLOW,
                                      LENGTH_OF_STAY_INBUS = c.LENGTH_OF_STAY_INBUS,
                                      DATE_OF_COMMENCEMENT = c.DATE_OF_COMMENCEMENT,
                                      SOURCE_OF_ASSET = c.SOURCE_OF_ASSET,
                                      HISTORY_OF_CUSTOMER = c.HISTORY_OF_CUSTOMER,
                                  }).FirstOrDefault();
            }
            if (records3 > 1)
            {
                benOwners = (from c in _db.CDMA_BENEFICIALOWNERS
                            where c.ORGKEY == id
                            where c.AUTHORISED == "U"
                            select new BenOwnersModel
                            {
                                ORGKEY = c.ORGKEY,
                                PERCENTAGE_OF_BENEFICIARY = c.PERCENTAGE_OF_BENEFICIARY,
                                NAMES_OF_BENEFICIARY = c.NAMES_OF_BENEFICIARY,
                                PRIMARY_SOL_ID = c.PRIMARY_SOL_ID,
                            }).FirstOrDefault();
            }
            else if (records3 == 1)
            {
                benOwners = (from c in _db.CDMA_BENEFICIALOWNERS
                             where c.ORGKEY == id
                            where c.AUTHORISED == "A"
                            select new BenOwnersModel
                            {
                                ORGKEY = c.ORGKEY,
                                PERCENTAGE_OF_BENEFICIARY = c.PERCENTAGE_OF_BENEFICIARY,
                                NAMES_OF_BENEFICIARY = c.NAMES_OF_BENEFICIARY,
                                PRIMARY_SOL_ID = c.PRIMARY_SOL_ID,
                            }).FirstOrDefault();
            }
            if (records4 > 1)
            {
                corpADD = (from c in _db.CDMA_CORP_ADDITIONAL_DETAILS
                             where c.CUSTOMER_NO == id
                             where c.AUTHORISED == "U"
                             select new CorpADDModel
                             {
                                 CUSTOMER_NO = c.CUSTOMER_NO,
                                 BRANCH_CODE = c.BRANCH_CODE,
                                 COUNTERPARTIES_CLIENTS_OF_CUST = c.COUNTERPARTIES_CLIENTS_OF_CUST,
                                 PARENT_COMPANY_CTRY_INCORP = c.PARENT_COMPANY_CTRY_INCORP,
                             }).FirstOrDefault();
            }
            else if (records4 == 1)
            {
                corpADD = (from c in _db.CDMA_CORP_ADDITIONAL_DETAILS
                             where c.CUSTOMER_NO == id
                             where c.AUTHORISED == "A"
                             select new CorpADDModel
                             {
                                 CUSTOMER_NO = c.CUSTOMER_NO,
                                 BRANCH_CODE = c.BRANCH_CODE,
                                 COUNTERPARTIES_CLIENTS_OF_CUST = c.COUNTERPARTIES_CLIENTS_OF_CUST,
                                 PARENT_COMPANY_CTRY_INCORP = c.PARENT_COMPANY_CTRY_INCORP,
                             }).FirstOrDefault();
            }

            PrepareCompDetailsModel(compDetails);
            PrepareCompInfoModel(compInfo);
            PrepareBenOwnersModel(benOwners);
            PrepareCorpAddModel(corpADD);

            model.CompDetailsModel = compDetails;
            model.CompInfoModel = compInfo;
            model.BenOwnersModel = benOwners;
            model.CorpADDModel = corpADD;

            if (model == null)
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
        public ActionResult Edit(CorpCustModel corpCustModel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var identity = ((CustomPrincipal)User).CustomIdentity;
            bool updateFlag = false;

            if (ModelState.IsValid)
            {
                CDMA_COMPANY_DETAILS originalObject = new CDMA_COMPANY_DETAILS();
                CDMA_COMPANY_INFORMATION originalObject2 = new CDMA_COMPANY_INFORMATION();
                CDMA_BENEFICIALOWNERS originalObject3 = new CDMA_BENEFICIALOWNERS();
                CDMA_CORP_ADDITIONAL_DETAILS originalObject4 = new CDMA_CORP_ADDITIONAL_DETAILS();

                using (var db = new AppDbContext())
                {
                    int records = db.CDMA_COMPANY_DETAILS.Count(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO);
                    int records2 = db.CDMA_COMPANY_INFORMATION.Count(o => o.CUSTOMER_NO == corpCustModel.CompInfoModel.CUSTOMER_NO);
                    int records3 = db.CDMA_BENEFICIALOWNERS.Count(o => o.ORGKEY == corpCustModel.BenOwnersModel.ORGKEY);
                    int records4 = db.CDMA_CORP_ADDITIONAL_DETAILS.Count(o => o.CUSTOMER_NO == corpCustModel.CorpADDModel.CUSTOMER_NO);

                    if (records > 1)
                    {
                        updateFlag = true;
                        originalObject = _db.CDMA_COMPANY_DETAILS.Where(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();
                        var entity = db.CDMA_COMPANY_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity != null)
                        {
                            entity.CERT_OF_INCORP_REG_NO = corpCustModel.CompDetailsModel.CERT_OF_INCORP_REG_NO;
                            entity.JURISDICTION_OF_INCORP_REG = corpCustModel.CompDetailsModel.JURISDICTION_OF_INCORP_REG;
                            entity.SCUML_NO = corpCustModel.CompDetailsModel.SCUML_NO;
                            entity.GENDER_CONTROLLING_51_PERC = corpCustModel.CompDetailsModel.GENDER_CONTROLLING_51_PERC;
                            entity.SECTOR_OR_INDUSTRY = corpCustModel.CompDetailsModel.SECTOR_OR_INDUSTRY;
                            entity.OPERATING_BUSINESS_1 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_1;
                            entity.CITY_1 = corpCustModel.CompDetailsModel.CITY_1;
                            entity.COUNTRY_1 = corpCustModel.CompDetailsModel.COUNTRY_1;
                            entity.ZIP_CODE_1 = corpCustModel.CompDetailsModel.ZIP_CODE_1;
                            entity.BIZ_ADDRESS_REG_OFFICE_1 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_1;
                            entity.OPERATING_BUSINESS_2 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_2;
                            entity.CITY_2 = corpCustModel.CompDetailsModel.CITY_2;
                            entity.COUNTRY_2 = corpCustModel.CompDetailsModel.COUNTRY_2;
                            entity.ZIP_CODE_2 = corpCustModel.CompDetailsModel.ZIP_CODE_2;
                            entity.BIZ_ADDRESS_REG_OFFICE_2 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_2;
                            entity.COMPANY_EMAIL_ADDRESS = corpCustModel.CompDetailsModel.COMPANY_EMAIL_ADDRESS;
                            entity.WEBSITE = corpCustModel.CompDetailsModel.WEBSITE;
                            entity.OFFICE_NUMBER = corpCustModel.CompDetailsModel.OFFICE_NUMBER;
                            entity.MOBILE_NUMBER = corpCustModel.CompDetailsModel.MOBILE_NUMBER;
                            entity.TIN = corpCustModel.CompDetailsModel.TIN;
                            entity.CRMB_NO_BORROWER_CODE = corpCustModel.CompDetailsModel.CRMB_NO_BORROWER_CODE;
                            entity.EXPECTED_ANNUAL_TURNOVER = corpCustModel.CompDetailsModel.EXPECTED_ANNUAL_TURNOVER;
                            entity.IS_COMPANY_ON_STOCK_EXCH = corpCustModel.CompDetailsModel.IS_COMPANY_ON_STOCK_EXCH;
                            entity.STOCK_EXCHANGE_NAME = corpCustModel.CompDetailsModel.STOCK_EXCHANGE_NAME;
                            entity.BRANCH_CODE = corpCustModel.CompDetailsModel.BRANCH_CODE;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";

                            db.CDMA_COMPANY_DETAILS.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), corpCustModel.CompDetailsModel.CUSTOMER_NO, updateFlag, originalObject);
                            _messageService.LogEmailJob(identity.ProfileId, entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if (records == 1)
                    {
                        updateFlag = false;
                        var entity = db.CDMA_COMPANY_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject = _db.CDMA_COMPANY_DETAILS.Where(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject != null)
                        {
                            entity.CERT_OF_INCORP_REG_NO = corpCustModel.CompDetailsModel.CERT_OF_INCORP_REG_NO;
                            entity.JURISDICTION_OF_INCORP_REG = corpCustModel.CompDetailsModel.JURISDICTION_OF_INCORP_REG;
                            entity.SCUML_NO = corpCustModel.CompDetailsModel.SCUML_NO;
                            entity.GENDER_CONTROLLING_51_PERC = corpCustModel.CompDetailsModel.GENDER_CONTROLLING_51_PERC;
                            entity.SECTOR_OR_INDUSTRY = corpCustModel.CompDetailsModel.SECTOR_OR_INDUSTRY;
                            entity.OPERATING_BUSINESS_1 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_1;
                            entity.CITY_1 = corpCustModel.CompDetailsModel.CITY_1;
                            entity.COUNTRY_1 = corpCustModel.CompDetailsModel.COUNTRY_1;
                            entity.ZIP_CODE_1 = corpCustModel.CompDetailsModel.ZIP_CODE_1;
                            entity.BIZ_ADDRESS_REG_OFFICE_1 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_1;
                            entity.OPERATING_BUSINESS_2 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_2;
                            entity.CITY_2 = corpCustModel.CompDetailsModel.CITY_2;
                            entity.COUNTRY_2 = corpCustModel.CompDetailsModel.COUNTRY_2;
                            entity.ZIP_CODE_2 = corpCustModel.CompDetailsModel.ZIP_CODE_2;
                            entity.BIZ_ADDRESS_REG_OFFICE_2 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_2;
                            entity.COMPANY_EMAIL_ADDRESS = corpCustModel.CompDetailsModel.COMPANY_EMAIL_ADDRESS;
                            entity.WEBSITE = corpCustModel.CompDetailsModel.WEBSITE;
                            entity.OFFICE_NUMBER = corpCustModel.CompDetailsModel.OFFICE_NUMBER;
                            entity.MOBILE_NUMBER = corpCustModel.CompDetailsModel.MOBILE_NUMBER;
                            entity.TIN = corpCustModel.CompDetailsModel.TIN;
                            entity.CRMB_NO_BORROWER_CODE = corpCustModel.CompDetailsModel.CRMB_NO_BORROWER_CODE;
                            entity.EXPECTED_ANNUAL_TURNOVER = corpCustModel.CompDetailsModel.EXPECTED_ANNUAL_TURNOVER;
                            entity.IS_COMPANY_ON_STOCK_EXCH = corpCustModel.CompDetailsModel.IS_COMPANY_ON_STOCK_EXCH;
                            entity.STOCK_EXCHANGE_NAME = corpCustModel.CompDetailsModel.STOCK_EXCHANGE_NAME;
                            entity.BRANCH_CODE = corpCustModel.CompDetailsModel.BRANCH_CODE;
                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";
                            //

                            db.CDMA_COMPANY_DETAILS.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), corpCustModel.CompDetailsModel.CUSTOMER_NO, updateFlag, originalObject);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_COMPANY_DETAILS();
                            newentity.CERT_OF_INCORP_REG_NO = corpCustModel.CompDetailsModel.CERT_OF_INCORP_REG_NO;
                            newentity.JURISDICTION_OF_INCORP_REG = corpCustModel.CompDetailsModel.JURISDICTION_OF_INCORP_REG;
                            newentity.SCUML_NO = corpCustModel.CompDetailsModel.SCUML_NO;
                            newentity.GENDER_CONTROLLING_51_PERC = corpCustModel.CompDetailsModel.GENDER_CONTROLLING_51_PERC;
                            newentity.SECTOR_OR_INDUSTRY = corpCustModel.CompDetailsModel.SECTOR_OR_INDUSTRY;
                            newentity.OPERATING_BUSINESS_1 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_1;
                            newentity.CITY_1 = corpCustModel.CompDetailsModel.CITY_1;
                            newentity.COUNTRY_1 = corpCustModel.CompDetailsModel.COUNTRY_1;
                            newentity.ZIP_CODE_1 = corpCustModel.CompDetailsModel.ZIP_CODE_1;
                            newentity.BIZ_ADDRESS_REG_OFFICE_1 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_1;
                            newentity.OPERATING_BUSINESS_2 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_2;
                            newentity.CITY_2 = corpCustModel.CompDetailsModel.CITY_2;
                            newentity.COUNTRY_2 = corpCustModel.CompDetailsModel.COUNTRY_2;
                            newentity.ZIP_CODE_2 = corpCustModel.CompDetailsModel.ZIP_CODE_2;
                            newentity.BIZ_ADDRESS_REG_OFFICE_2 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_2;
                            newentity.COMPANY_EMAIL_ADDRESS = corpCustModel.CompDetailsModel.COMPANY_EMAIL_ADDRESS;
                            newentity.WEBSITE = corpCustModel.CompDetailsModel.WEBSITE;
                            newentity.OFFICE_NUMBER = corpCustModel.CompDetailsModel.OFFICE_NUMBER;
                            newentity.MOBILE_NUMBER = corpCustModel.CompDetailsModel.MOBILE_NUMBER;
                            newentity.TIN = corpCustModel.CompDetailsModel.TIN;
                            newentity.CRMB_NO_BORROWER_CODE = corpCustModel.CompDetailsModel.CRMB_NO_BORROWER_CODE;
                            newentity.EXPECTED_ANNUAL_TURNOVER = corpCustModel.CompDetailsModel.EXPECTED_ANNUAL_TURNOVER;
                            newentity.IS_COMPANY_ON_STOCK_EXCH = corpCustModel.CompDetailsModel.IS_COMPANY_ON_STOCK_EXCH;
                            newentity.STOCK_EXCHANGE_NAME = corpCustModel.CompDetailsModel.STOCK_EXCHANGE_NAME;
                            newentity.BRANCH_CODE = corpCustModel.CompDetailsModel.BRANCH_CODE;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = corpCustModel.CompDetailsModel.CUSTOMER_NO;
                            db.CDMA_COMPANY_DETAILS.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", corpCustModel.CompDetailsModel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }

                    if (records2 > 1)
                    {
                        updateFlag = true;
                        originalObject2 = _db.CDMA_COMPANY_INFORMATION.Where(o => o.CUSTOMER_NO == corpCustModel.CompInfoModel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();
                        var entity2 = db.CDMA_COMPANY_INFORMATION.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.CompInfoModel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity2 != null)
                        {
                            entity2.COMPANY_NAME = corpCustModel.CompInfoModel.COMPANY_NAME;
                            entity2.DATE_OF_INCORP_REGISTRATION = corpCustModel.CompInfoModel.DATE_OF_INCORP_REGISTRATION;
                            entity2.CUSTOMER_TYPE = corpCustModel.CompInfoModel.CUSTOMER_TYPE;
                            entity2.REGISTERED_ADDRESS = corpCustModel.CompInfoModel.REGISTERED_ADDRESS;
                            entity2.CATEGORY_OF_BUSINESS = corpCustModel.CompInfoModel.CATEGORY_OF_BUSINESS;
                            entity2.BRANCH_CODE = corpCustModel.CompInfoModel.BRANCH_CODE;
                            entity2.EOC_RISK = corpCustModel.CompInfoModel.EOC_RISK;
                            entity2.CURRENT_LINE_OF_BUSINESS = corpCustModel.CompInfoModel.CURRENT_LINE_OF_BUSINESS;
                            entity2.COMPANY_NETWORTH_SOA = corpCustModel.CompInfoModel.COMPANY_NETWORTH_SOA;
                            entity2.INTRODUCED_BY = corpCustModel.CompInfoModel.INTRODUCED_BY;
                            entity2.BRF_INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.BRF_INVESTIGATION_MEDIA_REPORT;
                            entity2.INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.INVESTIGATION_MEDIA_REPORT;
                            entity2.ADD_VERIFICATION_STATUS = corpCustModel.CompInfoModel.ADD_VERIFICATION_STATUS;
                            entity2.COUNTERPARTIES_CLIENTS_OF_CUST = corpCustModel.CompInfoModel.COUNTERPARTIES_CLIENTS_OF_CUST;
                            entity2.ANTICIPATED_TRANS_OUTFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_OUTFLOW;
                            entity2.ANTICIPATED_TRANS_INFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_INFLOW;
                            entity2.LENGTH_OF_STAY_INBUS = corpCustModel.CompInfoModel.LENGTH_OF_STAY_INBUS;
                            entity2.DATE_OF_COMMENCEMENT = corpCustModel.CompInfoModel.DATE_OF_COMMENCEMENT;
                            entity2.SOURCE_OF_ASSET = corpCustModel.CompInfoModel.SOURCE_OF_ASSET;
                            entity2.HISTORY_OF_CUSTOMER = corpCustModel.CompInfoModel.HISTORY_OF_CUSTOMER;
                            entity2.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity2.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";

                            db.CDMA_COMPANY_INFORMATION.Attach(entity2);
                            db.Entry(entity2).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), corpCustModel.CompInfoModel.CUSTOMER_NO, updateFlag, originalObject2);
                            _messageService.LogEmailJob(identity.ProfileId, entity2.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if (records2 == 1)
                    {
                        updateFlag = false;
                        var entity2 = db.CDMA_COMPANY_INFORMATION.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.CompInfoModel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject2 = _db.CDMA_COMPANY_INFORMATION.Where(o => o.CUSTOMER_NO == corpCustModel.CompInfoModel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject2 != null)
                        {
                            entity2.COMPANY_NAME = corpCustModel.CompInfoModel.COMPANY_NAME;
                            entity2.DATE_OF_INCORP_REGISTRATION = corpCustModel.CompInfoModel.DATE_OF_INCORP_REGISTRATION;
                            entity2.CUSTOMER_TYPE = corpCustModel.CompInfoModel.CUSTOMER_TYPE;
                            entity2.REGISTERED_ADDRESS = corpCustModel.CompInfoModel.REGISTERED_ADDRESS;
                            entity2.CATEGORY_OF_BUSINESS = corpCustModel.CompInfoModel.CATEGORY_OF_BUSINESS;
                            entity2.BRANCH_CODE = corpCustModel.CompInfoModel.BRANCH_CODE;
                            entity2.EOC_RISK = corpCustModel.CompInfoModel.EOC_RISK;
                            entity2.CURRENT_LINE_OF_BUSINESS = corpCustModel.CompInfoModel.CURRENT_LINE_OF_BUSINESS;
                            entity2.COMPANY_NETWORTH_SOA = corpCustModel.CompInfoModel.COMPANY_NETWORTH_SOA;
                            entity2.INTRODUCED_BY = corpCustModel.CompInfoModel.INTRODUCED_BY;
                            entity2.BRF_INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.BRF_INVESTIGATION_MEDIA_REPORT;
                            entity2.INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.INVESTIGATION_MEDIA_REPORT;
                            entity2.ADD_VERIFICATION_STATUS = corpCustModel.CompInfoModel.ADD_VERIFICATION_STATUS;
                            entity2.COUNTERPARTIES_CLIENTS_OF_CUST = corpCustModel.CompInfoModel.COUNTERPARTIES_CLIENTS_OF_CUST;
                            entity2.ANTICIPATED_TRANS_OUTFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_OUTFLOW;
                            entity2.ANTICIPATED_TRANS_INFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_INFLOW;
                            entity2.LENGTH_OF_STAY_INBUS = corpCustModel.CompInfoModel.LENGTH_OF_STAY_INBUS;
                            entity2.DATE_OF_COMMENCEMENT = corpCustModel.CompInfoModel.DATE_OF_COMMENCEMENT;
                            entity2.SOURCE_OF_ASSET = corpCustModel.CompInfoModel.SOURCE_OF_ASSET;
                            entity2.HISTORY_OF_CUSTOMER = corpCustModel.CompInfoModel.HISTORY_OF_CUSTOMER;
                            entity2.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity2.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";
                            //

                            db.CDMA_COMPANY_INFORMATION.Attach(entity2);
                            db.Entry(entity2).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), corpCustModel.CompInfoModel.CUSTOMER_NO, updateFlag, originalObject2);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_COMPANY_INFORMATION();
                            newentity.COMPANY_NAME = corpCustModel.CompInfoModel.COMPANY_NAME;
                            newentity.DATE_OF_INCORP_REGISTRATION = corpCustModel.CompInfoModel.DATE_OF_INCORP_REGISTRATION;
                            newentity.CUSTOMER_TYPE = corpCustModel.CompInfoModel.CUSTOMER_TYPE;
                            newentity.CATEGORY_OF_BUSINESS = corpCustModel.CompInfoModel.CATEGORY_OF_BUSINESS;
                            newentity.BRANCH_CODE = corpCustModel.CompInfoModel.BRANCH_CODE;
                            newentity.EOC_RISK = corpCustModel.CompInfoModel.EOC_RISK;
                            newentity.CURRENT_LINE_OF_BUSINESS = corpCustModel.CompInfoModel.CURRENT_LINE_OF_BUSINESS;
                            newentity.COMPANY_NETWORTH_SOA = corpCustModel.CompInfoModel.COMPANY_NETWORTH_SOA;
                            newentity.INTRODUCED_BY = corpCustModel.CompInfoModel.INTRODUCED_BY;
                            newentity.BRF_INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.BRF_INVESTIGATION_MEDIA_REPORT;
                            newentity.INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.INVESTIGATION_MEDIA_REPORT;
                            newentity.ADD_VERIFICATION_STATUS = corpCustModel.CompInfoModel.ADD_VERIFICATION_STATUS;
                            newentity.COUNTERPARTIES_CLIENTS_OF_CUST = corpCustModel.CompInfoModel.COUNTERPARTIES_CLIENTS_OF_CUST;
                            newentity.ANTICIPATED_TRANS_OUTFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_OUTFLOW;
                            newentity.ANTICIPATED_TRANS_INFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_INFLOW;
                            newentity.LENGTH_OF_STAY_INBUS = corpCustModel.CompInfoModel.LENGTH_OF_STAY_INBUS;
                            newentity.DATE_OF_COMMENCEMENT = corpCustModel.CompInfoModel.DATE_OF_COMMENCEMENT;
                            newentity.SOURCE_OF_ASSET = corpCustModel.CompInfoModel.SOURCE_OF_ASSET;
                            newentity.HISTORY_OF_CUSTOMER = corpCustModel.CompInfoModel.HISTORY_OF_CUSTOMER;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = corpCustModel.CompInfoModel.CUSTOMER_NO;
                            db.CDMA_COMPANY_INFORMATION.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", corpCustModel.CompInfoModel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }

                    if (records3 > 1)
                    {
                        updateFlag = true;
                        originalObject3 = _db.CDMA_BENEFICIALOWNERS.Where(o => o.ORGKEY == corpCustModel.BenOwnersModel.ORGKEY && o.AUTHORISED == "U").FirstOrDefault();
                        var entity3 = db.CDMA_BENEFICIALOWNERS.FirstOrDefault(o => o.ORGKEY == corpCustModel.BenOwnersModel.ORGKEY && o.AUTHORISED == "U");

                        if (entity3 != null)
                        {
                            entity3.PERCENTAGE_OF_BENEFICIARY = corpCustModel.BenOwnersModel.PERCENTAGE_OF_BENEFICIARY;
                            entity3.NAMES_OF_BENEFICIARY = corpCustModel.BenOwnersModel.NAMES_OF_BENEFICIARY;
                            entity3.PRIMARY_SOL_ID = corpCustModel.BenOwnersModel.PRIMARY_SOL_ID;
                            entity3.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity3.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";

                            db.CDMA_BENEFICIALOWNERS.Attach(entity3);
                            db.Entry(entity3).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), corpCustModel.BenOwnersModel.ORGKEY, updateFlag, originalObject3);
                            _messageService.LogEmailJob(identity.ProfileId, entity3.ORGKEY, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if (records3 == 1)
                    {
                        updateFlag = false;
                        var entity3 = db.CDMA_BENEFICIALOWNERS.FirstOrDefault(o => o.ORGKEY == corpCustModel.BenOwnersModel.ORGKEY && o.AUTHORISED == "A");
                        originalObject3 = _db.CDMA_BENEFICIALOWNERS.Where(o => o.ORGKEY == corpCustModel.BenOwnersModel.ORGKEY && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject3 != null)
                        {
                            entity3.PERCENTAGE_OF_BENEFICIARY = corpCustModel.BenOwnersModel.PERCENTAGE_OF_BENEFICIARY;
                            entity3.NAMES_OF_BENEFICIARY = corpCustModel.BenOwnersModel.NAMES_OF_BENEFICIARY;
                            entity3.PRIMARY_SOL_ID = corpCustModel.BenOwnersModel.PRIMARY_SOL_ID;
                            entity3.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity3.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";
                            //

                            db.CDMA_BENEFICIALOWNERS.Attach(entity3);
                            db.Entry(entity3).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), corpCustModel.BenOwnersModel.ORGKEY, updateFlag, originalObject3);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_BENEFICIALOWNERS();
                            newentity.PERCENTAGE_OF_BENEFICIARY = corpCustModel.BenOwnersModel.PERCENTAGE_OF_BENEFICIARY;
                            newentity.NAMES_OF_BENEFICIARY = corpCustModel.BenOwnersModel.NAMES_OF_BENEFICIARY;
                            newentity.PRIMARY_SOL_ID = corpCustModel.BenOwnersModel.PRIMARY_SOL_ID;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.ORGKEY = corpCustModel.BenOwnersModel.ORGKEY;
                            db.CDMA_BENEFICIALOWNERS.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.ORGKEY, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", corpCustModel.BenOwnersModel.ORGKEY);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }

                    if (records4 > 1)
                    {
                        updateFlag = true;
                        originalObject4 = _db.CDMA_CORP_ADDITIONAL_DETAILS.Where(o => o.CUSTOMER_NO == corpCustModel.CorpADDModel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();
                        var entity4 = db.CDMA_CORP_ADDITIONAL_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.CorpADDModel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity4 != null)
                        {
                            entity4.BRANCH_CODE = corpCustModel.CorpADDModel.BRANCH_CODE;
                            entity4.COUNTERPARTIES_CLIENTS_OF_CUST = corpCustModel.CorpADDModel.COUNTERPARTIES_CLIENTS_OF_CUST;
                            entity4.PARENT_COMPANY_CTRY_INCORP = corpCustModel.CorpADDModel.PARENT_COMPANY_CTRY_INCORP;
                            entity4.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity4.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";

                            db.CDMA_CORP_ADDITIONAL_DETAILS.Attach(entity4);
                            db.Entry(entity4).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), corpCustModel.CorpADDModel.CUSTOMER_NO, updateFlag, originalObject4);
                            _messageService.LogEmailJob(identity.ProfileId, entity4.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if (records4 == 1)
                    {
                        updateFlag = false;
                        var entity4 = db.CDMA_CORP_ADDITIONAL_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.CorpADDModel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject4 = _db.CDMA_CORP_ADDITIONAL_DETAILS.Where(o => o.CUSTOMER_NO == corpCustModel.CorpADDModel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();

                        if (originalObject4 != null)
                        {
                            entity4.BRANCH_CODE = corpCustModel.CorpADDModel.BRANCH_CODE;
                            entity4.COUNTERPARTIES_CLIENTS_OF_CUST = corpCustModel.CorpADDModel.COUNTERPARTIES_CLIENTS_OF_CUST;
                            entity4.PARENT_COMPANY_CTRY_INCORP = corpCustModel.CorpADDModel.PARENT_COMPANY_CTRY_INCORP;
                            entity4.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity4.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";
                            //

                            db.CDMA_CORP_ADDITIONAL_DETAILS.Attach(entity4);
                            db.Entry(entity4).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), corpCustModel.CorpADDModel.CUSTOMER_NO, updateFlag, originalObject4);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_CORP_ADDITIONAL_DETAILS();
                            newentity.BRANCH_CODE = corpCustModel.CorpADDModel.BRANCH_CODE;
                            newentity.COUNTERPARTIES_CLIENTS_OF_CUST = corpCustModel.CorpADDModel.COUNTERPARTIES_CLIENTS_OF_CUST;
                            newentity.PARENT_COMPANY_CTRY_INCORP = corpCustModel.CorpADDModel.PARENT_COMPANY_CTRY_INCORP;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = corpCustModel.CorpADDModel.CUSTOMER_NO;
                            db.CDMA_CORP_ADDITIONAL_DETAILS.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", corpCustModel.CorpADDModel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                }

                SuccessNotification("Corporate Customer Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = corpCustModel.CorpADDModel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }

            PrepareCompDetailsModel(corpCustModel.CompDetailsModel);
            PrepareCompInfoModel(corpCustModel.CompInfoModel);
            PrepareBenOwnersModel(corpCustModel.BenOwnersModel);
            PrepareCorpAddModel(corpCustModel.CorpADDModel);
            //PrepareModel(actxmodel);
            return View(corpCustModel);
        }

        public ActionResult Create()
        {
            CorpCustModel model = new CorpCustModel();
            CompDetailsModel compDetailsModel = new CompDetailsModel();
            CompInfoModel compInfoModel = new CompInfoModel();
            BenOwnersModel benOwnersModel = new BenOwnersModel();
            CorpADDModel corpADDModel = new CorpADDModel();

            model.CompDetailsModel = compDetailsModel;
            model.CompInfoModel = compInfoModel;
            model.BenOwnersModel = benOwnersModel;
            model.CorpADDModel = corpADDModel;

            PrepareCompDetailsModel(compDetailsModel);
            PrepareCompInfoModel(compInfoModel);
            PrepareBenOwnersModel(benOwnersModel);
            PrepareCorpAddModel(corpADDModel);
            //PrepareModel(model);
            return View(model);
        }

        // POST: AccInfoContext/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CorpCustModel corpCustModel, bool continueEditing)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageStores))
            //    return AccessDeniedView();
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            string ip_address = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (ModelState.IsValid)
            {
                CDMA_COMPANY_DETAILS compDetails = new CDMA_COMPANY_DETAILS
                {
                    CUSTOMER_NO = corpCustModel.CompDetailsModel.CUSTOMER_NO,
                    CERT_OF_INCORP_REG_NO = corpCustModel.CompDetailsModel.CERT_OF_INCORP_REG_NO,
                    JURISDICTION_OF_INCORP_REG = corpCustModel.CompDetailsModel.JURISDICTION_OF_INCORP_REG,
                    SCUML_NO = corpCustModel.CompDetailsModel.SCUML_NO,
                    GENDER_CONTROLLING_51_PERC = corpCustModel.CompDetailsModel.GENDER_CONTROLLING_51_PERC,
                    SECTOR_OR_INDUSTRY = corpCustModel.CompDetailsModel.SECTOR_OR_INDUSTRY,
                    OPERATING_BUSINESS_1 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_1,
                    CITY_1 = corpCustModel.CompDetailsModel.CITY_1,
                    COUNTRY_1 = corpCustModel.CompDetailsModel.COUNTRY_1,
                    ZIP_CODE_1 = corpCustModel.CompDetailsModel.ZIP_CODE_1,
                    BIZ_ADDRESS_REG_OFFICE_1 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_1,
                    OPERATING_BUSINESS_2 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_2,
                    CITY_2 = corpCustModel.CompDetailsModel.CITY_2,
                    COUNTRY_2 = corpCustModel.CompDetailsModel.COUNTRY_2,
                    ZIP_CODE_2 = corpCustModel.CompDetailsModel.ZIP_CODE_2,
                    BIZ_ADDRESS_REG_OFFICE_2 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_2,
                    COMPANY_EMAIL_ADDRESS = corpCustModel.CompDetailsModel.COMPANY_EMAIL_ADDRESS,
                    WEBSITE = corpCustModel.CompDetailsModel.WEBSITE,
                    OFFICE_NUMBER = corpCustModel.CompDetailsModel.OFFICE_NUMBER,
                    MOBILE_NUMBER = corpCustModel.CompDetailsModel.MOBILE_NUMBER,
                    TIN = corpCustModel.CompDetailsModel.TIN,
                    CRMB_NO_BORROWER_CODE = corpCustModel.CompDetailsModel.CRMB_NO_BORROWER_CODE,
                    EXPECTED_ANNUAL_TURNOVER = corpCustModel.CompDetailsModel.EXPECTED_ANNUAL_TURNOVER,
                    IS_COMPANY_ON_STOCK_EXCH = corpCustModel.CompDetailsModel.IS_COMPANY_ON_STOCK_EXCH,
                    STOCK_EXCHANGE_NAME = corpCustModel.CompDetailsModel.STOCK_EXCHANGE_NAME,
                    BRANCH_CODE = corpCustModel.CompDetailsModel.BRANCH_CODE,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                _db.CDMA_COMPANY_DETAILS.Add(compDetails);
                _db.SaveChanges();

                CDMA_COMPANY_INFORMATION compInfo = new CDMA_COMPANY_INFORMATION
                {
                    CUSTOMER_NO = corpCustModel.CompInfoModel.CUSTOMER_NO,
                    COMPANY_NAME = corpCustModel.CompInfoModel.COMPANY_NAME,
                    DATE_OF_INCORP_REGISTRATION = corpCustModel.CompInfoModel.DATE_OF_INCORP_REGISTRATION,
                    CUSTOMER_TYPE = corpCustModel.CompInfoModel.CUSTOMER_TYPE,
                    CATEGORY_OF_BUSINESS = corpCustModel.CompInfoModel.CATEGORY_OF_BUSINESS,
                    BRANCH_CODE = corpCustModel.CompInfoModel.BRANCH_CODE,
                    EOC_RISK = corpCustModel.CompInfoModel.EOC_RISK,
                    CURRENT_LINE_OF_BUSINESS = corpCustModel.CompInfoModel.CURRENT_LINE_OF_BUSINESS,
                    COMPANY_NETWORTH_SOA = corpCustModel.CompInfoModel.COMPANY_NETWORTH_SOA,
                    INTRODUCED_BY = corpCustModel.CompInfoModel.INTRODUCED_BY,
                    BRF_INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.BRF_INVESTIGATION_MEDIA_REPORT,
                    INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.INVESTIGATION_MEDIA_REPORT,
                    ADD_VERIFICATION_STATUS = corpCustModel.CompInfoModel.ADD_VERIFICATION_STATUS,
                    COUNTERPARTIES_CLIENTS_OF_CUST = corpCustModel.CompInfoModel.COUNTERPARTIES_CLIENTS_OF_CUST,
                    ANTICIPATED_TRANS_OUTFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_OUTFLOW,
                    ANTICIPATED_TRANS_INFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_INFLOW,
                    LENGTH_OF_STAY_INBUS = corpCustModel.CompInfoModel.LENGTH_OF_STAY_INBUS,
                    DATE_OF_COMMENCEMENT = corpCustModel.CompInfoModel.DATE_OF_COMMENCEMENT,
                    SOURCE_OF_ASSET = corpCustModel.CompInfoModel.SOURCE_OF_ASSET,
                    HISTORY_OF_CUSTOMER = corpCustModel.CompInfoModel.HISTORY_OF_CUSTOMER,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                _db.CDMA_COMPANY_INFORMATION.Add(compInfo);
                _db.SaveChanges();

                CDMA_BENEFICIALOWNERS benOwners = new CDMA_BENEFICIALOWNERS
                {
                    ORGKEY = corpCustModel.BenOwnersModel.ORGKEY,
                    PERCENTAGE_OF_BENEFICIARY = corpCustModel.BenOwnersModel.PERCENTAGE_OF_BENEFICIARY,
                    NAMES_OF_BENEFICIARY = corpCustModel.BenOwnersModel.NAMES_OF_BENEFICIARY,
                    PRIMARY_SOL_ID = corpCustModel.BenOwnersModel.PRIMARY_SOL_ID,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                _db.CDMA_BENEFICIALOWNERS.Add(benOwners);
                _db.SaveChanges();

                CDMA_CORP_ADDITIONAL_DETAILS corpAddDetails = new CDMA_CORP_ADDITIONAL_DETAILS
                {
                    CUSTOMER_NO = corpCustModel.CorpADDModel.CUSTOMER_NO,
                    BRANCH_CODE = corpCustModel.CorpADDModel.BRANCH_CODE,
                    COUNTERPARTIES_CLIENTS_OF_CUST = corpCustModel.CorpADDModel.COUNTERPARTIES_CLIENTS_OF_CUST,
                    PARENT_COMPANY_CTRY_INCORP = corpCustModel.CorpADDModel.PARENT_COMPANY_CTRY_INCORP,
                    CREATED_BY = identity.ProfileId.ToString(),
                    CREATED_DATE = DateTime.Now,
                    LAST_MODIFIED_BY = identity.ProfileId.ToString(),
                    LAST_MODIFIED_DATE = DateTime.Now,
                    AUTHORISED_BY = null,
                    AUTHORISED_DATE = null,
                    IP_ADDRESS = ip_address
                };
                _db.CDMA_CORP_ADDITIONAL_DETAILS.Add(corpAddDetails);
                _db.SaveChanges();

                SuccessNotification("New Corporate Customer has been Added");
                return continueEditing ? RedirectToAction("Edit", new { id = corpCustModel.CompDetailsModel.CUSTOMER_NO }) : RedirectToAction("Create");
            }

            PrepareCompDetailsModel(corpCustModel.CompDetailsModel);
            PrepareCompInfoModel(corpCustModel.CompInfoModel);
            PrepareBenOwnersModel(corpCustModel.BenOwnersModel);
            PrepareCorpAddModel(corpCustModel.CorpADDModel);
            //PrepareModel(actxmodel);
            return View(corpCustModel);
        }

        [NonAction]
        public virtual void PrepareCompDetailsModel(CompDetailsModel model)
        {
            //if (model == null)
            // throw new ArgumentNullException("model");
            model.IsStockExchange.Add(new SelectListItem { Text = "Yes", Value = "Y" });
            model.IsStockExchange.Add(new SelectListItem { Text = "No", Value = "N" });
            model.Branches = new SelectList(_db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
            model.BusinessSegments = new SelectList(_db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT").ToList();
            model.Countries = new SelectList(_db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
        }

        [NonAction]
        public virtual void PrepareCompInfoModel(CompInfoModel model)
        {
            //if (model == null)
            //    throw new ArgumentNullException("model");

            model.BusinessSegments = new SelectList(_db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT").ToList();
            model.CustomerTypes = new SelectList(_db.CDMA_CUSTOMER_TYPE, "TYPE_ID", "CUSTOMER_TYPE").ToList();
            model.Branches = new SelectList(_db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
        }

        [NonAction]
        public virtual void PrepareBenOwnersModel(BenOwnersModel model)
        {
            //if (model == null)
            //    throw new ArgumentNullException("model");

            model.Branches = new SelectList(_db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
        }

        [NonAction]
        public virtual void PrepareCorpAddModel(CorpADDModel model)
        {
            //if (model == null)
            //    throw new ArgumentNullException("model");

            model.Branches = new SelectList(_db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(CorpCustModel corpCustModel, bool disapproveRecord)
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
                    _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), corpCustModel.CompDetailsModel.AuthoriserRemarks);
                    SuccessNotification("Corporate Customer Not Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, corpCustModel.CompDetailsModel.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(corpCustModel.CompDetailsModel.LastUpdatedby));
                }

                else
                {
                    _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(), identity.ProfileId);
                    SuccessNotification("Corporate Customer Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, corpCustModel.CompDetailsModel.CUSTOMER_NO, MessageJobEnum.MailType.Authorize, Convert.ToInt32(corpCustModel.CompDetailsModel.LastUpdatedby));
                }

                return RedirectToAction("AuthList", "DQQue");
                //return RedirectToAction("Index");
            }

            PrepareCompDetailsModel(corpCustModel.CompDetailsModel);
            PrepareCompInfoModel(corpCustModel.CompInfoModel);
            PrepareBenOwnersModel(corpCustModel.BenOwnersModel);
            PrepareCorpAddModel(corpCustModel.CorpADDModel);
            //PrepareModel(actxmodel);
            return View(corpCustModel);
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "continueEditing")]
        [FormValueRequired("disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult DisApprove_(CorpCustModel corpCustModel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            if (ModelState.IsValid)
            {
                _dqQueService.DisApproveExceptionQueItems(corpCustModel.CompDetailsModel.ExceptionId.ToString(), corpCustModel.CompDetailsModel.AuthoriserRemarks);

                SuccessNotification("Account Info Authorised");
                return continueEditing ? RedirectToAction("Authorize", new { id = corpCustModel.CompDetailsModel.CUSTOMER_NO }) : RedirectToAction("Authorize", "AccInfoContext");
                //return RedirectToAction("Index");
            }

            PrepareCompDetailsModel(corpCustModel.CompDetailsModel);
            PrepareCompInfoModel(corpCustModel.CompInfoModel);
            PrepareBenOwnersModel(corpCustModel.BenOwnersModel);
            PrepareCorpAddModel(corpCustModel.CorpADDModel);
            //PrepareModel(actxmodel);
            return View(corpCustModel);
        }


        #region scaffolded
        // GET: CorporateCustomer
        public ActionResult Index()
        {
            return View();
        }
        #endregion scaffolded
    }
}