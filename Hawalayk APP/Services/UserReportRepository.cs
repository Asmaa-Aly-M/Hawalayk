using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class UserReportRepository : IUserReportRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserService applicationUserService;
        public UserReportRepository(ApplicationDbContext context, IApplicationUserService _applicationUserService)
        {
            _context = context;
            applicationUserService = _applicationUserService;
        }
        public async Task<UserReport> GetById(int id)
        {
            UserReport UserRepo = await _context.UserReports.FirstOrDefaultAsync(s => s.Id == id);
            return UserRepo;
        }
        public async Task<List<UserReport>> GetAll()
        {
            return await _context.UserReports.ToListAsync();
        }

        public async Task<int> Create(string id, UserReportDTO UserRepo)
        {
            ApplicationUser ApplicationUser = await applicationUserService.GetByIdAsync(id);
            UserReport userReport = new UserReport()
            {
                ReporedId = UserRepo.ReporedId,
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
