
using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Service;
using FA.JustBlog.Models;
using FA.JustBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        // GET: Post
        public async Task<ActionResult> Index(string byWhat, string currentByWhat, string currentFilter, string searchString, int? page, int? pageSize)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            if (string.IsNullOrEmpty(byWhat))
            {
                byWhat = currentByWhat;
            }
            ViewData["CurrentByWhat"] = byWhat;

            Expression<Func<Post, bool>> filter = null;
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Title.Contains(searchString) && b.PostContent.Contains(searchString);
            }
            if(orderBy == null)
            {
                orderBy = b => b.OrderByDescending(x => x.ViewCount);
            }
            var response = await _postService.GetPublishedAsyncPagination(filter: filter, orderBy: orderBy, page: page ?? 1, pageSize: pageSize ?? 4);

            if (response.Status == 200)
            {
                ListPostViewModel data = new ListPostViewModel()
                {
                    ListPost = response.Data as PaginationList<Post>,
                    Type = Common.ListPostTypeToIndex.AllPost
                };

                return View(data);
            }
            else
            {
                return HttpNotFound(response.MessageText);
            }
        }

        public ActionResult LatestPostsinside()
        {
            var response = _postService.GetLatestPost(5);

            if (response.Status == 200)
            {
                var list = response.Data as List<Post>;
                ListPostModel result = new ListPostModel()
                {
                    ListPost = list.Where(p => p.Published).ToList(),
                    Type = TypeListPostEnum.LatestPost
                };
                return PartialView("_ListPosts", result);
            }
            else
            {
                return HttpNotFound(response.MessageText);
            }
        }

        public ActionResult MostViewedPosts()
        {
            var response = _postService.GetMostViewedPost(5);

            if (response.Status == 200)
            {
                var list = response.Data as List<Post>;
                ListPostModel result = new ListPostModel()
                {
                    ListPost = list.Where(p => p.Published).ToList(),
                    Type = TypeListPostEnum.MostViewPost
                };
                return PartialView("_ListPosts", result);
            }
            else
            {
                return HttpNotFound(response.MessageText);
            }
        }

        public ActionResult DetailPost(int year, int month, string title)
        {
            var response = _postService.GetDetail(year, month, title);

            if (response.Status == 200)
            {
                Post post = response.Data as Post;
                ViewBag.Comments = post.Comments;
                return View(post);
            }
            else
            {
                return HttpNotFound(response.MessageText);
            }
        }

        public async Task<ActionResult> ListPostByCategory(string byWhat, string currentByWhat, string currentFilter, string searchString, int? page, int? pageSize)
        {
            if (searchString != null)
            {
                page = 1;
                byWhat = currentByWhat;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            if (string.IsNullOrEmpty(byWhat))
            {
                byWhat = currentByWhat;
            }
            ViewData["CurrentByWhat"] = byWhat;

            Expression<Func<Post, bool>> filter = null;
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null;
            Expression<Func<Post, bool>> filterByUser = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Title.Contains(searchString) && b.PostContent.Contains(searchString);
            }
            if (orderBy == null)
            {
                orderBy = b => b.OrderByDescending(x => x.ViewCount);
            }
            if(filterByUser == null)
            {
                filterByUser= b => b.Category.Name.Contains(byWhat);
            }
            var response = await _postService.GetPublishedAsyncPagination(filter: filter, filterByUser: filterByUser, orderBy: orderBy, page: page ?? 1, pageSize: pageSize ?? 4);

            if (response.Status == 200)
            {
                ListPostViewModel data = new ListPostViewModel()
                {
                    ListPost = response.Data as PaginationList<Post>,
                    Type = Common.ListPostTypeToIndex.ByCategory,
                    ByWhat = byWhat
                };

                return View("Index", data);
            }
            else
            {
                return HttpNotFound(response.MessageText);
            }
        }

        public async Task<ActionResult> GetPostsByTag(string byWhat, string currentByWhat, string currentFilter, string searchString, int? page, int? pageSize)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            if (string.IsNullOrEmpty(byWhat))
            {
                byWhat = currentByWhat;
            }
            ViewData["CurrentByWhat"] = byWhat;

            Expression<Func<Post, bool>> filter = null;
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null;
            Expression<Func<Post, bool>> filterByUser = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Title.Contains(searchString) && b.PostContent.Contains(searchString);
            }
            if (orderBy == null)
            {
                orderBy = b => b.OrderByDescending(x => x.ViewCount);
            }
            if (filterByUser == null)
            {
                filterByUser = b => b.Tags.Any(t => t.Name.Contains(byWhat));
            }
            var response = await _postService.GetPublishedAsyncPagination(filter: filter, filterByUser: filterByUser, orderBy: orderBy, page: page ?? 1, pageSize: pageSize ?? 4);

            if (response.Status == 200)
            {
                ListPostViewModel data = new ListPostViewModel()
                {
                    ListPost = response.Data as PaginationList<Post>,
                    Type = Common.ListPostTypeToIndex.ByTag,
                    ByWhat = byWhat
                };

                return View("Index",data);
            }
            else
            {
                return HttpNotFound(response.MessageText);
            }
        }
    }
}