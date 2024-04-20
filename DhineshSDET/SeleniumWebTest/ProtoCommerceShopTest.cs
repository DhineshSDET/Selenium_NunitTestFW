using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using WebFramework.Utilities;

namespace SeleniumTest
{
    public class ProtoCommerceShopTest : BaseTest
    {
        private IWebDriver driver;

        public void EnterUserNameAndPassword(String userName, String password)
        {
            driver.FindElement(By.Name("username")).SendKeys(userName); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys(password); //Enter Password
        }
        [Test]
        public void LoginPage() 
        {

            String expectedLoginPageTitle = "LoginPage Practise | Rahul Shetty Academy";

            EnterUserNameAndPassword("rahulshettyacademy", "learning");
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedLoginPageTitle));
        }
        [Test]
        public void HomePage()
        {
            String expectedHomePageTitle = "ProtoCommerce";
            EnterUserNameAndPassword("rahulshettyacademy", "learning");
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(3000);
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void CheckoutTwoProducts()
        {
            String[] expectedProduct = { "iphone X", "Blackberry" }; // expected string list
            EnterUserNameAndPassword("rahulshettyacademy", "learning");
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(5));// Explicit wait
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
            IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));//List of WebElements
            foreach (IWebElement product in products)// Each WebElement loop
            {   //product - Specific section within the page
                if (expectedProduct.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer i")).Click();
                }
            }
            driver.FindElement(By.PartialLinkText("Checkout")).Click();
        }
        [Test]
        public void E2ECommerceTest()
        {
            //Arrange
            String[] expectedProduct = { "iphone X", "Blackberry" }; // expected string list
            String[] actualProduct = new string[2];
            
            //Act Login
            EnterUserNameAndPassword("rahulshettyacademy", "learning");
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));// Explicit wait
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
            //Act Add to cart
            IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));//List of WebElements
            foreach (IWebElement product in products)// Each WebElement loop
            {   //product - Specific section within the page
                if (expectedProduct.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer i")).Click();
                }
            }
            //Act Checkout
            driver.FindElement(By.PartialLinkText("Checkout")).Click();
            IList<IWebElement> checkoutCardsElements = driver.FindElements(By.CssSelector("h4 a"));

            for (int i = 0; i < checkoutCardsElements.Count; i++)
            {
                actualProduct[i]=checkoutCardsElements[i].Text;
            }
            Assert.AreEqual(expectedProduct,actualProduct); //Assert Array Comparision

            driver.FindElement(By.CssSelector(".btn.btn-success")).Click();
            driver.FindElement(By.XPath("//input[@id='country']")).SendKeys("ind");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.FindElement(By.LinkText("India")).Click();
            driver.FindElement(By.XPath("//label[@for='checkbox2']")).Click();
            driver.FindElement(By.XPath("//input[@value='Purchase']")).Click();
            String actualPurchaseSuccessMessage = driver.FindElement(By.CssSelector(".alert-success")).Text;
            
            StringAssert.Contains("Success", actualPurchaseSuccessMessage);//String Assert Contains

        }
    }
}
