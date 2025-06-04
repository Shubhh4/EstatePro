using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace EstatePro.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; } = "Pending";

        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
