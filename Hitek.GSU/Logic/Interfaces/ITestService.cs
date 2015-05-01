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

        object CheckTest(Hitek.GSU.Models.Validation.Test.TestForCheack raw);

        /// <summary>
        /// Получение подробностей о прохождении по ID.
        /// </summary>
        /// <param name="id">ID истории</param>
        /// <returns></returns>
        HistoryResult GetHistoryTestById(long id);

        /// <summary>
        /// История прохождения тестов пользователя по ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICollection<HistoryResult> GetAllHistoryTestByUserId(long id);
    }
}
