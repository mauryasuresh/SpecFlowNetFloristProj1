using AutoItX3Lib;
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

namespace SpecFlowNetFloristProj.ProductSearchScript
{
    public class PerWithTextImage : SeleniumUtility
    {

        IWebDriver driver;
        SearchHomePage searchflower;
        GiftWizardHomePage giftwizard;
        LoginPage LoginPage;
        CheckOutPage CheckOutPage;
        WebDriverWait wait;
        

        string productCode = "PER3744";
        List<string> addresses = new List<string>()
        {
            "Parade hotel, 191 O R Tambo Parade , Marine Parade, North Beach, Durban, Kwa-zulu Natal",
            "Hillcrest Primary School, 5 Bollihope Cres, Mowbray, Cape Town, Western Cape",
            "34 Barbet Crescent, ,Midrand, Johannesburg, Gauteng",
            "Baragwanath Hospital, 26 Chris Hani Rd, Diepkloof, Soweto, Gauteng"
        };
        [Test]
        public void TextImage1()
        {
            PerProductWithTextImage(productCode, addresses[0], "Business");
        }
        [Test]
        public void TextImage2()
        {
            PerProductWithTextImage(productCode, addresses[1], "School");
        }
        [Test]
        public void TextImage3()
        {
            PerProductWithTextImage(productCode, addresses[2], "Residence");
        }
            [Test]
            public void TextImage4()
            {
                PerProductWithTextImage(productCode, addresses[3], "Hospital");

            }
       
        public void PerProductWithTextImage(string productCode, string address, string addressType)
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
                IWebElement personalizedButton = driver.FindElement(By.XPath("//*[@id='PTool_Attribut_Title']"));

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
                string imagePath = @"C:\Users\Priyanka Shirsath\Pictures\Screenshots\sample.png";

                List<IWebElement> UploadImage = driver.FindElements(By.XPath("//a[@title='Upload']")).ToList();

                if (UploadImage.Count > 0)
                {
                    for (int i = 0; i < UploadImage.Count && i < imagePath.Length; i++)
                    {
                        UploadImage[i].Click();

                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UploadImage"))).Click();

                        AutoItX3 autoIt = new AutoItX3();
                        autoIt.WinWait("Open", "", 10);
                        autoIt.ControlSetText("Open", "", "Edit1", imagePath);
                        autoIt.ControlClick("Open", "", "Button1");
                        Thread.Sleep(1000);

                        try
                        {
                            IWebElement previewImage = driver.FindElement(By.XPath("//*[@id='preview']/div/div[1]"));
                            IWebElement uploadButton = driver.FindElement(By.Id("uploadbutton"));

                            previewImage.Click();
                            uploadButton.Click();
                        }
                        catch (NoSuchElementException)
                        {
                            Console.WriteLine("Upload elements not found");
                        }

                        //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[2]/div[5]/div/div/a")));

                        IWebElement successPop = null;

                        try
                        {
                            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            var loaderMask = By.CssSelector(".loaderMask");
                            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(loaderMask));
                            successPop = driver.FindElement(By.XPath("//html/body/div[3]/div[2]/div[5]/div/div/a"));
                        }
                        catch (NoSuchElementException)
                        {
                            // Success pop button is not present
                        }

                        if (successPop != null && successPop.Displayed)
                        {
                            successPop.Click();
                            Thread.Sleep(1000);
                        }
                        IWebElement buttonPreview;
                        try
                        {
                            buttonPreview = driver.FindElement(By.Id("btnPreview"));
                        }
                        catch (Exception)
                        {
                            buttonPreview = null;
                        }
                        try
                        {
                            buttonPreview = driver.FindElement(By.Id("btnPreview"));
                        }
                        catch (NoSuchElementException)
                        {
                            // Preview button is not present
                        }

                        if (buttonPreview != null && buttonPreview.Displayed)
                        {
                            buttonPreview.Click();
                            Thread.Sleep(2000);
                            buttonPreview.SendKeys(Keys.Escape);
                            Console.WriteLine("Click");
                        }

                        IWebElement confirm = driver.FindElement(By.Id("chkNFPersonalisedTermsAndCond"));
                        confirm.Click();
                        Console.WriteLine("Performing preview and confirm action");

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
                        giftwizard.SelectDateFromCalendar("24"); //provide date 
                        giftwizard.NextDeliveryType();
                        LoginPage.AddToFinalBasket();
                        Thread.Sleep(1000);
                        CheckOutPage.ClickOnCheckOut();
                        CheckOutPage.CheckOutProcess();

                        // CheckOutPage.OnCostRef("Test");
                        // CheckOutPage.OnPlaceOrder();
                    }
                }
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
                 driver.Quit();

            }

        }

    }
}


