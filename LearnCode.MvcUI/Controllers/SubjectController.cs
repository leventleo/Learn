using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnCode.Bussiness.Interfaces;
using LearnCode.Entities;
using LearnCode.MvcUI.Models;
using LLearnCode.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnCode.MvcUI.Controllers
{

    public class SubjectController : Controller
    {
        private readonly ILesson _lesson;
        private readonly IContent _content;
        private readonly ISubject _subject;
        private readonly IViewLevel _viewLevel;

        public SubjectController(ILesson lesson, IContent content, ISubject subject, IViewLevel viewLevel)
        {
            _lesson = lesson;
            _content = content;
            _subject = subject;
            _viewLevel = viewLevel;
        }





        public async Task<IActionResult> Add()
        {

            AddViewModel model = new AddViewModel
            {
                Lessons = await _lesson.GetList(),
                Subject = new Subject()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(string subject, string id)
        {
            if (!string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(subject))
            {
                Subject model = new Subject
                {
                    Lessonid = Convert.ToInt32(id),
                    SubjectName = subject

                };

                _subject.Add(model);
                _subject.Save();
                TempData["result"] = "Saved..";
            }
            else
            {
                TempData["result"] = "Please fill in text....";
            }



            return RedirectToAction("List", "Lesson");
        }

        //public IActionResult Remove()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Remove(int id = 0 )
        { //TODO : Refactoring Unit of Work
            var lessonid = 0;
            if (id > 0)
            {
                 
                    var removeSubject = _subject.FindById(id);
                bool viewlevelChangeState = false; 
                    lessonid = removeSubject.Lessonid;
                    _subject.Remove(removeSubject);
                var getlist = _content.GetList(x => x.Subjectid == id).Result;
                    getlist.ForEach(x =>
                    {
                        var removeContent = _content.FindById(x.id);
                        _content.Remove(removeContent);
                        if (_viewLevel.GetList(v => v.Contentid == x.id).Result.Count>0)
                        {
                            _viewLevel.GetList(v => v.Contentid == x.id).Result.ForEach(f =>
                            {
                                var removeLevel = _viewLevel.FindById(f.id);
                                _viewLevel.Remove(removeLevel);
                                viewlevelChangeState = true;
                            });
                        }
                       

                    });

                if (viewlevelChangeState) {
                    _viewLevel.Save();
                };

                _content.Save();
                _subject.Save();
                
               
                    
                
            }

            return RedirectToAction("subjectlist","lesson", new {id=lessonid});
        }

        [HttpPost]
        public PartialViewResult Getchildlist(int id=0)
        {
            var model = new ListViewModel();
            if (id>0)
            {
                var subject = _subject.FindById(id);
                model.SubjectName = subject.SubjectName;
                model.selectedLesson = _lesson.FindById(subject.Lessonid).id;
                model.Contents =  _content.GetList(x => x.Subjectid == id).Result;
              
                model.Contents.ForEach(x => {
                                      
                    x.ViewLevels =_viewLevel.GetList(v => v.Contentid == x.id).Result;
 
                });
            }

            return PartialView(model);
            

        }

    }
}