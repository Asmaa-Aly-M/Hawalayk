using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class ImageRepository : IImageRepository
    {
        ApplicationDbContext Context;
        public ImageRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }

        public Image GetById(int id)
        {
            Image image = Context.Images.FirstOrDefault(s => s.Id == id);
            return image;
        }
        public List<Image> GetAll()
        {
            return Context.Images.ToList();
        }

        public int Create(Image newImage)
        {
            Context.Images.Add(newImage);
            int row = Context.SaveChanges();
            return row;
        }
        public int Update(int id, Image newImage)
        {
            Image OldImage = Context.Images.FirstOrDefault(s => s.Id == id);
            OldImage.Path = newImage.Path;
            OldImage.DatePosted = newImage.DatePosted;
            OldImage.Description = newImage.Description;
            int row = Context.SaveChanges();
            return row;
        }
        public int Delete(int id)
        {
            Image OldImage = Context.Images.FirstOrDefault(s => s.Id == id);
            Context.Images.Remove(OldImage);
            int row = Context.SaveChanges();
            return row;
        }
    }
}
