using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetleModels.Entities;

namespace TweetleModels.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {   
        public string? Level { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = null;

        // Navigation Property
        public ICollection<QuizAttempt>? QuizAttempts { get; set; }
        public ICollection<Progress>? Progresses { get; set; }
    }
}
