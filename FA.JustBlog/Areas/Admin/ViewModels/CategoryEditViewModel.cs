using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Areas.Admin.ViewModels
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Category")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Url Slug")]
        public string UrlSlug { get; set; }


        [StringLength(1000, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}