using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Interfaces
{
    public interface ITestHistoryService
    {
        /// <summary>
        /// Получение подробностей о прохождении по ID.
        /// </summary>
        /// <param name="id">ID истории</param>
        /// <returns></returns>
        TestFull GetHistoryTestById(long id);

        /// <summary>
        /// История прохождения тестов пользователя по ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<HistoryResult> GetAllHistoryTestByUserId(long id);
    }
}