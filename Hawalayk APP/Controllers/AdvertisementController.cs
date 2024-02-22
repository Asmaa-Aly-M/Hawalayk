using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
         AdvertisementRepository advertisementRepository;

        public AdvertisementController(AdvertisementRepository _advertisementRepository)
        {
            advertisementRepository = _advertisementRepository;
        }

        [HttpGet]
        public ActionResult<List<Advertisement>> GetAllAdvertisements()
        {
            var advertisements = advertisementRepository.GetAll();
            return Ok(advertisements);
        }

        [HttpGet("{id}")]
        public ActionResult<Advertisement> GetAdvertisementById(int id)
        {
            var advertisement = advertisementRepository.GetById(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return Ok(advertisement);
        }
        [HttpPost]
        public IActionResult Create(Advertisement advertisement)
        {
            advertisementRepository.Create(advertisement);
            return CreatedAtRoute("GetAdvertisement", new { id = advertisement.Id }, advertisement);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingAdvertisement = advertisementRepository.GetById(id);
            if (existingAdvertisement == null)
            {
                return NotFound();
            }
            advertisementRepository.Delete(id);
            return NoContent();
        }
    }
}
