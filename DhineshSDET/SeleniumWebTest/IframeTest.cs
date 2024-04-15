using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTest
{
    public class IframeTest
    {
        private IWebDriver driver;
        public String GetUrl = "https://rahulshettyacademy.com/AutomationPractice/";
        [SetUp]
        public void StartBrowser()
        {
            SelectBrowser("Chrome");
            driver.Manage().Window.Maximize(); //maximize browser
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5);
            driver.Url = GetUrl;
        }
        public void SelectBrowser(String browser)
        {
            switch (browser)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver(); //object for chrome browser
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver(); //object for Edge browser
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver(); //object for Firefox browser
                    break;
                default:
                    TestContext.Progress.WriteLine("Incorrect browser is mentioned");
                    break;
            }
        }
        [Test]
        public void SwitchFrame()
        {
            //Scroll using javascriptExecutor
            IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", frameScroll);

            //Switch to Frame - id, name, index
            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.XPath("//a[@class='new-navbar-highlighter']")).Click();
            Thread.Sleep(3000);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);

            //Switch to driver
            driver.SwitchTo().DefaultContent();
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
        }

        [TearDown]
        public void StopBrowser()
        {
            driver.Close(); // Current instance window is closed
            //driver.Quit(); // all windows are closed
        }
    }
}
