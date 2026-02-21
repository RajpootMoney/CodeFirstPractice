namespace CodeFirstPractice.Models
{
    public class Order
    {
        private readonly List<OrderItem> _items = new();
        public int Id { get; set; }
        public Money TotalAmount =>
            _items
                .Select(i => i.SubTotal)
                .Aggregate(
                    Money.Zero("USD"), // <-- currency pass karo
                    (acc, next) => acc.Add(next)
                );

        public int CustomerId { get; set; } // Foreign Key
        public Customer Customer { get; set; } // Navigation
        public IReadOnlyCollection<OrderItem> OrderItems => _items.AsReadOnly();
    }
}
