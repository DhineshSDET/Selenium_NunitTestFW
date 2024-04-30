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
using SeleniumTest.Utilities;
//using WebDriver.driver.Configs.Impl;

namespace SeleniumTest
{
    [Parallelizable(ParallelScope.Self)]
    public class DragAndDropTest : BaseTest
    {
        public String DragandDropUrl = "https://demoqa.com/droppable/";
        public String MoveToElementUrl = "https://rahulshettyacademy.com/";
        public String ExpectedPageTitle = "About Us | Rahul Shetty Academy";

        [Test]
        public void DragAndDrop()
        {
            //Thread.Sleep(8000);
            driver.Value.Url = DragandDropUrl;
            Actions a = new Actions(driver.Value);
            Thread.Sleep(3000);
            IWebElement dragElement = driver.Value.FindElement(By.XPath("//div[@id='draggable']"));
            IWebElement dropElement = driver.Value.FindElement(By.XPath("//div[@id='droppable']"));
            a.DragAndDrop(dragElement, dropElement).Build().Perform();

        }
        [Test]
        public void HoverMoveToElement()
        {
            Thread.Sleep(8000);
            driver.Value.Url = MoveToElementUrl;
            Actions a = new Actions(driver.Value);
            a.MoveToElement(driver.Value.FindElement(By.XPath("//a[@class='dropdown-toggle']"))).Perform();
            Thread.Sleep(3000);
            a.MoveToElement(driver.Value.FindElement(By.XPath("//a[@href='about-my-mission']"))).Click().Perform();
            Thread.Sleep(3000);
            String PageTitle = driver.Value.Title;
            Assert.That(PageTitle, Is.EqualTo(ExpectedPageTitle));
        }
    }
}
