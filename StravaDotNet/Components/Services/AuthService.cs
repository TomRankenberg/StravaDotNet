using Contracts.Interfaces;

namespace StravaDotNet.Components.Services
{
    public class AuthService(IStravaUserRepo stravaUserRepo)
    {
        public async Task<IStravaUser> ValidateCredentialsAsync()
        {
            IStravaUser user = await stravaUserRepo.GetUserByIdAsync(1);
            return user;
        }

        public async Task RemoveCredentialsAsync()
        {
            await stravaUserRepo.RemoveCredentialsByIdAsync(1);
        }
    }
}
