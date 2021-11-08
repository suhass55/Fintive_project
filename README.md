# Finitive Application 

Selenium-CSharp-Specflow(BDD)-Framework-Nunit

# Introduction
This Test Automation Framework is created using C# + Selenium Web Driver + SpecFlow(BDD)+ Page Object Model + Nunit
Framework helps to automate a web based applications. We would not need to start from scratch as it has all the helper classes

# What is this repository for?
It is used for internal purpose(Finitive_DEV)

# Pre-requisites to be installed

Install Visual Studio Community 2019(Version 16 or above) or Visual Studio Code Latest Version

#Execution
If you wanted to run the Scripts from VisualStudio TestExplorer from your Local Machine please follow the below steps:

    1   Open the "..\FinitiveProject\FinitestLibrary.sln" file from Visual studio and Build the Project
	
	2   Under Test Menu -> Configure Run Settings file and Select Solution Wide Run Settings Files (Settings.runsettings file from the Framework)
	
	3   Now from TestExplorer, search or select the test scripts and run them
	
	4   Once we trigger the run, scripts will execute on your local machine and once the execution is completed we will have the Extent reports and Log files under the Folder 
	    ..\Automation_Report\ExtentReportsUpdated
		

# Artifacts have the below
ExtentReports:
Scenario Summary,Execution Details, Failure error details and screenshots
Logfile:
log4net is configured to capture the test execution logs

# Utilities
This framework contains all the helper classes which has Synchronization, Timeouts, Utilities, Waits, Pop ups and ExcelManager

# Contribution guidelines
If you have a fix for any existing problem, please submit a pull request


