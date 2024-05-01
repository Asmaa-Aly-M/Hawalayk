using Hawalayk_APP.Context;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Microsoft.AspNetCore.Identity;

public class SeedingDataService : ISeedingDataService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public SeedingDataService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public void SeedingData()
    {
        if (_context.ApplicationUsers.Any())
        {
            return;
        }

        var defaultPassword = "aYa@111@";

        var superAdmin = new Admin
        {
            FirstName = "User",
            LastName = "User",
            UserName = "SuperAdmin",
            Gender = Gender.Male,
            BirthDate = DateTime.Now,
            EmailConfirmed = false,
            Email = "a@gmail.com",
            PhoneNumber = "+201000000000",
            ProfilePicture = "j.jpg",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            AccessFailedCount = 0,
        };

        var craftsman = new Craftsman
        {
            FirstName = "User",
            LastName = "User",
            UserName = "Craftsman",
            Gender = Gender.Male,
            BirthDate = DateTime.Now,
            EmailConfirmed = false,
            Email = "aa@gmail.com",
            PhoneNumber = "+201000000001",
            ProfilePicture = "j.jpg",
            PersonalImage = "j.jpg",
            NationalIDImage = "j.jpg",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            AccessFailedCount = 0,
            Rating = 0.0,
            Craft = new Craft
            {
                Name = CraftName.painter,
            }
        };

        var customer = new Customer
        {
            FirstName = "User",
            LastName = "User",
            UserName = "Customer",
            Gender = Gender.Male,
            BirthDate = DateTime.Now,
            EmailConfirmed = false,
            Email = "a@gmail.com",
            PhoneNumber = "+201000000002",
            ProfilePicture = "j.jpg",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            AccessFailedCount = 0,
        };

        _userManager.CreateAsync(superAdmin, defaultPassword).Wait();
        _userManager.AddToRoleAsync(superAdmin, "SuperAdmin").Wait();

        _userManager.CreateAsync(craftsman, defaultPassword).Wait();
        _userManager.AddToRoleAsync(craftsman, "Craftsman").Wait();

        _userManager.CreateAsync(customer, defaultPassword).Wait();
        _userManager.AddToRoleAsync(customer, "Customer").Wait();
    }
}
