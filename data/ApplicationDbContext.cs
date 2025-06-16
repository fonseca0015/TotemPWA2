using Microsoft.EntityFrameworkCore;
using TotemPWA.Models;

namespace TotemPWA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Combo_product> Combo_product {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define self-referencing relationship for Category
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.NoAction);  // Allow parent category to be null

            // Define 1-to-many relationship between Product and Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            // idk if this works
            modelBuilder.Entity<Combo_product>()
                .HasKey(co => new { co.comboId, co.productId });
            modelBuilder.Entity<Combo_product>()
                .HasOne(co => co.product)
                .WithMany(p => p.InCombo)
                .HasForeignKey(co => co.productId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Combo_product>()
                .HasOne(co => co.combo)
                .WithMany(p => p.Combo)
                .HasForeignKey(co => co.comboId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}