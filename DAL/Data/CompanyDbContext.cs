using DAL.Entities;
using DAL.Providers;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<Calculation> Calculation { get; set; }
        public DbSet<ServiceProduct> ServiceProducts { get; set; }
        public DbSet<ProductRequiredServiceProduct> ProductRequiredServiceProducts { get; set; }
        public DbSet<ServiceProviderStock> ServiceProviderStock { get; set; }
        public DbSet<ServiceProviderStockAudit> ServiceProviderStockAudit { get; set; }

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

            modelBuilder.Entity<CalculationAssignment>()
       .HasKey(ca => new { ca.CalculationId, ca.AssignmentId });

            modelBuilder.Entity<CalculationAssignment>()
                .HasOne(ca => ca.Calculation)
                .WithMany(c => c.CalculationAssignments)
                .HasForeignKey(ca => ca.CalculationId);

            modelBuilder.Entity<CalculationAssignment>()
                .HasOne(ca => ca.Assignment)
                .WithMany(a => a.CalculationAssignments)
                .HasForeignKey(ca => ca.AssignmentId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductRequiredServiceProduct>(e =>
            {
                e.ToTable("ProductRequiredServiceProducts");
                e.HasKey(x => new { x.ProductId, x.ServiceProductId });
                e.Property(x => x.Quantity).HasDefaultValue(1);

                e.HasIndex(x => x.ServiceProductId).HasDatabaseName("IX_PRSP_ServiceProductId");
            });

        }
    }
}
