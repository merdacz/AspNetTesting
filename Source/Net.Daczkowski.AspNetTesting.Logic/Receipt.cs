namespace Net.Daczkowski.AspNetTesting.Logic
{
    using System;
    using System.Collections.Generic;

    public class Receipt
    {
        private readonly IDictionary<int, ReceiptItem> items = new Dictionary<int, ReceiptItem>();

        public decimal TotalPrice { get; private set; }

        public IEnumerable<ReceiptItem> Items
        {
            get
            {
                return this.items.Values;
            }
        }

        public void AddOrderItem(int id, string name, decimal unitPrice, int quantity)
        {
            var item = new ReceiptItem();
            item.Id = id;
            item.Name = name;
            item.UnitPrice = unitPrice;
            item.Quantity = quantity;
            this.items.Add(id, item);

            this.TotalPrice += item.Price;
        }

        public void Diff(Receipt receipt, Action<string> notify)
        {
            if (receipt == null)
            {
                return;
            }

            foreach (var item in receipt.items.Values)
            {
                if (!this.items.ContainsKey(item.Id))
                {
                    continue;
                }

                var unitPrice = this.items[item.Id].UnitPrice;
                if (unitPrice != item.UnitPrice)
                {
                    notify(string.Format("Unit price has changed from {0} to {1}", item.UnitPrice, unitPrice));
                }
            }
        }
    }
}