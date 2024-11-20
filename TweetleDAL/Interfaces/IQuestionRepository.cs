using TweetleModels.Entities;

namespace TweetleDAL.Interfaces
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(Guid id);
        Task<Question> CreateQuestionAsync(Question question);
        Task<Question> UpdateQuestionAsync(Guid id, Question question);
        Task<bool> DeleteQuestionAsync(Guid id);
    }
}
