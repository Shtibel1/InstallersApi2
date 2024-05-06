using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DAL.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var characters = modelBuilder.Entity<Category>();
            var InstallerPricing = modelBuilder.Entity<InstallerPricing>();
            var assignment = modelBuilder.Entity<Assignment>();
            var installers = modelBuilder.Entity<Installer>();
            var managers = modelBuilder.Entity<Manager>();
            var products = modelBuilder.Entity<Product>();
            var commends = modelBuilder.Entity<Comment>();
            var categoryInstaller = modelBuilder.Entity<CategoryInstaller>();

            categoryInstaller.HasOne(ci => ci.Category).WithMany(c => c.CategoryInstallers).HasForeignKey(ci => ci.CategoryId);
            categoryInstaller.HasOne(ci => ci.Installer).WithMany(c => c.CategoryInstallers).HasForeignKey(ci => ci.InstallerId);
            categoryInstaller.HasKey(ci => new { ci.CategoryId, ci.InstallerId });


            assignment.HasOne(a => a.Manager).WithMany().HasForeignKey(a => a.ManagerId).OnDelete(DeleteBehavior.Restrict);
            //prevent customer to delete when deleting an assignment
            assignment.HasOne(a => a.Customer).WithMany().HasForeignKey(a => a.CustomerId).OnDelete(DeleteBehavior.Restrict);

            managers.HasBaseType(typeof(Worker));
            installers.HasBaseType(typeof(Worker));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<InstallerPricing> InstallerPricing { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Installer> Installers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CategoryInstaller> CategoryInstaller { get; set; }

    }















    public class EfRepository<T> : IAsyncRepository<T> where T: class
    {
        private readonly DbContext _dbcontext;
        public EfRepository(DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync().ConfigureAwait(false);
        }


        public async Task<T> AddAsync(T entity, bool isTransaction = true)
        {
            var res = await _dbcontext.Set<T>().AddAsync(entity).ConfigureAwait(false);
            if (!isTransaction)
                await _dbcontext.SaveChangesAsync().ConfigureAwait(false);
            return res.Entity;
        }
    }
    public interface IAsyncRepository<T> where T: class
    {
        public Task<IEnumerable<T>> ListAllAsync();
        public Task<T> AddAsync(T entity, bool isTransaction = true);

    }
    public class QueryBaseSpecification<T>
    {
        public List<Func<T,bool>> Where { get; set; }
        public List<object> Include { get; set; }
        public void AddWhere(Func<T, bool> predicate)
        {
            Where.Add(predicate);
        }
    }
}
