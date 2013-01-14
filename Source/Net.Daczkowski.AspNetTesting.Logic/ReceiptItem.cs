namespace Net.Daczkowski.AspNetTesting.Logic
{
    public class ReceiptItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Price
        {
            get
            {
                return this.UnitPrice * this.Quantity;
            }
        }
    }
}