using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetleModels.Entities;
using TweetleModels.IdentityModels;

namespace TweetleModels.Dtos
{
    public class UserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
