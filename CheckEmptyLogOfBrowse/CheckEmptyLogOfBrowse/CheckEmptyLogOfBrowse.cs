using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using OpenQA.Selenium.IE;

namespace CheckEmptyLogOfBrowse
{
    [TestFixture]
    public class CheckEmptyLogOfBrowse
    {
        private IWebDriver driver;

        [SetUp]
        public void CheckEmptyLog()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }

        [Test]    
        public void CheckEmptyLogTest()
        {
            //driver.Url = "https://selenium2.ru/";

            //    foreach (LogEntry l in driver.Manage().Logs.GetLog(LogType.Driver))
            //    {
            //        System.Diagnostics.Debug.WriteLine(l);
            //    }
            string Text = "admin";
            driver.Url = "http://localhost:8081/litecard/admin/";
            driver.FindElement(By.Name("username")).SendKeys(Text);
            driver.FindElement(By.Name("password")).SendKeys(Text);
            driver.FindElement(By.TagName("button")).Click();
            driver.FindElements(By.CssSelector("#app-"))[1].Click();
            driver.FindElement(By.LinkText("Rubber Ducks")).Click();
            var products = driver.FindElements(By.CssSelector("td a")).Where(y => string.IsNullOrEmpty(y.GetAttribute("title"))).Skip(3).ToList();                                                                                                                                  
            for (int i = 0; i < products.Count(); i++)
            {
                driver.FindElements(By.CssSelector("td a")).Where(y => string.IsNullOrEmpty(y.GetAttribute("title"))).
                    Skip(3).ElementAt(i).Click();

                foreach (LogEntry l in driver.Manage().Logs.GetLog(LogType.Driver))
                {
                    System.Diagnostics.Debug.WriteLine(l);
                }
                driver.FindElement(By.CssSelector("[name=cancel]")).Click();
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

            
    }
}
