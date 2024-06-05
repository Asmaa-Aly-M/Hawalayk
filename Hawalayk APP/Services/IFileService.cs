namespace Hawalayk_APP.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folder);
    }
}
