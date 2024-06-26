﻿using Hawalayk_APP.Context;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class BanRepository : IBanRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BanRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task CreateAsync(string userId, int banDurationInHours)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.IsBanned = true;
                await _userManager.UpdateAsync(user);

                var ban = new Ban
                {
                    UserId = userId,
                    BanDurationInMinutes = banDurationInHours*60
                };

                _context.Bans.Add(ban);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnbanUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.IsBanned = false;
                await _userManager.UpdateAsync(user);

                var ban = await _context.Bans.FirstOrDefaultAsync(b => b.UserId == userId);
                if (ban != null)
                {
                    _context.Bans.Remove(ban);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<bool> IsUserBannedAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return user.IsBanned;
        }

        public async Task<List<ApplicationUser>> GetBannedUsersAsync()
        {
            var bannedUsers = await _context.ApplicationUsers.Where(u => u.IsBanned == true).ToListAsync();
            return bannedUsers;
        }

    }
}
