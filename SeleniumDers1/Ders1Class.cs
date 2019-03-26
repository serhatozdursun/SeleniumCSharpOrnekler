using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SeleniumDers1
{
    [TestFixture]
    public class Ders1Class
    {
        private RemoteWebDriver driver;

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

            driver.Navigate().GoToUrl("https://letskodeit.teachable.com/");
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }


        [Test]
        public void XpathAndCss()
        {
            driver.Navigate().GoToUrl("https://www.apsiyon.com");
            //css class name 
            var log = driver.FindElementByCssSelector(".svg-logo");
            //xpath class name
            var xpathLogo = driver.FindElementByXPath("//*[@class='logo-section']");
            // selenium class name
            var classname = driver.FindElementByClassName("logo-section");

            var loogo1 = driver.FindElement(By.CssSelector(".svg-logo"));
        }

        [Test]
        public void SeleniumClick()
        {
            WaitForelement(By.CssSelector("[href='/pages/practice']"), 5);
            var practice = driver.FindElementByCssSelector("[href='/pages/practice']");
            practice.Click();
            WaitForelement(By.CssSelector("[for='bmw']"), 5);
            var bmwoption = driver.FindElementByCssSelector("[for='bmw']");
            Assert.IsTrue(bmwoption.Displayed);
        }


        //Radio buttons
        [Test]
        public void RadioButtonExample()
        {
            SeleniumClick();

            // Tüm radio buttonları buluyor
            var radiobuttons = driver.FindElementsByCssSelector("input[type = 'radio']");

            // ilk radio buttonun seçili olmadığını doğruluyor
            Assert.IsFalse(radiobuttons[0].Selected);

            // tüm radio butonların seçilebilir olduğunu doğruluyor
            foreach (var radioButton in radiobuttons)
            {
                radioButton.Click();
                Assert.IsTrue(radioButton.Selected);
            }
        }

        [Test]
        public void SelectByValue()
        {
            SeleniumClick();

            // select list elementini buluyor ve bir select sınıfı oluşturuyor
            var listSelect = new SelectElement(driver.FindElementById("carselect"));

            //value ile select listen seçim yapıyor.
            listSelect.SelectByValue("benz");

            // Benz 'in seçildiğini doğruluyor
            Assert.IsTrue(listSelect.SelectedOption.Text.Equals("Benz"));
        }

        [Test]
        public void SelectWithoutSelectClass()
        {
            SeleniumClick();

            // html tag'i select olmayan listboxlar da seleniumun 
            //select sınıfını kullanamaycağımız için
            //öncelikle list boxa tıklayıp daha sonra çıkan seçeneklerden birini
            //tılayarak seçmemiz gerekiyor

            //select boxı tıklıyor
            WaitForelement(By.Id("carselect"), 5);
            var elmSelect = driver.FindElementById("carselect");
            elmSelect.Click();

            //value su benz olan seçeneği tıklıyor
            elmSelect.FindElement(By.CssSelector("[value='benz']")).Click();

            //value su benz olan web element seçili durumda mı kontrol ediyor
            Assert.IsTrue(elmSelect.FindElement(By.CssSelector("[value='benz']")).Selected);
        }

        [Test]
        public void SelectByIndex()
        {
            SeleniumClick();
            WaitForelement(By.Id("carselect"), 5);

            // select içerisinden index ile de seçim yapabiliriz.
            // index 0 dan başladığı için ilk seçeneği sıfır var sayarak seçim yapabilirsiniz

            // select list elementini buluyor ve bir select sınıfı oluşturuyor
            var listSelect = new SelectElement(driver.FindElementById("carselect"));

            // indexi bir olan yani 2. seçeneği seçiyoruz
            listSelect.SelectByIndex(1);

            //tüm optionları ReadOnlyCollection<IWebElement> tipinde getiriyoruz
            var listOptions = driver.FindElementsByCssSelector("#carselect>option");

            //collection içinden indexi 1 olan seçili mi kontrol ediyoruz
            Assert.IsTrue(listOptions[1].Selected);
        }

        [Test]
        public void SelectByVisibleText()
        {
            SeleniumClick();
            WaitForelement(By.Id("carselect"), 5);

            // selecteki seçenkleri sayfada gözüken textleri ile de seçebiliriz.

            // select list elementini buluyor ve bir select sınıfı oluşturuyor
            var listSelect = new SelectElement(driver.FindElementById("carselect"));

            //Sayfada gözüken text i Honda olan seçeneği seçiyor
            listSelect.SelectByText("Honda");

            //Sayfada gözüken text i Honda olan seçili mi kontrol ediyor
            Assert.IsTrue(listSelect.SelectedOption.Text.Equals("Honda"));
        }

        [Test]
        public void CheckAllOptionsSelectable()
        {
            SeleniumClick();
            WaitForelement(By.Id("carselect"), 5);
            //Seçeneklerin value, index veya text lerine bağlı kalmadan
            //tüm hepsi seçilebiliyor mu kontrol edebiliriz.

            // select list elementini buluyor ve bir select sınıfı oluşturuyor
            var listSelect = new SelectElement(driver.FindElementById("carselect"));

            //Tüm seçenekleri foreach döngüsü ile teker teker çekiyoruz
            //Seçenekler değiştiğinde veya artığında bu sayede hata almaycağız
            foreach (var option in listSelect.Options)
            {
                //döngü select in içinde ki tüm seçenekleri teker teker webelement tipinde option
                //değişkenine atayacak

                // option nın textini çekip text ile o optionun seçilmesini sağlıyoruz.
                listSelect.SelectByText(option.Text);

                //option seçili durumda mı kontrol ediyoruz.
                Assert.IsTrue(option.Selected);
            }
        }

        public void WaitForelement(By by, int second)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(second));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }
    }
}