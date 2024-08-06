using GBBulletin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GBBulletin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WebsiteContent> WebsiteContents { get; set; }
        public DbSet<Newsinfo> Newsinfo { get; set; }
        public DbSet<NewsGenre> NewsGenres { get; set; }
        public DbSet<NewsStaff> NewsStaffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Add your entity configurations here
            //modelBuilder.Entity<NewsArticle>()
            //    .HasOne(a => a.NewsGenre)
            //    .WithMany()
            //    .HasForeignKey(a => a.NewsGenreId)
            //    .OnDelete(DeleteBehavior.Restrict); // This line sets the delete behavior to Restrict (no cascading delete)
        }
    }
}
