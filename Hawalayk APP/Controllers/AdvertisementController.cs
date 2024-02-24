using Hawalayk_APP.DataTransferObject;
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
         IAdvertisementRepository advertisementRepository;

        public AdvertisementController(IAdvertisementRepository _advertisementRepository)
        {
            advertisementRepository = _advertisementRepository;
        }

        [HttpGet]
        public ActionResult<List<Advertisement>> GetAll()
        {
            List<Advertisement> advertisements = advertisementRepository.GetAll();
            
            return Ok(advertisements);
        }

        [HttpGet("{id}")]
        public ActionResult<Advertisement> GetById(int id)
        {
            var advertisement = advertisementRepository.GetById(id);

            if (advertisement == null)
            {
                return NotFound();
            }
            AdvertisementData advertisementData = new AdvertisementData();
            advertisementData.ID = advertisement.Id;
            advertisementData.Image= advertisement.Image;
            advertisementData.EebSitURL = advertisement.ClickUrl;

            return Ok(advertisementData);
        }
        //[HttpPost]
        //public IActionResult Create(Advertisement advertisement)
        //{
        //    advertisementRepository.Create(advertisement);
        //    return CreatedAtRoute("GetAdvertisement", new { id = advertisement.Id }, advertisement);
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var existingAdvertisement = advertisementRepository.GetById(id);
        //    if (existingAdvertisement == null)
        //    {
        //        return NotFound();
        //    }
        //    advertisementRepository.Delete(id);
        //    return NoContent();
        //}
    }
}
