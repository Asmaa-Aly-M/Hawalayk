using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IGovernorateRepository _governorateRepository;
        private readonly ICityRepository _cityRepository;

        public AddressController(IAddressRepository addressRepository, IGovernorateRepository governorateRepository, ICityRepository cityRepository)
        {
            _addressRepository = addressRepository;
            _governorateRepository = governorateRepository;
            _cityRepository = cityRepository;
        }

        [HttpGet("getAllAddresses")]

        public async Task<IActionResult> GetAllAddresses()
        {
            var addresses = await _addressRepository.GetAllAsync();
            return Ok(addresses);
        }


        [HttpGet]
        [Route("address/{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }


        [HttpGet("governorates")]
        public async Task<IActionResult> GetGovernorateNames()
        {
            var governorateNames = await _governorateRepository.GetGovernorateNamesAsync();
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
                var cityNames = await _cityRepository.GetCitiesNamesByGovernorateNameAsync(governorateName);
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
                var address = await _addressRepository.CreateAsync(addressDTO.GovernorateName, addressDTO.CityName, addressDTO.StreetName);
                return Ok(address);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
