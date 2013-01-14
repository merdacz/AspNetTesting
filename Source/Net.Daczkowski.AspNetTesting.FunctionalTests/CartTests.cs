namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using System;
    using System.IO;
    using FluentAssertions;
    using Net.Daczkowski.AspNetTesting.FunctionalTests.PageObjects;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Support.UI;

    public class CartTests
    {
        [Test]
        public void GivenCartWithProduct_WhenItsPriceChanges_ShouldRecalculateAndNotify()
        {
            IWebDriver driver = new FirefoxDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            // IWebDriver driver = new InternetExplorerDriver(GetDriverDirectory());
            // IWebDriver driver = new ChromeDriver(GetDriverDirectory());
            // IWebDriver driver = new SafariDriver();
            driver.Manage().Window.Maximize();

            var admin = new HomePageObject(driver)
                .BuyFirstItem()
                .GoToAdmin();

            var search = admin
                .ChangePriceForFirstItem()
                .GoToHomePage();

            search.PriceChangeNotifications.Should().HaveCount(1);

            driver.Quit();
        }

        private static string GetDriverDirectory()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var packagesDir = LocateDir(currentDir, "tools");

            return new DirectoryInfo(packagesDir).FullName;
        }

        private static string LocateDir(string currentDir, string dirToFind)
        {
            if (currentDir.ToLower().Contains(dirToFind))
            {
                return currentDir;
            }

            var dirPath = new DirectoryInfo(currentDir).Parent.FullName;
            string[] dirs = Directory.GetDirectories(dirPath);

            foreach (string dir in dirs)
            {
                if (dir.ToLower().Contains(dirToFind))
                {
                    return dir;
                }
            }

            return LocateDir(dirPath, dirToFind);
        }
    }
}