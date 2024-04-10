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
        }
        public void LocatorById()
        {
            //Arrange
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedEmptyErrorMessage = "Empty username/password.";
            String expectedIncorrectErrorMessage = "Incorrect username/password.";
            String expectedHomePageTitle = "ProtoCommerce";

            //Act
            //Verify error message by clicking signin button without username and password
            driver.FindElement(By.Id("signInBtn")).Click();//Click Sign In
            String emptyErrorMessage = driver.FindElement((By.ClassName("alert"))).Text; //Empty username/password. Error Message
            TestContext.Progress.WriteLine(emptyErrorMessage); //Print Error Message
            Assert.That(emptyErrorMessage, Is.EqualTo(expectedEmptyErrorMessage)); //Assert
            Thread.Sleep(5000);

            //Verify error message by entering incorrect username and password
            driver.FindElement(By.Id("username")).SendKeys("RahulShetty"); //Enter Username
            driver.FindElement(By.Id("password")).SendKeys("1245398"); //Enter Password
            driver.FindElement(By.Id("signInBtn")).Click();//Click Sign In
            String incorrectErrorMessage = driver.FindElement((By.ClassName("alert"))).Text; //Incorrect username/password. Error Message
            TestContext.Progress.WriteLine(incorrectErrorMessage); //Print Error Message
            Assert.That(incorrectErrorMessage, Is.EqualTo(expectedIncorrectErrorMessage)); //Assert

            //Verify login is successful after entering correct username and password
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Id("password")).SendKeys("learning"); //Enter Password
            driver.FindElement(By.Id("signInBtn")).Click();//Click Sign In
            Thread.Sleep(5000);
            String actualHomePageTitle = driver.Title;
            TestContext.Progress.WriteLine(actualHomePageTitle); //Print Page Title
            Assert.That(actualHomePageTitle, Is.EqualTo(expectedHomePageTitle)); //Assert
        }
        [Test]
        public void LoginWithCorrectUsernameCorrectPassword()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String expectedHomePageTitle = "ProtoCommerce";
            //Verify login is successful after entering correct username and password
            driver.FindElement(By.Name("username")).Clear();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy"); //Enter Username
            driver.FindElement(By.Name("password")).SendKeys("learning"); //Enter Password
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click(); //Click Agree to terms and conditions checkbox
            driver.FindElement(By.Name("signin")).Click();//Click Sign In
            Thread.Sleep(5000);
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
            Thread.Sleep(3000);
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
            driver.Close();
        }
    }
}
