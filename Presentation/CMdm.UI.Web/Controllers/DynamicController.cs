using CMdm.Data;
using CMdm.Entities.Domain.Customer;
using CMdm.Entities.ViewModels;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBioData(DynamicViewModel DynamicModel) {
           
            //Address details
           var authrizer = DynamicModel.AddressDetails.AUTHORISED;
           var AUTHORISED_BY = DynamicModel.AddressDetails.AUTHORISED_BY;
           var AUTHORISED_DATE = DynamicModel.AddressDetails.AUTHORISED_DATE;
           var CITY_TOWN_OF_RESIDENCE = DynamicModel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
           var COUNTRY_OF_RESIDENCE = DynamicModel.AddressDetails.COUNTRY_OF_RESIDENCE;
           var CREATED_BY = DynamicModel.AddressDetails.CREATED_BY;
           var CREATED_DATE = DynamicModel.AddressDetails.CREATED_DATE;
           var CUSTOMER_NO = DynamicModel.AddressDetails.CUSTOMER_NO;
           var IP_ADDRESS = DynamicModel.AddressDetails.IP_ADDRESS;
           var LAST_MODIFIED_BY = DynamicModel.AddressDetails.LAST_MODIFIED_BY;
           var LAST_MODIFIED_DATE = DynamicModel.AddressDetails.LAST_MODIFIED_DATE;
           var LGA_OF_RESIDENCE = DynamicModel.AddressDetails.LGA_OF_RESIDENCE;
           var NEAREST_BUS_STOP_LANDMARK = DynamicModel.AddressDetails.NEAREST_BUS_STOP_LANDMARK;
           var RESIDENCE_OWNED_OR_RENT = DynamicModel.AddressDetails.RESIDENCE_OWNED_OR_RENT;
           var RESIDENTIAL_ADDRESS = DynamicModel.AddressDetails.RESIDENTIAL_ADDRESS;
           var STATE_OF_RESIDENCE = DynamicModel.AddressDetails.STATE_OF_RESIDENCE;
           var ZIP_POSTAL_CODE = DynamicModel.AddressDetails.ZIP_POSTAL_CODE;

            // Biodata            
           var AGE = DynamicModel.BioData.AGE;
           var AUTHORISED = DynamicModel.BioData.AUTHORISED;
           var BIO_AUTHORISED_BY = DynamicModel.BioData.AUTHORISED_BY;
           var BIO_AUTHORISED_DATE = DynamicModel.BioData.AUTHORISED_DATE;
           var BRANCH_CODE = DynamicModel.BioData.BRANCH_CODE;
           var COMPLEXION = DynamicModel.BioData.COMPLEXION;
           var COUNTRY_OF_BIRTH = DynamicModel.BioData.COUNTRY_OF_BIRTH;
           var BIO_CREATED_BY = DynamicModel.BioData.CREATED_BY;
           var BIO_CREATED_DATE = DynamicModel.BioData.CREATED_DATE;
           var BIO_CUSTOMER_NO = DynamicModel.BioData.CUSTOMER_NO;
           var DATE_OF_BIRTH = DynamicModel.BioData.DATE_OF_BIRTH;
           var DISABILITY = DynamicModel.BioData.DISABILITY;
           var FIRST_NAME = DynamicModel.BioData.FIRST_NAME;
           var BIO_IP_ADDRESS = DynamicModel.BioData.IP_ADDRESS;
           var BIO_LAST_MODIFIED_DATE = DynamicModel.BioData.LAST_MODIFIED_DATE;
           var MARITAL_STATUS = DynamicModel.BioData.MARITAL_STATUS;
           var MOTHER_MAIDEN_NAME = DynamicModel.BioData.MOTHER_MAIDEN_NAME;
           var NATIONALITY = DynamicModel.BioData.NATIONALITY;
           var NICKNAME_ALIAS = DynamicModel.BioData.NICKNAME_ALIAS;
           var NUMBER_OF_CHILDREN = DynamicModel.BioData.NUMBER_OF_CHILDREN;
           var OTHER_NAME = DynamicModel.BioData.OTHER_NAME;
           var PLACE_OF_BIRTH = DynamicModel.BioData.PLACE_OF_BIRTH;
           var RELIGION = DynamicModel.BioData.RELIGION;
           var SEX = DynamicModel.BioData.SEX;
           var STATE_OF_ORIGIN = DynamicModel.BioData.STATE_OF_ORIGIN;
           var SURNAME = DynamicModel.BioData.SURNAME;
           var TITLE = DynamicModel.BioData.TITLE;


            //contact	 
          var CONTACT_AUTHORISED =  DynamicModel.contact.AUTHORISED;
          var CONTACT_AUTHORISED_BY = DynamicModel.contact.AUTHORISED_BY;
          var CONTACT_AUTHORISED_DATE = DynamicModel.contact.AUTHORISED_DATE;
          var CONTACT_CREATED_BY = DynamicModel.contact.CREATED_BY;
          var CONTACT_CREATED_DATE = DynamicModel.contact.CREATED_DATE;
          var CONTACT_CUSTOMER_NO = DynamicModel.contact.CUSTOMER_NO;
          var CONTACT_AUTHEMAIL_ADDRESSORISED = DynamicModel.contact.EMAIL_ADDRESS;
          var CONTACT_IP_ADDRESS = DynamicModel.contact.IP_ADDRESS;
          var CONTACT_LAST_MODIFIED_BY = DynamicModel.contact.LAST_MODIFIED_BY;
          var CONTACT_LAST_MODIFIED_DATE = DynamicModel.contact.LAST_MODIFIED_DATE;
          var CONTACT_MAILING_ADDRESS = DynamicModel.contact.MAILING_ADDRESS;
          var CONTACT_MOBILE_NO = DynamicModel.contact.MOBILE_NO;

            // identification 
          var identification_AUTHORISED =  DynamicModel.identification.AUTHORISED;
          var identification_AUTHORISED_BY = DynamicModel.identification.AUTHORISED_BY;
          var identification_AUTHORISED_DATE = DynamicModel.identification.AUTHORISED_DATE;
          var identification_CREATED_BY = DynamicModel.identification.CREATED_BY;
          var identification_CREATED_DATE = DynamicModel.identification.CREATED_DATE;
          var identification_CUSTOMER_NO = DynamicModel.identification.CUSTOMER_NO;
          var identification_IDENTIFICATION_TYPE = DynamicModel.identification.IDENTIFICATION_TYPE;
          var identification_ID_EXPIRY_DATE = DynamicModel.identification.ID_EXPIRY_DATE;
          var identification_ID_ISSUE_DATE = DynamicModel.identification.ID_ISSUE_DATE;
          var identification_ID_NO  = DynamicModel.identification.ID_NO;
          var identification_IP_ADDRESS = DynamicModel.identification.IP_ADDRESS;
          var identification_LAST_MODIFIED_BY = DynamicModel.identification.LAST_MODIFIED_BY;
          var identification_LAST_MODIFIED_DATE = DynamicModel.identification.LAST_MODIFIED_DATE;
          var identification_PLACE_OF_ISSUANCE = DynamicModel.identification.PLACE_OF_ISSUANCE;

            //otherdetails	 
          var otherdetails_PLACE_OF_ISSUANCE = DynamicModel.otherdetails.AUTHORISED;
          var otherdetails_PLACE_OF_AUTHORISED_BY = DynamicModel.otherdetails.AUTHORISED_BY;
          var otherdetails_PLACE_OF_AUTHORISED_DATE = DynamicModel.otherdetails.AUTHORISED_DATE;
          var otherdetails_PLACE_OF_CREATED_BY = DynamicModel.otherdetails.CREATED_BY;
          var otherdetails_PLACE_OF_CREATED_DATE = DynamicModel.otherdetails.CREATED_DATE;
          var otherdetails_PLACE_OF_CUSTOMER_NO = DynamicModel.otherdetails.CUSTOMER_NO;
          var otherdetails_PLACE_OF_IP_ADDRESS = DynamicModel.otherdetails.IP_ADDRESS;
          var otherdetails_PLACE_OF_LAST_MODIFIED_DATE = DynamicModel.otherdetails.LAST_MODIFIED_DATE;
          var otherdetails_PLACE_OF_TIN_NO = DynamicModel.otherdetails.TIN_NO;










            return PartialView("SaveBioData", DynamicModel);


        }

        public ActionResult Dataquality(string c_id, decimal branch, decimal rule,string table)
        {
            //ViewBag.TRANSACTIONTYPE = new SelectList(db.TranxType.ToList(), "TRANXID", "TRANXDESC");

            switch (table)
            {
                case "CDMA_INDIVIDUAL_BIO_DATA":
                    if (c_id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    cDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_BIO_DATA = db.CDMA_INDIVIDUAL_BIO_DATA.Find(c_id);
                    ViewBag.record = cDMA_INDIVIDUAL_BIO_DATA;

                    CDMA_INDIVIDUAL_ADDRESS_DETAIL cDMA_INDIVIDUAL_ADDRESS_DETAIL = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Find(c_id);
                    CDMA_INDIVIDUAL_CONTACT_DETAIL cDMA_INDIVIDUAL_CONTACT_DETAIL = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Find(c_id);
                    CDMA_INDIVIDUAL_IDENTIFICATION cDMA_INDIVIDUAL_IDENTIFICATION = db.CDMA_INDIVIDUAL_IDENTIFICATION.Find(c_id);
                    CDMA_INDIVIDUAL_OTHER_DETAILS cDMA_INDIVIDUAL_OTHER_DETAILS = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Find(c_id);
                    //save current data into session variable for future usage
                    this.Session["cDMA_INDIVIDUAL_BIO_DATA"] = cDMA_INDIVIDUAL_BIO_DATA;
                    this.Session["cDMA_INDIVIDUAL_ADDRESS_DETAIL"] = cDMA_INDIVIDUAL_ADDRESS_DETAIL;
                    this.Session["cDMA_INDIVIDUAL_CONTACT_DETAIL"] = cDMA_INDIVIDUAL_CONTACT_DETAIL;
                    this.Session["cDMA_INDIVIDUAL_IDENTIFICATION"] = cDMA_INDIVIDUAL_IDENTIFICATION;
                    this.Session["cDMA_INDIVIDUAL_OTHER_DETAILS"] = cDMA_INDIVIDUAL_OTHER_DETAILS;

                    if (cDMA_INDIVIDUAL_BIO_DATA == null)
                    {
                        return HttpNotFound();
                    }

                    var viewModel = new DynamicViewModel(); //Create an instance of the above view model
                    viewModel.BioData = cDMA_INDIVIDUAL_BIO_DATA;  
                    viewModel.AddressDetails = cDMA_INDIVIDUAL_ADDRESS_DETAIL;
                    viewModel.contact = cDMA_INDIVIDUAL_CONTACT_DETAIL;
                    viewModel.identification = cDMA_INDIVIDUAL_IDENTIFICATION;
                    viewModel.otherdetails = cDMA_INDIVIDUAL_OTHER_DETAILS;
                    return PartialView("EditCustomer", viewModel); 
 
                case "Beet Armyworm":
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
    }
}

