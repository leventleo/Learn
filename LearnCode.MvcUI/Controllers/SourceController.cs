using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using LearnCode.Bussiness.Interfaces;
using LearnCode.Entities;
using LearnCode.MvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnCode.MvcUI.Controllers
{
    public class SourceController : Controller
    {
        private readonly ISourceFile _sourceFile;
        private readonly IFileStore _fileStore;

        public SourceController(ISourceFile sourceFile, IFileStore fileStore)
        {
            _sourceFile = sourceFile;
            _fileStore = fileStore;
        }

        public IActionResult SourceFileLoad()
        {
            ListViewModel model = new ListViewModel();
            model.SourceFiles =  _sourceFile.Table().ToList();
            model.SourceFiles.ForEach(x =>
            {
                x.Length = x.Length / 1024;

            });
            return View(model);
        }

        public IActionResult Download(string FileName, int id = 0)
        {

            if (id > 0)
            {
                var file = _fileStore.FindById(id);

                return File(file.File, "application/zip", FileName, true);
            }


            return Json("Error");
        }


        [HttpPost]
        public async Task<IActionResult> SourceFileLoad(SourceFileStub SourceFileStub)
        {
 

                if (ModelState.IsValid)
                {
                    try
                    {
                        SourceFile model = new SourceFile();
                        FileStore fmodel = new FileStore();

                        model.SourceType = SourceFileStub.SourceType;

                        using (var memoryStream = new MemoryStream())
                        {
                            await SourceFileStub.File.CopyToAsync(memoryStream);
                            fmodel.File = memoryStream.ToArray();
                            model.Length = SourceFileStub.File.Length;
                            model.FileName = SourceFileStub.File.FileName;
                        }

                        _sourceFile.Add(model);
                        _sourceFile.Save();
                        fmodel.id =model.id;
                        _fileStore.Add(fmodel);
                        _fileStore.Save();

                    TempData["Error"] = "This file is Added FileSores";

                }
                    catch (Exception ex)
                    {
                        TempData["Error"] = ex.Message.ToString();

                    }


                }



            return RedirectToAction("SourceFileLoad");
        }

        public IActionResult deletefile(int id=0)
        {

            try
            {
                if (id > 0)
                {
                    var deleteFileEntity = _fileStore.FindById(id);
                    _fileStore.Remove(deleteFileEntity);
                    _fileStore.Save();
                    var deleteEntity = _sourceFile.FindById(id);
                    _sourceFile.Remove(deleteEntity);
                    _sourceFile.Save();

                    TempData["Error"] = "File is Deleted";
                }
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("SourceFileLoad");

        }



    }



}