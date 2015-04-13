using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hitek.GSU.Logic.Service;
using Hitek.GSU.Logic.Interfaces;
using Moq;
using System.Data.Entity;
using System.Linq;
using DB = Hitek.GSU.Logic.Database;
using M = Hitek.GSU.Models;

namespace Hitek.GSU.Tests.Service
{
    [TestClass]
    public class Test
    {

        ITestService testService;


        public  Test()
        {
            var context = new TestContext();
            DB.TestQuestion question1 = new DB.TestQuestion()
            {
                Id=1,
                Name ="Первый",
                TestId = 1,
                Text = "Вопрос",
                TestAnswers = new List<DB.TestAnswer>() { new DB.TestAnswer { Id = 1, Text = "Верный", IsRight = false, TestQuestionId = 1 } }
            };
            var test1 = new DB.Test
            { 
                Id = 1, 
                Name = "123",
                TestQuestions = new List<DB.TestQuestion>() { question1 }
            };
            context.Test.Add(test1);
            context.TestQuestion.Add(question1);
            context.TestAnswer.Add(new DB.TestAnswer { Id = 1, TestQuestionId = 1 });





            testService = new TestService(context);
            
        }
        [TestMethod]
        public void TestServiceGetTestById()
        {
            M.TestFull etalon = new M.TestFull
            {
                Id = 1,
                Name = "123",
                
                Questions = new List<M.TestQuestion>(){
                    new M.TestQuestion(){
                        Id = 1,
                        Name = "Первый",
                        Text = "Вопрос",
                        Answers = new List<M.TestAnswer>(){
                            new M.TestAnswer(){
                                Id =1,
                                Text = "Верный",
                                IsRight = false,
                                Name = ""
                            }
                        }
                    }
                
                }
            };

            var testRes = testService.GetTestById(1);
            Assert.AreEqual(etalon.ToJSON(),testRes.ToJSON() ,"Не совпадают"); 
  
        }


    }
}
