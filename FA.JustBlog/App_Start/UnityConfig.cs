using FA.JustBlog.Areas.Admin.Controllers;
using FA.JustBlog.Controllers;
using FA.JustBlog.Core;
using FA.JustBlog.Core.Infrastructure;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Core.Service;
using System;
using System.Data.Entity;
using Unity;
using Unity.Injection;

namespace FA.JustBlog
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterSingleton<DbContext, JustBlogContext>();
            container.RegisterSingleton<IDbFactory, DbFactory>();
            container.RegisterSingleton<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IGerericRepository<Post, long>, PostRepository>();
            container.RegisterType<IPostRepository, PostRepository>();

            container.RegisterType<IGerericRepository<Tag, int>, TagRepository>();
            container.RegisterType<ITagRepository, TagRepository>();

            container.RegisterType<IGerericRepository<Category, int>, CategoryRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();

            container.RegisterType<IGerericRepository<Comment, Guid>, CommentRepository>();
            container.RegisterType<ICommentRepository, CommentRepository>();


            container.RegisterType<IPostService, PostService>();
            container.RegisterType<ITagService, TagService>();
            container.RegisterType<ICatgoryService, CategoryService>();
            container.RegisterType<ICommentService, CommentService>();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<RolesAdminController>(new InjectionConstructor());
            container.RegisterType<UsersAdminController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
        }
    }
}