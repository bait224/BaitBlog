using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Core.Models
{
    public class Tag : AuditableEntity<int>
    {
        public Tag()
        {
            Posts = new HashSet<Post>();
            CountPost();
        }

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Url Slug")]
        public string UrlSlug { get; set; }


        [StringLength(1000, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [NotMapped]
        public int Count { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public void CountPost()
        {
            Count = Posts.Count;
        }
    }
}
