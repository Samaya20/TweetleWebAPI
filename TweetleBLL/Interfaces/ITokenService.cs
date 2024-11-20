using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetleBLL.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(string userId, string role);
    }
}
