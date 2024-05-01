using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using System.ComponentModel;
using System.Reflection;

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
        //public async Task<AppReport> GetOrCreateCraftAsync(string reprotedIssue)
        //{

        //    ReportedIssue ReportedIssue;
        //    AppReport existingAppReport = null;
        //    if (Enum.TryParse(reprotedIssue, out ReportedIssue))
        //    {
        //        existingAppReport = await _context.AppReports.FirstOrDefaultAsync(c => c.ReportedIssue == ReportedIssue);

        //    }//parsing : enum 

        //    var appReport = new AppReport {  };
        //    Context.Crafts.Add(newCraft);
        //    await Context.SaveChangesAsync();

        //    return newCraft;
        //}


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
        //public int Create(string id, AppReportDTO appReport)
        //{
        //    ApplicationUser ApplicationUser = applicationUserServiceRepo.GetById(id);


        //    ReportedIssue reportedIssue;
        //    AppReport existingAppReport = null;
        //    if (Enum.TryParse(appReport.ReportedIssue, out reportedIssue))
        //    {
        //        //existingAppReport =  _context.AppReports.FirstOrDefault(c => c.ReportedIssue == reportedIssue);

        //    }//parsing : enum 

        //    AppReport newAppReport = new AppReport()
        //    {
        //        //  Id = appreport.Id,
        //        ReportedIssue = reportedIssue,
        //        Description = appReport.Description,
        //        ReporerId = ApplicationUser.Id
        //    };
        //    _context.AppReports.Add(newAppReport);
        //    int row = _context.SaveChanges();
        //    return row;
        //}
        //
        public int Create(string userId, AppReportDTO appReport)
        {
            ApplicationUser ApplicationUser = applicationUserServiceRepo.GetById(userId);

            //ReportedIssue reportedIssue = Enum.Parse<ReportedIssue>(appReport.ReportedIssue, true);

            ReportedIssue enumValue = (ReportedIssue)ConvertToEnum<ReportedIssue>(appReport.ReportedIssue);

            AppReport newAppReport = new AppReport()
            {
                ReportedIssue = enumValue,
                Description = appReport.Description,
                ReporerId = ApplicationUser.Id
            };

            _context.AppReports.Add(newAppReport);
            int rowsAffected = _context.SaveChanges();
            return rowsAffected;
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
        private static T? ConvertToEnum<T>(string arabicString) where T : struct
        {
            Type enumType = typeof(T);

            if (enumType.IsEnum)
            {
                foreach (FieldInfo field in enumType.GetFields())
                {
                    if (Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                    {
                        if (attribute.Description == arabicString)
                        {
                            return (T)field.GetValue(null);
                        }
                    }
                }
            }
            return null;
        }
    }
}