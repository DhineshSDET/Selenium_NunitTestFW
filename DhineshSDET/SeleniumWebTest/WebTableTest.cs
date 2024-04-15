using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Collections;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTest
{
    public class WebTableTest
    {
        private IWebDriver driver;
        public String GetUrl = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
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
        public void SortTable()
        {
            ArrayList manualSortArrayList = new ArrayList();
            ArrayList webSortArrayList = new ArrayList();
            IWebElement pageSize = driver.FindElement(By.XPath("//select[@id='page-menu']"));
            SelectElement dropdown = new SelectElement(pageSize);
            dropdown.SelectByText("20");
            //Get all Veg names in arrayList
            IList<IWebElement> veggiesList = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement veggie in veggiesList)
            {
                manualSortArrayList.Add(veggie.Text);// all veggie added to array list
            }

            //sort this arraylist by C# - A
            manualSortArrayList.Sort(); //now veggie is sorted
            foreach (String element in manualSortArrayList)
            {
                TestContext.Progress.WriteLine(element);
            }

            //Click column to do web sort
            driver.FindElement(By.CssSelector("th[aria-label*='Veg/fruit name']")).Click(); // Css with Regular expression of partial text
            //driver.FindElement(By.XPath("//th[contains(@aria-label,'Veg/fruit name')]")).Click(); // xpath with partial text

            //Get all Veg names in arrayList - B
            IList<IWebElement> sortedVeggiesList = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement veggie in sortedVeggiesList)
            {
                webSortArrayList.Add(veggie.Text);// all sorted veggie added to array list
            }

            TestContext.Progress.WriteLine(webSortArrayList);

            //Compare A and B should be equal
            Assert.AreEqual(manualSortArrayList, webSortArrayList);//Assert

        }
        [TearDown]
        public void StopBrowser()
        {
            driver.Close(); // Current instance window is closed
            //driver.Quit(); // all windows are closed
        }
    }
}
