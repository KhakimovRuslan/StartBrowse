using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace Login
{
    [TestFixture]
    public class LoginAsAdmin
    {
        private IWebDriver driver;
        //private WebDriverWait wait;
        [SetUp]
        public void startBrowse()
        {
            //driver = new FirefoxDriver();
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"C:\Program Files (x86)\Nightly\firefox.exe";
            options.UseLegacyImplementation = false;
            driver = new FirefoxDriver(options);
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void Enter()
        {
            String Text = "admin";
            driver.Url = "http://localhost:8081/litecard/admin";
            driver.FindElement(By.Name("username")).SendKeys(Text);
            driver.FindElement(By.Name("password")).SendKeys(Text);
            driver.FindElement(By.TagName("button")).Click();

        }
         [TearDown]
          public void stop()
          {
            driver.Quit();
            driver = null;
          }


      }
}

