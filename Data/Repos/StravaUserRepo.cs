using Contracts.Interfaces;
using Data.Context;
using Data.Models;

namespace Data.Repos
{
    public class StravaUserRepo(DatabaseContext context) : IStravaUserRepo
    {
        public void AddUser(IStravaUser user)
        {
            var entity = user as StravaUser;
            context.Users.Add(entity);
            context.SaveChanges();
        }
        public void UpdateUser(IStravaUser user)
        {
            var entity = user as StravaUser;
            context.Users.Update(entity);
            context.SaveChangesAsync();
        }
        public IStravaUser GetUserById(int id)
        {
            return context.Users.FirstOrDefault(x => x.UserId == id);
        }
        public void DeleteUser()
        {
            throw new NotImplementedException();
        }
    }
}
