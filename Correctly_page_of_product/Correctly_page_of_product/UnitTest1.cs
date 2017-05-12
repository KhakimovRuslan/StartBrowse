using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using System.Linq;

namespace Correctly_page_of_product
{
    [TestFixture]
    public class Correctly_page_of_product
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void StartBrowse()
        {
            //driver = new ChromeDriver();
            //driver = new InternetExplorerDriver();
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Check_page()
        {
            driver.Url = "http://localhost:8081/litecard/en/";
            //вкладка campaign
            string NameProduct = driver.FindElement(By.CssSelector("#box-campaigns .name")).GetAttribute("textContent"); 
            string RegularPrice = driver.FindElement(By.CssSelector("#campaign-products .price-wrapper .regular-price")).GetAttribute("textContent").Replace("$","");
            int RegularPrice1 = Convert.ToInt32(RegularPrice);
            int RegularPriceHeight = driver.FindElement(By.CssSelector("#campaign-products .price-wrapper .regular-price")).Size.Height;
            string RegularPriceTagName = driver.FindElement(By.CssSelector("#campaign-products .price-wrapper .regular-price")).TagName;
            string RegularPriceColor = driver.FindElement(By.CssSelector("#campaign-products .price-wrapper .regular-price")).GetCssValue("color");
            if (RegularPriceColor.Contains("rgb"))
                RegularPriceColor = RegularPriceColor.Replace("rgb","rgba").Replace(")", ", 1)");
            string CampaignPrice = driver.FindElement(By.CssSelector("#campaign-products .price-wrapper .campaign-price")).GetAttribute("textContent").Replace("$", "");
            int CampaignPrice1 = Convert.ToInt32(CampaignPrice);
            int CampaignPriceHeight = driver.FindElement(By.CssSelector("#campaign-products .price-wrapper .campaign-price")).Size.Height;
            string CampaignPriceTagName = driver.FindElement(By.CssSelector("#campaign-products .price-wrapper .campaign-price")).TagName;
            string CampaignPriceColor = driver.FindElement(By.CssSelector("#campaign-products .price-wrapper .campaign-price")).GetCssValue("color");
            if (CampaignPriceColor.Contains("rgb"))
                CampaignPriceColor = CampaignPriceColor.Replace("rgb","rgba").Replace(")", ", 1)");
            driver.FindElement(By.CssSelector("#box-campaigns .product")).Click();

            //страница продукта
            string NameProductPage = driver.FindElement(By.CssSelector("h1.title")).GetAttribute("textContent");  
            string RegularPricePage = driver.FindElement(By.CssSelector(".price-wrapper .regular-price")).GetAttribute("textContent").Replace("$", "");
            int RegularPricePage1 = Convert.ToInt32(RegularPricePage);
            int RegularPricePageHeight = driver.FindElement(By.CssSelector(".price-wrapper .regular-price")).Size.Height;
            string RegularPricePageColor = driver.FindElement(By.CssSelector(".price-wrapper .regular-price")).GetCssValue("color");
            if (RegularPricePageColor.Contains("rgb"))
                RegularPricePageColor = RegularPricePageColor.Replace("rgb", "rgba").Replace(")", ", 1)");
            string CampaignPricePage = driver.FindElement(By.CssSelector(".price-wrapper .campaign-price")).GetAttribute("textContent").Replace("$", "");
            int CampaignPricePage1 = Convert.ToInt32(CampaignPricePage);
            int CampaignPricePageHeight = driver.FindElement(By.CssSelector(".price-wrapper .campaign-price")).Size.Height;
            string CampaignPricePageColor = driver.FindElement(By.CssSelector(".price-wrapper .campaign-price")).GetCssValue("color");
            if (CampaignPricePageColor.Contains("rgb"))
                CampaignPricePageColor = CampaignPricePageColor.Replace("rgb", "rgba").Replace(")", ", 1)");

            //проверки
            Assert.AreEqual(NameProduct, NameProductPage);
            Assert.AreEqual(RegularPrice1, RegularPricePage1);
            Assert.AreEqual(CampaignPrice1, CampaignPricePage1);
            Assert.AreEqual(RegularPriceTagName, "s");  //проверка, что TagName элемента - "s" т.к он делает текст зачеркнутым
            Assert.AreEqual(RegularPriceColor, "rgba(51, 51, 51, 1)");
            Assert.AreEqual(CampaignPriceTagName, "strong");  //проверка, что TagName элемента - "strong" т.к он выделяет текст жирным
            Assert.AreEqual(CampaignPriceColor, "rgba(204, 0, 0, 1)");
            Assert.AreEqual(RegularPricePageColor, "rgba(51, 51, 51, 1)");
            Assert.AreEqual(CampaignPricePageColor, "rgba(204, 0, 0, 1)");
            Assert.Greater(CampaignPriceHeight, RegularPriceHeight);
            Assert.Greater(CampaignPricePageHeight, RegularPricePageHeight);
            

            
        }
        
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null; 
        }
    }

}
