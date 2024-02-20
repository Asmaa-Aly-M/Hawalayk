using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        ApplicationDbContext Context;
        public JobApplicationRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
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

        public int Create(JobApplication newJob)
        {
            Context.JobApplications.Add(newJob);
            int row = Context.SaveChanges();
            return row;
        }
        public int Update(int id, JobApplication newJob)////////هل محتاجينها؟؟؟
        {
            JobApplication OldJob = Context.JobApplications.FirstOrDefault(s => s.Id == id);
            OldJob.Content = newJob.Content;
            OldJob.InitialPrice = newJob.InitialPrice;
            OldJob.ResponseStatus = newJob.ResponseStatus;

            int row = Context.SaveChanges();
            return row;
        }
        public int Delete(int id)
        {
            JobApplication OldJob = Context.JobApplications.FirstOrDefault(s => s.Id == id);
            Context.JobApplications.Remove(OldJob);
            int row = Context.SaveChanges();
            return row;
        }
    }
}
