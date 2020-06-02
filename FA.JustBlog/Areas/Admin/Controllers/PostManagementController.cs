using AutoMapper;
using FA.JustBlog.Areas.Admin.ViewModels;
using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FA.JustBlog.Common;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Contributor")]
    public class PostManagementController : Controller
    {
        private const string _ImagesPath = "~/Assets/images";
        private readonly IPostService _postService;
        private readonly ICatgoryService _categoryService;
        private readonly ITagService _tagService;

        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PostManagementController));

        public PostManagementController(IPostService postService, ICatgoryService categoryService, ITagService tagService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task<ActionResult> ListPost(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "25", Value = "25" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" },
                new SelectListItem { Text = "200", Value = "200" }
            };
            foreach (var item in items)
            {
                if (item.Value == pageSize.ToString()) item.Selected = true;
            }

            ViewBag.size = items;
            ViewData["CurrentSize"] = pageSize ?? 5;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PostedOnSortParm"] = sortOrder == "PostedOn" ? "posted_on_desc" : "PostedOn";
            ViewData["ModifiedOnSortParm"] = sortOrder == "ModifiedOn" ? "modified_on_desc" : "ModifiedOn";
            ViewData["ViewsSortParm"] = sortOrder == "Views" ? "view_desc" : "Views";
            ViewData["RateSortParm"] = sortOrder == "Rate" ? "rate_desc" : "Rate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Post, bool>> filter = null;
            Expression<Func<Post, bool>> filterByUser = null;
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null;
            string userName = User.Identity.Name;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Title.Contains(searchString);
            }

            if (User.IsInRole("Contributor") && !User.IsInRole("Administrators"))
            {
                filterByUser = b => (b.CreatedBy == userName);
            }
            switch (sortOrder)
            {
                case "title_desc":
                    orderBy = b => b.OrderByDescending(s => s.Title);
                    break;
                case "PostedOn":
                    orderBy = b => b.OrderBy(s => s.PostedOn);
                    break;
                case "posted_on_desc":
                    orderBy = b => b.OrderByDescending(s => s.PostedOn);
                    break;
                case "ModifiedOn":
                    orderBy = b => b.OrderBy(s => s.ModifiedDate);
                    break;
                case "modified_on_desc":
                    orderBy = b => b.OrderByDescending(s => s.ModifiedDate);
                    break;
                case "Views":
                    orderBy = b => b.OrderBy(s => s.ViewCount);
                    break;
                case "views_desc":
                    orderBy = b => b.OrderByDescending(s => s.ViewCount);
                    break;
                case "Rate":
                    orderBy = b => b.OrderBy(s => s.TotalRate / s.RateCount);
                    break;
                case "rate_desc":
                    orderBy = b => b.OrderByDescending(s => s.TotalRate / s.RateCount);
                    break;
                default:
                    orderBy = b => b.OrderBy(s => s.Title);
                    break;
            }

            var response = await _postService.GetAllAsyncPagination(filter: filter, filterByUSer: filterByUser, orderBy: orderBy, page: page ?? 1, pageSize: pageSize ?? 5);
            if (response.Status != 200)
            {
                return HttpNotFound();
            }
            else
            {
                ListPostViewModel data = new ListPostViewModel()
                {
                    ListPost = response.Data as PaginationList<Post>,
                    Type = Common.ListPostTypeForManage.AllPost
                };
                return View("ListPost", data);
            }
        }

        public async Task<ActionResult> PublishedListPost(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "25", Value = "25" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" },
                new SelectListItem { Text = "200", Value = "200" }
            };
            foreach (var item in items)
            {
                if (item.Value == pageSize.ToString()) item.Selected = true;
            }

            ViewBag.size = items;
            ViewData["CurrentSize"] = pageSize ?? 5;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PostedOnSortParm"] = sortOrder == "PostedOn" ? "posted_on_desc" : "PostedOn";
            ViewData["ModifiedOnSortParm"] = sortOrder == "ModifiedOn" ? "modified_on_desc" : "ModifiedOn";
            ViewData["ViewsSortParm"] = sortOrder == "Views" ? "view_desc" : "Views";
            ViewData["RateSortParm"] = sortOrder == "Rate" ? "rate_desc" : "Rate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Post, bool>> filter = null;
            Expression<Func<Post, bool>> filterByUser = null;
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null;
            string userName = User.Identity.Name;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Title.Contains(searchString);
            }

            if (User.IsInRole("Contributor") && !User.IsInRole("Administrators"))
            {
                filterByUser = b => b.CreatedBy.Contains(userName);
            }

            switch (sortOrder)
            {
                case "title_desc":
                    orderBy = b => b.OrderByDescending(s => s.Title);
                    break;
                case "PostedOn":
                    orderBy = b => b.OrderBy(s => s.PostedOn);
                    break;
                case "posted_on_desc":
                    orderBy = b => b.OrderByDescending(s => s.PostedOn);
                    break;
                case "ModifiedOn":
                    orderBy = b => b.OrderBy(s => s.ModifiedDate);
                    break;
                case "modified_on_desc":
                    orderBy = b => b.OrderByDescending(s => s.ModifiedDate);
                    break;
                case "Views":
                    orderBy = b => b.OrderBy(s => s.ViewCount);
                    break;
                case "views_desc":
                    orderBy = b => b.OrderByDescending(s => s.ViewCount);
                    break;
                case "Rate":
                    orderBy = b => b.OrderBy(s => s.Rate);
                    break;
                case "rate_desc":
                    orderBy = b => b.OrderByDescending(s => s.Rate);
                    break;
                default:
                    orderBy = b => b.OrderBy(s => s.Title);
                    break;
            }

            var response = await _postService.GetPublishedAsyncPagination(filter: filter, filterByUser: filterByUser, orderBy: orderBy, page: page ?? 1, pageSize: pageSize ?? 5);
            if (response.Status != 200)
            {
                return HttpNotFound();
            }
            else
            {
                ListPostViewModel data = new ListPostViewModel()
                {
                    ListPost = response.Data as PaginationList<Post>,
                    Type = Common.ListPostTypeForManage.PublishedPost
                };
                return View("ListPost", data);
            }
        }

        public async Task<ActionResult> UnPublishedListPost(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "25", Value = "25" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" },
                new SelectListItem { Text = "200", Value = "200" }
            };
            foreach (var item in items)
            {
                if (item.Value == pageSize.ToString()) item.Selected = true;
            }

            ViewBag.size = items;
            ViewData["CurrentSize"] = pageSize ?? 5;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PostedOnSortParm"] = sortOrder == "PostedOn" ? "posted_on_desc" : "PostedOn";
            ViewData["ModifiedOnSortParm"] = sortOrder == "ModifiedOn" ? "modified_on_desc" : "ModifiedOn";
            ViewData["ViewsSortParm"] = sortOrder == "Views" ? "view_desc" : "Views";
            ViewData["RateSortParm"] = sortOrder == "Rate" ? "rate_desc" : "Rate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Post, bool>> filter = null;
            Expression<Func<Post, bool>> filterByUser = null;
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null;
            string userName = User.Identity.Name;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Title.Contains(searchString);
            }

            if (User.IsInRole("Contributor") && !User.IsInRole("Administrators"))
            {
                filterByUser = b => b.CreatedBy.Contains(userName);
            }

            switch (sortOrder)
            {
                case "title_desc":
                    orderBy = b => b.OrderByDescending(s => s.Title);
                    break;
                case "PostedOn":
                    orderBy = b => b.OrderBy(s => s.PostedOn);
                    break;
                case "posted_on_desc":
                    orderBy = b => b.OrderByDescending(s => s.PostedOn);
                    break;
                case "ModifiedOn":
                    orderBy = b => b.OrderBy(s => s.ModifiedDate);
                    break;
                case "modified_on_desc":
                    orderBy = b => b.OrderByDescending(s => s.ModifiedDate);
                    break;
                case "Views":
                    orderBy = b => b.OrderBy(s => s.ViewCount);
                    break;
                case "views_desc":
                    orderBy = b => b.OrderByDescending(s => s.ViewCount);
                    break;
                case "Rate":
                    orderBy = b => b.OrderBy(s => s.Rate);
                    break;
                case "rate_desc":
                    orderBy = b => b.OrderByDescending(s => s.Rate);
                    break;
                default:
                    orderBy = b => b.OrderBy(s => s.Title);
                    break;
            }

            var response = await _postService.GetUnPublishedAsyncPagination(filter: filter, filterByUser: filterByUser, orderBy: orderBy, page: page ?? 1, pageSize: pageSize ?? 5);
            if (response.Status != 200)
            {
                return HttpNotFound();
            }
            else
            {
                ListPostViewModel data = new ListPostViewModel()
                {
                    ListPost = response.Data as PaginationList<Post>,
                    Type = Common.ListPostTypeForManage.UnPublishedPost
                };
                return View("ListPost", data);
            }
        }

        public ActionResult CreatePost()
        {
            List<Category> listCategory = _categoryService.GetAll().Data as List<Category>;
            ViewBag.Categories = new SelectList(listCategory.OrderBy(c => c.Id), "Id", "Name");

            List<Tag> listTag = _tagService.GetAll().Data as List<Tag>;
            PostAddViewModel postEditViewModel = new PostAddViewModel()
            {
                Tags = listTag
            };
            return View(postEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreatePost(PostAddViewModel postViewModel)
        {

            if (ModelState.IsValid)
            {
                var files = Request.Files;
                var image = files["file"];
                if(image != null)
                {
                    string fileName = image.FileName;
                    if (image != null && image.ContentLength > 0)
                    {
                        try
                        {
                            fileName = Path.GetFileName(fileName);
                            string path = Path.Combine(Server.MapPath(_ImagesPath), Path.GetFileName(fileName));
                            image.SaveAs(path);

                            postViewModel.PosterImg = fileName;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                }

                List<Tag> tags = new List<Tag>();
                if (postViewModel.TagsId != null)
                {
                    foreach (var tag in postViewModel.TagsId)
                    {
                        tags.Add(_tagService.Find(tag).Data as Tag);
                    }
                }


                postViewModel.Tags = tags;

                var post = Mapper.Map<Post>(postViewModel);
                var response = _postService.Add(post);
                if (response.Status == 200)
                {
                    logger.Info($"{User.Identity.Name} add a new post");
                    return Json(new
                    {
                        HasErrors = false,
                        Message = response.MessageText
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        HasErrors = false,
                        Message = response.MessageText
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            List<Category> listCategory = _categoryService.GetAll().Data as List<Category>;
            ViewBag.Categories = new SelectList(listCategory.OrderBy(c => c.Id), "Id", "Name");

            List<Tag> listTag = _tagService.GetAll().Data as List<Tag>;
            postViewModel.Tags = listTag;

            var errorMessages = ModelState.ToDictionary(
                                    ms => ms.Key,
                                    ms => ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                ).Where(ms => ms.Value.Count() > 0);

            return Json(new
            {
                HasErrors = true,
                Message = errorMessages
            }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrators")]
        public string DeletePost(int Id)
        {
            var response = _postService.Delete(Id);

            return response.MessageText;
        }

        public ActionResult EditPost(int Id)
        {
            var response = _postService.Find(Id);
            if (response.Status == 200)
            {
                List<Category> listCategory = _categoryService.GetAll().Data as List<Category>;
                List<Tag> listTag = _tagService.GetAll().Data as List<Tag>;

                var editPostViewModel = Mapper.Map<PostEditViewModel>(response.Data);
                editPostViewModel.Categories = GetListOfSelectListItemFromListCategory(listCategory);
                editPostViewModel.ListTags = GetListOfSelectListItemFromListTag(listTag);
                List<int> tagsID = new List<int>();
                foreach (var tag in editPostViewModel.Tags)
                {
                    tagsID.Add(tag.Id);
                }
                editPostViewModel.TagsIdSeleted = tagsID;

                return View(editPostViewModel);
            }

            return HttpNotFound(response.MessageText);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditPost(PostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<Tag> tags = new List<Tag>();
                if (model.TagsIdSeleted != null)
                {
                    foreach (var tag in model.TagsIdSeleted)
                    {
                        tags.Add(_tagService.Find(tag).Data as Tag);
                    }
                }

                model.Tags = tags;

                var originPost = _postService.Find(model.Id).Data as Post;
                originPost.Tags.Clear();
                var responseUpdate1 = _postService.Update(originPost);
                if (responseUpdate1.Status == 200)
                {
                    originPost.Tags = model.Tags;
                    originPost.CategoryId = model.CategoryId;
                    originPost.Title = model.Title;
                    originPost.UrlSlug = model.UrlSlug;
                    originPost.Published = model.Published;
                    originPost.ShortDescription = model.ShortDescription;
                    originPost.PostContent = model.PostContent;
                    originPost.PostedOn = model.PostedOn;
                    originPost.Modified = true;
                    originPost.ModifiedDate = DateTime.Now;

                    var response = _postService.Update(originPost);
                    if (response.Status == 200)
                    {
                        return RedirectToAction("ListPost");
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult DetailPost(long? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            var response = _postService.Find(Id.Value);
            if (response.Status == 200)
            {
                return View(response.Data);
            }
            else
            {
                return HttpNotFound(response.MessageText);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public JsonResult UpdatePublish(long id, bool status)
        {
            var response = _postService.UpdatePublish(id, status);
            if (response.Status == 200)
            {
                return Json(new
                {
                    IsSuccess = true,
                    Message = response.MessageText
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    IsSuccess = false,
                    Message = response.MessageText
                }, JsonRequestBehavior.AllowGet);
            }
        }

        private List<SelectListItem> GetListOfSelectListItemFromListCategory(List<Category> list)
        {
            var result = new List<SelectListItem>();
            foreach (var item in list)
            {
                var selectItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                };
                result.Add(selectItem);
            }

            return result;
        }
        private List<SelectListItem> GetListOfSelectListItemFromListTag(List<Tag> list)
        {
            var result = new List<SelectListItem>();
            foreach (var item in list)
            {
                var selectItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                };
                result.Add(selectItem);
            }

            return result;
        }
    }
}