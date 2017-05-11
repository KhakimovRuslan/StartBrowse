using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Linq;
using System.Collections.Generic;

namespace Sort_of_elements
{
    [TestFixture]
    public class LoginAsAdmin
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void startBrowse()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();

        }
        [Test]
        public void Enter()
        {
            String Text = "admin";
            driver.Url = "http://localhost:8081/litecard/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys(Text);
            driver.FindElement(By.Name("password")).SendKeys(Text);
            driver.FindElement(By.TagName("button")).Click();
            var ListCountries = driver.FindElements(By.CssSelector("tbody a")).Where(x=>string.IsNullOrEmpty(x.GetAttribute("title"))).Select(x=>x.Text).ToList();
            var ListCountriesCopy = new List<string>(ListCountries);
            ListCountriesCopy.Sort();
            Assert.AreEqual(ListCountries, ListCountriesCopy);
            var Countries = driver.FindElements(By.CssSelector("tbody tr")).Where(y => int.Parse(y.FindElements(By.CssSelector("td"))[5].Text) > 0).ToList();
            for (int i = 0; i < Countries.Count; i++)
            {
                driver.FindElements(By.CssSelector("tbody tr")).Where(y => int.Parse(y.FindElements(By.CssSelector("td"))[5].Text) > 0).ElementAt(i).FindElements(By.CssSelector("td"))[4].FindElement(By.CssSelector("a")).Click();
                var ListZones = driver.FindElements(By.CssSelector("tbody tr")).Select(j=>j.FindElements(By.CssSelector("td"))[2].FindElement(By.CssSelector("input.form-control")).GetAttribute("Value")).ToList();
                var ListZonesCopy = new List<string>(ListZones);
                ListZonesCopy.Sort();
                Assert.AreEqual(ListZones, ListZonesCopy);
                driver.Url = "http://localhost:8081/litecard/admin/?app=countries";

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