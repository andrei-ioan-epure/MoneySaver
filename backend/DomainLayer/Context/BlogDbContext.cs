using System.Runtime.InteropServices;
using System.Xml.Linq;
using DomainLayer.Mappings;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DomainLayer.Context
{
    public class BlogDbContext : DbContext
    {
        private readonly string WindowsConnectionString = @"Server=.\SQLExpress;Database=MoneySaverSP23DB;Trusted_Connection=True;TrustServerCertificate=true";
        private readonly string Windows2ConnectionString = @"Server=localhost\SQLEXPRESS;Database=MoneySaverSP23DB;Trusted_Connection=True;TrustServerCertificate=True;";
        private readonly string MacOSConnectionString = "Server=localhost,1433;Database=MoneySaverSP23DB;User=SA;Password=Admin123;TrustServerCertificate=True;Encrypt=false;";

        public BlogDbContext() { }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<User>()
                .HasMany(x => x.FavoriteArticles)
                .WithMany(x => x.FavoriteUsers);

            builder.Entity<User>()
                .HasMany(x => x.LikedComments)
                .WithMany(x => x.LikedBy);

            builder.Entity<Comment>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.CreatorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Article>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedArticles)
                .HasForeignKey(x => x.CreatorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Comment>()
                .HasOne(x => x.Article)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ArticleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            UserMapping.Map(builder);
            CommentMapping.Map(builder);
            ArticleMapping.Map(builder);
            //base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            optionsBuilder.UseSqlServer(isWindows ? WindowsConnectionString : MacOSConnectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get;set;}
        public DbSet<Comment> Comments { get; set; }
    }
}
