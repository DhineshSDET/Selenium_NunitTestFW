using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace WebFramework.PageObjects
{
    public class CheckOutPage
    {
        private IWebDriver driver;

        //Constructor
        public CheckOutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //Page Object Factory
        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> selectedCards;

        [FindsBy(How = How.CssSelector, Using = ".btn.btn-success")]
        private IWebElement CheckOut;

        public CheckOutPage CheckOutButton()
        {
            CheckOut.Click();
            return new CheckOutPage(driver);

        }
        public IList<IWebElement> GetSelectedCards()
        {
            return selectedCards;
        }
    }
}
