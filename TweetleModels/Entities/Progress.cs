using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetleModels.IdentityModels;

namespace TweetleModels.Entities
{
    public class Progress : BaseEntity
    {
        public int? CompletedLessons { get; set; }

        // Foreign keys
        public Guid ApplicationUserId { get; set; }
        public Guid CourseId { get; set; }

        // Navigation Properties
        public ApplicationUser? ApplicationUser { get; set; }
        public Course? Course { get; set; }

    }
}
