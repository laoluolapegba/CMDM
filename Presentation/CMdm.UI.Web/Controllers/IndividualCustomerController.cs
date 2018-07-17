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
using System.Reflection;
using CMdm.Services.DqQue;
using CMdm.Entities.ViewModels; 
using CMdm.Data.Rbac;
using CMdm.Services.Messaging;

namespace CMdm.UI.Web.Controllers 
{
    public class IndividualCustomerController : BaseController
    {
        #region Constructors
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        private IMessagingService _messageService;

        public IndividualCustomerController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new DqQueService();
            _messageService = new MessagingService();
        }
        #endregion Constructors

        public ActionResult Authorize(string id)
        {
            
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            var querecord = _dqQueService.GetQueDetailItembyId(Convert.ToInt32(id));
            if (querecord == null)
            {
                return RedirectToAction("AuthList", "DQQue");
            }

            #region Biodata
                IndividualBioDataModel indvdmodel = new IndividualBioDataModel();
                indvdmodel  = (from c in _db.CDMA_INDIVIDUAL_BIO_DATA
                         where c.CUSTOMER_NO == querecord.CUST_ID
                              where c.AUTHORISED == "U"
                              select new IndividualBioDataModel
                              {
                                  CUSTOMER_NO = c.CUSTOMER_NO,
                                  TITLE = c.TITLE,
                                  SURNAME = c.SURNAME,
                                  FIRST_NAME = c.FIRST_NAME,
                                  OTHER_NAME = c.OTHER_NAME,
                                  DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                                  SEX = c.SEX,
                                  MARITAL_STATUS = c.MARITAL_STATUS,
                                  LGAOFORIGIN = c.LGAOFORIGIN,
                                  MOTHER_MAIDEN_NAME = c.MOTHER_MAIDEN_NAME,
                                  COUNTRY_OF_BIRTH = c.COUNTRY_OF_BIRTH,
                                  RESIDENCE_COUNTRY = c.RESIDENCE_COUNTRY,
                                  NATIONALITY = c.NATIONALITY,
                                  RELIGION = c.RELIGION,
                                  STATE_OF_ORIGIN = c.STATE_OF_ORIGIN,
                                  TIN = c.TIN,
                                  RESIDENCEPERMIT_ISSDATE = c.RESIDENCEPERMIT_ISSDATE,
                                  RESIDENCEPERMIT_EXPDATE = c.RESIDENCEPERMIT_EXPDATE,
                                  PURPOSEOFACCOUNT = c.PURPOSEOFACCOUNT,
                                  RESIDENCEPERMITNO = c.RESIDENCEPERMITNO,
                                  SECONDNATIONALITY = c.SECONDNATIONALITY,
                                  BRANCH_CODE = c.BRANCH_CODE,
                                  BVN = c.BVN,
                                  TIER = c.TIER,
                                  LastUpdatedby = c.LAST_MODIFIED_BY,
                                  LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                  LastAuthdby = c.AUTHORISED_BY,
                                  LastAuthDate = c.AUTHORISED_DATE,
                                  ExceptionId = querecord.EXCEPTION_ID
                              }).FirstOrDefault();
            var changelog = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_BIO_DATA" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();
            
            if (changelog != null && indvdmodel != null)
            {
                string changeId = changelog.CHANGEID;
                var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId);
                foreach (var item in indvdmodel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError("Biodata." + item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }

                }
            }
            #endregion

            #region Identification
            IndividualIDDetail idmodel = new IndividualIDDetail();
            var iddata = (from c in _db.CDMA_INDIVIDUAL_IDENTIFICATION
                                where c.CUSTOMER_NO == querecord.CUST_ID
                                where c.AUTHORISED == "U"
                                select new IndividualIDDetail
                                {
                                    CUSTOMER_NO = c.CUSTOMER_NO,
                                    IDENTIFICATION_TYPE = c.IDENTIFICATION_TYPE,
                                    ID_NO = c.ID_NO,
                                    ID_EXPIRY_DATE = c.ID_EXPIRY_DATE,
                                    ID_ISSUE_DATE = c.ID_ISSUE_DATE,
                                    BRANCH_CODE = c.BRANCH_CODE,
                                    TIER = c.TIER,
                                }).FirstOrDefault();
            if (iddata != null) idmodel = iddata;
            var changelog3 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_IDENTIFICATION" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();
            
