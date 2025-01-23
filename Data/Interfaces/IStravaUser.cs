using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Interfaces
{
    public interface IStravaUserRepo
    {
        public void AddUser(StravaUser user);
        public StravaUser GetUserById(int id);
        public void UpdateUser(StravaUser user);
        public void DeleteUser();

    }
}
