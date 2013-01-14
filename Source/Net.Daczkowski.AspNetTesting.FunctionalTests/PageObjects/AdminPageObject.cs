namespace Net.Daczkowski.AspNetTesting.FunctionalTests.PageObjects
{
    using System.Globalization;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class AdminPageObject : PageObject
    {
        public AdminPageObject(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement HomePageLink
        {
            get
            {
                return this.Driver.FindElement(By.LinkText("Go to home page"));
            }
        }

        protected override string RelativeLocation
        {
            get
            {
                return "Admin";
            }
        }

        private IWebElement PriceEntry
        {
            get
            {
                this.Wait.Until(d => d.FindElement(By.Name("newPrice")));
                return this.Driver.FindElement(By.Name("newPrice"));        
            }
        }

        private IWebElement ChangePriceButton
        {
            get
            {
                return this.Driver.FindElement(By.ClassName("data-automation-changeprice"));
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
            return new HomePageObject(this.Driver);
        }
    }
}