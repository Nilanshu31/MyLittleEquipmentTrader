using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using MyLittleEquipmentTrader.Domain.Entities;

namespace MyLittleEquipmentTrader.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor passing DbContextOptions to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for your entities (tables in the database)
        public DbSet<Product> Products { get; set; }               // Represents the Products table
        public DbSet<ProductAttribute> ProductAttributes { get; set; } // Represents the ProductAttributes table
        public DbSet<ProductTag> ProductTags { get; set; }   // Represents the ProductTags table
        public DbSet<AccessRole> AccessRoles { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; } // Represents the SalesOrders table
        public DbSet<Manufacturer> Manufacturers { get; set; }   // Represents the Manufacturers table
        //public DbSet<Category> Manufac { get; set; }
        // Configure relationships and entity properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Configure ProductAttribute Key (if not already configured in ProductAttribute class)
            ProductAttribute.ConfigureKey(modelBuilder);

            // 2. Configure relationships between Product and ProductAttributes
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductAttributes) // A product can have many product attributes
                .WithOne(pa => pa.Product)        // A product attribute belongs to one product
                .HasForeignKey(pa => pa.ProductID) // Foreign key in ProductAttribute linking to Product
                .OnDelete(DeleteBehavior.SetNull); // Set ProductID to null when the associated product is deleted

            // 3. Configure ProductID to be nullable in ProductAttribute
            modelBuilder.Entity<ProductAttribute>()
                .Property(pa => pa.ProductID)
                .IsRequired(false); // Make ProductID nullable in ProductAttribute table

            // 4. Configure decimal precision for financial fields in the Product entity
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // Defines the precision and scale for the Price field (18 digits, 2 decimal places)

            modelBuilder.Entity<Product>()
                .Property(p => p.SalePrice)
                .HasColumnType("decimal(18,2)"); // Defines the precision and scale for the SalePrice field (18 digits, 2 decimal places)

            // 5. Configure unique index on Slug (used for SEO purposes)
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Slug)
                .IsUnique(); // Ensures the Slug value is unique in the Products table

            // 6. Configure unique index on SKU (Stock Keeping Unit)
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique(); // Ensures the SKU value is unique in the Products table

            // 7. Configure ProductTag entity relationships and indexes (if necessary)
            modelBuilder.Entity<ProductTag>()
                .HasKey(pt => pt.TagID); // Configures TagID as the primary key for the ProductTags table

            // 8. Configure relationships and constraints for ProductTags (if needed)
            // If you plan to have relationships like ParentTagID (for nested tags), you can configure them here

            // 9. Configure the many-to-many relationship between Product and ProductTag (No explicit join entity)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductTags) // A Product can have many ProductTags
                .WithMany(pt => pt.Products) // A ProductTag can have many Products
                .UsingEntity(j => j.ToTable("ProductTagProduct")); // This specifies the join table name

            modelBuilder.Entity<AccessRole>(entity =>
            {
                entity.HasKey(e => e.RoleId); // Explicitly declare primary key
            });

            // SalesOrder Configuration
            modelBuilder.Entity<SalesOrder>()
                .HasKey(so => so.SalesOrderId);  // Configure the primary key for SalesOrder

            //modelBuilder.Entity<SalesOrder>()
            //    .HasOne(so => so.Tenant)  // A SalesOrder belongs to one Tenant
            //    .WithMany(t => t.SalesOrders)  // A Tenant can have many SalesOrders
            //    .HasForeignKey(so => so.TenantId) // Foreign key in SalesOrder linking to Tenant
            //    .OnDelete(DeleteBehavior.Cascade);  // On Tenant delete, delete related SalesOrders

            //modelBuilder.Entity<SalesOrder>()
            //    .HasOne(so => so.Product)  // A SalesOrder belongs to one Product
            //    .WithMany(p => p.SalesOrders)  // A Product can have many SalesOrders
            //    .HasForeignKey(so => so.ProductId) // Foreign key in SalesOrder linking to Product
            //    .OnDelete(DeleteBehavior.SetNull);  // On Product delete, set SalesOrder's ProductId to null

            // You can add additional configurations for other tables/entities as needed
        }
    }
}
