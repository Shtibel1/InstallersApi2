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
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Additional> Additionals { get; set; }
        public DbSet<AdditionalPrice> AdditionalsPrices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceProviderCategory> ServiceProviderCategories { get; set; }
        public DbSet<Marketer> Marketers { get; set; }
        public DbSet<AssignmentAdditionalPrice> AssignmentAdditionalPrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AssignmentAdditionalPrice>()
           .HasKey(ap => new { ap.AssignmentId, ap.AdditionalPriceId });

            modelBuilder.Entity<AssignmentAdditionalPrice>()
                .HasOne(ap => ap.Assignment)
                .WithMany(a => a.AssignmentAdditionalPrices)
                .HasForeignKey(ap => ap.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade); // Set cascade delete on Assignment

            modelBuilder.Entity<AssignmentAdditionalPrice>()
                .HasOne(ap => ap.AdditionalPrice)
                .WithMany(ap => ap.AssignmentAdditionalPrices)
                .HasForeignKey(ap => ap.AdditionalPriceId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

        }
    }
}
