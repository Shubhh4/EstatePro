using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstatePro.Models
{
    public class LeaseAgreement
    {
        [Key]
        public int LeaseId { get; set; }

        [Required]
        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        [Required]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public User Tenant { get; set; }

        [Required]
        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        [Required(ErrorMessage = "Lease start date is required")]
        public DateTime LeaseStartDate { get; set; }

        [Required(ErrorMessage = "Lease end date is required")]
        public DateTime LeaseEndDate { get; set; }

        [Required(ErrorMessage = "Rent amount is required")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Rent must be a positive amount")]
        public decimal? RentAmount { get; set; }

        [Required(ErrorMessage = "Security deposit is required")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Security deposit must be a positive amount")]
        public decimal? SecurityDeposit { get; set; }
        public bool IsDepositPaid { get; set; } = false;
        [Required]
        public LeaseStatus LeaseStatus { get; set; } // Enum: Pending, Accepted, Rejected

    }


}
