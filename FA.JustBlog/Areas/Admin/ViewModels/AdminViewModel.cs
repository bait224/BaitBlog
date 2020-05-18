using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FA.JustBlog.Areas.Admin.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your full name.")]
        [StringLength(30, ErrorMessage = "The full name must be between 3 and 30 characters.", MinimumLength = 3)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your city.")]
        [StringLength(30, ErrorMessage = "The city name must be between 3 and 30 characters.", MinimumLength = 3)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "The address must be between 5 and 100 characters.", MinimumLength = 5)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your birth day.")]
        [Display(Name = "Birth Day")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}