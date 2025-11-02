namespace Contracts.Interfaces
{
    public interface IAccessToken
    {
        string access_token { get; set; }
        int expires_at { get; set; }
        int expires_in { get; set; }
        string refresh_token { get; set; }
        string token_type { get; set; }
    }
}