using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FA.JustBlog.Areas.Admin.ViewModels
{
    public class ListCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; }

        [Display(Name = "Post Number")]
        public int PostCount { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}