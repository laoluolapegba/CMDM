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
 

namespace CMdm.UI.Web.Controllers 
{
    public class IndividualCustomerController : BaseController
    {
        private AppDbContext _db = new AppDbContext();
        private IDqQueService _dqQueService;
        public IndividualCustomerController()
        {
            //bizrule = new DQQueBiz();
            _dqQueService = new DqQueService();
        }



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
            //get all changed columns

            var changeId = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_BIO_DATA" &&  a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId); //.Select(a=>a.PROPERTYNAME);
            var indvdmodel = (from c in _db.CDMA_INDIVIDUAL_BIO_DATA
                         where c.CUSTOMER_NO == querecord.CUST_ID
                              where c.AUTHORISED == "U"
                              select new IndividualBioDataModel
                         {
                             CUSTOMER_NO = c.CUSTOMER_NO,
                             TITLE = c.TITLE,
                             SURNAME = c.SURNAME,
                             DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                             FIRST_NAME = c.FIRST_NAME,
                             OTHER_NAME = c.OTHER_NAME,
                             NICKNAME_ALIAS = c.NICKNAME_ALIAS,
                             COUNTRY_OF_BIRTH = c.COUNTRY_OF_BIRTH,
                             PLACE_OF_BIRTH = c.PLACE_OF_BIRTH,
                             SEX = c.SEX,
                             AGE = c.AGE,
                             MARITAL_STATUS = c.MARITAL_STATUS,
                             NATIONALITY = c.NATIONALITY,
                             STATE_OF_ORIGIN = c.STATE_OF_ORIGIN,
                             MOTHER_MAIDEN_NAME = c.MOTHER_MAIDEN_NAME,
                             DISABILITY = c.DISABILITY,
                             COMPLEXION = c.COMPLEXION,
                             NUMBER_OF_CHILDREN = c.NUMBER_OF_CHILDREN,
                             RELIGION = c.RELIGION,                             
                             LastUpdatedby = c.LAST_MODIFIED_BY,
                             LastUpdatedDate = c.LAST_MODIFIED_DATE,
                             LastAuthdby = c.AUTHORISED_BY,
                             LastAuthDate = c.AUTHORISED_DATE,
                             ExceptionId = querecord.EXCEPTION_ID
                         }).FirstOrDefault();

            var changeId2 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_ADDRESS_DETAIL" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet2 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId2);

            var addressmodel = (from c in _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL
                                where c.CUSTOMER_NO == querecord.CUST_ID
                                where c.AUTHORISED == "U"
                                select new IndividualAddressDetails
                                  {
                                    CUSTOMER_NO =c.CUSTOMER_NO,
                                    CITY_TOWN_OF_RESIDENCE = c.CITY_TOWN_OF_RESIDENCE,
                                    COUNTRY_OF_RESIDENCE =  c.COUNTRY_OF_RESIDENCE,
                                    LGA_OF_RESIDENCE = c.LGA_OF_RESIDENCE,
                                    NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
                                    RESIDENCE_OWNED_OR_RENT = c.RESIDENCE_OWNED_OR_RENT,
                                    RESIDENTIAL_ADDRESS = c.RESIDENTIAL_ADDRESS,
                                    STATE_OF_RESIDENCE = c.STATE_OF_RESIDENCE,
                                    ZIP_POSTAL_CODE =c.ZIP_POSTAL_CODE
                                  }).FirstOrDefault();

            var changeId3 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_IDENTIFICATION" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet3 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId3);

            var idmodel = (from c in _db.CDMA_INDIVIDUAL_IDENTIFICATION
                                where c.CUSTOMER_NO == querecord.CUST_ID
                                where c.AUTHORISED == "U"
                                select new individualIDDetail
                                {
                                    CUSTOMER_NO = c.CUSTOMER_NO,
                                    IDENTIFICATION_TYPE = c.IDENTIFICATION_TYPE,
                                    ID_EXPIRY_DATE = c.ID_EXPIRY_DATE,
                                    ID_ISSUE_DATE = c.ID_ISSUE_DATE,
                                    ID_NO = c.ID_NO,
                                    PLACE_OF_ISSUANCE = c.PLACE_OF_ISSUANCE,
                                    
                                }).FirstOrDefault();

