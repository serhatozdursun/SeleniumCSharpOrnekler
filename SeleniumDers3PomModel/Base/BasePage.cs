using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;


/// <summary>
/// Bu class ta test başlamadan
/// yapılması gereken işlemleri tanımlayacağız (ör: driver tanımı base url time out süreleri)
/// ve test sonrası işlemleri yapılır (ör: driver.quit)
/// </summary>
namespace SeleniumDers3PomModel.Base
{
    /// <summary>
    /// Driver, timeout ve base url in tanımlandığı metot
    /// burada ki driver test   sürecinde aktif olarak kullanılacak
    /// o yüzden driver ve bu class public olmalı
    /// </summary>
    [TestFixture]
    public class BasePage
    {
        public static RemoteWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            // Driver path
            string driverDirectory = Path.Combine(Path.GetDirectoryName
                (Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory))), "Driver");
            //driver başlatma
            driver = new ChromeDriver(driverDirectory);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMilliseconds(50000);
            driver.Navigate().GoToUrl("https://www.apsiyon.com");
        }

        /// <summary>
        /// test sonrası işlemleri
        /// </summary>
        [TearDown]
        public void Close()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Quit();
        }
    }
}