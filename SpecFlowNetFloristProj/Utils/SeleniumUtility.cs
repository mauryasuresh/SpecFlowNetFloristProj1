using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports;
using Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V110.DOM;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace SpecFlowNetFloristProj.Utils
{
    public class SeleniumUtility    
    {
        public static IWebDriver driver =null;
        public static Actions action = null;
        public WebDriverWait wait = null;

        public IWebDriver SetUp(string browserName, string appUrl)
        {
            IWebDriver driver;

            if (browserName.Equals("Chrome"))
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("no-sandbox");
                ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                driver = new ChromeDriver(driverService, options, TimeSpan.FromMinutes(3));
            }
            else if (browserName.Equals("Firefox"))
            {
                new DriverManager().SetUpDriver(new FirefoxConfig());
                driver = new FirefoxDriver();
            }
            else
            {
                throw new ArgumentException("Unsupported browser name");
            }

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl(appUrl);

            return driver;
        }


        public void performClick(IWebElement element)
        {
            element.Click();
        }

        //sendkeys 
        public void typeInput(IWebElement element,string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public void waitTillElementIsClickable(WebElement element)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));

        }
     

        //Dropdown

        public static void SelectFromDropDownByText(IWebElement element, string text)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByText(text);
            
        }

        public static void SelectFromDropDownByIndex(IWebElement element, int index)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByIndex(index);

        }

        public static void SelectFromDropDownByValue(IWebElement element, string value)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByValue(value);

        }


        public static void WaitForElementToBeClickable(IWebDriver driver, By by, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        //Screenshot
        public static void TakeScreenshot(IWebDriver driver, string filePath)
        {
            if (driver != null)
            { 
                ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
          }

            else
            {
                Console.WriteLine("Driver instance is null. Skipping screenshot capture.");
            }
        }

        //Alert accept
        public static void AlertAccept(IWebDriver driver)
        {
         
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch(NoAlertPresentException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                driver.SwitchTo().DefaultContent();
            }
        }

        //Alert accept dismiss
        public static void AlertDismiss(IWebDriver driver)
        {
             
            try
            {
                IAlert alert =  driver.SwitchTo().Alert();
                alert.Dismiss();

            }
            catch (NoAlertPresentException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                driver.SwitchTo().DefaultContent();
            }
        }

        public static bool IsElementDisplayed(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public static bool IsElementEnabled(IWebElement element)
        {
            try
            {
                return element.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public void cleanUp()
        {
            driver.Close();

        }

        public static  void GoToNextPage(IWebDriver driver)
        {
            IWebElement nextButton = driver.FindElement(By.XPath("//*[@id='ctl00_MainContent_ctl00_LinkNext']"));
            nextButton.Click();
            Thread.Sleep(2000);
        }

        public bool IsElementPresent(By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

       public static void SelectDateFromCalendar(string desiredDate)
        {
            string dateXPath = $"//div[@id='pddDatePicker']//td/a[contains(text(), '{desiredDate}')]";

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











        //private void GoToPage(int page)
        //{
        //    IWebElement pageLink = driver.FindElement(By.XPath($"//*[@id='ctl00_PagingBarTop_PagerTopBar']/div[1]/a[{page}]"));
        //    pageLink.Click();

        //    // Wait for the page to load completely
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //    wait.Until(ExpectedConditions.UrlContains($"&page={page}"));

        //}


    }
}


//**********************************
//checking personalised product with textfield and image
//public void ProcessPersonalizedProduct()
//{

//    Console.WriteLine("Product starting with 'PER'");
//    WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

//    IWebElement personalizedButton = driver.FindElement(By.CssSelector(".headRibbon"));
//    string personalizedButtonText = personalizedButton.Text.Trim();

//    if (personalizedButtonText != "Personalise this item")
//    {
//        personalizedButton.Click();
//        wait1.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='collapsibleContent']/div[@class='clearFix']/div/input[@type='text']")));
//    }

//    List<IWebElement> textFields = driver.FindElements(By.XPath("//div[@class='collapsibleContent']/div[@class='clearFix']/div/input[@type='text']")).ToList();
//    foreach (IWebElement textField in textFields)
//    {
//        textField.Clear();
//        textField.SendKeys("X");

//    }


//}



//click on preiew and confirm 
//private void PreviewAndConfirm()
//{


//    // Check if preview button is present
//    IWebElement buttonPreview;
//    try
//    {
//        buttonPreview = driver.FindElement(By.Id("btnPreview"));
//    }
//    catch (Exception)
//    {
//        buttonPreview = null;
//    }
//    try
//    {
//        buttonPreview = driver.FindElement(By.Id("btnPreview"));
//    }
//    catch (NoSuchElementException)
//    {
//        // Preview button is not present
//    }

//    if (buttonPreview != null && buttonPreview.Displayed)
//    {
//        buttonPreview.Click();
//        Thread.Sleep(2000);
//        buttonPreview.SendKeys(Keys.Escape);
//        Console.WriteLine("Click");
//    }

//    IWebElement confirm = driver.FindElement(By.Id("chkNFPersonalisedTermsAndCond"));
//    confirm.Click();
//    Console.WriteLine("Performing preview and confirm action");
//}  
//if (productCode.StartsWith("PER"))
//{
//    if (IsTextFieldPresent())
//    {
//        Console.WriteLine("Text field is present");
//        ProcessPersonalizedProduct();

//        if (IsImageFieldPresent())
//        {
//            Console.WriteLine("Image field is present");
//            photoOption();
//        }
//    }
//    if (IsImageFieldPresent())
//    {
//        Console.WriteLine("Image field is present");
//        photoOption();
//    }
//    PreviewAndConfirm();
//}