using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetleModels.Entities
{
    public class Quiz : BaseEntity
    {
        public string? Title { get; set; }

        // Foreign key
        public Guid LessonId { get; set; }

        // Navigation properties
        public Lesson? Lesson { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<QuizAttempt>? QuizAttempts { get; set; }
    }
}
