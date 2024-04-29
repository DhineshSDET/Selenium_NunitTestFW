using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using SeleniumTest.Utilities;
using WebFramework.PageObjects;

namespace SeleniumTest
{
    [Parallelizable(ParallelScope.Children)]
    public class LoginTest : BaseTest
    {
        private String expectedEmptyErrorMessage = "Empty username/password.";
        private String expectedIncorrectErrorMessage = "Incorrect username/password.";
        private String expectedHomePageTitle = "ProtoCommerce";
        private String expectedUrl = "https://rahulshettyacademy.com/documents-request";

        [Test]
        public void LoginPageWebElementTest()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            Thread.Sleep(8000);
            bool userNameDisplayed = driver.Value.FindElement(By.XPath("//input[@id='username']")).Displayed;
            bool passwordDisplayed = driver.Value.FindElement(By.XPath("//input[@id='password']")).Displayed;
            bool termsCheckboxDisplayed = driver.Value.FindElement(By.XPath("//input[@id='terms']")).Displayed;
            bool isAdminRadioButtonDisplayed = driver.Value.FindElement(By.XPath("//span[normalize-space()='Admin']")).Displayed;
            bool isUserRadioButtonDisplayed = driver.Value.FindElement(By.XPath("//span[normalize-space()='User']")).Displayed;
            bool isLoginAsDisplayed = driver.Value.FindElement(By.XPath("//select[@class='form-control']")).Displayed;
            bool isAllCondition = userNameDisplayed && passwordDisplayed && termsCheckboxDisplayed && isAdminRadioButtonDisplayed && isUserRadioButtonDisplayed && isLoginAsDisplayed;
            Assert.That(isAllCondition, Is.True);
        }
        [Test]
        public void LoginWithCorrectUsernameCorrectPassword()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //Verify login is successful after entering correct username and password
            LoginPage loginPage = new LoginPage(GetDriver());
            ProductsPage productsPage = loginPage.ValidLogin("rahulshettyacademy", "learning");
            productsPage.WaitForPageDisplay();
            String actualHomePageTitle = driver.Value.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void LoginWithUsernameWithoutPassword()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //Verify error message by clicking signin button with username and without password
            driver.Value.FindElement(By.Name("username")).SendKeys("RahulShetty"); //Enter Username
            driver.Value.FindElement(By.Name("signin")).Click();//Click Sign In
            WebDriverWait wait = new WebDriverWait(driver.Value,TimeSpan.FromMilliseconds(4));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(
                   driver.Value.FindElement(By.XPath("//input[@name='signin']")),"Sign In"));//Implicit wait
            String emptyErrorMessage = driver.Value.FindElement((By.ClassName("alert"))).Text; //Empty username/password. Error Message
            TestContext.Progress.WriteLine(emptyErrorMessage); //Print Error Message
            Assert.That(emptyErrorMessage, Is.EqualTo(expectedEmptyErrorMessage)); //Assert
        }
        [Test]
        public void LoginWithoutUsernameWithPassword()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //Verify error message by clicking signin button without username and with password
            driver.Value.FindElement(By.Name("username")).Clear();
            driver.Value.FindElement(By.Name("password")).SendKeys("1245398"); //Enter Password
            driver.Value.FindElement(By.Name("signin")).Click();//Click Sign In
            String emptyErrorMessage = driver.Value.FindElement((By.ClassName("alert"))).Text; //Empty username/password. Error Message
            TestContext.Progress.WriteLine(emptyErrorMessage); //Print Error Message
            Assert.That(emptyErrorMessage, Is.EqualTo(expectedEmptyErrorMessage)); //Assert
        }
        [Test]
        public void LoginWithoutUsernameWithoutPassword()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //Verify error message by clicking signin button without username and without password
            driver.Value.FindElement(By.Name("signin")).Click();//Click Sign In
            String emptyErrorMessage = driver.Value.FindElement((By.ClassName("alert"))).Text; //Empty username/password. Error Message
            TestContext.Progress.WriteLine(emptyErrorMessage); //Print Error Message
            Assert.That(emptyErrorMessage, Is.EqualTo(expectedEmptyErrorMessage)); //Assert
        }
        [Test]
        public void LoginWithIncorrectUsernameCorrectPassword()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //Verify error message by entering incorrect username and correct password
            driver.Value.FindElement(By.Name("username")).SendKeys("RahulShetty"); //Enter Username
            driver.Value.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            driver.Value.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(3000);//Hard Sleep
            String incorrectErrorMessage = driver.Value.FindElement((By.ClassName("alert"))).Text; //Incorrect username/password. Error Message
            TestContext.Progress.WriteLine(incorrectErrorMessage); //Print Error Message
            Assert.That(incorrectErrorMessage, Is.EqualTo(expectedIncorrectErrorMessage)); //Assert
        }
        [Test]
        public void LoginWithCorrectUsernameIncorrectPassword()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //Verify error message by entering correct username and incorrect password
            driver.Value.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.Value.FindElement(By.Name("password")).SendKeys("121345"); //Enter Password
            driver.Value.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(3000);
            String incorrectErrorMessage = driver.Value.FindElement((By.ClassName("alert"))).Text; //Incorrect username/password. Error Message
            TestContext.Progress.WriteLine(incorrectErrorMessage); //Print Error Message
            Assert.That(incorrectErrorMessage, Is.EqualTo(expectedIncorrectErrorMessage)); //Assert
        }
        [Test]
        public void LoginAsStudent()
        {
            Thread.Sleep(8000);
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Value.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.Value.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            IWebElement dropdownElement = driver.Value.FindElement(By.CssSelector("select.form-control"));
            SelectElement selectElement = new SelectElement(dropdownElement);
            selectElement.SelectByText("Student");//User Dropdown
            driver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click(); //Click Agree to terms and conditions checkbox
            driver.Value.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Value.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void LoginAsTeacher()
        {
            Thread.Sleep(8000);
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Value.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.Value.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            IWebElement dropdownElement = driver.Value.FindElement(By.CssSelector("select.form-control"));
            SelectElement selectElement = new SelectElement(dropdownElement);
            selectElement.SelectByText("Teacher");//User Dropdown
            driver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click(); //Click Agree to terms and conditions checkbox
            driver.Value.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Value.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void LoginAsConsultant()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Value.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.Value.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            IWebElement dropdownElement = driver.Value.FindElement(By.CssSelector("select.form-control"));
            SelectElement selectElement = new SelectElement(dropdownElement);
            selectElement.SelectByText("Consultant");//User Dropdown
            driver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click(); //Click Agree to terms and conditions checkbox
            driver.Value.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Value.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void LoginAsAdmin()
        {
            Thread.Sleep(8000);
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Value.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.Value.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            IList<IWebElement> radioList = driver.Value.FindElements(By.CssSelector("input[name='radio']"));

            foreach (IWebElement radioLst in radioList)
            {
                if (radioLst.GetAttribute("value").Equals("Admin"))
                {
                    radioLst.Click();
                }
            }

            driver.Value.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Value.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void LoginAsUser()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Value.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.Value.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            IList<IWebElement> radioList = driver.Value.FindElements(By.CssSelector("input[name='radio']"));

            foreach (IWebElement radioLst in radioList)
            {
                if (radioLst.GetAttribute("value").Equals("user"))
                {
                    radioLst.Click();
                    Thread.Sleep(2000);
                    //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(4)); //Explicitly wait for WebElement to present
                    //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@id='okayBtn']")));
                    driver.Value.FindElement(By.CssSelector("#okayBtn")).Click();
                }
            }
            driver.Value.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(2000);
            String actualHomePageTitle = driver.Value.Title;
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle));
        }
        [Test]
        public void FreeAccessToInterviewQuesLinkText()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            IWebElement freeLinkText = driver.Value.FindElement((By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material")));
            String actualUrl = freeLinkText.GetAttribute("href");
            TestContext.Progress.WriteLine(actualUrl); //Print Url
            Assert.That(actualUrl, Is.EqualTo(expectedUrl));//Assert
            freeLinkText.Click();
        }
    }
}
