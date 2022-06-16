using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Areas.Admin.Models.Vendors;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts
{
    /// <summary>
    /// Represents a vendor list model to add to the discount
    /// </summary>
    public partial record AddVendorToDiscountListModel : BasePagedListModel<VendorModel>
    {
    }
}