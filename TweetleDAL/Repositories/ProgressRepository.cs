using Microsoft.EntityFrameworkCore;
using TweetleDAL.Interfaces;
using TweetleModels.Entities;

namespace TweetleDAL.Services
{
    public class ProgressRepository : IProgressRepository
    {
        private readonly TweetleDbContext _context;

        public ProgressRepository(TweetleDbContext context)
        {
            _context = context;
        }

        // Get all progresss
        public async Task<List<Progress>> GetAllProgressesAsync()
        {
            return await _context.Progresses.ToListAsync();
        }

        // Get a progress by id
        public async Task<Progress> GetProgressByIdAsync(Guid id)
        {
            return await _context.Progresses.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Create a new progress
        public async Task<Progress> CreateProgressAsync(Progress progress)
        {
            _context.Progresses.Add(progress);
            await _context.SaveChangesAsync();
            return progress;
        }

        // Update an existing progress
        public async Task<Progress> UpdateProgressAsync(Guid id, Progress progress)
        {
            var existingProgress = await _context.Progresses.FindAsync(id);
            if (existingProgress == null) return null;

            existingProgress.CompletedLessons = progress.CompletedLessons;

            await _context.SaveChangesAsync();
            return existingProgress;
        }

        // Delete a progress
        public async Task<bool> DeleteProgressAsync(Guid id)
        {
            var progress = await _context.Progresses.FindAsync(id);
            if (progress == null) return false;

            _context.Progresses.Remove(progress);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
