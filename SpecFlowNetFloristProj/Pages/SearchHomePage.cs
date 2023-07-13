using MongoDB.Driver.Core.Operations;
using NPOI.SS.Formula.Functions;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;


namespace SpecFlowNetFloristProj.Pages
{
    public class SearchHomePage
    {

        private IWebDriver driver;
        public SearchHomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }




        //search product
        [FindsBy(How = How.XPath, Using = "//*[@id='inputSearch']")]
        private IWebElement SearchText;

        //[FindsBy(How = How.Id, Using = "SearchButton")]
        //private IWebElement SearchButton;

        public void searchProduct(string product)
        {
            SearchText.SendKeys(product);
            SearchText.SendKeys(Keys.Enter);
            Thread.Sleep(1000);
        }

        //Select product from search list

        //[FindsBy(How = How.XPath, Using = "//*[@id='SearchContainer']/div/div/div/div[4]/div[2]/div/a")]
        //private IWebElement SelectProduct;

        public void selectProduct()
        {
            List<IWebElement> products = driver.FindElements(By.XPath("//*[@id='SearchContainer']/div/div/div/div[4]/div[2]/div/a")).ToList();
            int productCount = products.Count;
            Console.WriteLine("No.of Products " +productCount.ToString());

            for (int i = 0; i < productCount; i++)
            {
                try
                {
                    products[i].Click();
                    Thread.Sleep(1000);
                }
                catch (StaleElementReferenceException)
                {
                    //Console.WriteLine("stale element");
                }

            }
      
            
        }





    }
}
//String CurrentWindowHandle = driver.CurrentWindowHandle;
//Console.WriteLine(CurrentWindowHandle);

//IReadOnlyCollection<string> allWindowHandles = driver.WindowHandles;
//Console.WriteLine(allWindowHandles.Count);
//foreach (string windowHandle in allWindowHandles)
//{
//    // Switch to the new window
//    if (windowHandle != CurrentWindowHandle)
//    {
//        driver.SwitchTo().Window(windowHandle);
//        break;
//    }

//    driver.SwitchTo().Window(CurrentWindowHandle);
//    Console.WriteLine(allWindowHandles);
//}
