//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using WebApplication1.Models;
//using WebApplication1.Contexts;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using System.IO;
//using Microsoft.EntityFrameworkCore;

//namespace WebApplication1.Controllers
//{
//    public class FilesUploaderController : Controller
//    {
//        FileContext _context;
//        IHostingEnvironment _environment;

//        public FilesUploaderController(FileContext context, IHostingEnvironment environment)
//        {
//            _context = context;
//            _environment = environment;
//        }
//        public IActionResult Index()
//        {
//            //return View(_context.Files.ToList());
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> AddFile(IFormFileCollection uploads)
//        {
//            foreach (var uploadedFile in uploads)
//            {
//                string path = "/files/" + uploadedFile.FileName;
//                using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
//                {
//                    await uploadedFile.CopyToAsync(fileStream);
//                }
//                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
//                _context.FileModels.Add(file);
//            }
//            _context.SaveChanges();

//            return RedirectToAction("Index");
//        }

//        public IActionResult ShowFilesView()
//        {
//            List<FileModel> files = _context.FileModels.ToList();
//            //string rootPath = _environment.WebRootPath;
//            //files.RemoveAll(DeleletedFile);            
//            for (int i = files.Count - 1; i >= 0; i--)
//            {
//                if (!System.IO.File.Exists(Path.Combine(_environment.WebRootPath, files[i].Path.Remove(0, 1))))
//                    files.RemoveAt(i);
//            }
            
//            ViewBag.Count = files.Count;
//            //ViewBag.Path = _environment.WebRootPath;
//            //ViewBag.Files = files;
//            return View(files);
//        }
//        private bool DeleletedFile(FileModel file)
//        {
//            return !System.IO.File.Exists(Path.Combine(_environment.WebRootPath, file.Path));
//        }

//        [HttpPost]
//        public IActionResult ShowFiles()
//        {
//            return RedirectToAction("ShowFilesView");
//        }
//    }
//}
