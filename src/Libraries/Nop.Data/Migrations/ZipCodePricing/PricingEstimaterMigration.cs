using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Mapping;
using Nop.Data.Extensions;
using Nop.Core.Domain.ZipcodeSelling;

namespace Nop.Data.Migrations
{
    [NopMigration("2022/05/19 11:26:08:9037699", "Adding PricingEstimater pricing to ZipcodeSelling", UpdateMigrationType.Data, MigrationProcessType.Update)]
    public class PricingEstimaterMigration : AutoReversingMigration
    {

        #region Methods

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(PriceEstimater))).Exists())
                Create.TableFor<PriceEstimater>();


            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(PriceEstimater))).Column(nameof(PriceEstimater.CategoryType)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(PriceEstimater)))
            //        .AddColumn(nameof(PriceEstimater.CategoryType)).AsString().Nullable();
            //}


            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(PriceEstimater))).Column(nameof(PriceEstimater.PricingType)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(PriceEstimater)))
            //        .AddColumn(nameof(PriceEstimater.PricingType)).AsString().Nullable();
            //}
            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(PriceEstimater))).Column(nameof(PriceEstimater.CategoryName)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(PriceEstimater)))
            //        .AddColumn(nameof(PriceEstimater.CategoryName)).AsString().Nullable();
            //}


            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(PriceEstimater))).Column(nameof(PriceEstimater.Pricing)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(PriceEstimater)))
            //        .AddColumn(nameof(PriceEstimater.Pricing)).AsFloat().Nullable();
            //}

        }

        #endregion
    }
}
