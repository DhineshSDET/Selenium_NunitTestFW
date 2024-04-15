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
    internal class WindowHandleTest
    {
        private IWebDriver driver;
        public String GetUrl = "https://rahulshettyacademy.com/loginpagePractise/";
        public String Email = "mentor@rahulshettyacademy.com";
        [SetUp]
        public void StartBrowser()
        {
            SelectBrowser("Chrome");
            driver.Manage().Window.Maximize(); //maximize browser
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
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
        public void SwitchWindows()
        {
            String parentWindowName = driver.CurrentWindowHandle;
            driver.FindElement(By.XPath("//a[@class='blinkingText']")).Click();
            Thread.Sleep(2000);
            Assert.AreEqual(2, driver.WindowHandles.Count);//Assert
            String childWindowName = driver.WindowHandles[1];
            driver.SwitchTo().Window(childWindowName);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);
            String textEmail = driver.FindElement(By.CssSelector(".red")).Text;
            String[] splittedText = textEmail.Split("at");
            String[] trimmedText = splittedText[1].Trim().Split(" ");
            String loginText = trimmedText[0];
            TestContext.Progress.WriteLine(loginText);
            Assert.AreEqual(Email, loginText);//Assert

            driver.SwitchTo().Window(parentWindowName);
            driver.FindElement(By.Name("username")).SendKeys(loginText); //Enter Username
        }
        [TearDown]
        public void StopBrowser()
        {
            driver.Close(); // Current instance window is closed
            //driver.Quit(); // all windows are closed
        }
    }
}
