using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using MdmDqi.Data;
using System.Web.Caching;
using System.Web.Security;
//using Cdma.Web.Models;
using CMdm.UI.Web.Helpers.CrossCutting.Security;
using CMdm.Data;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web.Routing;
using CMdm.Entities.Domain.Entity;
using CMdm.Entities.Domain.Mdm;

namespace CMdm.UI.Web.BLL
{

    

    public class DQIParamSettingClass
    {

        public AppDbContext DQIdb = new AppDbContext();

        public bool CreateWeight(string WeightValue)
        {
             
            MdmWeights Mdmw = new MdmWeights();

            //Mdmw.WEIGHT_ID = WeightValue;
            Mdmw.WEIGHT_VALUE = Convert.ToInt16(WeightValue);
            Mdmw.CREATED_BY = "admin";
            Mdmw.CREATED_DATE = DateTime.Now;
            Mdmw.RECORD_STATUS = 1;
            DQIdb.MDM_WEIGHTS.Add(Mdmw);
            DQIdb.SaveChanges();

            if (Mdmw.WEIGHT_ID == null)
            {
                return false;
            }
            else
            {
                return true;

            }

        }

        public bool getWeight(Int16 p)
        {
            bool status = false;
            MdmWeights MdmW = new MdmWeights();
            var Val = p;
            var count = from n in DQIdb.MDM_WEIGHTS
                        where n.WEIGHT_VALUE == Val
                        select new { n.WEIGHT_ID };

            if (count == null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        //public bool CreateDQIParam(string username, string password, string RoleId)
        //{
        //    //string salt = null;

        //    //ICollection<CM_USER_ROLE_XREF> UserRoleColl = new Collection<CM_USER_ROLE_XREF>();
        //    //CM_USER_PROFILE up = new CM_USER_PROFILE();
        //    //string passwordHash = pwdManager.GeneratePasswordHash(password, out salt);
        //    //decimal profileId = 0;
        //    //using (var cdma = new CDMA_Model())
        //    //{
        //    //    var usr = new CM_USER_PROFILE
        //    //    {

        //    //        COD_PASSWORD = passwordHash,
        //    //        PASSWORDSALT = salt,
        //    //        USER_ID = username,
        //    //        ISLOCKED = 0,  //isLoggedin
        //    //        CREATED_DATE = DateTime.Now,
        //    //        ROLE_ID = Convert.ToInt32(RoleId),


        //    //    };
        //    //    cdma.CM_USER_PROFILE.Add(usr);
        //    //    cdma.SaveChanges();
        //    //    profileId = usr.PROFILE_ID;

        //    //}
        //}

        public bool CheckDQIParam(string p1, string p2)
        {
            throw new NotImplementedException();
        }

        public bool CreateDQIParam(string tblCat, string tblCol, string tblWeight, string tblName)
        {
            //string salt = null;
            //MdmWeights Mdmw = new MdmWeights();

            ////Mdmw.WEIGHT_ID = WeightValue;
            //Mdmw.WEIGHT_VALUE = Convert.ToInt16(WeightValue);
            //Mdmw.CREATED_BY = "admin";
            //Mdmw.CREATED_DATE = DateTime.Now;
            //Mdmw.RECORD_STATUS = "Y";
            //DQIdb.MDM_WEIGHTS.Add(Mdmw);
            //DQIdb.SaveChanges();

            //if (Mdmw.WEIGHT_ID == null)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;

            //}

            //ICollection<CM_USER_ROLE_XREF> UserRoleColl = new Collection<CM_USER_ROLE_XREF>();
            //EntityDetails entityObj = new EntityDetails();

            decimal ENTITY_DETAIL_ID = 0;

            using (var mdm = new AppDbContext())
            {
                var entityObj = new MdmEntityDetails
                {
                    ENTITY_TAB_NAME = tblName,
                    ENTITY_COL_NAME = tblCol,
                    FLG_MANDATORY = true,
                    WEIGHT_ID = Convert.ToInt16(tblWeight),
                    CREATED_BY = "Admin",
                    RECORD_STATUS = true,
                    CREATED_DATE = DateTime.Now,

                };
                mdm.EntityDetails.Add(entityObj);
                mdm.SaveChanges();
                ENTITY_DETAIL_ID = entityObj.ENTITY_DETAIL_ID;
            };

            if (ENTITY_DETAIL_ID == null)
            {
                return false;
            }
            else
            {
                return true;

            }
        }         
    }
}