            var changeId4 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_OTHER_DETAILS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet4 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId3);

            var otherdetailsmodel = (from c in _db.CDMA_INDIVIDUAL_OTHER_DETAILS
                           where c.CUSTOMER_NO == querecord.CUST_ID
                           where c.AUTHORISED == "U"
                           select new OtherDetails
                           {
                               CUSTOMER_NO = c.CUSTOMER_NO,
                               TIN_NO = c.TIN_NO,
                           }).FirstOrDefault();
            var changeId5 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_CONTACT_DETAIL" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet5 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId3);

            var contactmodel = (from c in _db.CDMA_INDIVIDUAL_CONTACT_DETAIL
                                     where c.CUSTOMER_NO == querecord.CUST_ID
                                     where c.AUTHORISED == "U"
                                     select new IndividualContactDetails
                                     {
                                         CUSTOMER_NO = c.CUSTOMER_NO,
                                         EMAIL_ADDRESS = c.EMAIL_ADDRESS,
                                         MAILING_ADDRESS = c.MAILING_ADDRESS,
                                         MOBILE_NO = c.MOBILE_NO,
                                     }).FirstOrDefault();


            var model = new BiodataInfoViewModel();
            model.BioData = indvdmodel;
            model.AddressDetails = addressmodel;
            model.identification = idmodel;
            model.otherdetails = otherdetailsmodel;
            model.contact = contactmodel;

            foreach (var item in model.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
            {
                foreach (var item2 in changedSet)
                {
                    if (item2.PROPERTYNAME == item.Name)
                    {
                        ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                    }
                }
                //props.Add(item.Name);

            }
            //var matchItems = props.Intersect(changedSet);
            model.BioData.ReadOnlyForm = "True";
            //PrepareModel(model);
            return View(model);
        }



