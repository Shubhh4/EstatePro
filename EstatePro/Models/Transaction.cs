using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace EstatePro.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? TransactionDate { get; set; }

        public TransactionType? TransactionType { get; set; }

        public PaymentStatus? PaymentStatus { get; set; }
    }
}
