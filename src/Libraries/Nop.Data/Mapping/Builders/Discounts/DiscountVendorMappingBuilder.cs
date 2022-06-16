using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Vendors;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Discounts
{
    /// <summary>
    /// Represents a discount vendor mapping entity builder
    /// </summary>
    public partial class DiscountVendorMappingBuilder : NopEntityBuilder<DiscountVendorMapping>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DiscountVendorMapping), nameof(DiscountVendorMapping.DiscountId)))
                    .AsInt32().PrimaryKey().ForeignKey<Discount>()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DiscountVendorMapping), nameof(DiscountVendorMapping.EntityId)))
                    .AsInt32().PrimaryKey().ForeignKey<Vendor>();
        }

        #endregion
    }
}