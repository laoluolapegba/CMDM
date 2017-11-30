using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// using BlueChip.NAPIMS.Web.Views.MasterData;

namespace CMdm.Business
{

    public class Customer : Address
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretQuestionAnswer { get; set; }
        public string CustomerType { get; set; }
        public string EmailAddress1 { get; set; }
        public string EmailAddress2 { get; set; }
        public string WebAddress { get; set; }
        public string PoliticallyExposedPerson { get; set; }
        public string FinanciallyExposedPerson { get; set; }
        public string AnniversaryType1 { get; set; }
        public string AnniversaryDate1 { get; set; }
        public string AnniversaryType2 { get; set; }
        public string AnniversaryDate2 { get; set; }
        public string AnniversaryType3 { get; set; }
        public string AnniversaryDate3 { get; set; }
        public string AnniversaryType4 { get; set; }
        public string AnniversaryDate4 { get; set; }
        public string AnniversaryType5 { get; set; }
        public string AnniversaryDate5 { get; set; }
        public string AnniversaryType6 { get; set; }
        public string AnniversaryDate6 { get; set; }
        public string AnniversaryType7 { get; set; }
        public string AnniversaryDate7 { get; set; }
    }

    public class CompanyDetails : Customer
    {
        //public Int64 ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string CertOfIncorpRegNo { get; set; }
        public string JurisdictionOfIncorpReg { get; set; }
        public string ScumlNo { get; set; }
        public string GenderControlling51Perc { get; set; }
        public string SectorOrIndustry { get; set; }
        public string OperatingBusiness1 { get; set; }
        public string OperatingBusiness2 { get; set; }
        public string City1 { get; set; }
        public string City2 { get; set; }
        public string Country1 { get; set; }
        public string Country2 { get; set; }
        public string ZipCode1 { get; set; }
        public string ZipCode2 { get; set; }
        public string BizAddressRegOffice1 { get; set; }
        public string BizAddressRegOffice2 { get; set; }
        public string CompanyEmailAddress { get; set; }
        public string Website { get; set; }
        public string OfficeNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Tin { get; set; }
        public string CrmbNoBorrowerCode { get; set; }
        public string ExpectedAnnualTurnover { get; set; }

        public string IsCompanyOnStockExch { get; set; }
        public string StockExchangeName { get; set; }
    }

    public class CustomerCorpBulkUpload
    {

        //Individual data class
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string TaxIdentificationNumber { get; set; }
        public string RcNumber { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string IncorporationState { get; set; }
        public string IncorporationCountry { get; set; }
        public string IncorporationDate { get; set; }
        public string CbnIsicCategorization { get; set; }
        public string DirectorName { get; set; }
        public string CompanyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public string IssuingAuth { get; set; }
        public string IssuingCountry { get; set; }
        public string IssuingState { get; set; }
        public string IssuingCity { get; set; }
        public string IssuingDate { get; set; }
        public string ExpiryDate { get; set; }
        public string PoliticallyExposedPerson { get; set; }
        public string FinanciallyExposedPerson { get; set; }
        public string PreferredMeanOfCommunication { get; set; }
        public string CompanyName { get; set; }
        public string Signatory { get; set; }
        public string EmailAddress1 { get; set; }
        public string EmailAddress2 { get; set; }
        public string AddressType { get; set; }
        public string HouseIdentifier { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AdministrativeArea { get; set; }
        public string Locality { get; set; }
        public string LocationCoordinates { get; set; }
        public string PostCode { get; set; }
        public string PostOfficeBox { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PhoneCategory { get; set; }
        public string AreaCode { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtensionNo { get; set; }
        public string PhoneType { get; set; }
        public string ChannelSupported { get; set; }
        public string ReachableHour { get; set; }
        public string InmcomeBand { get; set; }
        public string InmcomeSegment { get; set; }
    }

    public class CustomerIndividualBulkUpload
    {

        //Individual data class
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MaidenName { get; set; }
        public string SocialSecurityNo { get; set; }
        public string TaxNo { get; set; }
        public string Title { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Religion { get; set; }
        public string StateOfOrigin { get; set; }
        public string Nationality { get; set; }
        public string Heigth { get; set; }
        public string Complextion { get; set; }
        public string EyeColor { get; set; }
        public string Disability { get; set; }
        public string Race { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public string IssuingAuth { get; set; }
        public string IssuingCountry { get; set; }
        public string IssuingState { get; set; }
        public string IssuingDate { get; set; }
        public string ExpiryDate { get; set; }
        public string IssuingCity { get; set; }
        public string IncomeSource { get; set; }
        public string IncomeSegment { get; set; }
        public string IncomeBand { get; set; }
        public string AccountNumber { get; set; }
        public string Branch { get; set; }
        public string AccountType { get; set; }
        public string Currency { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string NOKFamilyRelationshipType { get; set; }
        public string NOKFullName { get; set; }
        public string NOKAddress { get; set; }
        public string NOKTelephoneNumber { get; set; }
        public string NOKDateOfBirth { get; set; }
        public string NOKEmploymentDetail { get; set; }
        public string NOKOccupation { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
        public string VisaValidFrom { get; set; }
        public string VisaValidTill { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssueDate { get; set; }
        public string PassportExpiryDate { get; set; }
        public string PassportIssueCountry { get; set; }
        public string ResidentPermitNumber { get; set; }
        public string CaseNumber { get; set; }
        public string CaseDescription { get; set; }
        public string CaseDate { get; set; }
        public string PoliceSummation { get; set; }
        public string FamilyRelationship { get; set; }
        public string FamilyFullname { get; set; }
        public string FamilyDateOfBirth { get; set; }
        public string FamilyAddress { get; set; }
        public string FamilyPhoneNumber { get; set; }
        public string FamilyEmploymentDetail { get; set; }
        public string FamilyOccupation { get; set; }
        public string EmploymentType { get; set; }
        public string CurrentEmployerName { get; set; } //NUMBER
        public string CurrentEmployerAddress { get; set; }
        public string CurrentEmployerPhone { get; set; }
        public string CurrentEmployerPositionHeld { get; set; }
        public string EmploymentDate { get; set; }
        public string PreviousEmployerAddress { get; set; }
        public string PreviousEmployerName { get; set; }
        public string PrevEmployerPositionHeld { get; set; }
        public string TimeSpentWithPrevEmployer { get; set; }
        public string BusinessOccupation { get; set; }

    }



    public class Biodata : Audit
    {
        public string CustomerId { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string ShortName { get; set; }
        public string PreferredName { get; set; }
        public string MotherMaidenName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string TaxIdentificationNumber { get; set; }
        public string Title { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Religion { get; set; }
        public string StateOfOrigin { get; set; }
        public string Nationality { get; set; }
        public string Height { get; set; }
        public string Complexion { get; set; }
        public string EyeColor { get; set; }
        public string Disability { get; set; }
        public string FacialMarks { get; set; }
        public string Race { get; set; }
        public string PoliticallyExposedPerson { get; set; }
        public string FinanciallyExposedPerson { get; set; }
        public string DocumentType { get; set; }
        public string IdentityNumber { get; set; }
        public string IssuingAuthority { get; set; }
        public string IssuingCountry { get; set; }
        public string IssuingState { get; set; }
        public string IssuingDate { get; set; }

        public string ExpiryDate { get; set; }

        public string IssueCity { get; set; }
    }

    public class CustomerIncome : Audit
    {
        //public int ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string InitialDeposit { get; set; }
        public string IncomeBand { get; set; }
    }
    public class AdditionalInformation : Audit
    {
        //public int ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string AnnualSalaryExpectedInc { get; set; }
        public string FaxNumber { get; set; }


    }

    public class AuthForFinInclusion : Audit
    {
        //public int ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string SocialFinancialDisadvtage { get; set; }
        public string SocialFinancialDocuments { get; set; }
        public string EnjoyedTieredKyc { get; set; }
        public string RiskCategory { get; set; }
        public string MandateAuthCombineRule { get; set; }
        public string AccountWithOtherBanks { get; set; }


    }
    public class NextOfkin : Audit
    {
        public string CustomerNo { get; set; }
        public string Title { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Relationship { get; set; }
        public string OfficeNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string HouseNo { get; set; }
        public string IDType { get; set; }
        public string IDIssueDate { get; set; }
        public string IDExpiryDate { get; set; }
        public string ResidentPermitNo { get; set; }
        public string PlaceOfIssuance { get; set; }
        public string StreetName { get; set; }
        public string NearestBStop { get; set; }
        public string CityTown { get; set; }
        public string ZipCode { get; set; }
        public string LGA { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

    }
    public class Foreigner : Audit
    {
        //public int ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string PassportResidencePermit { get; set; }
        public DateTime PermitIssueDate { get; set; }
        public DateTime PermitExpiryDate { get; set; }
        public string ForeignAddress { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string zip_postal_code { get; set; }
        public string foreign_tel_number { get; set; }
        public string purpose_of_account { get; set; }
    }

    public class Jurat : Audit
    {
        //public long ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string NameOfInterpreter { get; set; }
        public DateTime OathDate { get; set; }
        public string AddressOfInterpreter { get; set; }
        public string TelephoneNo { get; set; }
        public string LanguageOfInterpretation { get; set; }

    }

    public class Address : CustomerContact
    {
        public string CustomerId { get; set; }
        public string AddressType { get; set; }
        public string HouseIdentifier { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string Addressline3 { get; set; }
        public string AdministrativeArea { get; set; }
        public string Locality { get; set; }
        public string LocationCoordinates { get; set; }
        public string PostCode { get; set; }
        public string PostOfficeBox { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string CountryOfResidence { get; set; }
    }

    public class CustomerContact : Audit
    {
        public string CustomerId { get; set; }
        public string PhoneCategory { get; set; }
        public string AreaCode { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtensionNo { get; set; }
        public string PhoneType { get; set; }
        public string ChannelSupported { get; set; }
        public string ReachableHours { get; set; }
    }

    public class Audit
    {
        public string CreatedDate { get; set; }
        public string Createdby { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string Authorised { get; set; }
        public string AuthorisedBy { get; set; }
        public string AuthorisedDate { get; set; }
        public string HoldData { get; set; }
        public string ErrorMessage { get; set; }
        public string CustomerName { get; set; }
        //public string IPAddress { get { return new UtilityClass().GetUserIP(); } }// ServerVariables["REMOTE_ADDR"];
    }

    public class Account : Audit
    {
        //public Int64 ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string TypeOfAccount { get; set; }
        public string AccountHolder { get; set; }
        public string AccountNumber { get; set; }
        public string AccountOfficer { get; set; }
        public string AccountTitle { get; set; }
        public string Branch { get; set; }
        public string BranchClass { get; set; }
        public string BusinessDivision { get; set; }
        public string BizSegment { get; set; }
        public string BizSize { get; set; }
        public string BVNNo { get; set; }
        public string CAVRequired { get; set; }
        public string CustomerIc { get; set; }
        //public string CustomerId{ get; set; }
        public string CustomerSegment { get; set; }
        public string CustomerType { get; set; }
        public string OperatingInstruction { get; set; }
        public string OriginatingBranch { get; set; }
        //Account services required.
        public string CardPreference { get; set; }
        public string ElectronicBankingPreference { get; set; }
        public string StatementPreferences { get; set; }
        public string TranxAlertPreference { get; set; }
        public string StatementFrequency { get; set; }
        public string ChequeBookRequisition { get; set; }
        public string ChequeLeavesRequired { get; set; }
        public string ChequeConfirmation { get; set; }
        public string ChequeConfirmationThreshold { get; set; }
        public string ChequeConfirmationThresholdRange { get; set; }
        public string OnlineTransferLimit { get; set; }
        public string OnlineTransferLimitRange { get; set; }
        public string Token { get; set; }
        public string AccountSignatory { get; set; }
        public string SecondSignatory { get; set; }

    }



    public class TrustClientAccount : Audit
    {
        //public Int64 ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string TrustsClientAccounts { get; set; }
        public string NameOfBeneficialOwner { get; set; }
        public string SpouseName { get; set; }
        public DateTime SpouseDateOfBirth { get; set; }
        public string SpouseOccupation { get; set; }
        public string SourcesOfFundToAccount { get; set; }
        public string OtherSourceExpectAnnInc { get; set; }
        public string InsiderRelation { get; set; }
        public string NameOfAssociatedBusiness { get; set; }
        public string FreqInternationalTraveler { get; set; }
        public string PoliticallyExposedPerson { get; set; }
        public string PowerOfAttorney { get; set; }
        public string HolderName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Nationality { get; set; }
        public string TelephoneNumber { get; set; }

    }

    public class Employment : Audit
    {
        //public Int64 ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string EmploymentStatus { get; set; }
        public string EmployerInstName { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string SectorClass { get; set; }
        public string SubSector { get; set; }
        public string NatureOfBuzOcc { get; set; }
        public string IndustrySegment { get; set; }

    }
    public class CompanyInformation : Audit
    {
        //public Int64 ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string CompanyName { get; set; }
        public DateTime DateOfIncorpRegistration { get; set; }
        public string CustomerType { get; set; }
        public string RegisteredAddress { get; set; }
        public string BizCategory { get; set; }

    }






    public class CorpPOAInformation : Audit
    {
        //public Int64 ReferenceId { get; set; }
        public string CustomerNo { get; set; }

        public string AccountNo  { get; set; }
        public string HoldersNAme { get; set; }
        public string Address { get; set; }

        public string Country { get; set; }
        public string Nationality { get; set; }

        public string Telephone { get; set; }


    }


    public class AWOBInformation : Audit
    {

        public string CustomerNo { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Status { get; set; }


    }

    public class DIRIdentityInformation : Audit
    {

        public string CustomerNo { get; set; }
        public string ManagementNo { get; set; }
        public string TypeOfID { get; set; }
        public string IDNo { get; set; }
        public DateTime IDIssueDate { get; set; }
        public DateTime IDExpiryDate { get; set; }
        public string BVNID { get; set; }
        public string TIN { get; set; }


    }


    public class DIRNOKInformation : Audit
    {

        public string CustomerNo { get; set; }
        public string ManagementNo { get; set; }
        public string Title { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Othernames { get; set; }
        public string DOB { get; set; }
        public string Sex { get; set; }
        public string Relationship { get; set; }
        public string OfficeNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string HouseNo { get; set; }
        public string StreetName { get; set; }
        public string NearestBStop { get; set; }
        public string City { get; set; }
        public string LGA { get; set; }
        public string ZipPostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

    }
    public class DIRForeignerInformation : Audit
    {

        public string CustomerNo { get; set; }
        public string ManagementNo { get; set; }
        public string isForeigner { get; set; }
        public string ResidentPermitNo { get; set; }
        public string Nationality { get; set; }
        public DateTime PermitIssueDate { get; set; }
        public DateTime PermitExpiryDate { get; set; }
        public string ForeignAddy { get; set; }
        public string ForeignTelNo { get; set; }
        public string PassportResidentPermitNo { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipPostalCode { get; set; }

    }

    public class DIRBiodataInformation : Audit
    {

        public string CustomerNo { get; set; }
        public string ManagementNo { get; set; }
        public string ManagementType { get; set; }
        public string Title { get; set; }
        public string MaritalStatus { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Othernames { get; set; }
        public string DOB { get; set; }
        public string POB { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }
        public string MothersMaidenName { get; set; }
        public string Occupation { get; set; }
        public string JobTitle { get; set; }
        public string ClassOfSignitory { get; set; }

    }
    public class AddressInformation : Audit
    {

        public string CustomerNo { get; set; }
        public string ManagementNo { get; set; }
        public string HomeNo { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string LGA { get; set; }
        public string State { get; set; }
        public string NearestBstop { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string OfficeNo { get; set; }
        public string MobileNo { get; set; }

    }


    public class CorpAdditionalInformation : Audit
    {
        //public Int64 ReferenceId { get; set; }
        public string CustomerNo { get; set; }
        public string AffliliatedCompBody { get; set; }
        public string ParentCompanyIncCountry { get; set; }

    }
    //sbulifm
    //    public string IncorporationState { get; set; }
    //    public string IncorporationCountry { get; set; }
    //    public string IncorporationDate { get; set; }
    //    public string CbnIsicCategorization { get; set; }
    //}

    public class AccountInformation : Audit
    {
        public string CustomerNo { get; set; }
        public string AccountType { get; set; }
        public string DomicileBranch { get; set; }
        public string ReferralCode { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string CardPreference { get; set; }
        public string ElectronicBankingPrefer { get; set; }
        public string StatementPreferences { get; set; }
        public string TransactionAlertPreference { get; set; }
        public string StatementFrequency { get; set; }
        public string ChequeBookRequisition { get; set; }
        public string ChequeConfirmation { get; set; }
        public string ChequeConfirmThreshold { get; set; }
    }

    public class Credit : Audit
    {
        public string CustomerId { get; set; }
        public Int64 ReferenceId { get; set; }
        public string CreditTypeHeld { get; set; }
        public string TenorOfCredit { get; set; }
        public string AddressCreditCompany { get; set; }
    }
    public class Share : Audit
    {
        public string CustomerId { get; set; }
        public Int64 ReferenceId { get; set; }
        public string ShareholderYn { get; set; }
        public string AccessBankUnitsHeld { get; set; }
        public string SubsidiariesUnitsHeld { get; set; }
    }
    public class Subsidiary : Audit
    {
        public string CustomerId { get; set; }
        public Int64 ReferenceId { get; set; }
        public string CustNameBiz { get; set; }
        public string CustTypeBiz { get; set; }
    }
    public class Channel : Audit
    {
        public string CustomerId { get; set; }
        public string Branch { get; set; }
        public string Atm { get; set; }
        public string InternetBanking { get; set; }
        public string MobileBanking { get; set; }
        public string TelephoneBanking { get; set; }
    }

    public class RelatedStaff : Audit
    {
        public string CustomerId { get; set; }
        public Int64 ReferenceId { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffRelationshipType { get; set; }
    }

    public class RelatedCustomer : Audit
    {
        public string CustomerId { get; set; }
        public Int64 ReferenceId { get; set; }
        public string RelatedCustomerKey { get; set; }
        public string RelationshipType { get; set; }
        public string Referrer { get; set; }
        public string ReferrerId { get; set; }
    }

    public class Preference : Audit
    {
        public string CustomerId { get; set; }
        public DateTime VacationPreferredPeriod { get; set; }
        public string VacationPreferredLocation { get; set; }
        public string Game { get; set; }
        public string Club { get; set; }
        public string Restaurant { get; set; }
        public string Music { get; set; }

        public string Hobby { get; set; }
    }

    public class Membership : Audit
    {
        public string CustomerId { get; set; }
        public Int64 ReferenceId { get; set; }
        public string MembershipType { get; set; }
        public string Name { get; set; }
    }
    public class School : Audit
    {
        public string CustomerId { get; set; }
        public Int64 ReferenceId { get; set; }
        public string SchoolType { get; set; }
        public string SchoolName { get; set; }
        public DateTime GraduationDate { get; set; }
        public DateTime EntryDate { get; set; }

        public string Qualification { get; set; }
    }
    public class Dependant : Audit
    {
        public string CustomerId { get; set; }
        public Int64 ReferenceId { get; set; }
        public string FamilyRelationshipType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmploymentDetail { get; set; }
        public string Occupation { get; set; }
        public DateTime DateOfBirth { get; set; }
    }


    public class Card : Audit
    {
        public string CustomerId { get; set; }

        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string CardFinancialInstitution { get; set; }
        public DateTime CardExpiryDate { get; set; }
    }

    public class Connection
    {

        public string ConnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString; }

        }
    }
}
//}