using Newtonsoft.Json.Linq;
using Finitive.API;
using System;
using System.Net.Http;

namespace OneAtmosphere.Utilities.Generic
{
    public class SalesforceObjects
    {
        private string OAuthClientID;
        private string OAuthClientSecret;
        private string OAuthAgentUserName;
        private string OAuthAgentSecretPassword;
        private string AutomationCompanyAccountId;
        private string AutomationCompanyTenantId;
        private string AutoCompanyAccountId;
        private string AutoCompanyTenantId;
        public SalesforceObjects(string _OAuthClientID, string _OAuthClientSecret, string _OAuthAgentUserName, string _OAuthAgentSecretPassword
        , string _AutomationCompanyAccountId, string _AutomationCompanyTenantId, string _AutoCompanyAccountId, string _AutoCompanyTenantId)
        {
            OAuthClientID = _OAuthClientID;
            OAuthClientSecret = _OAuthClientSecret;
            OAuthAgentUserName = _OAuthAgentUserName;
            OAuthAgentSecretPassword = _OAuthAgentSecretPassword;
            AutomationCompanyAccountId = _AutomationCompanyAccountId;
            AutomationCompanyTenantId = _AutomationCompanyTenantId;
            AutoCompanyAccountId = _AutoCompanyAccountId;
            AutoCompanyTenantId = _AutoCompanyTenantId;

        }

