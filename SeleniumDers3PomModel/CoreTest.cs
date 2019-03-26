using System;
using NUnit.Framework;
using SeleniumDers3PomModel.Base;
using SeleniumDers3PomModel.Page;

namespace SeleniumDers3PomModel
{
    /// <summary>
    /// Bu class ta test  senaryolarımızı tanımlayacağız
    /// aktif driverı bu classta kullanabilmek ve page class larımızı gönderebilmek için
    /// bu classı base class a refere ediyoruz(extend) bu sayede
    /// aktif driver ı miras alacak
    /// </summary> 
    [TestFixture]
    public class CoreTest : BasePage
    {
        /// <summary>
        /// test metodu
        /// </summary>
        [Test]
        public void Test()
        {
            // page classımızı tanımlarken base class ta 
            // tanımlanan driverı gönderiyoruz
            var hp = new HomePage(driver);
            hp.AssertPageLoad()
                .MoveLoginMenu()
                .ClickAdminSingin()
                .AssertAdminPageLoad();
        }
    }
}