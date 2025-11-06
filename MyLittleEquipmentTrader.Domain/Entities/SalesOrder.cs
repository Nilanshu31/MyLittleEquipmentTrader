using System;

namespace MyLittleEquipmentTrader.Domain.Entities
{
    public class SalesOrder
    {
        public int SalesOrderId { get; set; }

        // Required foreign keys & navigation
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public int CustomerId { get; set; }
        //public Customer Customer { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int SKU { get; set; } // You may want to adjust SKU as per your product catalog

        // Order Details
        public string OrderNumber { get; set; }  // Unique Order Number

        public decimal SubtotalAmount { get; set; } // Before taxes, discounts, etc.
        public decimal DiscountAmount { get; set; } // Discount applied
        public decimal TaxAmount { get; set; } // Tax calculated
        public decimal TotalAmount { get; set; } // After all adjustments (subtotal + tax - discount)

        public decimal ShippingAmount { get; set; } // Shipping charges
        public decimal PaymentProcessingFee { get; set; } // Payment gateway fee

        public string Currency { get; set; } // Currency of the order (USD, EUR, etc.)
        public string PaymentMethod { get; set; } // E.g., "Credit Card", "PayPal"

        public string CouponCode { get; set; } // Coupon code used
        public int? CouponId { get; set; } // Link to coupon, nullable if no coupon used

        public bool IsSubscriptionOrder { get; set; } // True if this order is a subscription order
        public int? SubscriptionId { get; set; } // Foreign Key for Subscription, nullable

        public DateTime OrderDate { get; set; } // Date when the order was placed
        public DateTime? FulfillmentDate { get; set; } // Date when the order is fulfilled (nullable if pending)

        public string FulfillmentStatus { get; set; } // Pending, Shipped, Delivered, etc.

        public string OrderNotes { get; set; } // Any additional notes regarding the order

        public bool IsGift { get; set; } // Flag to check if this order is a gift
        public bool IsArchived { get; set; } // Flag for archival status
        public bool IsDeleted { get; set; } // Flag to mark the order as deleted (soft delete)

        // Shipping Details
        public string ShippingAddress { get; set; } // Full address for delivery
        public string ShippingMethod { get; set; } // E.g., Standard, Express, etc.
        public string ShippingTrackingNumber { get; set; } // Tracking number for shipped order

        // Customer / Order Tracking
        public int? AffiliateId { get; set; } // Optional affiliate link, if applicable
        public string Device { get; set; } // The device from which the order was placed (e.g., "Mobile", "Desktop")

        // Payment & Delivery
        public bool IsPaid { get; set; } // Status to check if payment has been completed
        public DateTime? PaidDate { get; set; } // Date the order was paid
        public bool IsRefunded { get; set; } // Flag to mark if the order has been refunded
        public DateTime? RefundDate { get; set; } // Date the order was refunded

        // Tracking Fields (Audit)
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
