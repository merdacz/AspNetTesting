namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using System;
    using System.Configuration;
    using System.Globalization;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public class AdminPageObject
    {
        private readonly IWebDriver driver;

        private readonly WebDriverWait wait;

        public AdminPageObject(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(5));
            if (this.driver.Url == ConfigurationManager.AppSettings["BaseUrl"] + "Admin")
            {
                return;
            }

            this.driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["BaseUrl"] + "Admin");
        }

        private IWebElement PriceEntry
        {
            get
            {
                this.wait.Until(d => d.FindElement(By.Name("newPrice")));
                return this.driver.FindElement(By.Name("newPrice"));        
            }
        }

        private IWebElement ChangePriceButton
        {
            get
            {
                return this.driver.FindElement(By.ClassName("data-automation-changeprice"));
            }
        }

        private IWebElement HomePageLink
        {
            get
            {
                return this.driver.FindElement(By.LinkText("Go to home page"));
            }
        }

        public AdminPageObject ChangePriceForFirstItem()
        {
            var oldValue = this.PriceEntry.GetAttribute("value");
            var newValue = decimal.Parse(oldValue, CultureInfo.InvariantCulture) + 1;
            this.PriceEntry.Clear();
            this.PriceEntry.SendKeys(newValue.ToString(CultureInfo.InvariantCulture));
            this.ChangePriceButton.Click();
            return this;
        }

        public HomePageObject GoToHomePage()
        {
            this.HomePageLink.Click();
            return new HomePageObject(this.driver);
        }
    }
}