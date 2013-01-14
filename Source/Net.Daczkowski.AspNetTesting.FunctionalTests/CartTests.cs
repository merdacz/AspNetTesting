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
            var driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

            driver.Navigate().GoToUrl("http://localhost/Net.Daczkowski.AspNetTesting.Web/");
            driver.FindElementByClassName("data-automation-buyitem").Click();
            driver.FindElementByClassName("data-automation-cartitem-name").Text.Should().NotBeNullOrEmpty();
            driver.FindElementsByClassName("data-automation-message-pricechanged").Should().BeEmpty();
            driver.FindElementByLinkText("Go to admin dashboard").Click();

            var priceField = driver.FindElementByName("newPrice");
            var newPrice = decimal.Parse(priceField.GetAttribute("value"), CultureInfo.InvariantCulture) + 1;
            priceField.Clear();
            priceField.SendKeys(newPrice.ToString(CultureInfo.InvariantCulture));
            driver.FindElementByClassName("data-automation-changeprice").Click();

            driver.FindElementByLinkText("Go to home page").Click();
            driver.FindElementsByClassName("data-automation-message-pricechanged").Should().HaveCount(1);

            driver.Quit();
        }
    }
}