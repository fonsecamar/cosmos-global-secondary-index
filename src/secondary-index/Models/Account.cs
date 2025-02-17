namespace secondary_index.Models
{
    public class Account
    {
        public required string id { get; set; }
        public required string Name { get; set; }
        public required string Ssn { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}