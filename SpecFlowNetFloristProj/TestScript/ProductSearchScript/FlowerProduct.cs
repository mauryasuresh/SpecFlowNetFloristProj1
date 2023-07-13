using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecFlowNetFloristProj.Pages;
using SpecFlowNetFloristProj.Utils;


namespace SpecFlowNetFloristProj.TestsScript
{
    public class FlowerProduct : ProductBaseClass
    {
        IWebDriver driver;
        SearchHomePage searchflower;
        GiftWizardHomePage giftwizard;
        LoginPage LoginPage;
        CheckOutPage CheckOutPage;
        WebDriverWait wait;

   string productCode = "NETBQ002";
        List<string> addresses = new List<string>()
        {
            "Parade hotel, 191 O R Tambo Parade , Marine Parade, North Beach, Durban, Kwa-zulu Natal",
            "Hillcrest Primary School, 5 Bollihope Cres, Mowbray, Cape Town, Western Cape",
            "34 Barbet Crescent, ,Midrand, Johannesburg, Gauteng",
            "Baragwanath Hospital, 26 Chris Hani Rd, Diepkloof, Soweto, Gauteng"
        };


        [Category("FlowerProduct")]
        [Test]
        public void FlowerAddress1()
        {
            ExecuteScriptForAddress("NETBQ002", addresses[0], "Business");
        }

        [Category("FlowerProduct")]
        [Test]
        public void FlowerAddress2()
        {
            ExecuteScriptForAddress("NETBQ002", addresses[1], "School");
        }

        [Category("FlowerProduct")]
        [Test]
        public void FlowerAddress3()
        {
            ExecuteScriptForAddress("NETBQ002", addresses[2], "Residence");
        }

        [Category("FlowerProduct")]
        [Test]
        public void FlowerAddress4()
        {
            ExecuteScriptForAddress("NETBQ002", addresses[3], "Hospital");
        }

    }
}







