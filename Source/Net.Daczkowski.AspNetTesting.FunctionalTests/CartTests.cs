namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using System;
    using System.Globalization;

    using FluentAssertions;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    public class CartTests
    {
        [Test]
        public void GivenCartWithProduct_WhenItsPriceChanges_ShouldRecalculateAndNotify()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

            driver.Navigate().GoToUrl("http://localhost/Net.Daczkowski.AspNetTesting.Web/");
            driver.FindElement(By.ClassName("data-automation-buyitem")).Click();
            driver.FindElement(By.ClassName("data-automation-cartitem-name")).Text.Should().NotBeNullOrEmpty();
            driver.FindElements(By.ClassName("data-automation-message-pricechanged")).Should().BeEmpty();
            driver.FindElement(By.LinkText("Go to admin dashboard")).Click();

            var priceField = driver.FindElement(By.Name("newPrice"));
            var newPrice = decimal.Parse(priceField.GetAttribute("value"), CultureInfo.InvariantCulture) + 1;
            priceField.Clear();
            priceField.SendKeys(newPrice.ToString(CultureInfo.InvariantCulture));
            driver.FindElement(By.ClassName("data-automation-changeprice")).Click();

            driver.FindElement(By.LinkText("Go to home page")).Click();
            driver.FindElements(By.ClassName("data-automation-message-pricechanged")).Should().HaveCount(1);

            driver.Quit();
        }
    }
}