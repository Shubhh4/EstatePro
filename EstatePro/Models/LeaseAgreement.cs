using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstatePro.Models
{
    public class LeaseAgreement
    {
        [Key]
        public int LeaseId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public User Tenant { get; set; }

        public DateTime? LeaseStartDate { get; set; }
        public DateTime? LeaseEndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RentAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SecurityDeposit { get; set; }
        public LeaseStatus LeaseStatus { get; set; }
    }


}
