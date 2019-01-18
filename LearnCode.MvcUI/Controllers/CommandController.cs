using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnCode.Bussiness.Interfaces;
using LearnCode.Entities;
using LearnCode.MvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearnCode.MvcUI.Controllers
{
    public class CommandController : Controller
    {
        private readonly ICommandIndex _command;
        private readonly ILesson _lesson;

        public CommandController(ICommandIndex command ,ILesson lesson)
        {
            _command = command;
            _lesson = lesson;
        }

        public  IActionResult Index()
        {
            
            ListViewModel model = new ListViewModel();
            using (LessonDbContext context = new LessonDbContext())
            {
               
                model.CommandIndices =( from c in context.CommandIndices
                                       join l in context.Lessons  
                                       on c.Lessonid equals l.id
                                       select new CommandIndexView {

                                            id=c.id,
                                             Command=c.Command,
                                              Description=c.Description,
                                              LessonName=l.LessonName

                                       }).ToList();

            }
            
            model.Lessons = _lesson.Table().ToList(); 
            return View(model);
        }

        [HttpPost]
        public IActionResult add(CommandIndex CommandIndex)
        {
            if (ModelState.IsValid)
            {

                _command.Add(CommandIndex);
                _command.Save();
            }


            TempData["info"] = "Added your command => "+ CommandIndex.Command;
            return RedirectToAction("Index");

        }

     
        public IActionResult delete(int id=0)
        {
            if (id>0)
            {
                var deleteEnttiy = _command.FindById(id);
                _command.Remove(deleteEnttiy);
                _command.Save();
            }
            
            TempData["info"] = "Deleted your command"; 
            return RedirectToAction("Index");

        }
    }
}