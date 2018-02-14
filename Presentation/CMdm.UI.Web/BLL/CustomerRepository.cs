using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;
//using BlueChip.NAPIMS.Web.Views.MasterData;
//using NullableReaders;
using Oracle.DataAccess.Types;
using System.Configuration;
using Elmah;

namespace CMdm.UI.Web.BLL
{
    public class CustomerRepository
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionStringCDMA"].ConnectionString;
        private OracleConnection conn = new OracleConnection(connString);
        // public OracleConnection conn = new OracleConnection(new Connection().ConnectionString);
        private OracleCommand cmd = new OracleCommand();
        private OracleDataReader rdr;
        private OracleDataAdapter adapter = new OracleDataAdapter();
        Customer cust = new Customer();
        private Account account;
        private CustomerIncome income;
        private TrustClientAccount TCAcct;
        private AdditionalInformation AddtnlInfo;
        private CorpAdditionalInformation CorpAddnlInfo;
        private DIRIdentityInformation DIRIDInfo;
        private AuthForFinInclusion Auth4FinInc;
        private UtilityClass utility;
        private NextOfkin nextKin;
        private Foreigner foreigner;
        private Jurat jurat;
        private CompanyInformation compInfo;
        private AccountInformation accountInfo;
        private Biodata bio;
        private Subsidiary sub;
        private Credit credit;
        private Channel chan;
        private School school;
        private Membership membershp;
        private Preference prefer;
        private RelatedCustomer relative;
        private RelatedStaff relatedStaff;
        private Share share;
        private Card card;
        private Dependant dependant;
        private CompanyDetails companyDetails;
        private Employment work;
        private CorpPOAInformation CorpPOAInfo;
        private AWOBInformation accountWOBInfo;
        private AddressInformation AddressInfo;
        private DIRBiodataInformation BiodataInfo;
        private DIRForeignerInformation ForeignerInfo;
        private DIRNOKInformation NOKInfo;

        public CustomerIncome GetCustomerIncome(string refId, string table)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn; conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_refid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                while (rdr.Read())
                {
                    income = new CustomerIncome
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),
                        InitialDeposit = Convert.ToString(rdr["INITIAL_DEPOSIT"]),
                        IncomeBand = Convert.ToString(rdr["INCOME_BAND"]),

