using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        ApplicationDbContext Context;
        private readonly ICraftsmenRepository craftsmanRepo;
        public JobApplicationRepository(ApplicationDbContext _Context, ICraftsmenRepository _craftsmanRepo)
        {
            Context = _Context;
            craftsmanRepo = _craftsmanRepo;
        }

        public async Task<JobApplicationSendDTO> GetJpbApplicationSend(int jobApplicationId)
        {
            var jobApplication = await GetById(jobApplicationId);
            return new JobApplicationSendDTO
            {
                JobApplicationId = jobApplicationId,
                Content = jobApplication.Content,
                InitialPrice = jobApplication.InitialPrice,
                CraftsmanFirstName = jobApplication.Craftsman.FirstName,
                CraftsmanId = jobApplication.Craftsman.Id,
                CraftsmanImg = Path.Combine("imgs/", jobApplication.Craftsman.ProfilePicture),
                CraftsmanLastName = jobApplication.Craftsman.LastName,
                CraftsmanUserName = jobApplication.Craftsman.UserName,
            };
        }
        public async Task<JobApplication> GetById(int id)
        {
            JobApplication job = await Context.JobApplications.Include(c => c.Craftsman).FirstOrDefaultAsync(s => s.Id == id);
            return job;
        }
        public async Task<List<JobApplication>> GetAll()
        {
            return await Context.JobApplications.ToListAsync();
        }

        public async Task<int> Create(string craftmanId, JobApplicationDTO newJob)
        {
            Craftsman craftsman = await craftsmanRepo.GetById(craftmanId);
            if (craftmanId == null)
            {
                return -1;
            }
            JobApplication job = new JobApplication()
            {
                //Id = newJob.Id,
                Content = newJob.Content,
                InitialPrice = newJob.InitialPrice,
                CraftsmanId = craftsman.Id,
                ResponseStatus = ResponseStatus.Pending

            };

            Context.JobApplications.Add(job);
            int row = await Context.SaveChangesAsync();
            if (row > 0)
            {
                return job.Id;
            }

            return -1;
        }
        /* public int Update(int id, JobApplicationDTO newJob)////////هل محتاجينها+فيها غلطات؟؟؟
         {
             JobApplicationDTO OldJob = Context.JobApplications.FirstOrDefault(s => s.Id == id);
             OldJob.Content = newJob.Content;
             OldJob.InitialPrice = newJob.InitialPrice;
             OldJob.ResponseStatus = newJob.ResponseStatus;

             int row = Context.SaveChanges();
             return row;
         }*/
        public async Task<int> Delete(int id)
        {
            JobApplication OldJob = await Context.JobApplications.FirstOrDefaultAsync(s => s.Id == id);
            Context.JobApplications.Remove(OldJob);
            int row = await Context.SaveChangesAsync();
            return row;
        }
    }
}
