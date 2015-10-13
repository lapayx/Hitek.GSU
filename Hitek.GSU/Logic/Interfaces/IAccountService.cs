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
        ApplicationUser GetCurrentUser();

    }
}
