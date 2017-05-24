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
        private IWebDriver driver;
        //private WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            DesiredCapabilities capabilities = DesiredCapabilities.InternetExplorer();

            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabilities);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

        }
        [Test]
        public void ClickAllElement_SeleniumGrid()
        {
            String Text = "admin";
            driver.Url = "http://192.168.209.53:8081/litecard/admin";
            driver.FindElement(By.Name("username")).SendKeys(Text);
            driver.FindElement(By.Name("password")).SendKeys(Text);
            driver.FindElement(By.TagName("button")).Click();

            var rootAnchors = driver.FindElements(By.CssSelector("#app->a"));
            for (int i = 0; i < rootAnchors.Count; i++)
            {
                driver.FindElements(By.CssSelector("#app->a"))[i].Click();
                driver.FindElement(By.TagName("h1"));

                var firstLevelAnchors = driver.FindElements(By.CssSelector("ul.docs li:not(.selected) a"));
                for (int j = 0; j < firstLevelAnchors.Count; j++)
                {
                    driver.FindElements(By.CssSelector("ul.docs li:not(.selected) a"))[j].Click();
                    driver.FindElement(By.TagName("h1"));
                }

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