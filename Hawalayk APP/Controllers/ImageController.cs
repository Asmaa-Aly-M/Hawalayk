using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Image = Hawalayk_APP.Models.Image;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            imageRepository = _imageRepository;
        }

        [HttpGet]
        public ActionResult<List<Image>> GetAllImages()
        {
            var images = _imageRepository.GetAll();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public ActionResult<Image> GetImageById(int id)
        {
            var image = _imageRepository.GetById(id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }
        [HttpPost]
        public IActionResult Create(Image image)
        {
            _imageRepository.Create(image);
            return CreatedAtRoute("GetAdvertisement", new { id = image.Id }, image);
        }
    }
}
