using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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




        public async Task<JobApplication> GetById(int id)
        {
            JobApplication job = await Context.JobApplications.FirstOrDefaultAsync(s => s.Id == id);
            return job;
        }
        public async Task<List<JobApplication>> GetAll()
        {
            return await Context.JobApplications.ToListAsync();
        }

        public async Task<int> Create(string craftmanId, JobApplicationDTO newJob)
        {
            Craftsman craftsman = await craftsmanRepo.GetById(craftmanId);
            JobApplication job = new JobApplication()
            {
                Id = newJob.Id,
                Content = newJob.Content,
                InitialPrice = newJob.InitialPrice,
                CraftsmanId = craftsman.Id,
                ResponseStatus = ResponseStatus.Pending

            };

            Context.JobApplications.Add(job);
            int row = Context.SaveChanges();
            return row;
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
