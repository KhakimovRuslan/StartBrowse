using System;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Threading;
using AutomatedTester.BrowserMob;
using AutomatedTester.BrowserMob.HAR;

namespace ExampleTestProxy
{
    [TestFixture]
    public class ExampleTestProxy
    {
        private IWebDriver driver;
        //private WebDriverWait wait;
        private Server server;
        private Client client;
        //private Proxy proxy;

        //private ChromeOptions chromeOptions;

        [SetUp]
        public void TestProxy()
        {
            server = new Server(@"C:\download\browsermob-proxy-2.1.4-bin\browsermob-proxy-2.1.4\bin\browsermob-proxy.bat", 8085);
            
            server.Start();
            client = server.CreateProxy();
            client.NewHar("Load Test Numbers");
            var seleniumProxy = new Proxy { HttpProxy = client.SeleniumProxy };
            var profile = new FirefoxProfile();
            profile.SetProxyPreferences(seleniumProxy);

            //    Server server = new Server(@"C:\Users\user.name\Desktop\BMP\bin\browsermob-proxy.bat");

            // Navigate to the page to retrieve performance stats for
            driver = new FirefoxDriver(profile);
            //Driver.Navigate().GoToUrl("http://www.google.co.uk");

            //proxy = new Proxy();                    
            //proxy.Kind = ProxyKind.Manual;
            //proxy.HttpProxy = "localhost:8888";
            //ChromeOptions options = new ChromeOptions();
            //driver = new ChromeDriver(options);
            //chromeOptions.Proxy = proxy;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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
            // Get the performance stats
            HarResult harData = client.GetHar();
            // Do whatever you want with the metrics here. Easy to persist 
            Log log = harData.Log;
            Entry[] entries = log.Entries;

            var file = new System.IO.StreamWriter("C:\\Logs\\test.txt");

            foreach (var entry in entries)
            {
                Request request = entry.Request;
                Response response = entry.Response;
                var url = request.Url;
                var time = entry.Time;
                var status = response.Status;
                Console.WriteLine("Url: " + url + " - Time: " + time + " Response: " + status);

                file.WriteLine("Url: " + url + " - Time: " + time + " Response: " + status);
            }

            file.Close();

            driver.Quit();
            driver = null;
            server.Stop();
        }


    }
}