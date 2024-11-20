using TweetleModels.Entities;

namespace TweetleDAL.Interfaces
{
    public interface IProgressRepository
    {
        Task<List<Progress>> GetAllProgressesAsync();
        Task<Progress> GetProgressByIdAsync(Guid id);
        Task<Progress> CreateProgressAsync(Progress progress);
        Task<Progress> UpdateProgressAsync(Guid id, Progress progress);
        Task<bool> DeleteProgressAsync(Guid id);
    }
}
