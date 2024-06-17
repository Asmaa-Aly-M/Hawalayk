using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class BlockingRepository : IBlockingRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlockingRepository(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task BlockUserAsync(string blockingUserId, string blockedUserId)
        {
            if (blockingUserId == blockedUserId)
            {
                throw new ArgumentException("A user cannot block themself.");
            }

            var blockingUser = await _userManager.FindByIdAsync(blockingUserId);
            var blockedUser = await _userManager.FindByIdAsync(blockedUserId);

            if (blockingUser == null || blockedUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            bool isBlocked = await IsUserBlockedAsync(blockingUserId, blockedUserId);

            if (isBlocked)
            {
                throw new Exception("User is already blocked.");
            }

            var newBlock = new Block
            {
                BlockingUser = blockingUser,
                BlockedUser = blockedUser,
            };

            _applicationDbContext.Blocks.Add(newBlock);
            await _applicationDbContext.SaveChangesAsync();
        }


        public async Task<bool> IsUserBlockedAsync(string blockingUserId, string blockedUserId)
        {
            if (string.IsNullOrEmpty(blockingUserId))
            {
                throw new ArgumentNullException(nameof(blockingUserId));
            }

            if (string.IsNullOrEmpty(blockedUserId))
            {
                throw new ArgumentNullException(nameof(blockedUserId));
            }

            var blockingUser = await _userManager.FindByIdAsync(blockingUserId);
            var blockedUser = await _userManager.FindByIdAsync(blockedUserId);

            if (blockingUser == null || blockedUser == null)
                throw new KeyNotFoundException("User not found.");

            var existingBlock = await _applicationDbContext.Blocks
                .FirstOrDefaultAsync(b => b.BlockingUserId == blockingUserId || b.BlockedUserId == blockingUserId);

            if (existingBlock != null)
                return true;
            else
                return false;
        }


        public async Task UnblockUserAsync(string blockingUserId, string blockedUserId)
        {
            if (string.IsNullOrEmpty(blockingUserId))
            {
                throw new ArgumentNullException(nameof(blockingUserId));
            }

            if (string.IsNullOrEmpty(blockedUserId))
            {
                throw new ArgumentNullException(nameof(blockedUserId));
            }

            var existingBlock = await _applicationDbContext.Blocks
                .FirstOrDefaultAsync(b => b.BlockingUserId == blockingUserId && b.BlockedUserId == blockedUserId);

            if (existingBlock == null)
            {
                throw new Exception("User is not blocked.");
            }

            _applicationDbContext.Blocks.Remove(existingBlock);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<string>> GetBlockedUsersAsync(string userId)
        {
            var blockedUsers = await _applicationDbContext.Blocks
                .Where(b => b.BlockingUserId == userId || b.BlockedUserId == userId)
                .Select(b => b.BlockingUserId == userId ? b.BlockedUserId : b.BlockingUserId)
                .ToListAsync();

            return blockedUsers;
        }

        public async Task<List<BlockedUserDTO>> GetMyBlockedUsersAsync(string userId)
        {
            var myBlockedUsers = await _applicationDbContext.Blocks
                .Where(b => b.BlockingUserId == userId)
                .Select(b => b.BlockedUser)
                .ToListAsync();

            var myBlockedUsersDto = myBlockedUsers.Select(user => new BlockedUserDTO
            {
                BlockedUserId = user.Id,
                BlockedUserFirstName = user.FirstName,
                BlockedUserLastName = user.LastName,
                BlockedUserProfilePicture = user.ProfilePicture
            }).ToList();

            return myBlockedUsersDto;
        }

    }
}
