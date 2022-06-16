using System.Collections.Generic;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Discounts
{
    /// <summary>
    /// Represents a vendor model to add to the discount
    /// </summary>
    public partial record AddVendorToDiscountModel : BaseNopModel
    {
        #region Ctor

        public AddVendorToDiscountModel()
        {
            SelectedVendorIds = new List<int>();
        }
        #endregion

        #region Properties

        public int DiscountId { get; set; }

        public IList<int> SelectedVendorIds { get; set; }

        #endregion
    }
}