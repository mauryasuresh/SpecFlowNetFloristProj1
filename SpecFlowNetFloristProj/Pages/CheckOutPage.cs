using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SpecFlowNetFloristProj.Pages
{
    public class CheckOutPage
    {
        private IWebDriver driver;

        public CheckOutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }

        //Checkout Button
        [FindsBy(How = How.XPath, Using = "//*[@id='topRibbon']/div/div[3]/div[4]/div/div[5]/div[2]/a")]
        private IWebElement CheckOutButton;

        public void ClickOnCheckOut()
        {
            CheckOutButton.Click();
        }


        [FindsBy(How = How.CssSelector, Using = ".btnCheckout.floatRight")]
        private IWebElement SecureCheckOutButton;

        public void ClickOnSecureCheckOut()
        {
            SecureCheckOutButton.Click();
        }


        //click on delivery ,payment options
        [FindsBy(How = How.CssSelector, Using = "li.iconDCSprite.deliveryInfo.deliveryTab")]
        private IWebElement DeliveryInfo;

        [FindsBy(How = How.CssSelector, Using = "li.iconDCSprite.paymentOptions.paymentTab")]
        private IWebElement PaymentOptions;

        public void CheckOutProcess()
        {
            DeliveryInfo.Click();
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div.loaderMask")));
            PaymentOptions.Click();
            // clickOnCard();
        }



        //click on visa Master Card button

        [FindsBy(How = How.XPath, Using = "//*[@id='PaymentContent_Credit_Card_OtherUsers']/div/div[1]/input")]
        private IWebElement CardButton;

        public void clickOnCard()
        {
            CardButton.Click();
        }

        [FindsBy(How=How.XPath,Using = "/html/body/iframe[@title='Hosted Checkout']")]
        private IWebElement IframeElement;

        [FindsBy(How = How.XPath, Using = "//*[@id='cardContent']")]
        private IWebElement CardContent;

        [FindsBy(How = How.XPath, Using = "//div[@class='cardNumber']/input[@id='cardNumber']")]
        private IWebElement CardNumber;


        [FindsBy(How = How.XPath, Using = "//select[@id='expiryMonth']")]
        private IWebElement ExpMonth;


        [FindsBy(How = How.XPath, Using = "//select[@id='expiryYear']")]
        private IWebElement ExpYear;


        [FindsBy(How = How.XPath, Using = "//*[@id=\"cardHolderName\"]")]
        private IWebElement CardholderName;

        [FindsBy(How = How.Id, Using = "csc")]
        private IWebElement SecurityCode;


        [FindsBy(How = How.XPath, Using = " //*[@id='button-row1']/button[2]")]
        private IWebElement PayNowButton;
       



        public void CardDetail(string cardNum,string cardholdername,string securityCode)
        {
            //driver.SwitchTo().Frame(IframeElement);
            CardNumber.SendKeys(cardNum);
            SelectElement selectMonth = new SelectElement(ExpMonth);
            selectMonth.SelectByText("10");

            SelectElement selectYear = new SelectElement(ExpYear);
            selectYear.SelectByText("22");
            CardholderName.SendKeys(cardholdername);
            SecurityCode.SendKeys(securityCode);
          //  PayNowButton.Click();

        }

        //PayNow Button

        [FindsBy(How = How.XPath, Using = "//*[@id='button-row1']/button[2]")]
        private IWebElement PayNow;

        public void ClickOnPayNow()
        {
            PayNow.Click();
        }



        [FindsBy(How = How.CssSelector, Using = ".popDeliveryDetails")]
        private IWebElement UnavailablePopUp;

        public void ClickOnUnavailablePopUp()
        {
            string ProductUnavailableText = UnavailablePopUp.Text;
            if(ProductUnavailableText.Contains("Not Available"))
            {
                Console.WriteLine("Product is Unavailable at this location");
            }
          
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='ctl00_MainContent_tbCorpAccountNumber']")]
        private IWebElement CostRef;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Place Order')]")]

        private IWebElement PlaceOrder;

        public void OnCostRef(string reference)
        {
            CostRef.SendKeys(reference);
          
        }

        public void OnPlaceOrder()
        {

            PlaceOrder.Click();

        }
    }
}
//CheckOutPage.clickOnCard();

////Fill CArd Details
//IWebElement iframeElement = driver.FindElement(By.XPath("//iframe[@title='Hosted Checkout']"));

//driver.SwitchTo().Frame(iframeElement);

//CheckOutPage.CardDetail("4606120200006589", "Test", "419");
//CheckOutPage.ClickOnPayNow();