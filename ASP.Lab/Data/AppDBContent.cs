using ASP.Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.Lab.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