                        AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }


            }
            catch (Exception ex)
            {
                income.ErrorMessage = refId.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //income.CustomerName = GetCustomer(income.CustomerNo).CustomerName;
            }

            return income;

        }
        public TrustClientAccount GetTrustClientAcct(string p_customer_no, string table)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn; conn.Open();//CDMA_INDIVIDUAL_NEXT_OF_KIN

            objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_customerid";//:p_refId
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.NVarchar2).Value = p_customer_no;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                while (rdr.Read())
                {
                    TCAcct = new TrustClientAccount
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),
                        TrustsClientAccounts = Convert.ToString(rdr["TRUSTS_CLIENT_ACCOUNTS"]),
                        NameOfBeneficialOwner = Convert.ToString(rdr["NAME_OF_BENEFICIAL_OWNER"]),
                        SpouseName = Convert.ToString(rdr["SPOUSE_NAME"]),
                        SpouseDateOfBirth = Convert.ToDateTime(rdr["SPOUSE_DATE_OF_BIRTH"]),
                        SpouseOccupation = Convert.ToString(rdr["SPOUSE_OCCUPATION"]),
                        SourcesOfFundToAccount = Convert.ToString(rdr["SOURCES_OF_FUND_TO_ACCOUNT"]),
                        OtherSourceExpectAnnInc = Convert.ToString(rdr["OTHER_SOURCE_EXPECT_ANN_INC"]),
                        InsiderRelation = Convert.ToString(rdr["INSIDER_RELATION"]),
                        NameOfAssociatedBusiness = Convert.ToString(rdr["NAME_OF_ASSOCIATED_BUSINESS"]),
                        FreqInternationalTraveler = Convert.ToString(rdr["FREQ_INTERNATIONAL_TRAVELER"]),
                        PoliticallyExposedPerson = Convert.ToString(rdr["POLITICALLY_EXPOSED_PERSON"]),
                        PowerOfAttorney = Convert.ToString(rdr["POWER_OF_ATTORNEY"]),
                        HolderName = Convert.ToString(rdr["HOLDER_NAME"]),
                        Address = Convert.ToString(rdr["ADDRESS"]),
                        Country = Convert.ToString(rdr["COUNTRY"]),
                        Nationality = Convert.ToString(rdr["NATIONALITY"]),
                        TelephoneNumber = Convert.ToString(rdr["TELEPHONE_NUMBER"])


                        //AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        //AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        //CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        //Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        //LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        //LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }


            }
            catch (Exception ex)
            {
                TCAcct.ErrorMessage = p_customer_no.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //acctServReq.CustomerName = GetCustomer(acctServReq.CustomerNo).CustomerName;
            }

            return TCAcct;
             
        }
        public NextOfkin GetNextOfKin(string p_customer_no, string table)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn; conn.Open();//CDMA_INDIVIDUAL_NEXT_OF_KIN

            objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_customerid";//:p_refId
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.Varchar2).Value = p_customer_no.Trim();

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                utility = new UtilityClass();
                while (rdr.Read())
                {
                    nextKin = new NextOfkin
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),
                        Title = Convert.ToString(rdr["TITLE"]),
                        Surname = Convert.ToString(rdr["SURNAME"]),
                        FirstName = Convert.ToString(rdr["FIRST_NAME"]),
                        OtherName = Convert.ToString(rdr["OTHER_NAME"]),
                        DateOfBirth = Convert.ToString(rdr["DATE_OF_BIRTH"]),
                        Sex = Convert.ToString(rdr["SEX"]),
                        Relationship = Convert.ToString(rdr["RELATIONSHIP"]),
                        OfficeNo = Convert.ToString(rdr["OFFICE_NO"]),
                        MobileNo = Convert.ToString(rdr["MOBILE_NO"]),
                        EmailAddress = Convert.ToString(rdr["EMAIL_ADDRESS"]),
                        HouseNo = Convert.ToString(rdr["HOUSE_NUMBER"]),
                        IDType = Convert.ToString(rdr["IDENTIFICATION_TYPE"]),
                        IDIssueDate = Convert.ToString(rdr["ID_ISSUE_DATE"]),
                        IDExpiryDate = Convert.ToString(rdr["ID_EXPIRY_DATE"]),
                        ResidentPermitNo = Convert.ToString(rdr["RESIDENT_PERMIT_NUMBER"]),
                        PlaceOfIssuance = Convert.ToString(rdr["PLACE_OF_ISSUANCE"]),
                        StreetName = Convert.ToString(rdr["STREET_NAME"]),
                        NearestBStop = Convert.ToString(rdr["NEAREST_BUS_STOP_LANDMARK"]),
                        CityTown = Convert.ToString(rdr["CITY_TOWN"]),
                        ZipCode = Convert.ToString(rdr["ZIP_POSTAL_CODE"]),
                        LGA = Convert.ToString(rdr["LGA"]),
                        State = Convert.ToString(rdr["STATE"]),
                        Country = Convert.ToString(rdr["COUNTRY"])

                        //AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        //CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        //Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        //LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        //LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }


            }
            catch (Exception ex)
            {
                nextKin.ErrorMessage = ex.Message + ex.InnerException + ex.Source + ex.StackTrace;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //nextKin.CustomerName = GetCustomer(nextKin.CustomerNo).CustomerName;
            }

            return nextKin;

        }
        public Foreigner GetForeigner(string p_customer_no, string table)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn; conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where customer_no=:p_customer_no";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customer_no", OracleDbType.NVarchar2).Value = p_customer_no;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                while (rdr.Read())
                {
                    foreigner = new Foreigner
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        PassportResidencePermit = Convert.ToString(rdr["PASSPORT_RESIDENCE_PERMIT"]),
                        PermitIssueDate = Convert.ToDateTime(rdr["PERMIT_ISSUE_DATE"]),
                        PermitExpiryDate = Convert.ToDateTime(rdr["PERMIT_EXPIRY_DATE"]),
                        ForeignAddress = Convert.ToString(rdr["FOREIGN_ADDRESS"]),
                        city = Convert.ToString(rdr["CITY"]),
                        country = Convert.ToString(rdr["COUNTRY"]),
                        zip_postal_code = Convert.ToString(rdr["ZIP_POSTAL_CODE"]),
                        foreign_tel_number = Convert.ToString(rdr["FOREIGN_TEL_NUMBER"]),
                        purpose_of_account = Convert.ToString(rdr["PURPOSE_OF_ACCOUNT"])


                        //AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        //AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        //CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        //Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        //LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        //LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }
            }
            catch (Exception ex)
            {
                foreigner.ErrorMessage = ex.Message; //refId.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //foreigner.CustomerName = GetCustomer(foreigner.CustomerNo).CustomerName;
            }

            return foreigner;

        }

        public Customer GetCustomer(string id)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;

            //if (con.State == ConnectionState.Closed)
            conn.Open();//
            // make call to StoredProcedure "pkg_cdms2.get_basic_details"
            objCmd.CommandText = "pkg_cdms2.get_basic_details";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_cusid", OracleDbType.NVarchar2).Value = id;
            //
            objCmd.Parameters.Add("custinfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                cust = new Customer();
                while (rdr.Read())
                {
                    cust.CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]);
                    cust.CustomerName = Convert.ToString(rdr["CUSTOMER_NAME"]);
                    cust.CustomerCode = Convert.ToString(rdr["CUSTOMER_CODE"]);
                    cust.SecretQuestion = Convert.ToString(rdr["SECRET_QUESTION"]);
                    cust.SecretQuestionAnswer = Convert.ToString(rdr["SECRET_QUESTION_ANSWER"]);
                    cust.CustomerType = Convert.ToString(rdr["CUSTOMER_TYPE"]);
                    cust.EmailAddress1 = Convert.ToString(rdr["EMAIL_ADDRESS_1"]);
                    cust.EmailAddress2 = Convert.ToString(rdr["EMAIL_ADDRESS_1_1"]);
                    cust.WebAddress = Convert.ToString(rdr["WEB_ADDRESS"]);
                    cust.PoliticallyExposedPerson = Convert.ToString(rdr["POLITICALLY_EXPOSED_PERSON"]);
                    cust.FinanciallyExposedPerson = Convert.ToString(rdr["FINANCIALLY_EXPOSED_PERSON"]);
                    cust.AnniversaryType1 = Convert.ToString(rdr["ANNIVERSARY_TYPE1"]);
                    cust.AnniversaryDate1 = Convert.ToString(rdr["ANNIVERSARY_DATE1"]);
                    cust.AnniversaryType2 = Convert.ToString(rdr["ANNIVERSARY_TYPE2"]);
                    cust.AnniversaryDate2 = Convert.ToString(rdr["ANNIVERSARY_DATE2"]);
                    cust.AnniversaryType3 = Convert.ToString(rdr["ANNIVERSARY_TYPE3"]);
                    cust.AnniversaryDate3 = Convert.ToString(rdr["ANNIVERSARY_DATE3"]);
                    //

                    cust.AnniversaryType4 = Convert.ToString(rdr["ANNIVERSARY_TYPE4"]);
                    cust.AnniversaryDate4 = Convert.ToString(rdr["ANNIVERSARY_DATE4"]);

                    cust.AnniversaryType5 = Convert.ToString(rdr["ANNIVERSARY_TYPE5"]);
                    cust.AnniversaryDate5 = Convert.ToString(rdr["ANNIVERSARY_DATE5"]);

                    cust.AnniversaryType6 = Convert.ToString(rdr["ANNIVERSARY_TYPE6"]);
                    cust.AnniversaryDate6 = Convert.ToString(rdr["ANNIVERSARY_DATE6"]);
                    //

                    cust.AnniversaryType7 = Convert.ToString(rdr["ANNIVERSARY_TYPE7"]);
                    cust.AnniversaryDate7 = Convert.ToString(rdr["ANNIVERSARY_DATE7"]);


                    cust.AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]);
                    cust.AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]);
                    cust.CreatedDate = Convert.ToString(rdr["CREATED_DATE"]);

                    cust.Createdby = Convert.ToString(rdr["CREATED_BY"]);
                    cust.LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]);
                    cust.LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"]);
                    //

                    cust.AddressType = Convert.ToString(rdr["ADDRESS_TYPE"]);
                    cust.HouseIdentifier = Convert.ToString(rdr["HOUSE_IDENTIFIER"]);
                    cust.Addressline1 = Convert.ToString(rdr["ADDRESS_LINE_1"]);
                    cust.Addressline2 = Convert.ToString(rdr["ADDRESS_LINE_2"]);

                    cust.Addressline3 = Convert.ToString(rdr["ADDRESS_LINE_3"]);
                    cust.AdministrativeArea = Convert.ToString(rdr["ADMINISTRATIVE_AREA"]);
                    cust.Locality = Convert.ToString(rdr["LOCALITY"]);
                    cust.LocationCoordinates = Convert.ToString(rdr["LOCATION_COORDINATES"]);
                    cust.PostCode = Convert.ToString(rdr["POST_CODE"]);
                    cust.PostOfficeBox = Convert.ToString(rdr["POST_OFFICE_BOX"]);

                    cust.Country = utility.TitleCase(Convert.ToString(rdr["COUNTRY"]));
                    cust.State = utility.TitleCase(Convert.ToString(rdr["STATE"])).Trim();
                    cust.City = utility.TitleCase(Convert.ToString(rdr["CITY"]));
                    cust.CountryOfResidence = utility.TitleCase(Convert.ToString(rdr["COUNTRY_OF_RESIDENCE"]));
                    //
                    cust.PhoneCategory = (Convert.ToString(rdr["PHONE_CATEGORY"]));
                    cust.AreaCode = Convert.ToString(rdr["AREA_CODE"]);
                    cust.CountryCode = Convert.ToString(rdr["COUNTRY_CODE"]);
                    cust.PhoneNumber = Convert.ToString(rdr["PHONE_NUMBER"]);

                    cust.ExtensionNo = Convert.ToString(rdr["EXTENSION_NO"]);
                    cust.PhoneType = (Convert.ToString(rdr["PHONE_TYPE"]));
                    cust.ChannelSupported = (Convert.ToString(rdr["CHANNEL_SUPPORTED"]));
                    cust.ReachableHours = (Convert.ToString(rdr["REACHABLE_HOURS"]));
                }


            }
            catch (Exception ex)
            {
                cust.ErrorMessage = id.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
            }

            return cust;
        }

        public Biodata GetBiodata(string custId, string table)
        {
            bio = new Biodata();
            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = conn;

            objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_customerid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.NVarchar2).Value = custId;
            //
            //int created = 0;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();//

                OracleDataReader reader;
                //var reader = new NullableDataReader(objCmd.ExecuteReader());
                reader = objCmd.ExecuteReader(behavior: CommandBehavior.SingleRow);
                //dr = new INullableDataReader(cmd.ExecuteReader());
                //if (reader.RecordsAffected >0 )
                if (reader.Read())
                {

                    bio = new Biodata
                    {
                        CustomerId = reader["CUSTOMER_NO"].ToString().ToUpper(),
                        Prefix = reader.IsDBNull(reader.GetOrdinal("PREFIX")) ? String.Empty : reader["PREFIX"].ToString().ToUpper(),
                        FirstName = reader.IsDBNull(reader.GetOrdinal("FIRST_NAME")) ? String.Empty : reader["FIRST_NAME"].ToString(),
                        ShortName = reader.IsDBNull(reader.GetOrdinal("SHORT_NAME")) ? String.Empty : reader["SHORT_NAME"].ToString(),
                        MiddleName = reader.IsDBNull(reader.GetOrdinal("FIRST_NAME")) ? String.Empty : reader["FIRST_NAME"].ToString(),
                        LastName = reader.IsDBNull(reader.GetOrdinal("LAST_NAME")) ? String.Empty : reader["LAST_NAME"].ToString(),
                        PreferredName = reader.IsDBNull(reader.GetOrdinal("PREFERRED_NAME")) ? String.Empty : reader["PREFERRED_NAME"].ToString(),
                        Height = reader.IsDBNull(reader.GetOrdinal("HEIGHT")) ? String.Empty : reader["HEIGHT"].ToString(),

                        DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DATE_OF_BIRTH")) ? String.Empty : String.Format("{0:d/M/yyyy}", reader["DATE_OF_BIRTH"]),
                        Religion = reader.IsDBNull(reader.GetOrdinal("RELIGION")) ? String.Empty : reader["RELIGION"].ToString(),
                        Nationality = reader.IsDBNull(reader.GetOrdinal("NATIONALITY")) ? String.Empty : reader["NATIONALITY"].ToString(),
                        Complexion = reader.IsDBNull(reader.GetOrdinal("COMPLEXION")) ? String.Empty : reader["COMPLEXION"].ToString(),
                        EyeColor = reader.IsDBNull(reader.GetOrdinal("EYE_COLOR")) ? String.Empty : reader["EYE_COLOR"].ToString(),
                        MaritalStatus = reader.IsDBNull(reader.GetOrdinal("MARITAL_STATUS")) ? String.Empty : reader["MARITAL_STATUS"].ToString(),
                        Gender = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? String.Empty : reader["GENDER"].ToString(),
                        FacialMarks = reader.IsDBNull(reader.GetOrdinal("FACIAL_MARKS")) ? String.Empty : reader["FACIAL_MARKS"].ToString(),
                        Disability = reader.IsDBNull(reader.GetOrdinal("DISABILITY")) ? String.Empty : reader["DISABILITY"].ToString(),
                        PlaceOfBirth = reader.IsDBNull(reader.GetOrdinal("PLACE_OF_BIRTH")) ? String.Empty : reader["PLACE_OF_BIRTH"].ToString(),
                        //reader["PLACE_OF_BIRTH"].ToString(),
                        StateOfOrigin = reader.IsDBNull(reader.GetOrdinal("STATE_OF_ORIGIN")) ? String.Empty : reader["STATE_OF_ORIGIN"].ToString(),
                        //reader["STATE_OF_ORIGIN"].ToString(),
                        SocialSecurityNumber = reader.IsDBNull(reader.GetOrdinal("SOCIAL_SECURITY_NUMBER")) ? String.Empty : reader["SOCIAL_SECURITY_NUMBER"].ToString(),
                        //reader["SOCIAL_SECURITY_NUMBER"].ToString(),
                        MotherMaidenName = reader.IsDBNull(reader.GetOrdinal("MOTHER_MAIDEN_NAME")) ? String.Empty : reader["MOTHER_MAIDEN_NAME"].ToString(),
                        //reader["MOTHER_MAIDEN_NAME"].ToString(),
                        TaxIdentificationNumber = reader.IsDBNull(reader.GetOrdinal("TAX_IDENTIFICATION_NUMBER")) ? String.Empty : reader["TAX_IDENTIFICATION_NUMBER"].ToString(),
                        //reader["TAX_IDENTIFICATION_NUMBER"].ToString(),
                        Race = reader.IsDBNull(reader.GetOrdinal("RACE")) ? String.Empty : reader["RACE"].ToString(),
                        //reader["RACE"].ToString(),
                        DocumentType = reader.IsDBNull(reader.GetOrdinal("DOCUMENT_TYPE")) ? String.Empty : reader["DOCUMENT_TYPE"].ToString(),
                        //reader["DOCUMENT_TYPE"].ToString(),
                        IssuingAuthority = reader.IsDBNull(reader.GetOrdinal("ISSUING_AUTHORITY")) ? String.Empty : reader["ISSUING_AUTHORITY"].ToString(),
                        //reader["ISSUING_AUTHORITY"].ToString(),
                        IssuingState = reader.IsDBNull(reader.GetOrdinal("ISSUING_STATE")) ? String.Empty : reader["ISSUING_STATE"].ToString(),
                        //reader["ISSUING_STATE"].ToString(),
                        IssueCity = reader.IsDBNull(reader.GetOrdinal("ISSUING_CITY")) ? String.Empty : reader["ISSUING_CITY"].ToString(),
                        //Convert.ToString(reader["ISSUING_CITY"]),
                        IssuingDate = reader.IsDBNull(reader.GetOrdinal("ISSUED_DATE")) ? String.Empty : String.Format("{0:d/M/yyyy}", reader["ISSUED_DATE"]),
                        //Convert.ToDateTime(reader["ISSUED_DATE"]),   String.Format("{0:d/M/yyyy}", rdr["ANNIVERSARY_DATE1"]);
                        IdentityNumber = reader.IsDBNull(reader.GetOrdinal("DOCUMENT_NUMBER")) ? String.Empty : reader["DOCUMENT_NUMBER"].ToString(),
                        //reader["DOCUMENT_NUMBER"].ToString(),
                        IssuingCountry = reader.IsDBNull(reader.GetOrdinal("ISSUING_COUNTRY")) ? String.Empty : reader["ISSUING_COUNTRY"].ToString(),
                        //reader["ISSUING_COUNTRY"].ToString(),

                        ExpiryDate = reader.IsDBNull(reader.GetOrdinal("EXPIRY_DATE")) ? String.Empty : String.Format("{0:d/M/yyyy}", reader["EXPIRY_DATE"]),

                    };

                }//end of while
                else
                {
                    bio.ErrorMessage = "Not found";
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                bio.ErrorMessage = "Error:" + ex.Message;
            }
            finally
            {
                objCmd = null;
                conn.Close();
                //bio.CustomerName = GetCustomer(bio.CustomerNo).CustomerName;
            }

            return bio;

        }



        public int SaveFamilyInfo(int? refId)
        {
            OracleCommand objCmd = new OracleCommand();


            return 0;
        }
        public Account GetAccountInfo4Makers(string custNo, string acctNo)
        {
            account = new Account();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();

            // make call to StoredProcedure "pkg_cdms2.get_basic_details"
            objCmd.CommandText = "pkg_cdms_maker.get_account_info";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_custNo", OracleDbType.NVarchar2).Value = custNo;
            objCmd.Parameters.Add("p_acctNo", OracleDbType.NVarchar2).Value = acctNo;
            //
            objCmd.Parameters.Add("custinfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {

                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                while (rdr.Read())
                {
                    account = new Account
                    {

                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),
                        TypeOfAccount = Convert.ToString(rdr["TYPE_OF_ACCOUNT"]),
                        //AccountHolder = Convert.ToString(rdr["ACCOUNT_HOLDER"]),
                        AccountNumber = Convert.ToString(rdr["ACCOUNT_NUMBER"]),
                        AccountOfficer = Convert.ToString(rdr["ACCOUNT_OFFICER"]),
                        AccountTitle = Convert.ToString(rdr["ACCOUNT_TITLE"]),
                        Branch = Convert.ToString(rdr["BRANCH"]),
                        BranchClass = Convert.ToString(rdr["BRANCH_CLASS"]),
                        BusinessDivision = Convert.ToString(rdr["BUSINESS_DIVISION"]),
                        BizSegment = Convert.ToString(rdr["BUSINESS_SEGMENT"]),
                        BizSize = Convert.ToString(rdr["BUSINESS_SIZE"]),
                        BVNNo = Convert.ToString(rdr["BVN_NUMBER"]),
                        CAVRequired = Convert.ToString(rdr["CAV_REQUIRED"]),
                        CustomerIc = Convert.ToString(rdr["CUSTOMER_IC"]),
                        //CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),
                        CustomerSegment = Convert.ToString(rdr["CUSTOMER_SEGMENT"]),
                        CustomerType = Convert.ToString(rdr["CUSTOMER_TYPE"]),
                        OperatingInstruction = Convert.ToString(rdr["OPERATING_INSTRUCTION"]),
                        OriginatingBranch = Convert.ToString(rdr["ORIGINATING_BRANCH"]),


                        CardPreference = Convert.ToString(rdr["CARD_PREFERENCE"]),
                        ElectronicBankingPreference = Convert.ToString(rdr["ELECTRONIC_BANKING_PREFERENCE"]),
                        StatementPreferences = Convert.ToString(rdr["STATEMENT_PREFERENCES"]),
                        TranxAlertPreference = Convert.ToString(rdr["TRANSACTION_ALERT_PREFERENCE"]),
                        StatementFrequency = Convert.ToString(rdr["STATEMENT_FREQUENCY"]),
                        ChequeBookRequisition = Convert.ToString(rdr["CHEQUE_BOOK_REQUISITION"]),
                        ChequeLeavesRequired = Convert.ToString(rdr["CHEQUE_LEAVES_REQUIRED"]),
                        ChequeConfirmation = Convert.ToString(rdr["CHEQUE_CONFIRMATION"]),
                        ChequeConfirmationThreshold = Convert.ToString(rdr["CHEQUE_CONFIRMATION_THRESHOLD"]),
                        ChequeConfirmationThresholdRange = Convert.ToString(rdr["CHEQUE_CONFIRM_THRESHLDRANGE"]),
                        OnlineTransferLimit = Convert.ToString(rdr["ONLINE_TRANSFER_LIMIT"]),
                        OnlineTransferLimitRange = Convert.ToString(rdr["ONLINE_TRANSFER_LIMIT_RANGE"]),
                        Token = Convert.ToString(rdr["TOKEN"]),
                        AccountSignatory = Convert.ToString(rdr["ACCOUNT_SIGNATORY"]),
                        SecondSignatory = Convert.ToString(rdr["SECOND_SIGNATORY"])


                        //AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        //AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        //CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        //Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        //LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        //LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }


            }
            catch (Exception ex)
            {
                account.ErrorMessage = custNo.ToString() + "  " + ex.Message + ex.InnerException;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //account.CustomerName = GetCustomer(account.CustomerNo).CustomerName;
            }

            return account;

        }


        public Account GetAccountInfo4Checkers(string custNo, string acctNo)
        {
            account = new Account();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();

            // make call to StoredProcedure "pkg_cdms2.get_basic_details"
            objCmd.CommandText = "pkg_cdms_checker.get_account_info";

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_custNo", OracleDbType.NVarchar2).Value = custNo;
            objCmd.Parameters.Add("p_acctNo", OracleDbType.NVarchar2).Value = acctNo;
            //
            objCmd.Parameters.Add("custinfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {

                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                while (rdr.Read())
                {
                    account = new Account
                    {

                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),
                        TypeOfAccount = Convert.ToString(rdr["TYPE_OF_ACCOUNT"]),
                        //AccountHolder = Convert.ToString(rdr["ACCOUNT_HOLDER"]),
                        AccountNumber = Convert.ToString(rdr["ACCOUNT_NUMBER"]),
                        AccountOfficer = Convert.ToString(rdr["ACCOUNT_OFFICER"]),
                        AccountTitle = Convert.ToString(rdr["ACCOUNT_TITLE"]),
                        Branch = Convert.ToString(rdr["BRANCH"]),
                        BranchClass = Convert.ToString(rdr["BRANCH_CLASS"]),
                        BusinessDivision = Convert.ToString(rdr["BUSINESS_DIVISION"]),
                        BizSegment = Convert.ToString(rdr["BUSINESS_SEGMENT"]),
                        BizSize = Convert.ToString(rdr["BUSINESS_SIZE"]),
                        BVNNo = Convert.ToString(rdr["BVN_NUMBER"]),
                        CAVRequired = Convert.ToString(rdr["CAV_REQUIRED"]),
                        CustomerIc = Convert.ToString(rdr["CUSTOMER_IC"]),
                        //CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),
                        CustomerSegment = Convert.ToString(rdr["CUSTOMER_SEGMENT"]),
                        CustomerType = Convert.ToString(rdr["CUSTOMER_TYPE"]),
                        OperatingInstruction = Convert.ToString(rdr["OPERATING_INSTRUCTION"]),
                        OriginatingBranch = Convert.ToString(rdr["ORIGINATING_BRANCH"]),


                        CardPreference = Convert.ToString(rdr["CARD_PREFERENCE"]),
                        ElectronicBankingPreference = Convert.ToString(rdr["ELECTRONIC_BANKING_PREFERENCE"]),
                        StatementPreferences = Convert.ToString(rdr["STATEMENT_PREFERENCES"]),
                        TranxAlertPreference = Convert.ToString(rdr["TRANSACTION_ALERT_PREFERENCE"]),
                        StatementFrequency = Convert.ToString(rdr["STATEMENT_FREQUENCY"]),
                        ChequeBookRequisition = Convert.ToString(rdr["CHEQUE_BOOK_REQUISITION"]),
                        ChequeLeavesRequired = Convert.ToString(rdr["CHEQUE_LEAVES_REQUIRED"]),
                        ChequeConfirmation = Convert.ToString(rdr["CHEQUE_CONFIRMATION"]),
                        ChequeConfirmationThreshold = Convert.ToString(rdr["CHEQUE_CONFIRMATION_THRESHOLD"]),
                        ChequeConfirmationThresholdRange = Convert.ToString(rdr["CHEQUE_CONFIRM_THRESHLDRANGE"]),
                        OnlineTransferLimit = Convert.ToString(rdr["ONLINE_TRANSFER_LIMIT"]),
                        OnlineTransferLimitRange = Convert.ToString(rdr["ONLINE_TRANSFER_LIMIT_RANGE"]),
                        Token = Convert.ToString(rdr["TOKEN"]),
                        AccountSignatory = Convert.ToString(rdr["ACCOUNT_SIGNATORY"]),
                        SecondSignatory = Convert.ToString(rdr["SECOND_SIGNATORY"])


                        //AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        //AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        //CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        //Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        //LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        //LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }


            }
            catch (Exception ex)
            {
                account.ErrorMessage = custNo.ToString() + "  " + ex.Message + ex.InnerException;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //account.CustomerName = GetCustomer(account.CustomerNo).CustomerName;
            }

            return account;

        }

        public CompanyInformation GetCompanyInfo(string custNo, string table)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_customerno";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerno", OracleDbType.NVarchar2).Value = custNo;
            //

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read()) //&& rdr.RecordsAffected > 0
                {
                    compInfo = new CompanyInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        CompanyName = Convert.ToString(rdr["COMPANY_NAME"]),
                        DateOfIncorpRegistration = Convert.ToDateTime(rdr["DATE_OF_INCORP_REGISTRATION"]),
                        CustomerType = Convert.ToString(rdr["CUSTOMER_TYPE"]),
                        RegisteredAddress = Convert.ToString(rdr["REGISTERED_ADDRESS"]),
                        BizCategory = Convert.ToString(rdr["CATEGORY_OF_BUSINESS"]),

                        AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }
                else
                {
                    compInfo.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                compInfo.ErrorMessage = custNo.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //compInfo.CustomerName = GetCustomer(compInfo.CustomerNo).CustomerName;
            }

            return compInfo;

        }





        public CorpAdditionalInformation GetCorpAdditionalInfo(string custNo, string table)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_customerno";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerno", OracleDbType.NVarchar2).Value = custNo;
            //

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read()) //&& rdr.RecordsAffected > 0
                {
                    CorpAddnlInfo = new CorpAdditionalInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        AffliliatedCompBody = Convert.ToString(rdr["AFFILIATE_COMPANY_BODY"]),
                        ParentCompanyIncCountry = Convert.ToString(rdr["PARENT_COMPANY_CTRY_INCORP"])



                        //AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        //AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        //CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        //Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        //LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        //LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }
                else
                {
                    CorpAddnlInfo.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                CorpAddnlInfo.ErrorMessage = custNo.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //compInfo.CustomerName = GetCustomer(compInfo.CustomerNo).CustomerName;
            }

            return CorpAddnlInfo;

        }


        public CorpPOAInformation GetCorpPOAInfo(string custNo, string acctNo, string table)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_custNo and ACCOUNT_NUMBER=:p_acctNo";
            objCmd.CommandType = CommandType.Text;
            //objCmd.Parameters.Add("p_customerno", OracleDbType.NVarchar2).Value = custNo;
            objCmd.Parameters.Add("p_custNo", OracleDbType.NVarchar2).Value = custNo;
            objCmd.Parameters.Add("p_acctNo", OracleDbType.NVarchar2).Value = acctNo;
            //

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read()) //&& rdr.RecordsAffected > 0
                {
                    CorpPOAInfo = new CorpPOAInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),
                        AccountNo = Convert.ToString(rdr["ACCOUNT_NUMBER"]),
                        HoldersNAme = Convert.ToString(rdr["HOLDER_NAME"]),
                        Address = Convert.ToString(rdr["ADDRESS"]),
                        Country = Convert.ToString(rdr["COUNTRY"]),
                        Nationality = Convert.ToString(rdr["NATIONALITY"]),
                        Telephone = Convert.ToString(rdr["TELEPHONE_NUMBER"])


                        //AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        //AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        //CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        //Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        //LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        //LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }
                else
                {
                    CorpPOAInfo.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                CorpPOAInfo.ErrorMessage = custNo.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //compInfo.CustomerName = GetCustomer(compInfo.CustomerNo).CustomerName;
            }

            return CorpPOAInfo;

        }


        public AddressInformation GetCorpAddressInfo(string custId, string mngtId, string table)
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where Customer_No=:p_customerid and MANAGEMENT_ID=:p_mngtid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.NVarchar2).Value = custId;
            objCmd.Parameters.Add("p_mngtid", OracleDbType.NVarchar2).Value = mngtId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read()) //&& rdr.RecordsAffected > 0
                {
                    AddressInfo = new AddressInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        ManagementNo = Convert.ToString(rdr["MANAGEMENT_ID"]),
                        HomeNo = Convert.ToString(rdr["HOUSE_NUMBER"]),
                        StreetName = Convert.ToString(rdr["STREET_NAME"]),
                        City = Convert.ToString(rdr["CITY_OR_TOWN"]),
                        LGA = Convert.ToString(rdr["LGA_RESIDENTIAL"]),
                        State = Convert.ToString(rdr["STATE"]),
                        NearestBstop = Convert.ToString(rdr["NEAREST_BUS_STOP_LANDMARK"]),
                        Country = Convert.ToString(rdr["COUNTRY"]),
                        Email = Convert.ToString(rdr["EMAIL_ADDRESS"]),
                        MobileNo = Convert.ToString(rdr["MOBILE_NUMBER"]),
                        OfficeNo = Convert.ToString(rdr["OFFICE_NUMBER"]),


                    };
                }
                else
                {
                    AddressInfo.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                AddressInfo.ErrorMessage = custId + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //accountInfo.CustomerName = GetCustomer(accountInfo.CustomerNo).CustomerName;
            }

            return AddressInfo;

        }

        public DIRBiodataInformation GetCorpBiodataInfo(string custId, string mngtId, string table)
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where Customer_No=:p_customerid and MANAGEMENT_ID=:p_mngtid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.NVarchar2).Value = custId;
            objCmd.Parameters.Add("p_mngtid", OracleDbType.NVarchar2).Value = mngtId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read()) //&& rdr.RecordsAffected > 0
                {
                    BiodataInfo = new DIRBiodataInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        ManagementNo = Convert.ToString(rdr["MANAGEMENT_ID"]),
                        ManagementType = Convert.ToString(rdr["MANAGEMENT_TYPE"]),
                        Title = Convert.ToString(rdr["TITLE"]),
                        MaritalStatus = Convert.ToString(rdr["MARITAL_STATUS"]),
                        Surname = Convert.ToString(rdr["SURNAME"]),
                        FirstName = Convert.ToString(rdr["FIRST_NAME"]),
                        Othernames = Convert.ToString(rdr["OTHER_NAMES"]),
                        DOB = Convert.ToString(rdr["DATE_OF_BIRTH"]),
                        POB = Convert.ToString(rdr["PLACE_OF_BIRTH"]),
                        Sex = Convert.ToString(rdr["SEX"]),
                        Nationality = Convert.ToString(rdr["NATIONALITY"]),
                        MothersMaidenName = Convert.ToString(rdr["MOTHERS_MAIDEN_NAME"]),
                        Occupation = Convert.ToString(rdr["OCCUPATION"]),
                        JobTitle = Convert.ToString(rdr["STATUS_OR_JOB_TITLE"]),
                        ClassOfSignitory = Convert.ToString(rdr["CLASS_OF_SIGNATORY"]),


                    };
                }
                else
                {
                    BiodataInfo.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                BiodataInfo.ErrorMessage =  ex.Message;
            }//custId + "  " +
            finally
            {
                rdr.Close();
                conn.Close();
                //accountInfo.CustomerName = GetCustomer(accountInfo.CustomerNo).CustomerName;
            }

            return BiodataInfo;

        }



        public DIRNOKInformation GetCorpNOKInfo(string custId, string mngtId, string table)
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where Customer_No=:p_customerid and MANAGEMENT_ID=:p_mngtid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.Varchar2).Value = custId;
            objCmd.Parameters.Add("p_mngtid", OracleDbType.Varchar2).Value = mngtId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read()) //&& rdr.RecordsAffected > 0
                {
                    NOKInfo = new DIRNOKInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        ManagementNo = Convert.ToString(rdr["MANAGEMENT_ID"]),
                        Title = Convert.ToString(rdr["TITLE"]),
                        Surname = Convert.ToString(rdr["SURNAME"]),
                        FirstName = Convert.ToString(rdr["FIRST_NAME"]),
                        Othernames = Convert.ToString(rdr["OTHER_NAME"]),
                        DOB = Convert.ToString(rdr["DATE_OF_BIRTH"]),
                        Sex = Convert.ToString(rdr["SEX"]),
                        Relationship = Convert.ToString(rdr["RELATIONSHIP"]),
                        OfficeNo = Convert.ToString(rdr["OFFICE_NO"]),
                        MobileNo = Convert.ToString(rdr["MOBILE_NO"]),
                        Email = Convert.ToString(rdr["EMAIL_ADDRESS"]),
                        HouseNo = Convert.ToString(rdr["HOUSE_NUMBER"]),
                        StreetName = Convert.ToString(rdr["STREET_NAME"]),
                        NearestBStop = Convert.ToString(rdr["NEAREST_BUS_STOP_LANDMARK"]),
                        City = Convert.ToString(rdr["CITY_TOWN"]),
                        LGA = Convert.ToString(rdr["LGA"]),
                        ZipPostalCode = Convert.ToString(rdr["ZIP_POSTAL_CODE"]),
                        State = Convert.ToString(rdr["STATE"]),
                        Country = Convert.ToString(rdr["COUNTRY"])
                    };
                }
                else
                {
                    NOKInfo.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                NOKInfo.ErrorMessage = custId + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //accountInfo.CustomerName = GetCustomer(accountInfo.CustomerNo).CustomerName;
            }

            return NOKInfo;

        }

        public DIRForeignerInformation GetCorpForeignerInfo(string custId, string mngtId, string table)
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where Customer_No=:p_customerid and MANAGEMENT_ID=:p_mngtid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.NVarchar2).Value = custId;
            objCmd.Parameters.Add("p_mngtid", OracleDbType.NVarchar2).Value = mngtId;


            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read()) //&& rdr.RecordsAffected > 0
                {
                    ForeignerInfo = new DIRForeignerInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        ManagementNo = Convert.ToString(rdr["MANAGEMENT_ID"]),
                        isForeigner = Convert.ToString(rdr["FOREIGNER"]),
                        ResidentPermitNo = Convert.ToString(rdr["RESIDENCE_PERMIT_NUMBER"]),
                        Nationality = Convert.ToString(rdr["NATIONALITY"]),
                        PermitIssueDate = Convert.ToDateTime(rdr["PERMIT_ISSUE_DATE"]),
                        PermitExpiryDate = Convert.ToDateTime(rdr["PERMIT_EXPIRY_DATE"]),
                        ForeignTelNo = Convert.ToString(rdr["FOREIGN_TEL_NUMBER"]),
                        PassportResidentPermitNo = Convert.ToString(rdr["PASSPORT_RESIDENT_PERMIT_NO"]),
                        ForeignAddy = Convert.ToString(rdr["FOREIGN_ADDRESS"]),
                        City = Convert.ToString(rdr["CITY"]),
                        Country = Convert.ToString(rdr["COUNTRY"]),
                        ZipPostalCode = Convert.ToString(rdr["ZIP_POSTAL_CODE"])

                    };
                }
                else
                {
                    ForeignerInfo.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                ForeignerInfo.ErrorMessage = custId + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //accountInfo.CustomerName = GetCustomer(accountInfo.CustomerNo).CustomerName;
            }

            return ForeignerInfo;

        }


        public DIRIdentityInformation GetCorpIDInfo(string custId, string mngtId, string table)
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where Customer_No=:p_customerid and MANAGEMENT_ID=:p_mngtid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.NVarchar2).Value = custId;
            objCmd.Parameters.Add("p_mngtid", OracleDbType.NVarchar2).Value = mngtId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read()) //&& rdr.RecordsAffected > 0
                {
                    DIRIDInfo = new DIRIdentityInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        ManagementNo = Convert.ToString(rdr["MANAGEMENT_ID"]),
                        TypeOfID = Convert.ToString(rdr["TYPE_OF_IDENTIFICATION"]),
                        IDNo = Convert.ToString(rdr["ID_NO"]),
                        IDIssueDate = Convert.ToDateTime(rdr["ID_ISSUE_DATE"]),
                        IDExpiryDate = Convert.ToDateTime(rdr["ID_EXPIRY_DATE"]),
                        BVNID = Convert.ToString(rdr["BVN_ID"]),
                        TIN = Convert.ToString(rdr["TIN"])

                    };
                }
                else
                {
                    DIRIDInfo.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                DIRIDInfo.ErrorMessage = custId + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //accountInfo.CustomerName = GetCustomer(accountInfo.CustomerNo).CustomerName;
            }

            return DIRIDInfo;

        }


        public AWOBInformation GetCorpAWOBInfo(string custId, string table)
        {
            // accountWOBInfo = new AWOBInformation();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where Customer_No=:p_customerid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.NVarchar2).Value = custId;
            //


            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read()) //&& rdr.RecordsAffected > 0
                {
                    accountWOBInfo = new AWOBInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        BankName = Convert.ToString(rdr["BANK_NAME"]),
                        BankAddress = Convert.ToString(rdr["BANK_ADDRESS_OR_BRANCH"]),
                        AccountNo = Convert.ToString(rdr["ACCOUNT_NUMBER"]),
                        AccountName = Convert.ToString(rdr["ACCOUNT_NAME"]),
                        Status = Convert.ToString(rdr["STATUS"]),

                    };
                }
                else
                {
                    accountWOBInfo.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                accountWOBInfo.ErrorMessage = custId + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //accountInfo.CustomerName = GetCustomer(accountInfo.CustomerNo).CustomerName;
            }

            return accountWOBInfo;

        }




        public AccountInformation GetAccountInformation(string custId, string table)
        {
            accountInfo = new AccountInformation();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where Customer_No=:p_customerid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.NVarchar2).Value = custId;
            //


            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read()) //&& rdr.RecordsAffected > 0
                {
                    accountInfo = new AccountInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        AccountType = Convert.ToString(rdr["ACCOUNT_TYPE"]),
                        DomicileBranch = Convert.ToString(rdr["DOMICILE_BRANCH"]),
                        ReferralCode = Convert.ToString(rdr["REFERRAL_CODE"]),
                        AccountNo = Convert.ToString(rdr["ACCOUNT_NUMBER"]),
                        AccountName = Convert.ToString(rdr["ACCOUNT_NAME"]),
                        CardPreference = Convert.ToString(rdr["CARD_PREFERENCE"]),
                        ElectronicBankingPrefer = Convert.ToString(rdr["ELECTRONIC_BANKING_PREFERENCE"]),
                        StatementPreferences = Convert.ToString(rdr["STATEMENT_PREFERENCES"]),
                        TransactionAlertPreference = Convert.ToString(rdr["TRANSACTION_ALERT_PREFERENCE"]),
                        StatementFrequency = Convert.ToString(rdr["STATEMENT_FREQUENCY"]),
                        ChequeBookRequisition = Convert.ToString(rdr["CHEQUE_BOOK_REQUISITION"]),
                        ChequeConfirmation = Convert.ToString(rdr["CHEQUE_CONFIRMATION"]),
                        ChequeConfirmThreshold = Convert.ToString(rdr["CHEQUE_CONFIRMATION_THRESHOLD"])

                        //AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        //AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        //CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        //Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        //LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        //LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }
                else
                {
                    accountInfo.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                accountInfo.ErrorMessage = custId + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //accountInfo.CustomerName = GetCustomer(accountInfo.CustomerNo).CustomerName;
            }

            return accountInfo;

        }
        public CompanyDetails GetCompanyDetails(string refId, string table)
        {
            companyDetails = new CompanyDetails();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn; conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_refid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                while (rdr.Read())
                {
                    //director = new Director
                    companyDetails = new CompanyDetails
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),
                        CertOfIncorpRegNo = Convert.ToString(rdr["CERT_OF_INCORP_REG_NO"]),
                        JurisdictionOfIncorpReg = Convert.ToString(rdr["JURISDICTION_OF_INCORP_REG"]),
                        ScumlNo = Convert.ToString(rdr["SCUML_NO"]),
                        GenderControlling51Perc = Convert.ToString(rdr["GENDER_CONTROLLING_51_PERC"]),
                        SectorOrIndustry = Convert.ToString(rdr["SECTOR_OR_INDUSTRY"]),
                        OperatingBusiness1 = Convert.ToString(rdr["OPERATING_BUSINESS_1"]),
                        City1 = Convert.ToString(rdr["CITY_1"]),
                        Country1 = Convert.ToString(rdr["COUNTRY_1"]),
                        ZipCode1 = Convert.ToString(rdr["ZIP_CODE_1"]),
                        BizAddressRegOffice1 = Convert.ToString(rdr["BIZ_ADDRESS_REG_OFFICE_1"]),
                        OperatingBusiness2 = Convert.ToString(rdr["OPERATING_BUSINESS_2"]),
                        City2 = Convert.ToString(rdr["CITY_2"]),
                        Country2 = Convert.ToString(rdr["COUNTRY_2"]),
                        ZipCode2 = Convert.ToString(rdr["ZIP_CODE_2"]),
                        BizAddressRegOffice2 = Convert.ToString(rdr["BIZ_ADDRESS_REG_OFFICE_2"]),
                        CompanyEmailAddress = Convert.ToString(rdr["COMPANY_EMAIL_ADDRESS"]),
                        Website = Convert.ToString(rdr["WEBSITE"]),
                        OfficeNumber = Convert.ToString(rdr["OFFICE_NUMBER"]),
                        MobileNumber = Convert.ToString(rdr["MOBILE_NUMBER"]),
                        Tin = Convert.ToString(rdr["TIN"]),
                        CrmbNoBorrowerCode = Convert.ToString(rdr["CRMB_NO_BORROWER_CODE"]),
                        ExpectedAnnualTurnover = Convert.ToString(rdr["EXPECTED_ANNUAL_TURNOVER"]),
                        IsCompanyOnStockExch = Convert.ToString(rdr["IS_COMPANY_ON_STOCK_EXCH"]),
                        StockExchangeName = Convert.ToString(rdr["STOCK_EXCHANGE_NAME"]),

                        AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }
            }
            catch (Exception ex)
            {
                companyDetails.ErrorMessage = refId.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //companyDetails.CustomerName = GetCustomer(companyDetails.CustomerId).CustomerName;
            }
            return companyDetails;
        }
        public Jurat GetJuratInfo(string refId, string table)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + "  where CUSTOMER_NO=:p_refid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read())
                {
                    jurat = new Jurat
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),
                        OathDate = Convert.ToDateTime(rdr["DATE_OF_OATH"]),
                        NameOfInterpreter = Convert.ToString(rdr["NAME_OF_INTERPRETER"]),

                        AddressOfInterpreter = Convert.ToString(rdr["ADDRESS_OF_INTERPRETER"]),
                        TelephoneNo = Convert.ToString(rdr["TELEPHONE_NO"]),
                        LanguageOfInterpretation = Convert.ToString(rdr["LANGUAGE_OF_INTERPRETATION"]),

                        AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }
                else
                {
                    jurat.ErrorMessage = "Not found!!!";
                }
            }
            catch (Exception ex)
            {
                jurat.ErrorMessage = refId.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                // jurat.CustomerName = GetCustomer(jurat.CustomerNo).CustomerName;
            }

            return jurat;
        }


        public AdditionalInformation GetAdditionalInfo(string refId, string table)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + "  where CUSTOMER_NO=:p_refid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read())
                {
                    AddtnlInfo = new AdditionalInformation
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),
                        AnnualSalaryExpectedInc = Convert.ToString(rdr["ANNUAL_SALARY_EXPECTED_INC"]),
                        FaxNumber = Convert.ToString(rdr["FAX_NUMBER"]),

                        AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }
                else
                {
                    AddtnlInfo.ErrorMessage = "Not found!!!";
                }
            }
            catch (Exception ex)
            {
                AddtnlInfo.ErrorMessage = refId.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //AddtnlInfo.CustomerName = GetCustomer(AddtnlInfo.CustomerNo).CustomerName;
            }

            return AddtnlInfo;
        }
        public AuthForFinInclusion GetAuthFinInclusion(string refId, string table)
        {
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + "  where CUSTOMER_NO=:p_refid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read())
                {
                    Auth4FinInc = new AuthForFinInclusion
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),
                        SocialFinancialDisadvtage = Convert.ToString(rdr["SOCIAL_FINANCIAL_DISADVTAGE"]),
                        SocialFinancialDocuments = Convert.ToString(rdr["SOCIAL_FINANCIAL_DOCUMENTS"]),
                        EnjoyedTieredKyc = Convert.ToString(rdr["ENJOYED_TIERED_KYC"]),
                        RiskCategory = Convert.ToString(rdr["RISK_CATEGORY"]),
                        MandateAuthCombineRule = Convert.ToString(rdr["MANDATE_AUTH_COMBINE_RULE"]),
                        AccountWithOtherBanks = Convert.ToString(rdr["ACCOUNT_WITH_OTHER_BANKS"]),

                        AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }
                else
                {
                    Auth4FinInc.ErrorMessage = "Not found!!!";
                }
            }
            catch (Exception ex)
            {
                Auth4FinInc.ErrorMessage = refId.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //Auth4FinInc.CustomerName = GetCustomer(Auth4FinInc.CustomerNo).CustomerName;
            }

            return Auth4FinInc;
        }


        public Subsidiary GetSubsidiary(string refId, string table)
        {
            sub = new Subsidiary();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn; conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where REFERENCE_ID=:p_refid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                while (rdr.Read())
                {
                    sub = new Subsidiary
                    {
                        CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),
                        ReferenceId = Convert.ToInt32(Convert.ToString(rdr["REFERENCE_ID"])),
                        CustNameBiz = Convert.ToString(rdr["CUST_NAME_BIZ"]),
                        CustTypeBiz = Convert.ToString(rdr["CUST_TYPE_BIZ"]),
                        AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }

                return sub;
            }
            catch (Exception ex)
            {
                sub.ErrorMessage = refId.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                // sub.CustomerName = GetCustomer(sub.CustomerId).CustomerName;
            }

            return sub;

        }


        public Share GetShare(Int64 refId, string table)
        {
            share = new Share();

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn; conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where REFERENCE_ID=:p_refid";//RELATED_CUSTOMER
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                while (rdr.Read())
                {
                    share = new Share
                    {
                        CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),
                        ReferenceId = Convert.ToInt32(Convert.ToString(rdr["REFERENCE_ID"])),
                        ShareholderYn = Convert.ToString(rdr["SHAREHOLDER_YN"]),
                        AccessBankUnitsHeld = Convert.ToString(rdr["ACCESS_BANK_UNITS_HELD"]),
                        SubsidiariesUnitsHeld = Convert.ToString(rdr["SUBSIDIARIES_UNITS_HELD"]),
                        AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }


            }
            catch (Exception ex)
            {
                share.ErrorMessage = refId.ToString() + "  " + ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //share.CustomerName = GetCustomer(share.CustomerId).CustomerName;
            }

            return share;

        }

        public Employment GetEmployment(string custId, string table)
        {
            work = new Employment();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            conn.Open();//

            objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_customerid";
            objCmd.CommandType = CommandType.Text;
            objCmd.Parameters.Add("p_customerid", OracleDbType.NVarchar2).Value = custId;
            //


            OracleDataReader rdr;
            rdr = objCmd.ExecuteReader();
            try
            {
                // Read Value to the object
                UtilityClass utility = new UtilityClass();
                if (rdr.Read())
                {
                    work = new Employment
                    {
                        CustomerNo = Convert.ToString(rdr["CUSTOMER_NO"]),

                        EmploymentStatus = rdr["EMPLOYMENT_STATUS"] == DBNull.Value ? "" : Convert.ToString(rdr["EMPLOYMENT_STATUS"]),
                        EmployerInstName = rdr["EMPLOYER_INSTITUTION_NAME"] == DBNull.Value ? "" : Convert.ToString(rdr["EMPLOYER_INSTITUTION_NAME"]),
                        EmploymentDate = rdr["EMPLOYER_INSTITUTION_NAME"] == DBNull.Value ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(rdr["DATE_OF_EMPLOYMENT"]),
                        SectorClass = rdr["SECTOR_CLASS"] == DBNull.Value ? "" : Convert.ToString(rdr["SECTOR_CLASS"]),
                        SubSector = rdr["SUB_SECTOR"] == DBNull.Value ? "" : Convert.ToString(rdr["SUB_SECTOR"]),
                        NatureOfBuzOcc = rdr["NATURE_OF_BUSINESS_OCCUPATION"] == DBNull.Value ? "" : Convert.ToString(rdr["NATURE_OF_BUSINESS_OCCUPATION"]),
                        IndustrySegment = rdr["INDUSTRY_SEGMENT"] == DBNull.Value ? "" : Convert.ToString(rdr["INDUSTRY_SEGMENT"]),

                        AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
                        AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
                        CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
                        Createdby = Convert.ToString(rdr["CREATED_BY"]),
                        LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
                        LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
                    };
                }
                else
                {
                    work.ErrorMessage = "Not found";
                }
            }
            catch (Exception ex)
            {
                work.ErrorMessage = ex.Message;
            }
            finally
            {
                rdr.Close();
                conn.Close();
                //work.CustomerName = GetCustomer(work.CustomerId).CustomerName;
            }

            return work;
        }
        
    }//End class CustomerRepository
}


