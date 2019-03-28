using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AlibabaFacebookLogin.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AlibabaFacebookLogin.Page
{
    public class HomePage:BaseTest
    {
        private RemoteWebDriver driver;

        public HomePage(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement loginMenu => driver.FindElementByCssSelector(".user-account-info");
        public IWebElement popupCloseBtn => driver.FindElementByCssSelector(".close-layer");
        public IWebElement facebookLogin => driver.FindElementByCssSelector("[title='facebook']");
        public IWebElement facebookUserNameInput => driver.FindElementById("email");
        public IWebElement facebookPasswordInput => driver.FindElementById("pass");
        public IWebElement facebookSinginBtn => driver.FindElementById("loginbutton");
        public IWebElement accountName => driver.FindElementByCssSelector(".account-name");
        public IWebElement searchKey => driver.FindElementById("search-key");
        public IWebElement searchBtn => driver.FindElementByCssSelector(".search-button");
        public HomePage CheckPageLoad()
        {
            Assert.IsTrue(loginMenu.Displayed);
            Assert.IsTrue(loginMenu.Displayed);
            return this;
        }

        public HomePage ClosePopUp()
        {
            try
            {
                WaitForElement(driver,10,By.CssSelector(".close-layer"));
                if (popupCloseBtn.Displayed && popupCloseBtn.Enabled)
                {
                    WaitForElementClickble(driver, 10, popupCloseBtn);
                    popupCloseBtn.Click();
                }
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e);
            }
           

            WaitInvisibilityOfElementLocated(driver, 30, By.CssSelector(".close-layer"));
            return this;
        }

        public HomePage MoveSinginMenu()
        {
            WaitForElement(driver, 15, By.CssSelector(".user-account-info"));
            MoveElement(driver,loginMenu);
            return this;
        }

        public HomePage ClickFacebookLogin()
        {
            WaitForElementClickble(driver,5,facebookLogin);
            facebookLogin.Click();
            return this;
        }



        public HomePage FacebookLogin()
        {
            var currentWindow = driver.CurrentWindowHandle;
            var winows = driver.WindowHandles;
            driver.SwitchTo().Window(winows.Last());

            facebookUserNameInput.SendKeys("5368361407");
            facebookPasswordInput.SendKeys("SeLiUm07_?");
            facebookSinginBtn.Click();
            driver.SwitchTo().Window(currentWindow);
            return this;
        }

        public HomePage IsLogged()
        {
            WaitForElement(driver,30, By.CssSelector(".account-name"));
            Assert.IsTrue(accountName.Displayed);
            return this;
        }

        public HomePage TypeSearchKey()
        {
            searchKey.SendKeys("leather zippo lighter");
            return this;
        }

        public SearchResultPage ClickSearchButton()
        {
            WaitForElementClickble(driver,10,searchBtn);
            searchBtn.Click();
            return new SearchResultPage(driver);
        }
    }
}
