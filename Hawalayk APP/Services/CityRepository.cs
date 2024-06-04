using Hawalayk_APP.Context;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext Context;
        public CityRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }
        public async Task<List<City>> GetAllAsync()
        {
            return await Context.cities.Include(c => c.Governorate).ToListAsync();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await Context.cities.Include(c => c.Governorate).FirstOrDefaultAsync(c => c.id == id);
        }


        public async Task<City> GetByNameAsync(string cityName)
        {
            return await Context.cities.Include(c => c.Governorate).FirstOrDefaultAsync(c => c.city_name_ar == cityName);
        }

        public async Task<List<string>> GetCitiesNamesByGovernorateNameAsync(string governorateName)
        {
            var governorate = await Context.governorates.FirstOrDefaultAsync(g => g.governorate_name_ar == governorateName);
            if (governorate == null)
            {
                throw new ArgumentException("Invalid Governorate name");
            }
            return await Context.cities.Where(c => c.governorate_id == governorate.id)
                                      .Select(c => c.city_name_ar)
                                      .ToListAsync();
        }
    }
}
