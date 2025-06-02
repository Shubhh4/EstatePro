using EstatePro.Models;

namespace EstatePro.Repository
{
    public interface ReportRepo
    {
        List<Report> FetchReports();
        void AddReport(Report report);
        Report GetReportById(int id);
        void EditReport(Report report);
        void DeleteReport(int id);
    }
}
