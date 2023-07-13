using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowNetFloristProj.Pages
{
    public  class LoginPage
    {
        public IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public LoginPage()
        {
        }

        [FindsBy(How = How.Id,Using ="divSignIn")]
        private IWebElement SignIn;

        public void ClickOnSignIn()
        {
            SignIn.Click();
        }

        [FindsBy(How = How.Id, Using = "tbUserName")]
        private IWebElement UserName;


        [FindsBy(How = How.Id, Using = "tbPassword")]
        private IWebElement UserPass;

        public void EnterUserNameAndPassWord(string userName,string password)
        {
            UserName.SendKeys(userName);
            UserPass.SendKeys(password);

         }
        [FindsBy(How = How.Id, Using = "iLinkLogin1")]
        private IWebElement btnGo;


        public void ClickLogin()
        {
            btnGo.Click();
        }


       //Login After add to basket

        [FindsBy(How = How.XPath, Using = "//div[@id='divAddressLogin']")]
        private IWebElement AddressLogin;

        [FindsBy(How = How.Id, Using = "loginId")]
        private IWebElement EmailFIeld;

        [FindsBy(How = How.Id, Using = "loginPwd")]
        private IWebElement PasswdFIeld;

        [FindsBy(How = How.Id, Using = "btnLogin")]
        private IWebElement LoginButton;

        public void ClickOnLogin(string emailid, string pwd)
        {
            AddressLogin.Click();
            EmailFIeld.SendKeys(emailid);
            PasswdFIeld.SendKeys(pwd);
            LoginButton.Click();

        }


        [FindsBy(How = How.Id, Using = "fName")]
        private IWebElement FirstName;

        [FindsBy(How = How.Id, Using = "lName")]
        private IWebElement LastName;

        [FindsBy(How = How.Id, Using = "telNo")]
        private IWebElement Phone;

        public void RecipientDetails(string fname, string lname, string phone)
        {
            FirstName.SendKeys(fname);
            LastName.SendKeys(lname);
            Phone.SendKeys(phone);
           
        }

        [FindsBy(How = How.Id, Using = "strtNameNo")]
        private IWebElement Street;

        [FindsBy(How = How.Id, Using = "ddlAddressType")]
        private IWebElement Address;



        [FindsBy(How = How.Id, Using = "aprtBldng")]
        private IWebElement Apartment;

        [FindsBy(How = How.Id, Using = "suburb")]
        private IWebElement Suburb;

        [FindsBy(How = How.XPath, Using = "//button[@class='homeSprite pddButton btnNext']")]
        private IWebElement NextDelDate;
        public void DeliveryDetails(string strtName,string apartment ,string suburb , string addressType)
        {
            Street.SendKeys(strtName);
            Address.Click();
            SelectElement AddSelect = new SelectElement(Address);
            AddSelect.SelectByText(addressType);
            Apartment.SendKeys(apartment);
            Suburb.SendKeys(suburb);
           
            List<IWebElement> SuburbsList = driver.FindElements(By.XPath("/html/body/ul/li")).ToList();

            SuburbsList[0].Click();
            NextDelDate.Click();
            Thread.Sleep(1000);

        }
         public void clearRecipientDetails()
        {
            FirstName.Clear();
            LastName.Clear();
            Phone.Clear();
        }

        public void clearDeliveryDetails()
        {
            Street.Clear();
            Apartment.Clear();
            Suburb.Clear();
        }

        [FindsBy(How = How.Id, Using = "endPDD")]
        IWebElement AddFinalBasket;

        public void AddToFinalBasket()
        {
            AddFinalBasket.Click();
        }

        // Checkout

    }
}
