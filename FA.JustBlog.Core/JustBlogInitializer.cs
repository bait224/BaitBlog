using FA.JustBlog.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core
{
    public class JustBlogInitializer : DropCreateDatabaseIfModelChanges<JustBlogContext>
    {
        protected override void Seed(JustBlogContext context)
        {
            CreateDefaultCategories(context);
            CreateDefaultTags(context);
            CreateDefaultPosts(context);

            Task.Run(async () => { await SeedAsync(context); }).Wait();
            context.SaveChanges();
        }

        private void CreateDefaultCategories(JustBlogContext context)
        {
            IList<Category> defaultCategories = new List<Category>
            {
                new Category() {Id = 1, Name = "Sport", UrlSlug = "sport", Description = "Sport news" },
                new Category() {Id = 2, Name = "Life", UrlSlug = "life", Description = "Life news" },
                new Category() {Id = 3, Name = "Entertainment", UrlSlug = "entertainment", Description = "Entartainment news" },
                new Category() {Id = 4, Name = "Technology", UrlSlug = "technology", Description = "Technology news" },
            };
            context.Categories.AddRange(defaultCategories);
            context.SaveChanges();
        }

        private void CreateDefaultTags(JustBlogContext context)
        {
            IList<Tag> defaultTags = new List<Tag>
            {
                new Tag() {Id = 1, Name = "computer", UrlSlug = "computer", Description = "" },
                new Tag() {Id = 2, Name = "automatic", UrlSlug = "automatic", Description = "" },
                new Tag() {Id = 3, Name = "tenis", UrlSlug = "tenis", Description = "" },
            };
            context.Tags.AddRange(defaultTags);
            context.SaveChanges();
        }

        private void CreateDefaultPosts(JustBlogContext context)
        {
            var tagSport = new Tag() { Name = "sport", UrlSlug = "sport", Description = "" };
            var tagFootball = new Tag() { Name = "football", UrlSlug = "football", Description = "" };
            var tagBasketball = new Tag() { Name = "basketball", UrlSlug = "basketball", Description = "" };
            var tagNews = new Tag() { Name = "news", UrlSlug = "news", Description = "" };
            var tagStar = new Tag() { Name = "star", UrlSlug = "star", Description = "" };
            var tagTV = new Tag() { Name = "tv", UrlSlug = "tv", Description = "" };
            var tagFaceBook = new Tag() { Name = "facebook", UrlSlug = "facebook", Description = "" };
            var tagHealthy = new Tag() { Name = "healthy", UrlSlug = "healthy", Description = "" };
            var tagCovid = new Tag() { Name = "covid19", UrlSlug = "covid19", Description = "" };
            var tagGoogle = new Tag() { Name = "google", UrlSlug = "google", Description = "" };

            IList<Post> defaultPosts = new List<Post>
            {
                new Post()
                {
                    Id = 1,
                    Title = "Cowboys VP Stephen Jones says new-look defense will take page from Bill Belichick’s book",
                    ShortDescription = "The Dallas Cowboys are rebranding their defense with the arrival of a new coaching staff and new defensive scheme.",
                    PostContent = "<p>The Dallas Cowboys are rebranding their defense with the arrival of a new coaching staff and new defensive scheme.</p><p>A shift in personnel, too, follows a free agency in which the Cowboys lost four defensive starters, including 11&frac12;-sack man Robert Quinn and sticky cornerback Byron Jones.</p>",
                    UrlSlug = "cowboys-want-defense-adopt-strategy-patriots-bill-belichick",
                    Published = true,
                    PostedOn = new DateTime(2019,04,18,10,0,0,DateTimeKind.Utc),
                    Modified = false,
                    ModifiedDate = DateTime.Now,
                    CategoryId = 1,
                    CreatedBy = "admin",
                    Tags = new HashSet<Tag> {tagSport, tagFootball, tagNews}
                },
                new Post()
                {
                    Id = 2,
                    Title = "New York Liberty select Sabrina Ionescu with No. 1 pick in WNBA draft",
                    ShortDescription = "The legend began on outdoor courts, over a couple bucks or a Slurpee.",
                    PostContent = "<p>The legend began on outdoor courts, over a couple bucks or a Slurpee.</p><p>Sabrina Ionescu honed her craft there long before the college basketball world knew her name. The pickup games with her twin brother, Eddy, became her summer pastime. And the first people to witness a special talent were the unsuspecting victims.</p>",
                    UrlSlug = "sabrina-ionescu-perfect-fit-new-york-liberty-wnba-draft",
                    Published = true,
                    PostedOn = new DateTime(2019,03,20,8,45,0,DateTimeKind.Utc),
                    Modified = false,
                    ModifiedDate = DateTime.Now,
                    CategoryId = 1,
                    CreatedBy = "admin",
                    Tags = new HashSet<Tag> {tagBasketball, tagNews, tagSport}
                },
                new Post()
                {
                    Id = 3,
                    Title = "Wait, it's Friday? How to make the weekend shine in lockdown when days blur",
                    ShortDescription = "Hosting the historic, first ever livestreamed Saturday Night Live,” Tom Hanks made a point in the opening monologue that many Americans have become acutely aware of.",
                    PostContent = "<p>Hosting the historic, first ever livestreamed Saturday Night Live, Tom Hanks made a point in the opening monologue that many Americans have become acutely aware of.</p><p>&ldquo;There&rsquo;s no such thing as Saturday anymore,&quot;&nbsp; Hanks said, speaking from his kitchen. &quot;Every day is just today.&rdquo;</p><p>He might have gone even further, pointing out that weekends have&nbsp;become endangered&nbsp;in the locked-down-at-home&nbsp;coronavirus era, as the days blurs together to vaguely different variations in a country sheltering in place.</p>",
                    UrlSlug = "coronavirus-weekend-survival-shine-when-days-blur",
                    Published = false,
                    PostedOn = new DateTime(2019,04,11,14,20,0,DateTimeKind.Utc),
                    Modified = true,
                    ModifiedDate = DateTime.Now,
                    CategoryId = 2,
                    CreatedBy = "admin",
                    Tags = new HashSet<Tag> {tagCovid, tagHealthy, tagNews }
                },
                new Post()
                {
                    Id = 4,
                    Title = "Beyoncé dedicates 'When You Wish Upon a Star' to health workers in 'Disney Family Singalong'",
                    ShortDescription = "When you wish upon a star, maybe Beyoncé will show up.",
                    PostContent = "<p>When you wish upon a star, maybe&nbsp;Beyonc&eacute; will show up.</p><p>Viewers enjoyed a surprise appearance by Queen Bey Thursday during &quot;The Disney Family Singalong,&quot; an ABC special designed to provide spoonfuls of sugar &ndash; as in the &quot;Mary Poppins&quot; classic and a brighter mood &ndash; &nbsp;during challenging coronavirus times.</p>",
                    UrlSlug = "beyonce-ariana-grande-demi-lovato-perform-disney-family-singalong",
                    Published = true,
                    PostedOn = new DateTime(2019,04,07,8,10,0,DateTimeKind.Utc),
                    Modified = false,
                    ModifiedDate = DateTime.Now,
                    CategoryId = 3,
                    CreatedBy = "admin",
                    Tags = new HashSet<Tag> {tagStar, tagTV, tagNews }
                },
                new Post()
                {
                    Id = 5,
                    Title = "Put Google in your wallet. Tech giant confirms it wants to release a debit card.",
                    ShortDescription = "Corrections & Clarifications: An earlier version of this story misidentified the Apple Card.",
                    PostContent = "<p><em>Corrections &amp; Clarifications: An earlier version of this story misidentified the Apple Card.</em></p><p>Google is hoping you&#39;d like have a branded debit card to go with your Google Pay account.&nbsp;</p><p>The tech giant teamed up with Citibank and Stanford Federal Credit Union to explore what it calls &quot;smart checking accounts,&quot; that would be attached to a Google debit card.&nbsp;</p>",
                    UrlSlug = "google-pay-debit-card-search-giant-seeks-take-apple-card",
                    Published = true,
                    PostedOn = new DateTime(2020,05,07,13,0,0,DateTimeKind.Utc),
                    Modified = true,
                    ModifiedDate = DateTime.Now,
                    CategoryId = 4,
                    CreatedBy = "admin",
                    Tags = new HashSet<Tag> {tagGoogle, tagNews }
                },
                new Post()
                {
                    Id = 6,
                    Title = "Facebook to warn users who 'liked' coronavirus misinformation, hoaxes on social network",
                    ShortDescription = "NEW YORK – Have you liked or commented on a Facebook post about the COVID-19 pandemic?",
                    PostContent = "<p>NEW YORK&nbsp;&ndash; Have you liked or commented on a Facebook post about the COVID-19 pandemic?</p><p>Facebook is about to begin letting you know if you&rsquo;ve spread bad information.</p><p>The company will soon be letting users know if they liked, reacted to, or commented on posts with harmful misinformation about the virus that was removed by moderators. It will also direct those who engaged with those posts to information about virus myths debunked by the World Health Organization.</p>",
                    UrlSlug = "coronavirus-facebook-warn-users-who-liked-pandemic-hoaxes",
                    Published = true,
                    PostedOn = new DateTime(2020,05,07,7,0,0,DateTimeKind.Utc),
                    Modified = false,
                    ModifiedDate = DateTime.Now,
                    CategoryId = 4,
                    CreatedBy = "admin",
                    Tags = new HashSet<Tag> {tagFaceBook, tagNews }
                },
                new Post()
                {
                    Id = 7,
                    Title = "How To Make The Mini Pancake Cereal That's Taking Over The Internet",
                    ShortDescription = "First it was dalgona coffee, but now there's a new TikTok food craze: mini pancake cereal.",
                    PostContent = "<p>NEW YORK&nbsp;&ndash; Have you liked or commented on a Facebook post about the COVID-19 pandemic?</p><p>Facebook is about to begin letting you know if you&rsquo;ve spread bad information.</p><p>The company will soon be letting users know if they liked, reacted to, or commented on posts with harmful misinformation about the virus that was removed by moderators. It will also direct those who engaged with those posts to information about virus myths debunked by the World Health Organization.</p>",
                    UrlSlug = "coronavirus-facebook-warn-users-who-liked-pandemic-hoaxes",
                    Published = false,
                    PostedOn = new DateTime(2020,05,07,7,0,0,DateTimeKind.Utc),
                    Modified = false,
                    ModifiedDate = DateTime.Now,
                    CategoryId = 4,
                    CreatedBy = "admin",
                    Tags = new HashSet<Tag> {tagFaceBook, tagNews }
                }
            };

            context.Posts.AddRange(defaultPosts);
            context.SaveChanges();
        }

        #region Add User and Role Identity

        private async Task SeedAsync(JustBlogContext context)
        {


            var userManager = new UserManager<ApplicationUser, int>(new UserStore<ApplicationUser, ApplicationRole, int,
                CustomUserLogin, CustomUserRole, CustomUserClaim>(context));
            var roleManager = new RoleManager<ApplicationRole, int>(new RoleStore<ApplicationRole, int, CustomUserRole>(context));
            var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "Administrators" });
                await roleManager.CreateAsync(new ApplicationRole { Name = "Contributor" });
                await roleManager.CreateAsync(new ApplicationRole { Name = "User" });
            }

            if (!userManager.Users.Any(u => u.UserName == "admin@domain.com"))
            {
                var user = new ApplicationUser
                {
                    FullName = "Admin",
                    BirthDate = new DateTime(2004, 07, 26),
                    City = "Ha Noi",
                    Address = "Thach That",
                    Photo = "avatar-4.png",
                    Email = "admin@domain.com",
                    UserName = "admin@domain.com",
                    PasswordHash = passwordHasher.HashPassword("Abc@1234"),
                    EmailConfirmed = true,
                    PhoneNumber = "0944551356",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                await userManager.CreateAsync(user, "Abc@1234");
                await userManager.AddToRoleAsync(user.Id, "Administrators");
                await userManager.AddToRoleAsync(user.Id, "Contributor");
                await userManager.AddToRoleAsync(user.Id, "User");
            }

            if (!userManager.Users.Any(u => u.UserName == "ba@domain.com"))
            {
                var user = new ApplicationUser
                {
                    FullName = "Xuan Ba",
                    BirthDate = new DateTime(2004, 07, 26),
                    City = "Ninh Binh",
                    Address = "Nho Quan",
                    Photo = "avatar-1.png",
                    Email = "ba@domain.com",
                    UserName = "ba@domain.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0944551356",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                await userManager.CreateAsync(user, "Abc@1234");
                await userManager.AddToRoleAsync(user.Id, "Contributor");
            }
            if (!userManager.Users.Any(u => u.UserName == "baaa@domain.com"))
            {
                var user = new ApplicationUser
                {
                    FullName = "Xuan Baaa",
                    BirthDate = new DateTime(2004, 07, 26),
                    City = "Ninh Binh",
                    Address = "Nho Quan",
                    Photo = "avatar-1.png",
                    Email = "ba@domain.com",
                    UserName = "baaa@domain.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0944551356",
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                await userManager.CreateAsync(user, "Abc@1234");
                await userManager.AddToRoleAsync(user.Id, "User");
            }

        }
        #endregion

    }
}
