using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Threading;

namespace Click_All_Elements
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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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