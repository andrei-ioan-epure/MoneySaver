using System.Runtime.InteropServices;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

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
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            optionsBuilder.UseSqlServer(isWindows ? WindowsConnectionString : MacOSConnectionString);
        }

        public DbSet<User> Users { get; set; }
    }
}
