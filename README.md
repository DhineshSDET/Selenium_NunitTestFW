This Repo Covers C# Fundamentals for Automation Testing using Selenium

/*Project Structure*/
.Net Framework - net8.0 LTS
-Solution [DhineshSDET]
  -Project [SeleniumWebTest][Nunit Test Project]
      -[Edit]Add Nuget Package of Selenium under [Manager Nuget Package]
        -[Edit]Added Selenium.WebDriver 4.18.1 & Selenium.Support 4.18.1 package
            ![image](https://github.com/DhineshSDET/Selenium_NunitTestFW/assets/163389482/dbebed17-2864-4379-a7eb-799071a55a7a)

/*Nunit Framework*/
Nunit - Commonly Used and QA's Popular and widely choosen framework

/*Naming Convention*/
Pascal Case 

/*Nunit Annotations*/
[Mostly Used]
SetUp - Indicates a method of a TestFixture called immediately before each test method
Test - Marks a method of a TestFixture that represents a test
TearDown - Indicates a method of a TestFixture called immediately after each test method

[Other]
Timeout - Provides a timeout value in milliseconds for test cases
Retry - Causes a test to rerun if it fails, up to a maximum number of times
MaxTime - Specifies the maximum time in milliseconds for a test case to succeed

/*Selenium*/
