using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SpecFlowNetFloristProj.Pages;
using NUnit.Framework;

namespace SpecFlowNetFloristProj.ProductSearchScript
{
    public class Hamper : ProductBaseClass
    {

        IWebDriver driver;
        SearchHomePage searchflower;
        GiftWizardHomePage giftwizard;
        LoginPage LoginPage;
        CheckOutPage CheckOutPage;
        WebDriverWait wait;

        string productCode = "RA2943";

        List<string> addresses = new List<string>()
        {
            "Parade hotel, 191 O R Tambo Parade , Marine Parade, North Beach, Durban, Kwa-zulu Natal",
            "Hillcrest Primary School, 5 Bollihope Cres, Mowbray, Cape Town, Western Cape",
            "34 Barbet Crescent, ,Midrand, Johannesburg, Gauteng",
            "Baragwanath Hospital, 26 Chris Hani Rd, Diepkloof, Soweto, Gauteng"
        };


        [Category("Hamper")]
        [Test]
        public void HamperAdd1()
        {
            ExecuteScriptForAddress(productCode, addresses[0], "Business");
        }

        [Category("Hamper")]
        [Test]
        public void HamperAdd2()
        {
            ExecuteScriptForAddress(productCode, addresses[1], "School");
        }

        [Category("Hamper")]
        [Test]
        public void HamperAdd3()
        {
            ExecuteScriptForAddress(productCode, addresses[2], "Residence");
        }

        [Category("Hamper")]
        [Test]
        public void HamperAdd4()
        {
            ExecuteScriptForAddress(productCode, addresses[3], "Hospital");
        }

    }
}
