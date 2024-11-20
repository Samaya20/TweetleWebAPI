using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TweetleModels.Entities;
using TweetleModels.IdentityModels;

namespace TweetleDAL
{
    public class TweetleDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public TweetleDbContext(DbContextOptions<TweetleDbContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<Progress> Progresses { get; set; }
    }
}
