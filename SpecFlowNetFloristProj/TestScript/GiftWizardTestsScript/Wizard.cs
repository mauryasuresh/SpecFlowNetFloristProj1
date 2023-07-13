
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecFlowNetFloristProj.Pages;
using AventStack.ExtentReports;
using SpecFlowNetFloristProj.Utils;
using Org.BouncyCastle.Bcpg;
using AutoItX3Lib;

namespace SpecFlowNetFloristProj
{
    public class Wizard : SeleniumUtility
    {

        private ExtentReports extent;
        ExtentTest test = null;
        IWebDriver driver;
        GiftWizardHomePage ghomePage;


        [SetUp]
        public void Setup()
        {
            extent = ExtentManager.GetExtent();
        }

        [TearDown]
        public void TearDown()
        {
            extent.Flush();
            
        }

        [Test]
        [Parallelizable]
        public void GWizard()

        {
            try
            {
                test = extent.CreateTest("GWizard").Info("Test Started");
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                test.Log(Status.Info, "Browser Launched");
                driver.Navigate().GoToUrl("https://stage2.netflorist.co.za/");
                //  driver.Navigate().GoToUrl("https://www.netflorist.co.za/");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

                GiftWizardHomePage ghomePage = new GiftWizardHomePage(driver);

                //Occassion

                //List<string> ids = new List<string>() {"30970","28704","30898","31123","28982","  ","29088", "29107", "28064","31142", "bramley",
                //                                      "27924","30912","28941","27924","31281","31103","30721","30568",
                //                                      "29982","33423","29534","37826","68127","28536","30200","65182","31507","26917","33732","31448" };

                List<string> ids = new List<string>() { "Oakland" };

                foreach (string id in ids)
                {

                    ghomePage.OccasionClick();
                    ghomePage.SelectSuburbById(id);
                    ghomePage.SelectDate(new DateTime(2023, 6, 10));
                    ghomePage.FindItNow();

                    int totalPages = GetTotalPages();
                    Console.WriteLine(totalPages);
                    test.Log(Status.Info, "totalPages");
                    int currentPage = 1;

                    while (currentPage <= totalPages)
                    {

                        List<IWebElement> Productlist = driver.FindElements(By.XPath("//div[@class ='ProductBox']")).ToList();
                        Console.WriteLine(Productlist.Count());

                        //testing each product
                        string productCode;

                        for (int i = 0; i < Productlist.Count; i++)
                        {
                            IWebElement product = Productlist[5];

                            //Out of stock(sold out)
                            IWebElement deliveryElement = product.FindElement(By.ClassName("NextDeliveryPhrase"));
                            string deliveryStatus = deliveryElement.Text;

                            IWebElement nameOfProduct = driver.FindElement(By.XPath(".//a[2]"));

                            if (deliveryStatus.Equals("Out of stock"))
                            {
                                Console.WriteLine("Product is out of stock " + nameOfProduct.Text + deliveryStatus);
                                test.Log(Status.Info, "Product is out of stock " + nameOfProduct.Text + deliveryStatus);
                                continue;

                            }

                            //click on each product
                            Actions actions = new Actions(driver);
                            actions.MoveToElement(product).Click().Perform();
                            Thread.Sleep(1000);

                            //Add to basket

                            IWebElement ProductName = driver.FindElement(By.Id("productName"));
                            string ProductText = ProductName.Text;
                            Console.Write("ProductText =");
                            test.Log(Status.Info, ProductName.Text + "=");

                            IWebElement element = driver.FindElement(By.XPath("//span[@id='CurrentProductCode']"));
                            productCode = element.Text;
                            Console.WriteLine(productCode);
                            test.Log(Status.Info, productCode);

                           // check personalized products
                                
                           if(productCode.StartsWith("CPER"))
                            {
                                ProcessPersonalizedProduct("CPER", false);
                            }
                            else if(productCode.StartsWith("PER"))
                            {
                                ProcessPersonalizedProduct("PER_WITH_IMAGE", true);
                            }
                            else if (productCode.StartsWith("PER"))
                            {
                                ProcessPersonalizedProduct("PER_WITH_IMAGE", false);
                            }

                            ghomePage.addBasket();

                            //Recipient Info

                            ghomePage.FillInfo("xxx", "xxx", "0712345678", "xxx", "xxx", id);
                            test.Log(Status.Pass, "Recipient Information filled");

                            //select date 

                            SelectDateFromCalendar(2,6);

                            ghomePage.NextDeliveryType();

                            driver.Navigate().Back();
                            Thread.Sleep(3000);
                            Productlist = driver.FindElements(By.XPath("//div[@class ='ProductBox']")).ToList();
                        }
                        //move to next page
                        if (currentPage < totalPages)
                        {
                            SeleniumUtility.GoToNextPage(driver);
                            currentPage++;

                        }
                    }
                }

            }
            //ss
            catch (Exception)
            {
                string screenshotPath = @"D:\Csharp\SpecFlowNetFloristProj\SpecFlowNetFloristProj\Screenshots\screenshot.png";
                SeleniumUtility.TakeScreenshot(driver, screenshotPath);
                test.Log(Status.Fail, "Test Failed",
                    MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                test.Log(Status.Fail, "Error Details");

                throw;

            }
        }


        //  select del date
        void SelectDateFromCalendar(int row, int column)
        {
            string dateXPath = $"//div[@id='pddDatePicker']/div/table/tbody/tr[{row}]/td[{column}]";

            try
            {
                IWebElement dateElement = driver.FindElement(By.XPath(dateXPath));
                dateElement.Click();
            }
            catch (NoSuchElementException)
            {
                // Handle the case when the date element is not found
                // Console.WriteLine("Date element not found in the calendar.");
            }
            catch (StaleElementReferenceException)
            {
                IWebElement dateElement = driver.FindElement(By.XPath(dateXPath));
                dateElement.Click();
            }
        }

        void ProcessPersonalizedProduct(string Sceanrio, bool hasImageUpload)
        {
            switch (Sceanrio)
            {
                case "CPER":
               
                    Console.WriteLine("Product without image upload option");

                    WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                    IWebElement personalizedButton1 = driver.FindElement(By.CssSelector(".headRibbon"));
                    personalizedButton1.Click();

                    wait1.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='collapsibleContent']/div[@class='clearFix']/div/input[@type='text']")));

                    List<IWebElement> textFields2 = driver.FindElements(By.XPath("//div[@class='collapsibleContent']/div[@class='clearFix']/div/input[@type='text']")).ToList();
                    foreach (IWebElement textField in textFields2)
                    {
                        // Clear the existing text and send new input	
                        textField.Clear();
                        textField.SendKeys("X");
                    }

                    IWebElement buttonPreview = null;
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
                        Thread.Sleep(1000);
                    }

                    IWebElement confirm1 = driver.FindElement(By.Id("chkNFPersonalisedTermsAndCond"));
                    confirm1.Click();

                    break;

                case "PER_WITH_IMAGE":
                    if (hasImageUpload)
                    {
                        Console.WriteLine("Product with image upload option");

                        WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                        IWebElement personalizedButton2 = driver.FindElement(By.CssSelector(".headRibbon"));
                        personalizedButton2.Click();

                        wait2.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='collapsibleContent']/div[@class='clearFix']/div/input[@type='text']")));

                        List<IWebElement> textFields = driver.FindElements(By.XPath("//div[@class='collapsibleContent']/div[@class='clearFix']/div/input[@type='text']")).ToList();
                        foreach (IWebElement textField in textFields)
                        {
                            // Clear the existing text and send new input	
                            textField.Clear();
                            textField.SendKeys("X");
                        }

                        IWebElement addImageButton = null;
                        try
                        {
                            addImageButton = driver.FindElement(By.Id("FileEL_1_1"));

                        }
                        catch (NoSuchElementException)
                        {
                            Console.WriteLine("Image Upload is not present");
                        }

                        if (addImageButton != null && addImageButton.Displayed)
                        {
                            string imagePath = @"C:\Users\Priyanka Shirsath\Desktop\Screenshot.png";

                            addImageButton.Click();

                            wait2.Until(ExpectedConditions.ElementToBeClickable(By.Id("UploadImage"))).Click();

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

                                wait2.Until(ExpectedConditions.AlertIsPresent()).Accept();
                            }
                            catch (NoSuchElementException)
                            {
                                Console.WriteLine("Upload elements not found");
                            }
                            catch (NoAlertPresentException)
                            {
                                Console.WriteLine("Upload Alert");
                            }


                            IWebElement buttonPreview2 = null;
                            try
                            {
                                buttonPreview2 = driver.FindElement(By.Id("btnPreview"));
                            }
                            catch (NoSuchElementException)
                            {
                                // Preview button is not present	
                            }

                            if (buttonPreview2 != null && buttonPreview2.Displayed)
                            {
                                buttonPreview2.Click();
                                Thread.Sleep(1000);

                                try
                                {
                                    driver.SwitchTo().Alert().Accept();
                                    Thread.Sleep(1000);
                                    buttonPreview2.Click();
                                }
                                catch (NoAlertPresentException)
                                {
                                    Console.WriteLine("Confirmation Alert");
                                }
                            }
                        
                        }

