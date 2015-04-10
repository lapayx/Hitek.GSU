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
            DataBase.TestQuestion question1 = new DataBase.TestQuestion(){
                Id=1,
                Name ="Первый",
                TestId = 1,
                Text = "Вопрос",
                TestAnswers = new List<DataBase.TestAnswer>(){new DataBase.TestAnswer { Id = 1, TestQuestionId = 1 }}
            };
            var test1 = new DataBase.Test { 
                Id = 1, 
                Name = "123",
                TestQuestions = new List< DataBase.TestQuestion>(){question1}
            };
            context.Test.Add(test1);
            context.TestQuestion.Add(question1);
            context.TestAnswer.Add(new DataBase.TestAnswer { Id = 1, TestQuestionId = 1 });





            var testService = new TestService(context);
            var res = testService.GetTestById(1);
            Assert.AreEqual("", res.ToString());  
        }
    }
}
