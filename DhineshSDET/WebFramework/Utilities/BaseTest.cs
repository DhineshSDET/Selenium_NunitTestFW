using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

namespace WebFramework.Utilities
{
    public class BaseTest
    {
        public AventStack.ExtentReports.ExtentReports Extent = new AventStack.ExtentReports.ExtentReports();
        public ExtentTest test;
        String browserName;

        [OneTimeSetUp]
        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportPath = projectDirectory +"\\Report"+ "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            Extent.AttachReporter(htmlReporter);
            Extent.AddSystemInfo("Host Name","Local host");
            Extent.AddSystemInfo("Environment", "QA");
            Extent.AddSystemInfo("Username", "DhineshKumar");

        }

        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        [SetUp]
        public void StartBrowser()
        {
            test = Extent.CreateTest(TestContext.CurrentContext.Test.Name);
            //Configuration
            browserName = TestContext.Parameters["browserName"];
            if (browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"];
            }

            SelectBrowser(browserName);
            driver.Value.Manage().Window.Maximize();

        }
        public IWebDriver GetDriver()
        {
            return driver.Value;
        }
        public void SelectBrowser(string browser)
        {
            //Browser Factory Pattern Implementation
            switch (browser)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver(); //object for chrome browser
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver(); //object for Edge browser
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver(); //object for Firefox browser
                    break;
                default:
                    TestContext.Progress.WriteLine("Incorrect browser is mentioned");
                    break;
            }
        }

        public static JsonReader GetDataParser()
        {
            return new JsonReader();
        }
        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime time=DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            if (status == TestStatus.Failed)
            {
                test.Fail("Test failed", captureScreenshot(driver.Value, fileName));
                test.Log(Status.Fail, "test failed with logtrace" + stackTrace);
            }
            else if (status == TestStatus.Passed)
            {
                test.Pass("Test Passed");
            }
            Extent.Flush();
            driver.Value.Quit();
        }

        public MediaEntityModelProvider captureScreenshot(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot= ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}
