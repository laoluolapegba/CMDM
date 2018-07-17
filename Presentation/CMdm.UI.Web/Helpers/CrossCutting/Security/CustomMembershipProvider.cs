using System;
using System.Collections.Specialized;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using CMdm.Data;
using CMdm.Data.Rbac;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web.Routing;
using System.Configuration;
//using CMdm.Business;
using System.Data;
using CMdm.Services.Authentication;
using Elmah;
using CMdm.Services;
using TwoFactorAuthenticationSvc;
using CMdm.Services.Messaging;
using CMdm.UI.Web.Models;
using System.IO;
using System.Windows;

namespace CMdm.UI.Web.Helpers.CrossCutting.Security
{
    public class CustomMembershipProvider : MembershipProvider
    {
        #region Properties

        private int _cacheTimeoutInMinutes = 30;
        static PasswordManager pwdManager = new PasswordManager();
        private IMessagingService _messagingService = new MessagingService();
        public AppDbContext db = new AppDbContext();

        private const string successStat = "0";
        private static string logs = "";
        //private Customer custObj;
        //public static string connString = ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
        //public OracleConnection con = new OracleConnection(connString);
        // public OracleCommand cmd = new OracleCommand();


        #endregion

        #region Overrides of MembershipProvider

        /// <summary>
        /// Initialize values from web.config.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            // Set Properties
            int val;
            if (!string.IsNullOrEmpty(config["cacheTimeoutInMinutes"]) && Int32.TryParse(config["cacheTimeoutInMinutes"], out val))
                _cacheTimeoutInMinutes = val;

            // Call base method
            base.Initialize(name, config);
        }

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        /// <param name="username">The name of the user to validate. </param><param name="password">The password for the specified user. </param>
        /// 

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            //Authenticate _auth = new Authenticate();
            int authSetting = int.Parse(ConfigurationManager.AppSettings["LDAPAuth"]);

            AuthenticationType authType = (AuthenticationType)(authSetting);

