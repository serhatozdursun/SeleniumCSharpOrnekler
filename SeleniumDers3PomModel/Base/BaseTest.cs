using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SeleniumDers3PomModel.Base
{
    /// <summary>
    /// bu class ta tüm page    classlarında kullaılabilecek
    /// metodları yazıyoruz (ör: waitforelement, actions, javascript metodları gibi)
    /// </summary>
    class BaseTest
    {
        public void WaitForelement(RemoteWebDriver driver, By by, int second)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }

        public void MoveElement(RemoteWebDriver driver, IWebElement elm)
        {
            var action = new Actions(driver);
            action.MoveToElement(elm).Build().Perform();
        }
    }
}