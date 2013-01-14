namespace Net.Daczkowski.AspNetTesting.Web.Models
{
    using System.Collections.Generic;

    using Net.Daczkowski.AspNetTesting.Read;

    public class AdminPageViewModel
    {
        private readonly IList<ProductSummary> products = new List<ProductSummary>();

        public AdminPageViewModel(IEnumerable<ProductQueries.ProductSummary> summaries)
        {
            foreach (var summary in summaries)
            {
                this.AddProduct(summary.Id, summary.Name, summary.Price);
            }
        }

        public IEnumerable<ProductSummary> Products
        {
            get
            {
                return this.products;
            }
        }

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
    }
}