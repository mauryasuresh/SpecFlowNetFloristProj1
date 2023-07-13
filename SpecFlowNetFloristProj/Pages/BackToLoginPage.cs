using Dynamitey.DynamicObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowNetFloristProj.Pages
{
   public class BackToLoginPage
    {
        private IWebDriver driver;

        public BackToLoginPage(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

            
        [FindsBy(How=How.XPath,Using = "/html/body/div[4]/div/div/nav/ul/li[6]/a")]
        private IWebElement FlowerSection;

        [FindsBy(How = How.XPath, Using = "/html/body/div[4]/div/div/nav/ul/li[6]/div/ul[2]/li[1]/ul/li[2]/a")]
        private IWebElement FlowerByColor;

        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/form/div[4]/div[7]/div[1]/div[1]")]
        private IWebElement SelectProduct;

        [FindsBy(How = How.XPath, Using = "//a[@id='ctl00_MainContent_Link']")]
        private IWebElement AddToBasket;

        [FindsBy(How = How.XPath, Using = "//div[@id='divAddressLogin']")]
        private IWebElement AddressLogin;

        [FindsBy(How = How.XPath, Using = "//a[@id='registerAccount']")]
        private IWebElement RegisterNewAcnt;

        [FindsBy(How = How.Id, Using = "nameId")]
        private IWebElement FName;

        [FindsBy(How = How.Id, Using = "lastname")]
        private IWebElement LName;

        [FindsBy(How = How.Id, Using = "emailId")]
        private IWebElement EmailId;

        [FindsBy(How = How.Id, Using = "passwordId")]
        private IWebElement Password;

        [FindsBy(How =How.Id,Using = "numberId")]
        private IWebElement Phone;

        [FindsBy(How = How.Id, Using = "GenderId")]
        private IWebElement Gender;


        [FindsBy(How = How.XPath, Using = "//a[@id='btnRegister']")]
        private IWebElement Register;


        [FindsBy(How = How.XPath, Using = "//a[@id='backToLogin']")]
        private IWebElement BackToLoginButton;




        public void ClickOnNewRegister()
        {
            FlowerSection.Click();
            FlowerByColor.Click();
            SelectProduct.Click();
            AddToBasket.Click();
            AddressLogin.Click();

        }


                public void Registernewacnt()
        {
            RegisterNewAcnt.Click();
        }

        public void EnterFirstName(string firstName)
        {
            FName.SendKeys(firstName);
        }
        public void EnterLasttName(string lasttName)
        {
            LName.SendKeys(lasttName);
        }

        public void EnterEmail(string email)
        {
            FName.SendKeys(email);
        }
        public void EnterPassword(string pwd)
        {
            FName.SendKeys(pwd);
        }
        public void EnterPhone(string phone)
        {
            FName.SendKeys(phone);
        }
        public void EnterGender(string gender)
        {
            SelectElement select = new SelectElement(Gender);
            select.SelectByText(gender);
        }
        //public void ClickOnRegister()
        //{
        //    Register.Click();
        //}

        public void ClickOnBackToLogin()
        {
            BackToLoginButton.Click();
        }

        public void TestRegistration(string fname,string lname,string email,string passwd,string phonenum,string gender )
        {
            EnterFirstName(fname);
            EnterLasttName(lname);
            EnterEmail(email);
            EnterPassword(passwd);
            EnterPhone(phonenum);
            EnterGender(gender);
            Register.Click();


        }

       
    }
}
