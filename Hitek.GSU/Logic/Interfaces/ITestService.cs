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

        ICollection<TestInfo> GetAllTest();

        ICollection<TestInfo> GetTestBySubjectId(long subjectId);

        /// <summary>
        /// Получение теста его вопросов и ответов по ID
        /// </summary>
        /// <param name="id">ID Теста</param>
        /// <returns></returns>
        TestFull GetTestById(long id,bool isNew);

        object CheckTest(Hitek.GSU.Models.Validation.Test.TestForCheack raw);

        object CreateOrEditTest(Hitek.GSU.Models.Validation.Admin.Test.CreatingTest raw);

        

        void DeleteTestById(long id);
        Hitek.GSU.Models.Validation.Admin.Test.CreatingTest GetTestForEditById( long id);
    
    }
}
