using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IGovernorateService _governorateService;
        private readonly ICityService _cityService;

        public AddressController(IAddressService addressService, IGovernorateService governorateService, ICityService cityService)
        {
            _addressService = addressService;
            _governorateService = governorateService;
            _cityService = cityService;
        }

        [HttpGet]
        [Route("addresses")]
        public async Task<IActionResult> GetAllAddresses()
        {
            var addresses = await _addressService.GetAllAsync();
            return Ok(addresses);
        }


        [HttpGet]
        [Route("address/{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var address = await _addressService.GetByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }


        [HttpGet("governorates")]
        public async Task<IActionResult> GetGovernorateNames()
        {
            var governorateNames = await _governorateService.GetGovernorateNamesAsync();
            return Ok(governorateNames);
        }


        [HttpGet("cities/{governorateName}")]
        public async Task<IActionResult> GetCityNamesByGovernorate(string governorateName)
        {
            if (string.IsNullOrEmpty(governorateName))
            {
                return BadRequest("Governorate name cannot be null or empty");
            }
            try
            {
                var cityNames = await _cityService.GetCitiesNamesByGovernorateNameAsync(governorateName);
                return Ok(cityNames);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressDTO addressDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var address = await _addressService.CreateAsync(addressDTO.GovernorateName, addressDTO.CityName, addressDTO.StreetName);
                return Ok(address);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
