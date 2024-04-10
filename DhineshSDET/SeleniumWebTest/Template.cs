using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTest
{
    public class Template
    {
        private IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            //WebDriverManager - Call , check browser version and get correct exe to run
            //chromedriver.exe on chrome browser
            //geckodriver.exe on firefox browser

            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            //driver = new EdgeDriver(); //object for Edge browser

            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //driver = new FirefoxDriver(); //object for Firefox browser

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver(); //object for chrome browser

            driver.Manage().Window.Maximize(); //maximize browser
            //driver.Manage().Window.FullScreen(); //Full screen browser
            //driver.Manage().Window.Minimize(); //minimize browser
        }
        [Test]
        public void Login() 
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            TestContext.Progress.WriteLine(driver.Title); //print title
            TestContext.Progress.WriteLine(driver.Url); //print url
        }
        [TearDown]
        public void StopBrowser()
        {
            //driver.Close(); // Current instance window is closed
            //driver.Quit(); // all windows are closed
        }
    }
}
