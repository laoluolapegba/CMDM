using CMdm.Data;
using CMdm.Data.Rbac;
using CMdm.Entities.Domain.Customer;
using CMdm.Entities.ViewModels;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
 
namespace CMdm.UI.Web.Controllers
{
    public class DynamicController : Controller
    {
        private AppDbContext db = new AppDbContext();
        public string changesComment = "";
        //public int[] index_id = new int[100];
        List<int> index_id = new List<int>();


        //List<int> termsList = new List<int>();
        // termsList.Add(value);
        // You can convert it back to an array if you would like to
        //int[] change_index = index_id.ToArray();

        public ActionResult loadlga(decimal state_id)
        {
            ViewBag.LGA = new SelectList(db.SRC_CDMA_LGA.Where(g => g.STATE_ID == state_id), "LGA_ID", "LGA_NAME");


            return PartialView("loadlga");
        }

        public CDMA_COUNTRIES countryById(decimal country_id) { 
            var countryDetails = db.CDMA_COUNTRIES.Where(a => a.COUNTRY_ID == country_id).FirstOrDefault();
            return countryDetails;
        }

        public CDMA_IDENTIFICATION_TYPE IdType(decimal id_type)
        {
            var idTypeDetails = db.CDMA_IDENTIFICATION_TYPE.Where(a => a.CODE == id_type).FirstOrDefault();
            return idTypeDetails;
        }

        public SRC_CDMA_LGA getloadlga(decimal lga_id)
        {
            SRC_CDMA_LGA lgaDetails = db.SRC_CDMA_LGA.Where(a => a.LGA_ID == lga_id).FirstOrDefault();
            return lgaDetails;
        }

        public SRC_CDMA_STATE getState(decimal state_id)
        {
            SRC_CDMA_STATE stateDetails = db.SRC_CDMA_STATE.Where(a => a.STATE_ID == state_id).FirstOrDefault();
            return stateDetails;
        }

        public CDMA_ACCOUNT_TYPE getAccountTypeName(decimal id) {

            CDMA_ACCOUNT_TYPE accType = db.CDMA_ACCOUNT_TYPE.Where(a => a.ACCOUNT_ID == id).FirstOrDefault();
            return accType;
        }

        public CDMA_RELIGION getRel(decimal rel_id)
        {
            CDMA_RELIGION relDetails = db.CDMA_RELIGION.Where(a => a.CODE == rel_id).FirstOrDefault();
            return relDetails;
        }

        public CM_USER_PROFILE getUserDetails(decimal user_id)
        {
            CM_USER_PROFILE detail = db.CM_USER_PROFILE.Where(a => a.PROFILE_ID == user_id).FirstOrDefault();
             
            return detail;
        }

        public CM_BRANCH getBranch(string id)
        {
            //var record = Convert.ToDecimal(id);
            CM_BRANCH detail = db.CM_BRANCH.Where(a => a.BRANCH_ID == id).FirstOrDefault();

            if (detail == null)
            {

            CM_BRANCH details = db.CM_BRANCH.Where(a => a.BRANCH_NAME == id).FirstOrDefault();
                      return details;
            }
            else
            {
             
            return detail;

            }

            
        }

        public CDMA_BRANCH_CLASS getBranchClass(string id)
        {
            CDMA_BRANCH_CLASS detail = db.CDMA_BRANCH_CLASS.Where(a => a.ID == Convert.ToDecimal(id)).FirstOrDefault();

            return detail;
        }
       
        public CDMA_BUSINESS_SEGMENT getBusSegment(decimal id)
        {
            CDMA_BUSINESS_SEGMENT detail = db.CDMA_BUSINESS_SEGMENT.Where(a => a.ID == id).FirstOrDefault();

            return detail;
        }

        public CDMA_BUSINESS_SIZE getBusinessSize(string id)
        {
            var size_id = Convert.ToDecimal(id);
            CDMA_BUSINESS_SIZE detail = db.CDMA_BUSINESS_SIZE.Where(a => a.SIZE_ID == size_id).FirstOrDefault();

            return detail;
        }


