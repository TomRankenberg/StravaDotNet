namespace Contracts.Interfaces
{
    public interface IStravaUserRepo
    {
        public Task AddUserAsync(IStravaUser user);
        public IStravaUser GetUserById(int id);
        public Task UpdateUserAsync(IStravaUser user);
        public void DeleteUser();

    }
}
