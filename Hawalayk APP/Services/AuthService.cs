using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Helpers;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Hawalayk_APP.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IApplicationUserService _appUser;
        private readonly ICraftRepository _craftsService;
        private readonly JWT _jwt;
        private readonly IAddressService _addressService;
        private readonly ISMSService _smsService;
        //public AuthService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, IOptions<JWT> jwt, ISMSService smsService)

        public AuthService(IAddressService addressService, UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, ICraftRepository craftsService, IOptions<JWT> jwt, ISMSService smsService, IApplicationUserService appUser, SignInManager<ApplicationUser> signInManager)

        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _craftsService = craftsService;
            _jwt = jwt.Value;
            _smsService = smsService;
            _appUser = appUser;
            _signInManager = signInManager;
            _addressService = addressService;
        }
        public async Task<AuthModel> RegisterCustomerAsync(RegisterCustomerModel model)
        {

            IFormFile file = model.ProfilePic;
            string fileName = file.FileName;
            string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
            using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            if (await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber) != null)
                return new AuthModel { Message = "Phone number is already registered!" };

            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return new AuthModel { Message = "UserName is already registered!" };

            var customer = new Customer
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                ProfilePicture = fileName,
                Address = await _addressService.CreateAsync(model.Goveronrate, model.City, model.Street),
                Gender = model.Gender,
                BirthDate = model.BirthDate,
                IsOtpVerified = false
            };

            var result = await _userManager.CreateAsync(customer, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(customer, "Customer");


            var jwtSecurityToken = await CreateJwtToken(customer);


            var otpToken = _smsService.GenerateOTP(false, 4);


            var smsResult = await _smsService.SendSMS(model.PhoneNumber, $"Your OTP is: {otpToken}");

            if (String.IsNullOrEmpty(smsResult.ErrorMessage))
            {
                var otpEntity = new OTPToken
                {
                    UserId = customer.Id,
                    PhoneNumber = model.PhoneNumber,
                    Token = otpToken,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(60)
                };

                _applicationDbContext.OTPTokens.Add(otpEntity);
                await _applicationDbContext.SaveChangesAsync();

                return new AuthModel
                {
                    UserName = customer.UserName,
                    Gender = customer.Gender,
                    PhoneNumber = customer.PhoneNumber,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = new List<string> { "Customer" },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    //Address = customer.Address,
                };
            }
            else
            {
                return new AuthModel { Message = smsResult.ErrorMessage };
            }

        }

        public async Task<AuthModel> ForgotPasswordAsync(string phoneNumber)
        {
            var user = await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            if (user == null)
                return new AuthModel { Message = "Phone number is not correct" };

            var otpToken = _smsService.GenerateOTP(false, 4);
            var smsResult = await _smsService.SendSMS(phoneNumber, $"Your OTP is: {otpToken}");

            if (String.IsNullOrEmpty(smsResult.ErrorMessage))
            {
                var otpEntity = new OTPToken
                {
                    UserId = user.Id,
                    PhoneNumber = phoneNumber,
                    Token = otpToken,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(5)
                };

                _applicationDbContext.OTPTokens.Add(otpEntity);
                await _applicationDbContext.SaveChangesAsync();

                return new AuthModel { Message = "We have sent an OTP code to your phone number.", ActionSucceeded = true };
            }

            else
                return new AuthModel { Message = smsResult.ErrorMessage };
        }

        public async Task<AuthModel> ResetPasswordAsync(ResetPasswordModel model)
        {
            var otpEntity = await _applicationDbContext.OTPTokens.FirstOrDefaultAsync(t => t.Token == model.OTPToken);
            if (otpEntity == null || otpEntity.ExpirationTime < DateTime.Now)
                return new AuthModel { Message = "Invalid token" };

            var user = await _userManager.FindByIdAsync(otpEntity.UserId);
            if (user == null)
                return new AuthModel { Message = "user not found!" };

            _applicationDbContext.OTPTokens.Remove(otpEntity);
            await _applicationDbContext.SaveChangesAsync();


            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModel { Message = errors };
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            return new AuthModel
            {
                UserName = user.UserName,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = rolesList.ToList(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
        }

        public async Task<AuthModel> RegisterCraftsmanAsync(RegisterCraftsmanModel model)
        {
            IFormFile file = model.PersonalImage;
            IFormFile filee = model.NationalIdImage;
            IFormFile profilePic = model.ProfilePic;
            string fileName = file.FileName;
            string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
            using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            string profilePicName = profilePic.FileName;
            string profilePicPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
            using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
            {
                profilePic.CopyTo(fileStream);
            }

            string fileeName = filee.FileName;
            string fileePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
            using (var fileStream = new FileStream(Path.Combine(fileePath, fileeName), FileMode.Create))
            {
                filee.CopyTo(fileStream);
            }
            //Images image = new Images();


            //   model.PersonalImage = fileName;


            if (await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber) != null)
                return new AuthModel { Message = "Phone number is already registered!" };

            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return new AuthModel { Message = "UserName is already registered!" };


            var craft = await _craftsService.GetOrCreateCraftAsync(model.CraftName.ToString());

            var craftsman = new Craftsman
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                Craft = craft,
                ProfilePicture = profilePicName,
                PersonalImage = fileName,
                NationalIDImage = fileeName,
                RegistrationStatus = CraftsmanRegistrationStatus.Pending,
                BirthDate = model.BirthDate,
                Address = await _addressService.CreateAsync(model.Goveronrate, model.City, model.Street),
            };

            var result = await _userManager.CreateAsync(craftsman, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(craftsman, "Craftsman");


            var jwtSecurityToken = await CreateJwtToken(craftsman);

            var otpToken = _smsService.GenerateOTP(false, 4);


            var smsResult = await _smsService.SendSMS(model.PhoneNumber, $"Your OTP is: {otpToken}");

            if (String.IsNullOrEmpty(smsResult.ErrorMessage))
            {
                var otpEntity = new OTPToken
                {
                    UserId = craftsman.Id,
                    PhoneNumber = model.PhoneNumber,
                    Token = otpToken,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(60)
                };

                _applicationDbContext.OTPTokens.Add(otpEntity);
                await _applicationDbContext.SaveChangesAsync();

                return new AuthModel
                {
                    UserName = craftsman.UserName,
                    Gender = craftsman.Gender,
                    PhoneNumber = craftsman.PhoneNumber,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = new List<string> { "Craftsman" },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    //Address = craftsman.Address,
                };
            }
            else
            {
                return new AuthModel { Message = smsResult.ErrorMessage };
            }

        }

        public async Task<AuthModel> VerifyOTPAsync(string otp)
        {
            var otpEntity = await _applicationDbContext.OTPTokens.FirstOrDefaultAsync(t=> t.Token == otp && t.ExpirationTime > DateTime.UtcNow);
            if (otpEntity == null)
                return new AuthModel { Message = "Invalid OTP!" };

            var user = await _userManager.FindByIdAsync(otpEntity.UserId);
            if (user == null)
                return new AuthModel { Message = "User not found!" };

            _applicationDbContext.OTPTokens.Remove(otpEntity);
            user.IsOtpVerified = true;
            await _userManager.UpdateAsync(user);
            await _applicationDbContext.SaveChangesAsync();

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            return new AuthModel
            {
                UserName = user.UserName,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = rolesList.ToList(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
        }

        public async Task<AuthModel> ResendOTPAsync(string phoneNumber)
        {
            var user = await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            if (user == null)
            {
                return new AuthModel { Message = "Phone number is not registered!" };
            }

            var otpToken = _smsService.GenerateOTP(false, 4);
            var smsResult = await _smsService.SendSMS(phoneNumber, $"Your OTP is: {otpToken}");

            if (String.IsNullOrEmpty(smsResult.ErrorMessage))
            {
                var existingOtpTokens = await _applicationDbContext.OTPTokens.Where(t => t.UserId == user.Id).ToListAsync();
                _applicationDbContext.OTPTokens.RemoveRange(existingOtpTokens);

                _applicationDbContext.OTPTokens.Add(new OTPToken
                {
                    UserId = user.Id,
                    PhoneNumber = phoneNumber,
                    Token = otpToken,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(60)
                });

                await _applicationDbContext.SaveChangesAsync();

                return new AuthModel { Message = "We have sent a new OTP code to your phone number.", ActionSucceeded = true };
            }
            else
            {
                return new AuthModel { Message = smsResult.ErrorMessage, ActionSucceeded = false };
            }
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,

                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );


            return token;
        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {

            var authModel = new AuthModel();
            var user = await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = " The PhoneNumber Or Password Is Not Correct ";
                return authModel;
            }
            if(user.IsOtpVerified == false)
            {
                authModel.Message = "OTP code is not verified yet";
            }

            if (user is Craftsman && ((Craftsman)user).RegistrationStatus != CraftsmanRegistrationStatus.Approved)
            {
                authModel.Message = "Account not approved yet.";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var Roles = await _userManager.GetRolesAsync(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.PhoneNumber = user.PhoneNumber;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;

            authModel.Roles = Roles.ToList();
            return authModel;
        }

        public async Task<DeleteUserDTO> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new DeleteUserDTO
                {
                    isDeleted = false,
                    Message = "This User Not Found : "
                };
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return new DeleteUserDTO
                {
                    isDeleted = false,
                    Message = "Failed To Delete This User : "
                };
            }

            return new DeleteUserDTO
            {
                isDeleted = true,
                Message = "The User Deleted Successfully : "
            };
        }

        public async Task<string> LogoutAsync(string userId)
        {
            if (await _appUser.getCurrentUser(userId) == null)
            {
                return "User Not Found";
            }
            await _signInManager.SignOutAsync();
            return "User Logged Out Successfully";
        }



    }
}