//public Credit GetCredit(Int64 refId, string table)
//{
//    credit = new Credit();
//    OracleCommand objCmd = new OracleCommand();
//    objCmd.Connection = conn; conn.Open();//

//    objCmd.CommandText = "SELECT * from " + table + " where REFERENCE_ID=:p_refid";
//    objCmd.CommandType = CommandType.Text;
//    objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

//    OracleDataReader rdr;
//    rdr = objCmd.ExecuteReader();
//    try
//    {
//        // Read Value to the object
//        UtilityClass utility = new UtilityClass();
//        while (rdr.Read())
//        {
//            // credit = new Credit
//            {
//                credit.CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]);
//                credit.ReferenceId = Convert.ToInt32(Convert.ToString(rdr["REFERENCE_ID"]));
//                credit.AddressCreditCompany = Convert.ToString(rdr["ADDRESS_CREDIT_COMPANY"]);
//                credit.CreditTypeHeld = Convert.ToString(rdr["CREDIT_TYPE_HELD"]);
//                credit.TenorOfCredit = Convert.ToString(rdr["TENOR_OF_CREDIT"]);
//                credit.AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]);
//                credit.AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]);
//                credit.CreatedDate = Convert.ToString(rdr["CREATED_DATE"]);
//                credit.Createdby = Convert.ToString(rdr["CREATED_BY"]);
//                credit.LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]);
//                credit.LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"]);
//            };
//        }


