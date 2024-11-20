using TweetleModels.Entities;

namespace TweetleDAL.Interfaces
{
    public interface ICoursesRepository
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(Guid id);
        Task<Course> CreateCourseAsync(Course course);
        Task<Course> UpdateCourseAsync(Guid id, Course course);
        Task<bool> DeleteCourseAsync(Guid id);
    }
}
