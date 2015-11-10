using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models.Validation.Admin.Test;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hitek.GSU.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DefaultController : Controller
    {
        readonly ITestSubjectService subjectService;
        readonly ITestService testservice;
        public DefaultController(ITestSubjectService service, ITestService testservice)
        {
            this.subjectService = service;
            this.testservice = testservice;
        }


        [HttpGet]
        // GET: Default
        public ActionResult Index()
        {
            ViewBag.subjects = subjectService.GetAllTestSubjects();
            return View();
        }

        [HttpPost]
        // GET: Default
        public ActionResult Index(long subjectId,string title,HttpPostedFileBase file)
            
        {
            string line;
            CreatingTest res = new CreatingTest();
            res.Questions = new List<CreatingTestQuestion>();
            res.Title = title;
            res.SubjectId = subjectId;
            CreatingTestQuestion tq = null;
            CreatingTestAnswer ta = null;
            if (file.ContentLength > 0) {

                StreamReader c = new StreamReader(file.InputStream);
                while(!c.EndOfStream){
                    line = c.ReadLine();
                    if (line.Length > 0)
                    {
                        if (line.IndexOf("1::") > -1) {
                            if (tq != null) {
                                res.Questions.Add(tq);
                            }
                            tq = new CreatingTestQuestion();

                            tq.Title = line.Substring(line.IndexOf("::") + 2, line.IndexOf("::",3) + 2);
                            tq.Content = line.Substring(line.IndexOf("::",3)+2);
                            tq.Answers = new List<CreatingTestAnswer>();
                            tq.IsRemoved = false;
                            continue;
                        }
                        ta = new CreatingTestAnswer();
                        ta.Content = line.Substring(1);
                        if (line[0] == '=') {
                            ta.IsRight = true;
                        }
                        if (tq != null) {
                            tq.Answers.Add(ta);
                        }
                    }

                }
                if (tq != null)
                {
                    res.Questions.Add(tq);
                }
                c.Close();
            }
           
            return Json(testservice.CreateOrEditTest(res));
        }


        [HttpPut]
        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