//    }
//    catch (Exception ex)
//    {
//        credit.ErrorMessage = refId.ToString() + "  " + ex.Message;
//    }
//    finally
//    {
//        rdr.Close();
//        conn.Close();
//        //credit.CustomerName = GetCustomer(credit.CustomerId).CustomerName;
//    }

//    return credit;

//}

//public Channel GetChannel(string custId, string table)
//{
//    chan = new Channel();
//    OracleCommand objCmd = new OracleCommand();
//    objCmd.Connection = conn; conn.Open();//

//    objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_refid";
//    objCmd.CommandType = CommandType.Text;
//    objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = custId;

//    OracleDataReader rdr;
//    rdr = objCmd.ExecuteReader();
//    try
//    {
//        // Read Value to the object
//        UtilityClass utility = new UtilityClass();
//        while (rdr.Read())
//        {
//            chan = new Channel
//            {
//                CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),

//                Branch = Convert.ToString(rdr["BRANCH"]),
//                Atm = Convert.ToString(rdr["ATM"]),
//                InternetBanking = Convert.ToString(rdr["INTERNET_BANKING"]),
//                MobileBanking = Convert.ToString(rdr["MOBILE_BANKING"]),
//                TelephoneBanking = Convert.ToString(rdr["TELEPHONE_BANKING"]),
//                AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
//                AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
//                CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
//                Createdby = Convert.ToString(rdr["CREATED_BY"]),
//                LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
//                LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
//            };
//        }

