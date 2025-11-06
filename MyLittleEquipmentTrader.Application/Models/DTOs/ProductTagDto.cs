namespace MyLittleEquipmentTrader.Application.Models.Dtos
{
    public class ProductTagDto
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string Subcategory { get; set; }
        public string TagIcon { get; set; }
        public string TagColor { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsActive { get; set; }
        public string Priority { get; set; }
        public int UserCount { get; set; }
        public string TagType { get; set; }
        public string Visibility { get; set; }
    }
}
