using Hawalayk_APP.Context;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        ApplicationDbContext Context;
        public AdvertisementRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }




        public async Task<Advertisement> GetById(int id)
        {
            Advertisement adverts = await Context.Advertisements.FirstOrDefaultAsync(s => s.Id == id);
            return adverts;
        }
        public async Task<List<Advertisement>> GetAll()
        {
            return await Context.Advertisements.ToListAsync();
        }

        public async Task<int> Create(Advertisement Advertis)
        {
            Context.Advertisements.Add(Advertis);
            int row = await Context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Update(int id, Advertisement Advertis)
        {
            Advertisement OldAdvertis = Context.Advertisements.FirstOrDefault(s => s.Id == id);
            OldAdvertis.Title = Advertis.Title;
            OldAdvertis.Image = Advertis.Image;
            OldAdvertis.ClickUrl = Advertis.ClickUrl;
            OldAdvertis.Advertiser = Advertis.Advertiser;
            OldAdvertis.Description = Advertis.Description;
            OldAdvertis.NumOfClicks = Advertis.NumOfClicks;
            OldAdvertis.StartDate = Advertis.StartDate;
            OldAdvertis.EndDate = Advertis.EndDate;
            OldAdvertis.DateCreated = Advertis.DateCreated;

            int row = await Context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Delete(int id)
        {
            Advertisement OldAdvertis = await Context.Advertisements.FirstOrDefaultAsync(s => s.Id == id);
            Context.Advertisements.Remove(OldAdvertis);
            int row = await Context.SaveChangesAsync();
            return row;
        }
    }
}
