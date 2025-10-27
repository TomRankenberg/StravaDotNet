namespace Contracts.Interfaces
{
    public interface IAuthService
    {
        Task<IAccessToken?> GetAccessTokenAsync(string authorizationCode);
        Task RemoveCredentialsAsync();
        Task<IStravaUser> ValidateCredentialsAsync();
    }
}