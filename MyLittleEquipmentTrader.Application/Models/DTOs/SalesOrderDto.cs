using MyLittleEquipmentTrader.Application.Models.Dtos;
using System;

namespace MyLittleEquipmentTrader.Application.Models.Dtos
{
    public class SalesOrderDto
    {
        public int SalesOrderId { get; set; }

        // Relationships (Foreign Keys)
        public int TenantId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int? SubscriptionId { get; set; } // Nullable if not a subscription order

        // Core Order Information
        public string OrderNumber { get; set; }  // Unique Order Number
        public decimal SubtotalAmount { get; set; } // Before taxes, discounts, etc.
        public decimal DiscountAmount { get; set; } // Discount applied
        public decimal TaxAmount { get; set; } // Tax calculated
        public decimal TotalAmount { get; set; } // After all adjustments (subtotal + tax - discount)
        public decimal ShippingAmount { get; set; } // Shipping charges
        public decimal PaymentProcessingFee { get; set; } // Payment gateway fee

        // Payment & Currency Information
        public string Currency { get; set; } // Currency of the order (USD, EUR, etc.)
        public string PaymentMethod { get; set; } // E.g., "Credit Card", "PayPal"

        // Optional Information
        public string CouponCode { get; set; } // Coupon code used
        public int? CouponId { get; set; } // Nullable, as not all orders will have a coupon
        public bool IsGift { get; set; } // Flag to check if this order is a gift
        public bool IsArchived { get; set; } // Flag for archival status
        public bool IsDeleted { get; set; } // Flag to mark the order as deleted (soft delete)

        // Shipping Details
        public string ShippingAddress { get; set; } // Shipping address
        public string ShippingMethod { get; set; } // E.g., "Standard", "Express"
        public string ShippingTrackingNumber { get; set; } // Tracking number for shipped order

        // Customer / Order Tracking
        public int? AffiliateId { get; set; } // Optional affiliate link, if applicable
        public string Device { get; set; } // The device from which the order was placed (e.g., "Mobile", "Desktop")

        // Payment & Fulfillment Status
        public bool IsPaid { get; set; } // Status to check if payment has been completed
        public DateTime? PaidDate { get; set; } // Date the order was paid
        public bool IsRefunded { get; set; } // Flag to mark if the order has been refunded
        public DateTime? RefundDate { get; set; } // Date the order was refunded

        public string FulfillmentStatus { get; set; } // E.g., "Pending", "Shipped", "Delivered"
        public DateTime? FulfillmentDate { get; set; } // Date when the order is fulfilled (nullable if pending)

        // Additional Notes
        public string OrderNotes { get; set; } // Any additional notes regarding the order

        // Tracking Fields (Audit)
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relationships (DTOs)
        public TenantDto Tenant { get; set; } // Tenant associated with the order
        //public CustomerDto Customer { get; set; } // Customer associated with the order
        public ProductDto Product { get; set; } // Product in the order
        public SubscriptionDto Subscription { get; set; } // Subscription (if applicable)
    }
}