//        return chan;
//    }
//    catch (Exception ex)
//    {
//        chan.ErrorMessage = custId.ToString() + "  " + ex.Message;
//    }
//    finally
//    {
//        rdr.Close();
//        conn.Close();
//        //chan.CustomerName = GetCustomer(chan.CustomerId).CustomerName;
//    }

//    return chan;

//}

//public School GetSchool(Int64 refId, string table)
//{
//    school = new School();
//    OracleCommand objCmd = new OracleCommand();
//    objCmd.Connection = conn; conn.Open();//

//    objCmd.CommandText = "SELECT * from " + table + " where REFERENCE_ID=:p_refid";
//    objCmd.CommandType = CommandType.Text;
//    objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

//    OracleDataReader rdr;
//    rdr = objCmd.ExecuteReader();
//    try
//    {
//        // Read Value to the object
//        UtilityClass utility = new UtilityClass();
//        while (rdr.Read())
//        {
//            school = new School
//            {
//                CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),
//                ReferenceId = Convert.ToInt32(Convert.ToString(rdr["REFERENCE_ID"])),
//                EntryDate = Convert.ToDateTime(rdr["ENTRY_DATE"]),
//                GraduationDate = Convert.ToDateTime(rdr["GRADUATION_DATE"]),
//                SchoolName = Convert.ToString(rdr["SCHOOL_NAME"]),
//                SchoolType = Convert.ToString(rdr["SCHOOL_TYPE"]),
//                Qualification = Convert.ToString(rdr["QUALIFICATION"]),
//                AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
//                AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
//                CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
//                Createdby = Convert.ToString(rdr["CREATED_BY"]),
//                LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
//                LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
//            };
//        }

