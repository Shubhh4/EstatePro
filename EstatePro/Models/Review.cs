using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstatePro.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int Rating { get; set; } // Use range validation
        public string? ReviewText { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }


}
