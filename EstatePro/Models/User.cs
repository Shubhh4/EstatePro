using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace EstatePro.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; } // Enum: Admin, Agent, Buyer, Seller, Tenant

        public string ProfilePicture { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Property> Properties { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<LeaseAgreement> LeaseAgreements { get; set; }
    }

}