            //string OTPtoke = "";
            switch (authType)
            {
                case AuthenticationType.TwoFactor:
                    #region TwoFactor

                    string domainName1 = db.Settings.Where(a => a.SETTING_NAME == "SMTP_DOMAIN").Select(a => a.SETTING_VALUE).FirstOrDefault();
                   
                    //domain: “fcmb.com” All FCMB UserIDs in FCMB AD is imported into this container in VASCO IAS
                    //userID: Provide the “userID” from the banking application
                    //pin: leave blank
                    //dpResponse: the “AD Password +OTP” (One Time Password) displayed when the hardware token(digipass) is pressed
                    //password: leave blank
                    //reqHostCode: “false” sjahdgsajs532522
                    bool authenticated1 = false;
                    try
                    {
                        ServiceSoapClient client = new ServiceSoapClient();
                        string hostResponse = client.AuthoriseUser(domainName1, username, "", password, "", false);
                        if (hostResponse.Substring(0,1) == successStat)
                        {
                            //Then check local user
                            using (var context = new AppDbContext())
                            {
                                var localuser = (from u in context.CM_USER_PROFILE
                                                 where u.USER_ID.ToLower() == username.ToLower()
                                                 where u.ISLOCKED == false
                                                 //where String.Compare(u.USER_ID, username, StringComparison.OrdinalIgnoreCase) == 0

                                                 //&& !u.Deleted
                                                 select u).FirstOrDefault();

                                if (localuser == null)
                                {
                                    HttpContext.Current.Session["Response"] = "Contact your system administrator";
                                    authenticated1 = false;
                                }
                                else
                                {
                                    _messagingService.SaveUserActivity(localuser.PROFILE_ID, "Logged into CMDM", DateTime.Now);
                                    authenticated1 = true;
                                }
                            }
                        }
                        else
                        {
                            ErrorSignal.FromCurrentContext().Raise(new NotImplementedException(hostResponse));
                            HttpContext.Current.Session["Response"] = "Please input a valid token";
                            _messagingService.SaveUserActivity(0, hostResponse + " by " + username, DateTime.Now, username);
                        }
                        //if (true == adAuth.IsAuthenticated(loginDomain, username, password))
                        //{
                        //    authenticated1 = true;

                        //}

                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Session["Response"] = ex.Message;
                        ErrorSignal.FromCurrentContext().Raise(ex);
                    }


                    return authenticated1;
                #endregion;
                case AuthenticationType.LDAP:
                    #region LDAPAuth
                    string domainName = db.Settings.Where(a => a.SETTING_NAME == "DOMAIN_NAME").Select(a => a.SETTING_VALUE).FirstOrDefault();
                    string serverName = db.Settings.Where(a => a.SETTING_NAME == "LDAP_SERVER").Select(a => a.SETTING_VALUE).FirstOrDefault();
                    string loginDomain = db.Settings.Where(a => a.SETTING_NAME == "LOGIN_DOMAIN").Select(a => a.SETTING_VALUE).FirstOrDefault();
                    string ldapPort = db.Settings.Where(a => a.SETTING_NAME == "LDAP_PORT").Select(a => a.SETTING_VALUE).FirstOrDefault();

                    //String adPath = serverName + "://" + domainName; //LDAP://corp.com"; //Fully-qualified Domain Name
                    String adPath = "LDAP://" + serverName + ":" + ldapPort; // + "/" + domainName;
                    LDAPAuthenticationService adAuth = new LDAPAuthenticationService(adPath);
                    bool authenticated = false;
                    try
                    {
                        if (true == adAuth.IsAuthenticated(loginDomain, username, password))
                        {
                            //String groups = adAuth.GetGroups(); we dont need d groups yet
                            //var user = (from u in db.CM_USER_PROFILE
                            //            where u.USER_ID.ToLower() == username.ToLower()
                            //            select u).FirstOrDefault();

                            //Then check local user
                            using (var context = new AppDbContext())
                            {
                                var localuser = (from u in context.CM_USER_PROFILE
                                                 where u.USER_ID.ToLower() == username.ToLower()
                                                 where u.ISLOCKED == false
                                                 //where String.Compare(u.USER_ID, username, StringComparison.OrdinalIgnoreCase) == 0

                                                 //&& !u.Deleted
                                                 select u).FirstOrDefault();

                                if (localuser == null)
                                {
                                    HttpContext.Current.Session["Response"] = "Contact your system administrator";
                                    authenticated = false;
                                }
                                else
                                {
                                    _messagingService.SaveUserActivity(localuser.PROFILE_ID, "Logged into CMDM", DateTime.Now);
                                    authenticated = true;
                                }                                
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Session["Response"] = ex.Message;
                        ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return authenticated;
                #endregion;
                case AuthenticationType.OpenAuth:
                    break;
                case AuthenticationType.ApplicationAuth:
                    #region ApplicationAuth
                    using (var context = new AppDbContext())
                    {
                        var localuser = (from u in context.CM_USER_PROFILE
                                         where u.USER_ID.ToLower() == username.ToLower()
                                         where u.ISLOCKED == false
                                         //where String.Compare(u.USER_ID, username, StringComparison.OrdinalIgnoreCase) == 0

                                         //&& !u.Deleted
                                         select u).FirstOrDefault();

                        if (localuser != null)
                        {
                            if (!pwdManager.IsPasswordMatch(password, localuser.PASSWORDSALT, localuser.COD_PASSWORD))
                            {
                                localuser = null;
                            }
                        }

                        //String.Compare(u.COD_PASSWORD, password, StringComparison.OrdinalIgnoreCase) == 0Session["LastLoginDate"] = dtLastLogin;
                        return localuser != null;
                    }
                    #endregion
                    break;
                default:
                    return false;
                    break;
            }

            return false;
        }

        public bool CreateUser(string username, string password, string RoleId, string FName, string LName, string Email)
        {
            string salt = null;

            ICollection<CM_USER_ROLE_XREF> UserRoleColl = new Collection<CM_USER_ROLE_XREF>();
            CM_USER_PROFILE up = new CM_USER_PROFILE();
            string passwordHash = pwdManager.GeneratePasswordHash(password, out salt);
            int profileId = 0;
            using (var cdma = new AppDbContext())
            {
                var usr = new CM_USER_PROFILE
                {

                    COD_PASSWORD = passwordHash,
                    PASSWORDSALT = salt,
                    USER_ID = username,
                    ISLOCKED = false,  //isLoggedin
                    CREATED_DATE = DateTime.Now,
                    ROLE_ID = Convert.ToInt32(RoleId),
                    FIRSTNAME = FName,
                    LASTNAME = LName,
                    DISPLAY_NAME = (FName.Substring(0, 1) + LName).ToLower(),
                    EMAIL_ADDRESS = Email,
                    ISAPPROVED = true,
                    LASTLOGINDATE = DateTime.Now,
                    LASTLOCKOUTDATE = DateTime.Now,
                    LASTPASSWORDCHANGEDDATE = DateTime.Now
                };
                cdma.CM_USER_PROFILE.Add(usr);
                cdma.SaveChanges();
                profileId = usr.PROFILE_ID;

            }

            CM_USER_ROLE_XREF urx = new CM_USER_ROLE_XREF();

            urx.USER_ID = profileId;
            urx.ROLE_ID = Convert.ToInt32(RoleId);

            UserRoleColl.Add(urx);

            up.CM_USER_ROLE_XREF = UserRoleColl;

            db.CM_USER_ROLE_XREF.Add(urx);
            db.SaveChanges();

            if (up.PROFILE_ID == null)
            {
                return false;
            }
            else
            {
                return true;

            }
        }


        public bool MapRolestoPermission(int roleID, int PermID)
        {
            CM_ROLE_PERM_XREF rpx = new CM_ROLE_PERM_XREF();

            // if (db.CM_ROLE_PERM_XREF.Count(x => x.ROLE_ID == roleID) > 0 && db.CM_ROLE_PERM_XREF.Count(x => x.PERMISSION_ID == PermID) > 0)
            var count = from n in db.CM_ROLE_PERM_XREF
                        where (n.ROLE_ID == roleID && n.PERMISSION_ID == PermID)
                        select new { n.RECORD_ID };

            if (count.Count() > 0)
            {
                return false;
            }
            else
            {
                rpx.ROLE_ID = roleID;
                rpx.PERMISSION_ID = PermID;
                rpx.CREATED_DATE = DateTime.Now;
                rpx.CREATED_BY = "Admin";
                rpx.PARENT_TASK = 1;

                db.CM_ROLE_PERM_XREF.Add(rpx);

                db.SaveChanges();

                return true;
            }
        }

        public bool MapMakertoChecker(int MakerID, int CheckerID)
        {
            CM_MAKER_CHECKER_XREF mcx = new CM_MAKER_CHECKER_XREF();

            var count = from n in db.CM_MAKER_CHECKER_XREF
                        where (n.MAKER_ID == MakerID && n.CHECKER_ID == CheckerID)
                        select new { n.RECORD_ID };

            if (count.Count() > 0)
            {
                return false;
            }
            else
            {
                mcx.MAKER_ID = MakerID;
                mcx.CHECKER_ID = CheckerID;
                //mcx.CREATED_DATE = DateTime.Now;
                //mcx.CREATED_BY = "Admin";
                //mcx.PARENT_TASK = 1;

                db.CM_MAKER_CHECKER_XREF.Add(mcx);

                db.SaveChanges();

                return true;
            }
        }


        public bool UpdateUser(string username, string firstname, string lastname, string roleId, string email, decimal userID)
        {

            var objUserP = from profile in db.CM_USER_PROFILE where profile.PROFILE_ID == userID select profile;

            var usr = objUserP.Single<CM_USER_PROFILE>();

            usr.ROLE_ID = roleId == string.Empty ? objUserP.FirstOrDefault().ROLE_ID : Convert.ToInt32(roleId);
            usr.FIRSTNAME = firstname;
            usr.LASTNAME = lastname;
            usr.DISPLAY_NAME = (firstname.Substring(0, 1) + lastname).ToLower();
            usr.EMAIL_ADDRESS = email;

            db.SaveChanges();

            return true;

        }

        public bool DeleteAssignedPermissions(decimal RecID)
        {
            //ICollection<CM_USER_ROLE_XREF> UserRoleColl = new Collection<CM_USER_ROLE_XREF>();
            //CM_USER_PROFILE up = new CM_USER_PROFILE();
            //CM_USER_ROLE_XREF urx = new CM_USER_ROLE_XREF();
            CM_ROLE_PERM_XREF rpx = new CM_ROLE_PERM_XREF();
            try
            {
                CM_ROLE_PERM_XREF objDelPermssnXRoleRef = (from PermssnXRoleRef in db.CM_ROLE_PERM_XREF where PermssnXRoleRef.RECORD_ID == RecID select PermssnXRoleRef).FirstOrDefault();

                db.CM_ROLE_PERM_XREF.Remove(objDelPermssnXRoleRef);
                db.SaveChanges();


                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteAssignedUsers(decimal RecID)
        {

            CM_MAKER_CHECKER_XREF rpx = new CM_MAKER_CHECKER_XREF();
            try
            {
                CM_MAKER_CHECKER_XREF objDelMakernXCheckerRef = (from MXCRef in db.CM_MAKER_CHECKER_XREF where MXCRef.RECORD_ID == RecID select MXCRef).FirstOrDefault();

                db.CM_MAKER_CHECKER_XREF.Remove(objDelMakernXCheckerRef);
                db.SaveChanges();


                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }




        public bool DeleteUser(decimal UserID)
        {
            //ICollection<CM_USER_ROLE_XREF> UserRoleColl = new Collection<CM_USER_ROLE_XREF>();
            CM_USER_PROFILE up = new CM_USER_PROFILE();
            CM_USER_ROLE_XREF urx = new CM_USER_ROLE_XREF();
            try
            {
                //First Delete Rec from UserRoleRef
                CM_USER_ROLE_XREF objDelUserRoleRef = (from Profile in db.CM_USER_ROLE_XREF where Profile.USER_ID == UserID select Profile).FirstOrDefault();

                db.CM_USER_ROLE_XREF.Remove(objDelUserRoleRef);
                db.SaveChanges();

                //First Delete Rec from MakerCheckerRef
                //CM_MAKER_CHECKER_XREF objDelMakerCheckerRef = (from MCX in db.CM_MAKER_CHECKER_XREF where MCX.CHECKER_ID == UserID || MCX.MAKER_ID == UserID select MCX).FirstOrDefault();

                //db.CM_MAKER_CHECKER_XREF.Remove(objDelMakerCheckerRef);
                //db.SaveChanges();

                CM_USER_PROFILE objDeleteUser = (from Profile in db.CM_USER_PROFILE where Profile.PROFILE_ID == UserID select Profile).FirstOrDefault();

                db.CM_USER_PROFILE.Remove(objDeleteUser);
                db.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool CreatePermissions(string desc, string controller, string action, string formURL)
        {

            CM_PERMISSIONS up = new CM_PERMISSIONS();

            up.PERMISSIONDESCRIPTION = desc;
            up.ACTION_NAME = action == string.Empty ? "Nill" : action;
            up.CONTROLLER_NAME = controller == string.Empty ? "Nill" : controller;
            up.FORM_URL = formURL == string.Empty ? "Nill" : formURL;
            up.ICON_CLASS = "Nill";
            up.IMAGE_URL = "Nill";
            up.ISACTIVE = true;
            up.ISOPEN_CLASS = "Nill";
            up.PARENT_PERMISSION = 1;
            up.TOGGLE_ICON = "Nill";

            db.CM_PERMISSIONS.Add(up);
            db.SaveChanges();

            if (up.PERMISSION_ID == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool getPermssn(string desc, string controller, string action, string URL)
        {
            bool status = false;

            string c = controller == string.Empty ? "Nill" : controller;
            string a = action == string.Empty ? "Nill" : action;

            CM_PERMISSIONS up = new CM_PERMISSIONS();

            var count = (from n in db.CM_PERMISSIONS
                         where n.PERMISSIONDESCRIPTION == desc && n.FORM_URL == URL && n.CONTROLLER_NAME == c && n.ACTION_NAME == a
                         select n).Count();
            //select new { n.PERMISSION_ID };

            if (count > 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool getUserr(string Username, string Email)
        {
            bool status = false;
            CM_USER_PROFILE upl = new CM_USER_PROFILE();

            var count = from n in db.CM_USER_PROFILE
                        where (n.USER_ID == Username && n.EMAIL_ADDRESS == Email)
                        select new { n.PROFILE_ID };

            if (count.Count() > 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool checkUserName(string Username)
        {
            bool status = false;
            CM_USER_PROFILE upl = new CM_USER_PROFILE();

            var count = from n in db.CM_USER_PROFILE
                        where (n.USER_ID == Username)
                        select new { n.PROFILE_ID };

            if (count.Count() > 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool CreateRole(string roleName)
        {
            //CDMA_Model mdb = new CDMA_Model();

            CM_USER_ROLES rl = new CM_USER_ROLES();

            rl.ROLE_NAME = roleName;
            rl.PSWD_LIFE_DAYS = 90;
            rl.CREATED_DATE = DateTime.Now;
            rl.CREATED_BY = "admin";

            db.CM_USER_ROLES.Add(rl);
            db.SaveChanges();

            if (rl.ROLE_ID == null)
            {
                return false;
            }
            else
            {
                return true;

            }

        }

        public decimal getProfileID(string p)
        {
            decimal status = 0;
            CM_USER_PROFILE up = new CM_USER_PROFILE();

            var profID = from n in db.CM_USER_PROFILE
                         where n.USER_ID == p.ToString()
                         select new { n.PROFILE_ID };

            status = profID.FirstOrDefault().PROFILE_ID;

            return status;

        }

        public bool getRole(string p)
        {
            bool status = false;
            CM_USER_ROLES rl = new CM_USER_ROLES();

            var count = from n in db.CM_USER_ROLES
                        where n.ROLE_NAME == p.ToString()
                        select new { n.ROLE_ID };

            if (count.Count() > 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        /// <param name="username">The name of the user to get information for. </param><param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user. </param>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var cacheKey = string.Format("UserData_{0}", username);
            if (HttpRuntime.Cache[cacheKey] != null)
                return (CustomMembershipUser)HttpRuntime.Cache[cacheKey];

            using (var context = new AppDbContext())
            {
                var user = (from u in context.CM_USER_PROFILE.Include(usr => usr.CM_USER_ROLES)
                            where String.Compare(u.USER_ID.ToUpper(), username.ToUpper(), StringComparison.OrdinalIgnoreCase) == 0
                            //&& !u.Deleted
                            select u).FirstOrDefault();

                if (user == null)
                    return null;

                var membershipUser = new CustomMembershipUser(user);
                if (userIsOnline)
                {
                    CMdm.Data.Rbac.CM_USER_PROFILE CM_USER_PROFILE = db.CM_USER_PROFILE.Find(membershipUser.ProfileId);

                    CM_USER_PROFILE.LASTLOGINDATE = DateTime.Now;
                    //CM_USER_PROFILE.ISLOCKED = 1;   //isloggedin
                    db.Entry(CM_USER_PROFILE).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //Store in cache
                HttpRuntime.Cache.Insert(cacheKey, membershipUser, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);

                return membershipUser;
            }
        }

        #endregion

        #region Overrides of MembershipProvider that throw NotImplementedException

        /// <summary>
        /// Adds a new membership user to the data source.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the information for the newly created user.
        /// </returns>
        /// <param name="username">The user name for the new user. </param><param name="password">The password for the new user. </param><param name="email">The e-mail address for the new user.</param><param name="passwordQuestion">The password question for the new user.</param><param name="passwordAnswer">The password answer for the new user</param><param name="isApproved">Whether or not the new user is approved to be validated.</param><param name="providerUserKey">The unique identifier from the membership data source for the user.</param><param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus"/> enumeration value indicating whether the user was created successfully.</param>
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes a request to update the password question and answer for a membership user.
        /// </summary>
        /// <returns>
        /// true if the password question and answer are updated successfully; otherwise, false.
        /// </returns>
        /// <param name="username">The user to change the password question and answer for. </param><param name="password">The password for the specified user. </param><param name="newPasswordQuestion">The new password question for the specified user. </param><param name="newPasswordAnswer">The new password answer for the specified user. </param>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the password for the specified user name from the data source.
        /// </summary>
        /// <returns>
        /// The password for the specified user name.
        /// </returns>
        /// <param name="username">The user to retrieve the password for. </param><param name="answer">The password answer for the user. </param>
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <returns>
        /// true if the password was updated successfully; otherwise, false.
        /// </returns>
        /// <param name="username">The user to update the password for. </param><param name="oldPassword">The current password for the specified user. </param><param name="newPassword">The new password for the specified user. </param>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <returns>
        /// The new password for the specified user.
        /// </returns>
        /// <param name="username">The user to reset the password for. </param><param name="answer">The password answer for the specified user. </param>
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser"/> object that represents the user to update and the updated information for the user. </param>
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <returns>
        /// true if the membership user was successfully unlocked; otherwise, false.
        /// </returns>
        /// <param name="userName">The membership user whose lock status you want to clear.</param>
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the user name associated with the specified e-mail address.
        /// </summary>
        /// <returns>
        /// The user name associated with the specified e-mail address. If no match is found, return null.
        /// </returns>
        /// <param name="email">The e-mail address to search for. </param>
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a user from the membership data source. 
        /// </summary>
        /// <returns>
        /// true if the user was successfully deleted; otherwise, false.
        /// </returns>
        /// <param name="username">The name of the user to delete.</param><param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of all the users in the data source in pages of data.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">The total number of matched users.</param>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the number of users currently accessing the application.
        /// </summary>
        /// <returns>
        /// The number of users currently accessing the application.
        /// </returns>

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <param name="emailToMatch">The e-mail address to search for.</param><param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">The total number of matched users.</param>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
        /// </summary>
        /// <returns>
        /// true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
        /// </returns>
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        /// <returns>
        /// true if the membership provider supports password reset; otherwise, false. The default is true.
        /// </returns>
        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        /// <returns>
        /// true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
        /// </returns>
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// The name of the application using the custom membership provider.
        /// </summary>
        /// <returns>
        /// The name of the application using the custom membership provider.
        /// </returns>
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        /// <returns>
        /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </returns>
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </summary>
        /// <returns>
        /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </returns>
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <returns>
        /// true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
        /// </returns>
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat"/> values indicating the format for storing passwords in the data store.
        /// </returns>
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <returns>
        /// The minimum length required for a password. 
        /// </returns>
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        /// <returns>
        /// The minimum number of special characters that must be present in a valid password.
        /// </returns>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <returns>
        /// A regular expression used to evaluate a password.
        /// </returns>
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param><param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        #endregion


        public override int GetNumberOfUsersOnline()
        {
            int status = 0;
            CM_USER_PROFILE up = new CM_USER_PROFILE();

            var count = from n in db.CM_USER_PROFILE
                        //where n.ISLOCKED == 1
                        select new { n.PROFILE_ID };


            status = count.Count();

            return status;

        }

        /// <summary>
        /// Gets a collection of membership users where the user name contains the specified user name to match.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <param name="usernameToMatch">The user name to search for.</param><param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">The total number of matched users.</param>


        internal bool DeletePermission(decimal p)
        {
            //ICollection<CM_USER_ROLE_XREF> UserRoleColl = new Collection<CM_USER_ROLE_XREF>();
            // CM_USER_PROFILE up = new CM_USER_PROFILE();
            //CM_USER_ROLE_XREF urx = new CM_USER_ROLE_XREF();
            try
            {
                var PermxCount = (from Permx in db.CM_ROLE_PERM_XREF where Permx.PERMISSION_ID == p select Permx).Count();
                if (PermxCount != null)
                {

                    for (int i = 0; i < PermxCount; i++)
                    {
                        CM_ROLE_PERM_XREF objDelPermissionRoleRef = (from Permission in db.CM_ROLE_PERM_XREF where Permission.PERMISSION_ID == p select Permission).FirstOrDefault();

                        db.CM_ROLE_PERM_XREF.Remove(objDelPermissionRoleRef);
                        db.SaveChanges();
                    }
                }

                CM_PERMISSIONS objDeletePermission = (from Perm in db.CM_PERMISSIONS where Perm.PERMISSION_ID == p select Perm).FirstOrDefault();

                db.CM_PERMISSIONS.Remove(objDeletePermission);
                db.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }



        public Boolean checkDateDiff(DateTime issueDate, DateTime expiryDate)
        {
            if (issueDate != null && expiryDate != null)
            {
                if (issueDate < expiryDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {

                return true;
            }

        }


    }
}