        // POST: MdmCatalogs/Edit/5
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
                    int records = db.CDMA_INDIVIDUAL_BIO_DATA.Count(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
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
                            entity.NICKNAME_ALIAS = bioDatamodel.BioData.NICKNAME_ALIAS;
                            entity.DATE_OF_BIRTH = bioDatamodel.BioData.DATE_OF_BIRTH;
                            entity.PLACE_OF_BIRTH = bioDatamodel.BioData.PLACE_OF_BIRTH;
                            entity.COUNTRY_OF_BIRTH = bioDatamodel.BioData.COUNTRY_OF_BIRTH;
                            entity.SEX = bioDatamodel.BioData.SEX;
                            entity.AGE = bioDatamodel.BioData.AGE;
                            entity.MARITAL_STATUS = bioDatamodel.BioData.MARITAL_STATUS;
                            entity.NATIONALITY = bioDatamodel.BioData.NATIONALITY;
                            entity.STATE_OF_ORIGIN = bioDatamodel.BioData.STATE_OF_ORIGIN;
                            entity.MOTHER_MAIDEN_NAME = bioDatamodel.BioData.MOTHER_MAIDEN_NAME;
                            entity.DISABILITY = bioDatamodel.BioData.DISABILITY;
                            entity.COMPLEXION = bioDatamodel.BioData.COMPLEXION;
                            entity.NUMBER_OF_CHILDREN = bioDatamodel.BioData.NUMBER_OF_CHILDREN;
                            entity.RELIGION = bioDatamodel.BioData.RELIGION;

                            entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            entity.LAST_MODIFIED_DATE = DateTime.Now;
                            db.CDMA_INDIVIDUAL_BIO_DATA.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject);


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
                            entity.NICKNAME_ALIAS = bioDatamodel.BioData.NICKNAME_ALIAS;
                            // entity.LAST_MODIFIED_BY = bioDatamodel.BioData.LAST_MODIFIED_BY;
                            entity.DATE_OF_BIRTH = bioDatamodel.BioData.DATE_OF_BIRTH;
                            entity.PLACE_OF_BIRTH = bioDatamodel.BioData.PLACE_OF_BIRTH;
                            entity.COUNTRY_OF_BIRTH = bioDatamodel.BioData.COUNTRY_OF_BIRTH;
                            entity.SEX = bioDatamodel.BioData.SEX;
                            entity.AGE = bioDatamodel.BioData.AGE;
                            entity.MARITAL_STATUS = bioDatamodel.BioData.MARITAL_STATUS;
                            entity.NATIONALITY = bioDatamodel.BioData.NATIONALITY;
                            entity.STATE_OF_ORIGIN = bioDatamodel.BioData.STATE_OF_ORIGIN;
                            entity.MOTHER_MAIDEN_NAME = bioDatamodel.BioData.MOTHER_MAIDEN_NAME;
                            entity.DISABILITY = bioDatamodel.BioData.DISABILITY;
                            entity.COMPLEXION = bioDatamodel.BioData.COMPLEXION;
                            entity.NUMBER_OF_CHILDREN = bioDatamodel.BioData.NUMBER_OF_CHILDREN;
                            entity.RELIGION = bioDatamodel.BioData.RELIGION;
                            //entity.AUTHORISED = "U";

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
                            newentity.NICKNAME_ALIAS = bioDatamodel.BioData.NICKNAME_ALIAS;
                            newentity.DATE_OF_BIRTH = bioDatamodel.BioData.DATE_OF_BIRTH;
                            newentity.PLACE_OF_BIRTH = bioDatamodel.BioData.PLACE_OF_BIRTH;
                            newentity.COUNTRY_OF_BIRTH = bioDatamodel.BioData.COUNTRY_OF_BIRTH;
                            newentity.SEX = bioDatamodel.BioData.SEX;
                            newentity.AGE = bioDatamodel.BioData.AGE;
                            newentity.MARITAL_STATUS = bioDatamodel.BioData.MARITAL_STATUS;
                            newentity.NATIONALITY = bioDatamodel.BioData.NATIONALITY;
                            newentity.STATE_OF_ORIGIN = bioDatamodel.BioData.STATE_OF_ORIGIN;
                            newentity.MOTHER_MAIDEN_NAME = bioDatamodel.BioData.MOTHER_MAIDEN_NAME;
                            newentity.DISABILITY = bioDatamodel.BioData.DISABILITY;
                            newentity.COMPLEXION = bioDatamodel.BioData.COMPLEXION;
                            newentity.NUMBER_OF_CHILDREN = bioDatamodel.BioData.NUMBER_OF_CHILDREN;
                            newentity.RELIGION = bioDatamodel.BioData.RELIGION;
                            newentity.CREATED_BY = identity.ProfileId.ToString();
                            newentity.CREATED_DATE = DateTime.Now;
                            newentity.AUTHORISED = "U";
                            newentity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            db.CDMA_INDIVIDUAL_BIO_DATA.Add(newentity);
                            db.SaveChanges(); //do not track audit.


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

