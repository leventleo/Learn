using LearnCode.Bussiness.Interfaces;
using LearnCode.Entities;
using LearnCode.MvcUI.Models;
using LLearnCode.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCode.MvcUI.Controllers
{

    public class LessonController : Controller
    {
        private readonly ILesson _lesson;
        private readonly IContent _content;
        private readonly ISubject _subject;
        private readonly IViewLevel _viewLevel;
        private readonly ICommandIndex _commandIndex;
        private readonly IRedisClient _distributedCache;

        public LessonController(ILesson lesson, IContent content, ISubject subject, IViewLevel viewLevel, ICommandIndex commandIndex, IRedisClient distributedCache)
        {
            _lesson = lesson;
            _content = content;
            _subject = subject;
            _viewLevel = viewLevel;
            _commandIndex = commandIndex;
            _distributedCache = distributedCache;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Lesson lesson)
        {

            _lesson.Add(lesson);
            _lesson.Save();
            return Redirect("/lesson/add");
        }

        public async Task<IActionResult> Add()
        {
            AddViewModel model = new AddViewModel
            {
                Lessons = await _lesson.GetList(),
                Lesson = new Lesson()

            };

            model.Lessons.OrderBy(x => x.LessonName).ToList();
            return View(model);
        }



        public async Task<IActionResult> List()
        {
            ListViewModel model = new ListViewModel();

            model.Lessons = await _lesson.GetList();
            model.Subjects = await _subject.GetList();
            model.Lessons.OrderBy(x => x.LessonName).ToList();





            return View(model);
        }


        public async Task<IActionResult> Remove()
        {




            ListViewModel model = new ListViewModel
            {
                Lessons = await _lesson.GetList(),
            };

            model.Lessons.OrderBy(x => x.LessonName).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Remove(int id = 0)
        {


            if (id > 0)
            {
                var removeEntity = _lesson.FindById(Convert.ToInt32(id));
                if (removeEntity != null)
                {
                    _lesson.Remove(removeEntity);
                    _lesson.Save();
                }

            }
            ListViewModel model = new ListViewModel
            {
                Lessons = await _lesson.GetList()

            };

            model.Lessons.OrderBy(x => x.LessonName).ToList();
            return Content(id.ToString());
        }


        public async Task<IActionResult> Subjectlist(int id = 0)
        {
            ListViewModel model = new ListViewModel();
            if (id > 0)
            {

                model.Subjects = await _subject.GetList(x => x.Lessonid == id);
                model.LessonName = _lesson.FindById(id).LessonName;
                model.Contents = await _content.GetList();
                model.selectedLesson = id;
                return View(model);
              
            }
           

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ContentList(int id)
        {
            ListViewModel model = new ListViewModel
            {
                Contents = await _content.GetList(x => x.Subjectid == id),

            };



            return View(model);
        }
        List<CompositViewModel> allTableJoin = new List<CompositViewModel>();
        [HttpPost]
        public IActionResult DeepSearch(string search)
        {
            var _search = search.ToLower();


            if (allTableJoin.Count < 1 || allTableJoin == null)
            {
                using (LessonDbContext context = new LessonDbContext())
                {
                    allTableJoin = (from l in context.Lessons
                                    join s in context.Subjects
                                    on l.id equals s.Lessonid
                                    join c in context.Contents
                                    on s.id equals c.Subjectid
                                    join w in context.ViewLevels
                                    on c.id equals w.Contentid
                                    where c.Tombstone == null
                                    select new CompositViewModel
                                    {
                                        LessonName = l.LessonName.ToLower(),
                                        SubjectName = s.SubjectName.ToLower(),
                                        ContentName = c.ContentName.ToLower(),
                                        ViewLevelName = w.LevelContent.ToLower()


                                    }).ToList();

                }
            }

            List<CompositViewModel> searchResult = new List<CompositViewModel>();

            if (allTableJoin != null)
            {
                allTableJoin.ForEach(c =>
                {

                    if (c.LessonName.Contains(_search) || c.SubjectName.Contains(_search)
                    || c.ContentName.Contains(_search) || c.ViewLevelName.Contains(_search))
                    {
                        searchResult.Add(c);
                    }

                });
            }


            return View("DeepSearch", searchResult);


        }


    }
}