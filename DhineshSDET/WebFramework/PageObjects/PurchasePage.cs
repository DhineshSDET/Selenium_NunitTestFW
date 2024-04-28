using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.PageObjects
{
    public class PurchasePage
    {
        private IWebDriver driver;

        public PurchasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //Page Object Factory
        [FindsBy(How = How.XPath, Using = "//input[@id='country']")]
        private IWebElement deliveryLocation;

        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement selectLocation;

        [FindsBy(How = How.XPath, Using = "//label[@for='checkbox2']")]
        private IWebElement iAgreeCheckbox;

        [FindsBy(How = How.XPath, Using = "//input[@value='Purchase']")]
        private IWebElement purchase;

        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private IWebElement success;

        public IWebElement GetDeliveryLocation()
        {
            return deliveryLocation;
        }
        public void WaitForPageDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));// Explicit wait
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
        }
        public IWebElement SelectLocation()
        {
            return selectLocation;
        }
        public IWebElement AgreeCheckbox()
        {
            return iAgreeCheckbox;
        }
        public IWebElement PurchaseButton()
        {
            return purchase;
        }
        public IWebElement Success()
        {
            return success;
        }
    }
}
