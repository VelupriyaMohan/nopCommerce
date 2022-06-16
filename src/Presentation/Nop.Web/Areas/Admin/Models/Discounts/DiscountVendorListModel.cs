using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts
{
    /// <summary>
    /// Represents a discount vendor list model
    /// </summary>
    public partial record DiscountVendorListModel : BasePagedListModel<DiscountVendorModel>
    {
    }
}