using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading;

namespace WebFramework.Utilities
{
    public class BaseTest
    {
        //public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        [SetUp]
        public void StartBrowser()
        {
            //Configuration
            String browserName = ConfigurationManager.AppSettings["browser"];
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
            driver.Value.Quit();
        }
    }
}
