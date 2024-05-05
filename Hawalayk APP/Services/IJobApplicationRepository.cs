using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IJobApplicationRepository
    {
        Task<int> Create(string craftmanId, JobApplicationDTO newJob);
        Task<int> Delete(int id);
        Task<List<JobApplication>> GetAll();
        Task<JobApplication> GetById(int id);
    }
}