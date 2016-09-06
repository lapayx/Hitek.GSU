using System.Data.Entity;
using Hitek.GSU.Logic.Database;
using Hitek.GSU.Logic.Database.Model;

namespace Hitek.GSU.Logic.Interfaces
{
    public interface IWorkTestRepository : ISavingRepository
    {

        IDbSet<WorkTest> WorkTest { get; }

        IDbSet<WorkTestAnswer> WorkTestAnswer { get; }

        IDbSet<WorkTestQuestion> WorkTestQuestion { get; }
    }
      
}
