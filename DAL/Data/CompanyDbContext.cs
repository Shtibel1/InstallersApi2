using DAL.Entities;
using DAL.Providers;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class CompanyDbContext : DbContext
    {
        private readonly string _connectionString;

        public CompanyDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-9C5JK3S\\SQLEXPRESS;Initial Catalog=Shtibay;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<ServiceProviderPricing> ServiceProviderPricing { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceProviderCategory> ServiceProviderCategories { get; set; }
        public DbSet<Marketer> Marketers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

        }
    }
}
