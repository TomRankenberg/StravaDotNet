namespace Contracts.Interfaces
{
    public interface IStravaUser
    {
        string AccessToken { get; set; }
        string AccessTokenExpiresAt { get; set; }
        string Email { get; set; }
        string RefreshToken { get; set; }
        string Secret { get; set; }
        int StravaId { get; set; }
        int UserId { get; set; }
    }
}