//        return school;
//    }
//    catch (Exception ex)
//    {
//        school.ErrorMessage = refId.ToString() + "  " + ex.Message;
//    }
//    finally
//    {
//        rdr.Close();
//        conn.Close();
//       // school.CustomerName = GetCustomer(school.CustomerId).CustomerName;
//    }

//    return school;

//}
//public Membership GetMembership(Int64 refId, string table)
//{
//    membershp = new Membership();
//    OracleCommand objCmd = new OracleCommand();
//    objCmd.Connection = conn; conn.Open();//

//    objCmd.CommandText = "SELECT * from " + table + " where REFERENCE_ID=:p_refid";//CUSTOMER_MEMBERSHIP
//    objCmd.CommandType = CommandType.Text;
//    objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

//    OracleDataReader rdr;
//    rdr = objCmd.ExecuteReader();
//    try
//    {
//        // Read Value to the object
//        UtilityClass utility = new UtilityClass();
//        while (rdr.Read())
//        {
//            membershp = new Membership
//            {
//                CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),
//                ReferenceId = Convert.ToInt32(Convert.ToString(rdr["REFERENCE_ID"])),
//                MembershipType = Convert.ToString(rdr["MEMBERSHIP_TYPE"]),
//                Name = Convert.ToString(rdr["NAME"]),
//                AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
//                AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
//                CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
//                Createdby = Convert.ToString(rdr["CREATED_BY"]),
//                LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
//                LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
//            };
//        } 
//    }
//    catch (Exception ex)
//    {
//        membershp.ErrorMessage = refId.ToString() + "  " + ex.Message;
//    }
//    finally
//    {
//        rdr.Close();
//        conn.Close();
//        //membershp.CustomerName = GetCustomer(membershp.CustomerId).CustomerName;
//    }

