using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    public class LoginTest
    {
        private IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver(); //Chrome browser object instance
            driver.Manage().Window.Maximize(); //maximize browser
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); // Implicit wait
           /* WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(4)); //Explicitly wait for WebElement to present
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(
                driver.FindElement(By.XPath("//input[@name='signin']")), "Sign In"));*/
        }
        [Test]
        public void LoginWithCorrectUsernameCorrectPassword()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedHomePageTitle = "ProtoCommerce";

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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedEmptyErrorMessage = "Empty username/password.";

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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedEmptyErrorMessage = "Empty username/password.";

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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedEmptyErrorMessage = "Empty username/password.";

            //Verify error message by clicking signin button without username and without password
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            String emptyErrorMessage = driver.FindElement((By.ClassName("alert"))).Text; //Empty username/password. Error Message
            TestContext.Progress.WriteLine(emptyErrorMessage); //Print Error Message
            Assert.That(emptyErrorMessage, Is.EqualTo(expectedEmptyErrorMessage)); //Assert
        }
        [Test]
        public void LoginWithIncorrectUsernameCorrectPassword()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedIncorrectErrorMessage = "Incorrect username/password.";

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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedIncorrectErrorMessage = "Incorrect username/password.";

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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedHomePageTitle = "ProtoCommerce";

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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedHomePageTitle = "ProtoCommerce";

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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedHomePageTitle = "ProtoCommerce";

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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedHomePageTitle = "ProtoCommerce";

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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedHomePageTitle = "ProtoCommerce";
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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedUrl = "https://rahulshettyacademy.com/documents-request";

            IWebElement freeLinkText = driver.FindElement((By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material")));
            String actualUrl = freeLinkText.GetAttribute("href");
            TestContext.Progress.WriteLine(actualUrl); //Print Url
            Assert.That(actualUrl, Is.EqualTo(expectedUrl));//Assert
            freeLinkText.Click();

        }
        [TearDown]
        public void TearDownBrowser()
        {
            driver.Quit();
        }
    }
}
