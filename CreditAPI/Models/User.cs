using System;

namespace CreditAPI.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Username { get; set; }   
        public string? Password { get; set; } 
	public string? PasswordHash { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
