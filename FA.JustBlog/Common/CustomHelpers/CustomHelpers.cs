using FA.JustBlog.Core.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FA.JustBlog.Common.CustomHelpers
{
    public static class CustomHelpers
    {
        public static IHtmlString CategoryLink(this HtmlHelper helper, Category category)
        {
            TagBuilder link = new TagBuilder("a");

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action("ListPostByCategory", "Post", new { area = "", byWhat = category.Name });

            link.MergeAttribute("href", url);
            link.InnerHtml = category.Name;

            return MvcHtmlString.Create(link.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            TagBuilder _tag = new TagBuilder("a");

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action("GetPostsByTag", "Post", new { area = "", byWhat = tag.Name });
            _tag.MergeAttribute("href", url);
            _tag.InnerHtml = "#" + tag.Name;

            var classBT = new
            {
                @class = "btn btn-secondary btn-lg"
            };

            _tag.MergeAttributes(new RouteValueDictionary(classBT), true);

            object styleAttributeData = new
            {
                style = "border-width: 0; font-size: 10px; box-shadow: none; padding: 0 12px; margin-right: 2px; text-transform: lowercase; line-height: 32px; background-color: black; transition: .3s all ease-in-out"
            };

            var styleValue = new RouteValueDictionary(styleAttributeData);
            foreach (var value in styleValue)
            {
                _tag.MergeAttribute(value.Key, value.Value.ToString());
            }


            return MvcHtmlString.Create(_tag.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString PostedOn(this HtmlHelper helper, DateTime postedOn)
        {
            string postedOnFormated = "";
            if (postedOn.Date == DateTime.Now.Date)
            {
                if (postedOn.Hour == DateTime.Now.Hour)
                {
                    postedOnFormated = String.Format("Posted {0} {1} ago", DateTime.Now.Minute - postedOn.Minute, "minutes");
                }
                else
                {
                    postedOnFormated = String.Format("Posted {0} {1} {2} {3} ago", DateTime.Now.Hour - postedOn.Hour, "hours", DateTime.Now.Minute - postedOn.Minute, "minutes");
                }

            }
            else if (DateTime.Now.Subtract(postedOn).Days == 1)
            {
                postedOnFormated = String.Format("Posted {0} {1} {2}", "Yesterday", "at", postedOn.ToString("HH:mm tt"));
            }
            else
            {
                postedOnFormated = String.Format("Posted {0} {1} {2}", postedOn.ToString("dd/MM/yyyy"), "at", postedOn.ToString("HH:mm tt"));
            }

            return MvcHtmlString.Create(postedOnFormated);
        }
    }
}