        public string PQI_API_PeriodSchedule(string PeriodScheduleName, string AccountName, string TenantName, string Frequency, string Country)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            if (AccountName == "Auto Company")
                AccountId = AutoCompanyAccountId;
            if (TenantName == "Auto Company Tenant")
                TenantId = AutoCompanyTenantId;
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            Random random = new Random();
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Period_Schedule_WD__c" }, {"referenceId", "112"} } },
                {"Name", PeriodScheduleName},
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"External_ID__c", random.Next(1,1000000).ToString()},
                {"Frequency__c", Frequency},
                {"Country_of_Service__c", Country}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Period_Schedule_WD__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }
            return pqi_Name;
        }
        public string PQI_API_PayPeriod(string PSId, string PeriodScheduleName, string AccountName, string TenantName, string Frequency, string ProcessingDate, string PeriodBeginDate, string PeriodEndDate, string PeriodPaymentDate)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            if (AccountName == "Auto Company")
                AccountId = AutoCompanyAccountId;
            if (TenantName == "Auto Company Tenant")
                TenantId = AutoCompanyTenantId;
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            Random random = new Random();
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Period_Schedule__c" }, {"referenceId", "112"} } },
                {"Name", PeriodScheduleName},
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"Currency_Code__c", "USD"},
                {"Frequency__c", Frequency},
                {"Period_Schedule_WD__c", PSId},
                {"Processing_Date__c", ProcessingDate+"T02:05:00"},
                {"Period_Begin_Date__c", PeriodBeginDate+"T02:05:00"},
                {"Period_End_Date__c", PeriodEndDate+"T02:05:00"},
                {"Period_Payment_Date__c", PeriodPaymentDate+"T02:05:00"}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Period_Schedule__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }
            return pqi_Name;
        }
        public string PQI_API_PayGroupDetail(string PeriodScheduleId, string AccountName, string TenantName, string DisabledFlag, string DashboardApplicableFlag)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            if (AccountName == "Auto Company")
                AccountId = AutoCompanyAccountId;
            if (TenantName == "Auto Company Tenant")
                TenantId = AutoCompanyTenantId;
            if (DashboardApplicableFlag == "True")
                DashboardApplicableFlag = "true";
            if (DisabledFlag == "False")
                DisabledFlag = "false";
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Pay_Group_Detail__c" }, {"referenceId", "112"} } },
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"Pay_Group_Detail_Descriptor__c", "AutoTestDescriptor"},
                {"Period_Schedule__c", PeriodScheduleId},
                {"Disabled__c", DisabledFlag},
                {"Dashboard_Applicable__c", DashboardApplicableFlag}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Pay_Group_Detail__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }
        public string PQI_API_PayPeriodWithPayGroupDetail(string PeriodScheduleId, string PayPeriodId, string PayGroupDetailId, string AccountName, string TenantName)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            if (AccountName == "Auto Company")
                AccountId = AutoCompanyAccountId;
            if (TenantName == "Auto Company Tenant")
                TenantId = AutoCompanyTenantId;
            Random random = new Random();
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Pay_Period_With_Paygroup_Detail__c" }, {"referenceId", "112"} } },
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"Period_Schedule__c", PeriodScheduleId},
                {"Pay_Period__c", PayPeriodId},
                {"Pay_Group_Detail__c", PayGroupDetailId},
                {"Pay_Period_PGD_External_Id__c", random.Next(1,999999).ToString()}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Pay_Period_With_Paygroup_Detail__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }
        public string PQI_API_PayrollGroupDetail(string PeriodScheduleId, string PayPeriodId, string PPWPGDId, string PayrollGroupName, string AccountName, string TenantName, string CurrencyCode, string PaymentDate,
             string TotalNetAmount, string TotalNetAmountSettled, string TotalTaxAmount, string TotalTaxAmountSettled, string TotalGarnishmentAmount, string TotalGarnishmentAmountSettled, string TotalDDPAmount)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            if (AccountName == "Auto Company")
                AccountId = AutoCompanyAccountId;
            if (TenantName == "Auto Company Tenant")
                TenantId = AutoCompanyTenantId;
            if (TotalNetAmount == "0")
                TotalNetAmount = null;
            if (TotalNetAmountSettled == "0")
                TotalNetAmountSettled = null;
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Payroll_Group_Detail__c" }, {"referenceId", "112"} } },
                {"Name", PayrollGroupName},
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"Payroll_Schedule__c", PayPeriodId},
                {"Company__c", "Test Payroll"},
                {"Payment_Date__c", PaymentDate+"T02:05:00"},
                {"Pay_Cycle_Type__c", "On Cycle"},
                {"Pay_Period_With_Paygroup_Detail__c", PPWPGDId},
                {"Pay_Period__c", PayPeriodId},
                {"Period_ScheduleWD__c", PeriodScheduleId},
                {"Currency_Code__c", CurrencyCode},
                {"Total_Net_Amount__c", TotalNetAmount},
                {"Total_Net_Amount_Settled__c", TotalNetAmountSettled},
                {"Total_Tax_Amount__c", TotalTaxAmount},
                {"Total_Tax_Amount_Settled__c", TotalTaxAmountSettled},
                {"Total_Garnishment_Amount__c", TotalGarnishmentAmount},
                {"Total_Garnishment_Amount_Settled__c", TotalGarnishmentAmountSettled},
                {"Total_DDP_Amount__c", TotalDDPAmount},
                {"Total_Check_Amount__c", "500"}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Payroll_Group_Detail__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }
        public string PQI_API_Settlement(string AccountName, string TenantName, string SettlementName, string Currency, string Settlementdate, string TotalNetAmount, string TotalNetAmountSettled, string TotalDDPAmount)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            if (AccountName == "Auto Company")
                AccountId = AutoCompanyAccountId;
            if (TenantName == "Auto Company Tenant")
                TenantId = AutoCompanyTenantId;
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Settlement__c" }, {"referenceId", "112"} } },
                {"Name", SettlementName},
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"Settlement_Date__c", Settlementdate+"T02:05:00"},
                {"Currency_Code__c", Currency},
                {"Total_Net_Amount__c", TotalNetAmount},
                {"Total_Net_Amount_Settled__c", TotalNetAmountSettled},
                {"Total_DDP_Amount__c", TotalDDPAmount},
                {"Total_Check_Amount__c", "500"}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Settlement__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }
        public string PQI_API_PaymentGroup(string AccountName, string TenantName, string PaymentMethod, string Currency, string SettlementId, string FirstPaymentdate)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            if (AccountName == "Auto Company")
                AccountId = AutoCompanyAccountId;
            if (TenantName == "Auto Company Tenant")
                TenantId = AutoCompanyTenantId;
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Payment_Group__c" }, {"referenceId", "112"} } },
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"Settlement__c", SettlementId},
                {"First_Payment_Date__c", FirstPaymentdate+"T02:05:00"},
                {"Payment_Method__c", PaymentMethod},
                {"Currency_Code__c", Currency}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Payment_Group__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }
        public string PQI_API_MoneyTransaction(string AccountName, string TenantName, string Status, string TransactionType, string MoneyMovementType, string TotalDollarAmount, string SettlementDate, string PayPeriodId, string SettlementId, string PayrollGDId, string PaymentGroupId)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Money_Transaction__c" }, {"referenceId", "112"} } },
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"status__c", Status},
                {"ACH_Type__c", TransactionType},
                {"Money_Movement_Type__c", MoneyMovementType},
                {"Total_ACH_Amount__c", TotalDollarAmount},
                {"Settlement_Date_First__c", SettlementDate+"T02:05:00"},
                {"Payroll_Group_Detail__c", PayrollGDId},
                {"Payment_Group__c", PaymentGroupId},
                {"Settlement__c", SettlementId},
                {"Period_Schedule__c", PayPeriodId}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Money_Transaction__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }
        public string PQI_API_BankingSweep(string Amount, string TransactionType)
        {
            string pqi_Name = "";
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Banking_Sweep__c" }, {"referenceId", "112"} } },
                {"Transaction_Type__c", TransactionType},
                {"Amount__c", Amount}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Banking_Sweep__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }
        public string PQI_API_CheckPrintingAndDistribution(string AccountName, string TenantName, string Status, string SettlementId, string PaymentGroupId, string CheckDate)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Check_Printing_and_Distribution__c" }, {"referenceId", "112"} } },
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"status__c", Status},
                {"Payment_Group__c", PaymentGroupId},
                {"Settlement__c", SettlementId},
                {"Check_Date__c", CheckDate+"T02:05:00"}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Check_Printing_and_Distribution__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }
        public string PQI_API_TreasuryException(string AccountName, string TenantName, string ExceptionType, string ExceptionDate, string Amount, string Checknumber, string PayeeName, string ExceptionReasonCode, string Company, string Status, string IsCheckStopPaymentCheckbox)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            if (IsCheckStopPaymentCheckbox == "True")
                IsCheckStopPaymentCheckbox = "true";
            if (IsCheckStopPaymentCheckbox == "False")
                IsCheckStopPaymentCheckbox = "false";
            string BillableCheckbox;
            string PaymentMethod;
            if (ExceptionType == "Void Check")
            {
                BillableCheckbox = "false";
                PaymentMethod = "Check";
            }
            else
            {
                BillableCheckbox = "true";
                PaymentMethod = "Direct Deposit";
            }
            string OriginalSettlementDate = null;
            if (ExceptionType == "Void Check" || ExceptionType == "Check Stop Payment")
            {
                OriginalSettlementDate = ExceptionDate + "T02:05:00";
            }
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Treasury_Exceptions__c" }, {"referenceId", "112"} } },
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"Exception_Type__c", ExceptionType},
                {"status__c", Status},
                {"Amount__c", Amount},
                {"Check_Number__c", Checknumber},
                {"Employee_Name__c", PayeeName},
                {"Exception_Reason__c", ExceptionReasonCode},
                {"Company__c", Company},
                {"Is_Check_Stop_Payment__c", IsCheckStopPaymentCheckbox},
                {"Billable__c", BillableCheckbox},
                {"Payment_Method__c", PaymentMethod},
                {"Exception_Date__c", ExceptionDate+"T02:05:00"},
                {"Original_Settlement_Date__c", OriginalSettlementDate}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Treasury_Exceptions__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }
        public string PQI_API_ReservedCheckNumber(string AccountName, string TenantName, string CheckPrintingAndDistributionId)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            Random random = new Random();
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Reserved_Check_Number__c" }, {"referenceId", "112"} } },
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"Processing_Bank_Account_Name__c", "Bank"+random.Next(1,99999).ToString()},
                {"Check_Printing_and_Distribution__c", CheckPrintingAndDistributionId}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Reserved_Check_Number__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }
            return pqi_Name;
        }
        public string PQI_API_CheckRegister(string Type, string Status, string AccountName, string TenantName, string PayeeName, string PayeeId, string CheckNumber, string CheckDate, string ReturnedCheck, string ExternalId, string Currency, string ShipmentMethod, string SettlementId, string ReservedCheckNumberId)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Check_Register__c" }, {"referenceId", "112"} } },
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"Type__c", Type},
                {"CurrencyIsoCode", Currency},
                {"Status__c", Status},
                {"Payee_Id__c", PayeeId},
                {"Payee_Name__c", PayeeName},
                {"Check_Number__c", CheckNumber},
                {"Check_Date__c", CheckDate+"T02:05:00"},
                {"Return_Check__c", ReturnedCheck},
                {"Check_Register_External_Id__c", ExternalId},
                {"Tracking_ID__c", ExternalId},
                {"Shipment_Method__c", ShipmentMethod},
                {"Settlement__c", SettlementId},
                {"Reserved_Check_Number__c", ReservedCheckNumberId},
                {"Check_Amount__c", "1000"}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Check_Register__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }
            return pqi_Name;
        }
        public string PQI_API_MoneyTransaction2(string AccountName, string TenantName, string Status, string TransactionType, string MoneyMovementType, string TotalDollarAmount, string AdjustedTotalDollarAmount, string BankName, string BankAccountNumber, string SettlementDate, string TaxBatchId)
        {
            string pqi_Name = "";
            string AccountId = "";
            string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            if (TenantName == "Automation Company Tenant")
                TenantId = AutomationCompanyTenantId;
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Money_Transaction__c" }, {"referenceId", "112"} } },
                {"Account__c", AccountId},
                {"Tenant__c", TenantId},
                {"status__c", Status},
                {"ACH_Type__c", TransactionType},
                {"Money_Movement_Type__c", MoneyMovementType},
                {"Total_ACH_Amount__c", TotalDollarAmount},
                {"Adjusted_Total_Dollar_Amount__c", AdjustedTotalDollarAmount},
                {"Settlement_Date_First__c", SettlementDate+"T02:05:00"},
                {"Bank_Name__c", BankName},
                {"Bank_Account_Number__c", BankAccountNumber},
                {"Tax_Batch__c", TaxBatchId}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Money_Transaction__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }
        public string PQI_API_TaxBatch(string AccountName, string Status, string EstimatedTotalCollection, string EstimatedTotalRefunds, string DeferredCollectionAmount, string DeferredRefundAmount, string TotalAmountCollected, string TotalAmountRefunded)
        {
            string pqi_Name = "";
            string AccountId = "";
            //string TenantId = "";
            if (AccountName == "Automation Company")
                AccountId = AutomationCompanyAccountId;
            PQI_Creation_API sc = new PQI_Creation_API(OAuthClientID, OAuthClientSecret, OAuthAgentUserName, OAuthAgentSecretPassword);
            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Tax_Batch__c" }, {"referenceId", "112"} } },
                {"Account__c", AccountId},
                {"status__c", Status},
                {"Estimated_Total_Collections__c", EstimatedTotalCollection},
                {"Estimated_Total_Refunds__c", EstimatedTotalRefunds},
                {"Deferred_Collection_Amount__c", DeferredCollectionAmount},
                {"Deferred_Refund_Amount__c", DeferredRefundAmount},
                {"Total_Amount_Collected__c", TotalAmountCollected},
                {"Total_Amount_Refunded__c", TotalAmountRefunded}
                };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Tax_Batch__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }

            return pqi_Name;
        }



        //==================
        public static string PQI_API(string clientId, string clientSecret, string username, string password)
        {
            string pqi_Name = "";
            PQI_Creation_API sc =
              new PQI_Creation_API(
                   clientId, clientSecret, username, password);

            string submittedDTT = DateTime.Now.ToString("yyyy-MM-dd'T'hh':'mm':'ss");
            string data = "{ \"IsDebug\":true,\"MyFlexPayProcess\":true,\"MyFlexPayRestCallSize\":\"50\"}";

            JObject sObject = new JObject {
                { "attributes", new JObject{ { "type", "Process_Queue_Item__c" }, {"referenceId", "112"} } },
                {"Tenant__c", "a0eg0000007Bcrw"},
                {"Process_Type__c", "vhrProcess"},
                {"Process_Name__c", "Load Payroll Schedules - Code VHR"},
                {"Status__c", "Submitted"},
                {"Data__c", data },
                {"Account__c", "001g000002VECHf"},
                {"SubmittedDTTM__c", submittedDTT},
                {"Process_Scheduler_ID__c", "QA1"}
            };
            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.createSObject("Process_Queue_Item__c", sObject);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var jsonResult = JObject.Parse(result);
                pqi_Name = jsonResult["results"].First["id"].ToString();
                pqi_Name = pqi_Name.Substring(0, (pqi_Name.Length - 3));
            }
            return pqi_Name;
        }

        //Process_Queue_Item__c or Settlement__c
        public static void PQI_API_Delete(string api, string pqi_Name, string ProcessSchedulerID)
        {
            if (ProcessSchedulerID.Equals("QA1"))
            {
                PQI_API_QADelete(api, pqi_Name);

            }
            else
            {
                Console.WriteLine("Details are not matching");
            }
        }
        public static void PQI_API_QADelete(string api, string pqi_Name)
        {
            PQI_Creation_API sc =
                 new PQI_Creation_API(
                      "3MVG9ahGHqp.k2_ydJrP7fAT4UBEbU7vl.9C04mebK8nCBrfbunijblkCZj0W6Wra4KWqJOF_IfgLeat7wtM5",
                  "7EDFF34F1831AD06EB4BCAF46937A3AE21C4695F606769AE0CAF9A0BE8754EB8",
                  "afaheem@onesourcevirtual.com.qa",
                  "Ace7373!!!!!AH3jllNe9kVYwKzaBHaFj2Dd");

            HttpResponseMessage response = new HttpResponseMessage();
            response = sc.deleteSObject(api, pqi_Name);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
            }
        }


    }
}
