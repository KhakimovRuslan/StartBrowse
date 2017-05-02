using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Check_stickers
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
            driver.Url = "http://localhost:8081/litecard/";

            var anchors = driver.FindElements(By.CssSelector(".nav-tabs li>a"));
            foreach (var a in anchors)
            {
                var links = driver.FindElements(By.CssSelector(a.GetAttribute("hash") + " .link"));
                foreach (var link in links)
                {
                    var stikers = link.FindElements(By.CssSelector(".sticker"));
                    Assert.AreEqual(stikers.Count, 1);
                }
            }



            //var tab = driver.FindElements(By.CssSelector("div#box-campaigns .link"));
            //for (int i = 0; i < tab.Count; i++)
            //{
            //    var stikers = tab[i].FindElements(By.CssSelector(".sticker"));
            //    Assert.AreEqual(stikers.Count, 1);
            //}
            //tab = driver.FindElements(By.CssSelector("div#box-most-popular .link"));
            //for (int i = 0; i < tab.Count; i++)
            //{
            //    var stikers = tab[i].FindElements(By.CssSelector(".sticker"));
            //    Assert.AreEqual(stikers.Count, 1);
            //}
            //tab = driver.FindElements(By.CssSelector("box-latest-products .link"));
            //for (int i = 0; i < tab.Count; i++)
            //{
            //    var stikers = tab[i].FindElements(By.CssSelector(".sticker"));
            //    Assert.AreEqual(stikers.Count, 1);
            //}
        }


        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }


    }
}