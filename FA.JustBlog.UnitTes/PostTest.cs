using FA.JustBlog.Core;
using FA.JustBlog.Core.Repositories;
using NUnit.Framework;
using System;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Service;
using FA.JustBlog.Core.Common;

namespace FA.JustBlog.UnitTest
{
    [TestFixture]
    public class PostTest
    {
        private IPostService postService;
        [SetUp]
        public void SetUp(IPostService _postService)
        {
            postService = _postService;
        }

        [TestCase]
        public void AddPostInServiceSuccess()
        {
            var newPost = new Post()
            {
                Title = "Day la post test",
                ShortDescription = "Day la short description",
                PostContent = "Day la content",
                Published = true,
                PostedOn = DateTime.UtcNow,
                Modified = false,
                CategoryId = 2

            };
            ResponseModel expected = new ResponseModel()
            {
                Status = 200,
                MessageText = "Add Success"
            };
            var addResult = postService.Add(newPost);
            Assert.AreEqual(expected.Status, addResult.Status);
            Assert.AreEqual(expected.MessageText, addResult.MessageText);
            Assert.AreEqual(expected.Data, addResult.Data);
        }

        [TestCase]
        public void AddPostInServiceFail()
        {
            var newPost = new Post()
            {
                Title = "",
                ShortDescription = "Day la short description",
                PostContent = "Day la content",
                Published = true,
                PostedOn = DateTime.UtcNow,
                Modified = false,
                CategoryId = 2

            };
            ResponseModel expected = new ResponseModel()
            {
                Status = 302,
                MessageText = "The Title must be required.\n"
            };
            var addResult = postService.Add(newPost);
            Assert.AreEqual(expected.Status, addResult.Status);
            Assert.AreEqual(expected.MessageText, addResult.MessageText);
            Assert.AreEqual(expected.Data, addResult.Data);
        }

        [TestCase]
        public void CountPostsForCategorySuccess()
        {
            ResponseModel expected = new ResponseModel()
            {
                Status = 200,
                MessageText = "Success",
                Data = 2
            };
            var countResult = postService.CountPostsForCategory("Technology");
            Assert.AreEqual(expected.Status, countResult.Status);
            Assert.AreEqual(expected.MessageText, countResult.MessageText);
            Assert.AreEqual(expected.Data, countResult.Data);
        }

        [TestCase]
        public void CountPostsForCategoryFail()
        {
            ResponseModel expected = new ResponseModel()
            {
                Status = 302,
                MessageText = "Input Invalid",
                Data = 0
            };
            var countResult = postService.CountPostsForCategory("");
            Assert.AreEqual(expected.Status, countResult.Status);
            Assert.AreEqual(expected.MessageText, countResult.MessageText);
            Assert.AreEqual(expected.Data, countResult.Data);
        }
    }
}
