using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SpecFlowNetFloristProj.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowNetFloristProj.StepDefinitions
{
    [Binding]
    public class GiftWizardTestStepDefcs
    {

        IWebDriver driver;
        GiftWizardPage gift;

        public GiftWizardTestStepDefcs(IWebDriver driver)
        {
            this.driver = driver;
            gift = new GiftWizardPage(driver);
        }

        [Given(@"I am on the gift wizard section of the website")]
        public void GivenIAmOnTheGiftWizardSectionOfTheWebsite()
        {
            driver.Navigate().GoToUrl("https://www.netflorist.co.za/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(90);

        }

        [When(@"I select the occasion, location, and date")]
        public void WhenISelectTheOccasionLocationAndDate()
        {

            gift.OccasionClick();
            gift.SuburbListSelect(); 
            gift.DateSelection();

        }

        [When(@"I click on the Find Now button")]
        public void WhenIClickOnTheButton()
        {
            gift.ClickOnFind();

        }

        [Then(@"I should see a list of all available products")]
        public void ThenIShouldSeeAListOfAllAvailableProducts()
        {
            gift.SelectProduct();

        }

        [When(@"I test each product's availability")]
        public void WhenITestEachProductsAvailability()
        {

            gift.Details("xxx", "xxxx", "0712345678", "xxxx", "Residence", "xxxx");
        }

        [Then(@"I should be able to add all available products to my basket")]
        public void ThenIShouldBeAbleToAddAllAvailableProductsToMyBasket()
        {
           gift.NextDeliveryType();
        }

        [Then(@"I should see an error message for any unavailable products")]
        public void ThenIShouldSeeAnErrorMessageForAnyUnavailableProducts()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                Console.WriteLine("Alert text: " + alertText);
                alert.Accept();
                //Console.WriteLine(driver.Title);

            }
            catch (NoAlertPresentException)
            {
                Console.WriteLine(driver.Title);
            }

        }

        [When(@"I check for the availability of products on the next page \(if available\)")]
        public void WhenICheckForTheAvailabilityOfProductsOnTheNextPageIfAvailable()
        {
            driver.Navigate().Back();
            Thread.Sleep(3000);
            

        }

        [Then(@"I should continue the testing process until all available products have been tested")]
        public void ThenIShouldContinueTheTestingProcessUntilAllAvailableProductsHaveBeenTested()
        {
            List<IWebElement> Productlist = driver.FindElements(By.XPath("//div[@class ='ProductBox']")).ToList();
        }
    }
}

