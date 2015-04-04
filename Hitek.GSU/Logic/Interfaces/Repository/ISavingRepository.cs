using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitek.GSU.Logic.Interfaces
{
      /// <summary>
    /// Интерфейс базового репозитория с возможностью сохраниения.
    /// </summary>
    public interface ISavingRepository
    {
        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        int SaveChanges();
    }

}
