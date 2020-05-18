using FA.JustBlog.Core.Service;
using System.Web.Mvc;

namespace FA.JustBlog.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [ChildActionOnly]
        public ActionResult PopularTags()
        {
            var popularTags = _tagService.GetPopularTags(10).Data;

            return PartialView("_PopularTags", popularTags);
        }
    }
}