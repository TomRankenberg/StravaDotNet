using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class StravaUser
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int StravaId { get; set; }
        public string Secret { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string AccessTokenExpiresAt { get; set; }
    }
}
