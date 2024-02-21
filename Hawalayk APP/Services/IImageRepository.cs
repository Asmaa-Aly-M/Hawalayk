using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IImageRepository
    {
        int Create(Image newImage);
        int Delete(int id);
        List<Image> GetAll();
        Image GetById(int id);
        int Update(int id, Image newImage);
    }
}