namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using NUnit.Framework;

    using OpenQA.Selenium;

    [TestFixture]
    public abstract class SeleniumTestBase
    {
        private readonly string driverName;

        protected SeleniumTestBase(string driverName)
        {
            this.driverName = driverName;
        }

        public IWebDriver Driver { get; set; }

        [SetUp]
        public void LoadDriver()
        {
            if (this.Driver == null)
            {
                this.Driver = SeleniumFactory.GetInstance().CreateDriver(this.driverName);
            }
        }

        [TearDown]
        public void CloseDriver()
        {
            if (this.Driver == null)
            {
                return;
            }

            this.Driver.Quit();
        }
    }
}