using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace CheckEmptyLogOfBrowse
{
    public class MakeScreenShots
    {
            protected IWebDriver driver;
       
        public void MakeScreenShot(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {

                var screenshot = driver.TakeScreenshot();
                var filePath = "C:\\Tools\\screenshot_errors";
                screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
                throw;
            }
        }
    }
}
