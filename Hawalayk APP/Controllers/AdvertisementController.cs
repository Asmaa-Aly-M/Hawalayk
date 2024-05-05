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
        public async Task<ActionResult<List<Advertisement>>> GetAll()
        {
            List<Advertisement> advertisements = await advertisementRepository.GetAll();
            
            return Ok(advertisements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Advertisement>> GetById(int id)//test
        {
            var advertisement = await advertisementRepository.GetById(id);

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

        [HttpPost]
        public async Task<IActionResult> Create(Advertisement advertisement)//test//لازم نضيف انها  autourized للادمن فقط
        {
            await advertisementRepository.Create(advertisement);
            return Ok();
        }

        [HttpDelete("{id}")]     
        public async Task<IActionResult> Delete(int id)//test لازم نضيف انها  autourized للادمن فقط
        {
            var existingAdvertisement = await advertisementRepository.GetById(id);
            if (existingAdvertisement == null)
            {
                return NotFound();
            }
            await advertisementRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")] //test لازم نضيف انها  autourized للادمن فقط
        public async Task<IActionResult> Update(int id, Advertisement updatedAdvertisement)
        {
            var existingAdvertisement = await advertisementRepository.GetById(id);

            if (existingAdvertisement == null)
            {
                return NotFound();
            }


            await advertisementRepository.Update(id,existingAdvertisement);

            return NoContent();
        }
    }
}
