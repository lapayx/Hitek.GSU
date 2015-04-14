using System.Data.Entity;
using Hitek.GSU.Logic.Database;

namespace Hitek.GSU.Logic.Interfaces
{
    public interface ITestRepository : ISavingRepository
    {
        /// <summary>
        /// 
        /// </summary>
        IDbSet<Test> Test { get; }

        IDbSet<TestAnswer> TestAnswer { get; }

        IDbSet<TestQuestion> TestQuestion { get; }
        
        IDbSet<TestHistory> TestHistory { get; }
    }
}
