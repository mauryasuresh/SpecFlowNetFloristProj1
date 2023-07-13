
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowNetFloristProj
{
    public class GiftWizard
    {
        IWebDriver driver;

        public object ExpectedConditions { get; private set; }

        [Test]
        public void TestGiftWizard()

        {

            string outputPath = "output.txt";
            StreamWriter writer = new StreamWriter(outputPath);

            Console.SetOut(writer);

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            // driver.Navigate().GoToUrl("https://stage2.netflorist.co.za/");
            driver.Navigate().GoToUrl("https://www.netflorist.co.za/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(90);

            //Occassion

            //List<string> ids = new List<string>() {"30970","28704","30898","31123","28982","28972","29088", "29107", "28064","31142", "bramley",
            //                                      "27924","30912","28941","27924","31281","31103","30721","30568",
            //                                      "29982","33423","29534","37826","68127","28536","30200","65182","31507","26917","33732","31448" };

            List<string> ids = new List<string>() { "31103" };
            foreach (string id in ids)
            {

                IWebElement Occasion = driver.FindElement(By.Id("txtOcc"));
                Occasion.Click();

                List<IWebElement> OccasionList = driver.FindElements(By.XPath("//div[@class='occasionList']/div")).ToList();
                foreach (IWebElement occasions in OccasionList)
                {
                    writer.WriteLine(occasions.Text);

                }
                OccasionList[1].Click();



                //Suburbs

                IWebElement SuburbText = driver.FindElement(By.Id("txtPlace"));
                SuburbText.Click();
                IWebElement Suburbs = driver.FindElement(By.Id("txtSuburb"));
                Suburbs.Clear();
                Suburbs.SendKeys(id);
                Thread.Sleep(1500);
                //driver.FindElement(By.XPath("/html/body/ul/li/a")).Click();

                Suburbs.SendKeys(Keys.Down);

                Suburbs.SendKeys(Keys.Enter);

                IWebElement SuburbType = driver.FindElement(By.XPath("//div[@class='addressList clearFix']/label[1]"));
                SuburbType.Click();

                IWebElement SuburbSave = driver.FindElement(By.Id("saveSuburb"));
                SuburbSave.Click();
                driver.FindElement(By.Id("txtSelectDate")).Click();

                IWebElement Date = driver.FindElement(By.XPath("//div[@id='ui-datepicker-div']/table/tbody/tr[2]/td[3]"));
                Date.Click();

                //stage2      
                // IWebElement applyButton = driver.FindElement(By.XPath("//button[@class='homeSprite pddButton btnNext']"));
                IWebElement FindNow = driver.FindElement(By.Id("_btnApply"));
                FindNow.Click();
                //Thread.Sleep(1000);

                Thread.Sleep(1500);

                List<IWebElement> Productlist = driver.FindElements(By.XPath("//div[@class ='ProductBox']")).ToList();


                //testing each product

                for (int i = 0; i < Productlist.Count; i++)
                {
                    Productlist[i].Click();

                    //Add to basket

                    IWebElement AddToBasket = driver.FindElement(By.Id("ctl00_MainContent_Link"));
                    AddToBasket.Click();
                    Thread.Sleep(1000);

                    //Recipient Info

                    IWebElement FName = driver.FindElement(By.Id("fName"));
                    FName.SendKeys("xyz");
                    IWebElement LName = driver.FindElement(By.Id("lName"));
                    LName.SendKeys("abc");
                    IWebElement Phone = driver.FindElement(By.Id("telNo"));
                    Phone.SendKeys("0712345678");

                    IWebElement Street = driver.FindElement(By.Id("strtNameNo"));
                    Street.SendKeys("xxx");
                    IWebElement Address = driver.FindElement(By.Id("ddlAddressType"));
                    Address.Click();
                    SelectElement AddSelect = new SelectElement(Address);
                    AddSelect.SelectByText("Residence");

                    IWebElement Apartment = driver.FindElement(By.Id("aprtBldng"));
                    Apartment.SendKeys("xxxx");
                    IWebElement Suburbs2 = driver.FindElement(By.Id("suburb"));
                    Suburbs2.SendKeys(id);

                    List<IWebElement> SuburbsList2 = driver.FindElements(By.XPath("/html/body/ul/li")).ToList();

                    SuburbsList2[0].Click();

                    IWebElement NextDelDate = driver.FindElement(By.XPath("//button[@class='homeSprite pddButton btnNext']"));
                    NextDelDate.Click();
                    Thread.Sleep(1000);

                    try
                    {

                        IWebElement NextDateSelect = driver.FindElement(By.XPath("//div[@id='pddDatePicker']/div/table/tbody/tr[2]/td[4]"));

                        NextDateSelect.Click();
                    }
                    catch (StaleElementReferenceException)
                    {
                        driver.FindElement(By.XPath("//div[@id='pddDatePicker']/div/table/tbody/tr[2]/td[4]")).Click();

                    }

                    IWebElement NextDelType = driver.FindElement(By.XPath("//button[@class='homeSprite pddButton btnNext']"));
                    NextDelType.Click();

                    string parentWindow = driver.CurrentWindowHandle;
                    List<string> windows = new List<string>(driver.WindowHandles);


                    writer.WriteLine(driver.Title);

                }
            }

        }


    }
}
