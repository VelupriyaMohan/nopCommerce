using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Discounts
{
    /// <summary>
    /// Represents a Vendor search model to add to the discount
    /// </summary>
    public partial record AddVendorToDiscountSearchModel : BaseSearchModel
    {
        #region Properties

        [NopResourceDisplayName("Admin.Catalog.Vendors.List.SearchVendorName")]
        public string SearchVendorName { get; set; }

        #endregion
    }
}