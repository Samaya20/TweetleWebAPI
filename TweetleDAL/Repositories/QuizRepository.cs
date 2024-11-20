using Microsoft.EntityFrameworkCore;
using TweetleDAL.Interfaces;
using TweetleModels.Entities;

namespace TweetleDAL.Services
{
    public class QuizRepository : IQuizRepository
    {
        private readonly TweetleDbContext _context;

        public QuizRepository(TweetleDbContext context)
        {
            _context = context;
        }

        // Get all quizs
        public async Task<List<Quiz>> GetAllQuizsAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }

        // Get a quiz by id
        public async Task<Quiz> GetQuizByIdAsync(Guid id)
        {
            return await _context.Quizzes.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Create a new quiz
        public async Task<Quiz> CreateQuizAsync(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        // Update an existing quiz
        public async Task<Quiz> UpdateQuizAsync(Guid id, Quiz quiz)
        {
            var existingQuiz = await _context.Quizzes.FindAsync(id);
            if (existingQuiz == null) return null;

            existingQuiz.Title = quiz.Title;

            await _context.SaveChangesAsync();
            return existingQuiz;
        }

        // Delete a quiz
        public async Task<bool> DeleteQuizAsync(Guid id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null) return false;

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
