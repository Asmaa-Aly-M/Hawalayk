using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hawalayk_APP.Controllers
//الكنترولر كامل محتاج تيست علشان عندي مشكلة في الداتا بيز
{
    [Route("api/[controller]")]//هنخليه  authurized للادمن فقط
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICraftsmenRepository _craftsmanRepository;
        private readonly IAppReportRepository _appReportRepository;
        private readonly IUserReportRepository _userReportRepository;
        private readonly IAdvertisementRepository _advertisementRepository;

        private readonly IServiceRequestRepository serviceRequestRepo;
        private readonly ISMSService _smsService;
        private readonly IBanService _banService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ISMSService smsService, IBanService banService, ICustomerRepository customerRepository, ICraftsmenRepository craftsmanRepository,
            IAppReportRepository appReportRepository, IUserReportRepository userReportRepository,
            IAdvertisementRepository advertisementRepository, UserManager<ApplicationUser> userManager, IServiceRequestRepository _serviceRequestRepo)
        {
            _smsService = smsService;
            _banService = banService;
            _customerRepository = customerRepository;
            _craftsmanRepository = craftsmanRepository;
            _appReportRepository = appReportRepository;
            _userReportRepository = userReportRepository;
            _advertisementRepository = advertisementRepository;
            _userManager = userManager;
            _serviceRequestRepo = serviceRequestRepo;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAll();
            return Ok(customers);
        }

        [HttpGet("craftsmen")]
        public async Task<IActionResult> GetAllCraftsmen()
        {
            var craftsmen = await _craftsmanRepository.GetAll();
            return Ok(craftsmen);
        }

        [HttpGet("app-reports")]
        public async Task<IActionResult> GetAllAppReports()
        {
            var appReports = await _appReportRepository.GetAll();
            return Ok(appReports);
        }

        [HttpGet("user-reports")]
        public async Task<IActionResult> GetAllUserReports()
        {
            var userReports = await _userReportRepository.GetAllUserReports();
            return Ok(userReports);
        }

        [HttpGet("advertisement")]
        public async Task<IActionResult> GetAlladvertisement()
        {
            var advertisements = await _advertisementRepository.GetAll();
            return Ok(advertisements);
        }

        [HttpGet("craftsmen/pending")]
        public async Task<IActionResult> GetPendingCraftsmen()
        {
            var pendingCraftsmen = await _craftsmanRepository.GetPendingCraftsmen();
            return Ok(pendingCraftsmen);
        }

        [HttpPut("approveCraftsman/{id}")]
        public async Task<IActionResult> ApproveCraftsman(string id, bool isApproved)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Craftsman ID cannot be empty.");
            }
            try
            {
                var craftsman = await _craftsmanRepository.ApproveCraftsman(id, isApproved);
                _smsService.SendCraftsmanApprovalNotification(craftsman.PhoneNumber, isApproved);
                return Ok("Craftsman approval status updated. SMS notification sent.");
            }

            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("ban-user")]
        public async Task<IActionResult> BanUser(string userId, int banDurationInMinutes)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            if (banDurationInMinutes <= 0)
            {
                return BadRequest("Ban duration must be greater than zero");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.IsBanned = true;
            await _userManager.UpdateAsync(user);

            await _banService.CreateAsync(userId, banDurationInMinutes);

            return Ok("User banned successfully.");
        }

        [HttpPost("unban-user")]
        public async Task<IActionResult> UnbanUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            if (!await _banService.IsUserBannedAsync(userId))
            {
                return NotFound("User is not currently banned.");
            }

            await _banService.UnbanUserAsync(userId);

            return Ok("User unbanned successfully.");
        }

        [HttpGet("banned-users")]
        public async Task<IActionResult> GetBannedUsers()
        {
            var bannedUsers = await _banService.GetBannedUsersAsync();
            return Ok(bannedUsers);
        }
        [HttpGet("last 5 service request")]
        public IActionResult Last5TopServic()
        {

            return Ok(serviceRequestRepo.GetLatestServiceRequests());

        }
        [HttpGet("CountUsersMakingRequestsLastMonth")]
        public IActionResult TotalNumberOfUsersMakeingRequstLastMonth()
        {

            return Ok(serviceRequestRepo.CountUsersMakingRequestsLastMonth());

        }
        [HttpGet("CountUsersMakingRequestsLastWeek")]
        public IActionResult TotalNumberOfUsersMakeingRequstLastWeek()
        {

            return Ok(serviceRequestRepo.CountUsersMakingRequestsLastWeek());

        }
        [HttpGet("CountUsersMakingRequestsToday")]
        public IActionResult TotalNumberOfUsersMakeingRequstToday()
        {

            return Ok(serviceRequestRepo.CountUsersMakingRequestsToday());

        }
    }
}
