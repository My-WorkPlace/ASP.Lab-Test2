using System.ComponentModel.DataAnnotations;

namespace ASP.Lab.Models
{
    public class Site
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter url")]
        public string UrlMainPage { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }
        //public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
