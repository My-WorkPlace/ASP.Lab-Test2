using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Lab.Data;
using ASP.Lab.Data.Interfaces;
using ASP.Lab.Models;

namespace ASP.Lab.Repository
{
    public class CategoryRepository : ISitesCategory
    {
        private AppDBContent _appDbContent;

        public CategoryRepository(AppDBContent appDbContent)
        {
            _appDbContent = appDbContent;
        }

        public IEnumerable<Category> AllCategories => _appDbContent.Categories;

    }
}
