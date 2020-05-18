using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FA.JustBlog.Areas.Admin.ViewModels
{
    public class PostEditViewModel
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

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(50000, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Content")]
        public string PostContent { get; set; }

        [Display(Name = "Publish?")]
        public bool Published { get; set; }

        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Url Slug")]
        public string UrlSlug { get; set; }

        [Display(Name = "Posted On")]
        public DateTime PostedOn { get; set; }

        public bool Modidied { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List<SelectListItem> Categories { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public List<SelectListItem> ListTags { get; set; }

       
        public List<Tag> Tags{ get; set; }

        [Display(Name = "Tag(s)")]
        public List<int> TagsIdSeleted { get; set; }

        public PostEditViewModel()
        {
            Categories = new List<SelectListItem>();
            ListTags = new List<SelectListItem>();
        }
    }
}