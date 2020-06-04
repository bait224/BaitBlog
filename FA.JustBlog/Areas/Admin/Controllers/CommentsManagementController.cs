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
    public class CommentsManagementController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsManagementController(ICommentService commentService)
        {
            _commentService = commentService;
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
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "email_desc" : "Email";
            ViewData["TimeSortParm"] = sortOrder == "CommentTime" ? "comment_time_desc" : "CommentTime";
            ViewData["PostSortParm"] = sortOrder == "Post" ? "post_desc" : "Post";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Comment, bool>> filter = null;
            Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Name.Contains(searchString);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = b => b.OrderByDescending(s => s.Name);
                    break;
                case "Email":
                    orderBy = b => b.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    orderBy = b => b.OrderByDescending(s => s.Email);
                    break;
                case "CommentTime":
                    orderBy = b => b.OrderBy(s => s.CommentTime);
                    break;
                case "comment_time_desc":
                    orderBy = b => b.OrderByDescending(s => s.CommentTime);
                    break;
                case "Post":
                    orderBy = b => b.OrderBy(s => s.Post.Title);
                    break;
                case "post_desc":
                    orderBy = b => b.OrderByDescending(s => s.Post.Title);
                    break;
                default:
                    orderBy = b => b.OrderBy(s => s.Name);
                    break;
            }

            var response = await _commentService.GetAllAsyncPagination(filter: filter, orderBy: orderBy, page: page ?? 1, pageSize: pageSize ?? 10);
            if (response.Status != 200)
            {
                return HttpNotFound();
            }
            else
            {
                return View(response.Data);
            }
        }
    }
}