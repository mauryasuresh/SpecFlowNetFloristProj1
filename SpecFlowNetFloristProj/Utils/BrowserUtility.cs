using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowNetFloristProj.Utils
{
    public class BrowserUtility
    {
        public IWebDriver init(IWebDriver driver)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://stage2.netflorist.co.za/";
            return driver;
        }
    }
}
