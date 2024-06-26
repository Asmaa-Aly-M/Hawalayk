﻿using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        ApplicationDbContext Context;
        private readonly ICraftsmenRepository _craftsmanRepository;
        public JobApplicationRepository(ApplicationDbContext _Context, ICraftsmenRepository craftsmanRepository)
        {
            Context = _Context;
            _craftsmanRepository = craftsmanRepository;
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
                CraftsmanImg = jobApplication.Craftsman.ProfilePicture,
                CraftsmanLastName = jobApplication.Craftsman.LastName,
                CraftsmanUserName = jobApplication.Craftsman.UserName,
                Rating=jobApplication.Craftsman.Rating,
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

        public async Task<int> Create(string craftmanId, int serviceID, JobApplicationDTO newJob)
        {
            Craftsman craftsman = await _craftsmanRepository.GetById(craftmanId);
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
                ResponseStatus = ResponseStatus.Pending,
                ServiceRequestId = serviceID,
                

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

        public async Task<AcceptedJobApplicationDTO> GetJobApplicationAcceptedByServiceRequest(int ServiceRequestID)
        {
            var job = Context.JobApplications
                .Include(j => j.Craftsman)
                .Include(s => s.ServiceRequest)
                .FirstOrDefault(job => job.ServiceRequestId == ServiceRequestID && job.ResponseStatus == ResponseStatus.Accepted);
            if (job == null)
            {
                return null;
            }


            AcceptedJobApplicationDTO acceptedJob = new AcceptedJobApplicationDTO
            {
                CustomerID=job.ServiceRequest.CustomerId,
                ServiceRequestID=job.ServiceRequestId,
                JobApplicationID=job.Id,
                CraftsmanID = job.CraftsmanId,
                CraftsmanFristName = job.Craftsman.FirstName,
                CraftsmanLastName = job.Craftsman.LastName,
                CraftsmanProfilePicture = job.Craftsman.ProfilePicture,
                InitialPrice = job.InitialPrice,
                Content = job.Content,
                Rating=job.Craftsman.Rating,    
            };
            return acceptedJob;


        }

        public async Task<List<AcceptedJobApplicationDTO>> GetJobApplicationsPendingByServiceRequest(int ServiceRequestID)
        {
            var jobs = Context.JobApplications.Include(j => j.Craftsman)
                .Include(s=>s.ServiceRequest)
                .Where(job => job.ServiceRequestId == ServiceRequestID && job.ResponseStatus == ResponseStatus.Pending);

            List<AcceptedJobApplicationDTO> pendingJobs = jobs.Select(j =>
                new AcceptedJobApplicationDTO
                {
                    CraftsmanID = j.CraftsmanId,
                    JobApplicationID=j.Id,
                    ServiceRequestID=j.ServiceRequestId,
                    CustomerID=j.ServiceRequest.CustomerId,
                    CraftsmanFristName = j.Craftsman.FirstName,
                    CraftsmanLastName = j.Craftsman.LastName,
                    CraftsmanProfilePicture = j.Craftsman.ProfilePicture,
                    InitialPrice = j.InitialPrice,
                    Content = j.Content,
                    Rating=j.Craftsman.Rating
                }).ToList();
            return pendingJobs;


        }
    }
}
