using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumTest.Utilities;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTest
{
    [Parallelizable(ParallelScope.All)]
    public class IframeTest : BaseTest
    {
        [Test]
        public void SwitchFrame()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            //Scroll using javascriptExecutor
            IWebElement frameScroll = driver.Value.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver.Value;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", frameScroll);

            //Switch to Frame - id, name, index
            driver.Value.SwitchTo().Frame("courses-iframe");
            driver.Value.FindElement(By.XPath("//a[@class='new-navbar-highlighter']")).Click();
            Thread.Sleep(3000);
            TestContext.Progress.WriteLine(driver.Value.FindElement(By.CssSelector("h1")).Text);

            //Switch to driver
            driver.Value.SwitchTo().DefaultContent();
            TestContext.Progress.WriteLine(driver.Value.FindElement(By.CssSelector("h1")).Text);
        }
    }
}
