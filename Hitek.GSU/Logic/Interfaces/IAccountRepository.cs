using System.Data.Entity;
using Hitek.GSU.Logic.Database;

namespace Hitek.GSU.Logic.Interfaces
{
    public interface IAccountRepository: ISavingRepository
    {
        /// <summary>
        /// 
        /// </summary>
        DbSet<Account> Account { get; set; }

    }
}
