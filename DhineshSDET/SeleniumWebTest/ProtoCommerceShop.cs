using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTest
{
    public class ProtoCommerceShop
    {
        private IWebDriver driver;
        public String GetUrl = "https://rahulshettyacademy.com/loginpagePractise/";
        private String expectedLoginPageTitle = "LoginPage Practise | Rahul Shetty Academy";

        [SetUp]
        public void StartBrowser()
        {
            SelectBrowser("Chrome"); 
            driver.Manage().Window.Maximize(); //maximize browser
            driver.Url = GetUrl;
        }
        public void SelectBrowser(String browser)
        {
            switch (browser)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver(); //object for chrome browser
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver(); //object for Edge browser
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver(); //object for Firefox browser
                    break;
                default:
                    TestContext.Progress.WriteLine("Incorrect browser is mentioned");
                    break;
            }
        }
        public void EnterUserNameAndPassword(String userName, String password)
        {
            driver.FindElement(By.Name("username")).SendKeys(userName); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys(password); //Enter Password
        }
        [Test]
        public void LoginPageTest() 
        {
            EnterUserNameAndPassword("rahulshettyacademy", "learning");
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedLoginPageTitle));
        }
        [Test]
        public void HomePageTest()
        {
            driver.Url = GetUrl;
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
            driver.Url = GetUrl;
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
        [TearDown]
        public void StopBrowser()
        {
            driver.Close(); // Current instance window is closed
            //driver.Quit(); // all windows are closed
        }
    }
}
