    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using SpecFlowNetFloristProj.Pages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace SpecFlowNetFloristProj.StepDefinitions
    {
        [Binding]
        public class ProfUpdateStepDef 
        {
            
            private IWebDriver driver;
            LoginPage loginPage;
            ProfileUpdatePage profileUpdatePage;

            public ProfUpdateStepDef(IWebDriver driver)
            {
                this.driver = driver;
                loginPage = new LoginPage(driver);
                profileUpdatePage = new ProfileUpdatePage(driver);
            }



            [Given(@"User is logged in")]
            public void GivenUserIsLoggedIn()
            {
               // driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Url = "https://www.netflorist.co.za/";
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


                loginPage.ClickOnSignIn();
                loginPage.EnterUserNameAndPassWord("priyanka.shirsath27@gmail.com", "1234567");
                loginPage.ClickLogin();
              

            }

            [Given(@"User is on the profile update page")]
            public void GivenUserIsOnTheProfileUpdatePage()
            {

                profileUpdatePage.ClickOnMyProfile();
                profileUpdatePage.ClickOnSelectProfile();
                 Thread.Sleep(1000);
               

            }   

            [When(@"User enters valid profile information")]
            public void WhenUserEntersValidProfileInformation()
            {
                profileUpdatePage.UpdateInfo("Priya", "Verma");

                }

            [When(@"User clicks on the update button")]
            public void WhenUserClicksOnTheUpdateButton()
            {

                profileUpdatePage.ClickOnSaveBtn();
           
                Thread.Sleep(1000);
            }

            [Then(@"User should see a success message")]
            public void ThenUserShouldSeeASuccessMessage()
            {

                IAlert simpleAlert = driver.SwitchTo().Alert();
                //String alertText = simpleAlert.Text;   
                //Console.WriteLine("Alert text is  "+ alertText);
                simpleAlert.Accept();
                       
            }


    }
    }
