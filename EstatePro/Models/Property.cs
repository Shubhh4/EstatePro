using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstatePro.Models
{
    public class Property
    {
        public int PropertyId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

        public PropertyType PropertyType { get; set; }
        public ListingType ListingType { get; set; }
        public PropertyStatus Status { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public OwnerRole OwnerRole { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }



}
