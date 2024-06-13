using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace Hawalayk_APP.Services
{
    public class AppReportRepository : IAppReportRepository
    {


        ApplicationDbContext _context;
        private readonly IApplicationUserRepository _applicationUserRepository;
        public AppReportRepository(ApplicationDbContext context, IApplicationUserRepository applicationUserRepository)
        {
            _context = context;
            _applicationUserRepository = applicationUserRepository;
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


        public async Task<List<AppReport>> GetAll()
        {
           // List<AppReport> AppReports = await _context.AppReports.ToListAsync();
            List<AppReport> AppReports = await _context.AppReports
                                         .Include(ar => ar.Reporter)// Eager loading of Reporter
                                         .ToListAsync();

            return AppReports;


        }
        public async Task<AppReport> GetById(int id)
        {
            AppReport appreport = await _context.AppReports.SingleOrDefaultAsync(c => c.Id == id);
            return appreport;
        }
        //public int Create(string id, AppReportDTO appReport)
        //{
        //    ApplicationUser ApplicationUser = _applicationUserRepositoryRepo.GetById(id);


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
        public async Task<int> Create(string userId, AppReportDTO appReport)
        {
            ApplicationUser ApplicationUser = await _applicationUserRepository.GetByIdAsync(userId);

            //ReportedIssue reportedIssue = Enum.Parse<ReportedIssue>(appReport.ReportedIssue, true);

            ReportedIssue enumValue = (ReportedIssue)ConvertToEnum<ReportedIssue>(appReport.ReportedIssue);

            AppReport newAppReport = new AppReport()
            {
                ReportedIssue = enumValue,
                Description = appReport.Description,
                ReporerId = ApplicationUser.Id
            };

            _context.AppReports.Add(newAppReport);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }

        public async Task<int> Update(int id, AppReport appreport)
        {
            AppReport Oldappreport = await _context.AppReports.SingleOrDefaultAsync(c => c.Id == id);
            // Oldappreport.Id = appreport.Id;
            Oldappreport.Reporter = appreport.Reporter;
            // Oldappreport.ReporerId = appreport.ReporerId;
            Oldappreport.ReportedIssue = appreport.ReportedIssue;
            Oldappreport.DatePosted = appreport.DatePosted;
            Oldappreport.Description = appreport.Description;

            int row = await _context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Delete(AppReport appreport)
        {
            _context.AppReports.Remove(appreport);
            int row = await _context.SaveChangesAsync();
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