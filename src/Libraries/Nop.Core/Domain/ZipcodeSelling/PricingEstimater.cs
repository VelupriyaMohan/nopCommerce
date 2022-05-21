namespace Nop.Core.Domain.ZipcodeSelling
{
    /// <summary>
    /// Represents a vendor
    /// </summary>
    public partial class PriceEstimater : BaseEntity
    {
        public string CategoryType  { get; set; }
        public string PricingType { get; set; }
        public string CategoryName { get; set; }
        public float Pricing { get; set; }

    }

    
}
