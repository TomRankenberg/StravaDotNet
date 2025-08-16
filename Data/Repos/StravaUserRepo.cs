using Contracts.Interfaces;
using Data.Context;
using Data.Models;

namespace Data.Repos
{
    public class StravaUserRepo(DatabaseContext context) : IStravaUserRepo
    {
        public async Task AddUserAsync(IStravaUser user)
        {
            var entity = user as StravaUser;
            context.Users.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(IStravaUser user)
        {
            var entity = user as StravaUser;
            context.Users.Update(entity);
            await context.SaveChangesAsync();
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