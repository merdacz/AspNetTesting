namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using System;
    using System.Globalization;
    using System.IO;

    using FluentAssertions;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Safari;
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

            driver.Navigate().GoToUrl("http://localhost/Net.Daczkowski.AspNetTesting.Web/");

            wait.Until(d => d.FindElement(By.ClassName("data-automation-buyitem")));
            driver.FindElement(By.ClassName("data-automation-buyitem")).Click();
            driver.FindElement(By.ClassName("data-automation-cartitem-name")).Text.Should().NotBeNullOrEmpty();
            driver.FindElements(By.ClassName("data-automation-message-pricechanged")).Should().BeEmpty();
            driver.FindElement(By.LinkText("Go to admin dashboard")).Click();

            wait.Until(d => d.FindElement(By.Name("newPrice")));
            var priceField = driver.FindElement(By.Name("newPrice"));
            var newPrice = decimal.Parse(priceField.GetAttribute("value"), CultureInfo.InvariantCulture) + 1;
            priceField.Clear();
            priceField.SendKeys(newPrice.ToString(CultureInfo.InvariantCulture));
            driver.FindElement(By.ClassName("data-automation-changeprice")).Click();
            driver.FindElement(By.LinkText("Go to home page")).Click();

            wait.Until(d => d.FindElement(By.ClassName("data-automation-message-pricechanged")));
            driver.FindElements(By.ClassName("data-automation-message-pricechanged")).Should().HaveCount(1);

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