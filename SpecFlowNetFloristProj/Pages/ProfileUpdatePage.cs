using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SpecFlowNetFloristProj.StepDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowNetFloristProj.Pages
{
    public class ProfileUpdatePage
    {

        IWebDriver driver;
      
        public ProfileUpdatePage(IWebDriver driver)
        { 
            this.driver = driver;
            PageFactory.InitElements(driver, this);       
         }

        [FindsBy(How = How.Id, Using = "divMyAccount")]
        private IWebElement MyProfile;

        public void ClickOnMyProfile()
        {
            MyProfile.Click();
        }
        [FindsBy(How = How.XPath, Using = "/html/body/div[4]/form/div[1]/div/div[7]/div/div/a[7]")]
        private IWebElement SelectMyProfile;

        public void ClickOnSelectProfile()
        {
            SelectMyProfile.Click();
         }

        [FindsBy(How = How.Id,Using= "ctl00_MainContent_tbFirstname")]
        private IWebElement FNameUpdate;


        [FindsBy(How = How.Id, Using = "ctl00_MainContent_tbSurname")]
        private IWebElement LNameUpdate;

        public void UpdateInfo(string fname, string lname)
        {
            FNameUpdate.Clear();
            FNameUpdate.SendKeys(fname);
            LNameUpdate.Clear() ;
            LNameUpdate.SendKeys(lname);
           
        }
        [FindsBy(How = How.XPath, Using = "//input[@id='ctl00_MainContent_ImgSave']")]
        private IWebElement SaveBtn;

        public void ClickOnSaveBtn()
        {
            SaveBtn.Click();
        }

            }
    
}
