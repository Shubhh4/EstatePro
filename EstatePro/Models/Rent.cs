using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstatePro.Models
{
    public class Rent
    {
        public int RentId { get; set; }

        [ForeignKey("LeaseAgreement")]
        public int LeaseId { get; set; }
        public LeaseAgreement LeaseAgreement { get; set; }

        public string? MonthYear { get; set; } // e.g., "2025-07"
        public DateTime? DueDate { get; set; }
        public decimal? Amount { get; set; }
        public RentStatus Status { get; set; } = RentStatus.Pending;

        [ForeignKey("Transaction")]
        public int TransactionId { get; set; } // Nullable FK
        public Transaction Transaction { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }


}
