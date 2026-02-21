namespace CodeFirstPractice.Models
{
    public class OrderItem
    {
        private OrderItem() { } // EF Core ke liye

        public int Id { get; private set; }

        public int OrderId { get; private set; }
        public Order Order { get; private set; }

        public int ProductId { get; private set; }
        public Product Product { get; private set; }

        public int Quantity { get; private set; }

        public Money SubTotal => Product.Price.Multiply(Quantity);

        public OrderItem(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public void ChangeQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Quantity = quantity;
        }
    }
}
