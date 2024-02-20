using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class UserReportRepository : IUserReportRepository
    {
        ApplicationDbContext Context;
        public UserReportRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }




        public UserReport GetById(int id)
        {
            UserReport UserRepo = Context.UserReports.FirstOrDefault(s => s.Id == id);
            return UserRepo;
        }
        public List<UserReport> GetAll()
        {
            return Context.UserReports.ToList();
        }

        public int Create(UserReport UserRepo)
        {
            Context.UserReports.Add(UserRepo);
            int row = Context.SaveChanges();
            return row;
        }
        public int Update(int id, UserReport UserRepo)
        {
            UserReport OldUserRepo = Context.UserReports.FirstOrDefault(s => s.Id == id);
            OldUserRepo.Description = UserRepo.Description;
            OldUserRepo.DatePosted = UserRepo.DatePosted;

            int row = Context.SaveChanges();
            return row;
        }
        public int Delete(int id)
        {
            UserReport OldUserRepo = Context.UserReports.FirstOrDefault(s => s.Id == id);
            Context.UserReports.Remove(OldUserRepo);
            int row = Context.SaveChanges();
            return row;
        }
    }
}
