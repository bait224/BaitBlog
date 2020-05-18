using AutoMapper;
using FA.JustBlog.Areas.Admin.ViewModels;
using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers 
{
    [Authorize(Roles = "Contributor")]
    public class CategoryManagementController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICatgoryService _categoryService;
        private readonly ITagService _tagService;

        public CategoryManagementController(IPostService postService, ICatgoryService categoryService, ITagService tagService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });
            foreach (var item in items)
            {
                if (item.Value == pageSize.ToString()) item.Selected = true;
            }

            ViewBag.size = items;
            ViewData["CurrentSize"] = pageSize ?? 10;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PosteCountSortParm"] = sortOrder == "PostCount" ? "post_count_desc" : "PostCount";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            

            Expression<Func<Category, bool>> filter = null;
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Name.Contains(searchString);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = b => b.OrderByDescending(s => s.Name);
                    break;
                case "PostCount":
                    orderBy = b => b.OrderBy(s => s.Posts.Count());
                    break;
                case "post_count_desc":
                    orderBy = b => b.OrderByDescending(s => s.Posts.Count());
                    break;
                default:
                    orderBy = b => b.OrderBy(s => s.Name);
                    break;
            }

            var response = await _categoryService.GetAllAsyncPagination(filter: filter, orderBy: orderBy, page: page ?? 1, pageSize: pageSize ?? 10);
            if (response.Status != 200)
            {
                return HttpNotFound();
            }
            else
            {
                return View(response.Data);
            }
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateCategory(CategoryEditViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = Mapper.Map<Category>(categoryViewModel);
                var response = _categoryService.Add(category);

                if(response.Status == 200)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(categoryViewModel);
                }
            }
            else
            {
                return View(categoryViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrators")]
        public string DeleteCategory(int Id)
        {
            var response = _categoryService.Delete(Id);

            return response.MessageText;
        }

        public ActionResult EditCategory(int Id)
        {
            var response = _categoryService.Find(Id);
            if(response.Status == 200)
            {
                var data = Mapper.Map<CategoryEditViewModel>(response.Data);
                return View(data);
            }

            return HttpNotFound(response.MessageText);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditCategory(CategoryEditViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<Category>(categoryViewModel);
                _categoryService.DetachEntity(category);
                var response = _categoryService.Update(category);
                if (response.Status == 200)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(categoryViewModel);
                }
            }
            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult QuickAddCategory(CategoryEditViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = Mapper.Map<Category>(categoryViewModel);
                var response = _categoryService.Add(category);

                List<SelectListItem> cateList = new List<SelectListItem>();
                if (response.Status == 200)
                {
                    SelectListItem newCategory = new SelectListItem()
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name
                    };
                    cateList.Add(newCategory);
                    return Json(cateList, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}