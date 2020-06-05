using System.Collections.Generic;
using ASP.Lab.Models;

namespace ASP.Lab.Data.Interfaces
{
    interface ISitesCategory
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
