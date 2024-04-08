using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
    //الكنترولر كامل محتاج تيست علشان عندي مشكلة في الداتا بيز
{
    [Route("api/[controller]")]//هنخليه  authurized للادمن فقط
    [ApiController]
    public class AdminController : ControllerBase
    {
         ICustomerRepository customer;
         ICraftsmenRepository craftsman;
         IAppReportRepository appReport;
         IUserReportRepository userReport;
        IAdvertisementRepository advertisement;

        public AdminController(ICustomerRepository customerRepository, ICraftsmenRepository craftsmanRepository,
            IAppReportRepository appReportRepository, IUserReportRepository userReportRepository, IAdvertisementRepository advertisementRepository)
        {
            customer = customerRepository;
            craftsman = craftsmanRepository;
            appReport = appReportRepository;
            userReport = userReportRepository;
            advertisement = advertisementRepository;
        }

        [HttpGet("customers")]
        public IActionResult GetAllCustomers()
        {
            var customers = customer.GetAll();
            return Ok(customers);
        }

        [HttpGet("craftsmen")]
        public IActionResult GetAllCraftsmen()
        {
            var craftsmen = craftsman.GetAll();
            return Ok(craftsmen);
        }

        [HttpGet("app-reports")]
        public IActionResult GetAllAppReports()
        {
            var appReports = appReport.GetAll();
            return Ok(appReports);
        }

        [HttpGet("user-reports")]
        public IActionResult GetAllUserReports()
        {
            var userReports =userReport.GetAll();
            return Ok(userReports);
        }
        [HttpGet("advertisement")]
        public IActionResult GetAlladvertisement()
        {
            var advertisements = advertisement.GetAll();
            return Ok(advertisement);
        }
    }
}
