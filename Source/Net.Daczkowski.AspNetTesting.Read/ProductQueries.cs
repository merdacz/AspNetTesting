namespace Net.Daczkowski.AspNetTesting.Read
{
    using System.Collections.Generic;

    public class ProductQueries
    {
        public static readonly IDictionary<int, ProductSummary> Summaries;

        static ProductQueries()
        {
            Summaries = new Dictionary<int, ProductSummary>()
                            {
                                { 1, new ProductSummary() { Id = 1, Name = "C# in Nutshell", Price = 1.0m } },
                                { 2, new ProductSummary() { Id = 2, Name = "CLR via C#", Price = 2.5m } },
                                { 3, new ProductSummary() { Id = 3, Name = "Clean Code", Price = 5.9m } },
                                { 4, new ProductSummary() { Id = 4, Name = "Programming Pearls", Price = 1.5m } }
                            };
        }

        public IEnumerable<ProductSummary> GetSummaryForAllProducts()
        {
            return Summaries.Values;
        }
        
        public class ProductSummary
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal Price { get; set; }
        }
    }
}
