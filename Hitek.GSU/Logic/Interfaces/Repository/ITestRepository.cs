using System.Data.Entity;
using Hitek.GSU.Logic.Database;

namespace Hitek.GSU.Logic.Interfaces
{
    public interface ITestRepository : ISavingRepository
    {
        /// <summary>
        /// Тесты.
        /// </summary>
        IDbSet<Test> Test { get; }

        /// <summary>
        /// Ответоы к вопросам тестов.
        /// </summary>
        IDbSet<TestAnswer> TestAnswer { get; }

        /// <summary>
        /// Репозиторий вопросов к тестам
        /// </summary>
        IDbSet<TestQuestion> TestQuestion { get; }
        
        /// <summary>
        /// История прохождения тестов.
        /// </summary>
        IDbSet<TestHistory> TestHistory { get; }

        /// <summary>
        /// Темы тестов.
        /// </summary>
        IDbSet<TestSubject> TestSubject { get; }
    }
}
