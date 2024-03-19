using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class UserReportRepository : IUserReportRepository
    {
        private readonly ApplicationDbContext _context;
        public UserReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public UserReport GetById(int id)
        {
            UserReport UserRepo = _context.UserReports.FirstOrDefault(s => s.Id == id);
            return UserRepo;
        }
        public List<UserReport> GetAll()
        {
            return _context.UserReports.ToList();
        }

        public int Create(UserReport UserRepo)
        {
            _context.UserReports.Add(UserRepo);
            int row = _context.SaveChanges();
            return row;
        }
        public int Update(int id, UserReport UserRepo)
        {
            UserReport OldUserRepo = _context.UserReports.FirstOrDefault(s => s.Id == id);
            OldUserRepo.Description = UserRepo.Description;
            OldUserRepo.DatePosted = UserRepo.DatePosted;

            int row = _context.SaveChanges();
            return row;
        }
        public int Delete(int id)
        {
            UserReport OldUserRepo = _context.UserReports.FirstOrDefault(s => s.Id == id);
            _context.UserReports.Remove(OldUserRepo);
            int row = _context.SaveChanges();
            return row;
        }
    }
}
