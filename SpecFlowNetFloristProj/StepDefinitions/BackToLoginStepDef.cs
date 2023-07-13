using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using SpecFlowNetFloristProj.Pages;
using Microsoft.Win32;

namespace SpecFlowNetFloristProj.StepDefinitions
{
    [Binding]
    public class BackToLoginStepDef
    {
        private readonly IObjectContainer objectContainer;
        private IWebDriver driver;
       

        public BackToLoginStepDef(IObjectContainer objectContainer) 
        {
            this.objectContainer = objectContainer;
            

        }
       
        [BeforeScenario]
        public void InitializeWebDriver()
        {
            this.driver = new ChromeDriver();
            objectContainer.RegisterInstanceAs<IWebDriver>(driver);
        }


        [Given(@"User is on home page")]
        
        public void GivenUserIsOnHomePage()
        {
            driver.Manage().Window.Maximize();   
            driver.Navigate().GoToUrl("https://stage4.netflorist.co.za/");
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(15);
        }

        
        [When(@"User add product to basket")]
        public void WhenUserAddProductToBasket()
        {
            

            IWebElement FlowerSection = driver.FindElement(By.XPath("/html/body/div[4]/div/div/nav/ul/li[6]/a"));
            FlowerSection.Click();
            Thread.Sleep(1000);
            IWebElement FlowerByColor = driver.FindElement(By.XPath("/html/body/div[4]/div/div/nav/ul/li[6]/div/ul[2]/li[1]/ul/li[2]/a"));
            FlowerByColor.Click();
            Thread.Sleep(1000);
            IWebElement SelectProduct = driver.FindElement(By.XPath("/html/body/div[2]/form/div[4]/div[7]/div[1]/div[1]"));
            SelectProduct.Click();
            IWebElement AddToBasket = driver.FindElement(By.XPath("//a[@id='ctl00_MainContent_Link']"));
            AddToBasket.Click();
            Thread.Sleep(1000);
            IWebElement DivAddLogin = driver.FindElement(By.XPath("//div[@id='divAddressLogin']"));
            DivAddLogin.Click();


        }
        
        [When(@"Redirect to recepient details page and click on register new account")]
        public void WhenRedirectToRecepientDetailsPageAndClickOnRegisterNewAccount()
        {
            
            IWebElement RegNewAcnt = driver.FindElement(By.XPath("//a[@id='registerAccount']"));
            RegNewAcnt.Click();

        }


        
        [Then(@"User Should see ""Back to login"" button")]
        public void ThenUserShouldSeeBackToLoginButton()
        {

            
            IWebElement BackToLoginButton = driver.FindElement(By.XPath("//a[@id='backToLogin']"));
            BackToLoginButton.Click();
            Thread.Sleep(1000);
            //Assert.IsTrue(BackToLoginButton.Displayed, "Back to Login button is not displayed");
        }

       
        [Then(@"User Should see ""Register"" button")]
        public void ThenUserShouldSeeRegisterButton()
        {
            
            WhenRedirectToRecepientDetailsPageAndClickOnRegisterNewAccount();
            driver.FindElement(By.Id("nameId")).SendKeys("lizaz");
            driver.FindElement(By.Id("lastname")).SendKeys("keys");
            driver.FindElement(By.Id("emailId")).SendKeys("lisa.keys12@gmail.com");
            driver.FindElement(By.Id("passwordId")).SendKeys("lisa23");
            driver.FindElement(By.Id("numberId")).SendKeys("7198665867");

            IWebElement gender = driver.FindElement(By.Id("GenderId"));
            SelectElement genderSelect = new SelectElement(gender);
            genderSelect.SelectByValue("Male");

            IWebElement registerButton = driver.FindElement(By.XPath("//a[@id='btnRegister']"));
            registerButton.Click();
            Thread.Sleep(1000);
           


           // Assert.IsTrue(registerButton.Displayed, "Register button is not displayed");
        }




    }
}
