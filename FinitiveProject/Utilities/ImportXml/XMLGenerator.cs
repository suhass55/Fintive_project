using TaxEx.ImportXML;
using System;
using System.Xml.Serialization;
using System.IO;
using SeleniumAutomation.Utilities;

namespace SeleniumAutomation.XMLGeneration
{    
    public class XMLGenerator1
    {
        public static string CompName;
        //public string CompName;
        AutomationUtilities _autoutilities = new AutomationUtilities();
        public string GenerateXMLWithCurrentQuarter()
        {           
            Random rand = new Random();
            long l = rand.Next();
            //string CompName = "FL UI" + l;
            CompName = "FL UI" + l;
            Console.WriteLine("CompName####: "+ CompName);

            CompanyImport co = new CompanyImport
            {
                Company = new CompanyImportCompany[]
                {
                    new CompanyImportCompany
                    {
                        TaxExStartDate = Convert.ToDateTime("01-01-2014"),
                        TaxExStartDateSpecified = true,
                        AsOfDate = Convert.ToDateTime("01-01-2014"),
                        AsOfDateSpecified = true,
                        Name = CompName,
                        Code = CompName,
                        ImportAndBalanceFlag = CompanyImportAndBalance.Item1,
                        ImportAndBalanceFlagSpecified = true,
                        ImportDailyDetail = 1,
                        ImportQTDDetail = 1,
                        ImportYTDDetail =1,
                        Address = new Address
                        {
                            PhysicalAddress = new EffectiveAddress
                            {
                                AsOfDate = Convert.ToDateTime("01-01-2014"),
                                AsOfDateSpecified = true,
                                Line1 = "PA",
                                Line2 = "aa",
                                City = "BB City",
                                State = "BB",
                                Zip = "02908-0101",
                                Country = "USA",
                                Note = ""
                            },
                            MailingAddress = new EffectiveAddress
                            {
                                AsOfDate = Convert.ToDateTime("01-01-2014"),
                                AsOfDateSpecified = true,
                                Line1 = "PA",
                                Line2 = "aa",
                                City = "BB City",
                                State = "BB",
                                Zip = "02908-0101",
                                Country = "USA",
                                Note = ""
                            }
                        },
                        Status = new EffectiveStatus
                        {
                            AsOfDate = Convert.ToDateTime("01-01-2014"),
                            AsOfDateSpecified = true,
                            Status = EffectiveStatusStatus.Item1,
                            StatusSpecified = true
                        },
                        ServiceType = new ServiceType
                        {
                            AsOfDate = Convert.ToDateTime("01-01-2014"),
                            AsOfDateSpecified = true,
                            PEO = "",
                            PayMaster = "",
                            Status = new CompanyServiceType
                            {
                                Service = CompanyServiceTypeService.Item3,
                                ServiceSpecified = true,
                                CompanyType = CompanyServiceTypeCompanyType.Item1,
                                CompanyTypeSpecified = true,
                                ThirdPartySickPay = true,
                                ThirdPartySickPaySpecified = true
                            }

                        },
                        Reporting = new Reporting
                        {
                            EmployeeWageDetail = true,
                            EmployeeWageDetailSpecified = true,
                            WorksiteReporting = false,
                            WorksiteReportingSpecified = true,
                            EmployeeW2 = true,
                            EmployeeW2Specified = true
                        },
                        Category = new Category
                        {
                            Name = "KPM",
                            Code = "KPM DESCRIPTION"
                        },
                        PayCycle = new PayCycle[]
                        {
                            new PayCycle
                            {
                                Code = "KPM",
                                Description = "KPM DESCRIPTION",
                                Status = new EffectiveStatus
                                {
                                    AsOfDate = Convert.ToDateTime("01-01-2014"),
                                    AsOfDateSpecified = true,
                                    Status = EffectiveStatusStatus.Item1,
                                    StatusSpecified = true

                                }
                            },
                        },
                        TaxSetup = new TaxSetup[]
                        {
                            //Tax Status for FE WH ...
                            new TaxSetup
                            {
                                Code = "FE WH",
                                Status = TaxStatus.Item1,
                                StatusSpecified = true,
                                EIN = "21-1573580",
                                Frequency = "Monthly",
                                Rate = 0,
                                RateSpecified = true,
                                Payment = new TaxSetupPayment
                                {
                                    Method = PaymentMethod.Item1,
                                    MethodSpecified = true,
                                    Status = PaymentStatus.Item2,
                                    StatusSpecified = true
                                }
                            },
                            ////Tax Status for FL UI ...
                            new TaxSetup
                            {
                                Code = "FL UI",
                                Status = TaxStatus.Item1,
                                StatusSpecified = true,
                                EIN = "9834523",
                                Frequency = "Quarterly OA",
                                Rate = 2,
                                RateSpecified = true,
                                Payment = new TaxSetupPayment
                                {
                                    Method = PaymentMethod.Item3,
                                    MethodSpecified = true,
                                    Status = PaymentStatus.Item2,
                                    StatusSpecified = true
                                }
                            }
                        },
                        EmployeeSetup = new EmployeeSetup[]
                        {
                            new EmployeeSetup
                            {
                                SSN = "111111111",
                                EmployeeNumber = "1011",
                                PayCycleCode = "KPM",
                                ReferenceNumber = "1011",
                                EmployeeName = new EmployeeName
                                {
                                    AsOfDate = Convert.ToDateTime("2012-12-31"),
                                    AsOfDateSpecified = true,
                                    First = "First_Name_B",
                                    Last = "Last_Name_A",
                                    Middle = "A"
                                },
                                Address = new EffectiveAddress
                                {
                                    AsOfDate = Convert.ToDateTime("2012-12-31"),
                                    AsOfDateSpecified = true,
                                    Line1 = "L2",
                                    Line2 = "L1",
                                    City = "Ecity",
                                    State = "AA",
                                    Zip = "11111",
                                    Country = "USA"
                                },
                                EmployeeProfile = new EmployeeProfile
                                {
                                    AsOfDate = Convert.ToDateTime("2012-01-07"),
                                    AsOfDateSpecified = true,
                                    Gender = Gender.M,
                                    GenderSpecified = true,
                                    PayType = PayType.Item1,
                                    PayTypeSpecified = true
                                }
                            },
                            new EmployeeSetup
                            {
                                SSN = "222222222",
                                EmployeeNumber = "1012",
                                PayCycleCode = "KPM",
                                ReferenceNumber = "1012",
                                EmployeeName = new EmployeeName
                                {
                                    AsOfDate = Convert.ToDateTime("2012-12-31"),
                                    AsOfDateSpecified = true,
                                    First = "First_Name_B",
                                    Last = "Last_Name_B",
                                    Middle = "B"
                                },
                                Address = new EffectiveAddress
                                {
                                    AsOfDate = Convert.ToDateTime("2012-12-31"),
                                    AsOfDateSpecified = true,
                                    Line1 = "L2",
                                    Line2 = "L1",
                                    City = "Ecity",
                                    State = "BB",
                                    Zip = "22222",
                                    Country = "USA"
                                },
                                EmployeeProfile = new EmployeeProfile
                                {
                                    AsOfDate = Convert.ToDateTime("2012-01-07"),
                                    AsOfDateSpecified = true,
                                    Gender = Gender.F,
                                    GenderSpecified = true,
                                    PayType = PayType.Item1,
                                    PayTypeSpecified = true
                                }
                            }
                        },

                        //Q2 Tax Details ...
                        TaxDetail = new TaxDetail[]
                        {
                            //M1 .... FE WH ....
                            new TaxDetail
                            {
                                CheckDate = Convert.ToDateTime("2019-04-30"),
                                CheckDateSpecified = true,
                                PayCycleCode = "KPM",
                                TaxCode = "FE WH",
                                Resident = false,
                                EmployeeDetail = new TaxDetailEmployeeDetail[]
                                {
                                    new TaxDetailEmployeeDetail     //Employee1 ..
                                    {
                                        SSN = "111111111",
                                        EmployeeNumber = "1011",
                                        Daily = new Daily
                                        {
                                            PeriodStartDate = Convert.ToDateTime("2019-04-01"),
                                            PeriodEndDate = Convert.ToDateTime("2019-06-30"),
                                            Tax = 40,
                                            Taxable = 2000,
                                            Gross = 2000,
                                            Hours = "39",
                                            Weeks = "1"
                                        },
                                        QTD = new QTD
                                        {
                                            Tax = 40,
                                            Taxable = 2000,
                                            Gross = 2000,
                                            Hours = "39",
                                            Weeks = "1",
                                            Month1_12 = true,
                                            Month1_12Specified = true,
                                            Month2_12 = false,
                                            Month2_12Specified = true,
                                            Month3_12 = false,
                                            Month3_12Specified = true
                                        },
                                        YTD = new YTD
                                        {
                                            Tax = 40,
                                            Taxable = 2000,
                                            Gross = 2000
                                        }
                                    },
                                    new TaxDetailEmployeeDetail     //Employee2 ..
                                    {
                                        SSN = "222222222",
                                        EmployeeNumber = "1012",
                                        Daily = new Daily
                                        {
                                            PeriodStartDate = Convert.ToDateTime("2019-04-01"),
                                            PeriodEndDate = Convert.ToDateTime("2019-06-30"),
                                            Tax = 30,
                                            Taxable = 1500,
                                            Gross = 1500,
                                            Hours = "39",
                                            Weeks = "1"
                                        },
                                        QTD = new QTD
                                        {
                                            Tax = 30,
                                            Taxable = 1500,
                                            Gross = 1500,
                                            Hours = "39",
                                            Weeks = "1",
                                            Month1_12 = true,
                                            Month1_12Specified = true,
                                            Month2_12 = false,
                                            Month2_12Specified = true,
                                            Month3_12 = false,
                                            Month3_12Specified = true
                                        },
                                        YTD = new YTD
                                        {
                                            Tax = 30,
                                            Taxable = 1500,
                                            Gross = 1500
                                        }
                                    }
                                }
                            },
                            //M1 ... FL UI ...
                            new TaxDetail
                            {
                                CheckDate = Convert.ToDateTime("2019-04-30"),
                                CheckDateSpecified = true,
                                PayCycleCode = "KPM",
                                TaxCode = "FL UI",
                                Resident = false,
                                EmployeeDetail = new TaxDetailEmployeeDetail[]
                                {
                                    new TaxDetailEmployeeDetail     //Employee1 ...
                                    {
                                        SSN = "111111111",
                                        EmployeeNumber = "1011",
                                        Daily = new Daily
                                        {
                                            PeriodStartDate = Convert.ToDateTime("2019-04-01"),
                                            PeriodEndDate = Convert.ToDateTime("2019-06-30"),
                                            Tax = 40,
                                            Taxable = 2000,
                                            Gross = 2000,
                                            Hours = "39",
                                            Weeks = "1"
                                        },
                                        QTD = new QTD
                                        {
                                            Tax = 40,
                                            Taxable = 2000,
                                            Gross = 2000,
                                            Hours = "39",
                                            Weeks = "1",
                                            Month1_12 = true,
                                            Month1_12Specified = true,
                                            Month2_12 = false,
                                            Month2_12Specified = true,
                                            Month3_12 = false,
                                            Month3_12Specified = true
                                        },
                                        YTD = new YTD
                                        {
                                            Tax = 40,
                                            Taxable = 2000,
                                            Gross = 2000
                                        }
                                    },
                                    new TaxDetailEmployeeDetail        //Employee2 ...
                                    {
                                        SSN = "222222222",
                                        EmployeeNumber = "1012",
                                        Daily = new Daily
                                        {
                                            PeriodStartDate = Convert.ToDateTime("2019-04-01"),
                                            PeriodEndDate = Convert.ToDateTime("2019-06-30"),
                                            Tax = 30,
                                            Taxable = 1500,
                                            Gross = 1500,
                                            Hours = "39",
                                            Weeks = "1"
                                        },
                                        QTD = new QTD
                                        {
                                            Tax = 30,
                                            Taxable = 1500,
                                            Gross = 1500,
                                            Hours = "39",
                                            Weeks = "1",
                                            Month1_12 = true,
                                            Month1_12Specified = true,
                                            Month2_12 = false,
                                            Month2_12Specified = true,
                                            Month3_12 = false,
                                            Month3_12Specified = true
                                        },
                                        YTD = new YTD
                                        {
                                            Tax = 30,
                                            Taxable = 1500,
                                            Gross = 1500
                                        }
                                    }
                                }
                            },
                            //M2 ... FL UI .... Check the details ......
                            new TaxDetail
                            {
                                CheckDate = Convert.ToDateTime("2019-05-31"),
                                CheckDateSpecified = true,
                                PayCycleCode = "KPM",
                                TaxCode = "FL UI",
                                Resident = false,
                                EmployeeDetail = new TaxDetailEmployeeDetail[]
                                {
                                    new TaxDetailEmployeeDetail     //Employee1 ....
                                    {
                                        SSN = "111111111",
                                        EmployeeNumber = "1011",
                                        Daily = new Daily
                                        {
                                            PeriodStartDate = Convert.ToDateTime("2019-05-01"),
                                            PeriodEndDate = Convert.ToDateTime("2019-05-31"),
                                            Tax = 40,
                                            Taxable = 2000,
                                            Gross = 2000,
                                            Hours = "39",
                                            Weeks = "1"
                                        },
                                        QTD = new QTD
                                        {
                                            Tax = 80,
                                            Taxable = 4000,
                                            Gross = 4000,
                                            Hours = "39",
                                            Weeks = "1",
                                            Month1_12 = false,
                                            Month1_12Specified = true,
                                            Month2_12 = true,
                                            Month2_12Specified = true,
                                            Month3_12 = false,
                                            Month3_12Specified = true
                                        },
                                        YTD = new YTD
                                        {
                                            Tax = 80,
                                            Taxable = 4000,
                                            Gross = 4000
                                        }
                                    },
                                    new TaxDetailEmployeeDetail     //Employee2 ...
                                    {
                                        SSN = "222222222",
                                        EmployeeNumber = "1012",
                                        Daily = new Daily
                                        {
                                            PeriodStartDate = Convert.ToDateTime("2019-05-01"),
                                            PeriodEndDate = Convert.ToDateTime("2019-05-31"),
                                            Tax = 30,
                                            Taxable = 1500,
                                            Gross = 1500,
                                            Hours = "39",
                                            Weeks = "1"
                                        },
                                        QTD = new QTD
                                        {
                                            Tax = 60,
                                            Taxable = 3000,
                                            Gross = 3000,
                                            Hours = "39",
                                            Weeks = "1",
                                            Month1_12 = true,
                                            Month1_12Specified = true,
                                            Month2_12 = false,
                                            Month2_12Specified = true,
                                            Month3_12 = false,
                                            Month3_12Specified = true
                                        },
                                        YTD = new YTD
                                        {
                                            Tax = 60,
                                            Taxable = 3000,
                                            Gross = 3000
                                        }
                                    }
                                }
                            },
                            //M3 ... FL UI .... Check the details ......
                            new TaxDetail
                            {
                                CheckDate = Convert.ToDateTime("2019-06-30"),
                                CheckDateSpecified = true,
                                PayCycleCode = "KPM",
                                TaxCode = "FL UI",
                                Resident = false,
                                EmployeeDetail = new TaxDetailEmployeeDetail[]
                                {
                                    new TaxDetailEmployeeDetail     //Employee1 ....
                                    {
                                        SSN = "111111111",
                                        EmployeeNumber = "1011",
                                        Daily = new Daily
                                        {
                                            PeriodStartDate = Convert.ToDateTime("2019-06-01"),
                                            PeriodEndDate = Convert.ToDateTime("2019-06-30"),
                                            Tax = 40,
                                            Taxable = 2000,
                                            Gross = 2000,
                                            Hours = "39",
                                            Weeks = "1"
                                        },
                                        QTD = new QTD
                                        {
                                            Tax = 120,
                                            Taxable = 6000,
                                            Gross = 6000,
                                            Hours = "39",
                                            Weeks = "1",
                                            Month1_12 = false,
                                            Month1_12Specified = true,
                                            Month2_12 = false,
                                            Month2_12Specified = true,
                                            Month3_12 = true,
                                            Month3_12Specified = true
                                        },
                                        YTD = new YTD
                                        {
                                            Tax = 120,
                                            Taxable = 6000,
                                            Gross = 6000
                                        }
                                    },
                                    new TaxDetailEmployeeDetail     //Employee2 ...
                                    {
                                        SSN = "222222222",
                                        EmployeeNumber = "1012",
                                        Daily = new Daily
                                        {
                                            PeriodStartDate = Convert.ToDateTime("2019-06-01"),
                                            PeriodEndDate = Convert.ToDateTime("2019-06-30"),
                                            Tax = 30,
                                            Taxable = 1500,
                                            Gross = 1500,
                                            Hours = "39",
                                            Weeks = "1"
                                        },
                                        QTD = new QTD
                                        {
                                            Tax = 90,
                                            Taxable = 4500,
                                            Gross = 4500,
                                            Hours = "39",
                                            Weeks = "1",
                                            Month1_12 = true,
                                            Month1_12Specified = true,
                                            Month2_12 = false,
                                            Month2_12Specified = true,
                                            Month3_12 = false,
                                            Month3_12Specified = true
                                        },
                                        YTD = new YTD
                                        {
                                            Tax = 90,
                                            Taxable = 4500,
                                            Gross = 4500
                                        }
                                    }
                                }
                            }
                        }   //Tax Detail closed ..


                    }   //CompanyImportCompany closed
                    

                }   //Company closed ...

            };      //CompanyImport closed ...           

            SerializeXML(co);
            Console.WriteLine("Company name is###########" + CompName);
            return CompName;
        }

        public string GetProjectLocation()
        {
            /* string sDirectory = Environment.CurrentDirectory;
             log.Info("===============current directory:=>" + sDirectory);
             sDirectory = sDirectory.Substring(0, sDirectory.IndexOf("SpecFlowSharp"));
             sDirectory = Path.Combine(sDirectory, "SpecFlowSharp");
             sDirectory = Path.Combine(sDirectory, "SpecFlowSharp");
             * */

            string sDirectory = Environment.CurrentDirectory;
            string sDir = Directory.GetParent(System.AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
            Console.WriteLine("Full project path (Batch Execution):=>>>>>>>" + sDir);
            //log.Info("path is:=>" + sDir);
            return sDir;
        }
        private static void SerializeXML(CompanyImport ci)
        {
            string Resources_Path;
            AutomationUtilities _autoutilities = new AutomationUtilities();
            XmlSerializer ser = new XmlSerializer(typeof(CompanyImport));
            Resources_Path = _autoutilities.GetResourcesFolder();
            string filepath = "Tax.xml";
            TextWriter writer = new StreamWriter(Resources_Path + filepath);
            //TextWriter writer = new StreamWriter(@"D:\Tax.xml");            
            ser.Serialize(writer, ci);           

        }
    }

    


}
