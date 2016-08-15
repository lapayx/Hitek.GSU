using Hitek.GSU.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitek.GSU.Models;

namespace Hitek.GSU.Logic.Service
{
    public class TestSubjectService : ITestSubjectService
    {

        readonly ITestRepository testRepository;
        public TestSubjectService(ITestRepository testRepositpry)
        {
            this.testRepository = testRepositpry;
        }

        public TestSubject GetTestSubjectById(long id)
        {
            return testRepository.TestSubject.Where(x => x.Id == id).Select(x => new TestSubject() { Id = x.Id,Name=x.Name}).FirstOrDefault();
        }


        public ICollection<TestSubject> GetAllTestSubjects()
        {

            /*var allSubject = this.testRepository.TestSubject.ToList();
            IList<TestSubject> res = new List<TestSubject>();
            var t = allSubject.Where(x => x.ParentId == null).ToList();
            foreach(var s in t){
                 res.Add(new TestSubject()
                {
                    Id = s.Id,
                    Name = s.Name,
                    //Childrens = getChildrenSubject(allSubject, s.Id)
                    ParentId = s.ParentId
                });
            
            }
            return res;*/
            return this.testRepository.TestSubject.Select(x => new TestSubject()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId
                }
            ).ToList();
        }

        public bool DeleteTestSubjectById(long id)
        {
            Hitek.GSU.Logic.Database.Model.TestSubject t = new Hitek.GSU.Logic.Database.Model.TestSubject(){Id= id};

            testRepository.TestSubject.Attach(t);
            testRepository.TestSubject.Remove(t);
            testRepository.SaveChanges();
            return true;
            
        }

        private IList<TestSubject> getChildrenSubject(List<Hitek.GSU.Logic.Database.Model.TestSubject> src, long parentId) { 
            
            IList<TestSubject> res = new List<TestSubject>();

            var t = src.Where(x => x.ParentId == parentId).ToList();
            foreach (var s in t) {
                res.Add(new TestSubject()
                {
                    Id = s.Id,
                    Name = s.Name,
                    //Childrens = getChildrenSubject(src,s.Id)
                });
                
            }
            

            return res;
 
        
        }

        public bool EditTestSubject(long id, TestSubject editSubject)
        {
            var temp = testRepository.TestSubject.Find(id);
            temp.Name = editSubject.Name;
            temp.ParentId = editSubject.ParentId;
            testRepository.SaveChanges();
            return true;
        }
        public bool AddTestSubject(TestSubject editSubject)
        {
            var temp = new Database.Model.TestSubject()
            {
                Name = editSubject.Name,
                ParentId = editSubject.ParentId
                
            };

            testRepository.TestSubject.Add(temp);
            testRepository.SaveChanges();
            return true;
        }
    }
}