using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace AddDeleteProductInCart
{
    [TestFixture]
    public class AddDeleteProductInCart
    {               
        private IWebDriver driver;
        private WebDriverWait wait;   

        [SetUp]
        public void WorkWithProduct()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }

        [Test]
        public void AddDeleteProduct()
        {
            for (int i = 0; i < 3; i++)
            {
                driver.Url = "http://localhost:8081/litecard/en/";

                driver.FindElement(By.CssSelector("#box-campaigns .link")).Click();

                if (IsElementPresent(By.Id("view-full-page")))
                {
                    driver.FindElement(By.LinkText("View full page")).Click();
                }

                int quantityItems = int.Parse(driver.FindElement(By.CssSelector(".quantity")).GetAttribute("textContent"));
                driver.FindElement(By.CssSelector("[name*=options] [value=Small]")).Click();
                int quantity = int.Parse(driver.FindElement(By.CssSelector("[name=quantity]")).GetAttribute("value"));
                driver.FindElement(By.CssSelector("[name=add_cart_product]")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.CssSelector("div.content span")), (quantityItems + quantity).ToString()));
            }
            driver.FindElement(By.CssSelector("#cart a")).Click();

            //var deleteProductsCount = 0;
            //do
            //{
            //    var deleteProducts = driver.FindElements(By.CssSelector("[name=remove_cart_item]"));
            //    deleteProductsCount = deleteProducts.Count;
            //    if (deleteProductsCount != 0)
            //    {
            //        deleteProducts[0].Click();
            //        wait.Until(d => d.FindElements(By.CssSelector("tbody tr.item")).Count == deleteProductsCount - 1);
            //    }

            //} while (deleteProductsCount != 0);

            var deleteProducts = driver.FindElements(By.CssSelector("[name=remove_cart_item]"));
            while (deleteProducts.Count != 0)
            {
                deleteProducts[0].Click();
                wait.Until(d => d.FindElements(By.CssSelector("tbody tr.item")).Count == deleteProducts.Count - 1);
                deleteProducts = driver.FindElements(By.CssSelector("[name=remove_cart_item]"));
            }

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
