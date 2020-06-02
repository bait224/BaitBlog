using FA.JustBlog.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace FA.JustBlog.Core
{
    public class JustBlogContext : IdentityDbContext<ApplicationUser, ApplicationRole,
        int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public JustBlogContext() : base("name=ConnString")
        {

        }
        static JustBlogContext()
        {
            Database.SetInitializer(new JustBlogInitializer());
        }

        public static JustBlogContext Create()
        {
            return new JustBlogContext();
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>()
                        .HasMany<Tag>(p => p.Tags)
                        .WithMany(t => t.Posts)
                        .Map(pt => {
                                     pt.MapLeftKey("PostId");
                                     pt.MapRightKey("TagId");
                                     pt.ToTable("PostTagMap");
                                   });
        }

        public override int SaveChanges()
        {
            OnBeforeSaveChange();

            return base.SaveChanges();
        }

        private void OnBeforeSaveChange()
        {
            var modifiedEntries = ChangeTracker.Entries()
                            .Where(x => x.Entity is IAuditableEntity
                                && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identityName = Thread.CurrentPrincipal.Identity.Name;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }
        }
    }
}
