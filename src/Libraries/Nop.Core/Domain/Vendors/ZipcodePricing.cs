namespace Nop.Core.Domain.Customers
{
    /// <summary>
    /// Represents a customer-address mapping class
    /// </summary>
    public partial class ZipcodeMapping : BaseEntity
    {
        public int ZipcodeId { get; set; }
        public int Zipcode { get; set; }
        public string District { get; set; }
        public string StateName { get; set; }
        public string Country { get; set; }

    }
}