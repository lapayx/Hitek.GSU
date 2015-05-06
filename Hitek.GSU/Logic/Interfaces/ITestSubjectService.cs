using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitek.GSU.Logic.Interfaces
{
    public interface ITestSubjectService
    {
        TestSubject GetTestSubjectById(long id);

        ICollection<TestSubject> GetAllTestSubjects();

        bool AddTestSubject(TestSubject newItem);

        bool EditTestSubject(long id, TestSubject editSubject);

        ICollection<TestInfo> GetListTestBySubjectId(long id);

        bool DeleteTestSubjectById(long id);

    }
}
