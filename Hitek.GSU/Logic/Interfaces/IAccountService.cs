using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitek.GSU.Logic.Interfaces
{
    public interface IAccountService
    {
        long GetCurrentUserId();
        //User GetCurrentUser();

        IList<User> GetAllUsers();
        User GetUserById(long id);

        bool AddRole(long userId, string role);
        bool RemoveRole(long userId, params string[] role);



    }
}