                            contact_entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            contact_entity.LAST_MODIFIED_DATE = DateTime.Now;
                            db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Attach(contact_entity);
                            db.Entry(contact_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject1);


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
                            //entity.AUTHORISED = "U";

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
                            new_contact_entity.CREATED_BY = identity.ProfileId.ToString();
                            new_contact_entity.CREATED_DATE = DateTime.Now;
                            new_contact_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Add(new_contact_entity);
                            db.SaveChanges(); //do not track audit.


                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", bioDatamodel.contact.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                    #endregion
                    #region AddressData
                    int records2 = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Count(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records2 > 1)
                    {
                        updateFlag = true;
                        originalObject2 = _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var Address_entity = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (Address_entity != null)
                        {
                            Address_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            Address_entity.RESIDENTIAL_ADDRESS = bioDatamodel.AddressDetails.RESIDENTIAL_ADDRESS;
                            Address_entity.CITY_TOWN_OF_RESIDENCE = bioDatamodel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
                            Address_entity.LGA_OF_RESIDENCE = bioDatamodel.AddressDetails.LGA_OF_RESIDENCE;
                            Address_entity.NEAREST_BUS_STOP_LANDMARK = bioDatamodel.AddressDetails.NEAREST_BUS_STOP_LANDMARK;
                            Address_entity.STATE_OF_RESIDENCE = bioDatamodel.AddressDetails.STATE_OF_RESIDENCE;
                            Address_entity.COUNTRY_OF_RESIDENCE = bioDatamodel.AddressDetails.COUNTRY_OF_RESIDENCE;
                            Address_entity.RESIDENCE_OWNED_OR_RENT = bioDatamodel.AddressDetails.RESIDENCE_OWNED_OR_RENT;
                            Address_entity.ZIP_POSTAL_CODE = bioDatamodel.AddressDetails.ZIP_POSTAL_CODE;

                            Address_entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            Address_entity.LAST_MODIFIED_DATE = DateTime.Now;
                            db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Attach(Address_entity);
                            db.Entry(Address_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject2);


                        }
                    }
                    else if (records2 == 1)
                    {
                        updateFlag = false;
                        var Address_entity = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject2 = _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject2 != null)
                        {
                            Address_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            Address_entity.RESIDENTIAL_ADDRESS = bioDatamodel.AddressDetails.RESIDENTIAL_ADDRESS;
                            Address_entity.CITY_TOWN_OF_RESIDENCE = bioDatamodel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
                            Address_entity.LGA_OF_RESIDENCE = bioDatamodel.AddressDetails.LGA_OF_RESIDENCE;
                            Address_entity.NEAREST_BUS_STOP_LANDMARK = bioDatamodel.AddressDetails.NEAREST_BUS_STOP_LANDMARK;
                            Address_entity.STATE_OF_RESIDENCE = bioDatamodel.AddressDetails.STATE_OF_RESIDENCE;
                            Address_entity.COUNTRY_OF_RESIDENCE = bioDatamodel.AddressDetails.COUNTRY_OF_RESIDENCE;
                            Address_entity.RESIDENCE_OWNED_OR_RENT = bioDatamodel.AddressDetails.RESIDENCE_OWNED_OR_RENT;
                            Address_entity.ZIP_POSTAL_CODE = bioDatamodel.AddressDetails.ZIP_POSTAL_CODE;

                            db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Attach(Address_entity);
                            db.Entry(Address_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject2);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var new_Address_entity = new CDMA_INDIVIDUAL_ADDRESS_DETAIL();
                            new_Address_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            new_Address_entity.RESIDENTIAL_ADDRESS = bioDatamodel.AddressDetails.RESIDENTIAL_ADDRESS;
                            new_Address_entity.CITY_TOWN_OF_RESIDENCE = bioDatamodel.AddressDetails.CITY_TOWN_OF_RESIDENCE;
                            new_Address_entity.LGA_OF_RESIDENCE = bioDatamodel.AddressDetails.LGA_OF_RESIDENCE;
                            new_Address_entity.NEAREST_BUS_STOP_LANDMARK = bioDatamodel.AddressDetails.NEAREST_BUS_STOP_LANDMARK;
                            new_Address_entity.STATE_OF_RESIDENCE = bioDatamodel.AddressDetails.STATE_OF_RESIDENCE;
                            new_Address_entity.COUNTRY_OF_RESIDENCE = bioDatamodel.AddressDetails.COUNTRY_OF_RESIDENCE;
                            new_Address_entity.RESIDENCE_OWNED_OR_RENT = bioDatamodel.AddressDetails.RESIDENCE_OWNED_OR_RENT;
                            new_Address_entity.ZIP_POSTAL_CODE = bioDatamodel.AddressDetails.ZIP_POSTAL_CODE;

                            new_Address_entity.CREATED_BY = identity.ProfileId.ToString();
                            new_Address_entity.CREATED_DATE = DateTime.Now;
                            new_Address_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Add(new_Address_entity);
                            db.SaveChanges(); //do not track audit.


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
                            ID_entity.PLACE_OF_ISSUANCE = bioDatamodel.identification.PLACE_OF_ISSUANCE;

                            ID_entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                            ID_entity.LAST_MODIFIED_DATE = DateTime.Now;
                            db.CDMA_INDIVIDUAL_IDENTIFICATION.Attach(ID_entity);
                            db.Entry(ID_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject3);


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
                            ID_entity.PLACE_OF_ISSUANCE = bioDatamodel.identification.PLACE_OF_ISSUANCE;

                            db.CDMA_INDIVIDUAL_IDENTIFICATION.Attach(ID_entity);
                            db.Entry(ID_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject3);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var new_ID_entity = new CDMA_INDIVIDUAL_IDENTIFICATION();

                            ID_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            ID_entity.IDENTIFICATION_TYPE = bioDatamodel.identification.IDENTIFICATION_TYPE;
                            ID_entity.ID_NO = bioDatamodel.identification.ID_NO;
                            ID_entity.ID_EXPIRY_DATE = bioDatamodel.identification.ID_EXPIRY_DATE;
                            ID_entity.ID_ISSUE_DATE = bioDatamodel.identification.ID_ISSUE_DATE;
                            ID_entity.PLACE_OF_ISSUANCE = bioDatamodel.identification.PLACE_OF_ISSUANCE;

                            new_ID_entity.CREATED_BY = identity.ProfileId.ToString();
                            new_ID_entity.CREATED_DATE = DateTime.Now;
                            new_ID_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_IDENTIFICATION.Add(new_ID_entity);
                            db.SaveChanges(); //do not track audit.


                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", bioDatamodel.identification.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                    #endregion
                    #region otherdetails
                    int records4 = db.CDMA_INDIVIDUAL_OTHER_DETAILS.Count(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);  // && o.AUTHORISED == "U" && o.LAST_MODIFIED_BY == identity.ProfileId.ToString()
                    //if there are more than one records, the 'U' one is the edited one
                    if (records4 > 1)
                    {
                        updateFlag = true;
                        originalObject4 = _db.CDMA_INDIVIDUAL_OTHER_DETAILS.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U").FirstOrDefault();

                        var Other_entity = db.CDMA_INDIVIDUAL_OTHER_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "U");

                        if (Other_entity != null)
                        {
                            Other_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            Other_entity.TIN_NO = bioDatamodel.otherdetails.TIN_NO;

                            Other_entity.LAST_MODIFIED_DATE = DateTime.Now;
                            db.CDMA_INDIVIDUAL_OTHER_DETAILS.Attach(Other_entity);
                            db.Entry(Other_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject4);


                        }
                    }
                    else if (records4 == 1)
                    {
                        updateFlag = false;
                        var Other_entity = db.CDMA_INDIVIDUAL_OTHER_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A");
                        originalObject4 = _db.CDMA_INDIVIDUAL_OTHER_DETAILS.Where(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO && o.AUTHORISED == "A").FirstOrDefault();
                        if (originalObject4 != null)
                        {
                            Other_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            Other_entity.TIN_NO = bioDatamodel.otherdetails.TIN_NO;

                            db.CDMA_INDIVIDUAL_OTHER_DETAILS.Attach(Other_entity);
                            db.Entry(Other_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO, updateFlag, originalObject4);  //track the audit


                            // There is no 'U' status row in the table so, Add new record with mnt_status U
                            //entity.AUTHORISED = "U";
                            var new_Other_entity = new CDMA_INDIVIDUAL_OTHER_DETAILS();

                            new_Other_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            new_Other_entity.TIN_NO = bioDatamodel.otherdetails.TIN_NO;

                            new_Other_entity.CREATED_BY = identity.ProfileId.ToString();
                            new_Other_entity.CREATED_DATE = DateTime.Now;
                            new_Other_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_OTHER_DETAILS.Add(new_Other_entity);
                            db.SaveChanges(); //do not track audit.


                        }
                        else
                        {
                            string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", bioDatamodel.identification.CUSTOMER_NO);
                            ModelState.AddModelError("", errorMessage);
                        }
                    }
                    #endregion
                }





                SuccessNotification("Bio Data Updated");
                return continueEditing ? RedirectToAction("Edit", new { id = bioDatamodel.BioData.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");

            }
    
            IndBioDataPrepareModel(bioDatamodel.BioData);
            return View(bioDatamodel.BioData);
        }

        // GET: CusBioData/Create
        public ActionResult Edit(string id)
        {
            var viewModel = new BiodataInfoViewModel();
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }
            int invrecords = _db.CDMA_INDIVIDUAL_BIO_DATA.Count(o => o.CUSTOMER_NO == id);
            var indvmodel = new IndividualBioDataModel();
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
                                 NICKNAME_ALIAS = c.NICKNAME_ALIAS,
                                 DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                                 PLACE_OF_BIRTH = c.PLACE_OF_BIRTH,
                                 COUNTRY_OF_BIRTH = c.COUNTRY_OF_BIRTH,
                                 SEX = c.SEX,
                                 AGE = c.AGE,
                                 MARITAL_STATUS = c.MARITAL_STATUS,
                                 NATIONALITY = c.NATIONALITY,
                                 STATE_OF_ORIGIN = c.STATE_OF_ORIGIN,
                                 MOTHER_MAIDEN_NAME = c.MOTHER_MAIDEN_NAME,
                                 DISABILITY = c.DISABILITY,
                                 COMPLEXION = c.COMPLEXION,
                                 NUMBER_OF_CHILDREN = c.NUMBER_OF_CHILDREN,
                                 RELIGION = c.RELIGION
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
                                 NICKNAME_ALIAS = c.NICKNAME_ALIAS,
                                 DATE_OF_BIRTH = c.DATE_OF_BIRTH,
                                 PLACE_OF_BIRTH = c.PLACE_OF_BIRTH,
                                 COUNTRY_OF_BIRTH = c.COUNTRY_OF_BIRTH,
                                 SEX = c.SEX,
                                 AGE = c.AGE,
                                 MARITAL_STATUS = c.MARITAL_STATUS,
                                 NATIONALITY = c.NATIONALITY,
                                 STATE_OF_ORIGIN = c.STATE_OF_ORIGIN,
                                 MOTHER_MAIDEN_NAME = c.MOTHER_MAIDEN_NAME,
                                 DISABILITY = c.DISABILITY,
                                 COMPLEXION = c.COMPLEXION,
                                 NUMBER_OF_CHILDREN = c.NUMBER_OF_CHILDREN,
                                 RELIGION = c.RELIGION
                             }).FirstOrDefault();
            }

            IndBioDataPrepareModel(indvmodel);
            int contactrecords = _db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Count(o => o.CUSTOMER_NO == id);
            var contactmodel = new IndividualContactDetails();
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
                                    CREATED_DATE = c.CREATED_DATE,
                                    CREATED_BY = c.CREATED_BY,
                                    LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                                    LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                                    IP_ADDRESS = c.IP_ADDRESS,
                                }).FirstOrDefault();
            }
            else if (invrecords == 1)
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
                                    CREATED_DATE = c.CREATED_DATE,
                                    CREATED_BY = c.CREATED_BY,
                                    LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                                    LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                                    IP_ADDRESS = c.IP_ADDRESS,
                                }).FirstOrDefault();
            }

            int addressrecords = _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Count(o => o.CUSTOMER_NO == id);
            var addressmodel = new IndividualAddressDetails();
            if (addressrecords > 1)
            {
                addressmodel = (from c in _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL
                                where c.CUSTOMER_NO == id
                                where c.AUTHORISED == "U"
                                select new IndividualAddressDetails
                                {
                                    CUSTOMER_NO = c.CUSTOMER_NO,
                                    RESIDENTIAL_ADDRESS = c.RESIDENTIAL_ADDRESS,
                                    CITY_TOWN_OF_RESIDENCE = c.CITY_TOWN_OF_RESIDENCE,
                                    LGA_OF_RESIDENCE = c.LGA_OF_RESIDENCE,
                                    NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
                                    STATE_OF_RESIDENCE = c.STATE_OF_RESIDENCE,
                                    COUNTRY_OF_RESIDENCE = c.COUNTRY_OF_RESIDENCE,
                                    RESIDENCE_OWNED_OR_RENT = c.RESIDENCE_OWNED_OR_RENT,
                                    ZIP_POSTAL_CODE = c.ZIP_POSTAL_CODE,
                                    CREATED_DATE = c.CREATED_DATE,
                                    CREATED_BY = c.CREATED_BY,
                                    LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                                    LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                                    IP_ADDRESS = c.IP_ADDRESS,
                                }).FirstOrDefault();
            }
            else if (invrecords == 1)
            {
                addressmodel = (from c in _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL
                                where c.CUSTOMER_NO == id
                                where c.AUTHORISED == "A"
                                select new IndividualAddressDetails
                                {
                                    CUSTOMER_NO = c.CUSTOMER_NO,
                                    RESIDENTIAL_ADDRESS = c.RESIDENTIAL_ADDRESS,
                                    CITY_TOWN_OF_RESIDENCE = c.CITY_TOWN_OF_RESIDENCE,
                                    LGA_OF_RESIDENCE = c.LGA_OF_RESIDENCE,
                                    NEAREST_BUS_STOP_LANDMARK = c.NEAREST_BUS_STOP_LANDMARK,
                                    STATE_OF_RESIDENCE = c.STATE_OF_RESIDENCE,
                                    COUNTRY_OF_RESIDENCE = c.COUNTRY_OF_RESIDENCE,
                                    RESIDENCE_OWNED_OR_RENT = c.RESIDENCE_OWNED_OR_RENT,
                                    ZIP_POSTAL_CODE = c.ZIP_POSTAL_CODE,
                                    CREATED_DATE = c.CREATED_DATE,
                                    CREATED_BY = c.CREATED_BY,
                                    LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                                    LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                                    IP_ADDRESS = c.IP_ADDRESS,
                                }).FirstOrDefault();
            }

            IndAddDataPrepareModel(addressmodel);

            int idrecords = _db.CDMA_INDIVIDUAL_IDENTIFICATION.Count(o => o.CUSTOMER_NO == id);
            var idmodel = new individualIDDetail();
            if (idrecords > 1)
            {
                idmodel = (from c in _db.CDMA_INDIVIDUAL_IDENTIFICATION
                           where c.CUSTOMER_NO == id
                           where c.AUTHORISED == "U"
                           select new individualIDDetail
                           {
                               CUSTOMER_NO = c.CUSTOMER_NO,
                               IDENTIFICATION_TYPE = c.IDENTIFICATION_TYPE,
                               ID_NO = c.ID_NO,
                               ID_EXPIRY_DATE = c.ID_EXPIRY_DATE,
                               ID_ISSUE_DATE = c.ID_ISSUE_DATE,
                               PLACE_OF_ISSUANCE = c.PLACE_OF_ISSUANCE,
                               LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                               LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                               IP_ADDRESS = c.IP_ADDRESS
                           }).FirstOrDefault();
            }
            else if (invrecords == 1)
            {
                idmodel = (from c in _db.CDMA_INDIVIDUAL_IDENTIFICATION
                           where c.CUSTOMER_NO == id
                           where c.AUTHORISED == "A"
                           select new individualIDDetail
                           {
                               CUSTOMER_NO = c.CUSTOMER_NO,
                               IDENTIFICATION_TYPE = c.IDENTIFICATION_TYPE,
                               ID_NO = c.ID_NO,
                               ID_EXPIRY_DATE = c.ID_EXPIRY_DATE,
                               ID_ISSUE_DATE = c.ID_ISSUE_DATE,
                               PLACE_OF_ISSUANCE = c.PLACE_OF_ISSUANCE,
                               LAST_MODIFIED_DATE = c.LAST_MODIFIED_DATE,
                               LAST_MODIFIED_BY = c.LAST_MODIFIED_BY,
                               IP_ADDRESS = c.IP_ADDRESS
                           }).FirstOrDefault();
            }
            IndIdDataPrepareModel(idmodel);
            int otherrecords = _db.CDMA_INDIVIDUAL_OTHER_DETAILS.Count(o => o.CUSTOMER_NO == id);
            var othermodel = new OtherDetails();
            if (otherrecords > 1)
            {
                othermodel = (from c in _db.CDMA_INDIVIDUAL_OTHER_DETAILS
                              where c.CUSTOMER_NO == id
                              where c.AUTHORISED == "U"
                              select new OtherDetails
                              {
                                  CUSTOMER_NO = c.CUSTOMER_NO,
                                  TIN_NO = c.TIN_NO
                              }).FirstOrDefault();
            }
            else if (invrecords == 1)
            {
                othermodel = (from c in _db.CDMA_INDIVIDUAL_OTHER_DETAILS
                              where c.CUSTOMER_NO == id
                              where c.AUTHORISED == "A"
                              select new OtherDetails
                              {
                                  CUSTOMER_NO = c.CUSTOMER_NO,
                                  TIN_NO = c.TIN_NO
                              }).FirstOrDefault();
            }



                //if (IndividualBioDataModel == null)
                //{
                //    return HttpNotFound();
                //}


                viewModel.BioData = indvmodel;
            viewModel.AddressDetails = addressmodel;
            viewModel.contact = contactmodel;
            viewModel.identification = idmodel;
            viewModel.otherdetails = othermodel;
            //  return JavaScript("alert('hey');");
            return View(viewModel);
            //ViewData["BioData"] = IndividualBioDataModel;
            //ViewData["AddressDetails"] = IndividualAddressModel;
            //ViewData["contact"] = IndividualContactModel;
            //ViewData["identification"] = individualIDModel;
            //ViewData["otherdetails"] = OtherDetailsModel;
            //return View(viewModel);
        }

        [NonAction]
        protected virtual void IndIdDataPrepareModel(individualIDDetail individualIDModel)
        {
            if (individualIDModel == null)
                throw new ArgumentNullException("model");
            individualIDModel.IdType = new SelectList(_db.CDMA_IDENTIFICATION_TYPE, "CODE", "ID_TYPE").ToList();
        }


        [NonAction]
        protected virtual void IndAddDataPrepareModel(IndividualAddressDetails IndividualAddressModel)
        {
            if (IndividualAddressModel == null)
                throw new ArgumentNullException("model");


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


            IndividualAddressModel.CountryofResidence = new SelectList(_db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
            IndividualAddressModel.StateofResidence = new SelectList(_db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME").ToList();
            IndividualAddressModel.LGAofResidence = new SelectList(_db.SRC_CDMA_LGA, "LGA_ID", "LGA_NAME").ToList();


        }



        [NonAction]
        protected virtual void IndBioDataPrepareModel(IndividualBioDataModel IndividualBioDataModel)
        {
            if (IndividualBioDataModel == null)
                throw new ArgumentNullException("model");


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
                Text = "Albino",
                Value = "Albino"
            });
            IndividualBioDataModel.CusComplexion.Add(new SelectListItem
            {
                Text = "Black",
                Value = "Black"
            });
            IndividualBioDataModel.CusComplexion.Add(new SelectListItem
            {
                Text = "Yellow",
                Value = "Yellow"
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
                Text = "Not Married",
                Value = "Not Married"
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

            IndividualBioDataModel.CountryofBirth = new SelectList(_db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
            IndividualBioDataModel.Nationalities = new SelectList(_db.CDMA_COUNTRIES, "COUNTRY_ID", "COUNTRY_NAME").ToList();
            IndividualBioDataModel.Religions = new SelectList(_db.CDMA_RELIGION, "CODE", "RELIGION").ToList();
           IndividualBioDataModel.Branchs = new SelectList(_db.CM_BRANCH, "BRANCH_ID", "BRANCH_NAME").ToList();
            IndividualBioDataModel.State = new SelectList(_db.SRC_CDMA_STATE, "STATE_ID", "STATE_NAME").ToList();
            IndividualBioDataModel.TitleTypes = new SelectList(_db.CDMA_CUST_TITLE, "TITLE_CODE", "TITLE_DESC").ToList();


        }

        [HttpPost, ParameterBasedOnFormName("disapprove", "disapproveRecord")]
        [FormValueRequired("approve", "disapprove")]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(IndividualBioDataModel indivmodel, bool disapproveRecord)
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

                    _dqQueService.DisApproveExceptionQueItems(exceptionId.ToString(), indivmodel.AuthoriserRemarks);
                    SuccessNotification("Customer record Not Authorised");
                }

                else
                {
                    _dqQueService.ApproveExceptionQueItems(exceptionId.ToString(), identity.ProfileId);
                    SuccessNotification("Customer record Authorised");
                }

                return RedirectToAction("AuthList", "DQQue");
                //return RedirectToAction("Index");
            }
            IndBioDataPrepareModel(indivmodel);
            return View(indivmodel);
        }

    }
}
