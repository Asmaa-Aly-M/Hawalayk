using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;
using Hawalayk_APP.Context;
using Microsoft.EntityFrameworkCore;
using Hawalayk_APP.Helpers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Hawalayk_APP.DataTransferObject;

namespace Hawalayk_APP.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _jwt = jwt.Value;
        }
        public async Task<AuthModel> RegisterCustomerAsync(RegisterCustomerModel model)
        {
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
                Gender = model.Gender,
                Address = model.Address
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

            return new AuthModel
            {
                UserName = customer.UserName,
                Gender = customer.Gender,
                PhoneNumber = customer.PhoneNumber,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "Customer" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Address = customer.Address,
            };

        }

        public async Task<AuthModel> RegisterCraftsmanAsync(RegisterCraftsmanModel model)
        {
            throw new NotImplementedException();

        }


        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim(ClaimTypes.Role, role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }






        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {

            var authModel = new AuthModel();
            var user = await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);

            if(user==null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = " The PhoneNumber Or Password Is Not Correct ";
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
    }
}
