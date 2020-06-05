using System.Collections.Generic;
using System.IO;
using System.Linq;
using ASP.Lab.Models;
using Microsoft.AspNetCore.Hosting;

namespace ASP.Lab.Data
{
    public class DBObjects
    {
        private  readonly IHostingEnvironment _hostingEnvironment;

        public DBObjects(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public void Initial(AppDBContent context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }
            if (!context.Sites.Any())
            {
                context.AddRange
                    (
                    new Site
                    {
                        Name = "Youtube",
                        UrlMainPage = "https://www.youtube.com/",
                        Category = Categories["Other"]
                    },
                    new Site
                    {
                        Name = "Google",
                        UrlMainPage = "https://www.google.com/",
                        Category = Categories["Other"]
                    },
                    new Site
                    {
                        Name = "Unian",
                        UrlMainPage = "https://www.unian.ua/",
                        Category = Categories["News"]
                    },
                    new Site
                    {
                        Name = "Правда",
                        UrlMainPage = "https://www.pravda.com.ua/news/",
                        Category = Categories["News"]
                    },
                    new Site
                    {
                        Name = "спорт",
                        UrlMainPage = "https://sport.ua/uk",
                        Category = Categories["Sport"]
                    },
                    new Site
                    {
                        Name = "УкрНет",
                        UrlMainPage = "https://www.ukr.net/news/sport.html",
                        Category = Categories["Sport"]
                    }
                    );
            }

            context.SaveChanges();

        }

        private static Dictionary<string, Category> _category;
        public  Dictionary<string, Category> Categories
        {

            get
            {
                if (_category == null)
                {
                    var projectRootPath = _hostingEnvironment.WebRootPath;

                    var news = projectRootPath+@"\images\news.jpg";
                    var sport = projectRootPath+@"\images\sport.jpg";
                    var other = projectRootPath+@"\images\\Other.jpg";
                    var list = new Category[]
                    {
                        new Category {Name = "News", Description = "Description news", Image = File.ReadAllBytes(news) },
                        new Category {Name = "Sport", Description = "Description sport", Image = File.ReadAllBytes(sport) },
                        new Category {Name = "Other", Description = "Description other", Image = File.ReadAllBytes(other)}
                    };
                    _category = new Dictionary<string, Category>();
                    foreach (var element in list)
                    {
                        _category.Add(element.Name, element);
                    }
                }

                return _category;
            }
        }
        
    }
}
