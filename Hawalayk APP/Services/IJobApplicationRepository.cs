using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IJobApplicationRepository
    {
        Task<int> Create(string craftmanId, JobApplicationDTO newJob);
        int Delete(int id);
        List<JobApplication> GetAll();
        JobApplication GetById(int id);
    }
}