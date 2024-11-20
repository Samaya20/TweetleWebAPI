using Microsoft.EntityFrameworkCore;
using TweetleDAL.Interfaces;
using TweetleModels.Entities;

namespace TweetleDAL.Services
{
    public class LessonsRepository : ILessonsRepository
    {
        private readonly TweetleDbContext _context;

        public LessonsRepository(TweetleDbContext context)
        {
            _context = context;
        }

        // Get all lessons
        public async Task<List<Lesson>> GetAllLessonsAsync()
        {
            return await _context.Lessons.ToListAsync();
        }

        // Get a lesson by id
        public async Task<Lesson> GetLessonByIdAsync(Guid id)
        {
            return await _context.Lessons.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Create a new lesson
        public async Task<Lesson> CreateLessonAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        // Update an existing lesson
        public async Task<Lesson> UpdateLessonAsync(Guid id, Lesson lesson)
        {
            var existingLesson = await _context.Lessons.FindAsync(id);
            if (existingLesson == null) return null;

            existingLesson.Title = lesson.Title;
            existingLesson.Content = lesson.Content;

            await _context.SaveChangesAsync();
            return existingLesson;
        }

        // Delete a lesson
        public async Task<bool> DeleteLessonAsync(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null) return false;

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
