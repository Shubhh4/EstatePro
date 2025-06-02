using EstatePro.Data;
using EstatePro.Models;
using EstatePro.Repository;

namespace EstatePro.Services
{
    public class ReportService : ReportRepo
    {
        ApplicationDbContext db;

        public ReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Report> FetchReports()
        {
            return db.Reports.ToList();
        }

        public void AddReport(Report report)
        {
            report.CreatedAt = DateTime.UtcNow;
            db.Reports.Add(report);
            db.SaveChanges();
        }

        public Report GetReportById(int id)
        {
            return db.Reports.FirstOrDefault(r => r.ReportId == id);
        }

        public void EditReport(Report report)
        {
            db.Reports.Update(report);
            db.SaveChanges();
        }

        public void DeleteReport(int id)
        {
            var report = db.Reports.Find(id);
            if (report != null)
            {
                db.Reports.Remove(report);
                db.SaveChanges();
            }
        }
    }
}
