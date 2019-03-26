using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SeleniumDers3PomModel.Base;

namespace SeleniumDers3PomModel.Page
{
    /// <summary>
    /// page classlarımız da o sayfada yapılacak test
    /// işlemlerinin metotlarını yazacağız
    /// aktif direvr ı alabilmek   için dışarıdan driver alan
    /// yapılandırıcı bir metod (Constructor) tanımlyacağız
    /// testlerimiz için yazdığımız ortak metotları kullanabilmek için (wait metodları, java scriptlerin metodları, actions metodları, v.b.)
    /// bu class ı da BaseTest classına refere ediyoruz (extend) bu sayede metodları miras olarak alıyor
    /// </summary>
    class AdminPage : BaseTest
    {
        private RemoteWebDriver driver;

        /// <summary>
        /// yapılandırıcı metod  bu metod içerisinde dışarıdan aldığımız
        /// driverı class ın kendi driverı ile eşliyoruz
        /// </summary>
        /// <param name="driver"></param>
        public AdminPage(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement mailInput => driver.FindElementByCssSelector("[name='mail']");

        public AdminPage AssertAdminPageLoad()
        {
            WaitForelement(driver, By.CssSelector("[name='mail']"), 5);
            Assert.IsTrue(mailInput.Displayed);
            return this;
        }
    }
}