using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Linq;
using System.IO;
using OpenQA.Selenium.Remote;

namespace AddProduct
{
    [TestFixture]
    public class TestRunningCloud
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void startBrowse()
        {
            DesiredCapabilities capability = DesiredCapabilities.Firefox();
            capability.SetCapability("version", "53");
            capability.SetCapability("platform", "WIN8");
            capability.SetCapability("browserstack.user", "ruslan269");
            capability.SetCapability("browserstack.key", "wd36VsC7Nqd9iqqFBcfR");
            capability.SetCapability("build", "First build");
            capability.SetCapability("browserstack.debug", "true");

            driver = new RemoteWebDriver(
              new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability
            );
            //driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void new_product()
        {
            driver.Url = "http://selenium2.ru";
        }




        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
