using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetleModels.Entities
{
    public class Course : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Level { get; set; }

        //Navigation properties
        public ICollection<Lesson>? Lessons { get; set; }
        public ICollection<Progress>? Progresses { get; set; }

    }
}
