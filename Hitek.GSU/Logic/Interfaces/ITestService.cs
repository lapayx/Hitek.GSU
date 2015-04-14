using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitek.GSU.Logic.Interfaces
{
    public interface ITestService
    {
        /// <summary>
        /// Получение теста его вопросов и ответов по ID
        /// </summary>
        /// <param name="id">ID Теста</param>
        /// <returns></returns>
        TestFull GetTestById(long id);

        object CheackTest(Hitek.GSU.Models.Validation.Test.TestForCheack raw);
    }
}
