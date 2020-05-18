using AutoMapper;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Service;
using FA.JustBlog.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FA.JustBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreateComment(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                Comment comment = Mapper.Map<Comment>(commentViewModel);
                comment.Id = new Guid();
                comment.CommentTime = DateTime.Now;
                var response = _commentService.Add(comment);

                if (response.Status == 200)
                {
                    return Json(new
                    {
                        HasErrors = false,
                        Data = commentViewModel,
                        Message = response.MessageText
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        HasErrors = false,
                        Data = commentViewModel,
                        Message = response.MessageText
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            var errorMessages = ModelState.ToDictionary(
                                    ms => ms.Key,
                                    ms => ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                ).Where(ms => ms.Value.Count() > 0);

            return Json(new
            {
                HasErrors = true,
                Data = new { },
                Message = errorMessages
            }, JsonRequestBehavior.AllowGet);
        }
    }
}