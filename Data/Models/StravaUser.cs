using Contracts.Interfaces;

namespace Data.Models
{
    public class StravaUser : IStravaUser
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string AccessTokenExpiresAt { get; set; }
    }
}
