namespace CodeFirstPractice.Models
{
    public class Money : IEquatable<Money>
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        private Money() { } // EF Core

        public static Money Zero(string currency) => new Money(0, currency);

        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative.");

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.");

            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money other)
        {
            ValidateSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Multiply(int quantity)
        {
            return new Money(Amount * quantity, Currency);
        }

        private void ValidateSameCurrency(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot operate on different currencies.");
        }

        public bool Equals(Money other)
        {
            if (other is null)
                return false;
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object obj) => Equals(obj as Money);

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);
    }
}
