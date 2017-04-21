using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace My_first_test_csharp
{
    [TestFixture]
    public class FirstWorkWithBrowse
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTest()
        {
            string SearchText = "Mazda";
            driver.Url = "http://www.drom.ru";
            driver.FindElement(By.Id("q")).SendKeys($"{SearchText}");
            driver.FindElement(By.Name("sa")).Click();

        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }

}
