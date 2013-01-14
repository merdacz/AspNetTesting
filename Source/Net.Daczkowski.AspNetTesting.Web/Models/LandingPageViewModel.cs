namespace Net.Daczkowski.AspNetTesting.Web.Models
{
    using System.Collections.Generic;

    using Net.Daczkowski.AspNetTesting.Logic;
    using Net.Daczkowski.AspNetTesting.Read;

    public class LandingPageViewModel
    {
        private readonly IList<ProductSummary> products = new List<ProductSummary>();

        private readonly IList<CartItemLine> cartItemsItems = new List<CartItemLine>();

        public LandingPageViewModel(IEnumerable<ProductQueries.ProductSummary> summaries, Receipt receipt = null)
        {
            foreach (var summary in summaries)
            {
                this.AddProduct(summary.Id, summary.Name, summary.Price);
            }

            if (receipt == null)
            {
                return;
            }

            foreach (var item in receipt.Items)
            {
                var cartLine = new CartItemLine();
                cartLine.Id = item.Id;
                cartLine.Name = item.Name;
                cartLine.UnitPrice = item.UnitPrice;
                cartLine.Quantity = item.Quantity;
                cartLine.Price = item.Price;
                this.cartItemsItems.Add(cartLine);
            }

            this.TotalPrice = receipt.TotalPrice;
        }

        public IEnumerable<ProductSummary> Products
        {
            get
            {
                return this.products;
            }
        }

        public IEnumerable<CartItemLine> CartItems
        {
            get
            {
                return this.cartItemsItems;
            }
        }

        public decimal TotalPrice { get; private set; }

        private void AddProduct(int id, string name, decimal price)
        {
            var product = new ProductSummary();
            product.Id = id;
            product.Name = name;
            product.Price = price;
            this.products.Add(product);
        }

        public class ProductSummary
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal Price { get; set; }
        }

        public class CartItemLine
        {
            public int Id { get; set;  }

            public string Name { get; set; }

            public decimal UnitPrice { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }
        }
    }
}