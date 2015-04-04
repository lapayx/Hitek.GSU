using System.Data.Entity;
using Hitek.GSU.Logic.Database;

namespace Hitek.GSU.Logic.Interfaces
{
    public interface IAccountRepository: ISavingRepository
    {
        /// <summary>
        /// 
        /// </summary>
        IDbSet<Account> Account { get; }

    }
}
