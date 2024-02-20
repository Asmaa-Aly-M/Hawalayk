using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IUserReportRepository
    {
        int Create(UserReport UserRepo);
        int Delete(int id);
        List<UserReport> GetAll();
        UserReport GetById(int id);
        int Update(int id, UserReport UserRepo);
    }
}