namespace CodeFirstPractice.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public virtual CustomerProfile Profile { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
