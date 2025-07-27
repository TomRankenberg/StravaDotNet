namespace Contracts.Interfaces
{
    public interface IStravaUserRepo
    {
        public void AddUser(IStravaUser user);
        public IStravaUser GetUserById(int id);
        public void UpdateUser(IStravaUser user);
        public void DeleteUser();

    }
}
