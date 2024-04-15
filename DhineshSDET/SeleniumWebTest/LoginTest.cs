using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace SeleniumTest
{
    public class LoginTest
    {
        private IWebDriver driver;
        public String GetUrl = "https://rahulshettyacademy.com/loginpagePractise/";
        private String expectedEmptyErrorMessage = "Empty username/password.";
        private String expectedIncorrectErrorMessage = "Incorrect username/password.";
        private String expectedHomePageTitle = "ProtoCommerce";
        private String expectedUrl = "https://rahulshettyacademy.com/documents-request";

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
        [Test]
        public void LoginPageWebElementTest()
        {
            bool userNameDisplayed = driver.FindElement(By.XPath("//input[@id='username']")).Displayed;
            bool passwordDisplayed = driver.FindElement(By.XPath("//input[@id='password']")).Displayed;
            bool termsCheckboxDisplayed = driver.FindElement(By.XPath("//input[@id='terms']")).Displayed;
            bool isAdminRadioButtonDisplayed = driver.FindElement(By.XPath("//span[normalize-space()='Admin']")).Displayed;
            bool isUserRadioButtonDisplayed = driver.FindElement(By.XPath("//span[normalize-space()='User']")).Displayed;
            bool isLoginAsDisplayed = driver.FindElement(By.XPath("//select[@class='form-control']")).Displayed;
            bool isAllCondition = userNameDisplayed && passwordDisplayed && termsCheckboxDisplayed && isAdminRadioButtonDisplayed && isUserRadioButtonDisplayed && isLoginAsDisplayed;
            Assert.That(isAllCondition, Is.True);
        }
        [Test]
        public void LoginWithCorrectUsernameCorrectPassword()
        {
            //Verify login is successful after entering correct username and password
            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click(); //Click Agree to terms and conditions checkbox
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void LoginWithUsernameWithoutPassword()
        {
            //Verify error message by clicking signin button with username and without password
            driver.FindElement(By.Name("username")).SendKeys("RahulShetty"); //Enter Username
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromMilliseconds(4));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(
                   driver.FindElement(By.XPath("//input[@name='signin']")),"Sign In"));//Implicit wait
            String emptyErrorMessage = driver.FindElement((By.ClassName("alert"))).Text; //Empty username/password. Error Message
            TestContext.Progress.WriteLine(emptyErrorMessage); //Print Error Message
            Assert.That(emptyErrorMessage, Is.EqualTo(expectedEmptyErrorMessage)); //Assert
        }
        [Test]
        public void LoginWithoutUsernameWithPassword()
        {
            //Verify error message by clicking signin button without username and with password
            driver.FindElement(By.Name("username")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("1245398"); //Enter Password
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            String emptyErrorMessage = driver.FindElement((By.ClassName("alert"))).Text; //Empty username/password. Error Message
            TestContext.Progress.WriteLine(emptyErrorMessage); //Print Error Message
            Assert.That(emptyErrorMessage, Is.EqualTo(expectedEmptyErrorMessage)); //Assert
        }
        [Test]
        public void LoginWithoutUsernameWithoutPassword()
        {
            //Verify error message by clicking signin button without username and without password
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            String emptyErrorMessage = driver.FindElement((By.ClassName("alert"))).Text; //Empty username/password. Error Message
            TestContext.Progress.WriteLine(emptyErrorMessage); //Print Error Message
            Assert.That(emptyErrorMessage, Is.EqualTo(expectedEmptyErrorMessage)); //Assert
        }
        [Test]
        public void LoginWithIncorrectUsernameCorrectPassword()
        {
            //Verify error message by entering incorrect username and correct password
            driver.FindElement(By.Name("username")).SendKeys("RahulShetty"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(3000);//Hard Sleep
            String incorrectErrorMessage = driver.FindElement((By.ClassName("alert"))).Text; //Incorrect username/password. Error Message
            TestContext.Progress.WriteLine(incorrectErrorMessage); //Print Error Message
            Assert.That(incorrectErrorMessage, Is.EqualTo(expectedIncorrectErrorMessage)); //Assert
        }
        [Test]
        public void LoginWithCorrectUsernameIncorrectPassword()
        {
            //Verify error message by entering correct username and incorrect password
            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("121345"); //Enter Password
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(3000);
            String incorrectErrorMessage = driver.FindElement((By.ClassName("alert"))).Text; //Incorrect username/password. Error Message
            TestContext.Progress.WriteLine(incorrectErrorMessage); //Print Error Message
            Assert.That(incorrectErrorMessage, Is.EqualTo(expectedIncorrectErrorMessage)); //Assert
        }
        [Test]
        public void LoginAsStudent()
        {
            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            IWebElement dropdownElement = driver.FindElement(By.CssSelector("select.form-control"));
            SelectElement selectElement = new SelectElement(dropdownElement);
            selectElement.SelectByText("Student");//User Dropdown
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click(); //Click Agree to terms and conditions checkbox
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void LoginAsTeacher()
        {

            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            IWebElement dropdownElement = driver.FindElement(By.CssSelector("select.form-control"));
            SelectElement selectElement = new SelectElement(dropdownElement);
            selectElement.SelectByText("Teacher");//User Dropdown
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click(); //Click Agree to terms and conditions checkbox
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void LoginAsConsultant()
        {
            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            IWebElement dropdownElement = driver.FindElement(By.CssSelector("select.form-control"));
            SelectElement selectElement = new SelectElement(dropdownElement);
            selectElement.SelectByText("Consultant");//User Dropdown
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click(); //Click Agree to terms and conditions checkbox
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void LoginAsAdmin()
        {
            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            IList<IWebElement> radioList = driver.FindElements(By.CssSelector("input[name='radio']"));

            foreach (IWebElement radioLst in radioList)
            {
                if (radioLst.GetAttribute("value").Equals("Admin"))
                {
                    radioLst.Click();
                }
            }

            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void LoginAsUser()
        {
            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            IList<IWebElement> radioList = driver.FindElements(By.CssSelector("input[name='radio']"));

            foreach (IWebElement radioLst in radioList)
            {
                if (radioLst.GetAttribute("value").Equals("user"))
                {
                    radioLst.Click();
                    Thread.Sleep(2000);
                    //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(4)); //Explicitly wait for WebElement to present
                    //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@id='okayBtn']")));
                    driver.FindElement(By.CssSelector("#okayBtn")).Click();
                }
            }
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void FreeAccessToInterviewQuesLinkText()
        {
            IWebElement freeLinkText = driver.FindElement((By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material")));
            String actualUrl = freeLinkText.GetAttribute("href");
            TestContext.Progress.WriteLine(actualUrl); //Print Url
            Assert.That(actualUrl, Is.EqualTo(expectedUrl));//Assert
            freeLinkText.Click();
        }
        [TearDown]
        public void StopBrowser()
        {
            driver.Close(); // Current instance window is closed
            //driver.Quit(); // all windows are closed
        }
    }
}
