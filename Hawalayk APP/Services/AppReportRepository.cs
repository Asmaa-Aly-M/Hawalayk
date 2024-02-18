using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class AppReportRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create operation
        public void Add(AppReport report)
        {
            _dbContext.AppReports.Add(report);
            _dbContext.SaveChanges();
        }

        // Read operation
        public AppReport GetById(int id)
        {
            return _dbContext.AppReports.FirstOrDefault(r => r.Id == id);
        }

        // Update operation
        public void Update(AppReport updatedReport)
        {
            var existingReport = _dbContext.AppReports.FirstOrDefault(r => r.Id == updatedReport.Id);
            if (existingReport != null)
            {
                existingReport.Reporter = updatedReport.Reporter;
                existingReport.ReportedIssue = updatedReport.ReportedIssue;
                existingReport.Description = updatedReport.Description;
                existingReport.DatePosted = updatedReport.DatePosted;

                _dbContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Report not found");
            }
        }

        // Delete operation
        public void Delete(int id)
        {
            var reportToRemove = _dbContext.AppReports.FirstOrDefault(r => r.Id == id);
            if (reportToRemove != null)
            {
                _dbContext.AppReports.Remove(reportToRemove);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Report not found");
            }
        }
    }
}
