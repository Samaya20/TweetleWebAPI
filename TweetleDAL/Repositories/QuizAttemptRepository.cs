using Microsoft.EntityFrameworkCore;
using TweetleDAL.Interfaces;
using TweetleModels.Entities;

namespace TweetleDAL.Services
{
    public class QuizAttemptRepository : IQuizAttemptRepository
    {
        private readonly TweetleDbContext _context;

        public QuizAttemptRepository(TweetleDbContext context)
        {
            _context = context;
        }

        // Get all quizAttempts
        public async Task<List<QuizAttempt>> GetAllQuizAttemptsAsync()
        {
            return await _context.QuizAttempts.ToListAsync();
        }

        // Get a quizAttempt by id
        public async Task<QuizAttempt> GetQuizAttemptByIdAsync(Guid id)
        {
            return await _context.QuizAttempts.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Create a new quizAttempt
        public async Task<QuizAttempt> CreateQuizAttemptAsync(QuizAttempt quizAttempt)
        {
            _context.QuizAttempts.Add(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        // Update an existing quizAttempt
        public async Task<QuizAttempt> UpdateQuizAttemptAsync(Guid id, QuizAttempt quizAttempt)
        {
            var existingQuizAttempt = await _context.QuizAttempts.FindAsync(id);
            if (existingQuizAttempt == null) return null;

            existingQuizAttempt.Score = quizAttempt.Score;

            await _context.SaveChangesAsync();
            return existingQuizAttempt;
        }

        // Delete a quizAttempt
        public async Task<bool> DeleteQuizAttemptAsync(Guid id)
        {
            var quizAttempt = await _context.QuizAttempts.FindAsync(id);
            if (quizAttempt == null) return false;

            _context.QuizAttempts.Remove(quizAttempt);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
