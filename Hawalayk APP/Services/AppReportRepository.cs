using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class AppReportRepository
    {
        

        ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<AppReport> GetAll()
        {
            List<AppReport> AppReports = _context.Customers.ToList();

            return AppReports;


        }
        public AppReport GetById(string id)
        {
            AppReport appreport = _context.AppReports.SingleOrDefault(c => c.Id == id);
            return appreport;
        }
        public int Create(AppReport appreport)
        {
            _context.AppReports.Add(appreport);
            int row = _context.SaveChanges();
            return row;
        }
        public int Update(string id, AppReport appreport)
        {
            AppReport Oldappreport = _context.AppReports.SingleOrDefault(c => c.Id == id);
            Oldappreport.Id = id;
            Oldappreport.Reporter = appreport.Reporter;
            Oldappreport.ReporerId = appreport.ReporerId;
            Oldappreport.ReportedIssue = appreport.ReportedIssue;
            Oldappreport.DatePosted = appreport.DatePosted;
            Oldappreport.Description = appreport.Description;
            
             int row = _context.SaveChanges();
            return row;
        }
        public int Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            int row = _context.SaveChanges();
            return row;
        }
    }
}
