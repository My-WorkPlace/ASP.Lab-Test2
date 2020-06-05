using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Lab.Data.Interfaces;
using ASP.Lab.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Lab.Controllers
{
    public class SiteController : Controller
    {
        private readonly IallSites _allSites;
        private readonly ISitesCategory _sitesCategory;




        public SiteController(IallSites allSites, ISitesCategory sitesCategory)
        {
            _allSites = allSites;
            _sitesCategory = sitesCategory;

        }

        public ViewResult AllSites()
        {
            var obj = new SiteListViewModel();
            obj.AllSites = _allSites.Sites;
            obj.CurrCategory = "Sites table";
            return View(obj);
        }


    }
}