using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class CategoryInstaller
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid InstallerId { get; set; }
        public Installer Installer { get; set; }
    }

    /*
     * modelBuilder.Entity<CategoryInstaller>()
            .HasKey(ci => new { ci.CategoryId, ci.InstallerId });

     * modelBuilder.Entity<CategoryInstaller>()
            .HasOne(ci => ci.Category)
            .WithMany(c => c.CategoryInstallers)
            .HasForeignKey(ci => ci.CategoryId);

        modelBuilder.Entity<CategoryInstaller>()
            .HasOne(ci => ci.Installer)
            .WithMany(i => i.CategoryInstallers)
            .HasForeignKey(ci => ci.InstallerId);
     */
}
