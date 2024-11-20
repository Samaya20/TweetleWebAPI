using Microsoft.EntityFrameworkCore;
using TweetleDAL.Interfaces;
using TweetleModels.Entities;

namespace TweetleDAL.Services
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly TweetleDbContext _context;

        public QuestionRepository(TweetleDbContext context)
        {
            _context = context;
        }

        // Get all questions
        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await _context.Questions.ToListAsync();
        }

        // Get a question by id
        public async Task<Question> GetQuestionByIdAsync(Guid id)
        {
            return await _context.Questions.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Create a new question
        public async Task<Question> CreateQuestionAsync(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        // Update an existing question
        public async Task<Question> UpdateQuestionAsync(Guid id, Question question)
        {
            var existingQuestion = await _context.Questions.FindAsync(id);
            if (existingQuestion == null) return null;

            existingQuestion.Content = question.Content;
            existingQuestion.Option1 = question.Option1;
            existingQuestion.Option2 = question.Option2;
            existingQuestion.Option3 = question.Option3;
            existingQuestion.Option4 = question.Option4;
            existingQuestion.CorrectAnswer = question.CorrectAnswer;

            await _context.SaveChangesAsync();
            return existingQuestion;
        }

        // Delete a question
        public async Task<bool> DeleteQuestionAsync(Guid id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return false;

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
