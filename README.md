# OSVExperience - OSVA Automation

Selenium-CSharp-Specflow(BDD)-Framework-Nunit

# Introduction
This Test Automation Framework is created using C# + Selenium Web Driver + SpecFlow(BDD)+ Page Object Model + Nunit
Framework helps to automate a web based applications. We would not need to start from scratch as it has all the helper classes

# What is this repository for?
It is used for internal purpose(OSVExperience - OSVA)

# Pre-requisites to be installed

Install Visual Studio Community 2019(Version 16 or above) or Visual Studio Code Latest Version
Clone the framework from repository "https://onesourcevirtual.visualstudio.com/DefaultCollection/OSVExperience/_git/OSVEAtmosphereAutomation"

#Execution
If you wanted to run the Scripts from VisualStudio TestExplorer from your Local Machine please follow the below steps:

    1   Open the "..\OneAtmosphere\OneAtmosTestLibrary.sln" file from Visual studio and Build the Project
	
	2   Under Test Menu -> Configure Run Settings file and Select Solution Wide Run Settings Files (Settings.runsettings file from the Framework)
	
	3   Now from TestExplorer, search or select the test scripts and run them
	
	4   Once we trigger the run, scripts will execute on your local machine and once the execution is completed we will have the Extent reports and Log files under the Folder 
	    ..\OSVEAtmosphereAutomation\ExtentReportsUpdated
		
If you wanted to execute Scripts from VSTS Pipelines we have the below 3 ways

```sh
1. Using Build Pipelines where we will Select 'TestPlan' and 'TestSuites' for running the Test Scripts which are integrated with the Test Cases

	https://onesourcevirtual.visualstudio.com/OSVExperience/_build?definitionId=94	
	
	Currently above Pipeline job is pointing to TestPlan: 36404 - OSV Atmosphere Regression Suite & TestSuite: OSV Atmosphere Regression Suite\OSVAtmosphere Workflows 
	
	If you wanted to execute any Particular Test Suite, Edit the above Pipeline job and update the Test Plan and Suite section details under 'VisualStudioTest' task
```	
```sh
2. Using Build Pipeline where we mention tags like Particular TestCategory of Scripts, we can also mention particular TestId as well (36713, 36604, 36666)	
	
	https://onesourcevirtual.visualstudio.com/OSVExperience/_build?definitionId=100
	
	To run a particular tag Scripts, Edit the Pipeline job and update 'TestCategory=50264' under Test filter criteria section of Visual Studio Test Task
```	

```sh
3. Using Build & Release Stage Pipelines where we directly select the Particular Test Case from TestSuites and execute with 'Run with Options'
	
	Prerequisite: Run the On Demand Stage Build Pipeline https://onesourcevirtual.visualstudio.com/OSVExperience/_build?definitionId=95 to run scripts on the latest Automation CodeBase
	
	Once the above Build Pipeline is completed Navigate to TestPlans and then TestSuite
	
	Click on 'Execute' tab and select the Test Cases that you would like to run then Click on 'Run with Options' under Run test Options
	
	Select test type and runner: Automated tests using release stage
	
	Select a build: Above Executed OSVAOnDemandStageBuild
	
	Release Pipeline: OSVA Automation Build
	
	Stage: Stage 1	
	
	After Selecting all the above options we click on 'Run' button so that the Test Scripts will be executed on the Configured Agents and update the results in TestSuites
```		
# What happens when a build is triggered and Completed
1. Reports will be Generated to download and View the Detailed information

	We are adding our Automation Reports as an Artifacts for the Build Pipeline jobs
	
	To Access the Report Please follow the below steps:
	
		Click on the Build Pipeline job after it is completed
		
		Under Summary Tab Click on 'Agent Job 1' under Jobs section
		
		Click on artifacts link displayed and then download the Reports, If we have any failure Reports will have detailed screnshots with the failure reasons

2. VSTS also generates the Test Execution Reports with Pie charts, exection time, %Pass and % Fail other details

	Click on 'Tests' tab next to 'Summary' tab in the Completed Build Pipeline
	
	If we have any Failures we will have Detailed information above failure and respective Screenshot and StandardConsoleOutput with Gherkin steps and it Completion Status

3. Test Case Status will be updated to Passed or Failed in the Test Suite without any Manual Intervention

# Artifacts have the below
ExtentReports:
Scenario Summary,Execution Details, Failure error details and screenshots
Logfile:
log4net is configured to capture the test execution logs

# Utilities
This framework contains all the helper classes which has Synchronization, Timeouts, Utilities, Waits, Pop ups and ExcelManager

# Contribution guidelines
If you have a fix for any existing problem, please submit a pull request

# OSVATaxExTestData Setup:

We are having a Parameter "OSVATaxExDataSetup" in Settings.runsettings file and we keep the value as "No" to skip the data Setup part in TaxEx applications

If you change the Parameter to "Yes" the PayrollTax and Garnishments Cases will execute with TaxEx Application Login and Setting up the data and validating in OSVACustomer Portal

We have placed the Test Companies data that we are using for PayrollTax And Garnishments widgets test cases under TaxExTestData/CompaniesData.json file

We need to ensure that the using companies are assigned to those Contact user we are using to validate the Widgets.

Example: 
  Navigate to https://qa.taxex.net/app/admin/useredit/?id=5901 and Ensure the Below Companies are assinged to the User  
  
  "TC_36567": "AutoComp_5117000,AutoComp_5117001,AutoComp_5117002",
  
  "TC_36575": "AutoComp_2288852,AutoComp_2288853,AutoComp_2288854",
  
  "TC_36581": "AutoComp_3542945,AutoComp_3542946,AutoComp_3542947",
  
  "TC_36586": "AutoComp_6923629,AutoComp_6923630,AutoComp_6923631",
  
  "TC_36591": "AutoComp_3892743,AutoComp_3892744,AutoComp_3892745",
  
  "TC_36592": "AutoComp_8097847,AutoComp_8097848,AutoComp_8097849",
  *5901-ContactUser: "dprdatavalidation@gmail.com"  (5901 is the trailing id to Navigate to the User directly)
  
Please have a look at TaxExTestData/CompaniesData.json file for the other PayrollTax Cases and the respective Contact users.

When you run PayrollTax And Garnishments Scripts with OSVATaxExDataSetup parameter to 'Yes' it will also update the existing Companies data in TaxExTestData/CompaniesData.json file

Whenever the data got Corrupted or Expired, please run the script with OSVATaxExDataSetup parameter to 'Yes' and Commit the TaxExTestData/CompaniesData.json file
