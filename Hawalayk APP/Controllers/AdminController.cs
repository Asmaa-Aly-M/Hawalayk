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
        private readonly ISMSService _smsService;
        private readonly IBanService _banService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ISMSService smsService, IBanService banService, ICustomerRepository customerRepository, ICraftsmenRepository craftsmanRepository,
            IAppReportRepository appReportRepository, IUserReportRepository userReportRepository,
            IAdvertisementRepository advertisementRepository, UserManager<ApplicationUser> userManager)
        {
            _smsService = smsService;
            _banService = banService;
            _customerRepository = customerRepository;
            _craftsmanRepository = craftsmanRepository;
            _appReportRepository = appReportRepository;
            _userReportRepository = userReportRepository;
            _advertisementRepository = advertisementRepository;
            _userManager = userManager;
        }

        [HttpGet("customers")]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerRepository.GetAll();
            return Ok(customers);
        }

        [HttpGet("craftsmen")]
        public IActionResult GetAllCraftsmen()
        {
            var craftsmen = _craftsmanRepository.GetAll();
            return Ok(craftsmen);
        }

        [HttpGet("app-reports")]
        public IActionResult GetAllAppReports()
        {
            var appReports = _appReportRepository.GetAll();
            return Ok(appReports);
        }

        [HttpGet("user-reports")]
        public IActionResult GetAllUserReports()
        {
            var userReports = _userReportRepository.GetAll();
            return Ok(userReports);
        }
        [HttpGet("advertisement")]
        public IActionResult GetAlladvertisement()
        {
            var advertisements = _advertisementRepository.GetAll();
            return Ok(advertisements);
        }
        [HttpGet]
        [Route("admin/craftsmen/pending")]
        public async Task<IActionResult> GetPendingCraftsmen()
        {
            var pendingCraftsmen = await _craftsmanRepository.GetPendingCraftsmen();
            return Ok(pendingCraftsmen);
        }


        [HttpPut]
        [Route("admin/craftsmen/{id}")]
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

        [HttpPost("admin/ban-user")]
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

        [HttpPost("admin/unban-user")]
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

        [HttpGet("admin/banned-users")]
        public async Task<IActionResult> GetBannedUsers()
        {
            var bannedUsers = await _banService.GetBannedUsersAsync();
            return Ok(bannedUsers);
        }
    }
}
