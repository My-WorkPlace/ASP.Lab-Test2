using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Lab.Data;
using ASP.Lab.Data.Interfaces;
using ASP.Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.Lab.Repository
{
    public class SiteRepository : IallSites
    {
        private AppDBContent _appDbContent;

        public SiteRepository(AppDBContent appDbContent)
        {
            _appDbContent = appDbContent;
        }

        public IEnumerable<Site> Sites => _appDbContent.Sites.Include(c => c.Category);
        public Site GetSiteById(int id) => _appDbContent.Sites.FirstOrDefault(p => p.Id == id);
    }
}
