
using AutoMapper;
using FA.JustBlog.Areas.Admin.ViewModels;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    public class TagsManagementController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICatgoryService _categoryService;
        private readonly ITagService _tagService;

        public TagsManagementController(IPostService postService, ICatgoryService categoryService, ITagService tagService)
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
            ViewData["PosteCountSortParm"] = sortOrder == "PosteCount" ? "post_count_desc" : "PosteCount";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Tag, bool>> filter = null;
            Func<IQueryable<Tag>, IOrderedQueryable<Tag>> orderBy = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Name.Contains(searchString);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = b => b.OrderByDescending(s => s.Name);
                    break;
                case "PosteCount":
                    orderBy = b => b.OrderBy(s => s.Posts.Count());
                    break;
                case "post_count_desc":
                    orderBy = b => b.OrderByDescending(s => s.Posts.Count());
                    break;
                default:
                    orderBy = b => b.OrderBy(s => s.Name);
                    break;
            }

            var response = await _tagService.GetAllAsyncPagination(filter: filter, orderBy: orderBy, page: page ?? 1, pageSize: pageSize ?? 10);
            if (response.Status != 200)
            {
                return HttpNotFound();
            }
            else
            {
                return View(response.Data);
            }
        }

        public ActionResult CreateTag()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateTag(TagEditViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                Tag tag = Mapper.Map<Tag>(tagViewModel);
                var response = _tagService.Add(tag);

                if (response.Status == 200)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(tagViewModel);
                }
            }

            return View(tagViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrators")]
        public string DeleteTag(int Id)
        {
            var response = _tagService.Delete(Id);

            return response.MessageText;
        }

        public ActionResult EditTag(int Id)
        {
            var response = _tagService.Find(Id);
            if (response.Status == 200)
            {
                var data = Mapper.Map<TagEditViewModel>(response.Data);
                return View(data);
            }

            return HttpNotFound(response.MessageText);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditTag(TagEditViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<Tag>(tagViewModel);
                _tagService.DetachEntity(category);
                var response = _tagService.Update(category);
                if (response.Status == 200)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(tagViewModel);
                }
            }
            return View(tagViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult QuickAddTag(TagEditViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                Tag tag = Mapper.Map<Tag>(tagViewModel);
                var response = _tagService.Add(tag);

                List<SelectListItem> tagList = new List<SelectListItem>();
                if (response.Status == 200)
                {
                    SelectListItem newtag = new SelectListItem()
                    {
                        Value = tag.Id.ToString(),
                        Text = tag.Name
                    };
                    tagList.Add(newtag);
                    return Json(tagList, JsonRequestBehavior.AllowGet);
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