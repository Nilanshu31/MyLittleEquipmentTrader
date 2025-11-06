public class CategoryDto
{
    public int CategoryID { get; set; }
    public int TenantID { get; set; }
    public string CategoryName { get; set; }
    public string Slug { get; set; }
    public int? ParentCategoryID { get; set; }
    public int CategoryLevel { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public bool ShowInMenu { get; set; }
    public string CategoryImageUrl { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsDiscounted { get; set; }
    public int? DiscountID { get; set; }
    public string RelatedCategoryIDs { get; set; }
    public int ProductCount { get; set; }
    public string MetaTitle { get; set; }
    public string MetaDescription { get; set; }
    public string Tags { get; set; }
    public string LanguageCode { get; set; }

    // Make nullable
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    public string AccessRoles { get; set; }
    public string AvailabilitySchedule { get; set; }
    public bool ShowInFooter { get; set; }
    public string LayoutType { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string BannerImageUrl { get; set; }
}
