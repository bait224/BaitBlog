using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Core.Models
{
    public class Category : AuditableEntity<int>
    {
        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(255,ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Category")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Url Slug")]
        public string UrlSlug { get; set; }


        [StringLength(1000, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual IList<Post> Posts { get; set; }
    }
}
