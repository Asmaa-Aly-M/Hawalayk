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
        private readonly IAdvertisementRepository _advertisementRepository;

        public AdvertisementController(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        [HttpGet("GetAllAdvertisements")]
        public async Task<ActionResult<List<Advertisement>>> GetAll()
        {
            List<Advertisement> advertisements = await _advertisementRepository.GetAll();
            
            return Ok(advertisements);
        }

        [HttpGet("GetAdvertisement/{id}")]
        public async Task<ActionResult<Advertisement>> GetById(int id)//test
        {
            var advertisement = await _advertisementRepository.GetById(id);

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

        [HttpPost("CreateAdvertisement")]
        public async Task<IActionResult> Create(Advertisement advertisement)//test//لازم نضيف انها  autourized للادمن فقط
        {
            await _advertisementRepository.Create(advertisement);
            return Ok();
        }

        [HttpDelete("DeleteAdvertisement/{id}")]     
        public async Task<IActionResult> Delete(int id)//test لازم نضيف انها  autourized للادمن فقط
        {
            var existingAdvertisement = await _advertisementRepository.GetById(id);
            if (existingAdvertisement == null)
            {
                return NotFound();
            }
            await _advertisementRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("UpdateAdvertisement/{id}")] //test لازم نضيف انها  autourized للادمن فقط
        public async Task<IActionResult> Update(int id, Advertisement updatedAdvertisement)
        {
            var existingAdvertisement = await _advertisementRepository.GetById(id);

            if (existingAdvertisement == null)
            {
                return NotFound();
            }


            await _advertisementRepository.Update(id,existingAdvertisement);

            return NoContent();
        }
    }
}
