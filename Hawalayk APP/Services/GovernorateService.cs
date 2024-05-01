using Hawalayk_APP.Context;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class GovernorateService:IGovernorateService
    {
        private readonly ApplicationDbContext Context;
        public GovernorateService(ApplicationDbContext _Context)
        {
            Context = _Context;
        }
        public async Task<List<Governorate>> GetAllAsync()
        {
            return await Context.governorates.Include(g => g.Cities).ToListAsync();
        }

        public async Task<Governorate> GetByIdAsync(int id)
        {
            return await Context.governorates.Include(g => g.Cities).FirstOrDefaultAsync(g => g.id == id);
        }


        public async Task<Governorate> GetByNameAsync(string govName)
        {
            return await Context.governorates.Include(g => g.Cities).FirstOrDefaultAsync(gov => gov.governorate_name_ar == govName);
        }

        public async Task<List<string>> GetGovernorateNamesAsync()
        {
            return await Context.governorates.Select(g => g.governorate_name_ar).ToListAsync();
        }
    }
}
