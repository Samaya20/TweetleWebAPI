using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetleModels.IdentityModels;

namespace TweetleModels.Entities
{
    public class QuizAttempt : BaseEntity
    {
        public int? Score { get; set; }

        // Foreign key
        public Guid ApplicationUserId { get; set; }
        public Guid QuizId { get; set; }

        // Navigation properties
        public ApplicationUser? ApplicationUser { get; set; }
        public Quiz? Quiz { get; set; }

    }
}
