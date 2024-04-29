using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Collections;
using OpenQA.Selenium.Support.UI;
using SeleniumTest.Utilities;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTest
{
    [Parallelizable(ParallelScope.All)]
    public class WebTableTest : BaseTest
    {
        [Test]
        public void SortTable()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
            ArrayList manualSortArrayList = new ArrayList();
            ArrayList webSortArrayList = new ArrayList();
            IWebElement pageSize = driver.Value.FindElement(By.XPath("//select[@id='page-menu']"));
            SelectElement dropdown = new SelectElement(pageSize);
            dropdown.SelectByText("20");
            //Get all Veg names in arrayList
            IList<IWebElement> veggiesList = driver.Value.FindElements(By.XPath("//tr/td[1]"));
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
            driver.Value.FindElement(By.CssSelector("th[aria-label*='Veg/fruit name']")).Click(); // Css with Regular expression of partial text
            //driver.Value.FindElement(By.XPath("//th[contains(@aria-label,'Veg/fruit name')]")).Click(); // xpath with partial text

            //Get all Veg names in arrayList - B
            IList<IWebElement> sortedVeggiesList = driver.Value.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement veggie in sortedVeggiesList)
            {
                webSortArrayList.Add(veggie.Text);// all sorted veggie added to array list
            }

            TestContext.Progress.WriteLine(webSortArrayList);

            //Compare A and B should be equal
            Assert.AreEqual(manualSortArrayList, webSortArrayList);//Assert

        }
    }
}
