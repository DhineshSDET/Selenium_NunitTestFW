This Repo Covers C# Fundamentals for Automation Testing using Selenium

Selenium Dependencies
  -  Selenium.WebDriver
  -  Selenium.Support
  -  WebDriverManager
  -  DotNetSeleniumExtras.WaitHelpers
  -  DotNetSeleniumExtras.PageObjects
  -  DotNetSeleniumExtras.PageObjects.Core
  -  AventStack.ExtentReports

Nunit Test Framework Used below Annotations
  - [Test]
  - [SetUp]
  - [TearDown]
  - [Parallelizable(ParallelScope.Self)]
  - [Parallelizable(ParallelScope.All)]
  - [TestCaseSource("AddTestData")]
  - [TestCase("username", "password")]  
  - [Category("Smoke")]
  - [OneTimeSetUp]
    
Built Base Test class which handles below
  - static driver initialization with Thread safe for parallel run
  - Custom method to choose browser
  - App.config to set key value pair for browser selection
  - Static JsonReader methods to read data from dataset collection method

Test Data Strategy
  - Json structure file to select by key
  - Created JsonReader class which has custom methods to handle String Data and String[] data to parse
  - Get list of strings and convert to array and pass token/key to fetch json value
  - Created static IEnumerable List method to fecth parameterized data from json file
  - Used yield to wait for each data to retrieve and return
    
Page Object Design Pattern 
  - Each page class with constructor to handle driver setting using PageFactory class
  - Created Page Object Factory to handle each WebElement/List of WebElement
  - Getter method to return WebElements and its operation in actual test class
  - Also return object for each page transition

Folder structure
  - Utilities
  - TestData
  - PageObjects
  - Tests

Selenium WebElement Test
  - Login Scenario's
  - Handling Alert
  - Handling Popup
  - Auto Suggestion Search
  - Drag and drop
  - Hover
  - Move to element
  - Switch Frames
  - Sort Table
  - Switch Windows
  - Checkout two products
  - End to End e-commerce Order creation

Assertion 
  - Assert.That
  - Assert.AreEqual
  - StringAssert 

Parallel Execution
  - All data sets of Test Methods in parallel
  - All test methods in one class in parallel
  - All test class in one project in parallel

Test Run
  - Test Explorer
  - Command Line CLI with Test Params
  - Jenkins

Test Report
  - Used Extent Reports to generate HTML test report
  - Capture Screenshots when tests are failing and base64.png available in report

Test Pattern
  - Each Test adheres to the //Arrange //Act //Assert 

Jenkins Pipeline
  - Configure Jenkins pipeline to pick this git repo - https://github.com/DhineshSDET/Selenium_NunitTestFW.git
  - Choice parameters could be added to select a browser during the selection
  - Change branch specifier from */master to */main
  - Add the below (Execute Windows batch command) under build steps and modify according to your need 
  - dotnet test DhineshSDET/WebFramework/WebFramework.csproj -- TestRunParameters.Parameter(name=\"browserName\", value=\"%browserName%\")


