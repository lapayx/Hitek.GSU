using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Service
{
    public class TestHistoryService: ITestHistoryService
    {
        readonly ITestRepository testRepository;

        public TestHistoryService(ITestRepository testRepositpry, IAccountService accountService)
        {
            this.testRepository = testRepositpry;
        }

        public HistoryResult GetHistoryTestById(long id)
        {
            HistoryResult res = testRepository.TestHistory.Where(x => x.Id == id)
                .Select(x => new HistoryResult() { Id = x.Id, Name = x.Test.Name, Result = x.Result, Date = x.Date })
                .FirstOrDefault();

            return res;
        }
        public IList<HistoryResult> GetAllHistoryTestByUserId(long userId)
        {
            IList<HistoryResult> res = testRepository.TestHistory
                                        .Where(x => x.AccountId == userId)
                                        .OrderByDescending(x=>x.Id)
                                        .Select(x => new HistoryResult()
                                        {
                                            Id = x.Id,
                                            Name = x.Test.Name,
                                            Result = x.Result,
                                            Date = x.Date
                                        })
                                        .ToList();
            return res;
        }
    }
}