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
        readonly ITestService testService;

        public TestHistoryService(IWorkTestRepository testRepositpry, ITestService testService)
        {
            this.testRepository = testRepositpry;
            this.testService = testService;
        }

        public TestFull GetHistoryTestById(long id)
        {
            var res = testService.GetExistTestById(id,true);

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
                                            StartDate = x.StartDate,
                                            IsCanShowResultAnswer  = x.IsCanShowResultAnswer
                                        })
                                        .ToList();
            return res;
        }
    }
}