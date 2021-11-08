using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Finitive.Pages.PageConstants
{
    class DataValidationPageLocators
    {
        // For QA TaxEx application...
        public static By UserNameForTaxEx = By.Id("input-email");
        public static By PasswordForTaxEx = By.Id("input-password");
        public static By SignInButton = By.XPath("//input[@type='submit']");
        public static By SignupButton = By.XPath("//a[@class='btn-link mainMenu__link mainMenu__link_button']");
        public static By SignupPage = By.Id("signUp");
        public static By FirstNameTextField = By.Id("FirstName");
        public static By LastNameTextField = By.Id("LastName");
        public static By CompanyNameTextField = By.Id("CompanyName");
        public static By PositionTextField = By.Id("Position");
        public static By CompanyWebsiteUrl = By.Id("BusinessUrl");
        public static By CountryCodeDropdown = By.XPath("//div[@aria-controls='iti-0__country-listbox']");
        public static By CountryCodeDropdownList = By.XPath("//ul[@class='iti__country-list iti__country-list--dropup']/li");
        public static By TelephoneNumberTextfield = By.XPath("//input[@id='PhoneNumber']");
        public static By CountryDropdown = By.Id("CountryId");
        public static By CountryDropdownList = By.XPath("//select[@id='CountryId']/option");
        public static By MarketingSourceDropdown = By.Id("MarketingSourceId");
        public static By MarketingSourceDropdownList = By.XPath("//select[@name='MarketingSourceId']/option");
        public static By FinitivePlateformList = By.XPath("//div[@class='finitiveForm__groups signUpForm__groups']/div/label");
        public static By BusinessEmailTextField = By.Id("Email");
        public static By PasswordTextField = By.Id("Password");
        public static By InstitutionalInvestorCheckbox = By.XPath("//div[@class='finitiveForm__groups signUpForm__institutionalInvestorOptions']/div/label");
        public static By DescribeYourCompanyCheckbox = By.XPath("//div[@class='finitiveForm__groups signUpForm__institutionalInvestorCompanyDescriptionOptions']/div/label");
        public static By ButtonSignup = By.XPath("//button[@type='submit']");
        public static By TermsprivacyCheckbox = By.Id("HasAgreedToTerms");
        public static By SignUpPageText = By.Id("//h2[@class='finitiveForm__title signUpForm__title text-center']");
        public static By EmailVerificationPendingText = By.XPath("//h1[@class='finitiveForm__title confirmEmailForm__title']");


        public static By HomePage = By.XPath("//i[@class='fa fa-home']");
        public static By ProcessScreen = By.XPath("(.//*[@id='MainNavMenu']//span[text()='Process'])[1]");
        public static By ImportScreen = By.XPath(".//*[@id='MainNavMenu']/li[3]//a[text()='Import']");
        public static By PROCESSIMPORTPAGE = By.XPath("//a[text()='Process Import']");
        public static By ImportButton = By.Id("ImportButton");
        public static By ImportSuccess = By.XPath(".//*[@id='DialogPlaceholder']/table[1]/tbody/tr/td/b");
        public static By OkButton = By.XPath("html/body/div[6]/div[3]/div/button");
        public static By CompanyTab = By.XPath(".//*[@id='MainNavMenu']/li[2]/span[text()='Company']");
        public static By CompanyTaxTab = By.XPath(".//*[@id='MainNavMenu']/li[2]//span[text()='Tax']");
        public static By TaxesTab = By.XPath("//*[@id='MainNavMenu']/li[2]//li[2]//li[1]/a[text()='Taxes']");
        public static By HistoryTab = By.XPath("//*[@id='HeaderContainer']//span[text()='History']");
        public static By PayFileTab = By.XPath("//*[@id='MainNavMenu']/li[5]/ul/li[2]/span[text()='Pay/File']");
        public static By PayFileDetailsTab = By.XPath("//*[@id='MainNavMenu']/li[5]/ul/li[2]/ul/li[2]/a[text()='Details']");
        public static By PaymentReportFieldTab = By.XPath("//a[contains(@href,'history/payfile/viewpaymentreportfields') and text()='Payment Report Fields']");
        public static By PaymentReportFieldLabel = By.XPath("//a[@title='View Payment Report Fields']");
        public static By PortalMessage = By.XPath("//*[@id='MainNavMenu']/li[5]/ul/li[15]/span[text()='Portal Message']");
        public static By CompanyMessage = By.XPath("//a[@class='t-link'][contains(text(),'Company Message')]");
        public static By MessagesTab = By.XPath("(//a[@class='t-link'][contains(text(),'Message')])[2]");
        public static By CompanyMessagePage = By.XPath("//a[@title='All Company Portal Message']");
        public static By MessagePage = By.XPath("//a[@title='All Portal Messages']");
        public static By HistoryPayFileDetailsPage = By.XPath("//*[@title='View payment / filing']");
        public static By PowerOfAttorneyTab = By.XPath(".//*[@id='MainNavMenu']//span[text()='Power of Attorney']");
        public static By PowerOfAttorneyPendingTab = By.XPath(".//*[@id='MainNavMenu']//li[4]//a[text()='Pending']");
        public static By CompaniesTab = By.XPath(".//*[@id='MainNavMenu']/li[2]//a[text()='Companies']");
        public static By PendingCompanyPowerOfAttorneyPage = By.XPath(".//*[@id='BreadCrumbContainer']//a[text()='Pending Company Power Of Attorney']");
        public static By CompaniesPage = By.XPath(".//*[@id='BreadCrumbContainer']//a[text()='Companies']");
        public static By BasicSearchIcon = By.Id("Basic-Search-FieldSet");
        public static By BasicSearch = By.XPath(".//*[@id='Basic-Search-FieldSet']/legend[text()='Basic']");
        public static By CompanyNameToFilter = By.XPath(".//*[@id='BasicFilterCriteria_CompanyName']");
        public static By TaxCodeFilter = By.Id("BasicFilterCriteria_TaxCode");
        public static By DisplayNameToFilter = By.Id("BasicFilterCriteria_DisplayName");
        public static By YearStartFilter = By.Id("BasicFilterCriteria_Year_Start");
        public static By YearEndFilter = By.Id("BasicFilterCriteria_Year_End");
        public static By QuartersField = By.Id("BasicFilterCriteria_Quarter");
        public static By CompanyNameFilterCompanyScreen = By.Id("BasicFilterCriteria_DisplayName");
        public static By ApplyButton = By.XPath(".//*[@id='TaxExSearchFilter']/div/div[1]/button");
        public static By FILTERPROCESS = By.XPath(".//*[@id='ui-id-1']");
        public static By ActionName = By.Id("Action-Select");
        public static By PerfromActionButton = By.Id("PerformActionDialogButton");
        public static By SubmitButton = By.XPath("//span[text()='Submit']");
        public static By ACTION_SUCCESS_MSG = By.XPath(".//*[@id='DialogPlaceholder']/table[1]/tbody/tr/td/b");
        public static By OKButton = By.XPath("//span[text()='Ok']");
        public static By EmployessTab = By.XPath(".//*[@id='MainNavMenu']//a[text()='Employees']");
        public static By CompanyEmployeesPage = By.XPath(".//*[@id='BreadCrumbContainer']//a[text()='Company Employees']");
        public static By OKButtonOnCompanyEmployees = By.Id("OkBtn");
        public static By ProcessTab = By.XPath(".//*[@id='MainNavMenu']//li[3]/span[text()='Process']");
        public static By ProcessScheduleTab = By.XPath(".//*[@id='MainNavMenu']/li[3]//*[text()='Schedule'] ");
        public static By PROCESSSCHEDULECOVID = By.XPath("//a[contains(@href,'/app/processing/covid19schedule/') and text()='Covid19 Hold']");
        public static By PROCESSSCHEDULECOVIDLINK = By.XPath("//a[text()='Process Covid19 Hold Schedule Detail']");
        public static By ProcessScheduleDetailTab = By.XPath(".//*[@id='MainNavMenu']/li[3]//a[text()='Detail']");
        public static By ProcessScheduleDetailPage = By.XPath(".//*[@id='BreadCrumbContainer']//a[text()='Process Schedule Detail']");
        public static By CHECKDATEFROM = By.Id("BasicFilterCriteria_CheckDate_Start");
        public static By CHECKDATETO = By.Id("BasicFilterCriteria_CheckDate_End");
        public static By DONE = By.XPath("//button[.='Done']");
        public static By ActionSelect = By.Id("Action-Select");
        public static By OkButtonForSchedule = By.XPath("//span[text()='Ok']");
        public static By COMPANY_TAB = By.XPath(".//*[@id='MainNavMenu']//span[text()='Company']");
        public static By QTDSTATUS = By.XPath(".//*[@id='MainNavMenu']//a[text()='QTD Status']");
        public static By QTDSTATUS_PAGE = By.XPath(".//*[@id='BreadCrumbContainer']//a[text()='Company QTD Status']");
        public static By Done_SetLastProcessDate = By.XPath("//div/button[2]");
        public static By Ok_SetLastProcessDate = By.XPath("//button//span[text()='Ok']");
        public static By TextFiled_Threshold = By.Id("AppNoteDetailVM_Value");
        public static By Threshold_Submit = By.XPath("//span[text()='Submit']");
        public static By ProcessPayFileTab = By.XPath("//li[3]//a[text()='Pay/File']");
        public static By PayFileAllPFTab = By.XPath("//li[3]//a[text()='All Pay/File']");
        public static By ProcessPFAllPFPage = By.XPath(".//*[@id='BreadCrumbContainer']//a[text()='All Pay/File']");
        public static By PayFileID = By.Id("BasicFilterCriteria");
        public static By Voucher_type = By.XPath(".//*[@id='ui-multiselect-BasicFilterCriteria_AvailableReportTypes_SelectedValues-option-9']");
        public static By Quarterly_Summary = By.XPath(".//*[@id='ui-multiselect-BasicFilterCriteria_AvailableReportTypes_SelectedValues-option-6']");
        public static By HistoryPayFile_Voucher = By.XPath(".//*[@id='ui-multiselect-BasicFilterCriteria_AvailableReportTypes_SelectedValues-option-14']");
        public static By HistoryPayFile_Annual = By.XPath(".//*[@id='ui-multiselect-BasicFilterCriteria_AvailableReportTypes_SelectedValues-option-1']");
        public static By HistoryPayFile_Quarterly_Summary = By.XPath(".//*[@id='ui-multiselect-BasicFilterCriteria_AvailableReportTypes_SelectedValues-option-10']");
        public static By Annual = By.XPath(".//*[@id='ui-multiselect-BasicFilterCriteria_AvailableReportTypes_SelectedValues-option-1']");
        public static By Type = By.XPath(".//*[@id='BasicFilterCriteria_AvailableReportTypes_SelectedValues']/..//button");
        public static By PeriodStartDate = By.Id("BasicFilterCriteria_PeriodEndDate_Start");
        public static By periodEndDate = By.Id("BasicFilterCriteria_PeriodEndDate_End");
        public static By ProcessBalanceTab = By.XPath("//li[3]//span[text()='Balance']");
        public static By BalanceQTDDetailTab = By.XPath("//li[3]//a[text()='QTD Detail']");
        public static By BalanceYTDDetailTab = By.XPath("//li[3]//a[text()='YTD Detail']");
        public static By QTDDetailScreenTaxCategoryDropDown = By.XPath("//*[contains(text() , 'Tax Category')]/../../..//button");
        public static By TaxCategoryState = By.XPath("//*[@id='ui-multiselect-BasicFilterCriteria_AvailableTaxCategories_SelectedValues-option-6']");
        public static By TaxCategoryLocal = By.XPath("//*[@id='ui-multiselect-BasicFilterCriteria_AvailableTaxCategories_SelectedValues-option-5']");
        public static By TaxCategoryFederal = By.XPath("//*[@id='ui-multiselect-BasicFilterCriteria_AvailableTaxCategories_SelectedValues-option-4']");
        public static By RequireYTDBalancing = By.Id("BasicFilterCriteria_NoUITaxCodes");
        public static By QTDDetailsScreenQuartersTextField = By.Id("BasicFilterCriteria_Quarter");
        public static By YTDDetailScreenTaxCategoryDropDown = By.XPath("//*[@id='Basic-Search-FieldSet']/div/div[21]/div[2]/div/button");
        public static By YTDTaxCategoryState = By.XPath("//*[@id='ui-multiselect-BasicFilterCriteria_AvailableTaxCategories_SelectedValues-option-6']");
        public static By YTDDetailsScreenYearsTextField = By.Id("BasicFilterCriteria_Year");
        public static By OkButtonForRefresh = By.XPath("//span[contains(text(),'Ok')]");
        public static By BalancingReasonDropdown = By.Id("AvailableBalancingReason_SelectedValue");
        public static By SelectMessageTypeDropDown = By.Id("AvailableMessageTypes_SelectedValue");
        public static By Message = By.Id("Message");
        public static By MessageSubmitButton = By.XPath("//div[@class='ui-dialog-buttonset']//span[contains(text(),'Submit')]");
        public static By MessageIDInputField = By.Id("BasicFilterCriteria_Id");
        public static By ToolsTab = By.XPath("//span[text()='Tools' and @class='t-link']");
        public static By PortalTab = By.XPath("//span[text()='Portal']");
        public static By Covid19Tab = By.XPath("//span[text()='Covid-19']");
        public static By DeferralTaxCodeTab = By.XPath("//a[@href='/app/tools/covid19/taxcodedeferral/']");
        public static By DeferralRepaymentTab = By.XPath("//a[@href='/app/tools/covid19/deferralrepayment/']");
        public static By SelectReason = By.XPath("//select[@id='AvailableResponseReason_SelectedValue']");

        public static By FirstEINEditLink = By.XPath("//*[@id='MergedCompanyTax-Grid']/tbody/tr[1]/td/a[text()='Edit' and contains(@href,'taxsetup/identifier')]");
        public static By SecondEINEditLink = By.XPath("//*[@id='MergedCompanyTax-Grid']/tbody/tr[2]/td/a[text()='Edit' and contains(@href,'taxsetup/identifier')]");

        public static By EditEINDetails = By.ClassName("taxex-command-link");
        public static By StatusDropdown = By.Id("AvailableCompanyIdentifierStatuses_SelectedValue");
        public static By ValueInputField = By.Id("Value");
        public static By SaveButton = By.XPath("//*[@class='ui-button-text' and text()='Save']");

        public static By CREDITTAB = By.XPath(".//*[@id='MainNavMenu']//a[.='Credit']");
        public static By CREDIT_PAGE = By.CssSelector("#BreadCrumbContainer a[href*='credit']");
        public static By PaymentDetailsLink = By.XPath("//a[contains(@href,'/app/processing/payfile/detail')]");
        public static By PayFileDetailsLabel = By.XPath("//a[text()='Process Pay/File Detail']");

        public static By ReportFieldLink = By.XPath("//a[contains(@href,'payfile/paymentreportfield/editfieldvalues')]"); 
        public static By HistoryPaymentReportLabel = By.XPath("//a[text()='History Payment Report Field']");
        public static By ReportFieldsDynamicLocator = By.XPath("//input[@id='Fields_{0}__Value']");
        public static By NotificationSavedMessage = By.XPath("//span[@id='NotificationBar-Message' and text()='The data was saved successfully.']");

        public static By GlobalSettingsTab = By.XPath("//span[text()='Global Settings']");
        public static By CategoryOption = By.XPath("//span[text()='Category']");
        public static By CompanyOption = By.XPath("//a[contains(@href,'app/globalsettings/category/companies') and text()='Company']");
        public static By CompanyListLabel = By.XPath("//a[text()='Category - Company - List']");
        public static By CompanyAccountField = By.Id("BasicFilterCriteria_Name");
        public static By EnableRDisableFeatureDrpDwn = By.Id("AvailableClientFeatures_SelectedValue");

        // public static By SelectMessageTypeDropdown = By.Id("AvailableMessageTypes_SelectedValue");

        // For QA Admin site...
        public static By Company_QAAdmin = By.XPath(".//*[@id='form0']//a[text()='Company']");
        public static By CompanyAssigned = By.XPath(".//*[@id='CompanyAssigned']");
        public static By TenantName = By.Id("CompanyName");
        public static By SearchButton = By.XPath("//button[contains(text(),'Search')]");
        public static By SaveButtonOnAdmin = By.XPath("//button[text()='Save']");
        public static By OkButtonInAdminSite = By.XPath("//button[text()='OK']");

        // For Atmosphere site..
        public static By UserNameForAtmos = By.XPath("//input[@placeholder='Username']");
        public static By PasswordForAtmos = By.XPath("//input[@placeholder='Password']");
        public static By SignInButtonForAtmos = By.XPath("//*[contains(text() , 'Sign In')]");
        public static By PowerOfAttorneyLink = By.XPath("//a[contains(text(), 'Power of Attorney')]");
        public static By Tax_Ellipses_Icon = By.XPath("//div[@data-aura-class='cAtmos_Tax_HomeWorklet']//span[text()='View Details']");
        public static By PowerOfAttorneyPage = By.XPath("//p[text()='Missing Power of Attorney']");
        public static By GoBackPage = By.XPath("//a[@class='backPage']");
        public static By InalidSSNLink = By.XPath("//a[contains(text(),'Invalid SSN')]");
        public static By InvalisSSNPage = By.XPath("//p[contains(text(), InvalidSSN)]");
        public static By MissingTaxIdAccountLink = By.XPath("//a[contains(text(),'Missing Tax ID/Account')]");
        public static By CompanySetupLink = By.XPath("//*[@class='slds-dropdown__item']//span[text()='Company Setup']");
        public static By TaxSetupLink = By.XPath("//*[@class='slds-dropdown__item']//span[text()='Tax Setup']");
        public static By Download = By.ClassName("downloadLink");
        public static By TaxCodeDeferralsLink = By.XPath("//a[contains(@href,'covid19DeferredTaxCodes')]");
        public static By Covid19TaxCodeDeferralsLabel = By.XPath("//p[text()='COVID-19 Tax Code Deferrals']");
        public static By DeferNewTaxCodeBtn = By.XPath("//button[contains(text(),'DEFER NEW TAX CODE') and @type='button']");
        public static By DeferNewTaxCodeLabel = By.XPath("//div[contains(text(),'DEFER NEW TAX CODE')]");
        public static By TaxCodeDropdown = By.Name("Tax Codes");
        public static By Company = By.XPath("//span[contains(text(),'{0}')]");
        public static By MoveToSelectedIcon = By.XPath("//span[text()='Move selection to Selected']");
        public static By DeferralCheckStartDate = By.XPath("//div[contains(text(),'Deferral Check Start Date')]/parent::div//input[@type='text']");
        public static By DeferralCheckStartDateLabel = By.XPath("//div[contains(text(),'Deferral Check Start Date')]");
        public static By RequestDeferralButton = By.XPath("//button[text()='Request Deferral']");
        public static By ConfirmBtn = By.XPath("//button[text()='CONFIRM']");
        public static By EditTaxCodeDeferralLabel = By.XPath("//div[contains(text(),'EDIT TAX CODE DEFERRAL')]");
        public static By DeferralCheckLastDate = By.XPath("//div[contains(text(),'Deferral Check Last Date')]/parent::div//input[@type='text']");
        public static By DeferralCheckLastDateLabel = By.XPath("//div[contains(text(),'Deferral Check Last Date')]");
        public static By ProceedButton = By.XPath("//button[text()='Proceed']");
        public static By SearchBox = By.XPath("//input[@class='slds-input slds-combobox__input']/../../../../../../../..//input[@role='combobox']");
        public static By DetailsTab_CaseDetails = By.XPath("//lightning-formatted-text[text()='{0}']/../../../../../../../../../../../../../../../../../../../..//a[text()='Details']");
        public static By CaseRecord = By.XPath(".//span[text()='Case Record Type']/../..//div[@class='recordTypeName slds-grow slds-truncate']/span[text()='TAX: Default']");
        public static By RequestDetail = By.XPath(".//span[text()='Request Detail']/../..//lightning-formatted-text[text()='Tax Deferral Request']");
        public static By NatureOfRequest = By.XPath(".//span[text()='Nature of Request']/../..//lightning-formatted-text[text()='COVID-19 CARES']");
        public static By Subject = By.XPath(".//span[text()='Subject Override']/../..//lightning-formatted-text[contains(text(),'Tax | Action: COVID-19 CARES - Tax Deferral Request')]");
        public static By Description = By.XPath(".//span[text()='Description']/../..//lightning-formatted-text[contains(text(),'Request to start a new tax code deferral for Tax Code')]");
        public static By PublicPost = By.XPath(".//span[text()='Text For Initial Public Post']/../..//span[contains(text(),'Dear Valued Customer,')]");
        public static By DeferralTaxCodeDownloadIcon = By.XPath("//button[@class='slds-button iconExcel cAtmos_Worklet_DownloadExcel']");

        // Need to change the data-year every year
        public static By CurrentReconciliationQ1 = By.XPath("//a[@data-type='Recon' and @data-year='2021'and @data-quarter='1' ]");
        public static By CurrentReconciliationQ2 = By.XPath("//a[@data-type='Recon' and @data-year='2021' and @data-quarter='2' ]");
        public static By CurrentReconciliationQ3 = By.XPath("//a[@data-type='Recon' and @data-year='2021' and @data-quarter='3' ]");
        public static By CurrentReconciliationQ4 = By.XPath("//a[@data-type='Recon' and @data-year='2021' and @data-quarter='4' ]");
        public static By CurrentCompanyPacketQ1 = By.XPath("//a[@data-type='CompanyPacket' and @data-year='2021' and @data-quarter='1' ]");
        public static By CurrentCompanyPacketQ2 = By.XPath("//a[@data-type='CompanyPacket' and @data-year='2021' and @data-quarter='2' ]");
        public static By CurrentCompanyPacketQ3 = By.XPath("//a[@data-type='CompanyPacket' and @data-year='2021' and @data-quarter='3' ]");
        public static By CurrentCompanyPacketQ4 = By.XPath("//a[@data-type='CompanyPacket' and @data-year='2021' and @data-quarter='4' ]");
        public static By CurrentAnnualReconciliation = By.XPath("//a[@data-type='AnnualRecon' and @data-year='2021']");


        // Need to change the data-year every year
        public static By PreviousReconciliationQ1 = By.XPath("//a[@data-type='Recon' and @data-year='2020'and @data-quarter='1' ]");
        public static By PreviousReconciliationQ2 = By.XPath("//a[@data-type='Recon' and @data-year='2020' and @data-quarter='2' ]");
        public static By PreviousReconciliationQ3 = By.XPath("//a[@data-type='Recon' and @data-year='2020' and @data-quarter='3' ]");
        public static By PreviousReconciliationQ4 = By.XPath("//a[@data-type='Recon' and @data-year='2020' and @data-quarter='4' ]");
        public static By PreviousCompanyPacketQ1 = By.XPath("//a[@data-type='CompanyPacket' and @data-year='2020' and @data-quarter='1' ]");
        public static By PreviousCompanyPacketQ2 = By.XPath("//a[@data-type='CompanyPacket' and @data-year='2020' and @data-quarter='2' ]");
        public static By PreviousCompanyPacketQ3 = By.XPath("//a[@data-type='CompanyPacket' and @data-year='2020' and @data-quarter='3' ]");
        public static By PreviousCompanyPacketQ4 = By.XPath("//a[@data-type='CompanyPacket' and @data-year='2020' and @data-quarter='4' ]");
        public static By PreviouAnnualReconciliation = By.XPath("//a[@data-type='AnnualRecon' and @data-year='2020']");

        //Select Company
        public static By SelectCompaniesLable = By.XPath("//h2[contains(text(),'Select Companies')]");
        public static By SelectAllCheckBox = By.XPath("//span[contains(text(),'Select All')]");
        public static By ShowSelectedOnlyCheckBox = By.XPath("//span[contains(text(),'Show Selected Only')]");
        public static By SaveButtonSelectCompanyPage = By.XPath("//button[@class='slds-button slds-button--neutral save uiButton']");
        public static By CloseSelectCompanyPage = By.XPath("(//i[@class='fa fa-times fa-lg'])[1]");
        public static By LetterSelect = By.ClassName("letterSelect");


    }
}
