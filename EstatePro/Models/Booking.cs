using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace EstatePro.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime BookingDate { get; set; }
    }



}
