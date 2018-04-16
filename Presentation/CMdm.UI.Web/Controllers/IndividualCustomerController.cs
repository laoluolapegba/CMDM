﻿using System;
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

            var model = new BiodataInfoViewModel();

            var changeId = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_BIO_DATA" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId); //.Select(a=>a.PROPERTYNAME);
            var changeId2 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_CONTACT_DETAIL" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet2 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId2); //.Select(a=>a.PROPERTYNAME);
            var changeId3 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_ADDRESS_DETAIL" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet3 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId3); //.Select(a=>a.PROPERTYNAME);
            var changeId4 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_IDENTIFICATION" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet4 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId4); //.Select(a=>a.PROPERTYNAME);
            var changeId5 = _db.CDMA_CHANGE_LOGS.Where(a => a.ENTITYNAME == "CDMA_INDIVIDUAL_OTHER_DETAILS" && a.PRIMARYKEYVALUE == querecord.CUST_ID).OrderByDescending(a => a.DATECHANGED).FirstOrDefault().CHANGEID;
            var changedSet5 = _db.CDMA_CHANGE_LOGS.Where(a => a.CHANGEID == changeId5); //.Select(a=>a.PROPERTYNAME);
               

            var biodata = (from c in _db.CDMA_INDIVIDUAL_BIO_DATA
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
                            RELIGION = c.RELIGION,
                            LastUpdatedby = c.LAST_MODIFIED_BY,
                             LastUpdatedDate = c.LAST_MODIFIED_DATE,
                             LastAuthdby = c.AUTHORISED_BY,
                             LastAuthDate = c.AUTHORISED_DATE,
                             ExceptionId = querecord.EXCEPTION_ID
                         }).FirstOrDefault();


            var contact = (from c in _db.CDMA_INDIVIDUAL_CONTACT_DETAIL
                           where c.CUSTOMER_NO == id
                           where c.AUTHORISED == "U"
                           select new IndividualContactDetails
                           {
                               CUSTOMER_NO = c.CUSTOMER_NO,
                               MOBILE_NO = c.MOBILE_NO,
                               EMAIL_ADDRESS = c.EMAIL_ADDRESS,
                               MAILING_ADDRESS = c.MAILING_ADDRESS,
                               LastUpdatedby = c.LAST_MODIFIED_BY,
                               LastUpdatedDate = c.LAST_MODIFIED_DATE,
                               LastAuthdby = c.AUTHORISED_BY,
                               LastAuthDate = c.AUTHORISED_DATE,
                               ExceptionId = querecord.EXCEPTION_ID
                           }).FirstOrDefault();

            var address = (from c in _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL
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
                               LastUpdatedby = c.LAST_MODIFIED_BY,
                               LastUpdatedDate = c.LAST_MODIFIED_DATE,
                               LastAuthdby = c.AUTHORISED_BY,
                               LastAuthDate = c.AUTHORISED_DATE,
                               ExceptionId = querecord.EXCEPTION_ID
                           }).FirstOrDefault();

            var identification = (from c in _db.CDMA_INDIVIDUAL_IDENTIFICATION
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
                               LastUpdatedby = c.LAST_MODIFIED_BY,
                               LastUpdatedDate = c.LAST_MODIFIED_DATE,
                               LastAuthdby = c.AUTHORISED_BY,
                               LastAuthDate = c.AUTHORISED_DATE,
                               ExceptionId = querecord.EXCEPTION_ID
                           }).FirstOrDefault();

            var otherdetails = (from c in _db.CDMA_INDIVIDUAL_OTHER_DETAILS
                                  where c.CUSTOMER_NO == id
                                  where c.AUTHORISED == "U"
                                  select new OtherDetails
                                  {
                                      CUSTOMER_NO = c.CUSTOMER_NO,
                                      TIN_NO = c.TIN_NO,
                                      LastUpdatedby = c.LAST_MODIFIED_BY,
                                      LastUpdatedDate = c.LAST_MODIFIED_DATE,
                                      LastAuthdby = c.AUTHORISED_BY,
                                      LastAuthDate = c.AUTHORISED_DATE,
                                      ExceptionId = querecord.EXCEPTION_ID
                                  }).FirstOrDefault();

            model.BioData = biodata;
            model.AddressDetails = address;
            model.contact = contact;
            model.identification = identification;
            model.otherdetails = otherdetails;

            if (model.BioData != null)
            {
                foreach (var item in model.BioData.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
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

            if (model.AddressDetails != null)
            {
                foreach (var item in model.AddressDetails.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
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

            if (model.contact != null)
            {
                foreach (var item in model.contact.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
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

            if (model.identification != null)
            {
                foreach (var item in model.identification.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
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

            if (model.otherdetails != null)
            {
                foreach (var item in model.otherdetails.GetType().GetProperties())  //BindingFlags.Public | BindingFlags.Static
                {
                    foreach (var item2 in changedSet5)
                    {
                        if (item2.PROPERTYNAME == item.Name)
                        {
                            ModelState.AddModelError(item.Name, string.Format("Field has been modified, value was {0}", item2.OLDVALUE));
                        }
                    }
                }
            }

            //var matchItems = props.Intersect(changedSet);
            model.AddressDetails.ReadOnlyForm = "True";
            model.BioData.ReadOnlyForm = "True";
            model.contact.ReadOnlyForm = "True";
            model.identification.ReadOnlyForm = "True";
            model.otherdetails.ReadOnlyForm = "True";

            IndIdDataPrepareModel(model.identification);
            IndAddDataPrepareModel(model.AddressDetails);
            IndBioDataPrepareModel(model.BioData);
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
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    var entity = db.CDMA_INDIVIDUAL_BIO_DATA.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);
                    var contact_entity = db.CDMA_INDIVIDUAL_CONTACT_DETAIL.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);
                    var Address_entity = db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);
                    var ID_entity = db.CDMA_INDIVIDUAL_IDENTIFICATION.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);
                    var Other_entity = db.CDMA_INDIVIDUAL_OTHER_DETAILS.FirstOrDefault(o => o.CUSTOMER_NO == bioDatamodel.BioData.CUSTOMER_NO);


                    if (entity == null)
                    {
                        string errorMessage = string.Format("Cannot update record with Id:{0} as it's not available.", bioDatamodel.BioData.CUSTOMER_NO);
                        ModelState.AddModelError("", errorMessage);
                    }
                    else
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

                        entity.LAST_MODIFIED_BY = identity.ProfileId.ToString();
                        entity.LAST_MODIFIED_DATE = DateTime.Now;
                        entity.AUTHORISED = "U";
                        db.CDMA_INDIVIDUAL_BIO_DATA.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO);
                         

                        DateTime today = DateTime.Today;
                        //update contact details
                        if (!(contact_entity == null))
                        {
                            contact_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            contact_entity.MOBILE_NO = bioDatamodel.contact.MOBILE_NO;
                            contact_entity.EMAIL_ADDRESS = bioDatamodel.contact.EMAIL_ADDRESS;
                            contact_entity.MAILING_ADDRESS = bioDatamodel.contact.MAILING_ADDRESS;

                            contact_entity.LAST_MODIFIED_DATE = bioDatamodel.contact.LAST_MODIFIED_DATE;
                            contact_entity.LAST_MODIFIED_BY = bioDatamodel.contact.LAST_MODIFIED_BY;
                            contact_entity.IP_ADDRESS = bioDatamodel.contact.IP_ADDRESS;
                            contact_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Attach(contact_entity);
                            db.Entry(contact_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO);
                        }
                        else
                        {

                            contact_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            contact_entity.MOBILE_NO = bioDatamodel.contact.MOBILE_NO;
                            contact_entity.EMAIL_ADDRESS = bioDatamodel.contact.EMAIL_ADDRESS;
                            contact_entity.MAILING_ADDRESS = bioDatamodel.contact.MAILING_ADDRESS;
                            contact_entity.CREATED_DATE = today;
                            contact_entity.CREATED_BY = identity.ProfileId.ToString();
                            contact_entity.IP_ADDRESS = bioDatamodel.contact.IP_ADDRESS;
                            contact_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Add(contact_entity);
                            db.SaveChanges();
                        }

                        if (!(Address_entity == null))
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

                            Address_entity.LAST_MODIFIED_DATE = bioDatamodel.AddressDetails.LAST_MODIFIED_DATE;
                            Address_entity.LAST_MODIFIED_BY = bioDatamodel.AddressDetails.LAST_MODIFIED_BY;
                            Address_entity.IP_ADDRESS = bioDatamodel.AddressDetails.IP_ADDRESS;
                            Address_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Attach(Address_entity);
                            db.Entry(Address_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO);
                        }
                        else
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
                            Address_entity.CREATED_DATE = today;
                            Address_entity.CREATED_BY = identity.ProfileId.ToString();
                            Address_entity.IP_ADDRESS = bioDatamodel.AddressDetails.IP_ADDRESS;
                            Address_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Add(Address_entity);
                            db.SaveChanges();
                        }

                        if (!(ID_entity == null))
                        {
                            ID_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            ID_entity.IDENTIFICATION_TYPE = bioDatamodel.identification.IDENTIFICATION_TYPE;
                            ID_entity.ID_NO = bioDatamodel.identification.ID_NO;
                            ID_entity.ID_EXPIRY_DATE = bioDatamodel.identification.ID_EXPIRY_DATE;
                            ID_entity.ID_ISSUE_DATE = bioDatamodel.identification.ID_ISSUE_DATE;
                            ID_entity.PLACE_OF_ISSUANCE = bioDatamodel.identification.PLACE_OF_ISSUANCE;
                            ID_entity.LAST_MODIFIED_DATE = bioDatamodel.identification.LAST_MODIFIED_DATE;
                            ID_entity.LAST_MODIFIED_BY = bioDatamodel.identification.LAST_MODIFIED_BY;
                            ID_entity.IP_ADDRESS = bioDatamodel.identification.IP_ADDRESS;
                            ID_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_IDENTIFICATION.Attach(ID_entity);
                            db.Entry(ID_entity).State = EntityState.Modified;
                            db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO);
                        }
                        else
                        {
                            ID_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                            ID_entity.IDENTIFICATION_TYPE = bioDatamodel.identification.IDENTIFICATION_TYPE;
                            ID_entity.ID_NO = bioDatamodel.identification.ID_NO;
                            ID_entity.ID_EXPIRY_DATE = bioDatamodel.identification.ID_EXPIRY_DATE;
                            ID_entity.ID_ISSUE_DATE = bioDatamodel.identification.ID_ISSUE_DATE;
                            ID_entity.PLACE_OF_ISSUANCE = bioDatamodel.identification.PLACE_OF_ISSUANCE;
                            ID_entity.LAST_MODIFIED_DATE = bioDatamodel.identification.LAST_MODIFIED_DATE;
                            ID_entity.LAST_MODIFIED_BY = bioDatamodel.identification.LAST_MODIFIED_BY;
                            ID_entity.CREATED_DATE = today;
                            ID_entity.CREATED_BY = identity.ProfileId.ToString();
                            ID_entity.IP_ADDRESS = bioDatamodel.identification.IP_ADDRESS;
                            ID_entity.AUTHORISED = "U";
                            db.CDMA_INDIVIDUAL_IDENTIFICATION.Add(ID_entity);
                            db.SaveChanges();
                        }

                    }



                    if (!(Other_entity == null))
                    {
                        Other_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                        Other_entity.TIN_NO = bioDatamodel.otherdetails.TIN_NO;
                        Other_entity.LAST_MODIFIED_DATE = bioDatamodel.otherdetails.LAST_MODIFIED_DATE;
                        Other_entity.AUTHORISED = "U";
                        db.CDMA_INDIVIDUAL_OTHER_DETAILS.Attach(Other_entity);
                        db.Entry(Other_entity).State = EntityState.Modified;
                        db.SaveChanges(identity.ProfileId.ToString(), bioDatamodel.BioData.CUSTOMER_NO);
                    }
                    else
                    {
                        Other_entity.CUSTOMER_NO = bioDatamodel.BioData.CUSTOMER_NO;
                        Other_entity.TIN_NO = bioDatamodel.otherdetails.TIN_NO;
                        Other_entity.LAST_MODIFIED_DATE = bioDatamodel.otherdetails.LAST_MODIFIED_DATE;
                        Other_entity.AUTHORISED = "U";
                        db.CDMA_INDIVIDUAL_OTHER_DETAILS.Add(Other_entity);
                        db.SaveChanges();

                    }

                }
            SuccessNotification("Bio Data Updated");
            return continueEditing ? RedirectToAction("Edit", new { id = bioDatamodel.BioData.CUSTOMER_NO }) : RedirectToAction("Index", "DQQue");
            //return RedirectToAction("Index");
        }
    
            IndBioDataPrepareModel(bioDatamodel.BioData);
           // IndAddDataPrepareModel(bioDatamodel.AddressDetails);
          //  IndIdDataPrepareModel(bioDatamodel.identification);
            return View(bioDatamodel.BioData);
        }

        // GET: CusBioData/Create
        public ActionResult Edit(string id)
        {
            var model = new BiodataInfoViewModel();

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }

            int records = _db.CDMA_INDIVIDUAL_BIO_DATA.Count(o => o.CUSTOMER_NO == id);
            int records2 = _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL.Count(o => o.CUSTOMER_NO == id);
            int records3 = _db.CDMA_INDIVIDUAL_CONTACT_DETAIL.Count(o => o.CUSTOMER_NO == id);
            int records4 = _db.CDMA_INDIVIDUAL_IDENTIFICATION.Count(o => o.CUSTOMER_NO == id);
            int records5 = _db.CDMA_INDIVIDUAL_OTHER_DETAILS.Count(o => o.CUSTOMER_NO == id);

            var biodata = new IndividualBioDataModel();
            var address = new IndividualAddressDetails();
            var contact = new IndividualContactDetails();
            var identification = new individualIDDetail();
            var otherdetails = new OtherDetails();

            if(records > 1)
            {
                biodata = (from c in _db.CDMA_INDIVIDUAL_BIO_DATA
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
            else if(records == 1)
            {
                biodata = (from c in _db.CDMA_INDIVIDUAL_BIO_DATA
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
            if(records2 > 1)
            {
                address = (from c in _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL
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
            else if(records2 == 1)
            {
                address = (from c in _db.CDMA_INDIVIDUAL_ADDRESS_DETAIL
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
            if(records3 > 1)
            {
                contact = (from c in _db.CDMA_INDIVIDUAL_CONTACT_DETAIL
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
            else if(records3 == 1)
            {
                contact = (from c in _db.CDMA_INDIVIDUAL_CONTACT_DETAIL
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
            if(records4 > 1)
            {
                 identification = (from c in _db.CDMA_INDIVIDUAL_IDENTIFICATION
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
            else if(records4 == 1)
            {
                identification = (from c in _db.CDMA_INDIVIDUAL_IDENTIFICATION
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
            if(records5 > 1)
            {
                otherdetails = (from c in _db.CDMA_INDIVIDUAL_OTHER_DETAILS
                                         where c.CUSTOMER_NO == id
                                         where c.AUTHORISED == "U"
                                         select new OtherDetails
                                         {
                                             CUSTOMER_NO = c.CUSTOMER_NO,
                                             TIN_NO = c.TIN_NO
                                         }).FirstOrDefault();
            }
            else if(records5 == 1)
            {
                otherdetails = (from c in _db.CDMA_INDIVIDUAL_OTHER_DETAILS
                                where c.CUSTOMER_NO == id
                                where c.AUTHORISED == "A"
                                select new OtherDetails
                                {
                                    CUSTOMER_NO = c.CUSTOMER_NO,
                                    TIN_NO = c.TIN_NO
                                }).FirstOrDefault();
            }
            
            IndBioDataPrepareModel(biodata);            
            IndAddDataPrepareModel(address);
            IndIdDataPrepareModel(identification);
            
            if (model == null)
            {
                return HttpNotFound();
            }


            model.BioData = biodata;
            model.AddressDetails = address;
            model.contact = contact;
            model.identification = identification;
            model.otherdetails = otherdetails;

            return View(model);

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



    }
}
