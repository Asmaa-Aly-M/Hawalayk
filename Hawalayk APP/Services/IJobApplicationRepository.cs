using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IJobApplicationRepository
    {
        int Create(JobApplication newJob);
        int Delete(int id);
        List<JobApplication> GetAll();
        JobApplication GetById(int id);
        int Update(int id, JobApplication newJob);
    }
}