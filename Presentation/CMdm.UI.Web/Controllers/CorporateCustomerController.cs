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

            var querecord = _dqQueService.GetCorpDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }
            //get all changed columns

            CorpCustModel model = new CorpCustModel();

            #region CompDetails
            CompDetailsModel compDetails = new CompDetailsModel();
            compDetails = (from c in _db.CDMA_COMPANY_DETAILS
                           where c.CUSTOMER_NO == querecord.CUST_ID
                           where c.AUTHORISED == "U"
                           select new CompDetailsModel
                           {
                               CUSTOMER_NO = c.CUSTOMER_NO,
                               CERT_OF_INCORP_REG_NO = c.CERT_OF_INCORP_REG_NO,
                               OPERATING_BUSINESS_1 = c.OPERATING_BUSINESS_1,
                               BIZ_ADDRESS_REG_OFFICE_1 = c.BIZ_ADDRESS_REG_OFFICE_1,
                               EXPECTED_ANNUAL_TURNOVER = c.EXPECTED_ANNUAL_TURNOVER,
                               BRANCH_CODE = c.BRANCH_CODE,
                               LastUpdatedby = c.LAST_MODIFIED_BY,
                               LastUpdatedDate = c.LAST_MODIFIED_DATE,
                               LastAuthdby = c.AUTHORISED_BY,
                               LastAuthDate = c.AUTHORISED_DATE,
                               ExceptionId = querecord.EXCEPTION_ID
                           }).FirstOrDefault();

            var changelog = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_COMPANY_DETAILS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();

            if (changelog != null && compDetails != null)
            {
                string changeId = changelog.CHANGEID;
                var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId);
                foreach (var item in compDetails.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError("CompDetailsModel." + item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }

                }
            }
            #endregion
            #region CompInfo
            CompInfoModel compInfo = new CompInfoModel();
            compInfo = (from c in _db.CDMA_COMPANY_INFORMATION
                                               where c.CUSTOMER_NO == querecord.CUST_ID
                                                where c.AUTHORISED == "U"
                                               select new CompInfoModel
                                               {
                                                   CUSTOMER_NO = c.CUSTOMER_NO,
                                                   COMPANY_NAME = c.COMPANY_NAME,
                                                   REGISTERED_ADDRESS = c.REGISTERED_ADDRESS,
                                                   BRANCH_CODE = c.BRANCH_CODE,
                                                   EOC_RISK = c.EOC_RISK,
                                                   CURRENT_LINE_OF_BUSINESS = c.CURRENT_LINE_OF_BUSINESS,
                                                   COMPANY_NETWORTH_SOA = c.COMPANY_NETWORTH_SOA,
                                                   INTRODUCED_BY = c.INTRODUCED_BY,
                                                   BRF_INVESTIGATION_MEDIA_REPORT = c.BRF_INVESTIGATION_MEDIA_REPORT,
                                                   INVESTIGATION_MEDIA_REPORT = c.INVESTIGATION_MEDIA_REPORT,
                                                   ADD_VERIFICATION_STATUS = c.ADD_VERIFICATION_STATUS,
                                                   ANTICIPATED_TRANS_OUTFLOW = c.ANTICIPATED_TRANS_OUTFLOW,
                                                   ANTICIPATED_TRANS_INFLOW = c.ANTICIPATED_TRANS_INFLOW,
                                                   LENGTH_OF_STAY_INBUS = c.LENGTH_OF_STAY_INBUS,
                                                   SOURCE_OF_ASSET = c.SOURCE_OF_ASSET,
                                                   HISTORY_OF_CUSTOMER = c.HISTORY_OF_CUSTOMER,
                                                   LastUpdatedby = c.LAST_MODIFIED_BY,
                                                   LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                                   LastAuthdby = c.AUTHORISED_BY,
                                                   LastAuthDate = c.AUTHORISED_DATE,
                                                   ExceptionId = querecord.EXCEPTION_ID
                                               }).FirstOrDefault();

            var changelog2 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_COMPANY_INFORMATION" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();

            if (changelog2 != null && compInfo != null)
            {
                string changeId2 = changelog2.CHANGEID;
                var changedSet2 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId2);
                foreach (var item in compInfo.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet2)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError("CompInfoModel." + item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }

                }
            }
            #endregion
            #region BenOwners
            BenOwnersModel benOwners = new BenOwnersModel();
            benOwners = (from c in _db.CDMA_BENEFICIALOWNERS
                                      where c.ORGKEY == querecord.CUST_ID
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
            var changelog3 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_BENEFICIALOWNERS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();

            if (changelog3 != null && benOwners != null)
            {
                string changeId3 = changelog3.CHANGEID;
                var changedSet3 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId3);
                foreach (var item in benOwners.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet3)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError("BenOwnersModel." + item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }

                }
            }
            #endregion
            #region CorpAdd
            CorpADDModel corpADD = new CorpADDModel();
            corpADD = (from c in _db.CDMA_CORP_ADDITIONAL_DETAILS
                                        where c.CUSTOMER_NO == querecord.CUST_ID
                              where c.AUTHORISED == "U"
                                        select new CorpADDModel
                                        {
                                            CUSTOMER_NO = c.CUSTOMER_NO,
                                            BRANCH_CODE = c.BRANCH_CODE,
                                            COUNTERPARTIES_CLIENTS_OF_CUST = c.COUNTERPARTIES_CLIENTS_OF_CUST,
                                            LastUpdatedby = c.LAST_MODIFIED_BY,
                                            LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                            LastAuthdby = c.AUTHORISED_BY,
                                            LastAuthDate = c.AUTHORISED_DATE,
                                            ExceptionId = querecord.EXCEPTION_ID
                                        }).FirstOrDefault();
            var changelog4 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_CORP_ADDITIONAL_DETAILS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();

            if (changelog4 != null && corpADD != null)
            {
                string changeId4 = changelog4.CHANGEID;
                var changedSet4 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId4);
                foreach (var item in corpADD.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet4)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError("CorpADDModel." + item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }

                }
            }
            #endregion
            #region Guarantor
            GuarantorModel guarantor = new GuarantorModel();
            guarantor = (from c in _db.CDMA_GUARANTOR
                                   where c.CUSTOMER_NO == querecord.CUST_ID
                                 where c.AUTHORISED == "U"
                                   select new GuarantorModel
                                   {
                                       CUSTOMER_NO = c.CUSTOMER_NO,
                                       LNAME_OF_GUARANTOR = c.LNAME_OF_GUARANTOR,
                                       FNAME_OF_GUARANTOR = c.FNAME_OF_GUARANTOR,
                                       TIN_OF_GUARANTOR = c.TIN_OF_GUARANTOR,
                                       BVN_OF_GUARANTOR = c.BVN_OF_GUARANTOR,
                                       GURANTEED_AMOUNT = c.GURANTEED_AMOUNT,
                                       BRANCH_CODE = c.BRANCH_CODE,
                                       LastUpdatedby = c.LAST_MODIFIED_BY,
                                       LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                       LastAuthdby = c.AUTHORISED_BY,
                                       LastAuthDate = c.AUTHORISED_DATE,
                                       ExceptionId = querecord.EXCEPTION_ID
                                   }).FirstOrDefault();
            var changelog5 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_GUARANTOR" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();

            if (changelog5 != null && guarantor != null)
            {
                string changeId5 = changelog5.CHANGEID;
                var changedSet5 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId5);
                foreach (var item in guarantor.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet5)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError("GuarantorModel." + item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }

                }
            }
            #endregion

            model.CompDetailsModel = compDetails;
            model.CompInfoModel = compInfo;
            model.BenOwnersModel = benOwners;
            model.CorpADDModel = corpADD;
            model.GuarantorModel = guarantor;

            model.CompDetailsModel.ReadOnlyForm = "True";
            PrepareCompDetailsModel(model.CompDetailsModel);
            PrepareCompInfoModel(model.CompInfoModel);
            PrepareBenOwnersModel(model.BenOwnersModel);
            PrepareCorpAddModel(model.CorpADDModel);
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "DQQue");
            }            
            CorpCustModel model = new CorpCustModel();

            #region CompanyDetails
            int records = _db.CDMA_COMPANY_DETAILS.Count(o => o.CUSTOMER_NO == id);
            CompDetailsModel compDetails = new CompDetailsModel();
            if (records > 1)
            {
                compDetails = (from c in _db.CDMA_COMPANY_DETAILS
                               where c.CUSTOMER_NO == id
                               where c.AUTHORISED == "U"
                               select new CompDetailsModel
                               {
                                   CUSTOMER_NO = c.CUSTOMER_NO,
                                   CERT_OF_INCORP_REG_NO = c.CERT_OF_INCORP_REG_NO,
                                   OPERATING_BUSINESS_1 = c.OPERATING_BUSINESS_1,
                                   BIZ_ADDRESS_REG_OFFICE_1 = c.BIZ_ADDRESS_REG_OFFICE_1,
                                   EXPECTED_ANNUAL_TURNOVER = c.EXPECTED_ANNUAL_TURNOVER,
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
                                   OPERATING_BUSINESS_1 = c.OPERATING_BUSINESS_1,
                                   BIZ_ADDRESS_REG_OFFICE_1 = c.BIZ_ADDRESS_REG_OFFICE_1,
                                   EXPECTED_ANNUAL_TURNOVER = c.EXPECTED_ANNUAL_TURNOVER,
                                   BRANCH_CODE = c.BRANCH_CODE,
                               }).FirstOrDefault();
            }
            PrepareCompDetailsModel(compDetails);
            #endregion
            #region Get CompanyDetails Exception Columns
            var compdetailscustid = "";
            try
            {
                compdetailscustid = _db.MdmCorpRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_COMPANY_DETAILS" && a.CUST_ID == compDetails.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (compdetailscustid != "" && compDetails != null)
            {
                var exceptionSet = _db.MdmCorpRunExceptions.Where(a => a.CUST_ID == compdetailscustid); //.Select(a=>a.PROPERTYNAME);
                if (compDetails != null)
                {
                    foreach (var item in compDetails.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in exceptionSet)
                        {
                            if (item2.CATALOG_TAB_COL == item.Name)
                            {
                                ModelState.AddModelError("CompDetailsModel." + item.Name, string.Format("Attention!"));
                            }
                        }
                        //props.Add(item.Name);

                    }
                }
            }
            #endregion
            #region CompanyInfo
            int records2 = _db.CDMA_COMPANY_INFORMATION.Count(o => o.CUSTOMER_NO == id);
            CompInfoModel compInfo = new CompInfoModel();
            if (records2 > 1)
            {
                compInfo = (from c in _db.CDMA_COMPANY_INFORMATION
                                  where c.CUSTOMER_NO == id
                                  where c.AUTHORISED == "U"
                                  select new CompInfoModel
                                  {
                                      CUSTOMER_NO = c.CUSTOMER_NO,
                                      COMPANY_NAME = c.COMPANY_NAME,
                                      REGISTERED_ADDRESS = c.REGISTERED_ADDRESS,
                                      BRANCH_CODE = c.BRANCH_CODE,
                                      EOC_RISK = c.EOC_RISK,
                                      CURRENT_LINE_OF_BUSINESS = c.CURRENT_LINE_OF_BUSINESS,
                                      COMPANY_NETWORTH_SOA = c.COMPANY_NETWORTH_SOA,
                                      INTRODUCED_BY = c.INTRODUCED_BY,
                                      BRF_INVESTIGATION_MEDIA_REPORT = c.BRF_INVESTIGATION_MEDIA_REPORT,
                                      INVESTIGATION_MEDIA_REPORT = c.INVESTIGATION_MEDIA_REPORT,
                                      ADD_VERIFICATION_STATUS = c.ADD_VERIFICATION_STATUS,
                                      ANTICIPATED_TRANS_OUTFLOW = c.ANTICIPATED_TRANS_OUTFLOW,
                                      ANTICIPATED_TRANS_INFLOW = c.ANTICIPATED_TRANS_INFLOW,
                                      LENGTH_OF_STAY_INBUS = c.LENGTH_OF_STAY_INBUS,
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
                                      REGISTERED_ADDRESS = c.REGISTERED_ADDRESS,
                                      BRANCH_CODE = c.BRANCH_CODE,
                                      EOC_RISK = c.EOC_RISK,
                                      CURRENT_LINE_OF_BUSINESS = c.CURRENT_LINE_OF_BUSINESS,
                                      COMPANY_NETWORTH_SOA = c.COMPANY_NETWORTH_SOA,
                                      INTRODUCED_BY = c.INTRODUCED_BY,
                                      BRF_INVESTIGATION_MEDIA_REPORT = c.BRF_INVESTIGATION_MEDIA_REPORT,
                                      INVESTIGATION_MEDIA_REPORT = c.INVESTIGATION_MEDIA_REPORT,
                                      ADD_VERIFICATION_STATUS = c.ADD_VERIFICATION_STATUS,
                                      ANTICIPATED_TRANS_OUTFLOW = c.ANTICIPATED_TRANS_OUTFLOW,
                                      ANTICIPATED_TRANS_INFLOW = c.ANTICIPATED_TRANS_INFLOW,
                                      LENGTH_OF_STAY_INBUS = c.LENGTH_OF_STAY_INBUS,
                                      SOURCE_OF_ASSET = c.SOURCE_OF_ASSET,
                                      HISTORY_OF_CUSTOMER = c.HISTORY_OF_CUSTOMER,
                                  }).FirstOrDefault();
            }
            PrepareCompInfoModel(compInfo);
            #endregion
            #region Get CompanyInfo Exception Columns
            var compinfocustid = "";
            try
            {
                compinfocustid = _db.MdmCorpRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_COMPANY_INFORMATION" && a.CUST_ID == compInfo.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (compinfocustid != "" && compInfo != null)
            {
                var exceptionSet = _db.MdmCorpRunExceptions.Where(a => a.CUST_ID == compinfocustid); //.Select(a=>a.PROPERTYNAME);
                if (compInfo != null)
                {
                    foreach (var item in compInfo.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in exceptionSet)
                        {
                            if (item2.CATALOG_TAB_COL == item.Name)
                            {
                                ModelState.AddModelError("CompInfoModel." + item.Name, string.Format("Attention!"));
                            }
                        }
                        //props.Add(item.Name);

                    }
                }
            }
            #endregion
            #region BenOwners
            int records3 = _db.CDMA_BENEFICIALOWNERS.Count(o => o.ORGKEY == id);
            BenOwnersModel benOwners = new BenOwnersModel();
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
            PrepareBenOwnersModel(benOwners);
            #endregion
            #region Get Ben Owners Exception Columns
            var bencustid = "";
            try
            {
                bencustid = _db.MdmCorpRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_BENEFICIALOWNERS" && a.CUST_ID == benOwners.ORGKEY).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (bencustid != "" && benOwners != null)
            {
                var exceptionSet = _db.MdmCorpRunExceptions.Where(a => a.CUST_ID == bencustid); //.Select(a=>a.PROPERTYNAME);
                if (bencustid != null)
                {
                    foreach (var item in benOwners.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in exceptionSet)
                        {
                            if (item2.CATALOG_TAB_COL == item.Name)
                            {
                                ModelState.AddModelError("BenOwnersModel." + item.Name, string.Format("Attention!"));
                            }
                        }
                        //props.Add(item.Name);

                    }
                }
            }
            #endregion
            #region CorpAdd
            int records4 = _db.CDMA_CORP_ADDITIONAL_DETAILS.Count(o => o.CUSTOMER_NO == id);
            CorpADDModel corpADD = new CorpADDModel();
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
                             }).FirstOrDefault();
            }
            PrepareCorpAddModel(corpADD);
            #endregion
            #region Get Corp Add Exception Columns
            var corpaddcustid = "";
            try
            {
                corpaddcustid = _db.MdmCorpRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_CORP_ADDITIONAL_DETAILS" && a.CUST_ID == corpADD.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (corpaddcustid != "" && corpADD != null)
            {
                var exceptionSet = _db.MdmCorpRunExceptions.Where(a => a.CUST_ID == corpaddcustid); //.Select(a=>a.PROPERTYNAME);
                if (corpaddcustid != null)
                {
                    foreach (var item in corpADD.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in exceptionSet)
                        {
                            if (item2.CATALOG_TAB_COL == item.Name)
                            {
                                ModelState.AddModelError("CorpADDModel." + item.Name, string.Format("Attention!"));
                            }
                        }
                        //props.Add(item.Name);

                    }
                }
            }
            #endregion
            #region Guarantor
            int record5 = _db.CDMA_GUARANTOR.Count(o => o.CUSTOMER_NO == id);
            GuarantorModel guarantorModel = new GuarantorModel();
            if (record5 > 1)
            {
                guarantorModel = (from c in _db.CDMA_GUARANTOR
                           where c.CUSTOMER_NO == id
                           where c.AUTHORISED == "U"
                           select new GuarantorModel
                           {
                               CUSTOMER_NO = c.CUSTOMER_NO,
                               LNAME_OF_GUARANTOR = c.LNAME_OF_GUARANTOR,
                               FNAME_OF_GUARANTOR = c.FNAME_OF_GUARANTOR,
                               TIN_OF_GUARANTOR = c.TIN_OF_GUARANTOR,
                               BVN_OF_GUARANTOR = c.BVN_OF_GUARANTOR,
                               GURANTEED_AMOUNT = c.GURANTEED_AMOUNT,
                               BRANCH_CODE = c.BRANCH_CODE,
                           }).FirstOrDefault();
            }
            else if (record5 == 1)
            {
                guarantorModel = (from c in _db.CDMA_GUARANTOR
                                  where c.CUSTOMER_NO == id
                           where c.AUTHORISED == "A"
                           select new GuarantorModel
                           {
                               CUSTOMER_NO = c.CUSTOMER_NO,
                               LNAME_OF_GUARANTOR = c.LNAME_OF_GUARANTOR,
                               FNAME_OF_GUARANTOR = c.FNAME_OF_GUARANTOR,
                               TIN_OF_GUARANTOR = c.TIN_OF_GUARANTOR,
                               BVN_OF_GUARANTOR = c.BVN_OF_GUARANTOR,
                               GURANTEED_AMOUNT = c.GURANTEED_AMOUNT,
                               BRANCH_CODE = c.BRANCH_CODE,
                           }).FirstOrDefault();
            }
            PrepareGuarantorModel(guarantorModel);
            #endregion
            #region Get Guarantor Exception Columns
            var guarantorcustid = "";
            try
            {
                guarantorcustid = _db.MdmCorpRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_GUARANTOR" && a.CUST_ID == guarantorModel.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (guarantorcustid != "" && guarantorModel != null)
            {
                var exceptionSet = _db.MdmCorpRunExceptions.Where(a => a.CUST_ID == guarantorcustid); //.Select(a=>a.PROPERTYNAME);
                if (guarantorcustid != null)
                {
                    foreach (var item in guarantorModel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in exceptionSet)
                        {
                            if (item2.CATALOG_TAB_COL == item.Name)
                            {
                                ModelState.AddModelError("GuarantorModel." + item.Name, string.Format("Attention!"));
                            }
                        }
                        //props.Add(item.Name);

                    }
                }
            }
            #endregion

            model.CompDetailsModel = compDetails;
            model.CompInfoModel = compInfo;
            model.BenOwnersModel = benOwners;
            model.CorpADDModel = corpADD;
            model.GuarantorModel = guarantorModel;

            if (model == null)
            {
                return HttpNotFound();
            }
            model.CompDetailsModel.ReadOnlyForm = "True";
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
                CDMA_GUARANTOR originalObject5 = new CDMA_GUARANTOR();

                using (var db = new AppDbContext())
                {
                    #region CompanyDetails
                    int records = db.CDMA_COMPANY_DETAILS.Count(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO);
                    if (records > 1)
                    {
                        updateFlag = true;
                        originalObject = _db.CDMA_COMPANY_DETAILS.Where(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();
                        var entity = db.CDMA_COMPANY_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity != null)
                        {                            
                            entity.BIZ_ADDRESS_REG_OFFICE_1 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_1;
                            entity.CERT_OF_INCORP_REG_NO = corpCustModel.CompDetailsModel.CERT_OF_INCORP_REG_NO;
                            entity.EXPECTED_ANNUAL_TURNOVER = corpCustModel.CompDetailsModel.EXPECTED_ANNUAL_TURNOVER;
                            entity.OPERATING_BUSINESS_1 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_1;
                            entity.BRANCH_CODE = corpCustModel.CompDetailsModel.BRANCH_CODE;
                            entity.QUEUE_STATUS = 1;

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
                            entity.BIZ_ADDRESS_REG_OFFICE_1 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_1;
                            entity.CERT_OF_INCORP_REG_NO = corpCustModel.CompDetailsModel.CERT_OF_INCORP_REG_NO;
                            entity.EXPECTED_ANNUAL_TURNOVER = corpCustModel.CompDetailsModel.EXPECTED_ANNUAL_TURNOVER;
                            entity.OPERATING_BUSINESS_1 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_1;
                            entity.BRANCH_CODE = corpCustModel.CompDetailsModel.BRANCH_CODE;
                            entity.QUEUE_STATUS = 1;
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
                            newentity.BIZ_ADDRESS_REG_OFFICE_1 = corpCustModel.CompDetailsModel.BIZ_ADDRESS_REG_OFFICE_1;
                            newentity.CERT_OF_INCORP_REG_NO = corpCustModel.CompDetailsModel.CERT_OF_INCORP_REG_NO;
                            newentity.EXPECTED_ANNUAL_TURNOVER = corpCustModel.CompDetailsModel.EXPECTED_ANNUAL_TURNOVER;
                            newentity.OPERATING_BUSINESS_1 = corpCustModel.CompDetailsModel.OPERATING_BUSINESS_1;
                            newentity.BRANCH_CODE = corpCustModel.CompDetailsModel.BRANCH_CODE;
                            newentity.QUEUE_STATUS = 1;
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
                    #endregion
                    #region CompanyInfo
                    int records2 = db.CDMA_COMPANY_INFORMATION.Count(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO);
                    if (records2 > 1)
                    {
                        updateFlag = true;
                        originalObject2 = _db.CDMA_COMPANY_INFORMATION.Where(o => o.CUSTOMER_NO == corpCustModel.CompInfoModel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();
                        var entity2 = db.CDMA_COMPANY_INFORMATION.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.CompInfoModel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity2 != null)
                        {
                            entity2.COMPANY_NAME = corpCustModel.CompInfoModel.COMPANY_NAME;
                            entity2.REGISTERED_ADDRESS = corpCustModel.CompInfoModel.REGISTERED_ADDRESS;
                            entity2.BRANCH_CODE = corpCustModel.CompInfoModel.BRANCH_CODE;
                            entity2.EOC_RISK = corpCustModel.CompInfoModel.EOC_RISK;
                            entity2.CURRENT_LINE_OF_BUSINESS = corpCustModel.CompInfoModel.CURRENT_LINE_OF_BUSINESS;
                            entity2.COMPANY_NETWORTH_SOA = corpCustModel.CompInfoModel.COMPANY_NETWORTH_SOA;
                            entity2.INTRODUCED_BY = corpCustModel.CompInfoModel.INTRODUCED_BY;
                            entity2.BRF_INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.BRF_INVESTIGATION_MEDIA_REPORT;
                            entity2.INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.INVESTIGATION_MEDIA_REPORT;
                            entity2.ADD_VERIFICATION_STATUS = corpCustModel.CompInfoModel.ADD_VERIFICATION_STATUS;
                            entity2.ANTICIPATED_TRANS_OUTFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_OUTFLOW;
                            entity2.ANTICIPATED_TRANS_INFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_INFLOW;
                            entity2.LENGTH_OF_STAY_INBUS = corpCustModel.CompInfoModel.LENGTH_OF_STAY_INBUS;
                            entity2.SOURCE_OF_ASSET = corpCustModel.CompInfoModel.SOURCE_OF_ASSET;
                            entity2.BRANCH_CODE = corpCustModel.CompInfoModel.BRANCH_CODE;
                            entity2.QUEUE_STATUS = 1;
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
                            entity2.REGISTERED_ADDRESS = corpCustModel.CompInfoModel.REGISTERED_ADDRESS;
                            entity2.BRANCH_CODE = corpCustModel.CompInfoModel.BRANCH_CODE;
                            entity2.EOC_RISK = corpCustModel.CompInfoModel.EOC_RISK;
                            entity2.CURRENT_LINE_OF_BUSINESS = corpCustModel.CompInfoModel.CURRENT_LINE_OF_BUSINESS;
                            entity2.COMPANY_NETWORTH_SOA = corpCustModel.CompInfoModel.COMPANY_NETWORTH_SOA;
                            entity2.INTRODUCED_BY = corpCustModel.CompInfoModel.INTRODUCED_BY;
                            entity2.BRF_INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.BRF_INVESTIGATION_MEDIA_REPORT;
                            entity2.INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.INVESTIGATION_MEDIA_REPORT;
                            entity2.ADD_VERIFICATION_STATUS = corpCustModel.CompInfoModel.ADD_VERIFICATION_STATUS;
                            entity2.ANTICIPATED_TRANS_OUTFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_OUTFLOW;
                            entity2.ANTICIPATED_TRANS_INFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_INFLOW;
                            entity2.LENGTH_OF_STAY_INBUS = corpCustModel.CompInfoModel.LENGTH_OF_STAY_INBUS;
                            entity2.SOURCE_OF_ASSET = corpCustModel.CompInfoModel.SOURCE_OF_ASSET;
                            entity2.HISTORY_OF_CUSTOMER = corpCustModel.CompInfoModel.HISTORY_OF_CUSTOMER;
                            entity2.BRANCH_CODE = corpCustModel.CompInfoModel.BRANCH_CODE;
                            entity2.QUEUE_STATUS = 1;
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
                            newentity.BRANCH_CODE = corpCustModel.CompInfoModel.BRANCH_CODE;
                            newentity.EOC_RISK = corpCustModel.CompInfoModel.EOC_RISK;
                            newentity.CURRENT_LINE_OF_BUSINESS = corpCustModel.CompInfoModel.CURRENT_LINE_OF_BUSINESS;
                            newentity.COMPANY_NETWORTH_SOA = corpCustModel.CompInfoModel.COMPANY_NETWORTH_SOA;
                            newentity.INTRODUCED_BY = corpCustModel.CompInfoModel.INTRODUCED_BY;
                            newentity.BRF_INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.BRF_INVESTIGATION_MEDIA_REPORT;
                            newentity.INVESTIGATION_MEDIA_REPORT = corpCustModel.CompInfoModel.INVESTIGATION_MEDIA_REPORT;
                            newentity.ADD_VERIFICATION_STATUS = corpCustModel.CompInfoModel.ADD_VERIFICATION_STATUS;
                            newentity.ANTICIPATED_TRANS_OUTFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_OUTFLOW;
                            newentity.ANTICIPATED_TRANS_INFLOW = corpCustModel.CompInfoModel.ANTICIPATED_TRANS_INFLOW;
                            newentity.LENGTH_OF_STAY_INBUS = corpCustModel.CompInfoModel.LENGTH_OF_STAY_INBUS;
                            newentity.SOURCE_OF_ASSET = corpCustModel.CompInfoModel.SOURCE_OF_ASSET;
                            newentity.HISTORY_OF_CUSTOMER = corpCustModel.CompInfoModel.HISTORY_OF_CUSTOMER;
                            newentity.BRANCH_CODE = corpCustModel.CompInfoModel.BRANCH_CODE;
                            newentity.QUEUE_STATUS = 1;
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
                    #endregion
                    #region BenOwners
                    int records3 = db.CDMA_BENEFICIALOWNERS.Count(o => o.ORGKEY == corpCustModel.CompDetailsModel.CUSTOMER_NO);
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
                            entity3.QUEUE_STATUS = 1;
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
                            entity3.QUEUE_STATUS = 1;
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
                            newentity.QUEUE_STATUS = 1;
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
                    #endregion
                    #region AddInfo
                    int records4 = db.CDMA_CORP_ADDITIONAL_DETAILS.Count(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO);
                    if (records4 > 1)
                    {
                        updateFlag = true;
                        originalObject4 = _db.CDMA_CORP_ADDITIONAL_DETAILS.Where(o => o.CUSTOMER_NO == corpCustModel.CorpADDModel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();
                        var entity4 = db.CDMA_CORP_ADDITIONAL_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.CorpADDModel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity4 != null)
                        {
                            entity4.BRANCH_CODE = corpCustModel.CorpADDModel.BRANCH_CODE;
                            entity4.COUNTERPARTIES_CLIENTS_OF_CUST = corpCustModel.CorpADDModel.COUNTERPARTIES_CLIENTS_OF_CUST;
                            entity4.BRANCH_CODE = corpCustModel.CorpADDModel.BRANCH_CODE;
                            entity4.QUEUE_STATUS = 1;
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
                            entity4.BRANCH_CODE = corpCustModel.CorpADDModel.BRANCH_CODE;
                            entity4.QUEUE_STATUS = 1;
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
                            newentity.BRANCH_CODE = corpCustModel.CorpADDModel.BRANCH_CODE;
                            newentity.QUEUE_STATUS = 1;
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
                    #endregion
                    #region Guarantor
                    int records5 = db.CDMA_GUARANTOR.Count(o => o.CUSTOMER_NO == corpCustModel.CompDetailsModel.CUSTOMER_NO);
                    if (records5 > 1)
                    {
                        updateFlag = true;
                        originalObject5 = _db.CDMA_GUARANTOR.Where(o => o.CUSTOMER_NO == corpCustModel.GuarantorModel.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();
                        var entity5 = db.CDMA_GUARANTOR.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.GuarantorModel.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity5 != null)
                        {
                            entity5.LNAME_OF_GUARANTOR = corpCustModel.GuarantorModel.LNAME_OF_GUARANTOR;
                            entity5.FNAME_OF_GUARANTOR = corpCustModel.GuarantorModel.FNAME_OF_GUARANTOR;
                            entity5.TIN_OF_GUARANTOR = corpCustModel.GuarantorModel.TIN_OF_GUARANTOR;
                            entity5.BVN_OF_GUARANTOR = corpCustModel.GuarantorModel.BVN_OF_GUARANTOR;
                            entity5.GURANTEED_AMOUNT = corpCustModel.GuarantorModel.GURANTEED_AMOUNT;
                            entity5.BRANCH_CODE = corpCustModel.GuarantorModel.BRANCH_CODE;
                            entity5.QUEUE_STATUS = 1;
                            entity5.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity5.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";

                            db.CDMA_GUARANTOR.Attach(entity5);
                            db.Entry(entity5).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), corpCustModel.GuarantorModel.CUSTOMER_NO, updateFlag, originalObject5);
                            _messageService.LogEmailJob(identity.ProfileId, entity5.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                    }
                    else if (records5 == 1)
                    {
                        updateFlag = false;
                        var entity5 = db.CDMA_GUARANTOR.FirstOrDefault(o => o.CUSTOMER_NO == corpCustModel.GuarantorModel.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject5 = _db.CDMA_GUARANTOR.Where(o => o.CUSTOMER_NO == corpCustModel.GuarantorModel.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();

                        if (originalObject5 != null)
                        {
                            entity5.LNAME_OF_GUARANTOR = corpCustModel.GuarantorModel.LNAME_OF_GUARANTOR;
                            entity5.FNAME_OF_GUARANTOR = corpCustModel.GuarantorModel.FNAME_OF_GUARANTOR;
                            entity5.TIN_OF_GUARANTOR = corpCustModel.GuarantorModel.TIN_OF_GUARANTOR;
                            entity5.BVN_OF_GUARANTOR = corpCustModel.GuarantorModel.BVN_OF_GUARANTOR;
                            entity5.GURANTEED_AMOUNT = corpCustModel.GuarantorModel.GURANTEED_AMOUNT;
                            entity5.BRANCH_CODE = corpCustModel.GuarantorModel.BRANCH_CODE;
                            entity5.QUEUE_STATUS = 1;
                            entity5.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity5.LAST_MODIFIED_DATE = DateTime.Now;
                            //entity.AUTHORISED = "U";
                            //

                            db.CDMA_GUARANTOR.Attach(entity5);
                            db.Entry(entity5).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), corpCustModel.GuarantorModel.CUSTOMER_NO, updateFlag, originalObject5);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_GUARANTOR();
                            newentity.LNAME_OF_GUARANTOR = corpCustModel.GuarantorModel.LNAME_OF_GUARANTOR;
                            newentity.FNAME_OF_GUARANTOR = corpCustModel.GuarantorModel.FNAME_OF_GUARANTOR;
                            newentity.TIN_OF_GUARANTOR = corpCustModel.GuarantorModel.TIN_OF_GUARANTOR;
                            newentity.BVN_OF_GUARANTOR = corpCustModel.GuarantorModel.BVN_OF_GUARANTOR;
                            newentity.GURANTEED_AMOUNT = corpCustModel.GuarantorModel.GURANTEED_AMOUNT;
                            newentity.BRANCH_CODE = corpCustModel.GuarantorModel.BRANCH_CODE;
                            newentity.QUEUE_STATUS = 1;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = corpCustModel.GuarantorModel.CUSTOMER_NO;
                            db.CDMA_GUARANTOR.Add(newentity);

                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", corpCustModel.GuarantorModel.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                    #endregion
                }

                SuccessNotification("Corporate Customer Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = corpCustModel.CompDetailsModel.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
                //return RedirectToAction("Index");
            }

            PrepareCompDetailsModel(corpCustModel.CompDetailsModel);
            PrepareCompInfoModel(corpCustModel.CompInfoModel);
            PrepareBenOwnersModel(corpCustModel.BenOwnersModel);
            PrepareCorpAddModel(corpCustModel.CorpADDModel);
            PrepareGuarantorModel(corpCustModel.GuarantorModel);
            //PrepareModel(actxmodel);
            return View(corpCustModel);
        }

        [NonAction]
        public virtual void PrepareCompDetailsModel(CompDetailsModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if(model != null)
            {
                model.IsStockExchange.Add(new SelectListItem { Text = "Yes", Value = "Y" });
                model.IsStockExchange.Add(new SelectListItem { Text = "No", Value = "N" });
                model.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
                model.BusinessSegments = new SelectList(_db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT").ToList();
                model.Countries = new SelectList(_db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
            }            
        }

        [NonAction]
        public virtual void PrepareCompInfoModel(CompInfoModel model)
        {
            //if (model == null)
            //    throw new ArgumentNullException("model");
            if (model != null)
            {
                model.BusinessSegments = new SelectList(_db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT").ToList();
                model.CustomerTypes = new SelectList(_db.CDMA_CUSTOMER_TYPE, "TYPE_ID", "CUSTOMER_TYPE").ToList();
                model.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
            }
        }

        [NonAction]
        public virtual void PrepareBenOwnersModel(BenOwnersModel model)
        {
            if (model != null)
            {
                model.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
            }
        }

        [NonAction]
        public virtual void PrepareCorpAddModel(CorpADDModel model)
        {
            if (model != null)
            {
                model.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
            }
        }

        [NonAction]
        public virtual void PrepareGuarantorModel(GuarantorModel model)
        {
            if (model != null)
            {
                model.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
            }
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
                    _dqQueService.DisApproveExceptionCorp(exceptionId.ToString(), corpCustModel.CompDetailsModel.AuthoriserRemarks);
                    SuccessNotification("Corporate Customer Not Authorised");
                    _messageService.LogEmailJob(identity.ProfileId, corpCustModel.CompDetailsModel.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(corpCustModel.CompDetailsModel.LastUpdatedby));
                }

                else
                {
                    _dqQueService.ApproveExceptionCorp(exceptionId.ToString(), identity.ProfileId);
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
                _dqQueService.DisApproveExceptionCorp(corpCustModel.CompDetailsModel.ExceptionId.ToString(), corpCustModel.CompDetailsModel.AuthoriserRemarks);

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

    }
}