        public string confirmYesNo(string n)
        {
            if (n == "Y")
            {
                return "Yes";
            }
            if (n == "N")
            {
                return "No";
            }

            return "Nil";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult approveAccInfodata(DynamicViewModel DynamicModel)
        {
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var dateAndTime = DateTime.Now;
            var c_id = DynamicModel.BioData.CUSTOMER_NO;//Request["customer_no"];


            //create a log before update
            string tied = DateTime.Now.ToString("hhmmssffffff");
            //var values = Request["AccInfo.CUSTOMER_NO"];
            CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO = db.CDMA_ACCOUNT_INFO.SingleOrDefault(c => c.CUSTOMER_NO == DynamicModel.BioData.CUSTOMER_NO);
            CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED = db.CDMA_ACCT_SERVICES_REQUIRED.SingleOrDefault(c => c.CUSTOMER_NO == DynamicModel.BioData.CUSTOMER_NO);
            CDMA_INDIVIDUAL_BIO_DATA biorecord = db.CDMA_INDIVIDUAL_BIO_DATA.SingleOrDefault(c => c.CUSTOMER_NO == DynamicModel.BioData.CUSTOMER_NO);



   
               cDMA_ACCOUNT_INFO.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];                
               cDMA_ACCOUNT_INFO.LAST_MODIFIED_DATE = dateAndTime;

                cDMA_ACCOUNT_INFO.AUTHORISED_BY = identity.ProfileId.ToString();
                cDMA_ACCOUNT_INFO.AUTHORISED_DATE = dateAndTime;// DynamicModel.AccInfo.BRANCH;
                cDMA_ACCOUNT_INFO.AUTHORISED = "A";
                db.SaveChanges();


                cDMA_ACCT_SERVICES_REQUIRED.AUTHORISED_BY = identity.ProfileId.ToString();               
               cDMA_ACCT_SERVICES_REQUIRED.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_ACCT_SERVICES_REQUIRED.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_ACCT_SERVICES_REQUIRED.AUTHORISED_DATE = dateAndTime;
                cDMA_ACCT_SERVICES_REQUIRED.AUTHORISED = "A";
                db.SaveChanges();
            return PartialView("SaveBioData");



        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult validatedata(DynamicViewModel DynamicModel)
        {
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var dateAndTime = DateTime.Now;
            var c_id = DynamicModel.BioData.CUSTOMER_NO;

            //create a log before update
            string tied = DateTime.Now.ToString("hhmmssffffff");

 


            CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.Find(c_id);
            CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Find(c_id);
            CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Find(c_id);
            CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.Find(c_id);
            CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Find(c_id);

            //Address details
            //  var authrizer = DynamicModel.AddressDetails.AUTHORISED;
             //var AUTHORISED_BY = DynamicModel.AddressDetails.AUTHORISED_BY;
              // var AUTHORISED_DATE = DynamicModel.AddressDetails.AUTHORISED_DATE;
            // checked if address record does not exist 
            if (cDMA_INDIVIDUAL_ADDRESS_DETAIL == null)
            {
                CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE = new CDMA_INDIVIDUAL_ADDRESS_DETAIL();
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.CREATED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.CREATED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.AUTHORISED = "U";
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.LGA_OF_RESIDENCE = retrunValue(Request["LGA"]); // DynamicModel.AddressDetails.LGA_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.NEAREST_BUS_STOP_LANDMARK = retrunValue(DynamicModel.AddressDetails.NEAREST_BUS_STOP_LANDMARK);
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.RESIDENCE_OWNED_OR_RENT = retrunValue(Request["RESIDENCE_OWNED_OR_RENT"]);
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.RESIDENTIAL_ADDRESS = retrunValue(DynamicModel.AddressDetails.RESIDENTIAL_ADDRESS);
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.STATE_OF_RESIDENCE = retrunValue(Request["STATES_RES"]);  //DynamicModel.AddressDetails.STATE_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.ZIP_POSTAL_CODE = retrunValue(DynamicModel.AddressDetails.ZIP_POSTAL_CODE);
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.RESIDENCE_OWNED_OR_RENT = retrunValue(DynamicModel.AddressDetails.RESIDENCE_OWNED_OR_RENT);

                db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Add(cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE);
                db.SaveChanges();
            }
            else
            {
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.CITY_TOWN_OF_RESIDENCE = DynamicModel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.COUNTRY_OF_RESIDENCE = retrunValue(Request["COUNTRY_OF_RESIDENCE"]); // DynamicModel.AddressDetails.COUNTRY_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.CUSTOMER_NO = retrunValue(DynamicModel.BioData.CUSTOMER_NO);
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.LGA_OF_RESIDENCE = retrunValue(Request["LGA"]); // DynamicModel.AddressDetails.LGA_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.NEAREST_BUS_STOP_LANDMARK = retrunValue(DynamicModel.AddressDetails.NEAREST_BUS_STOP_LANDMARK);
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.RESIDENCE_OWNED_OR_RENT = retrunValue(Request["RESIDENCE_OWNED_OR_RENT"]);
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.RESIDENTIAL_ADDRESS = retrunValue(DynamicModel.AddressDetails.RESIDENTIAL_ADDRESS);
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.STATE_OF_RESIDENCE = retrunValue(Request["STATES_RES"]);  //DynamicModel.AddressDetails.STATE_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.ZIP_POSTAL_CODE = retrunValue(DynamicModel.AddressDetails.ZIP_POSTAL_CODE);
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.RESIDENCE_OWNED_OR_RENT = retrunValue(DynamicModel.AddressDetails.RESIDENCE_OWNED_OR_RENT);
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.AUTHORISED = "U";
                db.SaveChanges();


                var address = (CDMA_INDIVIDUAL_ADDRESS_DETAIL)this.Session["cDMA_INDIVIDUAL_ADDRESS_DETAIL"];
                // compare the Old record with the new one and get comment
                var address_changes = compareAddress(cDMA_INDIVIDUAL_ADDRESS_DETAIL, address);
                changesComment = changesComment + address_changes;
                // log address details
                CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG address_log = ConvertToAddressLog(address);
                address_log.TIED = tied;
                db.CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.Add(address_log);
                db.SaveChanges();


            }

            // Biodata            
            //cDMA_INDIVIDUAL_BIO_DATA.AGE = DynamicModel.BioData.AGE;
            //var AUTHORISED = DynamicModel.BioData.AUTHORISED;
            //var BIO_AUTHORISED_BY = DynamicModel.BioData.AUTHORISED_BY;
            // var BIO_AUTHORISED_DATE = DynamicModel.BioData.AUTHORISED_DATE;
            cDMA_INDIVIDUAL_BIO_DATA.BRANCH_CODE = Request["BRANCH"];  //DynamicModel.BioData.BRANCH_CODE;
            //cDMA_INDIVIDUAL_BIO_DATA.COMPLEXION = DynamicModel.BioData.COMPLEXION;
            var COUNTRY_OF_BIRTH = Request["COUNTRY_OF_BIRTH"]; // DynamicModel.BioData.COUNTRY_OF_BIRTH;
                                                                // var BIO_CREATED_BY = DynamicModel.BioData.CREATED_BY;
                                                                // var BIO_CREATED_DATE = DynamicModel.BioData.CREATED_DATE;
            cDMA_INDIVIDUAL_BIO_DATA.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
            cDMA_INDIVIDUAL_BIO_DATA.DATE_OF_BIRTH = DynamicModel.BioData.DATE_OF_BIRTH;
            //cDMA_INDIVIDUAL_BIO_DATA.DISABILITY = retrunValue(DynamicModel.BioData.DISABILITY);
            cDMA_INDIVIDUAL_BIO_DATA.FIRST_NAME = DynamicModel.BioData.FIRST_NAME;
            cDMA_INDIVIDUAL_BIO_DATA.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
            cDMA_INDIVIDUAL_BIO_DATA.LAST_MODIFIED_DATE = dateAndTime;//DynamicModel.BioData.LAST_MODIFIED_DATE;
            cDMA_INDIVIDUAL_BIO_DATA.MARITAL_STATUS = DynamicModel.BioData.MARITAL_STATUS;
            cDMA_INDIVIDUAL_BIO_DATA.MOTHER_MAIDEN_NAME = DynamicModel.BioData.MOTHER_MAIDEN_NAME;
            cDMA_INDIVIDUAL_BIO_DATA.NATIONALITY = Request["BioData_NATIONALITY"];  //// DynamicModel.BioData.NATIONALITY;
            //cDMA_INDIVIDUAL_BIO_DATA.NICKNAME_ALIAS = DynamicModel.BioData.NICKNAME_ALIAS;
            //cDMA_INDIVIDUAL_BIO_DATA.NUMBER_OF_CHILDREN = DynamicModel.BioData.NUMBER_OF_CHILDREN;
            cDMA_INDIVIDUAL_BIO_DATA.OTHER_NAME = DynamicModel.BioData.OTHER_NAME;
            //cDMA_INDIVIDUAL_BIO_DATA.PLACE_OF_BIRTH = DynamicModel.BioData.PLACE_OF_BIRTH;
            cDMA_INDIVIDUAL_BIO_DATA.RELIGION = DynamicModel.BioData.RELIGION;
            cDMA_INDIVIDUAL_BIO_DATA.SEX = DynamicModel.BioData.SEX;
            cDMA_INDIVIDUAL_BIO_DATA.STATE_OF_ORIGIN = DynamicModel.BioData.STATE_OF_ORIGIN;
            cDMA_INDIVIDUAL_BIO_DATA.SURNAME = DynamicModel.BioData.SURNAME;
            cDMA_INDIVIDUAL_BIO_DATA.TITLE = DynamicModel.BioData.TITLE;
            cDMA_INDIVIDUAL_BIO_DATA.RELIGION = Request["RELIGION"];
            cDMA_INDIVIDUAL_BIO_DATA.COUNTRY_OF_BIRTH = Request["COUNTRY_OF_BIRTH"];
            cDMA_INDIVIDUAL_BIO_DATA.LAST_MODIFIED_BY = identity.ProfileId.ToString();
            cDMA_INDIVIDUAL_BIO_DATA.AUTHORISED = "U";
            db.SaveChanges();

            CDMA_INDIVIDUAL_BIO_DATA bio_data = (CDMA_INDIVIDUAL_BIO_DATA)this.Session["cDMA_INDIVIDUAL_BIO_DATA"];
            // compare old bio record with the new bio record
            var biodata_changes = compareBioRecord(cDMA_INDIVIDUAL_BIO_DATA, bio_data);
            changesComment = changesComment + biodata_changes;


            CDMA_INDIVIDUAL_BIO_LOG biodata_log = ConvertToBioDataLog(bio_data);
            biodata_log.TIED = tied;
            db.CDMA_INDIVIDUAL_BIO_LOG.Add(biodata_log);
            db.SaveChanges();


            //contact	 
            //var CONTACT_AUTHORISED =  DynamicModel.contact.AUTHORISED;
            // var CONTACT_AUTHORISED_BY = DynamicModel.contact.AUTHORISED_BY;
            //  var CONTACT_AUTHORISED_DATE = DynamicModel.contact.AUTHORISED_DATE;
            //  var CONTACT_CREATED_BY = DynamicModel.contact.CREATED_BY;
            //  var CONTACT_CREATED_DATE = DynamicModel.contact.CREATED_DATE;
            //  var CONTACT_CUSTOMER_NO = DynamicModel.contact.CUSTOMER_NO;
            if (cDMA_INDIVIDUAL_CONTACT_DETAIL == null)
            {
                CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE = new CDMA_INDIVIDUAL_CONTACT_DETAIL();
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.CREATED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.CREATED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.MAILING_ADDRESS = DynamicModel.contact.MAILING_ADDRESS;
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.MOBILE_NO = DynamicModel.contact.MOBILE_NO;
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.EMAIL_ADDRESS = DynamicModel.contact.EMAIL_ADDRESS;
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.AUTHORISED = "U";
                db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Add(cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE);
                db.SaveChanges();

            }
            else
            {
                cDMA_INDIVIDUAL_CONTACT_DETAIL.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_CONTACT_DETAIL.MAILING_ADDRESS = DynamicModel.contact.MAILING_ADDRESS;
                cDMA_INDIVIDUAL_CONTACT_DETAIL.MOBILE_NO = DynamicModel.contact.MOBILE_NO;
                cDMA_INDIVIDUAL_CONTACT_DETAIL.EMAIL_ADDRESS = DynamicModel.contact.EMAIL_ADDRESS;
                cDMA_INDIVIDUAL_CONTACT_DETAIL.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_CONTACT_DETAIL.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_CONTACT_DETAIL.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_CONTACT_DETAIL.AUTHORISED = "U";
                db.SaveChanges();
                CDMA_INDIVIDUAL_CONTACT_DETAIL contact_data = (CDMA_INDIVIDUAL_CONTACT_DETAIL)this.Session["cDMA_INDIVIDUAL_CONTACT_DETAIL"];

                var contact_changes = compareContactRecord(cDMA_INDIVIDUAL_CONTACT_DETAIL, contact_data);
                changesComment = changesComment + contact_changes;

                CDMA_INDIVIDUAL_CONTACT_LOG contact_log = ConvertToContactDataLog(contact_data);
                contact_log.TIED = tied;
                db.CDMA_INDIVIDUAL_CONTACT_LOG.Add(contact_log);
                db.SaveChanges();
            }



            // identification 
            if (cDMA_INDIVIDUAL_IDENTIFICATION == null)
            {
                CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION_SAVE = new CDMA_INDIVIDUAL_IDENTIFICATION();
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.CREATED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.CREATED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.IDENTIFICATION_TYPE = Request["CDMA_IDENTIFICATION_TYPE"];
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.ID_EXPIRY_DATE = DynamicModel.identification.ID_EXPIRY_DATE;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.ID_ISSUE_DATE = DynamicModel.identification.ID_ISSUE_DATE;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.ID_NO = DynamicModel.identification.ID_NO;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.LAST_MODIFIED_BY = DynamicModel.identification.LAST_MODIFIED_BY;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.LAST_MODIFIED_DATE = dateAndTime;
                //cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.PLACE_OF_ISSUANCE = DynamicModel.identification.PLACE_OF_ISSUANCE;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.AUTHORISED = "U";
                db.CDMA_INDIVIDUAL_IDENTIFICATION.Add(cDMA_INDIVIDUAL_IDENTIFICATION_SAVE);
                db.SaveChanges();

            }
            else
            {
                //var identification_AUTHORISED =  DynamicModel.identification.AUTHORISED;
                //var identification_AUTHORISED_BY = DynamicModel.identification.AUTHORISED_BY;
                // var identification_AUTHORISED_DATE = DynamicModel.identification.AUTHORISED_DATE;
                //var identification_CREATED_BY = DynamicModel.identification.CREATED_BY;
                // var identification_CREATED_DATE = DynamicModel.identification.CREATED_DATE;
                cDMA_INDIVIDUAL_IDENTIFICATION.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                cDMA_INDIVIDUAL_IDENTIFICATION.IDENTIFICATION_TYPE = Request["CDMA_IDENTIFICATION_TYPE"];
                cDMA_INDIVIDUAL_IDENTIFICATION.ID_EXPIRY_DATE = DynamicModel.identification.ID_EXPIRY_DATE;
                cDMA_INDIVIDUAL_IDENTIFICATION.ID_ISSUE_DATE = DynamicModel.identification.ID_ISSUE_DATE;
                cDMA_INDIVIDUAL_IDENTIFICATION.ID_NO = DynamicModel.identification.ID_NO;
                cDMA_INDIVIDUAL_IDENTIFICATION.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_IDENTIFICATION.LAST_MODIFIED_BY = DynamicModel.identification.LAST_MODIFIED_BY;
                cDMA_INDIVIDUAL_IDENTIFICATION.LAST_MODIFIED_DATE = dateAndTime;
                //cDMA_INDIVIDUAL_IDENTIFICATION.PLACE_OF_ISSUANCE = DynamicModel.identification.PLACE_OF_ISSUANCE;
                cDMA_INDIVIDUAL_IDENTIFICATION.AUTHORISED = "U";
                CDMA_INDIVIDUAL_IDENTIFICATION identification_data = (CDMA_INDIVIDUAL_IDENTIFICATION)this.Session["cDMA_INDIVIDUAL_IDENTIFICATION"];


                var identification_changes = compareIdentificationRecord(cDMA_INDIVIDUAL_IDENTIFICATION, identification_data);
                changesComment = changesComment + identification_changes;
                db.SaveChanges();

                CDMA_INDIVIDUAL_IDENTIFICATION_LOG identification_log = ConvertToIdentificationLog(identification_data);
                identification_log.TIED = tied;
                db.CDMA_INDIVIDUAL_IDENTIFICATION_LOG.Add(identification_log);
                db.SaveChanges();



            }



            //otherdetails	 

            if (cDMA_INDIVIDUAL_OTHER_DETAILS == null)
            {
                CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE = new CDMA_INDIVIDUAL_OTHER_DETAILS();
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.CREATED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.CREATED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.TIN_NO = DynamicModel.otherdetails.TIN_NO;
                cDMA_INDIVIDUAL_OTHER_DETAILS.AUTHORISED = "U";
                db.CDMA_INDIVIDUAL_OTHER_DETAILS.Add(cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE);
                db.SaveChanges();
            }
            else
            {
                //  var otherdetails_PLACE_OF_ISSUANCE = DynamicModel.otherdetails.AUTHORISED;
                // var otherdetails_PLACE_OF_AUTHORISED_BY = DynamicModel.otherdetails.AUTHORISED_BY;
                // var otherdetails_PLACE_OF_AUTHORISED_DATE = DynamicModel.otherdetails.AUTHORISED_DATE;
                //  var otherdetails_PLACE_OF_CREATED_BY = DynamicModel.otherdetails.CREATED_BY;
                // var otherdetails_PLACE_OF_CREATED_DATE = DynamicModel.otherdetails.CREATED_DATE;
                cDMA_INDIVIDUAL_OTHER_DETAILS.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                cDMA_INDIVIDUAL_OTHER_DETAILS.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_OTHER_DETAILS.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_OTHER_DETAILS.TIN_NO = DynamicModel.otherdetails.TIN_NO;
                cDMA_INDIVIDUAL_OTHER_DETAILS.AUTHORISED = "U";
                db.SaveChanges();
                CDMA_INDIVIDUAL_OTHER_DETAILS other_data = (CDMA_INDIVIDUAL_OTHER_DETAILS)this.Session["cDMA_INDIVIDUAL_OTHER_DETAILS"];

                var identification_changes = compareOtherRecord(cDMA_INDIVIDUAL_OTHER_DETAILS, other_data);
                changesComment = changesComment + identification_changes;

                CDMA_INDIVIDUAL_OTHER_DETAILS_LOG other_log = ConvertToOtherLog(other_data);
                other_log.TIED = tied;
                db.CDMA_INDIVIDUAL_OTHER_DETAILS_LOG.Add(other_log);
                db.SaveChanges();



            }



            CDMA_INDIVIDUAL_PROFILE_LOG SAVE_BIODATA_LOG = new CDMA_INDIVIDUAL_PROFILE_LOG();
            SAVE_BIODATA_LOG.AFFECTED_CATEGORY = this.Session["category"].ToString();
            SAVE_BIODATA_LOG.CUSTOMER_NO = c_id;
            SAVE_BIODATA_LOG.LOGGED_BY = Convert.ToDecimal(identity.ProfileId);
            SAVE_BIODATA_LOG.LOGGED_DATE = dateAndTime;
            SAVE_BIODATA_LOG.TIED = tied;
            SAVE_BIODATA_LOG.COMMENTS = changesComment;
            string change_index = String.Join(", ", index_id);
            //int[] change_index = index_id.ToArray();
            SAVE_BIODATA_LOG.CHANGE_INDEX = change_index.ToString();
            db.CDMA_INDIVIDUAL_PROFILE_LOG.Add(SAVE_BIODATA_LOG);
            db.SaveChanges();




            return PartialView("SaveBioData", DynamicModel);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveBioData(DynamicViewModel DynamicModel)
        {
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var dateAndTime = DateTime.Now;
            var c_id = DynamicModel.BioData.CUSTOMER_NO;

            //create a log before update
            string tied = DateTime.Now.ToString("hhmmssffffff");

 

            CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.Find(c_id);
            CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Find(c_id);
            CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Find(c_id);
            CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.Find(c_id);
            CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Find(c_id);

            //Address details
            //  var authrizer = DynamicModel.AddressDetails.AUTHORISED;
            // var AUTHORISED_BY = DynamicModel.AddressDetails.AUTHORISED_BY;
            //   var AUTHORISED_DATE = DynamicModel.AddressDetails.AUTHORISED_DATE;
            // checked if address record does not exist 
            if (!(cDMA_INDIVIDUAL_ADDRESS_DETAIL == null))
            {
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.AUTHORISED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.AUTHORISED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.AUTHORISED = "A";
                db.SaveChanges(); 

            }

            // Biodata            
                       
            cDMA_INDIVIDUAL_BIO_DATA.AUTHORISED_BY = identity.ProfileId.ToString();           
            cDMA_INDIVIDUAL_BIO_DATA.AUTHORISED = "A";
            cDMA_INDIVIDUAL_BIO_DATA.AUTHORISED_DATE = dateAndTime;          
            db.SaveChanges();

          

            //contact	 
            //var CONTACT_AUTHORISED =  DynamicModel.contact.AUTHORISED;
            // var CONTACT_AUTHORISED_BY = DynamicModel.contact.AUTHORISED_BY;
            //  var CONTACT_AUTHORISED_DATE = DynamicModel.contact.AUTHORISED_DATE;
            //  var CONTACT_CREATED_BY = DynamicModel.contact.CREATED_BY;
            //  var CONTACT_CREATED_DATE = DynamicModel.contact.CREATED_DATE;
            //  var CONTACT_CUSTOMER_NO = DynamicModel.contact.CUSTOMER_NO;
            if (!(cDMA_INDIVIDUAL_CONTACT_DETAIL == null))           
            {
                cDMA_INDIVIDUAL_CONTACT_DETAIL.AUTHORISED = "A";
                cDMA_INDIVIDUAL_CONTACT_DETAIL.AUTHORISED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_CONTACT_DETAIL.AUTHORISED_DATE = dateAndTime;
                db.SaveChanges();
                
            }



            // identification 
            if (!(cDMA_INDIVIDUAL_IDENTIFICATION == null))
               {

                cDMA_INDIVIDUAL_IDENTIFICATION.AUTHORISED = "A";
                cDMA_INDIVIDUAL_IDENTIFICATION.AUTHORISED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_IDENTIFICATION.AUTHORISED_DATE = dateAndTime;

                db.SaveChanges();

            }



            //otherdetails	 

            if (!(cDMA_INDIVIDUAL_OTHER_DETAILS == null))
            {
                cDMA_INDIVIDUAL_OTHER_DETAILS.AUTHORISED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_OTHER_DETAILS.AUTHORISED = "A";
                cDMA_INDIVIDUAL_OTHER_DETAILS.AUTHORISED_BY = identity.ProfileId.ToString();
                db.SaveChanges();           

            }

            return PartialView("SaveBioData", DynamicModel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisapproveBioData(DynamicViewModel DynamicModel)
        {
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var dateAndTime = DateTime.Now;
            var c_id = DynamicModel.BioData.CUSTOMER_NO;

            //create a log before update
            string tied = DateTime.Now.ToString("hhmmssffffff");

            CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.Find(c_id);
            CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Find(c_id);
            CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Find(c_id);
            CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.Find(c_id);
            CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Find(c_id);

            //Address details
            //  var authrizer = DynamicModel.AddressDetails.AUTHORISED;
            // var AUTHORISED_BY = DynamicModel.AddressDetails.AUTHORISED_BY;
            //   var AUTHORISED_DATE = DynamicModel.AddressDetails.AUTHORISED_DATE;
            // checked if address record does not exist 
            if (!(cDMA_INDIVIDUAL_ADDRESS_DETAIL == null))
            {
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.AUTHORISED_BY = null;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.AUTHORISED_DATE = null;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL.AUTHORISED = "N";
                db.SaveChanges();
            }

            // Biodata           
            cDMA_INDIVIDUAL_BIO_DATA.AUTHORISED_BY = null;
            cDMA_INDIVIDUAL_BIO_DATA.AUTHORISED = "N";
            cDMA_INDIVIDUAL_BIO_DATA.AUTHORISED_DATE = null;
            db.SaveChanges();
            
            if (!(cDMA_INDIVIDUAL_CONTACT_DETAIL == null))
            {
                cDMA_INDIVIDUAL_CONTACT_DETAIL.AUTHORISED = "N";
                cDMA_INDIVIDUAL_CONTACT_DETAIL.AUTHORISED_BY = null;
                cDMA_INDIVIDUAL_CONTACT_DETAIL.AUTHORISED_DATE = null;
                db.SaveChanges();
            }
            
            // identification 
            if (!(cDMA_INDIVIDUAL_IDENTIFICATION == null))
            {
                cDMA_INDIVIDUAL_IDENTIFICATION.AUTHORISED = "N";
                cDMA_INDIVIDUAL_IDENTIFICATION.AUTHORISED_BY = null;
                cDMA_INDIVIDUAL_IDENTIFICATION.AUTHORISED_DATE = null;

                db.SaveChanges();
            }

            //otherdetails	 s
            if (!(cDMA_INDIVIDUAL_OTHER_DETAILS == null))
            {
                cDMA_INDIVIDUAL_OTHER_DETAILS.AUTHORISED_DATE = null;
                cDMA_INDIVIDUAL_OTHER_DETAILS.AUTHORISED = "N";
                cDMA_INDIVIDUAL_OTHER_DETAILS.AUTHORISED_BY = null;
                db.SaveChanges();
            }

            return PartialView("SaveBioData", DynamicModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAccInfo(DynamicViewModel DynamicModel) {
            
                var identity = ((CustomPrincipal)User).CustomIdentity;
                var dateAndTime = DateTime.Now;
                var c_id = Request["customer_no"];
 

                //create a log before update
                string tied = DateTime.Now.ToString("hhmmssffffff");
                //var values = Request["AccInfo.CUSTOMER_NO"];
                CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO = db.CDMA_ACCOUNT_INFO.SingleOrDefault(c => c.CUSTOMER_NO == DynamicModel.AccInfo.CUSTOMER_NO);
                CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED = db.CDMA_ACCT_SERVICES_REQUIRED.SingleOrDefault(c => c.CUSTOMER_NO == DynamicModel.AccInfo.CUSTOMER_NO);
                CDMA_INDIVIDUAL_BIO_DATA biorecord = db.CDMA_INDIVIDUAL_BIO_DATA.SingleOrDefault(c => c.CUSTOMER_NO == DynamicModel.AccInfo.CUSTOMER_NO);

 

            if (cDMA_ACCOUNT_INFO == null)
            {
                CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO_SAVE = new CDMA_ACCOUNT_INFO();
                cDMA_ACCOUNT_INFO_SAVE.CREATED_BY = identity.ProfileId.ToString();
                cDMA_ACCOUNT_INFO_SAVE.CREATED_DATE = dateAndTime;
                cDMA_ACCOUNT_INFO_SAVE.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_ACCOUNT_INFO_SAVE.CUSTOMER_NO = c_id;

                cDMA_ACCOUNT_INFO_SAVE.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                cDMA_ACCOUNT_INFO_SAVE.LAST_MODIFIED_DATE = dateAndTime;

                cDMA_ACCOUNT_INFO_SAVE.TYPE_OF_ACCOUNT = Request["TYPE_OF_ACCOUNT"];// DynamicModel.AccInfo.TYPE_OF_ACCOUNT;
                cDMA_ACCOUNT_INFO_SAVE.BRANCH = Request["BRANCH"];// DynamicModel.AccInfo.BRANCH;
                cDMA_ACCOUNT_INFO_SAVE.BRANCH_CLASS = Request["BranchClass"]; //DynamicModel.AccInfo.BRANCH_CLASS;
                cDMA_ACCOUNT_INFO_SAVE.BUSINESS_DIVISION = Request["BUSINESSDIVISION"];//DynamicModel.AccInfo.BUSINESS_DIVISION;
                cDMA_ACCOUNT_INFO_SAVE.BUSINESS_SEGMENT = Request["BUSINESS_SEGMENT"];//DynamicModel.AccInfo.BUSINESS_SEGMENT;
                cDMA_ACCOUNT_INFO_SAVE.BUSINESS_SIZE = Request["BusinessSize"]; //DynamicModel.AccInfo.BUSINESS_SIZE;
                cDMA_ACCOUNT_INFO_SAVE.BVN_NUMBER = DynamicModel.AccInfo.BVN_NUMBER;
                cDMA_ACCOUNT_INFO_SAVE.CAV_REQUIRED = DynamicModel.AccInfo.CAV_REQUIRED;
                //cDMA_ACCOUNT_INFO_SAVE.CHEQUE_CONFIRM_THRESHLDRANGE = DynamicModel.AccInfo.CHEQUE_CONFIRM_THRESHLDRANGE;
                //cDMA_ACCOUNT_INFO_SAVE.ONLINE_TRANSFER_LIMIT_RANGE = Request["RangeLimit"];//DynamicModel.AccInfo.ONLINE_TRANSFER_LIMIT_RANGE;
                cDMA_ACCOUNT_INFO_SAVE.CUSTOMER_IC = DynamicModel.AccInfo.CUSTOMER_IC;
                cDMA_ACCOUNT_INFO_SAVE.CUSTOMER_SEGMENT = Request["CustomerSegment"]; //DynamicModel.AccInfo.CUSTOMER_SEGMENT;
                cDMA_ACCOUNT_INFO_SAVE.CUSTOMER_TYPE = Request["CustomerType"];//DynamicModel.AccInfo.CUSTOMER_TYPE;
                cDMA_ACCOUNT_INFO_SAVE.OPERATING_INSTRUCTION = DynamicModel.AccInfo.OPERATING_INSTRUCTION;
                cDMA_ACCOUNT_INFO_SAVE.ORIGINATING_BRANCH = Request["OringinBranch"];//DynamicModel.AccInfo.ORIGINATING_BRANCH;

                cDMA_ACCOUNT_INFO_SAVE.AUTHORISED = "U";


                db.CDMA_ACCOUNT_INFO.Add(cDMA_ACCOUNT_INFO_SAVE);
                db.SaveChanges();
            }
            else
            {
                cDMA_ACCOUNT_INFO.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_ACCOUNT_INFO.CUSTOMER_NO = DynamicModel.AccInfo.CUSTOMER_NO;

                cDMA_ACCOUNT_INFO.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                cDMA_ACCOUNT_INFO.LAST_MODIFIED_DATE = dateAndTime;

                cDMA_ACCOUNT_INFO.TYPE_OF_ACCOUNT = Request["TYPE_OF_ACCOUNT"];// DynamicModel.AccInfo.TYPE_OF_ACCOUNT;
                cDMA_ACCOUNT_INFO.BRANCH = Request["BRANCH"];// DynamicModel.AccInfo.BRANCH;
                cDMA_ACCOUNT_INFO.BRANCH_CLASS = Request["BranchClass"]; //DynamicModel.AccInfo.BRANCH_CLASS;
                cDMA_ACCOUNT_INFO.BUSINESS_DIVISION = Request["BUSINESSDIVISION"] ;//DynamicModel.AccInfo.BUSINESS_DIVISION;
                cDMA_ACCOUNT_INFO.BUSINESS_SEGMENT = Request["BUSINESS_SEGMENT"] ;//DynamicModel.AccInfo.BUSINESS_SEGMENT;
                cDMA_ACCOUNT_INFO.BUSINESS_SIZE = Request["BusinessSize"]; //DynamicModel.AccInfo.BUSINESS_SIZE;
                cDMA_ACCOUNT_INFO.BVN_NUMBER = DynamicModel.AccInfo.BVN_NUMBER;
                cDMA_ACCOUNT_INFO.ACCOUNT_TITLE = DynamicModel.AccInfo.ACCOUNT_TITLE;
                cDMA_ACCOUNT_INFO.ACCOUNT_OFFICER = DynamicModel.AccInfo.ACCOUNT_OFFICER;
                cDMA_ACCOUNT_INFO.CAV_REQUIRED = DynamicModel.AccInfo.CAV_REQUIRED;
                //cDMA_ACCOUNT_INFO.CHEQUE_CONFIRM_THRESHLDRANGE = DynamicModel.AccInfo.CHEQUE_CONFIRM_THRESHLDRANGE;
                //cDMA_ACCOUNT_INFO.ONLINE_TRANSFER_LIMIT_RANGE = Request["RangeLimit"] ;//DynamicModel.AccInfo.ONLINE_TRANSFER_LIMIT_RANGE;
                cDMA_ACCOUNT_INFO.CUSTOMER_IC = DynamicModel.AccInfo.CUSTOMER_IC;
                cDMA_ACCOUNT_INFO.CUSTOMER_SEGMENT = Request["CustomerSegment"]; //DynamicModel.AccInfo.CUSTOMER_SEGMENT;
                cDMA_ACCOUNT_INFO.CUSTOMER_TYPE = Request["CustomerType"] ;//DynamicModel.AccInfo.CUSTOMER_TYPE;
                cDMA_ACCOUNT_INFO.OPERATING_INSTRUCTION = DynamicModel.AccInfo.OPERATING_INSTRUCTION;
                cDMA_ACCOUNT_INFO.ORIGINATING_BRANCH = Request["OringinBranch"] ;//DynamicModel.AccInfo.ORIGINATING_BRANCH;
                //cDMA_ACCOUNT_INFO.ONLINE_TRANSFER_LIMIT_RANGE = DynamicModel.AccInfo.ONLINE_TRANSFER_LIMIT_RANGE;
                cDMA_ACCOUNT_INFO.AUTHORISED = "U";
                db.SaveChanges();

                CDMA_ACCOUNT_INFO accountInfo = (CDMA_ACCOUNT_INFO)this.Session["cDMA_ACCOUNT_INFO"];
                var serviceRequired = (CDMA_ACCT_SERVICES_REQUIRED)this.Session["cDMA_ACCT_SERVICES_REQUIRED"];
                // compare the Old record with the new one and get comment
                var acc_info_changes = compareAccInfo(cDMA_ACCOUNT_INFO, accountInfo);
                changesComment = changesComment + acc_info_changes;
                // log address details
                CDMA_ACCOUNT_INFO_LOG acc_info_log = ConvertToAccInfoLog(accountInfo);
                acc_info_log.TIED = tied;
                db.CDMA_ACCOUNT_INFO_LOG.Add(acc_info_log);
                db.SaveChanges();
            }


            if (cDMA_ACCT_SERVICES_REQUIRED == null)
            {
                CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED_SAVE = new CDMA_ACCT_SERVICES_REQUIRED();
   
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.CREATED_BY = identity.ProfileId.ToString();
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.CREATED_DATE = dateAndTime;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.CUSTOMER_NO = c_id;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.ACCOUNT_NUMBER = DynamicModel.AccInfo.ACCOUNT_NUMBER;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.CARD_PREFERENCE = DynamicModel.ServiceInfo.CARD_PREFERENCE;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.ELECTRONIC_BANKING_PREFERENCE = DynamicModel.ServiceInfo.ELECTRONIC_BANKING_PREFERENCE;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.STATEMENT_PREFERENCES = DynamicModel.ServiceInfo.STATEMENT_PREFERENCES;

                cDMA_ACCT_SERVICES_REQUIRED_SAVE.TRANSACTION_ALERT_PREFERENCE = DynamicModel.ServiceInfo.TRANSACTION_ALERT_PREFERENCE;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.STATEMENT_FREQUENCY = DynamicModel.ServiceInfo.STATEMENT_FREQUENCY;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.CHEQUE_BOOK_REQUISITION = DynamicModel.ServiceInfo.CHEQUE_BOOK_REQUISITION;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.CHEQUE_LEAVES_REQUIRED = DynamicModel.ServiceInfo.CHEQUE_LEAVES_REQUIRED;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.CHEQUE_CONFIRMATION = DynamicModel.ServiceInfo.CHEQUE_CONFIRMATION;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.CHEQUE_CONFIRMATION_THRESHOLD = Request["threshold"] ;//DynamicModel.ServiceInfo.CHEQUE_CONFIRMATION_THRESHOLD;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.CHEQUE_CONFIRM_THRESHOLD_RANGE = DynamicModel.ServiceInfo.CHEQUE_CONFIRM_THRESHOLD_RANGE;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.ONLINE_TRANSFER_LIMIT_RANGE = Request["RangeLimit"];//DynamicModel.AccInfo.ONLINE_TRANSFER_LIMIT_RANGE;

                cDMA_ACCT_SERVICES_REQUIRED_SAVE.ONLINE_TRANSFER_LIMIT = DynamicModel.ServiceInfo.ONLINE_TRANSFER_LIMIT;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.TOKEN = DynamicModel.ServiceInfo.TOKEN;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.ACCOUNT_SIGNATORY = DynamicModel.ServiceInfo.ACCOUNT_SIGNATORY;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.SECOND_SIGNATORY = DynamicModel.ServiceInfo.SECOND_SIGNATORY;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.AUTHORISED = DynamicModel.ServiceInfo.AUTHORISED;
                cDMA_ACCT_SERVICES_REQUIRED_SAVE.AUTHORISED = "U";

                db.CDMA_ACCT_SERVICES_REQUIRED.Add(cDMA_ACCT_SERVICES_REQUIRED_SAVE);
                db.SaveChanges();
            }
            else
            {
                 
                cDMA_ACCT_SERVICES_REQUIRED.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
             //   cDMA_ACCT_SERVICES_REQUIRED.CUSTOMER_NO = c_id; //biorecord.CUSTOMER_NO;
                cDMA_ACCT_SERVICES_REQUIRED.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_ACCT_SERVICES_REQUIRED.ACCOUNT_NUMBER = DynamicModel.AccInfo.ACCOUNT_NUMBER;
                cDMA_ACCT_SERVICES_REQUIRED.CARD_PREFERENCE = DynamicModel.ServiceInfo.CARD_PREFERENCE;
                cDMA_ACCT_SERVICES_REQUIRED.ELECTRONIC_BANKING_PREFERENCE = DynamicModel.ServiceInfo.ELECTRONIC_BANKING_PREFERENCE;
                cDMA_ACCT_SERVICES_REQUIRED.STATEMENT_PREFERENCES = DynamicModel.ServiceInfo.STATEMENT_PREFERENCES;

                cDMA_ACCT_SERVICES_REQUIRED.TRANSACTION_ALERT_PREFERENCE = DynamicModel.ServiceInfo.TRANSACTION_ALERT_PREFERENCE;
                cDMA_ACCT_SERVICES_REQUIRED.STATEMENT_FREQUENCY = DynamicModel.ServiceInfo.STATEMENT_FREQUENCY;
                cDMA_ACCT_SERVICES_REQUIRED.CHEQUE_BOOK_REQUISITION = DynamicModel.ServiceInfo.CHEQUE_BOOK_REQUISITION;
                cDMA_ACCT_SERVICES_REQUIRED.CHEQUE_LEAVES_REQUIRED = DynamicModel.ServiceInfo.CHEQUE_LEAVES_REQUIRED;
                cDMA_ACCT_SERVICES_REQUIRED.CHEQUE_CONFIRMATION = DynamicModel.ServiceInfo.CHEQUE_CONFIRMATION;
                cDMA_ACCT_SERVICES_REQUIRED.CHEQUE_CONFIRMATION_THRESHOLD = Request["threshold"]; //DynamicModel.ServiceInfo.CHEQUE_CONFIRMATION_THRESHOLD;
                cDMA_ACCT_SERVICES_REQUIRED.CHEQUE_CONFIRM_THRESHOLD_RANGE = DynamicModel.ServiceInfo.CHEQUE_CONFIRM_THRESHOLD_RANGE;
                cDMA_ACCT_SERVICES_REQUIRED.ONLINE_TRANSFER_LIMIT_RANGE = Request["RangeLimit"] ;//DynamicModel.AccInfo.ONLINE_TRANSFER_LIMIT_RANGE;

                cDMA_ACCT_SERVICES_REQUIRED.ONLINE_TRANSFER_LIMIT = DynamicModel.ServiceInfo.ONLINE_TRANSFER_LIMIT;
                cDMA_ACCT_SERVICES_REQUIRED.TOKEN = DynamicModel.ServiceInfo.TOKEN;
                cDMA_ACCT_SERVICES_REQUIRED.ACCOUNT_SIGNATORY = DynamicModel.ServiceInfo.ACCOUNT_SIGNATORY;
                cDMA_ACCT_SERVICES_REQUIRED.SECOND_SIGNATORY = DynamicModel.ServiceInfo.SECOND_SIGNATORY;
                cDMA_ACCT_SERVICES_REQUIRED.AUTHORISED = DynamicModel.ServiceInfo.AUTHORISED;
                cDMA_ACCT_SERVICES_REQUIRED.AUTHORISED = "U";
                db.SaveChanges();


                CDMA_ACCT_SERVICES_REQUIRED serviceRequired = (CDMA_ACCT_SERVICES_REQUIRED)this.Session["cDMA_ACCT_SERVICES_REQUIRED"];
                   // compare the Old record with the new one and get comment
                    var serv_req_changes = compareServiceReq(cDMA_ACCT_SERVICES_REQUIRED, serviceRequired);
                    changesComment = changesComment + serv_req_changes;
                    // log address details
                    CDMA_ACCT_SERVICES_LOG serviceRequired_log = ConvertToServiceRequiredLog(serviceRequired);
                    serviceRequired_log.TIED = tied;
                    db.CDMA_ACCT_SERVICES_LOG.Add(serviceRequired_log);
                    db.SaveChanges();
               


            }



            CDMA_INDIVIDUAL_PROFILE_LOG SAVE_ACC_INF_LOG = new CDMA_INDIVIDUAL_PROFILE_LOG();
            SAVE_ACC_INF_LOG.AFFECTED_CATEGORY = "accinfo";
            SAVE_ACC_INF_LOG.CUSTOMER_NO = c_id;
            SAVE_ACC_INF_LOG.LOGGED_BY = Convert.ToDecimal(identity.ProfileId);
            SAVE_ACC_INF_LOG.LOGGED_DATE = dateAndTime;
            SAVE_ACC_INF_LOG.TIED = tied;
            SAVE_ACC_INF_LOG.COMMENTS = changesComment;
            string change_index = String.Join(", ", index_id);
            //int[] change_index = index_id.ToArray();
            SAVE_ACC_INF_LOG.CHANGE_INDEX = change_index.ToString();
            db.CDMA_INDIVIDUAL_PROFILE_LOG.Add(SAVE_ACC_INF_LOG);
            db.SaveChanges();

            return PartialView("SaveBioData", DynamicModel);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBioData(DynamicViewModel DynamicModel) {
            var identity = ((CustomPrincipal)User).CustomIdentity;
            var dateAndTime = DateTime.Now;
            var c_id = DynamicModel.BioData.CUSTOMER_NO;

            //create a log before update
            string tied = DateTime.Now.ToString("hhmmssffffff");
           

            /*
            
           // log Individual Address Details
            var address = (CDMA_INDIVIDUAL_ADDRESS_DETAIL) this.Session["cDMA_INDIVIDUAL_ADDRESS_DETAIL"];
            CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG address_log = ConvertToAddressLog(address);
            address_log.TIED = tied;
            db.CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.Add(address_log);
            db.SaveChanges();

            // log Biodata Details
            CDMA_INDIVIDUAL_BIO_DATA bio_data = (CDMA_INDIVIDUAL_BIO_DATA)this.Session["cDMA_INDIVIDUAL_BIO_DATA"];
            CDMA_INDIVIDUAL_BIO_LOG biodata_log = ConvertToBioDataLog(bio_data);
            biodata_log.TIED = tied;
            db.CDMA_INDIVIDUAL_BIO_LOG.Add(biodata_log);
            db.SaveChanges();

            // log contact Details
            CDMA_INDIVIDUAL_CONTACT_DETAIL contact_data = (CDMA_INDIVIDUAL_CONTACT_DETAIL)this.Session["cDMA_INDIVIDUAL_CONTACT_DETAIL"];
            CDMA_INDIVIDUAL_CONTACT_LOG contact_log = ConvertToContactDataLog(contact_data);
            contact_log.TIED = tied;
            db.CDMA_INDIVIDUAL_CONTACT_LOG.Add(contact_log);
            db.SaveChanges();

            // identification
            CDMA_INDIVIDUAL_IDENTIFICATION identification_data = (CDMA_INDIVIDUAL_IDENTIFICATION)this.Session["cDMA_INDIVIDUAL_IDENTIFICATION"];
            CDMA_INDIVIDUAL_IDENTIFICATION_LOG identification_log = ConvertToIdentificationLog(identification_data);
            identification_log.TIED = tied;
            db.CDMA_INDIVIDUAL_IDENTIFICATION_LOG.Add(identification_log);
            db.SaveChanges();

            // OTHER DETAILS
            CDMA_INDIVIDUAL_OTHER_DETAILS other_data = (CDMA_INDIVIDUAL_OTHER_DETAILS)this.Session["cDMA_INDIVIDUAL_OTHER_DETAILS"];
            CDMA_INDIVIDUAL_OTHER_DETAILS_LOG other_log = ConvertToOtherLog(other_data);
            other_log.TIED = tied;
            db.CDMA_INDIVIDUAL_OTHER_DETAILS_LOG.Add(other_log);
            db.SaveChanges();
            */


            CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.Find(c_id);
            CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Find(c_id);
            CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Find(c_id);
            CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.Find(c_id);
            CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Find(c_id);

            //Address details
            //  var authrizer = DynamicModel.AddressDetails.AUTHORISED;
            // var AUTHORISED_BY = DynamicModel.AddressDetails.AUTHORISED_BY;
            //   var AUTHORISED_DATE = DynamicModel.AddressDetails.AUTHORISED_DATE;
           // checked if address record does not exist 
            if (cDMA_INDIVIDUAL_ADDRESS_DETAIL == null) {
                CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE = new CDMA_INDIVIDUAL_ADDRESS_DETAIL();
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.CREATED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.CREATED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.CITY_TOWN_OF_RESIDENCE = DynamicModel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.COUNTRY_OF_RESIDENCE = Request["COUNTRY_OF_RESIDENCE"]; // DynamicModel.AddressDetails.COUNTRY_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.LGA_OF_RESIDENCE = Request["LGA"]; // DynamicModel.AddressDetails.LGA_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.NEAREST_BUS_STOP_LANDMARK = DynamicModel.AddressDetails.NEAREST_BUS_STOP_LANDMARK;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.RESIDENCE_OWNED_OR_RENT = DynamicModel.AddressDetails.RESIDENCE_OWNED_OR_RENT;//Request["RESIDENCE_OWNED_OR_RENT"]; 
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.RESIDENTIAL_ADDRESS = DynamicModel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.STATE_OF_RESIDENCE = Request["STATES"];  //DynamicModel.AddressDetails.STATE_OF_RESIDENCE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.ZIP_POSTAL_CODE = DynamicModel.AddressDetails.ZIP_POSTAL_CODE;
                cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE.AUTHORISED = "U";

                db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Add(cDMA_INDIVIDUAL_ADDRESS_DETAIL_SAVE);
                db.SaveChanges();
            }
            else
            { 
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.CITY_TOWN_OF_RESIDENCE = DynamicModel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.COUNTRY_OF_RESIDENCE = Request["COUNTRY_OF_RESIDENCE"]; // DynamicModel.AddressDetails.COUNTRY_OF_RESIDENCE;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.LAST_MODIFIED_BY = identity.ProfileId.ToString();
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.LAST_MODIFIED_DATE = dateAndTime;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.LGA_OF_RESIDENCE = Request["LGA"]; // DynamicModel.AddressDetails.LGA_OF_RESIDENCE;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.NEAREST_BUS_STOP_LANDMARK = DynamicModel.AddressDetails.NEAREST_BUS_STOP_LANDMARK;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.RESIDENCE_OWNED_OR_RENT = Request["RESIDENCE_OWNED_OR_RENT"];
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.RESIDENTIAL_ADDRESS = DynamicModel.AddressDetails.RESIDENTIAL_ADDRESS;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.STATE_OF_RESIDENCE = Request["STATES_RES"];  //DynamicModel.AddressDetails.STATE_OF_RESIDENCE;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.ZIP_POSTAL_CODE = DynamicModel.AddressDetails.ZIP_POSTAL_CODE;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.RESIDENCE_OWNED_OR_RENT = DynamicModel.AddressDetails.RESIDENCE_OWNED_OR_RENT;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL.AUTHORISED = "U";
                db.SaveChanges();

               
            var address = (CDMA_INDIVIDUAL_ADDRESS_DETAIL)this.Session["cDMA_INDIVIDUAL_ADDRESS_DETAIL"];
            // compare the Old record with the new one and get comment
            var address_changes  = compareAddress(cDMA_INDIVIDUAL_ADDRESS_DETAIL, address);
                changesComment = changesComment + address_changes;
            // log address details
            CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG address_log = ConvertToAddressLog(address);
            address_log.TIED = tied;
            db.CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.Add(address_log);
            db.SaveChanges();


            }

            // Biodata            
            //cDMA_INDIVIDUAL_BIO_DATA.AGE = DynamicModel.BioData.AGE;
           //var AUTHORISED = DynamicModel.BioData.AUTHORISED;
           //var BIO_AUTHORISED_BY = DynamicModel.BioData.AUTHORISED_BY;
           // var BIO_AUTHORISED_DATE = DynamicModel.BioData.AUTHORISED_DATE;
           cDMA_INDIVIDUAL_BIO_DATA.BRANCH_CODE = Request["BRANCH"];  //DynamicModel.BioData.BRANCH_CODE;
           //cDMA_INDIVIDUAL_BIO_DATA.COMPLEXION = DynamicModel.BioData.COMPLEXION;
           var COUNTRY_OF_BIRTH = Request["COUNTRY_OF_BIRTH"]; // DynamicModel.BioData.COUNTRY_OF_BIRTH;
                                                               // var BIO_CREATED_BY = DynamicModel.BioData.CREATED_BY;
                                                               // var BIO_CREATED_DATE = DynamicModel.BioData.CREATED_DATE;
           cDMA_INDIVIDUAL_BIO_DATA.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
           cDMA_INDIVIDUAL_BIO_DATA.DATE_OF_BIRTH =  DynamicModel.BioData.DATE_OF_BIRTH;
           //cDMA_INDIVIDUAL_BIO_DATA.DISABILITY = retrunValue(DynamicModel.BioData.DISABILITY);
           cDMA_INDIVIDUAL_BIO_DATA.FIRST_NAME = DynamicModel.BioData.FIRST_NAME;
           cDMA_INDIVIDUAL_BIO_DATA.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
            //DynamicModel.BioData.LAST_MODIFIED_DATE;
            cDMA_INDIVIDUAL_BIO_DATA.MARITAL_STATUS = Request["MARITAL_STATUS"];//DynamicModel.BioData.MARITAL_STATUS;
           cDMA_INDIVIDUAL_BIO_DATA.MOTHER_MAIDEN_NAME = DynamicModel.BioData.MOTHER_MAIDEN_NAME;
           cDMA_INDIVIDUAL_BIO_DATA.NATIONALITY = Request["BioData_NATIONALITY"];  //// DynamicModel.BioData.NATIONALITY;
           //cDMA_INDIVIDUAL_BIO_DATA.NICKNAME_ALIAS = DynamicModel.BioData.NICKNAME_ALIAS;
           //cDMA_INDIVIDUAL_BIO_DATA.NUMBER_OF_CHILDREN = DynamicModel.BioData.NUMBER_OF_CHILDREN;
           cDMA_INDIVIDUAL_BIO_DATA.OTHER_NAME = DynamicModel.BioData.OTHER_NAME;
           //cDMA_INDIVIDUAL_BIO_DATA.PLACE_OF_BIRTH = DynamicModel.BioData.PLACE_OF_BIRTH;
           cDMA_INDIVIDUAL_BIO_DATA.RELIGION = DynamicModel.BioData.RELIGION;
           cDMA_INDIVIDUAL_BIO_DATA.SEX = DynamicModel.BioData.SEX;
           cDMA_INDIVIDUAL_BIO_DATA.STATE_OF_ORIGIN = DynamicModel.BioData.STATE_OF_ORIGIN;
           cDMA_INDIVIDUAL_BIO_DATA.SURNAME = DynamicModel.BioData.SURNAME;
           cDMA_INDIVIDUAL_BIO_DATA.TITLE = DynamicModel.BioData.TITLE;
           cDMA_INDIVIDUAL_BIO_DATA.RELIGION = Request["RELIGION"];
           cDMA_INDIVIDUAL_BIO_DATA.COUNTRY_OF_BIRTH = Request["COUNTRY_OF_BIRTH"];
            cDMA_INDIVIDUAL_BIO_DATA.LAST_MODIFIED_DATE = dateAndTime;
            cDMA_INDIVIDUAL_BIO_DATA.AUTHORISED = "U";
            db.SaveChanges();

           CDMA_INDIVIDUAL_BIO_DATA bio_data = (CDMA_INDIVIDUAL_BIO_DATA)this.Session["cDMA_INDIVIDUAL_BIO_DATA"];
           // compare old bio record with the new bio record
           var biodata_changes = compareBioRecord(cDMA_INDIVIDUAL_BIO_DATA, bio_data);
           changesComment = changesComment + biodata_changes;


            CDMA_INDIVIDUAL_BIO_LOG biodata_log = ConvertToBioDataLog(bio_data);
            biodata_log.TIED = tied;
            db.CDMA_INDIVIDUAL_BIO_LOG.Add(biodata_log);
            db.SaveChanges();


            //contact	 
            //var CONTACT_AUTHORISED =  DynamicModel.contact.AUTHORISED;
            // var CONTACT_AUTHORISED_BY = DynamicModel.contact.AUTHORISED_BY;
            //  var CONTACT_AUTHORISED_DATE = DynamicModel.contact.AUTHORISED_DATE;
            //  var CONTACT_CREATED_BY = DynamicModel.contact.CREATED_BY;
            //  var CONTACT_CREATED_DATE = DynamicModel.contact.CREATED_DATE;
            //  var CONTACT_CUSTOMER_NO = DynamicModel.contact.CUSTOMER_NO;
            if (cDMA_INDIVIDUAL_CONTACT_DETAIL == null)
            {
                CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE = new CDMA_INDIVIDUAL_CONTACT_DETAIL();
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.CREATED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.CREATED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.MAILING_ADDRESS = DynamicModel.contact.MAILING_ADDRESS;
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.MOBILE_NO = DynamicModel.contact.MOBILE_NO;
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.EMAIL_ADDRESS = DynamicModel.contact.EMAIL_ADDRESS;
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE.AUTHORISED = "U";
                db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Add(cDMA_INDIVIDUAL_CONTACT_DETAIL_SAVE);
                db.SaveChanges();

            }else {
              cDMA_INDIVIDUAL_CONTACT_DETAIL.LAST_MODIFIED_BY = identity.ProfileId.ToString();
              cDMA_INDIVIDUAL_CONTACT_DETAIL.MAILING_ADDRESS = DynamicModel.contact.MAILING_ADDRESS;
              cDMA_INDIVIDUAL_CONTACT_DETAIL.MOBILE_NO = DynamicModel.contact.MOBILE_NO;
              cDMA_INDIVIDUAL_CONTACT_DETAIL.EMAIL_ADDRESS = DynamicModel.contact.EMAIL_ADDRESS;
              cDMA_INDIVIDUAL_CONTACT_DETAIL.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
              cDMA_INDIVIDUAL_CONTACT_DETAIL.LAST_MODIFIED_BY = identity.ProfileId.ToString();
              cDMA_INDIVIDUAL_CONTACT_DETAIL.LAST_MODIFIED_DATE = dateAndTime;
              cDMA_INDIVIDUAL_CONTACT_DETAIL.AUTHORISED = "U";
                db.SaveChanges();
              CDMA_INDIVIDUAL_CONTACT_DETAIL contact_data = (CDMA_INDIVIDUAL_CONTACT_DETAIL)this.Session["cDMA_INDIVIDUAL_CONTACT_DETAIL"];

              var contact_changes = compareContactRecord(cDMA_INDIVIDUAL_CONTACT_DETAIL, contact_data);
              changesComment = changesComment + contact_changes;

              CDMA_INDIVIDUAL_CONTACT_LOG contact_log = ConvertToContactDataLog(contact_data);
              contact_log.TIED = tied;
              db.CDMA_INDIVIDUAL_CONTACT_LOG.Add(contact_log);
              db.SaveChanges();
            }



            // identification 
            if (cDMA_INDIVIDUAL_IDENTIFICATION == null)
            {
                CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION_SAVE= new CDMA_INDIVIDUAL_IDENTIFICATION();
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.CREATED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.CREATED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.IDENTIFICATION_TYPE = Request["CDMA_IDENTIFICATION_TYPE"];
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.ID_EXPIRY_DATE = DynamicModel.identification.ID_EXPIRY_DATE;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.ID_ISSUE_DATE = DynamicModel.identification.ID_ISSUE_DATE;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.ID_NO = DynamicModel.identification.ID_NO;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.LAST_MODIFIED_BY = DynamicModel.identification.LAST_MODIFIED_BY;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.LAST_MODIFIED_DATE = dateAndTime;
                //cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.PLACE_OF_ISSUANCE = DynamicModel.identification.PLACE_OF_ISSUANCE;
                cDMA_INDIVIDUAL_IDENTIFICATION_SAVE.AUTHORISED = "U";
                db.CDMA_INDIVIDUAL_IDENTIFICATION.Add(cDMA_INDIVIDUAL_IDENTIFICATION_SAVE);
                
                db.SaveChanges();

            }else { 
            //var identification_AUTHORISED =  DynamicModel.identification.AUTHORISED;
            //var identification_AUTHORISED_BY = DynamicModel.identification.AUTHORISED_BY;
            // var identification_AUTHORISED_DATE = DynamicModel.identification.AUTHORISED_DATE;
            //var identification_CREATED_BY = DynamicModel.identification.CREATED_BY;
            // var identification_CREATED_DATE = DynamicModel.identification.CREATED_DATE;
                  cDMA_INDIVIDUAL_IDENTIFICATION.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                  cDMA_INDIVIDUAL_IDENTIFICATION.IDENTIFICATION_TYPE = Request["CDMA_IDENTIFICATION_TYPE"];
                  cDMA_INDIVIDUAL_IDENTIFICATION.ID_EXPIRY_DATE = DynamicModel.identification.ID_EXPIRY_DATE;
                  cDMA_INDIVIDUAL_IDENTIFICATION.ID_ISSUE_DATE = DynamicModel.identification.ID_ISSUE_DATE;
                  cDMA_INDIVIDUAL_IDENTIFICATION.ID_NO = DynamicModel.identification.ID_NO;
                  cDMA_INDIVIDUAL_IDENTIFICATION.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                  cDMA_INDIVIDUAL_IDENTIFICATION.LAST_MODIFIED_BY = DynamicModel.identification.LAST_MODIFIED_BY;
                  cDMA_INDIVIDUAL_IDENTIFICATION.LAST_MODIFIED_DATE = dateAndTime;
                  //cDMA_INDIVIDUAL_IDENTIFICATION.PLACE_OF_ISSUANCE = DynamicModel.identification.PLACE_OF_ISSUANCE;
                  cDMA_INDIVIDUAL_IDENTIFICATION.AUTHORISED = "U";
                CDMA_INDIVIDUAL_IDENTIFICATION identification_data = (CDMA_INDIVIDUAL_IDENTIFICATION)this.Session["cDMA_INDIVIDUAL_IDENTIFICATION"];

                var identification_changes = compareIdentificationRecord(cDMA_INDIVIDUAL_IDENTIFICATION, identification_data);
                changesComment = changesComment + identification_changes;
                db.SaveChanges();

                CDMA_INDIVIDUAL_IDENTIFICATION_LOG identification_log = ConvertToIdentificationLog(identification_data);
                identification_log.TIED = tied;
                db.CDMA_INDIVIDUAL_IDENTIFICATION_LOG.Add(identification_log);
                db.SaveChanges();

               
                
            }



            //otherdetails	 

            if (cDMA_INDIVIDUAL_OTHER_DETAILS == null)
            {
                CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE = new CDMA_INDIVIDUAL_OTHER_DETAILS();
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.CREATED_BY = identity.ProfileId.ToString();
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.CREATED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.TIN_NO = DynamicModel.otherdetails.TIN_NO;
                cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE.AUTHORISED = "U";
                db.CDMA_INDIVIDUAL_OTHER_DETAILS.Add(cDMA_INDIVIDUAL_OTHER_DETAILS_SAVE);

                db.SaveChanges();
            }
            else
            {
            //  var otherdetails_PLACE_OF_ISSUANCE = DynamicModel.otherdetails.AUTHORISED;
            // var otherdetails_PLACE_OF_AUTHORISED_BY = DynamicModel.otherdetails.AUTHORISED_BY;
            // var otherdetails_PLACE_OF_AUTHORISED_DATE = DynamicModel.otherdetails.AUTHORISED_DATE;
            //  var otherdetails_PLACE_OF_CREATED_BY = DynamicModel.otherdetails.CREATED_BY;
            // var otherdetails_PLACE_OF_CREATED_DATE = DynamicModel.otherdetails.CREATED_DATE;
              cDMA_INDIVIDUAL_OTHER_DETAILS.CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
              cDMA_INDIVIDUAL_OTHER_DETAILS.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
              cDMA_INDIVIDUAL_OTHER_DETAILS.LAST_MODIFIED_DATE = dateAndTime;
                cDMA_INDIVIDUAL_OTHER_DETAILS.AUTHORISED = "U";
                cDMA_INDIVIDUAL_OTHER_DETAILS.TIN_NO = DynamicModel.otherdetails.TIN_NO;
              db.SaveChanges();
              CDMA_INDIVIDUAL_OTHER_DETAILS other_data = (CDMA_INDIVIDUAL_OTHER_DETAILS)this.Session["cDMA_INDIVIDUAL_OTHER_DETAILS"];

              var identification_changes = compareOtherRecord(cDMA_INDIVIDUAL_OTHER_DETAILS, other_data);
              changesComment = changesComment + identification_changes;

              CDMA_INDIVIDUAL_OTHER_DETAILS_LOG other_log = ConvertToOtherLog(other_data);
              other_log.TIED = tied;
              db.CDMA_INDIVIDUAL_OTHER_DETAILS_LOG.Add(other_log);
              db.SaveChanges();

              
                
            }



            CDMA_INDIVIDUAL_PROFILE_LOG SAVE_BIODATA_LOG = new CDMA_INDIVIDUAL_PROFILE_LOG();
            SAVE_BIODATA_LOG.AFFECTED_CATEGORY = this.Session["category"].ToString();
            SAVE_BIODATA_LOG.CUSTOMER_NO = c_id;
            SAVE_BIODATA_LOG.LOGGED_BY = Convert.ToDecimal(identity.ProfileId);
            SAVE_BIODATA_LOG.LOGGED_DATE = dateAndTime;
            SAVE_BIODATA_LOG.TIED = tied;
            SAVE_BIODATA_LOG.COMMENTS = changesComment;
            string change_index = String.Join(", ", index_id);
            //int[] change_index = index_id.ToArray();
            SAVE_BIODATA_LOG.CHANGE_INDEX = change_index.ToString();
            db.CDMA_INDIVIDUAL_PROFILE_LOG.Add(SAVE_BIODATA_LOG);
            db.SaveChanges();




            return PartialView("SaveBioData", DynamicModel);


        }




        public ActionResult DataValidation(string c_id, decimal branch, decimal rule, string table)
        {
            this.Session["table"] = table;
            
            string table_cat = "";
            string[] biodata_array = { "CDMA_INDIVIDUAL_BIO_DATA", "CDMA_INDIVIDUAL_CONTACT_DETAIL",
                                         "CDMA_INDIVIDUAL_ADDRESS_DETAIL", "CDMA_INDIVIDUAL_IDENTIFICATION" , "CDMA_INDIVIDUAL_OTHER_DETAILS"};
            string[] accinfo_array = { "CDMA_ACCOUNT_INFO", "CDMA_ACCT_SERVICES_REQUIRED" };
            string[] cusinfo_array = { "CDMA_CUSTOMER_INCOME" };
            //CDMA_CUSTOMER_INCOME

            if (biodata_array.Contains(table))
            {
                table_cat = "biodata";

            }
            else if (accinfo_array.Contains(table))
            {
                table_cat = "accinfo";
            }
            else if (accinfo_array.Contains(table))
            {
                table_cat = "cusinfo";
            }

                var viewModel = new DynamicViewModel(); //Create an instance of the above view model

 

            switch (table_cat)
            {
                case "biodata":
                    this.Session["category"] = "biodata";
                    if (c_id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.SingleOrDefault(c => c.CUSTOMER_NO == c_id);

                    //get all customer changes log  
                    ViewBag.profile_log = db.CDMA_INDIVIDUAL_PROFILE_LOG.Where(b => b.CUSTOMER_NO == c_id).ToList().Where(b => b.AFFECTED_CATEGORY == "biodata").OrderBy(i => i.LOG_ID).ToList();
                    string ChangeIndex = null;
                    int[] nums;
                    var lastUpdate = db.CDMA_INDIVIDUAL_PROFILE_LOG.Where(b => b.CUSTOMER_NO == c_id).ToList().Where(b => b.AFFECTED_CATEGORY == "biodata").OrderByDescending(i => i.LOG_ID).Take(1);
                    var changeIndexSingle = lastUpdate.SingleOrDefault();
                    if (!(changeIndexSingle == null))
                    {
                        ChangeIndex = changeIndexSingle.CHANGE_INDEX;
                    }
                    else
                    {
                        ChangeIndex = null;
                    }                    

                    if (!(ChangeIndex == null))
                    { 
                        nums = Array.ConvertAll(ChangeIndex.Split(','), int.Parse);
                        ViewBag.changeIndex = nums;
                    }
                    else
                    {
                        ViewBag.changeIndex = new int[] {};

                    }
                    

                    ViewBag.record = cDMA_INDIVIDUAL_BIO_DATA;
                    ViewBag.COUNTRY_OF_BIRTH = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME", cDMA_INDIVIDUAL_BIO_DATA.COUNTRY_OF_BIRTH);
                    ViewBag.BioData_NATIONALITY = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ABBREVIATION", "COUNTRY_NAME", cDMA_INDIVIDUAL_BIO_DATA.NATIONALITY);
                    ViewBag.RELIGION = new SelectList(db.CDMA_RELIGION, "CODE", "RELIGION", cDMA_INDIVIDUAL_BIO_DATA.RELIGION);
                    ViewBag.STATES = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", cDMA_INDIVIDUAL_BIO_DATA.STATE_OF_ORIGIN);

                    ViewBag.STATES_RES = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", cDMA_INDIVIDUAL_BIO_DATA.STATE_OF_ORIGIN);

                    if (cDMA_INDIVIDUAL_ADDRESS_DETAIL == null)
                    {
                        ViewBag.COUNTRY_OF_RESIDENCE = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME");
                    }
                    else
                    {
                        ViewBag.COUNTRY_OF_RESIDENCE = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME", cDMA_INDIVIDUAL_ADDRESS_DETAIL.COUNTRY_OF_RESIDENCE);
                    }


                    if (cDMA_INDIVIDUAL_IDENTIFICATION == null)
                    {
                        ViewBag.CDMA_IDENTIFICATION_TYPE = new SelectList(db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE");

                    }
                    else
                    {
                        ViewBag.CDMA_IDENTIFICATION_TYPE = new SelectList(db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE", cDMA_INDIVIDUAL_IDENTIFICATION.IDENTIFICATION_TYPE);

                    }
                    // decimal user_lga = Convert.ToDecimal(cDMA_INDIVIDUAL_ADDRESS_DETAIL.LGA_OF_RESIDENCE);

                    if (cDMA_INDIVIDUAL_ADDRESS_DETAIL == null)
                    {
                        ViewBag.LGA = new[] { new SelectListItem { } };
                    }
                    else
                    {
                        decimal user_lga = Convert.ToDecimal(cDMA_INDIVIDUAL_ADDRESS_DETAIL.LGA_OF_RESIDENCE);
                        decimal user_res_state = Convert.ToDecimal(cDMA_INDIVIDUAL_ADDRESS_DETAIL.STATE_OF_RESIDENCE);
                        ViewBag.LGA = new SelectList(db.SRC_CDMA_LGA.Where(g => g.LGA_ID == user_lga), "LGA_ID", "LGA_NAME", user_lga);
                        ViewBag.STATES_RES = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", user_res_state);

                    }

                    if (cDMA_INDIVIDUAL_BIO_DATA != null)
                    {
                        ViewBag.MARITAL_STATUS = new SelectList(db.CDMA_MARITALSTATUS, "CODE", "STATUS", cDMA_INDIVIDUAL_BIO_DATA.MARITAL_STATUS);
                    }
                    else
                    {
                        ViewBag.MARITAL_STATUS = new SelectList(db.CDMA_MARITALSTATUS, "CODE", "STATUS");
                    }

                    //save current data into session variable for future usage
                    //set the customer value incase of empty record 

                    this.Session["cDMA_INDIVIDUAL_BIO_DATA"] = cDMA_INDIVIDUAL_BIO_DATA;
                    this.Session["cDMA_INDIVIDUAL_ADDRESS_DETAIL"] = cDMA_INDIVIDUAL_ADDRESS_DETAIL;
                    this.Session["cDMA_INDIVIDUAL_CONTACT_DETAIL"] = cDMA_INDIVIDUAL_CONTACT_DETAIL;
                    this.Session["cDMA_INDIVIDUAL_IDENTIFICATION"] = cDMA_INDIVIDUAL_IDENTIFICATION;
                    this.Session["cDMA_INDIVIDUAL_OTHER_DETAILS"] = cDMA_INDIVIDUAL_OTHER_DETAILS;

                    if (cDMA_INDIVIDUAL_BIO_DATA == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.BRANCH = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME", cDMA_INDIVIDUAL_BIO_DATA.BRANCH_CODE);
                    //var viewModel = new DynamicViewModel(); //Create an instance of the above view model
                    viewModel.BioData = cDMA_INDIVIDUAL_BIO_DATA;
                    viewModel.AddressDetails = cDMA_INDIVIDUAL_ADDRESS_DETAIL;
                    viewModel.contact = cDMA_INDIVIDUAL_CONTACT_DETAIL;
                    viewModel.identification = cDMA_INDIVIDUAL_IDENTIFICATION;
                    viewModel.otherdetails = cDMA_INDIVIDUAL_OTHER_DETAILS;
                    //return PartialView("EditCustomer", viewModel);
                    return PartialView("validateData", viewModel);


                case "accinfo":
                    this.Session["category"] = "accinfo";
                    if (c_id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }



                    ViewBag.profile_log = db.CDMA_INDIVIDUAL_PROFILE_LOG.Where(b => b.CUSTOMER_NO == c_id).ToList().Where(b => b.AFFECTED_CATEGORY == "biodata").OrderBy(i => i.LOG_ID).ToList();
                    string ChangeIndexAccInfo = null;
                    int[] numsAccInfo;
                    var lastUpdateAccInfo = db.CDMA_ACCOUNT_RECORD_LOG.Where(b => b.CUSTOMER_NO == c_id).ToList().Where(b => b.AFFECTED_CATEGORY == "biodata").OrderByDescending(i => i.LOG_ID).Take(1);
                    var changeIndexSingleAcc = lastUpdateAccInfo.SingleOrDefault();
                    if (!(changeIndexSingleAcc == null))
                    {
                        ChangeIndex = changeIndexSingleAcc.CHANGE_INDEX;
                    }
                    else
                    {
                        ChangeIndex = null;
                    }

                    if (!(ChangeIndex == null))
                    {
                        numsAccInfo = Array.ConvertAll(ChangeIndex.Split(','), int.Parse);
                        ViewBag.changeIndex = numsAccInfo;
                    }
                    else
                    {
                        ViewBag.changeIndex = new int[] { };

                    }



                    CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO = db.CDMA_ACCOUNT_INFO.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED = db.CDMA_ACCT_SERVICES_REQUIRED.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_BIO_DATA biorecord = db.CDMA_INDIVIDUAL_BIO_DATA.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    ViewBag.BRANCH = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME", cDMA_ACCOUNT_INFO.BRANCH);
                    ViewBag.biorecord = biorecord;

                    ViewBag.acc_info_log = db.CDMA_ACCOUNT_RECORD_LOG.Where(b => b.CUSTOMER_NO == c_id).Where(b => b.AFFECTED_CATEGORY == "biodata").OrderBy(i => i.LOG_ID).ToList();


                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.ORIGINATING_BRANCH != null)
                    {
                        ViewBag.OringinBranch = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME", cDMA_ACCOUNT_INFO.ORIGINATING_BRANCH);
                    }
                    else
                    {
                        ViewBag.OringinBranch = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME");
                    }


                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.BRANCH_CLASS != null)
                    {
                        ViewBag.BranchClass = new SelectList(db.CDMA_BRANCH_CLASS, "ID", "CLASS", cDMA_ACCOUNT_INFO.BRANCH_CLASS);
                    }
                    else
                    {
                        ViewBag.BranchClass = new SelectList(db.CDMA_BRANCH_CLASS, "ID", "CLASS");
                    }

                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.CUSTOMER_SEGMENT != null)
                    {
                        ViewBag.CustomerSegment = new SelectList(db.CDMA_CUSTOMER_SEGMENT, "ID", "SEGMENT", cDMA_ACCOUNT_INFO.CUSTOMER_SEGMENT);
                    }
                    else
                    {
                        ViewBag.CustomerSegment = new SelectList(db.CDMA_CUSTOMER_SEGMENT, "ID", "SEGMENT");
                    }

                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.CUSTOMER_TYPE != null)
                    {
                        ViewBag.CustomerType = new SelectList(db.CDMA_CUSTOMER_TYPE, "TYPE_ID", "CUSTOMER_TYPE", cDMA_ACCOUNT_INFO.CUSTOMER_TYPE);
                    }
                    else
                    {
                        ViewBag.CustomerType = new SelectList(db.CDMA_CUSTOMER_TYPE, "TYPE_ID", "CUSTOMER_TYPE");
                    }

                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.BUSINESS_SIZE != null)
                    {
                        ViewBag.BusinessSize = new SelectList(db.CDMA_BUSINESS_SIZE, "SIZE_ID", "SIZE_RANGE", cDMA_ACCOUNT_INFO.BUSINESS_SIZE);
                    }
                    else
                    {
                        ViewBag.BusinessSize = new SelectList(db.CDMA_BUSINESS_SIZE, "SIZE_ID", "SIZE_RANGE");
                    }

                    if (cDMA_ACCT_SERVICES_REQUIRED != null && cDMA_ACCT_SERVICES_REQUIRED.CHEQUE_CONFIRMATION_THRESHOLD != null)
                    {
                        ViewBag.threshold = new SelectList(db.CDMA_CHEQUE_CONFIRM_THRESHOLD, "INCOME_ID", "EXPECTED_INCOME_BAND", cDMA_ACCT_SERVICES_REQUIRED.CHEQUE_CONFIRMATION_THRESHOLD);
                    }
                    else
                    {
                        ViewBag.threshold = new SelectList(db.CDMA_CHEQUE_CONFIRM_THRESHOLD, "INCOME_ID", "EXPECTED_INCOME_BAND");
                    }

                    if (cDMA_ACCT_SERVICES_REQUIRED != null && cDMA_ACCOUNT_INFO.TYPE_OF_ACCOUNT != null)
                    {
                        ViewBag.TYPE_OF_ACCOUNT = new SelectList(db.CDMA_ACCOUNT_TYPE, "ACCOUNT_ID", "ACCOUNT_NAME", cDMA_ACCOUNT_INFO.TYPE_OF_ACCOUNT);
                    }
                    else
                    {
                        ViewBag.TYPE_OF_ACCOUNT = new SelectList(db.CDMA_ACCOUNT_TYPE, "ACCOUNT_ID", "ACCOUNT_NAME");
                    }

                    if (cDMA_ACCT_SERVICES_REQUIRED != null && cDMA_ACCT_SERVICES_REQUIRED.ONLINE_TRANSFER_LIMIT_RANGE != null)
                    {
                        ViewBag.RangeLimit = new SelectList(db.Limit_Range, "ID", "LIMIT", cDMA_ACCT_SERVICES_REQUIRED.ONLINE_TRANSFER_LIMIT_RANGE);
                    }
                    else
                    {
                        ViewBag.RangeLimit = new SelectList(db.Limit_Range, "ID", "LIMIT");
                    }


                    if (cDMA_ACCT_SERVICES_REQUIRED != null && cDMA_ACCOUNT_INFO.BUSINESS_DIVISION != null)
                    {
                        ViewBag.BUSINESSDIVISION = new SelectList(db.CDMA_BUSINESS_DIVISION, "ID", "DIVISION", cDMA_ACCOUNT_INFO.BUSINESS_DIVISION);
                    }
                    else
                    {
                        ViewBag.BUSINESSDIVISION = new SelectList(db.CDMA_BUSINESS_DIVISION, "ID", "DIVISION");
                    }


                    if (cDMA_ACCT_SERVICES_REQUIRED != null && cDMA_ACCOUNT_INFO.BUSINESS_SEGMENT != null)
                    {
                        ViewBag.BUSINESS_SEGMENT = new SelectList(db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT", cDMA_ACCOUNT_INFO.BUSINESS_SEGMENT);
                    }
                    else
                    {
                        ViewBag.BUSINESS_SEGMENT = new SelectList(db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT");
                    }


                    //CDMA_CUSTOMER_TYPE
                    ViewBag.record = biorecord;


                    //save current data into session variable for future usage
                    //set the customer value incase of empty record 
                    if (cDMA_ACCOUNT_INFO != null)
                    {
                        this.Session["cDMA_ACCOUNT_INFO"] = cDMA_ACCOUNT_INFO;
                    }
                    else
                    {
                        this.Session["cDMA_ACCOUNT_INFO"] = null;
                    }

                    if (cDMA_ACCT_SERVICES_REQUIRED != null)
                    {
                        this.Session["cDMA_ACCT_SERVICES_REQUIRED"] = cDMA_ACCT_SERVICES_REQUIRED;
                    }
                    else
                    {
                        this.Session["cDMA_ACCT_SERVICES_REQUIRED"] = null;
                    }

                    //get all customer changes log  
                    ViewBag.profile_log = db.CDMA_INDIVIDUAL_PROFILE_LOG.Where(b => b.CUSTOMER_NO == c_id).ToList().Where(b => b.AFFECTED_CATEGORY == "biodata").OrderBy(i => i.LOG_ID).ToList();

                    //var viewAccInfoModel = new DynamicViewModel(); //Create an instance of the above view model CONFIRM_THRESHOLD
                    viewModel.AccInfo = cDMA_ACCOUNT_INFO;
                    viewModel.ServiceInfo = cDMA_ACCT_SERVICES_REQUIRED;
                    return PartialView("ApproveAccInfo", viewModel);

                case "cusinfo":
                    this.Session["category"] = "cusinfo";
                    if (c_id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    
                    return PartialView("ApproveAccInfo", viewModel);


                case "Beet Armyworm":
                 
                default:
                    return JavaScript("alert('No more images');");
            }





        }

         



        public ActionResult Dataquality(string c_id, decimal branch, decimal rule,string table)
        {
            this.Session["rule"] = rule;
            this.Session["branch"] = branch;
            this.Session["id"] = c_id;
            this.Session["table"] = table;
            this.Session["category"] = "biodata";
            string table_cat = "";
            string[] biodata_array = { "CDMA_INDIVIDUAL_BIO_DATA", "CDMA_INDIVIDUAL_CONTACT_DETAIL",
                                         "CDMA_INDIVIDUAL_ADDRESS_DETAIL", "CDMA_INDIVIDUAL_IDENTIFICATION" , "CDMA_INDIVIDUAL_OTHER_DETAILS"};
            string[] accinfo_array = { "CDMA_ACCOUNT_INFO", "CDMA_ACCT_SERVICES_REQUIRED"};

            string[] cusinfo_array = { "CDMA_CUSTOMER_INCOME","CDMA_CUSTOMER_INCOME" };//CDMA_CUSTOMER_INCOME
            //CDMA_CUSTOMER_INCOME

            if (biodata_array.Contains(table))
            {
                table_cat = "biodata";

            }
            else if (accinfo_array.Contains(table))
            {
                table_cat = "accinfo";
            }
            else if (cusinfo_array.Contains(table))
            {
                table_cat = "cusinfo";
            }
            var viewModel = new DynamicViewModel(); //Create an instance of the above view model
            
            switch (table_cat)
            {
                case "biodata":
                    if (c_id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    CDMA_INDIVIDUAL_BIO_DATA cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.SingleOrDefault(c => c.CUSTOMER_NO == c_id);

                    //get all customer changes log  
                    ViewBag.profile_log =  db.CDMA_INDIVIDUAL_PROFILE_LOG.Where(b => b.CUSTOMER_NO == c_id).ToList().Where(b => b.AFFECTED_CATEGORY == "biodata").OrderBy(i => i.LOG_ID).ToList();

                    ViewBag.logs = (from ep in db.CDMA_INDIVIDUAL_PROFILE_LOG
                                      join e in db.CM_USER_PROFILE on ep.LOGGED_BY equals e.PROFILE_ID
                                      where ep.CUSTOMER_NO == c_id
                                      where ep.AFFECTED_CATEGORY == "biodata"
                                      select new
                                      {
                                          NAME = e.DISPLAY_NAME,
                                          COMMENT = ep.COMMENTS,
                                          DATE = ep.LOGGED_DATE,
                                          TIED = ep.TIED,
                                          INDEX = ep.CHANGE_INDEX
                                      }).ToList();


                    ViewBag.record = cDMA_INDIVIDUAL_BIO_DATA;
                    ViewBag.COUNTRY_OF_BIRTH = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME", cDMA_INDIVIDUAL_BIO_DATA.COUNTRY_OF_BIRTH);
                    ViewBag.BioData_NATIONALITY = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ABBREVIATION", "COUNTRY_NAME", cDMA_INDIVIDUAL_BIO_DATA.NATIONALITY);
                    ViewBag.RELIGION = new SelectList(db.CDMA_RELIGION, "CODE", "RELIGION", cDMA_INDIVIDUAL_BIO_DATA.RELIGION);
                    ViewBag.BRANCH = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME", cDMA_INDIVIDUAL_BIO_DATA.BRANCH_CODE);
                    ViewBag.STATES = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", cDMA_INDIVIDUAL_BIO_DATA.STATE_OF_ORIGIN);

                    ViewBag.STATES_RES = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", cDMA_INDIVIDUAL_BIO_DATA.STATE_OF_ORIGIN);

                    if (cDMA_INDIVIDUAL_BIO_DATA != null)
                    {
                        ViewBag.MARITAL_STATUS = new SelectList(db.CDMA_MARITALSTATUS, "CODE", "STATUS", cDMA_INDIVIDUAL_BIO_DATA.MARITAL_STATUS);
                    }
                    else
                    {
                        ViewBag.MARITAL_STATUS = new SelectList(db.CDMA_MARITALSTATUS, "CODE", "STATUS");
                    }


                    if (cDMA_INDIVIDUAL_ADDRESS_DETAIL == null)
                    {
                        ViewBag.COUNTRY_OF_RESIDENCE = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME");
                    }
                    else
                    {
                        ViewBag.COUNTRY_OF_RESIDENCE = new SelectList(db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME", cDMA_INDIVIDUAL_ADDRESS_DETAIL.COUNTRY_OF_RESIDENCE);
                    }


                    if (cDMA_INDIVIDUAL_IDENTIFICATION == null)
                    {
                        ViewBag.CDMA_IDENTIFICATION_TYPE = new SelectList(db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE");

                    }
                    else
                    {
                        ViewBag.CDMA_IDENTIFICATION_TYPE = new SelectList(db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE", cDMA_INDIVIDUAL_IDENTIFICATION.IDENTIFICATION_TYPE);

                    }
                    // decimal user_lga = Convert.ToDecimal(cDMA_INDIVIDUAL_ADDRESS_DETAIL.LGA_OF_RESIDENCE);

                    if (cDMA_INDIVIDUAL_ADDRESS_DETAIL == null)
                    {
                        ViewBag.LGA = new[] { new SelectListItem { } }; 
                    }
                    else
                    {
                        decimal user_lga = Convert.ToDecimal(cDMA_INDIVIDUAL_ADDRESS_DETAIL.LGA_OF_RESIDENCE);
                        decimal user_res_state = Convert.ToDecimal(cDMA_INDIVIDUAL_ADDRESS_DETAIL.STATE_OF_RESIDENCE);
                        ViewBag.LGA = new SelectList(db.SRC_CDMA_LGA.Where(g => g.LGA_ID == user_lga) , "LGA_ID", "LGA_NAME", user_lga);
                        ViewBag.STATES_RES = new SelectList(db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME", user_res_state);

                    }
 
                    //save current data into session variable for future usage
                    //set the customer value incase of empty record 

                    this.Session["cDMA_INDIVIDUAL_BIO_DATA"] = cDMA_INDIVIDUAL_BIO_DATA;
                    this.Session["cDMA_INDIVIDUAL_ADDRESS_DETAIL"] = cDMA_INDIVIDUAL_ADDRESS_DETAIL;
                    this.Session["cDMA_INDIVIDUAL_CONTACT_DETAIL"] = cDMA_INDIVIDUAL_CONTACT_DETAIL;
                    this.Session["cDMA_INDIVIDUAL_IDENTIFICATION"] = cDMA_INDIVIDUAL_IDENTIFICATION;
                    this.Session["cDMA_INDIVIDUAL_OTHER_DETAILS"] = cDMA_INDIVIDUAL_OTHER_DETAILS;
                   
                    if (cDMA_INDIVIDUAL_BIO_DATA == null)
                    {
                        return HttpNotFound();
                    }


                    viewModel.BioData = cDMA_INDIVIDUAL_BIO_DATA;  
                    viewModel.AddressDetails = cDMA_INDIVIDUAL_ADDRESS_DETAIL;
                    viewModel.contact = cDMA_INDIVIDUAL_CONTACT_DETAIL;
                    viewModel.identification = cDMA_INDIVIDUAL_IDENTIFICATION;
                    viewModel.otherdetails = cDMA_INDIVIDUAL_OTHER_DETAILS;
                    //return PartialView("EditCustomer", viewModel);
                    return PartialView("EditCustomer", viewModel);
              
                    
                case "accinfo":
                    if (c_id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    CDMA_ACCOUNT_INFO cDMA_ACCOUNT_INFO = db.CDMA_ACCOUNT_INFO.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_ACCT_SERVICES_REQUIRED cDMA_ACCT_SERVICES_REQUIRED = db.CDMA_ACCT_SERVICES_REQUIRED.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_BIO_DATA biorecord = db.CDMA_INDIVIDUAL_BIO_DATA.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    
                    ViewBag.CUSTOMER_NO = biorecord.CUSTOMER_NO;


                    //cDMA_ACCOUNT_INFO.CUSTOMER_NO = biorecord.CUSTOMER_NO;



                    ViewBag.biorecord = biorecord;

                    ViewBag.acc_info_log = db.CDMA_ACCOUNT_RECORD_LOG.Where(b => b.CUSTOMER_NO == c_id).Where(b => b.AFFECTED_CATEGORY == "biodata").OrderBy(i => i.LOG_ID).ToList();

                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.BRANCH != null)
                    {
                       ViewBag.BRANCH = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME", cDMA_ACCOUNT_INFO.BRANCH);
                    }
                    else
                    {
                       ViewBag.BRANCH = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME");
                    }


                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.ORIGINATING_BRANCH != null)
                    {
                        ViewBag.OringinBranch = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME", cDMA_ACCOUNT_INFO.ORIGINATING_BRANCH);
                    }
                    else
                    {
                        ViewBag.OringinBranch = new SelectList(db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME");
                    }


                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.BRANCH_CLASS != null)
                    {
                        ViewBag.BranchClass = new SelectList(db.CDMA_BRANCH_CLASS, "ID", "CLASS", cDMA_ACCOUNT_INFO.BRANCH_CLASS);
                    }
                    else
                    {
                        ViewBag.BranchClass = new SelectList(db.CDMA_BRANCH_CLASS, "ID", "CLASS");
                    }

                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.CUSTOMER_SEGMENT != null)
                    {
                        ViewBag.CustomerSegment = new SelectList(db.CDMA_CUSTOMER_SEGMENT, "ID", "SEGMENT", cDMA_ACCOUNT_INFO.CUSTOMER_SEGMENT);
                    }
                    else
                    {
                        ViewBag.CustomerSegment = new SelectList(db.CDMA_CUSTOMER_SEGMENT, "ID", "SEGMENT");
                    }

                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.CUSTOMER_TYPE != null)
                    {
                        ViewBag.CustomerType = new SelectList(db.CDMA_CUSTOMER_TYPE, "TYPE_ID", "CUSTOMER_TYPE", cDMA_ACCOUNT_INFO.CUSTOMER_TYPE);
                    }
                    else
                    {
                        ViewBag.CustomerType = new SelectList(db.CDMA_CUSTOMER_TYPE, "TYPE_ID", "CUSTOMER_TYPE");
                    }


                    if (cDMA_ACCOUNT_INFO != null && cDMA_ACCOUNT_INFO.BUSINESS_SIZE != null)
                    {
                        ViewBag.BusinessSize = new SelectList(db.CDMA_BUSINESS_SIZE, "SIZE_ID", "SIZE_RANGE", cDMA_ACCOUNT_INFO.BUSINESS_SIZE);
                    }
                    else
                    {
                        ViewBag.BusinessSize = new SelectList(db.CDMA_BUSINESS_SIZE, "SIZE_ID", "SIZE_RANGE");
                    }

                    if (cDMA_ACCT_SERVICES_REQUIRED != null && cDMA_ACCT_SERVICES_REQUIRED.CHEQUE_CONFIRMATION_THRESHOLD != null)
                    {
                        ViewBag.threshold = new SelectList(db.CDMA_CHEQUE_CONFIRM_THRESHOLD, "INCOME_ID", "EXPECTED_INCOME_BAND", cDMA_ACCT_SERVICES_REQUIRED.CHEQUE_CONFIRMATION_THRESHOLD);
                    }
                    else
                    {
                        ViewBag.threshold = new SelectList(db.CDMA_CHEQUE_CONFIRM_THRESHOLD, "INCOME_ID", "EXPECTED_INCOME_BAND");
                    }

                    if (cDMA_ACCT_SERVICES_REQUIRED != null && cDMA_ACCOUNT_INFO.TYPE_OF_ACCOUNT != null)
                    {
                        ViewBag.TYPE_OF_ACCOUNT = new SelectList(db.CDMA_ACCOUNT_TYPE, "ACCOUNT_ID", "ACCOUNT_NAME", cDMA_ACCOUNT_INFO.TYPE_OF_ACCOUNT);
                    }
                    else
                    {
                        ViewBag.TYPE_OF_ACCOUNT = new SelectList(db.CDMA_ACCOUNT_TYPE, "ACCOUNT_ID", "ACCOUNT_NAME");
                    }

                    if (cDMA_ACCT_SERVICES_REQUIRED != null && cDMA_ACCOUNT_INFO.BUSINESS_DIVISION != null)
                    {
                        ViewBag.BUSINESSDIVISION = new SelectList(db.CDMA_BUSINESS_DIVISION, "ID", "DIVISION", cDMA_ACCOUNT_INFO.BUSINESS_DIVISION);
                    }
                    else
                    {
                        ViewBag.BUSINESSDIVISION = new SelectList(db.CDMA_BUSINESS_DIVISION, "ID", "DIVISION");
                    }

                    if (cDMA_ACCT_SERVICES_REQUIRED != null && cDMA_ACCT_SERVICES_REQUIRED.ONLINE_TRANSFER_LIMIT_RANGE != null)
                    {
                        ViewBag.RangeLimit = new SelectList(db.Limit_Range, "ID", "LIMIT", cDMA_ACCT_SERVICES_REQUIRED.ONLINE_TRANSFER_LIMIT_RANGE);
                    }
                    else
                    {
                        ViewBag.RangeLimit = new SelectList(db.Limit_Range, "ID", "LIMIT");
                    }


                    if (cDMA_ACCT_SERVICES_REQUIRED != null && cDMA_ACCOUNT_INFO.BUSINESS_SEGMENT != null)
                    {
                        ViewBag.BUSINESS_SEGMENT = new SelectList(db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT", cDMA_ACCOUNT_INFO.BUSINESS_SEGMENT);
                    }
                    else
                    {
                        ViewBag.BUSINESS_SEGMENT = new SelectList(db.CDMA_BUSINESS_SEGMENT, "ID", "SEGMENT");
                    }


                    //CDMA_BUSINESS_SEGMENT







                    //CDMA_CUSTOMER_TYPE
                    ViewBag.record = biorecord;


                    //save current data into session variable for future usage
                    //set the customer value incase of empty record 
                    if (cDMA_ACCOUNT_INFO != null)
                    {
                        this.Session["cDMA_ACCOUNT_INFO"] = cDMA_ACCOUNT_INFO;
                    }
                    else
                    {
                        this.Session["cDMA_ACCOUNT_INFO"] = null;
                    }

                    if (cDMA_ACCT_SERVICES_REQUIRED != null)
                    {
                        this.Session["cDMA_ACCT_SERVICES_REQUIRED"] = cDMA_ACCT_SERVICES_REQUIRED;
                    }
                    else
                    {
                        this.Session["cDMA_ACCT_SERVICES_REQUIRED"] = null;
                    }

                    //get all customer changes log  
                    ViewBag.profile_log = db.CDMA_INDIVIDUAL_PROFILE_LOG.Where(b => b.CUSTOMER_NO == c_id).ToList().Where(b => b.AFFECTED_CATEGORY == "biodata").OrderBy(i => i.LOG_ID).ToList();

                    //  var viewAccInfoModel = new DynamicViewModel(); //Create an instance of the above view model CONFIRM_THRESHOLD
                    viewModel.AccInfo = cDMA_ACCOUNT_INFO;
                    viewModel.ServiceInfo = cDMA_ACCT_SERVICES_REQUIRED;
                    return PartialView("EditAccInfo", viewModel);

                case "cusinfo":
                    if (c_id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    CDMA_CUSTOMER_INCOME cDMA_CUSTOMER_INCOME = db.CDMA_CUSTOMER_INCOME.SingleOrDefault(c => c.CUSTOMER_NO == c_id);
                    CDMA_INDIVIDUAL_BIO_DATA cusrecord = db.CDMA_INDIVIDUAL_BIO_DATA.SingleOrDefault(c => c.CUSTOMER_NO == c_id);

                    ViewBag.CUSTOMER_NO = cusrecord.CUSTOMER_NO;
                    ViewBag.biorecord = cusrecord;
                    if (cDMA_CUSTOMER_INCOME != null && cDMA_CUSTOMER_INCOME.INCOME_BAND != null)
                    {
                        ViewBag.INCOME_BAND = new SelectList(db.CDMA_INCOME_BAND, "INCOME_ID", "EXPECTED_INCOME_BAND", cDMA_CUSTOMER_INCOME.INCOME_BAND);
                    }
                    else
                    {
                        ViewBag.INCOME_BAND = new SelectList(db.CDMA_INCOME_BAND, "INCOME_ID", "EXPECTED_INCOME_BAND" );

                    }
                    
                    if (cDMA_CUSTOMER_INCOME != null && cDMA_CUSTOMER_INCOME.INITIAL_DEPOSIT != null)
                    {
                        ViewBag.CustomerSegment = new SelectList(db.CDMA_INITIAL_DEPOSIT_RANGE, "DEPOSIT_ID", "INITIAL_DEPOSIT_RANGE", cDMA_CUSTOMER_INCOME.INITIAL_DEPOSIT);
                    }
                    else
                    {
                        ViewBag.CustomerSegment = new SelectList(db.CDMA_INITIAL_DEPOSIT_RANGE, "DEPOSIT_ID", "INITIAL_DEPOSIT_RANGE");
                    }
 
                    //CDMA_CUSTOMER_TYPE
                    ViewBag.record = cusrecord;
                    viewModel.CusIncomeInfo = cDMA_CUSTOMER_INCOME;
                     
                    return PartialView("EditIncomeInfo", viewModel);

                case "Indian Meal Moth":
                case "Ash Pug":
                case "Latticed Heath":
                case "Ribald Wave":
                case "The Streak":
                    return JavaScript("alert('No more images');");
                default:
                    return JavaScript("alert('No more images');");
            }





        }

        public string compareOtherRecord(CDMA_INDIVIDUAL_OTHER_DETAILS current, CDMA_INDIVIDUAL_OTHER_DETAILS previous) {
            string result = "";
           // cDMA_INDIVIDUAL_OTHER_DETAILS.TIN_NO = DynamicModel.otherdetails.TIN_NO;
            if (!(retrunValue(current.TIN_NO).Equals(retrunValue(previous.TIN_NO))))
            {
                index_id.Add(1);
                result =  result + "<tr><td> Identification Type changed from <strong>"
                                + previous.TIN_NO + "</strong> To "
                                + current.TIN_NO + "</td></tr>";
            }

            return result;


        }
        public string compareIdentificationRecord(CDMA_INDIVIDUAL_IDENTIFICATION current, CDMA_INDIVIDUAL_IDENTIFICATION previous)
        {
            string result = "";
            if (!((previous.IDENTIFICATION_TYPE == null) || (current.IDENTIFICATION_TYPE == null)))
            {
               if (!(retrunValue(current.IDENTIFICATION_TYPE).Equals(retrunValue(previous.IDENTIFICATION_TYPE))))
                {
                    var previous_idtype = IdType(Convert.ToDecimal(previous.IDENTIFICATION_TYPE));
                    var current_idtype = IdType(Convert.ToDecimal(current.IDENTIFICATION_TYPE));
                    var prev_id = "";
                    index_id.Add(2);
                    if (!(previous_idtype == null))
                    {
                        prev_id = previous_idtype.ID_TYPE;
                    }
                    result = result + "<tr><td> Identification Type changed from <strong>"
                                    + prev_id + "</strong> To "
                                    + current_idtype.ID_TYPE + "</td></tr>";
                }

            }
            if (!(current.ID_EXPIRY_DATE.Equals(previous.ID_EXPIRY_DATE)))
            {
                index_id.Add(3);
                result = result + "<tr><td> Expiring date changed from <strong>"
                                + previous.ID_EXPIRY_DATE + "</strong> To "
                                + current.ID_EXPIRY_DATE + "</td></tr>";
            }

            if (!(current.ID_ISSUE_DATE.Equals(previous.ID_ISSUE_DATE)))
            {
                index_id.Add(4);
                result = result +  "<tr><td> ID Issued Date changed from  <strong>"
                                + previous.ID_ISSUE_DATE + "</strong> To "
                                + current.ID_ISSUE_DATE + "</td></tr>";
            }

            if (!(retrunValue(current.ID_NO).Equals(retrunValue(previous.ID_NO))))
            {
                index_id.Add(5);
                result = result + "<tr><td> ID Number changed from  <strong>"
                                + previous.ID_NO + "</strong> To "
                                + current.ID_NO + "</td></tr>";
            }

            //if (!(retrunValue(current.PLACE_OF_ISSUANCE).Equals(retrunValue(previous.PLACE_OF_ISSUANCE))))
            //{
            //    index_id.Add(6);
            //    result = result + "<tr><td> ID Place of Issueance changed from  <strong>"
            //                    + previous.PLACE_OF_ISSUANCE + "</strong> To "
            //                    + current.PLACE_OF_ISSUANCE + "</td></tr>";
            //}


            return result;

        }

        public string compareContactRecord(CDMA_INDIVIDUAL_CONTACT_DETAIL current, CDMA_INDIVIDUAL_CONTACT_DETAIL previous) {

            string result = "";
            if (!(retrunValue(current.MAILING_ADDRESS).Equals(retrunValue(previous.MAILING_ADDRESS))))
            {
                index_id.Add(7);
                result = result + "<tr><td> Mailing Address changed from <strong>"
                                + previous.MAILING_ADDRESS + "</strong> To "
                                + current.MAILING_ADDRESS + "</td></tr>";
            }
            if (!(retrunValue(current.MOBILE_NO).Equals(retrunValue(previous.MOBILE_NO))))
            {
                index_id.Add(8);
                result = result + "<tr><td> Mobile number changed from <strong>"
                                + previous.MOBILE_NO + "</strong> To "
                                + current.MOBILE_NO + "</td></tr>";
            }

            if (!(retrunValue(current.EMAIL_ADDRESS).Equals(retrunValue(previous.EMAIL_ADDRESS))))
            {
                index_id.Add(9);
                result = result + "<tr><td> Email changed from <strong>"
                                + previous.EMAIL_ADDRESS + "</strong> To "
                                + current.EMAIL_ADDRESS + "</td></tr>";
            }


            return result;
        }


        public string compareBioRecord(CDMA_INDIVIDUAL_BIO_DATA current, CDMA_INDIVIDUAL_BIO_DATA previous)
        {
            string result = "";
            if (previous == null) {

                return result;
            }

            if (!(current.DATE_OF_BIRTH.Equals(previous.DATE_OF_BIRTH)))
            {
                index_id.Add(10);
                result = result + "<tr><td> Date of birth changed from <strong>"
                                + previous.DATE_OF_BIRTH + "</strong> To "
                                + current.DATE_OF_BIRTH + "</td></tr>";
            }
            //if (!(retrunValue(current.DISABILITY).Equals(retrunValue(previous.DISABILITY))))
            //{
            //    index_id.Add(11);
            //    result = result + "<tr><td> Disability changed from <strong>"
            //                    + previous.DISABILITY + "</strong> To "
            //                    + current.DISABILITY + "</td></tr>";
            //}

            //if (!(retrunValue(current.DISABILITY).Equals(retrunValue(previous.DISABILITY))))
            //{
            //    /*index_id.Add(12);
            //    result = result +  "<tr><td> Disability changed from <strong>"
            //                    + previous.DISABILITY + "</strong> To "
            //                    + current.DISABILITY + "</td></tr>";*/
            //}

            if (!(retrunValue(current.FIRST_NAME).Equals(retrunValue(previous.FIRST_NAME))))
            {
                index_id.Add(13);
                result = result + "<tr><td> First Name changed from <strong>"
                                + previous.FIRST_NAME + "</strong> To "
                                + current.FIRST_NAME + "</td></tr>";
            }

            if (!(retrunValue(current.MARITAL_STATUS).Equals(retrunValue(previous.MARITAL_STATUS))))
            {
                index_id.Add(14);
                result = result + "<tr><td> Marital Status changed from <strong>"
                                + previous.MARITAL_STATUS + "</strong> To "
                                + current.MARITAL_STATUS + "</td></tr>";
            }

            if (!(retrunValue(current.MOTHER_MAIDEN_NAME).Equals(retrunValue(previous.MOTHER_MAIDEN_NAME))))
            {
                index_id.Add(15);
                result = result + "<tr><td> Mother maiden name changed<strong>"
                                + previous.MOTHER_MAIDEN_NAME + "</strong> To "
                                + current.MOTHER_MAIDEN_NAME + "</td></tr>";
            }

            if (!(retrunValue(current.NATIONALITY).Equals(retrunValue(previous.NATIONALITY))))
            {
                index_id.Add(16);
                //var previous_nationality = countryById(Convert.ToDecimal(previous.NATIONALITY));
                //var current_nationality = countryById(Convert.ToDecimal(current.NATIONALITY));
               // result = result + "<tr><td> Nationality changed from <strong>"
               //                 + previous_nationality.COUNTRY_NAME + "</strong> To "
               //                 + previous_nationality.COUNTRY_NAME + "</td></tr>";
                result = result + "<tr><td> Nationality changed from <strong>"
                               + previous.NATIONALITY + "</strong> To "
                               + current.NATIONALITY + "</td></tr>";
            }

            //if (!(retrunValue(current.NICKNAME_ALIAS).Equals(retrunValue(previous.NICKNAME_ALIAS))))
            //{
            //    index_id.Add(17);
            //    result = result + "<tr><td> Nickname Changed from<strong>"
            //                    + previous.NICKNAME_ALIAS + "</strong> To "
            //                    + current.NICKNAME_ALIAS + "</td></tr>";
            //}

            //if (!(current.NUMBER_OF_CHILDREN.Equals(previous.NUMBER_OF_CHILDREN)))
            //{
            //    index_id.Add(18);
            //    result = result + "<tr><td> Number of children changed from<strong>"
            //                    + previous.NUMBER_OF_CHILDREN + "</strong> To "
            //                    + current.NUMBER_OF_CHILDREN + "</td></tr>";
            //}

            if (!(retrunValue(current.OTHER_NAME).Equals(retrunValue(previous.OTHER_NAME))))
            {
                index_id.Add(19);
                result = result + "<tr><td> Other Name changed from<strong>"
                                + previous.OTHER_NAME + "</strong> To "
                                + current.OTHER_NAME + "</td></tr>";
            }
            //if (!(retrunValue(current.PLACE_OF_BIRTH).Equals(previous.PLACE_OF_BIRTH)))
            //{
            //    index_id.Add(20);
            //    result = result + "<tr><td> Place of birth changed from<strong>"
            //                    + previous.PLACE_OF_BIRTH + "</strong> To "
            //                    + current.PLACE_OF_BIRTH + "</td></tr>";
            //}

            if (!(retrunValue(current.RELIGION).Equals(previous.RELIGION)))
            {
                index_id.Add(21);
                result = result +  "<tr><td> Religion changed from<strong>"
                                + previous.RELIGION + "</strong> To "
                                + current.RELIGION + "</td></tr>";
            }

            if (!(retrunValue(current.SEX).Equals(retrunValue(previous.SEX))))
            {
                index_id.Add(22);
                result = result + "<tr><td> Sex changed from<strong>"
                                + previous.RELIGION + "</strong> To "
                                + current.RELIGION + "</td></tr>";
            }

            if (!(retrunValue(current.STATE_OF_ORIGIN).Equals(retrunValue(previous.STATE_OF_ORIGIN))))
            {
                index_id.Add(23);
                var previous_state_of_origin = getState(Convert.ToDecimal(previous.STATE_OF_ORIGIN));
                var current_state_of_origin = getState(Convert.ToDecimal(current.STATE_OF_ORIGIN));
                result = result + "<tr><td> Satte of Origin changed from <strong>"
                                + previous_state_of_origin.STATE_NAME + "</strong> To "
                                + current_state_of_origin.STATE_NAME + "</td></tr>";
            }

            if (!(retrunValue(current.SURNAME).Equals(retrunValue(previous.SURNAME))))
            {
                index_id.Add(24);
                result = result + "<tr><td> Surname chnaged from <strong>"
                                + previous.SURNAME + "</strong> To "
                                + current.SURNAME + "</td></tr>";
            }

            if (!(retrunValue(current.TITLE).Equals(retrunValue(previous.TITLE))))
            {
                index_id.Add(25);
                result = result + "<tr><td> Title changed from <strong>"
                                + previous.TITLE + "</strong> To "
                                + current.TITLE + "</td></tr>";
            }

            if (!(retrunValue(current.RELIGION).Equals(retrunValue(previous.RELIGION))))
            {
                index_id.Add(26);
                var previous_rel = getRel(Convert.ToDecimal(previous.RELIGION));
                var current_rel = getRel(Convert.ToDecimal(current.RELIGION));
                result = result + "<tr><td> Religion changed from <strong>"
                                + previous_rel.RELIGION + "</strong> To "
                                + current_rel.RELIGION + "</td></tr>";
            }

            if (!(retrunValue(current.COUNTRY_OF_BIRTH).Equals(retrunValue(previous.COUNTRY_OF_BIRTH))))
            {
                index_id.Add(27);
                var previous_val = "";
                var current_val = "";
                var previous_country = countryById(Convert.ToDecimal(previous.COUNTRY_OF_BIRTH));
                var current_country = countryById(Convert.ToDecimal(current.COUNTRY_OF_BIRTH));
                if(previous_country != null)
                {
                   if (previous_country.COUNTRY_NAME != null) { previous_val = previous_country.COUNTRY_NAME; }
                }
                if (current_country != null) { current_val = current_country.COUNTRY_NAME; }

                result = result + "<tr><td> Title changed from <strong>"
                                + previous_val + "</strong> To "
                                + current_val + "</td></tr>";
            }
 
            return result;

        }




        public string compareAccInfo(CDMA_ACCOUNT_INFO current, CDMA_ACCOUNT_INFO previous)
        {
            string result = "";

            if ((current.ACCOUNT_OFFICER != null) && (previous.ACCOUNT_OFFICER != null))
            {
                if (!(retrunValue(current.ACCOUNT_OFFICER).Equals(retrunValue(previous.ACCOUNT_OFFICER))))
                {
                    index_id.Add(64);
                    result = result + "<tr><td> ACCOUNT OFFICER changed<strong>"
                                    + previous.ACCOUNT_OFFICER + "</strong> To "
                                    + current.ACCOUNT_OFFICER + "</td></tr>";
                }
            }

            if ((current.ACCOUNT_TITLE != null) && ((previous.ACCOUNT_TITLE != null )))
            {
                 
                if (!(retrunValue(current.ACCOUNT_TITLE).Equals(retrunValue(previous.ACCOUNT_TITLE))))
                {
                    index_id.Add(65);
                    result = result + "<tr><td> ACCOUNT TITLE<strong>"
                                    + previous.ACCOUNT_TITLE + "</strong> To "
                                    + current.ACCOUNT_TITLE + "</td></tr>";
                }
            }

            if (!(retrunValue(current.CUSTOMER_IC).Equals(retrunValue(previous.CUSTOMER_IC))))
            {
                index_id.Add(36);
                result = result + "<tr><td> CUSTOMER IC changed<strong>"
                                + previous.CUSTOMER_IC + "</strong> To "
                                + current.CUSTOMER_IC + "</td></tr>";
            }


            if (!(retrunValue(current.CUSTOMER_SEGMENT).Equals(retrunValue(previous.CUSTOMER_SEGMENT))))
            {
                index_id.Add(37);
                result = result + "<tr><td> CUSTOMER SEGMENT <strong>"
                                + previous.CUSTOMER_SEGMENT + "</strong> To "
                                + current.CUSTOMER_SEGMENT + "</td></tr>";
            }


            if (!(retrunValue(current.CUSTOMER_TYPE).Equals(retrunValue(previous.CUSTOMER_TYPE))))
            {
                index_id.Add(38);
                result = result + "<tr><td> CUSTOMER TYPE <strong>"
                                + previous.CUSTOMER_TYPE + "</strong> To "
                                + current.CUSTOMER_TYPE + "</td></tr>";
            }


            if (!(retrunValue(current.ORIGINATING_BRANCH).Equals(retrunValue(previous.ORIGINATING_BRANCH))))
            {
                index_id.Add(39);
                result = result + "<tr><td> ORIGINATING BRANCH changed <strong>"
                                + previous.ORIGINATING_BRANCH + "</strong> To "
                                + current.ORIGINATING_BRANCH + "</td></tr>";
            }

            if (!(retrunValue(current.OPERATING_INSTRUCTION).Equals(retrunValue(previous.OPERATING_INSTRUCTION))))
            {
                index_id.Add(40);
                result = result + "<tr><td> OPERATING INSTRUCTION changed <strong>"
                                + previous.OPERATING_INSTRUCTION + "</strong> To "
                                + current.OPERATING_INSTRUCTION + "</td></tr>";
            }


            if (!(retrunValue(current.TYPE_OF_ACCOUNT).Equals(retrunValue(previous.TYPE_OF_ACCOUNT))))
            {
                index_id.Add(41);
                decimal curr_acc_typ = 0;
                decimal prev_acc_typ =0 ;
                if (current.TYPE_OF_ACCOUNT is string)
                {
                }
                else
                {
                    curr_acc_typ = Convert.ToDecimal(current.TYPE_OF_ACCOUNT);
                }
                if (previous.TYPE_OF_ACCOUNT is string)
                {
                }
                else
                {
                    prev_acc_typ = Convert.ToDecimal(previous.TYPE_OF_ACCOUNT);
                }
                var previous_acc = getAccountTypeName(prev_acc_typ);
                var current_acc = getAccountTypeName(curr_acc_typ); 
                index_id.Add(41);
                result = result + "<tr><td> Account Type changed <strong>"
                                + previous_acc + "</strong> To "
                                + current_acc + "</td></tr>";
            }
            if (!(retrunValue(current.BRANCH).Equals(retrunValue(previous.BRANCH))))
            {
                string prev_brc_name = "";
                string curr_brc_name = "";
                index_id.Add(42);
                var previous_branch = getBranch(previous.BRANCH);
                var current_branch = getBranch(current.BRANCH);
                if (previous_branch == null)
                {
                     prev_brc_name = "";
                }
                else
                {
                     prev_brc_name = previous_branch.BRANCH_NAME;
                }
                if (current_branch == null)
                {
                     curr_brc_name = "";
                }
                else
                {
                     curr_brc_name = current_branch.BRANCH_NAME;
                }
                result = result + "<tr><td> Branch changed from <strong>"
                                +  prev_brc_name + "</strong> To "
                                +  curr_brc_name + "</td></tr>";
            }

            if (!(retrunValue(current.BRANCH_CLASS).Equals(retrunValue(previous.BRANCH_CLASS))))
            {
                index_id.Add(43);
                var previous_branch_class = getBranchClass(previous.BRANCH_CLASS);
                var current_branch_class = getBranchClass(current.BRANCH_CLASS);
                result = result + "<tr><td> Branch Class changed from <strong>"
                                + previous_branch_class.CLASS + "</strong> To "
                                + current_branch_class.CLASS + "</td></tr>";
            }

            if (!(retrunValue(current.BUSINESS_DIVISION).Equals(retrunValue(previous.BUSINESS_DIVISION))))
            {
                index_id.Add(44);
                result = result + "<tr><td> LGA of residence changed from <strong>"
                            + retrunValue(previous.BUSINESS_DIVISION) + "</strong> To "
                            + retrunValue(current.BUSINESS_DIVISION) + "</td></tr>";
            }

            if (!(retrunValue(current.BUSINESS_SEGMENT).Equals(retrunValue(previous.BUSINESS_SEGMENT))))
            {
                index_id.Add(45);
                var previous_bus_seg = getBusSegment(Convert.ToDecimal(previous.BUSINESS_SEGMENT));
                var current_bus_seg = getBusSegment(Convert.ToDecimal(current.BUSINESS_SEGMENT));
                result = result + "<tr><td> Branch Class changed from <strong>"
                                + previous_bus_seg.SEGMENT + "</strong> To "
                                + current_bus_seg.SEGMENT + "</td></tr>";
            }

            if (!(retrunValue(current.BUSINESS_SIZE).Equals(retrunValue(previous.BUSINESS_SIZE))))
            {
                index_id.Add(46);
                var previous_bus_seg = getBusinessSize(previous.BUSINESS_SIZE);
                var current_bus_seg = getBusinessSize(current.BUSINESS_SIZE);
                result = result + "<tr><td> Branch Size changed from <strong>"
                                + previous_bus_seg.SIZE_RANGE + "</strong> To "
                                + current_bus_seg.SIZE_RANGE + "</td></tr>";
            }

            if (!(retrunValue(current.BVN_NUMBER).Equals(retrunValue(previous.BVN_NUMBER))))
            {
                index_id.Add(47);
                result = result + "<tr><td>BVN Number Changed <strong>"
                                + previous.BVN_NUMBER + "</strong> To "
                                + current.BVN_NUMBER + "</td></tr>";
            }

            if (!(retrunValue(current.CAV_REQUIRED).Equals(retrunValue(previous.CAV_REQUIRED))))
            {
                index_id.Add(48);
                result = result + "<tr><td> CAV REQUIRED changed from <strong>"
                                + previous.CAV_REQUIRED + "</strong> To "
                                + current.CAV_REQUIRED + "</td></tr>";
            }


            return result;
        }
         
        public string compareServiceReq(CDMA_ACCT_SERVICES_REQUIRED current, CDMA_ACCT_SERVICES_REQUIRED previous)
        {
            string result = "";
            
            //AM HERE
            if (!(retrunValue(current.CARD_PREFERENCE).Equals(retrunValue(previous.CARD_PREFERENCE))))
            {
                index_id.Add(51);
                result = result + "<tr><td> CARD PREFERENCE changed<strong>"
                                + confirmYesNo(previous.CARD_PREFERENCE) + "</strong> To "
                                + confirmYesNo(current.CARD_PREFERENCE) + "</td></tr>";
            }


            if (!(retrunValue(current.ELECTRONIC_BANKING_PREFERENCE).Equals(retrunValue(previous.ELECTRONIC_BANKING_PREFERENCE))))
            {
                index_id.Add(52);
                result = result + "<tr><td> CUSTOMER SEGMENT <strong>"
                                + confirmYesNo(previous.ELECTRONIC_BANKING_PREFERENCE) + "</strong> To "
                                + confirmYesNo(current.ELECTRONIC_BANKING_PREFERENCE) + "</td></tr>";
            }


            if (!(retrunValue(current.STATEMENT_PREFERENCES).Equals(retrunValue(previous.STATEMENT_PREFERENCES))))
            {
                index_id.Add(53);
                result = result + "<tr><td> STATEMENT PREFERENCES Changed <strong>"
                                + confirmYesNo(previous.STATEMENT_PREFERENCES) + "</strong> To "
                                + confirmYesNo(current.STATEMENT_PREFERENCES) + "</td></tr>";
            }
            

            if (!(retrunValue(current.TRANSACTION_ALERT_PREFERENCE).Equals(retrunValue(previous.TRANSACTION_ALERT_PREFERENCE))))
            {
                index_id.Add(54);
                result = result + "<tr><td> TRANSACTION ALERT PREFERENCE changed <strong>"
                                + confirmYesNo(previous.TRANSACTION_ALERT_PREFERENCE )+ "</strong> To "
                                + confirmYesNo(current.TRANSACTION_ALERT_PREFERENCE) + "</td></tr>";
            }

            if (!(retrunValue(current.STATEMENT_FREQUENCY).Equals(retrunValue(previous.STATEMENT_FREQUENCY))))
            {
                index_id.Add(55);
                result = result + "<tr><td> STATEMENT FREQUENCY changed <strong>"
                                + confirmYesNo(previous.STATEMENT_FREQUENCY) + "</strong> To "
                                + confirmYesNo(current.STATEMENT_FREQUENCY) + "</td></tr>";
            }


            if (!(retrunValue(current.CHEQUE_BOOK_REQUISITION).Equals(retrunValue(previous.CHEQUE_BOOK_REQUISITION))))
            { 
                index_id.Add(56);
                result = result + "<tr><td> CHEQUE BOOK REQUISITION changed <strong>"
                                + confirmYesNo(previous.CHEQUE_BOOK_REQUISITION) + "</strong> To "
                                + confirmYesNo(current.CHEQUE_BOOK_REQUISITION) + "</td></tr>";
            }
 
            if (!(retrunValue(current.CHEQUE_LEAVES_REQUIRED).Equals(retrunValue(previous.CHEQUE_LEAVES_REQUIRED))))
            {
                index_id.Add(57);
                    result = result + "<tr><td> CHEQUE LEAVES REQUIRED changed from <strong>"
                                + confirmYesNo(previous.CHEQUE_LEAVES_REQUIRED) + "</strong> To "
                                + confirmYesNo(current.CHEQUE_LEAVES_REQUIRED) + "</td></tr>";
            }
 
            if (!(retrunValue(current.CHEQUE_CONFIRMATION).Equals(retrunValue(previous.CHEQUE_CONFIRMATION))))
            {
                index_id.Add(58);
                    result = result + "<tr><td>CHEQUE CONFIRMATION changed from <strong>"
                                + confirmYesNo(retrunValue(previous.CHEQUE_CONFIRMATION)) + "</strong> To "
                                + confirmYesNo(retrunValue(current.CHEQUE_CONFIRMATION)) + "</td></tr>";
            }

            if (!(retrunValue(current.CHEQUE_CONFIRMATION_THRESHOLD).Equals(retrunValue(previous.CHEQUE_CONFIRMATION_THRESHOLD))))
            {
                index_id.Add(59); 
                result = result + "<tr><td> CHEQUE CONFIRMATION THRESHOLD changed from <strong>"
                                + previous.CHEQUE_CONFIRMATION_THRESHOLD + "</strong> To "
                                + current.CHEQUE_CONFIRMATION_THRESHOLD + "</td></tr>";
            }

            if (!(retrunValue(current.CHEQUE_CONFIRM_THRESHOLD_RANGE).Equals(retrunValue(previous.CHEQUE_CONFIRM_THRESHOLD_RANGE))))
            {
                index_id.Add(49);

                var previous_CT = confirmYesNo(previous.CHEQUE_CONFIRM_THRESHOLD_RANGE);
                var current_CT = confirmYesNo(current.CHEQUE_CONFIRM_THRESHOLD_RANGE);

                result = result + "<tr><td> CHEQUE CONFIRM THRESHLDRANGE changed <strong>"
                                + previous_CT + "</strong> To "
                                + previous_CT + "</td></tr>";
            }



            if (!(retrunValue(current.ONLINE_TRANSFER_LIMIT_RANGE).Equals(retrunValue(previous.ONLINE_TRANSFER_LIMIT_RANGE))))
            {
                index_id.Add(50);
                result = result + "<tr><td> ONLINE TRANSFER LIMIT RANGE changed <strong>"
                                + previous.ONLINE_TRANSFER_LIMIT_RANGE + "</strong> To "
                                + current.ONLINE_TRANSFER_LIMIT_RANGE + "</td></tr>";
            }

            if (!(retrunValue(current.ONLINE_TRANSFER_LIMIT).Equals(retrunValue(previous.ONLINE_TRANSFER_LIMIT))))
            {
                index_id.Add(60);
                result = result + "<tr><td> ONLINE TRANSFER LIMIT changed from <strong>"
                                + confirmYesNo(previous.ONLINE_TRANSFER_LIMIT) + "</strong> To "
                                + confirmYesNo(current.ONLINE_TRANSFER_LIMIT) + "</td></tr>";
            }

            if (!(retrunValue(current.TOKEN).Equals(retrunValue(previous.TOKEN))))
            {
                index_id.Add(61);
                result = result + "<tr><td>TOKEN changed from <strong>"
                                + previous.TOKEN + "</strong> To "
                                + current.TOKEN + "</td></tr>";
            }

            if (!(retrunValue(current.ACCOUNT_SIGNATORY).Equals(retrunValue(previous.ACCOUNT_SIGNATORY))))
            {
                index_id.Add(62);
                result = result + "<tr><td> ACCOUNT SIGNATORY changed from <strong>"
                                + confirmYesNo(previous.ACCOUNT_SIGNATORY) + "</strong> To "
                                + confirmYesNo(current.ACCOUNT_SIGNATORY) + "</td></tr>";
            }

            if (!(retrunValue(current.SECOND_SIGNATORY).Equals(retrunValue(previous.SECOND_SIGNATORY))))
            {
                index_id.Add(63);
                 
                result = result + "<tr><td> SECOND SIGNATORY changed <strong>"
                                + confirmYesNo(previous.SECOND_SIGNATORY) + "</strong> To "
                                + confirmYesNo(previous.SECOND_SIGNATORY) + "</td></tr>";
            }

        
            return result;
        }









        public string compareAddress(CDMA_INDIVIDUAL_ADDRESS_DETAIL current, CDMA_INDIVIDUAL_ADDRESS_DETAIL previous)
        {

            string result = "";
            if (!(retrunValue(current.CITY_TOWN_OF_RESIDENCE).Equals(retrunValue(previous.CITY_TOWN_OF_RESIDENCE)))) {
                index_id.Add(28);
                result = result + "<tr><td> City Town of Residence changed from <strong>" 
                                + previous.CITY_TOWN_OF_RESIDENCE + "</strong> To "
                                + current.CITY_TOWN_OF_RESIDENCE + "</td></tr>";
            }
            if (!(retrunValue(current.COUNTRY_OF_RESIDENCE).Equals(retrunValue(previous.COUNTRY_OF_RESIDENCE))))
            {
                index_id.Add(29);
                var previous_country = countryById(Convert.ToDecimal(previous.COUNTRY_OF_RESIDENCE));
                var current_country = countryById(Convert.ToDecimal(current.COUNTRY_OF_RESIDENCE));
                result = result + "<tr><td> Country of residence changed from <strong>"
                                + previous_country.COUNTRY_NAME + "</strong> To "
                                + current_country.COUNTRY_NAME + "</td></tr>";
            }

            //if (!(current.COUNTRY_OF_RESIDENCE.Equals(previous.COUNTRY_OF_RESIDENCE)))
            //{
            //    index_id.Add(30);
            //    var previous_country = countryById(Convert.ToDecimal(previous.COUNTRY_OF_RESIDENCE));
            //    var current_country = countryById(Convert.ToDecimal(current.COUNTRY_OF_RESIDENCE));
            //    result = result + "Country of residence changed from <strong>"
            //                    + previous_country.COUNTRY_NAME + "</strong> To "
            //                    + current_country.COUNTRY_NAME;
            //}

            if (!(retrunValue(current.LGA_OF_RESIDENCE).Equals(retrunValue(previous.LGA_OF_RESIDENCE))))
            {
                index_id.Add(30);
                var previous_lga_of_residence = getloadlga(Convert.ToDecimal(previous.LGA_OF_RESIDENCE));
                var current_lga_of_residence = getloadlga(Convert.ToDecimal(current.LGA_OF_RESIDENCE));
                result = result + "<tr><td> LGA of residence changed from <strong>"
                                + previous_lga_of_residence.LGA_NAME + "</strong> To "
                                + current_lga_of_residence.LGA_NAME + "</td></tr>";
            }

            if (!(retrunValue(current.NEAREST_BUS_STOP_LANDMARK).Equals(retrunValue(previous.NEAREST_BUS_STOP_LANDMARK))))
            {
                index_id.Add(31);
                result = result + "<tr><td> Nearest bus stop changed from <strong>"
                                + previous.NEAREST_BUS_STOP_LANDMARK + "</strong> To "
                                + current.NEAREST_BUS_STOP_LANDMARK + "</td></tr>";
            }

            if (!(retrunValue(current.RESIDENCE_OWNED_OR_RENT).Equals(retrunValue(previous.RESIDENCE_OWNED_OR_RENT))))
            {
                index_id.Add(32);
                result = result + "<tr><td> Residence status changed from <strong>"
                                + previous.RESIDENCE_OWNED_OR_RENT + "</strong> To "
                                + current.RESIDENCE_OWNED_OR_RENT + "</td></tr>";
            }

            if (!(retrunValue(current.RESIDENTIAL_ADDRESS).Equals(retrunValue(previous.RESIDENTIAL_ADDRESS))))
            {
                index_id.Add(33);
                result = result + "<tr><td> Residential address changed from <strong>"
                                + previous.RESIDENTIAL_ADDRESS + "</strong> To "
                                + current.RESIDENTIAL_ADDRESS + "</td></tr>";
            }

            if (!(retrunValue(current.STATE_OF_RESIDENCE).Equals(retrunValue(previous.STATE_OF_RESIDENCE))))
            {
                index_id.Add(34);
                var previous_state_of_residence = getState(Convert.ToDecimal(previous.STATE_OF_RESIDENCE));
                var previous_of_residence = getState(Convert.ToDecimal(current.STATE_OF_RESIDENCE));
                result = result + "<tr><td> State of Residence changed from <strong>"
                                + previous_state_of_residence.STATE_NAME + "</strong> To "
                                + previous_of_residence.STATE_NAME + "</td></tr>";
            }

            if (!(retrunValue(current.ZIP_POSTAL_CODE).Equals(retrunValue(previous.ZIP_POSTAL_CODE))))
            {
                index_id.Add(35);
                result = result + "<tr><td> State of Residence changed from <strong>"
                                + previous.ZIP_POSTAL_CODE + "</strong> To "
                                + current.ZIP_POSTAL_CODE + "</td></tr>";
            }
            

            return result;
        }




        public CDMA_ACCT_SERVICES_LOG ConvertToServiceRequiredLog(CDMA_ACCT_SERVICES_REQUIRED accountInfo)
        {
            var dateAndTime = DateTime.Now;
            var identity = ((CustomPrincipal)User).CustomIdentity;
            CDMA_ACCT_SERVICES_LOG cDMA_ACCT_SERVICES_LOG = new CDMA_ACCT_SERVICES_LOG();

            cDMA_ACCT_SERVICES_LOG.CREATED_BY = accountInfo.CREATED_BY;
            cDMA_ACCT_SERVICES_LOG.CREATED_DATE = accountInfo.CREATED_DATE;
            cDMA_ACCT_SERVICES_LOG.IP_ADDRESS = accountInfo.IP_ADDRESS;
            cDMA_ACCT_SERVICES_LOG.CUSTOMER_NO = accountInfo.CUSTOMER_NO;
            cDMA_ACCT_SERVICES_LOG.LAST_MODIFIED_DATE = accountInfo.LAST_MODIFIED_DATE;
            cDMA_ACCT_SERVICES_LOG.ACCOUNT_NUMBER = accountInfo.ACCOUNT_NUMBER;
            cDMA_ACCT_SERVICES_LOG.CARD_PREFERENCE = accountInfo.CARD_PREFERENCE;
            cDMA_ACCT_SERVICES_LOG.ELECTRONIC_BANKING_PREFERENCE = accountInfo.ELECTRONIC_BANKING_PREFERENCE;
            cDMA_ACCT_SERVICES_LOG.STATEMENT_PREFERENCES = accountInfo.STATEMENT_PREFERENCES;

            cDMA_ACCT_SERVICES_LOG.TRANSACTION_ALERT_PREFERENCE = accountInfo.TRANSACTION_ALERT_PREFERENCE;
            cDMA_ACCT_SERVICES_LOG.STATEMENT_FREQUENCY = accountInfo.STATEMENT_FREQUENCY;
            cDMA_ACCT_SERVICES_LOG.CHEQUE_BOOK_REQUISITION = accountInfo.CHEQUE_BOOK_REQUISITION;
            cDMA_ACCT_SERVICES_LOG.CHEQUE_LEAVES_REQUIRED = accountInfo.CHEQUE_LEAVES_REQUIRED;
            cDMA_ACCT_SERVICES_LOG.CHEQUE_CONFIRMATION = accountInfo.CHEQUE_CONFIRMATION;
            cDMA_ACCT_SERVICES_LOG.CHEQUE_CONFIRMATION_THRESHOLD = accountInfo.CHEQUE_CONFIRMATION_THRESHOLD;

            cDMA_ACCT_SERVICES_LOG.ONLINE_TRANSFER_LIMIT = accountInfo.ONLINE_TRANSFER_LIMIT;
            cDMA_ACCT_SERVICES_LOG.TOKEN = accountInfo.TOKEN;
            cDMA_ACCT_SERVICES_LOG.ACCOUNT_SIGNATORY = accountInfo.ACCOUNT_SIGNATORY;
            cDMA_ACCT_SERVICES_LOG.SECOND_SIGNATORY = accountInfo.SECOND_SIGNATORY;
            cDMA_ACCT_SERVICES_LOG.AUTHORISED = accountInfo.AUTHORISED;
            cDMA_ACCT_SERVICES_LOG.AUTHORISED = "U"; 

            return cDMA_ACCT_SERVICES_LOG;

        }



        public CDMA_ACCOUNT_INFO_LOG ConvertToAccInfoLog(CDMA_ACCOUNT_INFO accountInfo)
        {
            var dateAndTime = DateTime.Now;
            var identity = ((CustomPrincipal)User).CustomIdentity;
            CDMA_ACCOUNT_INFO_LOG cDMA_ACCOUNT_INFO_LOG = new CDMA_ACCOUNT_INFO_LOG();

            cDMA_ACCOUNT_INFO_LOG.IP_ADDRESS = this.Request.ServerVariables["REMOTE_ADDR"];
            cDMA_ACCOUNT_INFO_LOG.CUSTOMER_NO = accountInfo.CUSTOMER_NO;
            cDMA_ACCOUNT_INFO_LOG.LAST_MODIFIED_BY = accountInfo.LAST_MODIFIED_BY;
            cDMA_ACCOUNT_INFO_LOG.LAST_MODIFIED_DATE = accountInfo.LAST_MODIFIED_DATE;
            cDMA_ACCOUNT_INFO_LOG.TYPE_OF_ACCOUNT = accountInfo.TYPE_OF_ACCOUNT;
            cDMA_ACCOUNT_INFO_LOG.BRANCH = accountInfo.BRANCH;
            cDMA_ACCOUNT_INFO_LOG.BRANCH_CLASS = accountInfo.BRANCH_CLASS;
            cDMA_ACCOUNT_INFO_LOG.BUSINESS_DIVISION = accountInfo.BUSINESS_DIVISION;
            cDMA_ACCOUNT_INFO_LOG.BUSINESS_SEGMENT = accountInfo.BUSINESS_SEGMENT;
            cDMA_ACCOUNT_INFO_LOG.BUSINESS_SIZE = accountInfo.BUSINESS_SIZE;
            cDMA_ACCOUNT_INFO_LOG.BVN_NUMBER = accountInfo.BVN_NUMBER;
            cDMA_ACCOUNT_INFO_LOG.CAV_REQUIRED = accountInfo.CAV_REQUIRED;
            //cDMA_ACCOUNT_INFO_LOG.CHEQUE_CONFIRM_THRESHLDRANGE = accountInfo.CHEQUE_CONFIRM_THRESHLDRANGE;
            //cDMA_ACCOUNT_INFO_LOG.ONLINE_TRANSFER_LIMIT_RANGE = accountInfo.ONLINE_TRANSFER_LIMIT_RANGE;
            cDMA_ACCOUNT_INFO_LOG.CUSTOMER_IC = accountInfo.CUSTOMER_IC;
            cDMA_ACCOUNT_INFO_LOG.CUSTOMER_SEGMENT = accountInfo.CUSTOMER_SEGMENT;
            cDMA_ACCOUNT_INFO_LOG.CUSTOMER_TYPE = accountInfo.CUSTOMER_TYPE;
            cDMA_ACCOUNT_INFO_LOG.OPERATING_INSTRUCTION = accountInfo.OPERATING_INSTRUCTION;
            cDMA_ACCOUNT_INFO_LOG.ORIGINATING_BRANCH = accountInfo.ORIGINATING_BRANCH;
            

            return cDMA_ACCOUNT_INFO_LOG;

        }




        public CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG ConvertToAddressLog(CDMA_INDIVIDUAL_ADDRESS_DETAIL address)
        {
            var dateAndTime = DateTime.Now;
            var identity = ((CustomPrincipal)User).CustomIdentity;
            CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG = new CDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG();
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.CREATED_BY = identity.ProfileId.ToString();
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.CREATED_DATE = dateAndTime;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.CITY_TOWN_OF_RESIDENCE = address.CITY_TOWN_OF_RESIDENCE;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.COUNTRY_OF_RESIDENCE = address.COUNTRY_OF_RESIDENCE; 
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.CUSTOMER_NO = address.CUSTOMER_NO;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.IP_ADDRESS = address.IP_ADDRESS;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.LAST_MODIFIED_BY = identity.ProfileId.ToString();
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.LAST_MODIFIED_DATE = dateAndTime;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.LGA_OF_RESIDENCE = address.LGA_OF_RESIDENCE;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.NEAREST_BUS_STOP_LANDMARK = address.NEAREST_BUS_STOP_LANDMARK;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.RESIDENCE_OWNED_OR_RENT = address.RESIDENCE_OWNED_OR_RENT;//Request["RESIDENCE_OWNED_OR_RENT"]; 
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.RESIDENTIAL_ADDRESS = address.CITY_TOWN_OF_RESIDENCE;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.STATE_OF_RESIDENCE = address.STATE_OF_RESIDENCE;
            cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG.ZIP_POSTAL_CODE = address.ZIP_POSTAL_CODE;

            return  cDMA_INDIVIDUAL_ADDRESS_DETAIL_LOG;

        }

        public CDMA_INDIVIDUAL_CONTACT_LOG ConvertToContactDataLog(CDMA_INDIVIDUAL_CONTACT_DETAIL contact_data) {

            CDMA_INDIVIDUAL_CONTACT_LOG cDMA_INDIVIDUAL_CONTACT_log = new CDMA_INDIVIDUAL_CONTACT_LOG();
            cDMA_INDIVIDUAL_CONTACT_log.CUSTOMER_NO = contact_data.CUSTOMER_NO;
            cDMA_INDIVIDUAL_CONTACT_log.CREATED_BY = contact_data.CREATED_DATE.ToString();
            cDMA_INDIVIDUAL_CONTACT_log.CREATED_DATE = contact_data.CREATED_DATE;
            cDMA_INDIVIDUAL_CONTACT_log.MAILING_ADDRESS = contact_data.MAILING_ADDRESS;
            cDMA_INDIVIDUAL_CONTACT_log.MOBILE_NO = contact_data.MOBILE_NO;
            cDMA_INDIVIDUAL_CONTACT_log.EMAIL_ADDRESS = contact_data.EMAIL_ADDRESS;
            cDMA_INDIVIDUAL_CONTACT_log.IP_ADDRESS = contact_data.IP_ADDRESS;
            cDMA_INDIVIDUAL_CONTACT_log.LAST_MODIFIED_BY = contact_data.LAST_MODIFIED_BY;

            return cDMA_INDIVIDUAL_CONTACT_log;


        }

        public CDMA_INDIVIDUAL_OTHER_DETAILS_LOG ConvertToOtherLog(CDMA_INDIVIDUAL_OTHER_DETAILS other_data)
        {
            CDMA_INDIVIDUAL_OTHER_DETAILS_LOG cDMA_INDIVIDUAL_OTHER_DETAILS_LOG = new CDMA_INDIVIDUAL_OTHER_DETAILS_LOG();
            cDMA_INDIVIDUAL_OTHER_DETAILS_LOG.CREATED_BY = other_data.CREATED_BY;
            cDMA_INDIVIDUAL_OTHER_DETAILS_LOG.CREATED_DATE = other_data.CREATED_DATE;
            cDMA_INDIVIDUAL_OTHER_DETAILS_LOG.CUSTOMER_NO = other_data.CUSTOMER_NO;
            cDMA_INDIVIDUAL_OTHER_DETAILS_LOG.IP_ADDRESS = other_data.IP_ADDRESS;
            cDMA_INDIVIDUAL_OTHER_DETAILS_LOG.LAST_MODIFIED_DATE = other_data.LAST_MODIFIED_DATE;
            cDMA_INDIVIDUAL_OTHER_DETAILS_LOG.TIN_NO = other_data.TIN_NO;

            return cDMA_INDIVIDUAL_OTHER_DETAILS_LOG;

        }

        public CDMA_INDIVIDUAL_IDENTIFICATION_LOG ConvertToIdentificationLog(CDMA_INDIVIDUAL_IDENTIFICATION identification_data)
        {
            CDMA_INDIVIDUAL_IDENTIFICATION_LOG cDMA_INDIVIDUAL_IDENTIFICATION_LOG = new CDMA_INDIVIDUAL_IDENTIFICATION_LOG();
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.AUTHORISED = identification_data.AUTHORISED;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.AUTHORISED_BY = identification_data.AUTHORISED_BY;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.AUTHORISED_DATE = identification_data.AUTHORISED_DATE;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.CREATED_BY = identification_data.CREATED_BY;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.CREATED_DATE = identification_data.CREATED_DATE;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.CUSTOMER_NO = identification_data.CUSTOMER_NO;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.IDENTIFICATION_TYPE = identification_data.IDENTIFICATION_TYPE;// Request["CDMA_IDENTIFICATION_TYPE"];
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.ID_EXPIRY_DATE = identification_data.ID_EXPIRY_DATE;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.ID_ISSUE_DATE = identification_data.ID_ISSUE_DATE;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.ID_NO = identification_data.ID_NO;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.IP_ADDRESS = identification_data.IP_ADDRESS;//.ServerVariables["REMOTE_ADDR"];
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.LAST_MODIFIED_BY = identification_data.LAST_MODIFIED_BY;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.LAST_MODIFIED_DATE = identification_data.LAST_MODIFIED_DATE;
            //cDMA_INDIVIDUAL_IDENTIFICATION_LOG.PLACE_OF_ISSUANCE = identification_data.PLACE_OF_ISSUANCE;
            cDMA_INDIVIDUAL_IDENTIFICATION_LOG.LAST_MODIFIED_BY = identification_data.LAST_MODIFIED_BY;
             
            return cDMA_INDIVIDUAL_IDENTIFICATION_LOG;

        }

        public CDMA_INDIVIDUAL_BIO_LOG ConvertToBioDataLog(CDMA_INDIVIDUAL_BIO_DATA bio_data)
        {
            var dateAndTime = DateTime.Now;
            var identity = ((CustomPrincipal)User).CustomIdentity;
            CDMA_INDIVIDUAL_BIO_LOG cDMA_INDIVIDUAL_BIO_DATA_LOG = new CDMA_INDIVIDUAL_BIO_LOG();

            //cDMA_INDIVIDUAL_BIO_DATA_LOG.AGE = bio_data.AGE;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.AUTHORISED = bio_data.AUTHORISED;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.AUTHORISED_BY = bio_data.AUTHORISED_BY;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.AUTHORISED_DATE = bio_data.AUTHORISED_DATE;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.BRANCH_CODE = bio_data.BRANCH_CODE;
            //cDMA_INDIVIDUAL_BIO_DATA_LOG.COMPLEXION = bio_data.COMPLEXION;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.COUNTRY_OF_BIRTH = bio_data.COUNTRY_OF_BIRTH; // DynamicModel.BioData.COUNTRY_OF_BIRTH;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.CREATED_BY = bio_data.CREATED_BY;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.CREATED_DATE = bio_data.CREATED_DATE;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.CUSTOMER_NO = bio_data.CUSTOMER_NO;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.DATE_OF_BIRTH = bio_data.DATE_OF_BIRTH;
            //cDMA_INDIVIDUAL_BIO_DATA_LOG.DISABILITY = bio_data.DISABILITY;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.FIRST_NAME = bio_data.FIRST_NAME;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.IP_ADDRESS = bio_data.IP_ADDRESS;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.LAST_MODIFIED_DATE = bio_data.LAST_MODIFIED_DATE;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.MARITAL_STATUS = bio_data.MARITAL_STATUS;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.MOTHER_MAIDEN_NAME = bio_data.MOTHER_MAIDEN_NAME;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.NATIONALITY = bio_data.NATIONALITY;
            //cDMA_INDIVIDUAL_BIO_DATA_LOG.NICKNAME_ALIAS = bio_data.NICKNAME_ALIAS;
            //cDMA_INDIVIDUAL_BIO_DATA_LOG.NUMBER_OF_CHILDREN = bio_data.NUMBER_OF_CHILDREN;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.OTHER_NAME = bio_data.OTHER_NAME;
            //cDMA_INDIVIDUAL_BIO_DATA_LOG.PLACE_OF_BIRTH = bio_data.PLACE_OF_BIRTH;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.RELIGION = bio_data.RELIGION;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.SEX = bio_data.SEX;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.STATE_OF_ORIGIN = bio_data.STATE_OF_ORIGIN;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.SURNAME = bio_data.SURNAME;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.TITLE = bio_data.TITLE;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.RELIGION = bio_data.RELIGION;
            cDMA_INDIVIDUAL_BIO_DATA_LOG.COUNTRY_OF_BIRTH = bio_data.COUNTRY_OF_BIRTH;

            return cDMA_INDIVIDUAL_BIO_DATA_LOG;

        }

        public string retrunValue(string x) {

            if (String.IsNullOrEmpty(x))
            {

                return " ";
            }
            else if (x == null)
            {

                return " ";
            }
            else { return x; }


          //  return " ";

        }

        public decimal retrunDecimalValue(decimal x)
        {

            if (x==null)
            {

                return 0;
            }

            return x;

        }

    }
    }

