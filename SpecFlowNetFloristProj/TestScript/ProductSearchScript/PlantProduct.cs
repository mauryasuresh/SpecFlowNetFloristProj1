using NPOI.SS.Formula.Functions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SpecFlowNetFloristProj.Pages;



namespace SpecFlowNetFloristProj.ProductSearchScript
{
    public class PlantProduct: ProductBaseClass
    {
        IWebDriver driver;
        SearchHomePage searchflower;
        GiftWizardHomePage giftwizard;
        LoginPage LoginPage;
        CheckOutPage CheckOutPage;
        WebDriverWait wait;
        
         

        string productCode = "NETPL1682";
        List<string> addresses = new List<string>()
        {
            "Parade hotel, 191 O R Tambo Parade , Marine Parade, North Beach, Durban, Kwa-zulu Natal",
            "Hillcrest Primary School, 5 Bollihope Cres, Mowbray, Cape Town, Western Cape",
            "34 Barbet Crescent, ,Midrand, Johannesburg, Gauteng",
            "Baragwanath Hospital, 26 Chris Hani Rd, Diepkloof, Soweto, Gauteng"
        };

        [Category("PlantProduct")]
        [Test]
        public void PlantAddress1()
        {
            ExecuteScriptForAddress(productCode, addresses[0], "Business");
        }


        [Category("PlantProduct")]
        [Test]
        public void PlantAddress2()
        {
            ExecuteScriptForAddress(productCode, addresses[1], "School");
        }

        [Category("PlantProduct")]
        [Test]
        public void PlantAddress3()
        {
            ExecuteScriptForAddress(productCode, addresses[2], "Residence");
        }

        [Category("PlantProduct")]
        [Test]
        public void PlantAddress4()
        {
            ExecuteScriptForAddress(productCode, addresses[3], "Hospital");
        }

    }
}
