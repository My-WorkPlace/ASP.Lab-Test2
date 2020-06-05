using System.Collections.Generic;
using ASP.Lab.Models;

namespace ASP.Lab.ViewModels
{
    public class SiteListViewModel
    {
        public string CurrCategory { get; set; }
        public IEnumerable<Site> AllSites { get; set; }
    }
}
