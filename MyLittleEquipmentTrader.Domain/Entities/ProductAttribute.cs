using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;

namespace MyLittleEquipmentTrader.Domain.Entities
{
    public class ProductAttribute
    {
        public int AttributeID { get; set; }
        [JsonIgnore]
        public int? ProductID { get; set; }  // ProductID is nullable

        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public bool IsRequired { get; set; }
        public string AttributeType { get; set; }
        public string AttributeCode { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public string ValidationRule { get; set; }
        public bool IsActive { get; set; }
        public string DefaultValue { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string UnitType { get; set; }
        public string InputMask { get; set; }
        public string AttributeScope { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsSearchable { get; set; }
        public string AttributeGroup { get; set; }
        public int? ParentAttributeID { get; set; }
        public string TooltipText { get; set; }
        public bool IsMultiSelect { get; set; }
        public int CategoryID { get; set; }
        public bool IsVisibleInFilter { get; set; }

        // Navigation property
        public Product Product { get; set; }

        // Fluent API to configure the model
        public static void ConfigureKey(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAttribute>()
                .HasKey(pa => pa.AttributeID);  // Only AttributeID as the primary key

            // Configure the foreign key relationship with the Product table
            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.Product)
                .WithMany(p => p.ProductAttributes)  // Assuming Product has a collection of ProductAttributes
                .HasForeignKey(pa => pa.ProductID)
                .OnDelete(DeleteBehavior.SetNull);  // Set null on product deletion
        }
    }
}
