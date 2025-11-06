using System;
using System.Collections.Generic;

namespace MyLittleEquipmentTrader.Application.Models
{
    public class ProductFilterRequest
    {
        public List<FilterCriteria> Filters { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public string SortOrder { get; set; } = "asc";
    }

    public class FilterCriteria
    {
        public string Field { get; set; }   // Field to filter (e.g., "Price", "ProductName")
        public string Operator { get; set; } // Operator like 'eq', 'lt', 'gt', etc.
        public string Value { get; set; }    // The value to filter by (e.g., "5000", "Samsung")
        public string LogicalOperator { get; set; } // Logical operator to combine filters (e.g., "AND", "OR")
    }
}
