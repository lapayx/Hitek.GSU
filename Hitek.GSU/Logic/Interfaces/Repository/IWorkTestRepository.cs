using System.Data.Entity;
using Hitek.GSU.Logic.Database;
using Hitek.GSU.Logic.Database.Model;

namespace Hitek.GSU.Logic.Interfaces
{
    public interface IWorkTestRepository : ISavingRepository
    {

        IDbSet<WorkTest> Test { get; }

        IDbSet<WorkTestAnswer> TestAnswer { get; }

        IDbSet<WorkTestQuestion> TestQuestion { get; }
    }
      
}
