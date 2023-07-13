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
    public class GiftWizardPage
    {
        private IWebDriver driver;
       
        public GiftWizardPage(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        
        //Occasion
        [FindsBy(How = How.XPath, Using = "//*[@id='txtOcc']")]
        private IWebElement Occasion;

        public void OccasionClick()
        {
            Occasion.Click();
            List<IWebElement> ListOfOcc = driver.FindElements(By.XPath("//div[@class='occasionList']/div")).ToList();
            foreach (IWebElement occasionsel in ListOfOcc)
            {
                Console.WriteLine(occasionsel.Text);
            }
            ListOfOcc[2].Click();
        }



        //Suburb
        [FindsBy(How = How.XPath, Using = "//input[@id='txtPlace']")]
        private IWebElement SuburbField;


        [FindsBy(How = How.XPath, Using = "//input[@id='txtSuburb']")]
        private IWebElement SuburbText;


        //Selecttion of suburbs
        //[FindsBy(How = How.XPath, Using = "/html/body/ul/li")]
        //    private IWebElement SuburbList;

        public void SuburbListSelect()
        {
            List<string> ids = new List<string>() { "29088" };

            foreach (string id in ids)
            {

                SuburbField.Click();
                SuburbText.SendKeys(id);
                Thread.Sleep(1000);

                SuburbText.SendKeys(Keys.Down);

                SuburbText.SendKeys(Keys.Enter);
                AddList.Click();
                SaveSuburb.Click();

            }
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='addressList clearFix']/label[1]")]
        private IWebElement AddList;

        [FindsBy(How = How.XPath, Using = "//div[@id='saveSuburb']")]
        private IWebElement SaveSuburb;


        //DateSelect
        [FindsBy(How = How.XPath, Using = "//input[@id='txtSelectDate']")]
        private IWebElement DateField;

        [FindsBy(How = How.XPath, Using = "//div[@id='ui-datepicker-div']/table/tbody/tr[2]/td[3]")]
        private IWebElement Date;

        public void DateSelection()
        {
            DateField.Click();
            Date.Click();
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='_btnApply']")]
        private IWebElement FindNow;

        public void ClickOnFind()
        {
            FindNow.Click();
            Thread.Sleep(1000);
        }

        public void SelectProduct()
        {

            List<IWebElement> Productlist = driver.FindElements(By.XPath("//div[@class ='ProductBox']")).ToList();

            for (int i = 0; i < Productlist.Count; i++)
            {
                Console.WriteLine(Productlist[i]);
                Productlist[i].Click();
                 AddToBasket();
                Thread.Sleep(1000);
            }
        }





        [FindsBy(How = How.XPath, Using = "//a[@id='ctl00_MainContent_Link']")]
        private IWebElement AddBasket;


        public void AddToBasket()
        {
            AddBasket.Click();
        }

        //Receipient details
        [FindsBy(How = How.XPath, Using = "//input[@id='fName']")]
        private IWebElement FName;

        [FindsBy(How = How.XPath, Using = "//input[@id='fName']")]
        private IWebElement LName;

        [FindsBy(How = How.XPath, Using = "//input[@id='fName']")]
        private IWebElement Mobile;

        [FindsBy(How = How.XPath, Using = "//input[@id='strtNameNo']")]
        private IWebElement StreetName;

        [FindsBy(How = How.Id, Using = "ddlAddressType")]
        private IWebElement AddressType;

        [FindsBy(How = How.XPath, Using = "//input[@id='aprtBldng']")]
        private IWebElement Apartment;

        [FindsBy(How = How.XPath, Using = "//input[@id='suburb']")]
        private IWebElement Suburb2;

        [FindsBy(How= How.XPath,Using = "//button[@class='homeSprite pddButton btnNext']")]
        private IWebElement NextDelBtn;


        [FindsBy(How = How.XPath, Using = "//div[@id='pddDatePicker']/div/table/tbody/tr[2]/td[4]")]
        private IWebElement NextDate;

        public void Details(string firstname, string lastname, string phone, string street, string addType, string appartment)
        {
            FName.SendKeys(firstname);
            LName.SendKeys(lastname);
            Mobile.SendKeys(phone);
            StreetName.SendKeys(street);
            AddressType.Click();
            SelectElement select = new SelectElement(AddressType);
            select.SelectByText(addType);
            Apartment.SendKeys(appartment);
            Suburb2.SendKeys("29088");
            List<IWebElement> SuburbsList2 = driver.FindElements(By.XPath("/html/body/ul/li")).ToList();
            SuburbsList2[1].Click();
            NextDelBtn.Click();

        }

        public void ClickOnNext()
        {
            NextDate.Click();
        }


        [FindsBy(How = How.XPath, Using = "//div[@id='pddDatePicker']/div/table/tbody/tr[5]/td[3]")]
        private IWebElement DateDelivery;

        [FindsBy(How = How.XPath, Using = "//div[@id='pddContainer']/div[6]/div[2]/button")]
        private IWebElement NextDelivery;

        public void NextDeliveryType()
        {
            DateDelivery.Click();
            try
            {
                NextDelivery.Click();

            }
            catch (StaleElementReferenceException e)
            {
                NextDelivery.Click();
            }

        }

        [FindsBy(How = How.Id, Using = "endPDD")]
        private IWebElement BasketAdd;
     

        public void BasketAddType()
        {
            BasketAdd.Click();
        }









    }






}
