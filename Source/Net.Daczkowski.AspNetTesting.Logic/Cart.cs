namespace Net.Daczkowski.AspNetTesting.Logic
{
    using System;
    using System.Collections.Generic;

    using Net.Daczkowski.AspNetTesting.Read;

    public class Cart
    {
        private readonly IDictionary<int, CartItem> items = new Dictionary<int, CartItem>();

        private Receipt lastReceipt;

        public IEnumerable<CartItem> Items
        {
            get
            {
                return this.items.Values;
            }
        }

        public Receipt LastReceipt
        {
            get
            {
                return this.lastReceipt;
            }
        }

        public void AddProduct(int id, int quantity)
        {
            if (this.items.ContainsKey(id))
            {
                this.items[id].Quantity += quantity;
                return;
            }
            
            var item = new CartItem();
            item.Id = id;
            item.Quantity = quantity;
            this.items.Add(id, item);
        }

        public void CreateReceipt(Func<int, ProductQueries.ProductSummary> getProductSummary, Action<string> notify = null)
        {
            var receipt = new Receipt();
            foreach (var item in this.items.Values)
            {
                var summary = getProductSummary(item.Id);
                receipt.AddOrderItem(summary.Id, summary.Name, summary.Price, item.Quantity);
            }

            if (notify != null)
            {
                receipt.Diff(this.lastReceipt, notify);
            }

            this.lastReceipt = receipt;
        }

        public void RemoveProduct(int id)
        {
            if (this.items.ContainsKey(id))
            {
                this.items.Remove(id);
            }
        }
    }
}
