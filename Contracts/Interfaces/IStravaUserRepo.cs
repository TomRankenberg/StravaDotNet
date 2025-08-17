namespace Contracts.Interfaces
{
    public interface IStravaUserRepo
    {
        public Task AddUserAsync(IStravaUser user);
        public Task<IStravaUser> GetUserByIdAsync(int id);
        public Task UpdateUserAsync(IStravaUser user);
        public void DeleteUser();

    }
}
