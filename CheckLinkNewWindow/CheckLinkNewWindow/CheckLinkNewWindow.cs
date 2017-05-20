using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Linq;
using System.Collections.Generic;

namespace CheckLinkNewWindow
{
    [TestFixture]
    public class CheckLinkNewWindow
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void startBrowse()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromMilliseconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void CheckLink()
        {
            driver.Url = "http://localhost:8081/litecard/admin/";
            string UserPass = "admin";
            driver.FindElement(By.Name("username")).SendKeys(UserPass);
            driver.FindElement(By.Name("password")).SendKeys(UserPass);
            driver.FindElement(By.TagName("button")).Click();
            driver.FindElements(By.CssSelector(".list-vertical #app- a"))[2].Click();
            driver.FindElement(By.LinkText("Canada")).Click();
            string mainWindow = driver.CurrentWindowHandle;
            var WikiLink = driver.FindElements(By.CssSelector("[name=country_form] [target=_blank]"));
            for (int i = 0; i < WikiLink.Count; i++)
            {
                ICollection<string> oldWindows = driver.WindowHandles;
                driver.FindElements(By.CssSelector("[name=country_form] [target=_blank]"))[i].Click();
                wait.Until(d => d.WindowHandles.Count != oldWindows.Count);
                var newWindow = driver.WindowHandles.Except(oldWindows).First();
                //var qwe = driver.WindowHandles.Concat(oldWindows);
                //var asd = qwe.Distinct();
                //var qwe = driver.WindowHandles.Where(x => !oldWindows.Any(y => x == y));
                driver.SwitchTo().Window(newWindow);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);

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
