using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowNetFloristProj.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowNetFloristProj.StepDefinitions
{
    [Binding]
    public class LoginStepDef
    {
        private IWebDriver driver;
        LoginPage loginPage;



        public LoginStepDef(IWebDriver driver)
        {
            this.driver = driver;
            loginPage = new LoginPage(driver);
        }
       
        [Given(@"User is on the login page")]
        public void GivenUserIsOnLoginPage()
        {
          
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.netflorist.co.za/"); // Replace with your application's login URL
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
          
              
        }

        [When(@"User enters valid username and password")]
        public void WhenUserEntersValidUsernameAndPassword()
        {
            // Enter valid username and password
            //driver.FindElement(By.Id("divSignIn")).Click();
            //driver.FindElement(By.Id("tbUserName")).SendKeys("priyanka.shirsath27@gmail.com");
            //driver.FindElement(By.Id("tbPassword")).SendKeys("1234567");

            
            loginPage.ClickOnSignIn();
            loginPage.EnterUserNameAndPassWord("priyanka.shirsath27@gmail.com" , "1234567");
           
        
        
        }

        [When(@"User enters invalid username and/or password")]
       

        [When(@"User clicks on the login button")]
        public void WhenUserClicksOnLoginButton()
        {

            //  driver.FindElement(By.Id("iLinkLogin1")).Click(); 
            loginPage.ClickLogin();
        }

        [Then(@"User should be logged in successfully")]
        public void ThenUserShouldBeLoggedInSuccessfully()
        {
            string ActualPageTitle = driver.Title;
            Console.WriteLine();
            string ExpectedPageTitle = "NetFlorist";
            Assert.AreEqual(ExpectedPageTitle, ActualPageTitle);
        }
    



    }
  


}