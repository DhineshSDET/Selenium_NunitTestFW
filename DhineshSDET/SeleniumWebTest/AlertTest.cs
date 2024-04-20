using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTest
{
    public class AlertTest
    {
        private IWebDriver driver;
        public String AlertUrl = "https://rahulshettyacademy.com/AutomationPractice/";
        private String name = "Dhinesh";
        [SetUp]
        public void StartBrowser()
        {
            SelectBrowser("Chrome");
            driver.Manage().Window.Maximize(); //maximize browser
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5);
            driver.Url = AlertUrl;
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
        public void AlertMessageHandle()
        {
            driver.FindElement(By.XPath("//input[@id='name']")).SendKeys(name);//Enter Text
            driver.FindElement(By.XPath("//input[@id='alertbtn']")).Click();//Click Alert
            String alertText = driver.SwitchTo().Alert().Text;
            StringAssert.Contains(name, alertText);//Assert
            driver.SwitchTo().Alert().Accept();//Click Ok
        }
        [Test]
        public void AlertConfirmOk()
        {
            driver.FindElement(By.XPath("//input[@id='name']")).Clear();
            driver.FindElement(By.XPath("//input[@id='name']")).SendKeys("Dhinesh");//Enter Text
            driver.FindElement(By.CssSelector("input[onclick*='displayConfirm']")).Click();//Click Confirm
            driver.SwitchTo().Alert().Accept();//Click Ok
        }
        [Test]
        public void AlertConfirmCancel()
        {
            driver.FindElement(By.XPath("//input[@id='name']")).Clear();
            driver.FindElement(By.XPath("//input[@id='name']")).SendKeys("Dhinesh");//Enter Text
            driver.FindElement(By.CssSelector("input[onclick*='displayConfirm']")).Click();//Click Confirm
            driver.SwitchTo().Alert().Dismiss();//Click Cancel
        }
        [Test]
        public void AutoSuggestionSearch()
        {
            driver.FindElement(By.Id("autocomplete")).SendKeys("In");//Enter Text
            Thread.Sleep(3000);
            IList<IWebElement> autoSearchList = driver.FindElements(By.CssSelector(".ui-menu-item div"));
            foreach (IWebElement autoSearch in autoSearchList)
            {
                if(autoSearch.Text.Equals("India"))
                   autoSearch.Click();
            }
        }
        [TearDown]
        public void StopBrowser()
        {
            driver.Quit(); // all windows are closed
        }
    }
}
