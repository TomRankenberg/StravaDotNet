using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Data.Interfaces;
using Data.Models;

namespace Data.Repos
{
    public class StravaUserRepo(DatabaseContext context) : IStravaUserRepo
    {
        public void AddUser(StravaUser user)
        {
            context.Users.Add(user);
            context.SaveChangesAsync();
        }
        public void UpdateUser(StravaUser user)
        {
            context.Users.Update(user);
            context.SaveChangesAsync();
        }
        public StravaUser GetUserById(int id)
        {
            return context.Users.FirstOrDefault(x => x.UserId == id);
        }
        public void DeleteUser()
        {
            throw new NotImplementedException();
        }
    }
}
