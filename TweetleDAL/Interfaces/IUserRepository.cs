using TweetleModels.IdentityModels;

namespace TweetleDAL.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllApplicationUsersAsync();
        Task<ApplicationUser> GetApplicationUserByIdAsync(Guid id);
        Task<ApplicationUser> CreateApplicationUserAsync(ApplicationUser user);
        Task<ApplicationUser> UpdateApplicationUserAsync(Guid id, ApplicationUser user);
        Task<bool> DeleteApplicationUserAsync(Guid id);
    }
}
