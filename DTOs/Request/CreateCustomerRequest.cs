namespace CodeFirstPractice.DTOs.Request
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; } = string.Empty;

        public CreateCustomerProfileRequest Profile { get; set; } = null!;
    }
}
