using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASP.Lab.Data;
using ASP.Lab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using ASP.Lab.Models;

namespace ASP.Lab.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDBContent _content;

        public HomeController(AppDBContent context)
        {
            _content = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage(Category model)
        {
            foreach (var file in Request.Form.Files)
            {
                Category img = new Category();
                img.Name = model.Name;
                img.Description = model.Description;

                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                img.Image = ms.ToArray();

                ms.Close();
                ms.Dispose();

                _content.Categories.Add(img);
                _content.SaveChanges();
            }
            ViewBag.Message = "Image(s) stored in database!";
            return View("Index");
        }
        //[HttpPost]
        public ActionResult RetrieveImage()
        {
            Category img = _content.Categories.OrderByDescending
                (i => i.Id).FirstOrDefault();
            string imageBase64Data =
                Convert.ToBase64String(img.Image);
            string imageDataURL =
                string.Format("data:image/*;base64,{0}",
                    imageBase64Data);
            //ViewBag.ImageTitle = img.ImageTitle;
            ViewBag.ImageDataUrl = imageDataURL;
            return View("Index");
        }

        public IActionResult CreateCategory()
        {
            return View();
        }
    }
}
