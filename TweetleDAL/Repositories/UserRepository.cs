using Microsoft.EntityFrameworkCore;
using TweetleDAL.Interfaces;
using TweetleModels.IdentityModels;

namespace TweetleDAL.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly TweetleDbContext _context;

        public UserRepository(TweetleDbContext context)
        {
            _context = context;
        }

        // Get all users
        public async Task<List<ApplicationUser>> GetAllApplicationUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Get a user by id
        public async Task<ApplicationUser> GetApplicationUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Id == id.ToString());
        }

        // Create a new user
        public async Task<ApplicationUser> CreateApplicationUserAsync(ApplicationUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Update an existing user
        public async Task<ApplicationUser> UpdateApplicationUserAsync(Guid id, ApplicationUser user)
        {
            var existingApplicationUser = await _context.Users.FindAsync(id);
            if (existingApplicationUser == null) return null;

            existingApplicationUser.UserName = user.UserName;
            existingApplicationUser.Level = user.Level;
            existingApplicationUser.Level = user.Level;

            await _context.SaveChangesAsync();
            return existingApplicationUser;
        }

        // Delete a user
        public async Task<bool> DeleteApplicationUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
