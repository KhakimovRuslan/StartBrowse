using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace ClickAllElements_SeleniumGrid
{
    [TestFixture]
    public class ClickAllElements_SeleniumGrid
    {
        private IWebDriver driverIE;
        private IWebDriver driverChrome;

        [SetUp]

        public void Initialize()
        {
            Initialize(driverIE, DesiredCapabilities.InternetExplorer());
            Initialize(driverChrome, DesiredCapabilities.Chrome());

        }

        private void Initialize(IWebDriver driver,DesiredCapabilities capabilities)
        {


            driver = new RemoteWebDriver(new Uri("http://192.168.209.53:4444/"), capabilities);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void ClickAllElement_SeleniumGrid()
        {
            ClickAllElement_SeleniumGrid(driverIE);
            ClickAllElement_SeleniumGrid(driverChrome);

        }

        private void ClickAllElement_SeleniumGrid(IWebDriver driver)
        {
            String Text = "admin";
            driver.Url = "http://192.168.209.53:8081/litecard/admin";
            driver.FindElement(By.Name("username")).SendKeys(Text);
            driver.FindElement(By.Name("password")).SendKeys(Text);
            driverIE.FindElement(By.TagName("button")).Click();

            var rootAnchors = driverIE.FindElements(By.CssSelector("#app->a"));
            for (int i = 0; i < rootAnchors.Count; i++)
            {
                driverIE.FindElements(By.CssSelector("#app->a"))[i].Click();
                driverIE.FindElement(By.TagName("h1"));

                var firstLevelAnchors = driverIE.FindElements(By.CssSelector("ul.docs li:not(.selected) a"));
                for (int j = 0; j < firstLevelAnchors.Count; j++)
                {
                    driverIE.FindElements(By.CssSelector("ul.docs li:not(.selected) a"))[j].Click();
                    driverIE.FindElement(By.TagName("h1"));
                }

            }
        }

        [TearDown]
        public void stop()
        {
            driverIE.Quit();
            driverIE = null;
        }


    }
}