using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SpecFlowNetFloristProj.Pages;
using NUnit.Framework;

namespace SpecFlowNetFloristProj.ProductSearchScript
{
    public class BakeryProduct: ProductBaseClass
    {

        IWebDriver driver;
        SearchHomePage searchflower;
        GiftWizardHomePage giftwizard;
        LoginPage LoginPage;
        CheckOutPage CheckOutPage;
        WebDriverWait wait;
       

        string productCode = "CCUP014";
        List<string> addresses = new List<string>()
        {
            "Parade hotel, 191 O R Tambo Parade , Marine Parade, North Beach, Durban, Kwa-zulu Natal",
            "Hillcrest Primary School, 5 Bollihope Cres, Mowbray, Cape Town, Western Cape",
            "34 Barbet Crescent, ,Midrand, Johannesburg, Gauteng",
            "Baragwanath Hospital, 26 Chris Hani Rd, Diepkloof, Soweto, Gauteng"
        };

        [Category("BakeryProduct")]
        [Test]
        public void BakeryAdd1()
        {
            ExecuteScriptForAddress(productCode, addresses[0], "Business");
        }

        [Category("BakeryProduct")]
        [Test]
        public void BakeryAdd2()
        {
            ExecuteScriptForAddress(productCode, addresses[1], "School");
        }

        [Category("BakeryProduct")]
        [Test]
        public void BakeryAdd3()
        {
            ExecuteScriptForAddress(productCode, addresses[2], "Residence");
        }

        [Category("BakeryProduct")]
        [Test]
        public void BakeryAdd4()
        {
            ExecuteScriptForAddress(productCode, addresses[3], "Hospital");
        }

    }
}
