namespace Net.Daczkowski.AspNetTesting.FunctionalTests.PageObjects
{
    using System.Collections.Generic;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class HomePageObject : PageObject
    {
        public HomePageObject(IWebDriver driver)
            : base(driver)
        {
        }

        [FindsBy(How = How.ClassName, Using = "data-automation-message-cartitem-name")]
        public IWebElement FirstCartItem
        {
            get; set; 
        }

        public IList<IWebElement> PriceChangeNotifications
        {
            get
            {
                return this.Driver.FindElements(By.ClassName("data-automation-message-pricechanged"));
            }
        }

        public decimal TotalPrice
        {
            get
            {
                var value = this.Driver.FindElement(By.ClassName("data-automation-totalprice")).Text;
                return decimal.Parse(value);
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
                this.Wait.Until(d => d.FindElement(By.LinkText("Go to admin dashboard")));
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