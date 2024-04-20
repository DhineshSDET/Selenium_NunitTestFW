This Repo Covers C# Fundamentals for Automation Testing using Selenium

Selenium Dependencies
  -  Selenium.WebDriver
  -  Selenium.Support
  -  WebDriverManager
  -  DotNetSeleniumExtras.WaitHelpers
  -  DotNetSeleniumExtras.PageObjects
  -  DotNetSeleniumExtras.PageObjects.Core

Nunit Test Framework
  Used below Annotations
    -  [Test]
    -  [SetUp]
    -  [TearDown]
    -  [Parallelizable(ParallelScope.Self)]
    -  [Parallelizable(ParallelScope.All)]
    -  [TestCaseSource("AddTestData")]
    -  [TestCase("username", "password")]  
    
Built Base Test class which handles below
  -  static driver initialization with Thread safe for parallel run
  -  Custom method to choose browser
  -  App.config to set key value pair for browser selection
  -  Static JsonReader methods to read data from dataset collection method

Test Data Strategy
  -  Json structure file to select by key
  -  Created JsonReader class which has custom methods to handle String Data and String[] data to parse
  -  Get list of strings and conver to array and pass token/key to fetch json value
  -  Created static IEnumerable List method to fecth parameterized data from json file
  -  Used yield to wait for each data to fetch and return
    
Page Object Design Pattern 
  -  Each page class with constructor to handle driver setting using PageFactory class
  -  Created Page Object Factory to handle each WebElement/List of WebElement
  -  Getter method to return WebElements and its operation in actual test class
  -  Also return object for each page transition

Folder structure
  - Utilities
  - TestData
  - PageObjects
  - Tests

Assertion 
    -  Assert.That
    -  Assert.AreEqual
    -  StringAssert 

Each Test adheres to Arrange Act Assert Test Pattern
Object instance for each Test methods
Wait handle of Explicit wait


