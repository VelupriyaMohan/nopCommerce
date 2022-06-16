namespace Nop.Core.Domain.Discounts
{
    /// <summary>
    /// Represents a discount-vendor mapping class
    /// </summary>
    public partial class DiscountVendorMapping : DiscountMapping
    {
        /// <summary>
        /// Gets or sets the vendor identifier
        /// </summary>
        public override int EntityId { get; set; }
    }
}