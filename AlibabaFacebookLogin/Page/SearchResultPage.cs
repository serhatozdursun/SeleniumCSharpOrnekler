using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AlibabaFacebookLogin.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AlibabaFacebookLogin.Page
{
    public class SearchResultPage : BaseTest
    {
        private RemoteWebDriver driver;

        public IReadOnlyCollection<IWebElement> items => driver.FindElementsByCssSelector("#hs-list-items>li");

        public IReadOnlyCollection<IWebElement> blowList =>
            driver.FindElementsByCssSelector("#hs-below-list-items>ul>li");
        public IWebElement searchCount => driver.FindElementByCssSelector(".search-count");
        public IWebElement nextPage => driver.FindElementByCssSelector(".page-next");
        public IWebElement viewList => driver.FindElementById("view-list");
        public IReadOnlyCollection<IWebElement> noticeBody => driver.FindElementsByCssSelector(".ui-notice-body");
        public SearchResultPage(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        public SearchResultPage SearchPageIsLoad()
        {
            WaitForElement(driver, 15, By.Id("view-list"));
            Assert.IsTrue(viewList.Displayed);
            return this;
        }

        public int GetItemCount()
        {
            try
            {
                viewList.Click();
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e);
            }
            
            
            int count = 0;
            if (items.Count > 0)
            {
                count = items.Count;
                count += blowList.Count;
            }
            return count;
        }

        public SearchResultPage CheckItemCount()
        {
            int sItemCount = 0;


            while (driver.FindElementsByCssSelector(".page-end").Count == 0)
            {

                if (noticeBody.Count == 0)
                {
                    sItemCount += GetItemCount();
                    if (driver.FindElements(By.CssSelector(".page-next")).Count > 0)
                        nextPage.Click();
                }
                
            }

            sItemCount += GetItemCount();

            var pageCount = searchCount.Text;

            var itemCount = Int32.Parse(pageCount);
            Assert.IsTrue(sItemCount == itemCount || sItemCount + 1 == itemCount);
            return this;
        }

    }
}
