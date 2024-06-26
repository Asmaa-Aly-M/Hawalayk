﻿using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class UserReportRepository : IUserReportRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserRepository _applicationUserRepository;
        public UserReportRepository(ApplicationDbContext context, IApplicationUserRepository applicationUserRepository)
        {
            _context = context;
            _applicationUserRepository = applicationUserRepository;
        }
        public async Task<UserReport> GetById(int id)
        {
            UserReport UserRepo = await _context.UserReports.FirstOrDefaultAsync(s => s.Id == id);
            return UserRepo;
        }
        public async Task<List<UserReportForAdminDashBoard>> GetAllUserReports()
        {

            List<UserReportForAdminDashBoard> userReports = new List<UserReportForAdminDashBoard>();
            // List<UserReport> userReportList = await _context.UserReports.ToListAsync();
            List<UserReport> userReportList = await _context.UserReports
      .Include(ur => ur.Reporter)
      .Include(ur => ur.ReportedUser)
      .ToListAsync();

            foreach (var UserReport in userReportList)
            {
                UserReportForAdminDashBoard dto = new UserReportForAdminDashBoard
                {
                    Id = UserReport.Id,
                    ReporterId = UserReport.ReporerId,
                    ReporterName = UserReport.Reporter.FirstName + " "+ UserReport.Reporter.LastName,
                    ReportedId = UserReport.ReporedId,
                    ReportedName = UserReport.ReportedUser.FirstName +" "+ UserReport.ReportedUser.LastName,
                    Description = UserReport.Description
                };

                userReports.Add(dto);
            }

            return userReports;
        }

        public async Task<int> Create(string id, string reportedUserId, UserReportDTO UserRepo)
        {
            ApplicationUser ApplicationUser = await _applicationUserRepository.GetByIdAsync(id);
            if (ApplicationUser == null)
            {
               
                throw new Exception("Reported user does not exist.");
            }
            UserReport userReport = new UserReport()
            {
               // Id= UserRepo.Id,
                ReporedId = reportedUserId,
                Description = UserRepo.Description,
                ReporerId = ApplicationUser.Id
            };
            _context.UserReports.Add(userReport);
            int row = await _context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Update(int id, UserReport UserRepo)
        {
            UserReport OldUserRepo = await _context.UserReports.FirstOrDefaultAsync(s => s.Id == id);
            OldUserRepo.Description = UserRepo.Description;
            OldUserRepo.DatePosted = UserRepo.DatePosted;

            int row = await _context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Delete(int id)
        {
            UserReport OldUserRepo = await _context.UserReports.FirstOrDefaultAsync(s => s.Id == id);
            _context.UserReports.Remove(OldUserRepo);
            int row = await _context.SaveChangesAsync();
            return row;
        }

    }
}
