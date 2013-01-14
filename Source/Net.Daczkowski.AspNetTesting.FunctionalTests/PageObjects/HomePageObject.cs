namespace Net.Daczkowski.AspNetTesting.FunctionalTests.PageObjects
{
    using System.Collections.Generic;

    using OpenQA.Selenium;

    public class HomePageObject : PageObject
    {
        public HomePageObject(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement FirstCartItem
        {
            get
            {
                return this.Driver.FindElement(By.ClassName("data-automation-cartitem-name"));
            }
        }

        public IList<IWebElement> PriceChangeNotifications
        {
            get
            {
                return this.Driver.FindElements(By.ClassName("data-automation-message-pricechanged"));
            }
        }

        protected override string RelativeLocation
        {
            get
            {
                return string.Empty;
            }
        }

        private IWebElement FirstBuyButton
        {
            get
            {
                this.Wait.Until(d => d.FindElement(By.ClassName("data-automation-buyitem")));
                return this.Driver.FindElement(By.ClassName("data-automation-buyitem"));
            }
        }

        private IWebElement AdminDashboardLink
        {
            get
            {
                return this.Driver.FindElement(By.LinkText("Go to admin dashboard"));
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
            return new AdminPageObject(this.Driver);
        }
    }
}