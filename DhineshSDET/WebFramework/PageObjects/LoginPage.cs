using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WebFramework.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        //Constructor
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //Page Object Factory
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;
   
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private IWebElement checkBox;

        [FindsBy(How = How.Name, Using = "signin")]
        private IWebElement signIn;

        //Getters Encapsulation
        public IWebElement GetUserName()
        {
            return username;
        }
        public IWebElement GetPassword()
        {
            return password;
        }

        public ProductsPage ValidLogin(String userName, String passWord, bool isClick = true)
        {
            GetUserName().SendKeys(userName);
            GetPassword().SendKeys(passWord);
            GetCheckBox().Click();
            if(isClick==true)
                GetSignIn().Click();
            Thread.Sleep(3000);
            return new ProductsPage(driver);
        }
        public IWebElement GetCheckBox()
        {
            return checkBox;
        }
        public IWebElement GetSignIn()
        {
            return signIn;
        }

    }

}
