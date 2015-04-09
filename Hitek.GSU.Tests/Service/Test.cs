using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hitek.GSU.Logic.Service;
using Hitek.GSU.Logic.Interfaces;
using Moq;
using System.Data.Entity;
using System.Linq;
using DataBase = Hitek.GSU.Logic.Database;

namespace Hitek.GSU.Tests.Service
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestMethod1()
        {
            var context = new TestContext();

            context.Test.Add(new DataBase.Test { Id = 1, Name = "123" });
            context.TestQuestion.Add(new DataBase.TestQuestion { Id = 1, TestId = 1 });
            context.TestAnswer.Add(new DataBase.TestAnswer { Id = 1, TestQuestionId = 1 });


            var testService = new TestService(context);
            var res = testService.GetTestById(1);
            Assert.AreEqual("", res.ToString());  
        }
    }
}
