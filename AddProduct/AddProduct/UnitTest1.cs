using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Linq;
using System.IO;

namespace AddProduct
{
    [TestFixture]
    public class UnitTest1
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void startBrowse()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void new_product()
        {
            string Text = "admin";
            Random j = new Random();
            int i = j.Next(0, 1000);
            driver.Url = "http://localhost:8081/litecard/admin/?app=catalog&doc=catalog";
            driver.FindElement(By.Name("username")).SendKeys(Text);
            driver.FindElement(By.Name("password")).SendKeys(Text);
            driver.FindElement(By.TagName("button")).Click();
            driver.FindElement(By.LinkText("Add New Product")).Click();
            driver.FindElements(By.CssSelector("label.btn"))[0].Click();
            driver.FindElement(By.CssSelector("[name=code]")).SendKeys("FA001");
            driver.FindElement(By.CssSelector("[name*=name]")).SendKeys($"car{i}");
            driver.FindElement(By.CssSelector("[name=sku]")).SendKeys("544010");
            driver.FindElement(By.CssSelector("[name=taric]")).SendKeys("107632171-1");
            driver.FindElement(By.CssSelector("[name=quantity]")).SendKeys("1");
            driver.FindElement(By.CssSelector("[name=weight]")).Clear();
            driver.FindElement(By.CssSelector("[name=weight]")).SendKeys("0,184");
            driver.FindElement(By.CssSelector("[name=dim_x]")).Clear();
            driver.FindElement(By.CssSelector("[name=dim_y]")).Clear();
            driver.FindElement(By.CssSelector("[name=dim_z]")).Clear();
            driver.FindElement(By.CssSelector("[name=dim_x]")).SendKeys("0,07");
            driver.FindElement(By.CssSelector("[name=dim_y]")).SendKeys("0,07");
            driver.FindElement(By.CssSelector("[name=dim_z]")).SendKeys("0,16");
            driver.FindElements(By.CssSelector(".checkbox [name*=product_groups]"))[2].Click();
            SetDatepicker(driver, "[name=date_valid_from]", "17.05.2016");
            SetDatepicker(driver, "[name=date_valid_to]", "20.05.2016");
            attachFile(driver, "[type=file]", Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Attaches","toy_car.jpg"));
            driver.FindElement(By.LinkText("Information")).Click();
            driver.FindElements(By.CssSelector("[name=manufacturer_id] option"))[1].Click();
            driver.FindElement(By.CssSelector("[name=keywords]")).SendKeys("car");
            driver.FindElement(By.CssSelector("[name*=short_description]")).SendKeys("toy car");
            driver.FindElement(By.CssSelector(".trumbowyg-editor")).SendKeys("This is small toy car");
            driver.FindElement(By.CssSelector("[name*=attributes]")).SendKeys("test");
            driver.FindElement(By.CssSelector("[name*=head_title]")).SendKeys("toy car");
            driver.FindElement(By.CssSelector("[name*=meta_description]")).SendKeys("test");
            driver.FindElement(By.LinkText("Prices")).Click();
            driver.FindElement(By.CssSelector("[name=purchase_price]")).Clear();
            driver.FindElement(By.CssSelector("[name=purchase_price]")).SendKeys("6");
            driver.FindElement(By.CssSelector("[name=purchase_price_currency_code] [value=USD]")).Click();
            driver.FindElements(By.CssSelector("[name*=prices]"))[0].SendKeys("6");
            driver.FindElement(By.CssSelector("[name=save]")).Click();
            driver.FindElement(By.LinkText($"car{i}"));



        }
            public void SetDatepicker(IWebDriver driver, string cssSelector, string date)
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until<bool>(c => driver.FindElement(By.CssSelector(cssSelector)).Displayed);
                driver.FindElement(By.CssSelector(cssSelector)).SendKeys(date);
            }
            public void unhide(IWebDriver driver, IWebElement element)
                {
                    string script = "arguments[0].style.opacity=1;"
                        + "arguments[0].style['transform']='translate(0px, 0px) scale(1)';"
                        + "arguments[0].style['MozTransform']='translate(0px, 0px) scale(1)';"
                        + "arguments[0].style['WebkitTransform']='translate(0px, 0px) scale(1)';"
                        + "arguments[0].style['msTransform']='translate(0px, 0px) scale(1)';"
                        + "arguments[0].style['OTransform']='translate(0px, 0px) scale(1)';"
                        + "return true;";
            ((IJavaScriptExecutor)driver).ExecuteScript(script, element);
                }
            public void attachFile(IWebDriver driver, string cssSelector, string file)
                {
                    IWebElement input = driver.FindElement(By.CssSelector(cssSelector));
                    unhide(driver, input);
                    input.SendKeys(file);
                }

   
                      
 
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
