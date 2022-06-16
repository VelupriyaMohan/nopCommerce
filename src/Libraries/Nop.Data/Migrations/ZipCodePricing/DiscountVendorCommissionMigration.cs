using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Mapping;
using Nop.Data.Extensions;
using Nop.Core.Domain.Vendors;
using Nop.Core.Domain.Discounts;

namespace Nop.Data.Migrations
{
    [NopMigration("2022/06/08 12:26:08:9037999", "Adding vendor pricing to discount module", UpdateMigrationType.Data, MigrationProcessType.Update)]
    public class DiscountVendorCommissionMigration : AutoReversingMigration
    {

        #region Methods

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(DiscountVendorMapping))).Exists())
                Create.TableFor<DiscountVendorMapping>();


          
        }

        //public override void Down()
        //{
        //    //add the downgrade logic if necessary 
        //}

        #endregion
    }
}
