using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ASP.Lab.Data;
using ASP.Lab.Data.Interfaces;
using ASP.Lab.Models;
using ASP.Lab.ViewModels;
using Microsoft.AspNetCore.Mvc;

//using ASP.Lab.Models;

namespace ASP.Lab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISitesCategory _sitesCategory;
        private readonly IallSites _allSites;

        private readonly AppDBContent _content;

        public HomeController(ISitesCategory sitesRepository, IallSites allSites, AppDBContent context)
        {
            _content = context;
            _sitesCategory = sitesRepository;
            _allSites = allSites;
        }

        public IActionResult Index()
        {
            return RedirectToAction("GetAllCategories", "Home");
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

        public ActionResult GetAllCategories()
        {
            var obj = new CategoryListViewModel();
            obj.AllCategory = _sitesCategory.AllCategories;
            return View(obj);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [Route("Home/List")]
        [Route("Home/List/{category}")]
        public IActionResult List(string category)
        {
            var _category = category;
            IEnumerable<Site> sites = null;
            var currCategory = string.Empty;
            if (string.IsNullOrEmpty(category))
            {
                sites = _allSites.Sites.OrderBy(i => i.Id);
            }
            else
            {
                if (string.Equals("News", category, StringComparison.OrdinalIgnoreCase))
                {
                    sites = _allSites.Sites.Where(c => c.Category.Name.Equals("News")).OrderBy(i => i.Id);
                    currCategory = "News";
                }
                else if (string.Equals("Sport", category, StringComparison.OrdinalIgnoreCase))
                {
                    sites = _allSites.Sites.Where(c => c.Category.Name.Equals("Sport")).OrderBy(i => i.Id);
                    currCategory = "Sport";
                }
                else if (string.Equals("Other", category, StringComparison.OrdinalIgnoreCase))
                {
                    sites = _allSites.Sites.Where(c => c.Category.Name.Equals("Other")).OrderBy(i => i.Id);
                    currCategory = "Other";
                }
            }

            var obj = new SiteListViewModel();
            //TODO if zero sites return smt
            obj.AllSites = sites.ToList();
            obj.CurrCategory = "Sites table";

            ViewBag.Title = "Page with cars";
            return View(obj);
        }
        [Route("Home/ListCustom")]
        [Route("Home/ListCustom/{category}")]
        public IActionResult ListCustom(string category)
        {
            IEnumerable<Site> sites = null;
            var currCategory = string.Empty;
            if (!string.IsNullOrEmpty(category))
            {
                sites = _allSites.Sites.Where(c => c.Category.Name.Equals(category)).OrderBy(i => i.Id);
            }
            var obj = new SiteListViewModel();
            //TODO if zero sites return smt
            obj.AllSites = sites.ToList();
            obj.CurrCategory = "Sites table";

            ViewBag.Title = "Page with cars";
            return View(obj);
        }

    }
}
