namespace Net.Daczkowski.AspNetTesting.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Net.Daczkowski.AspNetTesting.Logic;
    using Net.Daczkowski.AspNetTesting.Read;
    using NUnit.Framework;

    [TestFixture]
    public class CartTests
    {
        [Test]
        public void GivenEmptyCartWhenAddedSingleProductTwiceShouldIncreaseItsQuantity()
        {
            var cart = new Cart();
            cart.AddProduct(1, 1);
            cart.AddProduct(1, 1);
            cart.Items.Should().HaveCount(1);
            cart.Items.First().Quantity.Should().Be(2);
        }

        [Test]
        public void GivenCartWithReceiptWhenProductPriceChangesShouldNotifyOnReceiptCreation()
        {
            var cart = new Cart();
            cart.AddProduct(1, 2);
            cart.CreateReceipt(id => new ProductQueries.ProductSummary() { Id = id, Price = 1.23m }, message => Assert.Fail());
            var messages = new List<string>();
            cart.CreateReceipt(id => new ProductQueries.ProductSummary() { Id = id, Price = 1.99m }, messages.Add);
            messages.Should().HaveCount(1);
        }

        [Test]
        public void GivenCartWithManyProductWhenAllTheirPricesChangeShouldNotifyOnReceiptCreation()
        {
            var cart = new Cart();
            cart.AddProduct(1, 2);
            cart.AddProduct(2, 3);
            cart.AddProduct(3, 1);
            cart.CreateReceipt(id => new ProductQueries.ProductSummary() { Id = id, Price = 1.23m }, message => Assert.Fail());
            var messages = new List<string>();
            cart.CreateReceipt(id => new ProductQueries.ProductSummary() { Id = id, Price = 1.99m }, messages.Add);
            messages.Should().HaveCount(3);
        }

        [Test]
        public void GivenCartWithSingleProductAddedWhenIDeleteItShouldGetEmptyCart()
        {
            var cart = new Cart();
            cart.AddProduct(1, 2);
            cart.RemoveProduct(1);
            cart.CreateReceipt(this.ShouldFail, message => Assert.Fail());
            cart.LastReceipt.TotalPrice.Should().Be(0);
        }

        private ProductQueries.ProductSummary ShouldFail(int arg)
        {
            Assert.Fail();
            return null;
        }
    }
}