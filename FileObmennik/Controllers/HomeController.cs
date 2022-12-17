using FileObmennik.Data;
using FileObmennik.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using File = FileObmennik.Models.File;

namespace FileObmennik.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment env;
        private ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment env)
        {
            _logger = logger;
            this.db = db;
            this.env = env;
        }

        public IActionResult Index()
        {
            IEnumerable<File> files=db.File;
            return View(files);
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var file = db.File.Find(id);

            if (file == null)
            {
                return NotFound();
            }
            string wwRoot = env.WebRootPath;
            string upload = wwRoot + PathManager.ImageProductPass;
            var oldFile = upload + file.FileName;
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }
            db.File.Remove(file);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult AddPost(int? id)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count>0)
            {
                string wwRoot = env.WebRootPath;
                string upload = wwRoot + PathManager.ImageProductPass;
                string imageName = files[0].FileName;
                string extansion = Path.GetExtension(files[0].FileName);
                string path = upload + imageName;
                //copy file to server
                var fileStream = new FileStream(path, FileMode.Create);

                files[0].CopyTo(fileStream);
                Models.File file = new Models.File()
                {
                    FileName = imageName,
                    FileType = extansion,
                   

                };
                db.File.Add(file);
                fileStream.Close();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Add",id);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}