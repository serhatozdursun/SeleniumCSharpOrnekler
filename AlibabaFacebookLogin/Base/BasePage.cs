using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AlibabaFacebookLogin.Base
{
    [TestFixture]
    public class BasePage
    {
        public RemoteWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Driver path
            string driverDirectory = Path.Combine(Path.GetDirectoryName
                (Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory))), "Driver");
            //driver başlatma
            driver = new ChromeDriver(driverDirectory);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMilliseconds(50000);

            driver.Navigate().GoToUrl("https://tr.aliexpress.com/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit(); 
        }

    }
}
