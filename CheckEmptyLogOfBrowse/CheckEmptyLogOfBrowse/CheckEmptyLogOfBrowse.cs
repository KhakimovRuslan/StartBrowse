using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Extensions;
using System.IO;

namespace CheckEmptyLogOfBrowse
{

    [TestFixture]
    public class CheckEmptyLogOfBrowse
    {
        private IWebDriver driver;

        [SetUp]
        public void CheckEmptyLog()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }
        public void MakeScreenShot(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {

                var screenshot = driver.TakeScreenshot();
                var filePath = @"C:\Tools\screenshot_errors\";
                var FileName = DateTime.Now.ToString("dd MMM HH_mm_ss");

                screenshot.SaveAsFile(filePath + FileName + ".png", ScreenshotImageFormat.Png);
                File.WriteAllText(filePath + FileName + ".txt", ex.Message + " " + ex.StackTrace);
                throw;
            }
        }
        [Test]
        public void CheckEmptyLogTest()
        {

            MakeScreenShot(() =>
            {
                string Text = "admin";
                driver.Url = "http://localhost:8081/litecard/admin/";
                driver.FindElement(By.Name("username")).SendKeys(Text);
                driver.FindElement(By.Name("password")).SendKeys(Text);
                driver.FindElement(By.TagName("button")).Click();
                driver.FindElements(By.CssSelector("#app-"))[1].Click();
                driver.FindElement(By.LinkText("Rubber Ducks")).Click();
                var products = driver.FindElements(By.CssSelector("td a")).Where(y => string.IsNullOrEmpty(y.GetAttribute("title"))).Skip(3).ToList();
                for (int i = 0; i < products.Count(); i++)
                {
                    driver.FindElements(By.CssSelector("td a")).Where(y => string.IsNullOrEmpty(y.GetAttribute("title"))).
                        Skip(3).ElementAt(i).Click();

                    foreach (var l in driver.Manage().Logs.GetLog(LogType.Driver))
                    {
                        System.Diagnostics.Debug.WriteLine(l);
                    }
                    driver.FindElement(By.CssSelector("[name=cancel]")).Click();
                }
            });

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

            
    }
}
