using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        ApplicationDbContext Context;
        public AdvertisementRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }




        public Advertisement GetById(int id)
        {
            Advertisement adverts = Context.Advertisements.FirstOrDefault(s => s.Id == id);
            return adverts;
        }
        public List<Advertisement> GetAll()
        {
            return Context.Advertisements.ToList();
        }

        public int Create(Advertisement Advertis)
        {
            Context.Advertisements.Add(Advertis);
            int row = Context.SaveChanges();
            return row;
        }
        public int Update(int id, Advertisement Advertis)
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

            int row = Context.SaveChanges();
            return row;
        }
        public int Delete(int id)
        {
            Advertisement OldAdvertis = Context.Advertisements.FirstOrDefault(s => s.Id == id);
            Context.Advertisements.Remove(OldAdvertis);
            int row = Context.SaveChanges();
            return row;
        }
    }
}
