using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Selenium
{
    [TestFixture]
    public class Ders2Class
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

            driver.Navigate().GoToUrl("https://learn.letskodeit.com/p/practice");
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }


        [Test]
        public void MultipleSelect()
        {
            //multiple select elementi bulunuyor 
            var elm = driver.FindElementById("multiple-select-example");
            //select sınıfı tanımlanıyor
            var select = new SelectElement(elm);

            // Tüm seçilebilir seçeneklerin tıklanması sağlanıyor
            foreach (var option in select.Options)
            {
                select.SelectByText(option.Text);
            }

            //seçilebilir option sayısı alınıyor
            var selected = select.AllSelectedOptions;

            //seçilen option sayısı ile seçilebilir option sayısı aynı mı kontrol ediliyor
            Assert.IsTrue(select.Options.Count == selected.Count);
        }

        [Test]
        public void OpenNewWindow()
        {
            var openWindow = driver.FindElementById("openwindow");
            // aktif pencerenin id'si alınyor
            var currentWindow = driver.CurrentWindowHandle;

            //Yeni pencere açacak buton tıklanyor
            openWindow.Click();

            //aktif tüm pencere id leri alınıyor
            var handels = driver.WindowHandles;

            //sayfada ki bir hatadan dolayı 2 saniye kadar testi bekletiyoruz. Normalde sleep kullanmamaya çalışın
            Thread.Sleep(TimeSpan.FromSeconds(2));

            //açılan en son pencereye geçiş yapılıyor
            driver.SwitchTo().Window(handels.Last());

            //sadece açılan yeni pencerede olan bir web element ile sayfanın açıldığı 
            //doğrulanıyor
            Assert.IsTrue(driver.FindElements(By.CssSelector("#search-courses")).Count > 0);
            //Yeni açılan pencere kapatılıyor
            driver.Close();
            //ana pencreye geri dönülüyor
            driver.SwitchTo().Window(currentWindow);
            //ana pencereye geri dönüldüğü sonradan açılan sayfada olan bir elementin
            //aktif olan sayfada olmadığını kontrol ederek doğrılanıyor
            Assert.IsFalse(driver.FindElements(By.CssSelector("#search-courses")).Count > 0);
        }


        [Test]
        public void OpenNewTab()
        {
            var openNewTab = driver.FindElementById("opentab");
            // aktif pencerenin id'si alınyor
            var currentWindow = driver.CurrentWindowHandle;

            //Yeni tab açacak buton tıklanyor
            openNewTab.Click();

            //aktif tüm pencere id leri alınıyor
            var handels = driver.WindowHandles;

            //sayfada ki bir hatadan dolayı 2 saniye kadar testi bekletiyoruz. Normalde sleep kullanmamaya çalışın
            Thread.Sleep(TimeSpan.FromSeconds(2));

            //açılan en son pencereye geçiş yapılıyor
            driver.SwitchTo().Window(handels.Last());

            //sadece açılan yeni pencerede olan bir web element ile sayfanın açıldığı 
            //doğrulanıyor
            Assert.IsTrue(driver.FindElements(By.CssSelector("#search-courses")).Count > 0);

            //Yeni açılan pencere kapatılıyor
            driver.Close();
            driver.SwitchTo().Window(currentWindow);

            //ana pencereye geri dönüldüğü sonradan açılan sayfada olan bir elementin
            //aktif olan sayfada olmadığını kontrol ederek doğrılanıyor
            Assert.IsFalse(driver.FindElements(By.CssSelector("#search-courses")).Count > 0);
        }

        [Test]
        public void AlertBox()
        {
            var alertbtn = driver.FindElementById("alertbtn");
            var name = driver.FindElementById("name");

            // inputa giriş yapılacak string tanımlanıyor
            var sname = "Software Testing Turkey";

            // inputa string değer giriliyor
            name.SendKeys(sname);

            //gelmesini beklediğimiz uyarı mesajını tanımlıyoruz
            var alert = $"Hello {sname}, share this practice page and share your knowledge";

            //uyarıyı aktif edecek butonu tıklıyoruz
            alertbtn.Click();

            //sayfada açılan uyarıyı webelement olarak alıyoruz
            var palert = driver.SwitchTo().Alert();

            //uyarı nın metini ile beklenen mesajın metini aynı mı kontrol ediyoruz
            Assert.IsTrue(alert.Equals(palert.Text));

            //uyarıyı tamam'a tıklayrak kapatıyoruz
            driver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void ConfirmBox()
        {
            var confrmbtn = driver.FindElementById("confirmbtn");
            var name = driver.FindElementById("name");

            // inputa giriş yapılacak string tanımlanıyor
            var sname = "Software Testing Turkey";

            // inputa string değer giriliyor
            name.SendKeys(sname);

            //gelmesini beklediğimiz confrim mesajını tanımlıyoruz
            var alert = $"Hello {sname}, Are you sure you want to confirm?";

            //confrim mesajını aktif edecek butonu tıklıyoruz
            confrmbtn.Click();

            //sayfada açılan confirm i webelement olarak alıyoruz
            var palert = driver.SwitchTo().Alert().Text;

            //confrim in metini ile beklenen mesajın metini aynı mı kontrol ediyoruz
            Assert.IsTrue(alert.Equals(palert));

            //confrimi onaylamadan kapatıyoruz
            driver.SwitchTo().Alert().Dismiss();
        }

        [Test]
        public void DataTable()
        {
            // sayfada ki datatable web element olarak bir değişkene atıyoruz
            var dataTable = driver.FindElementById("product");

            //tablonun içinden satırları alıyoruz
            var row = dataTable.FindElements(By.CssSelector("tr"));

            //tablonun hücrelerini yazacağımız 
            //bir list nesnesi tanımlıyoruz
            List<IWebElement[]> tableList = new List<IWebElement[]>();


            //sayfada ki satır sayısı kadar dönecek bir for döngüsü kuruyoruz
            for (int i = 0; i < row.Count; i++)
            {
                // satırların içinde ki hücreleri çekiyoruz
                // eğer i=0 sa bu satır başlık olduğu için hücreleri th tagından buluyoruz
                var cellc = i == 0
                    ? row[i].FindElements(By.CssSelector("th"))
                    : row[i].FindElements(By.CssSelector("td"));

                // alcağımız hücreleri web element olarak tutabilmek için
                //web element arrayi tanımlıyoruz
                IWebElement[] cellText = new IWebElement[cellc.Count];

                for (int j = 0; j < cellText.Length; j++)
                {
                    //hücreleri teker teker tanımladığımız
                    //web element arrayine atıyoruz
                    cellText[j] = cellc[j];
                }

                //satır içinde ki webelementler için oluşturduğumuz
                //webelement array ini daha önce tanımladığımız listeye ekliyoruz
                tableList.Add(cellText);
            }

            //artık data table webelement array listi olarak elimizde 
            //istediğimiz gibi doğrulama veya test yapabiliriz.

            //2. satırın 3. hücresinin 35 olduğunu doğruluyoruz
            Assert.IsTrue(tableList[1][2].Text.Equals("35"));

            // ikinci satırın 3. kolonun sizeını alıyoruz
            var elementSize = tableList[1][2].Size;
            //size istediğimiz aralıkta mı kontrol ediyoruz
            Assert.IsTrue(elementSize.Height >= 26 && elementSize.Height <= 28);
        }

        [Test]
        public void GetAttribute()
        {
            var hideBtn = driver.FindElementById("hide-textbox");
            var showbtn = driver.FindElementById("show-textbox");
            var input = driver.FindElementById("displayed-text");


            hideBtn.Click();

            //style attribute nün değerini çekiyoruz
            var style = input.GetAttribute("style");

            //bekleidğimiz değerle aynı mı kontrol ediyoruz
            Assert.IsTrue(style.Contains("none"));

            //attribute ün değerini değiştirekcek aksiyonu alıyoruz 'show tıklıyoruz'
            showbtn.Click();

            //style attribute nün değerini tekrar çekiyoruz
            style = input.GetAttribute("style");

            //bekleidğimiz değişiklik olmuş mu kontrol ediyoruz
            Assert.IsTrue(style.Contains("block"));
        }

        [Test]
        public void MoveElement()
        {
            //mouse hover yapılacak element tanımlanıyor
            var elm = driver.FindElementById("mousehover");

            //action sınıfı tanımlanıyor
            var actions = new Actions(driver);
            var reloded = driver.FindElementByXPath("//a[contains(.,'Top')]");

            // tanımladığımız webelemente mouse hover yapıyoruz
            actions.MoveToElement(elm).Build().Perform();

            //açılan menüden bir elemente tıklıyoruz
            reloded.Click();
        }

        [Test]
        public void SwitchToIFrame()
        {
            var iframe = driver.FindElementById("courses-iframe");

            driver.SwitchTo().Frame(iframe);
            Assert.IsTrue(driver.FindElementsById("search-courses").Count > 0);
            driver.SwitchTo().DefaultContent();
            Assert.IsFalse(driver.FindElementsById("search-courses").Count > 0);
        }
    }
}