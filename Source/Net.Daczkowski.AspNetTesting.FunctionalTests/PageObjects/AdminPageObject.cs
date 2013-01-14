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

        [FindsBy(How = How.LinkText, Using = "Go to home page")]
        public IWebElement HomePageLink { get; set; }

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

        [FindsBy(How = How.ClassName, Using = "data-automation-changeprice")]
        private IWebElement ChangePriceButton { get; set; }

        public AdminPageObject ChangePriceForFirstItem()
        {
            var oldValue = this.PriceEntry.GetAttribute("value");
            var newValue = decimal.Parse(oldValue, CultureInfo.InvariantCulture) + 1;
            this.PriceEntry.Clear();
            this.PriceEntry.SendKeys(newValue.ToString(CultureInfo.InvariantCulture));
            
            // something really bad is going on here
            PageFactory.InitElements(this.Driver, this);
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