using System.Collections.Generic;
using ASP.Lab.Models;

namespace ASP.Lab.Data.Interfaces
{
    public interface IallSites
    {
        IEnumerable<Site> Sites { get; }
        Site GetSiteById(int id);
    }
}
