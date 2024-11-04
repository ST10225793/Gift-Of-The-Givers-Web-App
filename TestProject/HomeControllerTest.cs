using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using System.IO;
using System.Reflection;

namespace GiftOfTheGivers.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        private IWebDriver driver;
        private string appURL;

        public HomeControllerTest()
        {
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void CheckUserTest()
        {
            driver.Navigate().GoToUrl(appURL + "/");
            Assert.IsTrue(driver.Title.Contains("User List"), "Verified title of the page");
        }

        [TestInitialize()]
        public void SetupTest()
        {
            appURL = "https://localhost:44374/";
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [TestCleanup()]
        public void myTestCleanUp()
        {
            driver.Quit();
        }
    }

}