using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts
{
    /// <summary>
    /// Represents a discount Vendor model
    /// </summary>
    public partial record DiscountVendorModel : BaseNopEntityModel
    {
        #region Properties

        public int VendorId { get; set; }

        public string VendorName { get; set; }

        #endregion
    }
}