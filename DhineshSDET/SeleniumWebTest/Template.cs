using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTest
{
    public class ProtoCommerceShop
    {
        private IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            //WebDriverManager - Call , check browser version and get correct exe to run
            //chromedriver.exe on chrome browser
            //geckodriver.exe on firefox browser

            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            //driver = new EdgeDriver(); //object for Edge browser

            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //driver = new FirefoxDriver(); //object for Firefox browser

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver(); //object for chrome browser

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3); // Implicit wait

            driver.Manage().Window.Maximize(); //maximize browser
            //driver.Manage().Window.FullScreen(); //Full screen browser
            //driver.Manage().Window.Minimize(); //minimize browser
        }

        [Test]
        public void LoginXpathCssLocatorTest()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedHomePageTitle = "LoginPage Practise | Rahul Shetty Academy";
            bool userNameDisplayed = driver.FindElement(By.XPath("//input[@id='username']")).Displayed;
            bool passwordDisplayed = driver.FindElement(By.XPath("//input[@id='password']")).Displayed;
            bool termsCheckboxDisplayed = driver.FindElement(By.XPath("//input[@id='terms']")).Displayed;
            bool isAdminRadioButtonDisplayed = driver.FindElement(By.XPath("//span[normalize-space()='Admin']")).Displayed;
            bool isUserRadioButtonDisplayed = driver.FindElement(By.XPath("//span[normalize-space()='User']")).Displayed;
            bool isLoginAsDisplayed = driver.FindElement(By.XPath("//select[@class='form-control']")).Displayed;
            bool isAllCondition = userNameDisplayed && passwordDisplayed && termsCheckboxDisplayed && isAdminRadioButtonDisplayed && isUserRadioButtonDisplayed && isLoginAsDisplayed;
            Assert.That(isAllCondition, Is.True);
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));

        }
        [Test]
        public void Login() 
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            
            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
        }
        [Test]
        public void HomePage()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedHomePageTitle = "ProtoCommerce";
            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(3000);
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));

        }
        [TearDown]
        public void StopBrowser()
        {
            driver.Close(); // Current instance window is closed
            //driver.Quit(); // all windows are closed
        }
    }
}
