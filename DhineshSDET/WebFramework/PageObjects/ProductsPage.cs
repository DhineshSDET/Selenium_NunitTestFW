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
    public class ProductsPage
    {
        private IWebDriver driver;
        private By cardTitle = By.CssSelector(".card-title a");
        private By addToCart = By.CssSelector(".card-footer i");

        //Constructor
        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //Page Object Factory
        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> cards;

        [FindsBy(How = How.CssSelector, Using = ".card-title a")]
        private IWebElement productTitle;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkOut;

        //Getters Encapsulation 
        public IList<IWebElement> Getcards()
        {
            return cards;
        }
        public CheckOutPage CheckOutButton()
        {
            checkOut.Click();
            return new CheckOutPage(driver);
        }
        public By GetCardTitle()
        {
            return cardTitle;
        }
        public By AddToCartButton()
        {
            return addToCart;
        }

        public void WaitForPageDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));// Explicit wait
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        }
    }
}
