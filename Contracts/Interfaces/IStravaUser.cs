namespace Contracts.Interfaces
{
    public interface IStravaUser
    {
        string AccessToken { get; set; }
        string AccessTokenExpiresAt { get; set; }
        string RefreshToken { get; set; }
        int UserId { get; set; }
    }
}