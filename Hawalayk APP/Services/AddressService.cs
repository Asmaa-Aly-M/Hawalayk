using Hawalayk_APP.Context;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class AddressService:IAddressService
    {
        private readonly ApplicationDbContext Context;
        public AddressService(ApplicationDbContext _Context)
        {
            Context = Context;
        }

        public async Task<List<Address>> GetAllAsync()
        {
            return await Context.Addresses.Include(a => a.Governorate)
                                           .Include(a => a.City)
                                           .ToListAsync();
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await Context.Addresses.Include(a => a.Governorate)
                                           .Include(a => a.City)
                                           .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Address> CreateAsync(string governorateName, string cityName, string streetName)
        {
            var governorate = await Context.governorates.FirstOrDefaultAsync(g => g.governorate_name_ar == governorateName);
            var city = await Context.cities.FirstOrDefaultAsync(c => c.city_name_ar == cityName && c.governorate_id == governorate.id);

            if (governorate == null || city == null)
            {
                throw new ArgumentException("Invalid Governorate or City name");
            }

            var address = new Address
            {
                Governorate = governorate,
                City = city,
                StreetName = streetName
            };

            Context.Addresses.Add(address);
            await Context.SaveChangesAsync();
            return address;
        }
    }
}