            if (changelog3 != null && idmodel != null)
            {
                string changeId3 = changelog3.CHANGEID;
                var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId3);
                foreach (var item in idmodel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError("identification." + item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }

                }
            }
            #endregion

            #region Contact
            IndividualContactDetails contactmodel = new IndividualContactDetails();

            var contactdata = (from c in _db.CDMA_INDIVIDUAL_CONTACT_DETAIL
                               where c.CUSTOMER_NO == querecord.CUST_ID
                               where c.AUTHORISED == "U"
                               select new IndividualContactDetails
                               {
                                   CUSTOMER_NO = c.CUSTOMER_NO,
                                   MOBILE_NO = c.MOBILE_NO,
                                   EMAIL_ADDRESS = c.EMAIL_ADDRESS,
                                   MAILING_ADDRESS = c.MAILING_ADDRESS,
                                   ALTERNATEPHONENO = c.ALTERNATEPHONENO,
                                   RESIDENCE_LGA = c.RESIDENCE_LGA,
                                   NEAREST_BUSTOP_LANDMARK = c.NEAREST_BUSTOP_LANDMARK,
                                   BRANCH_CODE = c.BRANCH_CODE,
                                   STATE = c.STATE,
                                   CITY = c.CITY,
                                   ADDRESS_NUMBER = c.ADDRESS_NUMBER,
                                   MAILING_ADDRESS_STREET = c.MAILING_ADDRESS_STREET,
                                   TIER = c.TIER,
                               }).FirstOrDefault();

            if (contactdata != null) contactmodel = contactdata;

            var changelog5 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_CONTACT_DETAIL" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();
            if (changelog5 != null && contactmodel != null)
            {
                string changeId5 = changelog5.CHANGEID;
                var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId5);
                foreach (var item in contactmodel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError("contact." + item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }

                }
            }
            #endregion
            
            #region Not Used Address
            //IndividualAddressDetails addressmodel  = new IndividualAddressDetails();
            //var adddata = (from c in _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL
            //                    where c.CUSTOMER_NO == querecord.CUST_ID
            //                    where c.AUTHORISED == "U"
            //                    select new IndividualAddressDetails
            //                    {
            //                        CUSTOMER_NO = c.CUSTOMER_NO,
            //                        CITY_TOWN_OF_RESIDENCE = c.CITY_TOWN_OF_RESIDENCE,
            //                        COUNTRY_OF_RESIDENCE = c.COUNTRY_OF_RESIDENCE,
            //                        LGA_OF_RESIDENCE = c.LGA_OF_RESIDENCE,
            //                        NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
            //                        RESIDENCE_OWNED_OR_RENT = c.RESIDENCE_OWNED_OR_RENT,
            //                        RESIDENTIAL_ADDRESS = c.RESIDENTIAL_ADDRESS,
            //                        STATE_OF_RESIDENCE = c.STATE_OF_RESIDENCE,
            //                        ZIP_POSTAL_CODE = c.ZIP_POSTAL_CODE
            //                    }).FirstOrDefault();
            //if (adddata != null) addressmodel = adddata;
            //var changelog2 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_ADDRESS_DETAIL" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();
            //if(changelog2 != null && addressmodel != null)
            //{
            //    string changeId2 = changelog2.CHANGEID;
            //    var changedSet2 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId2);
            //    foreach (var item in addressmodel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
            //    {
            //        foreach (var item2 in changedSet2)
            //        {
            //            if (item2.PROPERTYNAME == item.Name)
            //            {
            //                ModelState.AddModelError("AddressDetails." + item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
            //            }
            //        }

            //    }
            //}
            #endregion

            #region Not Used Others
            //OtherDetails otherdetailsmodel = new OtherDetails();

            //var otherdetdata = (from c in _db.CDMA_INDIVIDUAL_OTHER_DETAILS
            //               where c.CUSTOMER_NO == querecord.CUST_ID
            //               where c.AUTHORISED == "U"
            //               select new OtherDetails
            //               {
            //                   CUSTOMER_NO = c.CUSTOMER_NO,
            //                   TIN_NO = c.TIN_NO,
            //               }).FirstOrDefault();
            //if (otherdetdata != null) otherdetailsmodel = otherdetdata;
            //var changelog4 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_OTHER_DETAILS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault();

            //if (changelog4 != null && otherdetailsmodel != null)
            //{
            //    string changeId3 = changelog4.CHANGEID;
            //    var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId3);
            //    foreach (var item in otherdetailsmodel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
            //    {
            //        foreach (var item2 in changedSet)
            //        {
            //            if (item2.PROPERTYNAME == item.Name)
            //            {
            //                ModelState.AddModelError("otherdetails. " + item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
            //            }
            //        }

            //    }
            //}
            #endregion

            var model = new BiodataInfoViewModel();

            model.BioData = indvdmodel;
            model.identification = idmodel;
            model.contact = contactmodel;
            //model.otherdetails = otherdetailsmodel;
            //model.AddressDetails = addressmodel;
                        
            model.BioData.ReadOnlyForm = "True";
            IndBioDataPrepareModel(indvdmodel);
            IndContactPrepareModel(contactmodel);
            IndIdDataPrepareModel(idmodel);

            return View(model);
        }

        // GET: IndividualCustomer/Create
        public ActionResult Edit(string id)
        {
            var viewModel = new BiodataInfoViewModel();
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "DQQue");
            }

            #region Biodata
            int invrecords = _db.CDMA_INDIVIDUAL_BIO_DATA.Count(o => o.CUSTOMER_NO == id);
            IndividualBioDataModel indvmodel = new IndividualBioDataModel();
            if (invrecords > 1)
            {
                indvmodel = (from c in _db.CDMA_INDIVIDUAL_BIO_DATA

                             where c.CUSTOMER_NO == id
                             where c.AUTHORISED == "U"
                             select new IndividualBioDataModel
                             {
                                 CUSTOMER_NO = c.CUSTOMER_NO,
                                 TITLE = c.TITLE,
                                 SURNAME = c.SURNAME,
                                 FIRST_NAME = c.FIRST_NAME,
                                 OTHER_NAME = c.OTHER_NAME,
                                 DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                                 SEX = c.SEX,
                                 MARITAL_STATUS = c.MARITAL_STATUS,
                                 LGAOFORIGIN = c.LGAOFORIGIN,
                                 MOTHER_MAIDEN_NAME = c.MOTHER_MAIDEN_NAME,
                                 COUNTRY_OF_BIRTH = c.COUNTRY_OF_BIRTH,
                                 RESIDENCE_COUNTRY = c.RESIDENCE_COUNTRY,
                                 NATIONALITY = c.NATIONALITY,
                                 RELIGION = c.RELIGION,
                                 STATE_OF_ORIGIN = c.STATE_OF_ORIGIN,
                                 TIN = c.TIN,
                                 RESIDENCEPERMIT_ISSDATE = c.RESIDENCEPERMIT_ISSDATE,
                                 RESIDENCEPERMIT_EXPDATE = c.RESIDENCEPERMIT_EXPDATE,
                                 PURPOSEOFACCOUNT = c.PURPOSEOFACCOUNT,
                                 RESIDENCEPERMITNO = c.RESIDENCEPERMITNO,
                                 SECONDNATIONALITY = c.SECONDNATIONALITY,
                                 BRANCH_CODE = c.BRANCH_CODE,
                                 BVN = c.BVN,
                                 TIER = c.TIER
                             }).FirstOrDefault();
            }
            else if (invrecords == 1)
            {
                indvmodel = (from c in _db.CDMA_INDIVIDUAL_BIO_DATA

                             where c.CUSTOMER_NO == id
                             where c.AUTHORISED == "A"
                             select new IndividualBioDataModel
                             {
                                 CUSTOMER_NO = c.CUSTOMER_NO,
                                 TITLE = c.TITLE,
                                 SURNAME = c.SURNAME,
                                 FIRST_NAME = c.FIRST_NAME,
                                 OTHER_NAME = c.OTHER_NAME,
                                 DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                                 SEX = c.SEX,
                                 MARITAL_STATUS = c.MARITAL_STATUS,
                                 LGAOFORIGIN = c.LGAOFORIGIN,
                                 MOTHER_MAIDEN_NAME = c.MOTHER_MAIDEN_NAME,
                                 COUNTRY_OF_BIRTH = c.COUNTRY_OF_BIRTH,
                                 RESIDENCE_COUNTRY = c.RESIDENCE_COUNTRY,
                                 NATIONALITY = c.NATIONALITY,
                                 RELIGION = c.RELIGION,
                                 STATE_OF_ORIGIN = c.STATE_OF_ORIGIN,
                                 TIN = c.TIN,
                                 RESIDENCEPERMIT_ISSDATE = c.RESIDENCEPERMIT_ISSDATE,
                                 RESIDENCEPERMIT_EXPDATE = c.RESIDENCEPERMIT_EXPDATE,
                                 PURPOSEOFACCOUNT = c.PURPOSEOFACCOUNT,
                                 RESIDENCEPERMITNO = c.RESIDENCEPERMITNO,
                                 SECONDNATIONALITY = c.SECONDNATIONALITY,
                                 BRANCH_CODE = c.BRANCH_CODE,
                                 BVN = c.BVN,
                                 TIER = c.TIER
                             }).FirstOrDefault();
            }
            IndBioDataPrepareModel(indvmodel);
            #endregion

            #region Get Biodata Exception Columns
            var biocustid = "";
            try
            {
                biocustid = _db.MdmDqRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_INDIVIDUAL_BIO_DATA" && a.CUST_ID == indvmodel.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (biocustid != "" && indvmodel != null)
            {
                var exceptionSet = _db.MdmDqRunExceptions.Where(a => a.CUST_ID == biocustid); //.Select(a=>a.PROPERTYNAME);
                if (indvmodel != null)
                {
                    foreach (var item in indvmodel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in exceptionSet)
                        {
                            if (item2.CATALOG_TAB_COL == item.Name)
                            {
                                ModelState.AddModelError("Biodata." + item.Name, string.Format("Attention!"));
                            }
                        }
                        //props.Add(item.Name);

                    }
                }
            }
            #endregion

            #region ContactDetails
            int contactrecords = _db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Count(o => o.CUSTOMER_NO == id);
            IndividualContactDetails contactmodel = new IndividualContactDetails();
            if (contactrecords > 1)
            {
                contactmodel = (from c in _db.CDMA_INDIVIDUAL_CONTACT_DETAIL

                                where c.CUSTOMER_NO == id
                                where c.AUTHORISED == "U"
                                select new IndividualContactDetails
                                {
                                    CUSTOMER_NO = c.CUSTOMER_NO,
                                    MOBILE_NO = c.MOBILE_NO,
                                    EMAIL_ADDRESS = c.EMAIL_ADDRESS,
                                    MAILING_ADDRESS = c.MAILING_ADDRESS,
                                    ALTERNATEPHONENO = c.ALTERNATEPHONENO,
                                    RESIDENCE_LGA = c.RESIDENCE_LGA,
                                    NEAREST_BUSTOP_LANDMARK = c.NEAREST_BUSTOP_LANDMARK,
                                    BRANCH_CODE = c.BRANCH_CODE,
                                    STATE = c.STATE,
                                    CITY = c.CITY,
                                    ADDRESS_NUMBER = c.ADDRESS_NUMBER,
                                    MAILING_ADDRESS_STREET = c.MAILING_ADDRESS_STREET,
                                    TIER = c.TIER,
                                    CREATED_DATE = c.CREATED_DATE,
                                    CREATED_BY = c.CREATED_BY,
                                    LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                                    LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                                    IP_ADDRESS = c.IP_ADDRESS,
                                }).FirstOrDefault();
            }
            else if (contactrecords == 1)
            {
                contactmodel = (from c in _db.CDMA_INDIVIDUAL_CONTACT_DETAIL

                                where c.CUSTOMER_NO == id
                                where c.AUTHORISED == "A"
                                select new IndividualContactDetails
                                {
                                    CUSTOMER_NO = c.CUSTOMER_NO,
                                    MOBILE_NO = c.MOBILE_NO,
                                    EMAIL_ADDRESS = c.EMAIL_ADDRESS,
                                    MAILING_ADDRESS = c.MAILING_ADDRESS,
                                    ALTERNATEPHONENO = c.ALTERNATEPHONENO,
                                    RESIDENCE_LGA = c.RESIDENCE_LGA,
                                    NEAREST_BUSTOP_LANDMARK = c.NEAREST_BUSTOP_LANDMARK,
                                    BRANCH_CODE = c.BRANCH_CODE,
                                    STATE = c.STATE,
                                    CITY = c.CITY,
                                    ADDRESS_NUMBER = c.ADDRESS_NUMBER,
                                    MAILING_ADDRESS_STREET = c.MAILING_ADDRESS_STREET,
                                    TIER = c.TIER,
                                    CREATED_DATE = c.CREATED_DATE,
                                    CREATED_BY = c.CREATED_BY,
                                    LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                                    LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                                    IP_ADDRESS = c.IP_ADDRESS,
                                }).FirstOrDefault();
            }
            IndContactPrepareModel(contactmodel);
            #endregion

            #region Get Contact Exception Columns
            var contactcustid = "";
            try
            {
                contactcustid = _db.MdmDqRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_INDIVIDUAL_CONTACT_DETAIL" && a.CUST_ID == contactmodel.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (contactcustid != "" && contactmodel != null)
            {
                var exceptionSet2 = _db.MdmDqRunExceptions.Where(a => a.CUST_ID == contactcustid); //.Select(a=>a.PROPERTYNAME);
                if (contactmodel != null)
                {
                    foreach (var item in contactmodel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in exceptionSet2)
                        {
                            if (item2.CATALOG_TAB_COL == item.Name)
                            {
                                ModelState.AddModelError("contact." + item.Name, string.Format("Attention!"));
                            }
                        }
                        //props.Add(item.Name);

                    }
                }
            }
            #endregion

            #region Identification
            int idrecords = _db.CDMA_INDIVIDUAL_IDENTIFICATION.Count(o => o.CUSTOMER_NO == id);
            IndividualIDDetail idmodel = new IndividualIDDetail();
            if (idrecords > 1)
            {
                idmodel = (from c in _db.CDMA_INDIVIDUAL_IDENTIFICATION
                           where c.CUSTOMER_NO == id
                           where c.AUTHORISED == "U"
                           select new IndividualIDDetail
                           {
                               CUSTOMER_NO = c.CUSTOMER_NO,
                               IDENTIFICATION_TYPE = c.IDENTIFICATION_TYPE,
                               ID_NO = c.ID_NO,
                               ID_EXPIRY_DATE = c.ID_EXPIRY_DATE,
                               ID_ISSUE_DATE = c.ID_ISSUE_DATE,
                               BRANCH_CODE = c.BRANCH_CODE,
                               TIER = c.TIER,
                               LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                               LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                               IP_ADDRESS = c.IP_ADDRESS
                           }).FirstOrDefault();
            }
            else if (idrecords == 1)
            {
                idmodel = (from c in _db.CDMA_INDIVIDUAL_IDENTIFICATION
                           where c.CUSTOMER_NO == id
                           where c.AUTHORISED == "A"
                           select new IndividualIDDetail
                           {
                               CUSTOMER_NO = c.CUSTOMER_NO,
                               IDENTIFICATION_TYPE = c.IDENTIFICATION_TYPE,
                               ID_NO = c.ID_NO,
                               ID_EXPIRY_DATE = c.ID_EXPIRY_DATE,
                               ID_ISSUE_DATE = c.ID_ISSUE_DATE,
                               BRANCH_CODE = c.BRANCH_CODE,
                               TIER = c.TIER,
                               LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                               LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                               IP_ADDRESS = c.IP_ADDRESS
                           }).FirstOrDefault();
            }
            IndIdDataPrepareModel(idmodel);
            #endregion

            #region Get Id Exception Columns
            var idcustid = "";
            try
            {
                idcustid = _db.MdmDqRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_INDIVIDUAL_IDENTIFICATION" && a.CUST_ID == idmodel.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (idcustid != "" && idmodel != null)
            {
                var exceptionSet4 = _db.MdmDqRunExceptions.Where(a => a.CUST_ID == idcustid); //.Select(a=>a.PROPERTYNAME);
                if (idmodel != null)
                {
                    foreach (var item in idmodel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                    {
                        foreach (var item2 in exceptionSet4)
                        {
                            if (item2.CATALOG_TAB_COL == item.Name)
                            {
                                ModelState.AddModelError("identification." + item.Name, string.Format("Attention!"));
                            }
                        }
                        //props.Add(item.Name);

                    }
                }
            }
            #endregion

            #region Not used
            //int addressrecords = _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Count(o => o.CUSTOMER_NO == id);
            //IndividualAddressDetails addressmodel = new IndividualAddressDetails();
            //IndAddDataPrepareModel(addressmodel);
            //if (addressrecords > 1)
            //{
            //    addressmodel = (from c in _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL
            //                    where c.CUSTOMER_NO == id
            //                    where c.AUTHORISED == "U"
            //                    select new IndividualAddressDetails
            //                    {
            //                        CUSTOMER_NO = c.CUSTOMER_NO,
            //                        RESIDENTIAL_ADDRESS = c.RESIDENTIAL_ADDRESS,
            //                        CITY_TOWN_OF_RESIDENCE = c.CITY_TOWN_OF_RESIDENCE,
            //                        LGA_OF_RESIDENCE = c.LGA_OF_RESIDENCE,
            //                        NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
            //                        STATE_OF_RESIDENCE = c.STATE_OF_RESIDENCE,
            //                        COUNTRY_OF_RESIDENCE = c.COUNTRY_OF_RESIDENCE,
            //                        RESIDENCE_OWNED_OR_RENT = c.RESIDENCE_OWNED_OR_RENT,
            //                        ZIP_POSTAL_CODE = c.ZIP_POSTAL_CODE,
            //                        CREATED_DATE = c.CREATED_DATE,
            //                        CREATED_BY = c.CREATED_BY,
            //                        LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
            //                        LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
            //                        IP_ADDRESS = c.IP_ADDRESS,
            //                    }).FirstOrDefault();
            //}
            //else if (addressrecords == 1)
            //{
            //    addressmodel = (from c in _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL
            //                    where c.CUSTOMER_NO == id
            //                    where c.AUTHORISED == "A"
            //                    select new IndividualAddressDetails
            //                    {
            //                        CUSTOMER_NO = c.CUSTOMER_NO,
            //                        RESIDENTIAL_ADDRESS = c.RESIDENTIAL_ADDRESS,
            //                        CITY_TOWN_OF_RESIDENCE = c.CITY_TOWN_OF_RESIDENCE,
            //                        LGA_OF_RESIDENCE = c.LGA_OF_RESIDENCE,
            //                        NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
            //                        STATE_OF_RESIDENCE = c.STATE_OF_RESIDENCE,
            //                        COUNTRY_OF_RESIDENCE = c.COUNTRY_OF_RESIDENCE,
            //                        RESIDENCE_OWNED_OR_RENT = c.RESIDENCE_OWNED_OR_RENT,
            //                        ZIP_POSTAL_CODE = c.ZIP_POSTAL_CODE,
            //                        CREATED_DATE = c.CREATED_DATE,
            //                        CREATED_BY = c.CREATED_BY,
            //                        LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
            //                        LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
            //                        IP_ADDRESS = c.IP_ADDRESS,
            //                    }).FirstOrDefault();
            //}

            ////Get all address Exception Columns
            //var addresscustid = "";
            //try
            //{
            //    addresscustid = _db.MdmDqRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_INDIVIDUAL_ADDRESS_DETAIL" && a.CUST_ID == addressmodel.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //if (addresscustid != "" && addressmodel != null)
            //{
            //    var exceptionSet3 = _db.MdmDqRunExceptions.Where(a => a.CUST_ID == addresscustid); //.Select(a=>a.PROPERTYNAME);
            //    if (addressmodel != null)
            //    {
            //        foreach (var item in addressmodel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
            //        {
            //            foreach (var item2 in exceptionSet3)
            //            {
            //                if (item2.CATALOG_TAB_COL == item.Name)
            //                {
            //                    ModelState.AddModelError("AddressDetails." + item.Name, string.Format("Attention!"));
            //                }
            //            }
            //            //props.Add(item.Name);

            //        }
            //    }
            //}



            //int otherrecords = _db.CDMA_INDIVIDUAL_OTHER_DETAILS.Count(o => o.CUSTOMER_NO == id);
            //OtherDetails othermodel = new OtherDetails();
            //if (otherrecords > 1)
            //{
            //    othermodel = (from c in _db.CDMA_INDIVIDUAL_OTHER_DETAILS
            //                  where c.CUSTOMER_NO == id
            //                  where c.AUTHORISED == "U"
            //                  select new OtherDetails
            //                  {
            //                      CUSTOMER_NO = c.CUSTOMER_NO,
            //                      TIN_NO = c.TIN_NO
            //                  }).FirstOrDefault();
            //}
            //else if (otherrecords == 1)
            //{
            //    othermodel = (from c in _db.CDMA_INDIVIDUAL_OTHER_DETAILS
            //                  where c.CUSTOMER_NO == id
            //                  where c.AUTHORISED == "A"
            //                  select new OtherDetails
            //                  {
            //                      CUSTOMER_NO = c.CUSTOMER_NO,
            //                      TIN_NO = c.TIN_NO
            //                  }).FirstOrDefault();
            //}

            ////Get all other Exception Columns
            //var othercustid = "";
            //try
            //{
            //    othercustid = _db.MdmDqRunExceptions.Where(a => a.CATALOG_TABLE_NAME == "CDMA_INDIVIDUAL_OTHER_DETAILS" && a.CUST_ID == othermodel.CUSTOMER_NO).OrderByDescending(a => a.CREATED_DATE).FirstOrDefault().CUST_ID;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //if (othercustid != "" && othermodel != null)
            //{
            //    var exceptionSet5 = _db.MdmDqRunExceptions.Where(a => a.CUST_ID == othercustid); //.Select(a=>a.PROPERTYNAME);
            //    if (othermodel != null)
            //    {
            //        foreach (var item in othermodel.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
            //        {
            //            foreach (var item2 in exceptionSet5)
            //            {
            //                if (item2.CATALOG_TAB_COL == item.Name)
            //                {
            //                    ModelState.AddModelError("otherdetails." + item.Name, string.Format("Attention!"));
            //                }
            //            }
            //            //props.Add(item.Name);

            //        }
            //    }
            //}
            #endregion

            viewModel.BioData = indvmodel;
            viewModel.contact = contactmodel;
            viewModel.identification = idmodel;
            //viewModel.otherdetails = othermodel;
            //viewModel.AddressDetails = addressmodel;

            viewModel.BioData.ReadOnlyForm = "True";
            return View(viewModel);
        }

        // POST: IndividualCustomer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BiodataInfoViewModel bioDatamodel, bool continueEditing)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();
            var identity = ((CustomPrincipal)User).CustomIdentity;
            bool updateFlag = false;

            if (ModelState.IsValid)
            {
                CDMA_INDIVIDUAL_BIO_DATA originalObject = new CDMA_INDIVIDUAL_BIO_DATA();
                CDMA_INDIVIDUAL_CONTACT_DETAIL originalObject1 = new CDMA_INDIVIDUAL_CONTACT_DETAIL();
                CDMA_INDIVIDUAL_ADDRESS_DETAIL originalObject2 = new CDMA_INDIVIDUAL_ADDRESS_DETAIL();
                CDMA_INDIVIDUAL_IDENTIFICATION originalObject3 = new CDMA_INDIVIDUAL_IDENTIFICATION();
                CDMA_INDIVIDUAL_OTHER_DETAILS originalObject4 = new CDMA_INDIVIDUAL_OTHER_DETAILS();

                using (var db = new AppDbContext())
                {
                    #region BioData
                    int records = db.CDMA_INDIVIDUAL_BIO_DATA.Count(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);
                    //if there are more than one records, the 'U' one is the edited one
                    if (records > 1)
                    {
                        updateFlag = true;
                        originalObject = _db.CDMA_INDIVIDUAL_BIO_DATA.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var entity = db.CDMA_INDIVIDUAL_BIO_DATA.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (entity != null)
                        {
                            entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            entity.TITLE = bioDatamodel.BioData.TITLE;
                            entity.SURNAME = bioDatamodel.BioData.SURNAME;
                            entity.FIRST_NAME = bioDatamodel.BioData.FIRST_NAME;
                            entity.OTHER_NAME = bioDatamodel.BioData.OTHER_NAME;
                            entity.DATE_OF_BIRTH = bioDatamodel.BioData.DATE_OF_BIRTH;
                            entity.COUNTRY_OF_BIRTH = bioDatamodel.BioData.COUNTRY_OF_BIRTH;
                            entity.SEX = bioDatamodel.BioData.SEX;
                            entity.MARITAL_STATUS = bioDatamodel.BioData.MARITAL_STATUS;
                            entity.LGAOFORIGIN = bioDatamodel.BioData.LGAOFORIGIN;
                            entity.MOTHER_MAIDEN_NAME = bioDatamodel.BioData.MOTHER_MAIDEN_NAME;
                            entity.COUNTRY_OF_BIRTH = bioDatamodel.BioData.COUNTRY_OF_BIRTH;
                            entity.RESIDENCE_COUNTRY = bioDatamodel.BioData.RESIDENCE_COUNTRY;
                            entity.NATIONALITY = bioDatamodel.BioData.NATIONALITY;
                            entity.RELIGION = bioDatamodel.BioData.RELIGION;
                            entity.STATE_OF_ORIGIN = bioDatamodel.BioData.STATE_OF_ORIGIN;
                            entity.TIN = bioDatamodel.BioData.TIN;
                            entity.BVN = bioDatamodel.BioData.BVN;
                            entity.RESIDENCEPERMIT_ISSDATE = bioDatamodel.BioData.RESIDENCEPERMIT_ISSDATE;
                            entity.RESIDENCEPERMIT_EXPDATE = bioDatamodel.BioData.RESIDENCEPERMIT_EXPDATE;
                            entity.SECONDNATIONALITY = bioDatamodel.BioData.SECONDNATIONALITY;
                            entity.BRANCH_CODE = bioDatamodel.BioData.BRANCH_CODE;
                            entity.TIER = bioDatamodel.BioData.TIER;
                            entity.QUEUE_STATUS = 1;

                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            db.CDMA_INDIVIDUAL_BIO_DATA.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject);
                            _messageService.LogEmailJob(identity.ProfileId, entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                        }
                    }
                    else if (records == 1)
                    {
                        updateFlag = false;
                        var entity = db.CDMA_INDIVIDUAL_BIO_DATA.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject = _db.CDMA_INDIVIDUAL_BIO_DATA.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject != null)
                        {
                            entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            entity.TITLE = bioDatamodel.BioData.TITLE;
                            entity.SURNAME = bioDatamodel.BioData.SURNAME;
                            entity.FIRST_NAME = bioDatamodel.BioData.FIRST_NAME;
                            entity.OTHER_NAME = bioDatamodel.BioData.OTHER_NAME;
                            entity.DATE_OF_BIRTH = bioDatamodel.BioData.DATE_OF_BIRTH;
                            entity.COUNTRY_OF_BIRTH = bioDatamodel.BioData.COUNTRY_OF_BIRTH;
                            entity.SEX = bioDatamodel.BioData.SEX;
                            entity.MARITAL_STATUS = bioDatamodel.BioData.MARITAL_STATUS;
                            entity.LGAOFORIGIN = bioDatamodel.BioData.LGAOFORIGIN;
                            entity.MOTHER_MAIDEN_NAME = bioDatamodel.BioData.MOTHER_MAIDEN_NAME;
                            entity.COUNTRY_OF_BIRTH = bioDatamodel.BioData.COUNTRY_OF_BIRTH;
                            entity.RESIDENCE_COUNTRY = bioDatamodel.BioData.RESIDENCE_COUNTRY;
                            entity.NATIONALITY = bioDatamodel.BioData.NATIONALITY;
                            entity.RELIGION = bioDatamodel.BioData.RELIGION;
                            entity.STATE_OF_ORIGIN = bioDatamodel.BioData.STATE_OF_ORIGIN;
                            entity.TIN = bioDatamodel.BioData.TIN;
                            entity.BVN = bioDatamodel.BioData.BVN;
                            entity.RESIDENCEPERMIT_ISSDATE = bioDatamodel.BioData.RESIDENCEPERMIT_ISSDATE;
                            entity.RESIDENCEPERMIT_EXPDATE = bioDatamodel.BioData.RESIDENCEPERMIT_EXPDATE;
                            entity.SECONDNATIONALITY = bioDatamodel.BioData.SECONDNATIONALITY;
                            entity.BRANCH_CODE = bioDatamodel.BioData.BRANCH_CODE;
                            entity.TIER = bioDatamodel.BioData.TIER;
                            entity.QUEUE_STATUS = 1;

                            db.CDMA_INDIVIDUAL_BIO_DATA.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var newentity = new CDMA_INDIVIDUAL_BIO_DATA();
                            newentity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            newentity.TITLE = bioDatamodel.BioData.TITLE;
                            newentity.SURNAME = bioDatamodel.BioData.SURNAME;
                            newentity.FIRST_NAME = bioDatamodel.BioData.FIRST_NAME;
                            newentity.OTHER_NAME = bioDatamodel.BioData.OTHER_NAME;
                            newentity.DATE_OF_BIRTH = bioDatamodel.BioData.DATE_OF_BIRTH;
                            newentity.COUNTRY_OF_BIRTH = bioDatamodel.BioData.COUNTRY_OF_BIRTH;
                            newentity.SEX = bioDatamodel.BioData.SEX;
                            newentity.MARITAL_STATUS = bioDatamodel.BioData.MARITAL_STATUS;
                            newentity.LGAOFORIGIN = bioDatamodel.BioData.LGAOFORIGIN;
                            newentity.MOTHER_MAIDEN_NAME = bioDatamodel.BioData.MOTHER_MAIDEN_NAME;
                            newentity.COUNTRY_OF_BIRTH = bioDatamodel.BioData.COUNTRY_OF_BIRTH;
                            newentity.RESIDENCE_COUNTRY = bioDatamodel.BioData.RESIDENCE_COUNTRY;
                            newentity.NATIONALITY = bioDatamodel.BioData.NATIONALITY;
                            newentity.RELIGION = bioDatamodel.BioData.RELIGION;
                            newentity.STATE_OF_ORIGIN = bioDatamodel.BioData.STATE_OF_ORIGIN;
                            newentity.TIN = bioDatamodel.BioData.TIN;
                            newentity.BVN = bioDatamodel.BioData.BVN;
                            newentity.RESIDENCEPERMIT_ISSDATE = bioDatamodel.BioData.RESIDENCEPERMIT_ISSDATE;
                            newentity.RESIDENCEPERMIT_EXPDATE = bioDatamodel.BioData.RESIDENCEPERMIT_EXPDATE;
                            newentity.SECONDNATIONALITY = bioDatamodel.BioData.SECONDNATIONALITY;
                            newentity.BRANCH_CODE = bioDatamodel.BioData.BRANCH_CODE;
                            newentity.TIER = bioDatamodel.BioData.TIER;

                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            newentity.QUEUE_STATUS = 1;
                            db.CDMA_INDIVIDUAL_BIO_DATA.Add(newentity);
                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, newentity.CUSTOMER_NO, MessageJobEnum.MailType.Change);
                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", bioDatamodel.BioData.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                    #endregion
                    #region ContactData
                    int records1 = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Count(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records1 > 1)
                    {
                        updateFlag = true;
                        originalObject1 = _db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var contact_entity = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (contact_entity != null)
                        {
                            contact_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            contact_entity.MOBILE_NO = bioDatamodel.contact.MOBILE_NO;
                            contact_entity.EMAIL_ADDRESS = bioDatamodel.contact.EMAIL_ADDRESS;
                            contact_entity.MAILING_ADDRESS = bioDatamodel.contact.MAILING_ADDRESS;
                            contact_entity.ALTERNATEPHONENO = bioDatamodel.contact.ALTERNATEPHONENO;
                            contact_entity.RESIDENCE_LGA = bioDatamodel.contact.RESIDENCE_LGA;
                            contact_entity.NEAREST_BUSTOP_LANDMARK = bioDatamodel.contact.NEAREST_BUSTOP_LANDMARK;
                            contact_entity.BRANCH_CODE = bioDatamodel.contact.BRANCH_CODE;
                            contact_entity.STATE = bioDatamodel.contact.STATE;
                            contact_entity.CITY = bioDatamodel.contact.CITY;
                            contact_entity.ADDRESS_NUMBER = bioDatamodel.contact.ADDRESS_NUMBER;
                            contact_entity.MAILING_ADDRESS_STREET = bioDatamodel.contact.MAILING_ADDRESS_STREET;
                            contact_entity.TIER = bioDatamodel.contact.TIER;
                            contact_entity.QUEUE_STATUS = 1;

                            contact_entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            contact_entity.LAST_MODIFIED_DATE = DateTime.Now;
                            db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Attach(contact_entity);
                            db.Entry(contact_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject1);
                            _messageService.LogEmailJob(identity.ProfileId, contact_entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                        }
                    }
                    else if (records1 == 1)
                    {
                        updateFlag = false;
                        var contact_entity = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject1 = _db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject1 != null)
                        {
                            contact_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            contact_entity.MOBILE_NO = bioDatamodel.contact.MOBILE_NO;
                            contact_entity.EMAIL_ADDRESS = bioDatamodel.contact.EMAIL_ADDRESS;
                            contact_entity.MAILING_ADDRESS = bioDatamodel.contact.MAILING_ADDRESS;
                            contact_entity.ALTERNATEPHONENO = bioDatamodel.contact.ALTERNATEPHONENO;
                            contact_entity.RESIDENCE_LGA = bioDatamodel.contact.RESIDENCE_LGA;
                            contact_entity.NEAREST_BUSTOP_LANDMARK = bioDatamodel.contact.NEAREST_BUSTOP_LANDMARK;
                            contact_entity.BRANCH_CODE = bioDatamodel.contact.BRANCH_CODE;
                            contact_entity.STATE = bioDatamodel.contact.STATE;
                            contact_entity.CITY = bioDatamodel.contact.CITY;
                            contact_entity.ADDRESS_NUMBER = bioDatamodel.contact.ADDRESS_NUMBER;
                            contact_entity.MAILING_ADDRESS_STREET = bioDatamodel.contact.MAILING_ADDRESS_STREET;
                            contact_entity.TIER = bioDatamodel.contact.TIER;
                            contact_entity.QUEUE_STATUS = 1;

                            db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Attach(contact_entity);
                            db.Entry(contact_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject1);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var new_contact_entity = new CDMA_INDIVIDUAL_CONTACT_DETAIL();
                            new_contact_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            new_contact_entity.MOBILE_NO = bioDatamodel.contact.MOBILE_NO;
                            new_contact_entity.EMAIL_ADDRESS = bioDatamodel.contact.EMAIL_ADDRESS;
                            new_contact_entity.MAILING_ADDRESS = bioDatamodel.contact.MAILING_ADDRESS;
                            new_contact_entity.ALTERNATEPHONENO = bioDatamodel.contact.ALTERNATEPHONENO;
                            new_contact_entity.RESIDENCE_LGA = bioDatamodel.contact.RESIDENCE_LGA;
                            new_contact_entity.NEAREST_BUSTOP_LANDMARK = bioDatamodel.contact.NEAREST_BUSTOP_LANDMARK;
                            new_contact_entity.BRANCH_CODE = bioDatamodel.contact.BRANCH_CODE;
                            new_contact_entity.STATE = bioDatamodel.contact.STATE;
                            new_contact_entity.CITY = bioDatamodel.contact.CITY;
                            new_contact_entity.ADDRESS_NUMBER = bioDatamodel.contact.ADDRESS_NUMBER;
                            new_contact_entity.MAILING_ADDRESS_STREET = bioDatamodel.contact.MAILING_ADDRESS_STREET;
                            new_contact_entity.TIER = bioDatamodel.contact.TIER;
                            new_contact_entity.QUEUE_STATUS = 1;

                            new_contact_entity.CREATED_BY = identity.ProfileId.ToString();
                            new_contact_entity.CREATED_DATE = DateTime.Now;
                            new_contact_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Add(new_contact_entity);
                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, new_contact_entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", bioDatamodel.contact.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                    #endregion
                    #region Iddata
                    int records3 = db.CDMA_INDIVIDUAL_IDENTIFICATION.Count(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records3 > 1)
                    {
                        updateFlag = true;
                        originalObject3 = _db.CDMA_INDIVIDUAL_IDENTIFICATION.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var ID_entity = db.CDMA_INDIVIDUAL_IDENTIFICATION.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (ID_entity != null)
                        {
                            ID_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            ID_entity.IDENTIFICATION_TYPE = bioDatamodel.identification.IDENTIFICATION_TYPE;
                            ID_entity.ID_NO = bioDatamodel.identification.ID_NO;
                            ID_entity.ID_EXPIRY_DATE = bioDatamodel.identification.ID_EXPIRY_DATE;
                            ID_entity.ID_ISSUE_DATE = bioDatamodel.identification.ID_ISSUE_DATE;
                            ID_entity.BRANCH_CODE = bioDatamodel.identification.BRANCH_CODE;
                            ID_entity.TIER = bioDatamodel.identification.TIER;
                            ID_entity.QUEUE_STATUS = 1;

                            ID_entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            ID_entity.LAST_MODIFIED_DATE = DateTime.Now;
                            db.CDMA_INDIVIDUAL_IDENTIFICATION.Attach(ID_entity);
                            db.Entry(ID_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject3);
                            _messageService.LogEmailJob(identity.ProfileId, ID_entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                        }
                    }
                    else if (records3 == 1)
                    {
                        updateFlag = false;
                        var ID_entity = db.CDMA_INDIVIDUAL_IDENTIFICATION.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject3 = _db.CDMA_INDIVIDUAL_IDENTIFICATION.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject3 != null)
                        {
                            ID_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            ID_entity.IDENTIFICATION_TYPE = bioDatamodel.identification.IDENTIFICATION_TYPE;
                            ID_entity.ID_NO = bioDatamodel.identification.ID_NO;
                            ID_entity.ID_EXPIRY_DATE = bioDatamodel.identification.ID_EXPIRY_DATE;
                            ID_entity.ID_ISSUE_DATE = bioDatamodel.identification.ID_ISSUE_DATE;
                            ID_entity.BRANCH_CODE = bioDatamodel.identification.BRANCH_CODE;
                            ID_entity.TIER = bioDatamodel.identification.TIER;
                            ID_entity.QUEUE_STATUS = 1;

                            db.CDMA_INDIVIDUAL_IDENTIFICATION.Attach(ID_entity);
                            db.Entry(ID_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject3);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var new_ID_entity = new CDMA_INDIVIDUAL_IDENTIFICATION();

                            new_ID_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            new_ID_entity.IDENTIFICATION_TYPE = bioDatamodel.identification.IDENTIFICATION_TYPE;
                            new_ID_entity.ID_NO = bioDatamodel.identification.ID_NO;
                            new_ID_entity.ID_EXPIRY_DATE = bioDatamodel.identification.ID_EXPIRY_DATE;
                            new_ID_entity.ID_ISSUE_DATE = bioDatamodel.identification.ID_ISSUE_DATE;
                            new_ID_entity.BRANCH_CODE = bioDatamodel.identification.BRANCH_CODE;
                            new_ID_entity.TIER = bioDatamodel.identification.TIER;
                            new_ID_entity.QUEUE_STATUS = 1;

                            new_ID_entity.CREATED_BY = identity.ProfileId.ToString();
                            new_ID_entity.CREATED_DATE = DateTime.Now;
                            new_ID_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_IDENTIFICATION.Add(new_ID_entity);
                            db.SaveChanges(); //do not track audit.
                            _messageService.LogEmailJob(identity.ProfileId, new_ID_entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", bioDatamodel.identification.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                    #endregion

                    #region Not used AddressData
                    //int records2 = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Count(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    ////if there are more than one records, the 'U' one is the edited one
                    //if (records2 > 1)
                    //{
                    //    updateFlag = true;
                    //    originalObject2 = _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                    //    var Address_entity = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U");

                    //    if (Address_entity != null)
                    //    {
                    //        Address_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                    //        Address_entity.RESIDENTIAL_ADDRESS = bioDatamodel.AddressDetails.RESIDENTIAL_ADDRESS;
                    //        Address_entity.CITY_TOWN_OF_RESIDENCE = bioDatamodel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
                    //        Address_entity.LGA_OF_RESIDENCE = bioDatamodel.AddressDetails.LGA_OF_RESIDENCE;
                    //        Address_entity.NEAREST_BUS_STOP_LANDMARK = bioDatamodel.AddressDetails.NEAREST_BUS_STOP_LANDMARK;
                    //        Address_entity.STATE_OF_RESIDENCE = bioDatamodel.AddressDetails.STATE_OF_RESIDENCE;
                    //        Address_entity.COUNTRY_OF_RESIDENCE = bioDatamodel.AddressDetails.COUNTRY_OF_RESIDENCE;
                    //        Address_entity.RESIDENCE_OWNED_OR_RENT = bioDatamodel.AddressDetails.RESIDENCE_OWNED_OR_RENT;
                    //        Address_entity.ZIP_POSTAL_CODE = bioDatamodel.AddressDetails.ZIP_POSTAL_CODE;

                    //        Address_entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                    //        Address_entity.LAST_MODIFIED_DATE = DateTime.Now;
                    //        db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Attach(Address_entity);
                    //        db.Entry(Address_entity).State = EntityState.Modified;
                    //        db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject2);
                    //        _messageService.LogEmailJob(identity.ProfileId, Address_entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                    //    }
                    //}
                    //else if (records2 == 1)
                    //{
                    //    updateFlag = false;
                    //    var Address_entity = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A");
                    //    originalObject2 = _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                    //    if (originalObject2 != null)
                    //    {
                    //        Address_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                    //        Address_entity.RESIDENTIAL_ADDRESS = bioDatamodel.AddressDetails.RESIDENTIAL_ADDRESS;
                    //        Address_entity.CITY_TOWN_OF_RESIDENCE = bioDatamodel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
                    //        Address_entity.LGA_OF_RESIDENCE = bioDatamodel.AddressDetails.LGA_OF_RESIDENCE;
                    //        Address_entity.NEAREST_BUS_STOP_LANDMARK = bioDatamodel.AddressDetails.NEAREST_BUS_STOP_LANDMARK;
                    //        Address_entity.STATE_OF_RESIDENCE = bioDatamodel.AddressDetails.STATE_OF_RESIDENCE;
                    //        Address_entity.COUNTRY_OF_RESIDENCE = bioDatamodel.AddressDetails.COUNTRY_OF_RESIDENCE;
                    //        Address_entity.RESIDENCE_OWNED_OR_RENT = bioDatamodel.AddressDetails.RESIDENCE_OWNED_OR_RENT;
                    //        Address_entity.ZIP_POSTAL_CODE = bioDatamodel.AddressDetails.ZIP_POSTAL_CODE;

                    //        db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Attach(Address_entity);
                    //        db.Entry(Address_entity).State = EntityState.Modified;
                    //        db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject2);  //track the audit


                    //        // There is no 'U' status row in the table so, Add new record with mnt_status U
                    //        //entity.AUTHORISED = "U";
                    //        var new_Address_entity = new CDMA_INDIVIDUAL_ADDRESS_DETAIL();
                    //        new_Address_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                    //        new_Address_entity.RESIDENTIAL_ADDRESS = bioDatamodel.AddressDetails.RESIDENTIAL_ADDRESS;
                    //        new_Address_entity.CITY_TOWN_OF_RESIDENCE = bioDatamodel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
                    //        new_Address_entity.LGA_OF_RESIDENCE = bioDatamodel.AddressDetails.LGA_OF_RESIDENCE;
                    //        new_Address_entity.NEAREST_BUS_STOP_LANDMARK = bioDatamodel.AddressDetails.NEAREST_BUS_STOP_LANDMARK;
                    //        new_Address_entity.STATE_OF_RESIDENCE = bioDatamodel.AddressDetails.STATE_OF_RESIDENCE;
                    //        new_Address_entity.COUNTRY_OF_RESIDENCE = bioDatamodel.AddressDetails.COUNTRY_OF_RESIDENCE;
                    //        new_Address_entity.RESIDENCE_OWNED_OR_RENT = bioDatamodel.AddressDetails.RESIDENCE_OWNED_OR_RENT;
                    //        new_Address_entity.ZIP_POSTAL_CODE = bioDatamodel.AddressDetails.ZIP_POSTAL_CODE;

                    //        new_Address_entity.CREATED_BY = identity.ProfileId.ToString();
                    //        new_Address_entity.CREATED_DATE = DateTime.Now;
                    //        new_Address_entity.AUTHORISED = "U";
                    //        db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Add(new_Address_entity);
                    //        db.SaveChanges(); //do not track audit.
                    //        _messageService.LogEmailJob(identity.ProfileId, new_Address_entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                    //    }
                    //    else
                    //    {
                    //        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", bioDatamodel.contact.CUSTOMER_NO);
                    //        ModelState.AddModelError("", errorMessage);
                    //    }
                    //}
                    #endregion
                    #region Not Used OtherDetails
                    //int records4 = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Count(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    ////if there are more than one records, the 'U' one is the edited one
                    //if (records4 > 1)
                    //{
                    //    updateFlag = true;
                    //    originalObject4 = _db.CDMA_INDIVIDUAL_OTHER_DETAILS.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                    //    var Other_entity = db.CDMA_INDIVIDUAL_OTHER_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U");

                    //    if (Other_entity != null)
                    //    {
                    //        Other_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                    //        Other_entity.TIN_NO = bioDatamodel.otherdetails.TIN_NO;

                    //        Other_entity.LAST_MODIFIED_DATE = DateTime.Now;
                    //        db.CDMA_INDIVIDUAL_OTHER_DETAILS.Attach(Other_entity);
                    //        db.Entry(Other_entity).State = EntityState.Modified;
                    //        db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject4);
                    //        _messageService.LogEmailJob(identity.ProfileId, Other_entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                    //    }
                    //}
                    //else if (records4 == 1)
                    //{
                    //    updateFlag = false;
                    //    var Other_entity = db.CDMA_INDIVIDUAL_OTHER_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A");
                    //    originalObject4 = _db.CDMA_INDIVIDUAL_OTHER_DETAILS.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                    //    if (originalObject4 != null)
                    //    {
                    //        Other_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                    //        Other_entity.TIN_NO = bioDatamodel.otherdetails.TIN_NO;

                    //        db.CDMA_INDIVIDUAL_OTHER_DETAILS.Attach(Other_entity);
                    //        db.Entry(Other_entity).State = EntityState.Modified;
                    //        db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject4);  //track the audit


                    //        // There is no 'U' status row in the table so, Add new record with mnt_status U
                    //        //entity.AUTHORISED = "U";
                    //        var new_Other_entity = new CDMA_INDIVIDUAL_OTHER_DETAILS();

                    //        new_Other_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                    //        new_Other_entity.TIN_NO = bioDatamodel.otherdetails.TIN_NO;

                    //        new_Other_entity.CREATED_BY = identity.ProfileId.ToString();
                    //        new_Other_entity.CREATED_DATE = DateTime.Now;
                    //        new_Other_entity.AUTHORISED = "U";
                    //        db.CDMA_INDIVIDUAL_OTHER_DETAILS.Add(new_Other_entity);
                    //        db.SaveChanges(); //do not track audit.
                    //        _messageService.LogEmailJob(identity.ProfileId, new_Other_entity.CUSTOMER_NO, MessageJobEnum.MailType.Change);

                    //    }
                    //    else
                    //    {
                    //        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", bioDatamodel.identification.CUSTOMER_NO);
                    //        ModelState.AddModelError("", errorMessage);
                    //    }
                    //}
                    #endregion Not Used
                }
                SuccessNotification("Bio Data Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = bioDatamodel.BioData.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
            }    
            IndBioDataPrepareModel(bioDatamodel.BioData);
            return View(bioDatamodel.BioData);
        }

        [NonAction]
        protected virtual void IndBioDataPrepareModel(IndividualBioDataModel IndividualBioDataModel)
        {
            if (IndividualBioDataModel == null)
                throw new ArgumentNullException("model");
            if (IndividualBioDataModel != null)
            {
                IndividualBioDataModel.Gender.Add(new SelectListItem
                {
                    Text = "Male",
                    Value = "Male"
                });
                IndividualBioDataModel.Gender.Add(new SelectListItem
                {
                    Text = "Female",
                    Value = "Female"
                });
                IndividualBioDataModel.IndDisAbility.Add(new SelectListItem
                {
                    Text = "Yes",
                    Value = "Y"
                });
                IndividualBioDataModel.IndDisAbility.Add(new SelectListItem
                {
                    Text = "No",
                    Value = "N"
                });

                IndividualBioDataModel.CusComplexion.Add(new SelectListItem
                {
                    Text = "Dark",
                    Value = "Dark"
                });
                IndividualBioDataModel.CusComplexion.Add(new SelectListItem
                {
                    Text = "Fair",
                    Value = "Fair"
                });
                IndividualBioDataModel.CusComplexion.Add(new SelectListItem
                {
                    Text = "Other",
                    Value = "Other"
                });

                IndividualBioDataModel.MaritalStatus.Add(new SelectListItem
                {
                    Text = "Married",
                    Value = "Married"
                });

                IndividualBioDataModel.MaritalStatus.Add(new SelectListItem
                {
                    Text = "Divorced",
                    Value = "Divorced"
                });
                IndividualBioDataModel.MaritalStatus.Add(new SelectListItem
                {
                    Text = "Single",
                    Value = "Single"
                });

                IndividualBioDataModel.CountryofBirth = new SelectList(_db.CDMA_COUNTRIES.OrderBy(x => x.COUNTRY_NAME), "COUNTRY_ID", "COUNTRY_NAME").ToList();
                IndividualBioDataModel.Nationalities = new SelectList(_db.CDMA_COUNTRIES.OrderBy(x => x.COUNTRY_NAME), "COUNTRY_ID", "COUNTRY_NAME").ToList();
                IndividualBioDataModel.Religions = new SelectList(_db.CDMA_RELIGION.OrderBy(x => x.RELIGION), "CODE", "RELIGION").ToList();
                IndividualBioDataModel.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
                IndividualBioDataModel.State = new SelectList(_db.SRC_CDMA_STATE.OrderBy(x => x.STATE_NAME), "STATE_ID", "STATE_NAME").ToList();
                IndividualBioDataModel.TitleTypes = new SelectList(_db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC").ToList();
                IndividualBioDataModel.LGAs = new SelectList(_db.SRC_CDMA_LGA.OrderBy(x => x.LGA_NAME), "LGA_ID", "LGA_NAME").ToList();
            }
        }

        [NonAction]
        protected virtual void IndIdDataPrepareModel(IndividualIDDetail individualIDModel)
        {
            //if (individualIDModel == null)
            //    throw new ArgumentNullException("model");
            if (individualIDModel != null)
            {
                individualIDModel.IdType = new SelectList(_db.CDMA_IDENTIFICATION_TYPE.OrderBy(x => x.ID_TYPE), "CODE", "ID_TYPE").ToList();
                individualIDModel.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
            }

        }

        [NonAction]
        protected virtual void IndContactPrepareModel(IndividualContactDetails individualContactDetails)
        {
            if( individualContactDetails != null)
            {
                individualContactDetails.Branches = new SelectList(_db.CM_BRANCH.OrderBy(x => x.BRANCH_NAME), "BRANCH_ID", "BRANCH_NAME").ToList();
                individualContactDetails.States = new SelectList(_db.SRC_CDMA_STATE.OrderBy(x => x.STATE_NAME), "STATE_ID", "STATE_NAME").ToList();
                individualContactDetails.LGAs = new SelectList(_db.SRC_CDMA_LGA.OrderBy(x => x.LGA_NAME), "LGA_ID", "LGA_NAME").ToList();
            }
        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(BiodataInfoViewModel indivmodel, bool disapproveRecord)
        {
            if (!User.Identity.IsAuthenticated)
                return AccessDeniedView();

            var identity = ((CustomPrincipal)User).CustomIdentity;
            var routeValues = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values;

            int exceptionId = 0;
            if (routeValues.ContainsKey("id"))
                exceptionId = int.Parse((string)routeValues["id"]);
            if (disapproveRecord)
            {
                _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), indivmodel.BioData.AuthoriserRemarks);
                SuccessNotification("Customer record Not Authorised");
                _messageService.LogEmailJob(identity.ProfileId, indivmodel.BioData.CUSTOMER_NO, MessageJobEnum.MailType.Reject, Convert.ToInt32(indivmodel.BioData.LastUpdatedby));
            }
            else
            {
                int que_status = 0;

                string userRole = identity.UserRoleName;
                if (userRole.ToUpper().Contains("CSM"))
                    que_status = 1;
                else if (userRole.ToUpper().Contains("AMU CHECKER"))
                    que_status = 2;
                else que_status = 1;

                _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(),  identity.ProfileId);
                SuccessNotification("Customer record Authorised");
                _messageService.LogEmailJob(identity.ProfileId, indivmodel.BioData.CUSTOMER_NO, MessageJobEnum.MailType.Authorize, Convert.ToInt32(indivmodel.BioData.LastUpdatedby));
            }
            return RedirectToAction("AuthList", "DQQue");
        }

        #region Not in use
        [NonAction]
        protected virtual void IndAddDataPrepareModel(IndividualAddressDetails IndividualAddressModel)
        {
            //if (IndividualAddressModel == null)
            //    throw new ArgumentNullException("model");
            if (IndividualAddressModel != null)
            {

                IndividualAddressModel.ResidenceStatus.Add(new SelectListItem
                {
                    Text = "Owned",
                    Value = "Owned"
                });
                IndividualAddressModel.ResidenceStatus.Add(new SelectListItem
                {
                    Text = "Rented",
                    Value = "Rented"
                });


                IndividualAddressModel.CountryofResidence = new SelectList(_db.CDMA_COUNTRIES.OrderBy(x => x.COUNTRY_NAME), "COUNTRY_ID", "COUNTRY_NAME").ToList();
                IndividualAddressModel.StateofResidence = new SelectList(_db.SRC_CDMA_STATE.OrderBy(x => x.STATE_NAME), "STATE_ID", "STATE_NAME").ToList();
                IndividualAddressModel.LGAofResidence = new SelectList(_db.SRC_CDMA_LGA.OrderBy(x => x.LGA_NAME), "LGA_ID", "LGA_NAME").ToList();
            }
        }
        #endregion Not in use
    }
}
