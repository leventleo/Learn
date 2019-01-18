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
    public class ContentController : Controller
    {

        private readonly ILesson _lesson;
        private readonly IContent _content;
        private readonly ISubject _subject;
        private readonly IViewLevel _viewLevel;

        public ContentController(ILesson lesson, IContent content, ISubject subject, IViewLevel viewLevel)
        {
            _lesson = lesson;
            _content = content;
            _subject = subject;
            _viewLevel = viewLevel;
        }



        [HttpPost]
        public IActionResult Add(string content, string id ,string returnUrl ,string subjectid)
        {

            if (!string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(id))
            {
                Content model = new Content
                {
                    Subjectid = Convert.ToInt32(id),
                    ContentName = content

                };

                _content.Add(model);
                _content.Save();

            }


            if (returnUrl=="contentlist")
            {
                return RedirectToAction(returnUrl, "Content", new { id = id });
            }
            else
            {
                return RedirectToAction(returnUrl, "lesson", new { id = subjectid });
            }
           

        }


        public async Task<IActionResult> contentlist(int id, string ReturnUrl)
        {
            ListViewModel model = new ListViewModel();

            model.Contents =await _content.GetList(x => x.Subjectid == id);
            model.viewLevels = await _viewLevel.GetList();
            var subject = _subject.FindById(id);
            model.SubjectName = subject.SubjectName;
            model.selectedSubject = subject.id;
            model.LessonName = _lesson.FindById(_subject.FindById(id).Lessonid).LessonName;
            if (model.Contents.Count < 1)
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                return View(model);
            }


        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id = 0)
        {

            if (id > 0)
            {
                var updateEntity = _content.FindById(id);
                updateEntity.Tombstone = DateTime.Now;

                var updateViewlevels =await _viewLevel.GetList(x => x.Contentid == id);
                if (updateViewlevels.Count > 0)
                {
                    updateViewlevels.ForEach(x => x.Tombstone = DateTime.Now);

                }
                _content.Save();
                _viewLevel.Save();
            }

            return Json(new { result = "ok" });
        }



        [HttpPost]
        public IActionResult Update(string content, string id)
        {

            if (!string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(id))
            {

                var updateEntity = _content.FindById(Convert.ToInt32(id));
                updateEntity.ContentName = content;
                _content.Update(updateEntity);
                _content.Save();

            }


            return Content(content);
        }
    }
}