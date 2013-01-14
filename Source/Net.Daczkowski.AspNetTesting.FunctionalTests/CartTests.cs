namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using FluentAssertions;
    using Net.Daczkowski.AspNetTesting.FunctionalTests.PageObjects;
    using NUnit.Framework;
    
    public class CartTests : RunOnAllBrowsers
    {
        public CartTests(string driverName)
            : base(driverName)
        {
        }

        [Test]
        public void GivenCartWithProduct_WhenItsPriceChanges_ShouldRecalculateAndNotify()
        {
            var admin = new HomePageObject(this.Driver)
                .BuyFirstItem()
                .GoToAdmin();

            var search = admin
                .ChangePriceForFirstItem()
                .GoToHomePage();

            search.PriceChangeNotifications.Should().HaveCount(1);
        }
    }
}