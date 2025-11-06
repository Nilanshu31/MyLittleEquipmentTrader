    using System.ComponentModel.DataAnnotations.Schema;

    namespace MyLittleEquipmentTrader.Domain.Entities
    {
        public class Product
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public string ProductType { get; set; }
            public decimal Price { get; set; }
            public decimal OriginalPrice { get; set; }
            public decimal SalePrice { get; set; }
            public double RatingAverage { get; set; }
            public int ReviewCount { get; set; }
            public string SKU { get; set; }
            public int StockQuantity { get; set; }
            public int QuantitySold { get; set; }
            public bool IsActive { get; set; }
            public bool IsDeleted { get; set; }
            public bool IsDiscounted { get; set; }
            public bool IsFeatured { get; set; }
            public bool IsInStock { get; set; }
            public bool IsNew { get; set; }
            public bool IsPreOrder { get; set; }
            public bool IsLimitedEdition { get; set; }
            public bool IsTrackInventory { get; set; }
            public bool IsSubscriptionBased { get; set; }
            public int? SubscriptionPeriod { get; set; }
            public DateTime? SubscriptionStartDate { get; set; }
            public DateTime? SubscriptionEndDate { get; set; }
            public int? DiscountID { get; set; }
            public DateTime? DiscountStartDate { get; set; }
            public DateTime? DiscountEndDate { get; set; }
            public decimal CostPrice { get; set; }
            public string? Currency { get; set; }
            public int Warranty { get; set; }
            public string MetaTitle { get; set; }
            public string MetaDescription { get; set; }
            public string Manufacturer { get; set; }
            public int BrandID { get; set; }
            public string UPC { get; set; }
            public string CountryOfOrigin { get; set; }
            public decimal Weight { get; set; }
            public string Dimensions { get; set; }
            public string Color { get; set; }
            public string Slug { get; set; }
            public string ThumbnailURL { get; set; }
            public List<string> ImageURLs { get; set; }
            public string VideoURL { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public string ModifiedBy { get; set; }
            public DateTime ModifiedDate { get; set; }
            public List<string> Tags { get; set; }
            [NotMapped]
            public Dictionary<string, string> CustomAttributes { get; set; }

            public List<int> CrossSellProducts { get; set; }
            public string AvailabilitySchedule { get; set; }

            public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
    }
    }
