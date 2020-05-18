using FA.JustBlog.Core;
using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Core.Service;
using NUnit.Framework;
using System;

namespace FA.JustBlog.UnitTes
{
    class CategoryTest
    {
        private ICatgoryService categoryService;
        [SetUp]
        public void SetUp(ICatgoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        [TestCase]
        public void AddCategoryInServiceSuccess()
        {
            var newCategory = new Category()
            {
               Name = "CategoryDeTest",
               Description = "Description",
            };
            ResponseModel expected = new ResponseModel()
            {
                Status = 200,
                MessageText = "Add Success"
            };
            var addResult = categoryService.Add(newCategory);
            Assert.AreEqual(expected.Status, addResult.Status);
            Assert.AreEqual(expected.MessageText, addResult.MessageText);
            Assert.AreEqual(expected.Data, addResult.Data);
        }
    }
}
