using FA.JustBlog.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.Controllers
{
    public class CategoryController : Controller
    {
        private ICatgoryService _categoryService;

        public CategoryController(ICatgoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [ChildActionOnly]
        public ActionResult GetMenuCategory()
        {
            var response = _categoryService.GetAll();

            if (response.Status == 200)
            {
                return PartialView("_MenuCategory", response.Data);
            }
            else
            {
                return PartialView("_MenuCategory", null);
            }
        }
    }
}