using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASP.Lab.Data;
using ASP.Lab.Data.Interfaces;
using ASP.Lab.Models;
using ASP.Lab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.Lab.Controllers
{
    public class EditorController : Controller
    {
        private readonly ISitesCategory _sitesCategory;
        private readonly IallSites _allSites;
        private readonly AppDBContent _content;
        private CategoryListViewModel obj = new CategoryListViewModel();

        public EditorController(ISitesCategory sitesRepository, IallSites allSites, AppDBContent content)
        {
            _sitesCategory = sitesRepository;
            _allSites = allSites;
            _content = content;
            obj.AllCategory = _sitesCategory.AllCategories;
        }
        public IActionResult AddSite()
        {
            ViewBag.Categories = new SelectList(_sitesCategory.AllCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveSite(Site site)
        {
            //TODO if site is null return or validation
            var category = _sitesCategory.AllCategories.FirstOrDefault(c => c.Id == site.CategoryId);
            var path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            _content.Sites.Add(site);
            await _content.SaveChangesAsync();

            //return Redirect($"{path}/Home/Index");
            //return RedirectToAction("Index", "Home");
            //return View("~/Views/Home/Index.cshtml");
            return RedirectToAction("GetAllCategories", "Home");
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(Category model)
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
                await _content.SaveChangesAsync();
            }
            ViewBag.Message = "Image(s) stored in database!";
            return RedirectToAction("GetAllCategories", "Home");
        }
    }
}