//    return membershp;

//}

//public Preference GetPreference(string custId, string table)
//{
//    prefer = new Preference();
//    OracleCommand objCmd = new OracleCommand();
//    objCmd.Connection = conn; conn.Open();//

//    objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_custid";//CUSTOMER_MEMBERSHIP
//    objCmd.CommandType = CommandType.Text;
//    objCmd.Parameters.Add("p_custid", OracleDbType.NVarchar2).Value = custId;

//    OracleDataReader rdr;
//    rdr = objCmd.ExecuteReader();
//    try
//    {
//        // Read Value to the object
//        UtilityClass utility = new UtilityClass();
//        while (rdr.Read())
//        {
//            prefer = new Preference
//            {
//                CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),
//                VacationPreferredPeriod = Convert.ToDateTime(rdr["VACATION_PREFERRED_PERIOD"]),
//                VacationPreferredLocation = Convert.ToString(rdr["VACATION_PREFERRED_LOCATION"]),
//                Game = Convert.ToString(rdr["GAME"]),
//                Club = Convert.ToString(rdr["CLUB"]),
//                Music = Convert.ToString(rdr["MUSIC"]),
//                Restaurant = Convert.ToString(rdr["RESTAURANT"]),
//                Hobby = Convert.ToString(rdr["HOBBY"]),
//                AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
//                AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
//                CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
//                Createdby = Convert.ToString(rdr["CREATED_BY"]),
//                LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
//                LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
//            };
//        }
//    }
//    catch (Exception ex)
//    {
//        prefer.ErrorMessage = custId.ToString() + "  " + ex.Message;
//    }
//    finally
//    {
//        rdr.Close();
//        conn.Close();
//        //prefer.CustomerName = GetCustomer(prefer.CustomerId).CustomerName;
//    }

