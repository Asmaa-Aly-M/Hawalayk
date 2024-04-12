using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
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




        public JobApplication GetById(int id)
        {
            JobApplication job = Context.JobApplications.FirstOrDefault(s => s.Id == id);
            return job;
        }
        public List<JobApplication> GetAll()
        {
            return Context.JobApplications.ToList();
        }

        public int Create(string craftmanId, JobApplicationDTO newJob)
        {
            Craftsman craftsman = craftsmanRepo.GetById(craftmanId);
            JobApplication job = new JobApplication()
            {
                Id = newJob.Id,
                Content = newJob.Content,
                InitialPrice = newJob.InitialPrice,
                CraftsmanId = craftsman.Id
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
        public int Delete(int id)
        {
            JobApplication OldJob = Context.JobApplications.FirstOrDefault(s => s.Id == id);
            Context.JobApplications.Remove(OldJob);
            int row = Context.SaveChanges();
            return row;
        }
    }
}
