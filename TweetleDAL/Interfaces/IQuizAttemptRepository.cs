using TweetleModels.Entities;

namespace TweetleDAL.Interfaces
{
    public interface IQuizAttemptRepository 
    {
        Task<List<QuizAttempt>> GetAllQuizAttemptsAsync();
        Task<QuizAttempt> GetQuizAttemptByIdAsync(Guid id);
        Task<QuizAttempt> CreateQuizAttemptAsync(QuizAttempt quizAttempt);
        Task<QuizAttempt> UpdateQuizAttemptAsync(Guid id, QuizAttempt quizAttempt);
        Task<bool> DeleteQuizAttemptAsync(Guid id);
    }
}
