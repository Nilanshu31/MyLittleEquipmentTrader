// Application/Models/Dtos/ProductAttributeDto.cs
using System;

namespace MyLittleEquipmentTrader.Application.Models.Dtos
{
    public class ProductAttributeDto
    {
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public string AttributeType { get; set; }
        public bool IsRequired { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string AttributeGroup { get; set; }
    }
}
