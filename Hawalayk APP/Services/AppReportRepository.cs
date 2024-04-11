using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class AppReportRepository : IAppReportRepository
    {


        ApplicationDbContext _context;
        private readonly IApplicationUserService applicationUserServiceRepo;
        public AppReportRepository(ApplicationDbContext context, IApplicationUserService _applicationUserServiceRepo)
        {
            _context = context;
            applicationUserServiceRepo = _applicationUserServiceRepo;
        }

        public List<AppReport> GetAll()
        {
            List<AppReport> AppReports = _context.AppReports.ToList();

            return AppReports;


        }
        public AppReport GetById(int id)
        {
            AppReport appreport = _context.AppReports.SingleOrDefault(c => c.Id == id);
            return appreport;
        }
        public int Create(string id, AppReportDTO appreport)
        {
            ApplicationUser ApplicationUser = applicationUserServiceRepo.GetById(id);
            AppReport newAppReport = new AppReport()
            {
                //  Id = appreport.Id,
                ReportedIssue = appreport.ReportedIssue,
                Description = appreport.Description,
                ReporerId = ApplicationUser.Id
            };
            _context.AppReports.Add(newAppReport);
            int row = _context.SaveChanges();
            return row;
        }
        public int Update(int id, AppReport appreport)
        {
            AppReport Oldappreport = _context.AppReports.SingleOrDefault(c => c.Id == id);
            // Oldappreport.Id = appreport.Id;
            Oldappreport.Reporter = appreport.Reporter;
            // Oldappreport.ReporerId = appreport.ReporerId;
            Oldappreport.ReportedIssue = appreport.ReportedIssue;
            Oldappreport.DatePosted = appreport.DatePosted;
            Oldappreport.Description = appreport.Description;

            int row = _context.SaveChanges();
            return row;
        }
        public int Delete(AppReport appreport)
        {
            _context.AppReports.Remove(appreport);
            int row = _context.SaveChanges();
            return row;
        }
    }
}