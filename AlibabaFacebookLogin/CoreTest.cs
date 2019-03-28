using System;
using AlibabaFacebookLogin.Base;
using AlibabaFacebookLogin.Page;
using NUnit.Framework;

namespace AlibabaFacebookLogin
{
    public class CoreTest:BasePage
    {

        [Test,Retry(2)]
        public void FacebookLoginTest()
        {
            var homePage = new HomePage(driver);
            homePage.CheckPageLoad()
                .ClosePopUp()
                .MoveSinginMenu()
                .ClickFacebookLogin()
                .FacebookLogin()
                .ClosePopUp()
                .IsLogged()
                .TypeSearchKey()
                .ClickSearchButton()
                .SearchPageIsLoad()
                .CheckItemCount();
        }
    }
}
