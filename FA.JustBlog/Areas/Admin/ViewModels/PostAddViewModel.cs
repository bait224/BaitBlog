using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Areas.Admin.ViewModels
{
    public class PostAddViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(500, ErrorMessage = "The Title only a maximum of 500 characters")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(500, ErrorMessage = "The Short Description only a maximum of 500 characters")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Poster")]
        public string PosterImg { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(50000, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Content")]
        public string PostContent { get; set; }

        [Display(Name = "Publish?")]
        public bool Published { get; set; }

        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Url Slug")]
        public string UrlSlug { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public NewCategoryModel Category { get; set; }

        public virtual List<Tag> Tags { get; set; }

        [Display(Name = "Tag(s)")]
        public List<int> TagsId { get; set; }

    }

    public class NewCategoryModel
    {
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
    }
}