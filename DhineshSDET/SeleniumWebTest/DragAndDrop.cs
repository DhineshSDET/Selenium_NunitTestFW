using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTest
{
    public class DragAndDropTest
    {
        public IWebDriver driver;
        public String DragandDropUrl = "https://demoqa.com/droppable/";
        public String MoveToElementUrl = "https://rahulshettyacademy.com/";
        public String ExpectedPageTitle = "About Us | Rahul Shetty Academy";
        [SetUp]
        public void StartBrowser()
        {
            SelectBrowser("Chrome");
            driver.Manage().Window.Maximize(); //maximize browser
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
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
        public void DragAndDrop()
        {
            driver.Url = DragandDropUrl;
            Actions a = new Actions(driver);
            Thread.Sleep(3000);
            IWebElement dragElement = driver.FindElement(By.XPath("//div[@id='draggable']"));
            IWebElement dropElement = driver.FindElement(By.XPath("//div[@id='droppable']"));
            a.DragAndDrop(dragElement, dropElement).Build().Perform();

        }
        [Test]
        public void HoverMoveToElement()
        {
            Actions a = new Actions(driver);
            driver.Url = MoveToElementUrl;
            a.MoveToElement(driver.FindElement(By.XPath("//a[@class='dropdown-toggle']"))).Perform();
            Thread.Sleep(3000);
            a.MoveToElement(driver.FindElement(By.XPath("//a[@href='about-my-mission']"))).Click().Perform();
            Thread.Sleep(3000);
            String PageTitle = driver.Title;
            Assert.That(PageTitle, Is.EqualTo(ExpectedPageTitle));
        }
        [TearDown]
        public void StopBrowser()
        {
            driver.Close(); // Current instance window is closed
            //driver.Quit(); // all windows are closed
        }
    }
}
