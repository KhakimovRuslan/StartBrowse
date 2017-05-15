using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Linq;

namespace Create_of_user
{
    [TestFixture]
    public class registration_of_user
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
        public void Create()
        {
            string fio = "ivanov";
            string pass = "P@ssw0rd";
            Random j = new Random();
            int i = j.Next(0, 100000);
            string mail = $"test{i}@test.test";
            driver.Url = "http://localhost:8081/litecard/en/create_account";
            driver.FindElement(By.CssSelector("[name = firstname]")).SendKeys(fio);
            driver.FindElement(By.CssSelector("[name = lastname]")).SendKeys(fio);
            driver.FindElement(By.CssSelector("[name = address1]")).SendKeys("test");
            driver.FindElement(By.CssSelector("[name = country_code]")).Click();
            driver.FindElement(By.CssSelector("[name = country_code] [value = US]")).Click();
            driver.FindElement(By.CssSelector(".row [name = email]")).SendKeys(mail);
            driver.FindElement(By.CssSelector(".row [name = password]")).SendKeys(pass);
            driver.FindElement(By.CssSelector("[name = confirmed_password]")).SendKeys(pass);
            driver.FindElement(By.CssSelector("[name = create_account]")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.Url = "http://localhost:8081/litecard/en/logout";
            driver.FindElement(By.CssSelector("[name = email]")).SendKeys(mail);
            driver.FindElement(By.CssSelector("[name = password]")).SendKeys(pass);
            driver.FindElement(By.CssSelector("[name = login]")).Click();
            driver.FindElement(By.LinkText("Logout")).Click(); ;


        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
