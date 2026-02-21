namespace CodeFirstPractice.Models;

public class CustomerProfile
{
    public int Id { get; set; }
    public string Address { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
