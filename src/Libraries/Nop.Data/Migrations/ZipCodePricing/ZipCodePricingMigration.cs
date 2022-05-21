using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Mapping;
using Nop.Data.Extensions;
using Nop.Core.Domain.Vendors;

namespace Nop.Data.Migrations
{
    [NopMigration("2022/05/07 11:26:08:9037680", "Adding Zipcode pricing to vendor", UpdateMigrationType.Data, MigrationProcessType.Update)]
    public class ZipCodePricingMigration : AutoReversingMigration
    {

        #region Methods

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Exists())
                Create.TableFor<Vendor>();


            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.Latitude)).Exists())
            {
                //add new column
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.Latitude)).AsDouble().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.Longitude)).Exists())
            {
                //add new column
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.Longitude)).AsDouble().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.IsWholeCitySupply)).Exists())
            {
                //add new column
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.IsWholeCitySupply)).AsBoolean().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.Citys)).Exists())
            {
                //add new column
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.Citys)).AsString().Nullable();
            }


            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.ZipCodes)).Exists())
            {
                //add new column
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.ZipCodes)).AsString().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.CityFromPrice)).Exists())
            {
                //add new column
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.CityFromPrice)).AsFloat().Nullable();
            }


            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.CityFromWeight)).Exists())
            {
                //add new column
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.CityFromWeight)).AsFloat().Nullable();
            }


            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.CityUptoPrice)).Exists())
            {
                //add new column
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.CityUptoPrice)).AsFloat().Nullable();
            }


            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.CityUptoWeight)).Exists())
            {
                //add new column
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.CityUptoWeight)).AsFloat().Nullable();
            }

        }

        //public override void Down()
        //{
        //    //add the downgrade logic if necessary 
        //}

        #endregion
    }
}
