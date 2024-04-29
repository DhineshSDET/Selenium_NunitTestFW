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
    public class WindowHandleTest : BaseTest
    {
        public string Email = "mentor@rahulshettyacademy.com";

        [Test]
        public void SwitchWindows()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            string parentWindowName = driver.Value.CurrentWindowHandle;
            driver.Value.FindElement(By.XPath("//a[@class='blinkingText']")).Click();
            Thread.Sleep(2000);
            Assert.AreEqual(2, driver.Value.WindowHandles.Count);//Assert
            string childWindowName = driver.Value.WindowHandles[1];
            driver.Value.SwitchTo().Window(childWindowName);
            TestContext.Progress.WriteLine(driver.Value.FindElement(By.CssSelector(".red")).Text);
            string textEmail = driver.Value.FindElement(By.CssSelector(".red")).Text;
            string[] splittedText = textEmail.Split("at");
            string[] trimmedText = splittedText[1].Trim().Split(" ");
            string loginText = trimmedText[0];
            TestContext.Progress.WriteLine(loginText);
            Assert.AreEqual(Email, loginText);//Assert

            driver.Value.SwitchTo().Window(parentWindowName);
            driver.Value.FindElement(By.Name("username")).SendKeys(loginText); //Enter Username
        }
    }
}
