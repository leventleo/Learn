using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnCode.Bussiness.Interfaces;
using LearnCode.Entities;
using LearnCode.MvcUI.Models;
using LLearnCode.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.IO;


namespace LearnCode.MvcUI.Controllers
{
    public class ViewLevelController : Controller
    {

        private readonly ILesson _lesson;
        private readonly IContent _content;
        private readonly ISubject _subject;
        private readonly IViewLevel _viewLevel;

        public ViewLevelController(ILesson lesson, IContent content, ISubject subject, IViewLevel viewLevel)
        {
            _lesson = lesson;
            _content = content;
            _subject = subject;
            _viewLevel = viewLevel;
        }



        public IActionResult Add()
        {


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(ViewLevelwithImage ViewLevelwithImage)
        {
            int Subjectid = 0;
            if (ModelState.IsValid)
            {
                ViewLevel model = new ViewLevel
                {
                    Contentid = ViewLevelwithImage.Contentid,
                    LevelContent = ViewLevelwithImage.LevelContent,
                    Levelid = ViewLevelwithImage.Levelid

                };
                if (ViewLevelwithImage.LevelidName != null)
                {
                    model.LevelidName = ViewLevelwithImage.LevelidName;
                }

                if (string.IsNullOrEmpty(ViewLevelwithImage.LevelContent))
                {
                    model.LevelContent = "Empty";
                }

                if (ViewLevelwithImage.LevelImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await ViewLevelwithImage.LevelImage.CopyToAsync(memoryStream);
                        model.LevelImage = memoryStream.ToArray();
                    }

                }

                _viewLevel.Add(model);
                _viewLevel.Save();
                Subjectid = _content.FindById((int)model.Contentid).Subjectid;

            }

            return RedirectToAction("contentlist", "content", new { id = Subjectid });
        }

        [HttpPost]
        public async Task<IActionResult> GetContentLevel(int id = 0)
        {
            ListViewModel model = new ListViewModel();

            if (id > 0)
            {

                model.viewLevels = await _viewLevel.GetList(v => v.Contentid == id);
                model.ContentName = _content.FindById(id).ContentName;
                model.viewLevels.OrderBy(x => x.Levelid);
            }


            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> ContentlevelsAsync(int id = 0)
        {
            var list = new List<ViewLevel>();
            if (id > 0)
            {
                list =await _viewLevel.GetList(x => x.Contentid == id);


            }

            return Json(list);
        }

        [HttpPost]
        public IActionResult Getlevel(int id = 0)
        {
            var model = new ViewLevel();

            if (id > 0)
            {

                model = _viewLevel.FindById(id);

            }


            return Json(model);

        }

        [HttpPost]
        public async Task<IActionResult> updateAsync(ViewLevelwithImage ViewLevelwithImage)
        {
            int Subjectid = 0;

            if (ModelState.IsValid)
            {
                ViewLevel model = _viewLevel.FindById(ViewLevelwithImage.id);

                
                model.LevelContent = ViewLevelwithImage.LevelContent;
                model.Levelid = ViewLevelwithImage.Levelid;
                model.LevelidName = ViewLevelwithImage.LevelidName;
                

                if (string.IsNullOrEmpty(ViewLevelwithImage.LevelContent))
                {
                    model.LevelContent = "Empty";
                }

                if (ViewLevelwithImage.LevelImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await ViewLevelwithImage.LevelImage.CopyToAsync(memoryStream);
                        model.LevelImage = memoryStream.ToArray();
                    }

                }

                _viewLevel.Update(model);
                _viewLevel.Save();

                Subjectid = _content.FindById((int)model.Contentid).Subjectid;

            }


            return RedirectToActionPermanent ("contentlist", "content", new { id = Subjectid });

        }

        [HttpPost]
        public IActionResult delete(int id=0)
        {

            if (id>0)
            {
                var deleteEntity = _viewLevel.FindById(id);
                _viewLevel.Remove(deleteEntity);
                _viewLevel.Save();

            }

            return Json("Deleted");
        }

       
    }



}

