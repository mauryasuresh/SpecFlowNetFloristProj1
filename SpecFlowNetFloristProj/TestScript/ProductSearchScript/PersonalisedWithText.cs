using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecFlowNetFloristProj.Pages;
using SpecFlowNetFloristProj.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowNetFloristProj
{
    public class PersonalisedWithText : SeleniumUtility
    {
        IWebDriver driver;
        SearchHomePage searchflower;
        GiftWizardHomePage giftwizard;
        LoginPage LoginPage;
        CheckOutPage CheckOutPage;
        WebDriverWait wait;


        string productCode = "PER544";
        List<string> addresses = new List<string>()
        {
            "Parade hotel, 191 O R Tambo Parade , Marine Parade, North Beach, Durban, Kwa-zulu Natal",
            "Hillcrest Primary School, 5 Bollihope Cres, Mowbray, Cape Town, Western Cape",
            "34 Barbet Crescent, ,Midrand, Johannesburg, Gauteng",
            "Baragwanath Hospital, 26 Chris Hani Rd, Diepkloof, Soweto, Gauteng"
        };

        [Test]
        public void TextAdd1()
        {
            PersonalisedTextProduct(productCode, addresses[0], "Business");
        }

        [Test]
        public void TextAdd2()
        {
            PersonalisedTextProduct(productCode, addresses[1], "School");
        }

        [Test]
        public void TextAdd3()
        {
            PersonalisedTextProduct(productCode, addresses[2], "Residence");
        }

        [Test]
        public void TextAdd4()
        {
            PersonalisedTextProduct(productCode, addresses[3], "Hospital");
        }



        public void PersonalisedTextProduct(string productCode, string address, string addressType)
        {

            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("no-sandbox");
                ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                driver = new ChromeDriver(driverService, options, TimeSpan.FromMinutes(1));

                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://stage2.netflorist.co.za/");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);

                searchflower = new SearchHomePage(driver);
                giftwizard = new GiftWizardHomePage(driver);
                LoginPage = new LoginPage(driver);
                CheckOutPage = new CheckOutPage(driver);

                searchflower.searchProduct(productCode);
                searchflower.selectProduct();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                IWebElement personalizedButton = driver.FindElement(By.XPath("//*[@id='ctl00_MainContent_pnlPUAttributes']/div[1]"));

                wait.Until(ExpectedConditions.ElementToBeClickable(personalizedButton));

                string personalizedButtonText = personalizedButton.Text.Trim();

                if (personalizedButton.Enabled)  // Modify this condition based on the actual text of the active button
                {
                    personalizedButton.Click();
                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='collapsibleContent']/div[@class='clearFix']/div/input[@type='text']")));
                }

                List<IWebElement> textFields = driver.FindElements(By.XPath("//div[@class='collapsibleContent']/div[@class='clearFix']/div/input[@type='text']")).ToList();


                foreach (IWebElement textField in textFields)
                {
                    textField.Clear();
                    textField.SendKeys("Test");
                    Thread.Sleep(1000);
                }

                IWebElement confirm = driver.FindElement(By.Id("chkNFPersonalisedTermsAndCond"));
                confirm.Click();

                giftwizard.addBasket();
                LoginPage.ClickOnLogin("priyanka.shirsath27@gmail.com", "1234567");
                LoginPage.RecipientDetails("xxx", "xxx", "0712345678");

                string[] addressParts = address.Split(',');
                string addressLine1 = addressParts[0];
                string addressLine2 = addressParts[1];
                string city = addressParts[2];

                LoginPage.DeliveryDetails(addressLine1, addressLine2, city, addressType);

                //Handle unavailable popup

                try
                {
                    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='popDeliveryDetails']")));

                    IWebElement UnavailablePopUp = driver.FindElement(By.XPath("//*[@class='popDeliveryDetails']"));
                    string popText = UnavailablePopUp.Text;
                    //Console.WriteLine(popText);

                    if (popText.Contains("Not Available"))
                    {
                        IWebElement ProductText = driver.FindElement(By.XPath("//*[@id='pddErrorPrdName']"));
                        IWebElement ProductLocation = UnavailablePopUp.FindElement(By.XPath("//*[@id='pddErrorArea']/strong[1]"));

                        string prodLocationText = ProductLocation.Text;
                        UnavailablePopUp.FindElement(By.XPath("//*[@id='closeDeliverypop']")).Click();
                        Thread.Sleep(1500);

                        Console.WriteLine("Product is Unavailable: " + prodLocationText);
                    }
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Popup did not appear");
                }
                giftwizard.SelectDateFromCalendar("21"); //provide date 
                giftwizard.NextDeliveryType();
                LoginPage.AddToFinalBasket();
                Thread.Sleep(1000);
                CheckOutPage.ClickOnCheckOut();
                CheckOutPage.CheckOutProcess();

               // CheckOutPage.OnCostRef("Test");
               // CheckOutPage.OnPlaceOrder();
            }
            catch (Exception)
            {
                string screenshotFilePath = @"D:\Csharp\SpecFlowNetFloristProj\SpecFlowNetFloristProj\Screenshots\screenshot3.png"; 
                TakeScreenshot(driver, screenshotFilePath);
                Console.WriteLine("Screenshot captured: " + screenshotFilePath);
                //Console.WriteLine("Exception occurred: " + ex.Message);

            }
            finally
            {
              // driver.Quit();

            }
        }
    }

}





