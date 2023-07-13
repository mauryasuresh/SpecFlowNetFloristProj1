using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecFlowNetFloristProj.Pages;
using SpecFlowNetFloristProj.Utils;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace SpecFlowNetFloristProj
{
    public class ProductBaseClass : SeleniumUtility
    {
        IWebDriver driver;
        SearchHomePage searchflower;
        GiftWizardHomePage giftwizard;
        LoginPage LoginPage;
        CheckOutPage CheckOutPage;
        WebDriverWait wait;

        public void ExecuteScriptForAddress(string productCode, string address, string addressType)
        {
            try
            {
                SeleniumUtility utility = new SeleniumUtility();
                driver = utility.SetUp("Chrome", "https://stage2.netflorist.co.za/");

                searchflower = new SearchHomePage(driver);
                giftwizard = new GiftWizardHomePage(driver);    
                LoginPage = new LoginPage(driver);
                CheckOutPage = new CheckOutPage(driver);

                searchflower.searchProduct(productCode);
                searchflower.selectProduct();
                giftwizard.addBasket();
                LoginPage.ClickOnLogin("priyanka.shirsath27@gmail.com", "1234567");
                // LoginPage.ClickOnLogin("netfloristtest", "LaurenA");
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
                        Thread.Sleep(1000);

                        Console.WriteLine("Product is Unavailable: " +prodLocationText);
                    }
                }
                catch (NoSuchElementException)
                {
                    // Popup did not appear, continue with the rest of the code
                }
                giftwizard.SelectDateFromCalendar("30");
                giftwizard.NextDeliveryType();
                LoginPage.AddToFinalBasket();
                Thread.Sleep(1000);
                CheckOutPage.ClickOnCheckOut();
                CheckOutPage.CheckOutProcess();

                //CheckOutPage.OnCostRef("Test");
                //CheckOutPage.OnPlaceOrder();

            }
            catch (Exception)
            {
                string screenshotFilePath = @"D:\AutoTesting\SpecFlowNetFloristProj\SpecFlowNetFloristProj\Screenshots\screenshot2.png";  // Provide the desired file path
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
