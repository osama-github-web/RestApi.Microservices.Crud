using CategoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<ProductCategory> ProductCategories  { get;set;}
        public DbSet<Category> Categories  { get;set;}
    }
}
