
using Contracts.Interfaces;

namespace Data.Models
{
    public class AccessToken : IAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_at { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}
