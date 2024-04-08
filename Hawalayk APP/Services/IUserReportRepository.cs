using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IUserReportRepository
    {
        int Create(string id, UserReportDTO UserRepo);
        int Delete(int id);
        List<UserReport> GetAll();
        UserReport GetById(int id);
        int Update(int id, UserReport UserRepo);
    }
}