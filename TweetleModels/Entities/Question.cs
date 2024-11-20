using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetleModels.Entities
{
    public class Question : BaseEntity
    {
        public string? Content { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public int? CorrectAnswer { get; set; }

        // Foreign key
        public Guid QuizId { get; set; }

        // Navigation properties
        public Quiz? Quiz { get; set; }
    }
}
