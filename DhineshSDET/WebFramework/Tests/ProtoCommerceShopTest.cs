using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using WebFramework.PageObjects;
using WebFramework.Utilities;

namespace WebFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    public class ProtoCommerceShopTest : BaseTest
    {
        
        [Test]
        public void LoginPage()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //Arrange
            string expectedLoginPageTitle = "LoginPage Practise | Rahul Shetty Academy";
            string expectedHomePageTitle = "ProtoCommerce";
            //Act
            string actualLoginPageTitle = driver.Value.Title;
            //Assert
            Assert.That(actualLoginPageTitle, Is.EqualTo(expectedLoginPageTitle));
            //Act
            LoginPage loginPage = new LoginPage(GetDriver());
            loginPage.ValidLogin("rahulshettyacademy", "learning");
            string actualHomePageTitle = driver.Value.Title;
            //Assert
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void HomePage()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //Arrange
            string expectedHomePageTitle = "ProtoCommerce";
            //Act
            LoginPage loginPage = new LoginPage(GetDriver());
            loginPage.ValidLogin("rahulshettyacademy", "learning");
            string actualHomePageTitle = driver.Value.Title;
            //Assert
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void CheckoutTwoProducts()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //Arrange
            string[] expectedProduct = { "iphone X", "Blackberry" }; // expected string list
            string[] actualProduct = new string[2];
            //Act
            LoginPage loginPage = new LoginPage(GetDriver());
            ProductsPage productsPage = loginPage.ValidLogin("rahulshettyacademy", "learning");
            productsPage.WaitForPageDisplay();
            IList<IWebElement> products = productsPage.Getcards();
            foreach (IWebElement product in products)// Each WebElement loop
            {   //product - Specific section within the page
                if (expectedProduct.Contains(product.FindElement(productsPage.GetCardTitle()).Text))
                {
                    product.FindElement(productsPage.AddToCartButton()).Click();
                }
            }
            CheckOutPage checkoutpage = productsPage.CheckOutButton();

            IList<IWebElement> checkoutCardsElements = checkoutpage.GetSelectedCards();
            for (int i = 0; i < checkoutCardsElements.Count; i++)
            {
                actualProduct[i] = checkoutCardsElements[i].Text;
            }
            //Assert Array Comparison
            Assert.AreEqual(expectedProduct, actualProduct);

        }
        //[TestCase("rahulshettyacademy", "learning")]
        //[TestCase("rahulshetty", "learning")]
        [Test]
        [TestCaseSource("AddTestData")]
        //[Parallelizable(ParallelScope.All)]
        public void E2ECommerceTest(String username, String password, String[] expectedProduct)
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //Arrange
            //string[] expectedProduct = { "iphone X", "Blackberry" }; // expected string list
            string[] actualProduct = new string[2];

            //Act
            LoginPage loginPage = new LoginPage(GetDriver());
            ProductsPage productsPage = loginPage.ValidLogin(username, password);
            productsPage.WaitForPageDisplay();
            IList<IWebElement> products = productsPage.Getcards();
            foreach (IWebElement product in products)// Each WebElement loop
            {   //product - Specific section within the page
                if (expectedProduct.Contains(product.FindElement(productsPage.GetCardTitle()).Text))
                {
                    product.FindElement(productsPage.AddToCartButton()).Click();
                }
            }
            CheckOutPage checkoutpage = productsPage.CheckOutButton();

            IList<IWebElement> checkoutCardsElements = checkoutpage.GetSelectedCards();
            for (int i = 0; i < checkoutCardsElements.Count; i++)
            {
                actualProduct[i] = checkoutCardsElements[i].Text;
            }
            //Assert Array Comparison
            Assert.AreEqual(expectedProduct, actualProduct);

            //Act
            checkoutpage.CheckOutButton();
            PurchasePage purchasePage = new PurchasePage(driver.Value);
            purchasePage.GetDeliveryLocation().SendKeys("ind");
            purchasePage.WaitForPageDisplay();
            purchasePage.SelectLocation().Click();
            purchasePage.AgreeCheckbox().Click();
            purchasePage.PurchaseButton().Click();
            string actualPurchaseSuccessMessage = purchasePage.Success().Text;
            //Assert String Contains
            StringAssert.Contains("Success", actualPurchaseSuccessMessage);

        }

        public static IEnumerable<TestCaseData> AddTestData()
        {
            yield return new TestCaseData(GetDataParser().ExtractStringData("username"),GetDataParser().ExtractStringData("password"),GetDataParser().ExtractArrayData("products"));
            yield return new TestCaseData(GetDataParser().ExtractStringData("username"),
                GetDataParser().ExtractStringData("password"), GetDataParser().ExtractArrayData("products"));
        }
    }
}