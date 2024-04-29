﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
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
    public class AlertTest : BaseTest
    {
        private String name = "Dhinesh";
        private String country = "India";
        [Test] 
        public void AlertMessageHandle()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            driver.Value.FindElement(By.XPath("//input[@id='name']")).SendKeys(name);//Enter Text
            driver.Value.FindElement(By.XPath("//input[@id='alertbtn']")).Click();//Click Alert
            String alertText = driver.Value.SwitchTo().Alert().Text;
            StringAssert.Contains(name, alertText);//Assert
            driver.Value.SwitchTo().Alert().Accept();//Click Ok
        }
        [Test]
        public void AlertConfirmOk()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            driver.Value.FindElement(By.XPath("//input[@id='name']")).Clear();
            driver.Value.FindElement(By.XPath("//input[@id='name']")).SendKeys(name);//Enter Text
            driver.Value.FindElement(By.CssSelector("input[onclick*='displayConfirm']")).Click();//Click Confirm
            driver.Value.SwitchTo().Alert().Accept();//Click Ok
        }
        [Test]
        public void AlertConfirmCancel()
        {
            Thread.Sleep(5000);
            driver.Value.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            driver.Value.FindElement(By.XPath("//input[@id='name']")).Clear();
            driver.Value.FindElement(By.XPath("//input[@id='name']")).SendKeys(name);//Enter Text
            driver.Value.FindElement(By.CssSelector("input[onclick*='displayConfirm']")).Click();//Click Confirm
            driver.Value.SwitchTo().Alert().Dismiss();//Click Cancel
        }
        [Test]
        public void AutoSuggestionSearch()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            driver.Value.FindElement(By.Id("autocomplete")).SendKeys("In");//Enter Text
            Thread.Sleep(3000);
            IList<IWebElement> autoSearchList = driver.Value.FindElements(By.CssSelector(".ui-menu-item div"));
            foreach (IWebElement autoSearch in autoSearchList)
            {
                if(autoSearch.Text.Equals(country))
                   autoSearch.Click();
            }
        }
    }
}
