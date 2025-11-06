using System.Collections.Generic;

namespace MyLittleEquipmentTrader.Application.Models.Dtos
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string SKU { get; set; }
        public string Slug { get; set; }
        public string ThumbnailURL { get; set; }

        public ICollection<ProductAttributeDto> ProductAttributes { get; set; }

        // Change this type to DTO collection
        public ICollection<ProductTagDto> ProductTags { get; set; }
    }
}