//    return prefer;

//}
//public RelatedCustomer GetRelatedCustomer(Int64 refId, string table)
//{
//    relative = new RelatedCustomer();
//    OracleCommand objCmd = new OracleCommand();
//    objCmd.Connection = conn; conn.Open();//

//    objCmd.CommandText = "SELECT * from " + table + " where REFERENCE_ID=:p_refid";//RELATED_CUSTOMER
//    objCmd.CommandType = CommandType.Text;
//    objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

//    OracleDataReader rdr;
//    rdr = objCmd.ExecuteReader();
//    try
//    {
//        // Read Value to the object
//        UtilityClass utility = new UtilityClass();
//        while (rdr.Read())
//        {
//            relative = new RelatedCustomer
//            {
//                CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),
//                ReferenceId = Convert.ToInt32(Convert.ToString(rdr["REFERENCE_ID"])),
//                RelatedCustomerKey = Convert.ToString(rdr["RELATED_CUSTOMER_KEY"]),
//                RelationshipType = Convert.ToString(rdr["RELATIONSHIP_TYPE"]),
//                Referrer = Convert.ToString(rdr["REFERRER"]),
//                ReferrerId = Convert.ToString(rdr["REFERRER_ID"]),
//                AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
//                AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
//                CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
//                Createdby = Convert.ToString(rdr["CREATED_BY"]),
//                LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
//                LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
//            };
//        }
//    }
//    catch (Exception ex)
//    {
//        relative.ErrorMessage = refId.ToString() + "  " + ex.Message;
//    }
//    finally
//    {
//        rdr.Close();
//        conn.Close();
//        //relative.CustomerName = GetCustomer(relative.CustomerId).CustomerName;
//    }

//    return relative;

//}

//public RelatedStaff GetRelatedStaff(Int64 refId, string table)
//{
//    relatedStaff = new RelatedStaff();
//    OracleCommand objCmd = new OracleCommand();
//    objCmd.Connection = conn; conn.Open();//

//    objCmd.CommandText = "SELECT * from " + table + " where REFERENCE_ID=:p_refid";//RELATED_CUSTOMER
//    objCmd.CommandType = CommandType.Text;
//    objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

//    OracleDataReader rdr;
//    rdr = objCmd.ExecuteReader();
//    try
//    {
//        // Read Value to the object
//        UtilityClass utility = new UtilityClass();
//        while (rdr.Read())
//        {
//            relatedStaff = new RelatedStaff
//            {
//                CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),
//                ReferenceId = Convert.ToInt32(Convert.ToString(rdr["REFERENCE_ID"])),
//                StaffId = Convert.ToString(rdr["STAFF_ID"]),
//                StaffName = Convert.ToString(rdr["STAFF_NAME"]),
//                StaffRelationshipType = Convert.ToString(rdr["STAFF_RELATIONSHIP_TYPE"]),
//                AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
//                AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
//                CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
//                Createdby = Convert.ToString(rdr["CREATED_BY"]),
//                LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
//                LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
//            };
//        }


//    }
//    catch (Exception ex)
//    {
//        relatedStaff.ErrorMessage = refId.ToString() + "  " + ex.Message;
//    }
//    finally
//    {
//        rdr.Close();
//        conn.Close();
//        //relatedStaff.CustomerName = GetCustomer(relatedStaff.CustomerId).CustomerName;
//    }

//    return relatedStaff;
//}

//public Dependant GetDependant(Int64 refId, string table)
//{
//    dependant = new Dependant();
//    OracleCommand objCmd = new OracleCommand();
//    objCmd.Connection = conn; conn.Open();//

//    objCmd.CommandText = "SELECT * from " + table + " where REFERENCE_ID=:p_refid";
//    objCmd.CommandType = CommandType.Text;
//    objCmd.Parameters.Add("p_refid", OracleDbType.NVarchar2).Value = refId;

//    OracleDataReader rdr;
//    rdr = objCmd.ExecuteReader();
//    try
//    {
//        // Read Value to the object
//        UtilityClass utility = new UtilityClass();
//        while (rdr.Read())
//        {
//             dependant = new Dependant
//            {
//                CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),
//                ReferenceId = Convert.ToInt32(Convert.ToString(rdr["REFERENCE_ID"])),
//                FamilyRelationshipType = Convert.ToString(rdr["FAMILY_RELATIONSHIP_TYPE"]),
//                Name = Convert.ToString(rdr["NAME"]),
//                Occupation = Convert.ToString(rdr["OCCUPATION"]),
//                Address = Convert.ToString(rdr["ADDRESS"]),
//                DateOfBirth = Convert.ToDateTime(rdr["DATE OF BIRTH"]),
//                TelephoneNumber = Convert.ToString(rdr["TELEPHONE_NUMBER"]),
//                EmploymentDetail = Convert.ToString(rdr["EMPLOYMENT_DETAIL"]),
//                AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
//                AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
//                CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
//                Createdby = Convert.ToString(rdr["CREATED_BY"]),
//                LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
//                LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
//            };
//        }


//    }
//    catch (Exception ex)
//    {
//        dependant.ErrorMessage = refId.ToString() + "  " + ex.Message;
//    }
//    finally
//    {
//        rdr.Close();
//        conn.Close();
//        //dependant.CustomerName = GetCustomer(dependant.CustomerId).CustomerName;
//    }

//    return dependant;

//}
//public Card GetCard(string custId, string table)
//{
//    card = new Card();
//    OracleCommand objCmd = new OracleCommand();
//    objCmd.Connection = conn;
//    conn.Open();//

//    objCmd.CommandText = "SELECT * from " + table + " where CUSTOMER_NO=:p_customerid";
//    objCmd.CommandType = CommandType.Text;
//    objCmd.Parameters.Add("p_customerid", OracleDbType.NVarchar2).Value = custId;
//    //


//    OracleDataReader rdr;
//    rdr = objCmd.ExecuteReader();
//    try
//    {
//        // Read Value to the object
//        UtilityClass utility = new UtilityClass();
//        if (rdr.Read()) //&& rdr.RecordsAffected > 0
//        {
//            card = new Card
//            {
//                CustomerId = Convert.ToString(rdr["CUSTOMER_NO"]),

//                CardType = Convert.ToString(rdr["CARD_TYPE"]),
//                CardNumber = Convert.ToString(rdr["CARD_NUMBER"]),
//                CardExpiryDate = Convert.ToDateTime(rdr["CARD_EXPIRY_DATE"]),
//                CardFinancialInstitution = Convert.ToString(rdr["CARD_FINANCIAL_INSTITUTION"]),
//                AuthorisedBy = Convert.ToString(rdr["AUTHORISED_BY"]),
//                AuthorisedDate = Convert.ToString(rdr["AUTHORISED_DATE"]),
//                CreatedDate = Convert.ToString(rdr["CREATED_DATE"]),
//                Createdby = Convert.ToString(rdr["CREATED_BY"]),
//                LastModifiedDate = Convert.ToString(rdr["LAST_MODIFIED_DATE"]),
//                LastModifiedBy = Convert.ToString(rdr["LAST_MODIFIED_BY"])
//            };
//        }
//        else
//        {
//            card.ErrorMessage = "Not found";
//        }
//    }
//    catch (Exception ex)
//    {
//        card.ErrorMessage = custId.ToString() + "  " + ex.Message;
//    }
//    finally
//    {
//        rdr.Close();
//        conn.Close();
//        //card.CustomerName = GetCustomer(card.CustomerId).CustomerName;
//    }

//    return card;

//}