                        IWebElement confirm = driver.FindElement(By.Id("chkNFPersonalisedTermsAndCond"));
                        confirm.Click();
                    }
                    else
                    {
                        Console.WriteLine("Product without image upload option (same as CPER)");

                        WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                        IWebElement personalizedButton3 = driver.FindElement(By.CssSelector(".headRibbon"));
                        personalizedButton3.Click();

                        wait3.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='collapsibleContent']/div[@class='clearFix']/div/input[@type='text']")));

                        List<IWebElement> textFields3 = driver.FindElements(By.XPath("//div[@class='collapsibleContent']/div[@class='clearFix']/div/input[@type='text']")).ToList();
                        foreach (IWebElement textField in textFields3)
                        {
                            // Clear the existing text and send new input	
                            textField.Clear();
                            textField.SendKeys("X");
                        }

                        IWebElement buttonPreview3 = null;
                        try
                        {
                            buttonPreview = driver.FindElement(By.Id("btnPreview"));
                        }
                        catch (NoSuchElementException)
                        {
                            // Preview button is not present	
                        }

                        if (buttonPreview3 != null && buttonPreview3.Displayed)
                        {
                            buttonPreview3.Click();
                            Thread.Sleep(1000);
                        }

                        IWebElement confirm3 = driver.FindElement(By.Id("chkNFPersonalisedTermsAndCond"));
                        confirm3.Click();
                    }
                    break;

                default:
                    Console.WriteLine("Invalid product name");
                    break;
            }
                      
            }
       

        private int GetTotalPages()
        {
            IWebElement paginationElement = driver.FindElement(By.XPath("//*[@id='ctl00_PagingBarTop_PagerTopBar']/div[1]"));
            IReadOnlyCollection<IWebElement> pageLinks = paginationElement.FindElements(By.TagName("a"));
            return pageLinks.Count;
        }



    }
}

















