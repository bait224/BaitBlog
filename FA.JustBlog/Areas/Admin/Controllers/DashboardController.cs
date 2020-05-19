using FA.JustBlog.Areas.Admin.ViewModels;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Contributor")]
    public class DashboardController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICatgoryService _categoryService;

        public DashboardController(IPostService postService, ICatgoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }
        public ActionResult Index()
        {
            var publishedPost = _postService.GetPublisedPosts().Data as List<Post>;
            var unPublishedPost = _postService.GetUnpublisedPosts().Data as List<Post>;
            var totalPost = _postService.GetAll().Data as List<Post>;

            string userName = User.Identity.Name;
            DashboardViewModel data = new DashboardViewModel();
            if (publishedPost.Any() && publishedPost != null)
            {
                data.PublishedCount = User.IsInRole("Contributor") && !User.IsInRole("Administrators") ? publishedPost.Where(x => x.CreatedBy == userName).Count() : publishedPost.Count();
            }
            if (unPublishedPost.Any() && unPublishedPost != null)
            {
                data.UnPublishedCount = User.IsInRole("Contributor") && !User.IsInRole("Administrators") ? unPublishedPost.Where(x => x.CreatedBy == userName).Count() : unPublishedPost.Count();
            }
            if (totalPost.Any() && totalPost != null)
            {
                data.TotalPost = User.IsInRole("Contributor") && !User.IsInRole("Administrators") ? totalPost.Where(x => x.CreatedBy == userName).Count() : totalPost.Count();
            }

            return View(data);
        }

        public JsonResult GetPostForChart()
        {
            var responseCate = _categoryService.GetAll();
            if(responseCate.Status == 200)
            {
                List<Category> listCate = responseCate.Data as List<Category>;
                List<CategoryChart> DataChart = new List<CategoryChart>();
                foreach(Category cate in listCate)
                {
                    int countPost = Convert.ToInt32(_postService.CountPostsForCategory(cate.Name).Data);
                    DataChart.Add(new CategoryChart()
                    {
                        Category = cate.Name,
                        CountPost = countPost
                    });
                }

                return Json(DataChart, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}