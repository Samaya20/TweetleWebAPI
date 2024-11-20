using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetleModels.Entities
{
    public class Lesson : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        // Foreign key
        public Guid CourseId { get; set; }

        // Navigation properties
        public Course? Course { get; set; }
        public ICollection<Quiz>? Quizzes { get; set; }
    }
}
