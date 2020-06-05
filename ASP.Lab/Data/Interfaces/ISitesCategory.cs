using System.Collections.Generic;
using ASP.Lab.Models;

namespace ASP.Lab.Data.Interfaces
{
    public interface ISitesCategory
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
