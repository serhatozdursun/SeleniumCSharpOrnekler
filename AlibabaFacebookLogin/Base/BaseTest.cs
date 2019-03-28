using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace AlibabaFacebookLogin.Base
{
    public class BaseTest
    {
        /// <summary>
        /// belirtilen elemente doğru mouse hover yapar
        /// </summary>
        /// <param name="driver"> remote web driver </param>
        /// <param name="element"> web element yolla</param>
        public void MoveElement(RemoteWebDriver driver,IWebElement element)
        {
            var actions = new Actions(driver);
            actions.MoveToElement(element).Build().Perform();

        }

        public void WaitForElementClickble(RemoteWebDriver driver,int second,IWebElement element)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public void WaitForElement(RemoteWebDriver driver, int second, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }

        public void WaitInvisibilityOfElementLocated(RemoteWebDriver driver, int second, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }
    }
}
