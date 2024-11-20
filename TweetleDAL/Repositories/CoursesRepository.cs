using Microsoft.EntityFrameworkCore;
using TweetleDAL.Interfaces;
using TweetleModels.Entities;

namespace TweetleDAL.Services
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly TweetleDbContext _context;

        public CoursesRepository(TweetleDbContext context)
        {
            _context = context;
        }

        // Get all courses
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        // Get a course by id
        public async Task<Course> GetCourseByIdAsync(Guid id)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Create a new course
        public async Task<Course> CreateCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        // Update an existing course
        public async Task<Course> UpdateCourseAsync(Guid id, Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(id);
            if (existingCourse == null) return null;

            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.Level = course.Level;

            await _context.SaveChangesAsync();
            return existingCourse;
        }

        // Delete a course
        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
