using System.ComponentModel.DataAnnotations.Schema;

namespace EstatePro.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }


}
