using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstatePro.Models
{
    public class Property
    {
        public int PropertyId { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        [Required]
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

        // Enums or separate tables - here assumed enums
        public PropertyType PropertyType { get; set; }
        public ListingType ListingType { get; set; }
        public PropertyStatus Status { get; set; }

        // Owner FK
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public OwnerRole OwnerRole { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<LeaseAgreement> LeaseAgreements { get; set; } = new List<LeaseAgreement>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    }



}
