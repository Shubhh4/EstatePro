using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace EstatePro.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, StringLength(50)]
        public string? FirstName { get; set; }

        [Required, StringLength(50)]
        public string? LastName { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Role { get; set; } // Admin, Agent, Buyer, Seller, Tenant

        public string? ProfilePicture { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<LeaseAgreement> LeaseAgreements { get; set; } = new List<LeaseAgreement>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

}
