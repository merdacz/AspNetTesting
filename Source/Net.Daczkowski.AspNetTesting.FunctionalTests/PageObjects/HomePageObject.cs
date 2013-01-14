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

        // no support for lists in current PageFactory
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

        [FindsBy(How = How.LinkText, Using = "Go to admin dashboard")]
        private IWebElement AdminDashboardLink { get; set; }

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