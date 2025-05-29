using System.ComponentModel.DataAnnotations;

namespace EstatePro.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public ReportType? ReportType { get; set; }
        public string? ReportData { get; set; } // Store JSON as string
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }


}
