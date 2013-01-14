namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public class HomePageObject
    {
        private readonly IWebDriver driver;

        private readonly WebDriverWait wait;

        public HomePageObject(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(5));
            if (this.driver.Url == ConfigurationManager.AppSettings["BaseUrl"])
            {
                return;
            }

            this.driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["BaseUrl"]);
        }

        public IWebElement FirstCartItem
        {
            get
            {
                return this.driver.FindElement(By.ClassName("data-automation-cartitem-name"));
            }
        }

        public IList<IWebElement> PriceChangeNotifications
        {
            get
            {
                return this.driver.FindElements(By.ClassName("data-automation-message-pricechanged"));
            }
        }

        private IWebElement FirstBuyButton
        {
            get
            {
                this.wait.Until(d => d.FindElement(By.ClassName("data-automation-buyitem")));
                return this.driver.FindElement(By.ClassName("data-automation-buyitem"));
            }
        }

        private IWebElement AdminDashboardLink
        {
            get
            {
                return this.driver.FindElement(By.LinkText("Go to admin dashboard"));
            }
        }

        public HomePageObject BuyFirstItem()
        {
            this.FirstBuyButton.Click();
            return this;
        }

        public AdminPageObject GoToAdmin()
        {
            this.AdminDashboardLink.Click();
            return new AdminPageObject(this.driver);
        }
    }
}