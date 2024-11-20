using TweetleModels.Entities;

namespace TweetleDAL.Interfaces
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetAllQuizsAsync();
        Task<Quiz> GetQuizByIdAsync(Guid id);
        Task<Quiz> CreateQuizAsync(Quiz quiz);
        Task<Quiz> UpdateQuizAsync(Guid id, Quiz quiz);
        Task<bool> DeleteQuizAsync(Guid id);
    }
}
