using CheckoutApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CheckoutApp.DataAccess
{
    public class BasketContext: DbContext
    {
        private readonly string _connectionString;

        public DbSet<BasketDto> Baskets { get; set; }
        public DbSet<ArticleDto> Articles { get; set; }
        public DbSet<BasketArticleDto> BasketArticles { get; set; }

        public BasketContext():base()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("DbConfig");
        }
        public BasketContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder.UseNpgsql(_connectionString, b => b.MigrationsAssembly("CheckoutApp.DataAccess"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleDto>().HasIndex(a => a.Item).IsUnique();
            modelBuilder.Entity<ArticleDto>().HasData(
                new ArticleDto
                {
                    Id = 1,
                    Price = 20,
                    Item = "Book",
                },
                new ArticleDto
                {
                    Id = 2,
                    Price = 30,
                    Item = "Pencil",
                },
                new ArticleDto
                {
                    Id = 3,
                    Price = 12,
                    Item = "Paper",
                }
            );
        }
    }
}