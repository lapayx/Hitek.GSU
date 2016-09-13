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
        readonly IWorkTestRepository testRepository;

        public TestHistoryService(IWorkTestRepository testRepositpry, IAccountService accountService)
        {
            this.testRepository = testRepositpry;
        }

        public HistoryResult GetHistoryTestById(long id)
        {
            HistoryResult res = testRepository.WorkTest.Where(x => x.Id == id)
                .Select(x => new HistoryResult() {
                    Id = x.Id,
                    Name = x.Test.Name,
                    Result = x.Result,
                    EndDate = x.EndDate,
                    StartDate = x.StartDate
                })
                .FirstOrDefault();

            return res;
        }
        public IList<HistoryResult> GetAllHistoryTestByUserId(long userId)
        {
            IList<HistoryResult> res = testRepository.WorkTest
                                        .Where(x => x.UserId == userId)
                                        .OrderByDescending(x=>x.Id)
                                        .Select(x => new HistoryResult()
                                        {
                                            Id = x.Id,
                                            Name = x.Test.Name,
                                            Result = x.Result,
                                            EndDate = x.EndDate,
                                            StartDate = x.StartDate
                                        })
                                        .ToList();
            return res;
        }
    }
}