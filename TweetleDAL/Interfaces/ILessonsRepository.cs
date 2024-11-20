using TweetleModels.Entities;

namespace TweetleDAL.Interfaces
{
    public interface ILessonsRepository
    {
        Task<List<Lesson>> GetAllLessonsAsync();
        Task<Lesson> GetLessonByIdAsync(Guid id);
        Task<Lesson> CreateLessonAsync(Lesson lesson);
        Task<Lesson> UpdateLessonAsync(Guid id, Lesson lesson);
        Task<bool> DeleteLessonAsync(Guid id);
    }
}
