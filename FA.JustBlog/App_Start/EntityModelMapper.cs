using AutoMapper;
using FA.JustBlog.Areas.Admin.ViewModels;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.App_Start
{
    internal class EntityModelMapper : Profile
    {
        public EntityModelMapper()
        {
            #region Enity To Model

            CreateMap<Post, PostAddViewModel>();
            CreateMap<Post, PostEditViewModel>();
            CreateMap<Category, CategoryEditViewModel>();
            CreateMap<Tag, TagEditViewModel>();

            #endregion

            #region Model to Entity

            CreateMap<PostAddViewModel, Post>();
            CreateMap<PostEditViewModel, Post>();
            CreateMap<CategoryEditViewModel, Category>();
            CreateMap<TagEditViewModel, Tag>();

            #endregion
        }
    }
}