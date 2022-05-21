using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Data.Mapping;
using Nop.Data.Extensions;
using Nop.Core.Domain.ZipcodeSelling;

namespace Nop.Data.Migrations
{
    [NopMigration("2022/05/19 11:26:08:9037689", "Adding Zipcode Lists to ZipcodePricing", UpdateMigrationType.Data, MigrationProcessType.Update)]
    public class ZipcodeListMigration : AutoReversingMigration
    {

        #region Methods

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList))).Exists())
                Create.TableFor<ZipcodeList>();




            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList))).Column(nameof(ZipcodeList.StateCircleName)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList)))
            //        .AddColumn(nameof(ZipcodeList.StateCircleName)).AsString().Nullable();
            //}


            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList))).Column(nameof(ZipcodeList.CityRegionName)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList)))
            //        .AddColumn(nameof(ZipcodeList.CityRegionName)).AsString().Nullable();
            //}

            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList))).Column(nameof(ZipcodeList.DivisionName)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList)))
            //        .AddColumn(nameof(ZipcodeList.DivisionName)).AsString().Nullable();
            //}

            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList))).Column(nameof(ZipcodeList.OfficeName)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList)))
            //        .AddColumn(nameof(ZipcodeList.OfficeName)).AsString().Nullable();
            //}

            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList))).Column(nameof(ZipcodeList.PostalZipcode)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList)))
            //        .AddColumn(nameof(ZipcodeList.PostalZipcode)).AsString().Nullable();
            //}

            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList))).Column(nameof(ZipcodeList.StateName)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList)))
            //        .AddColumn(nameof(ZipcodeList.StateName)).AsString().Nullable();
            //}

            //if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList))).Column(nameof(ZipcodeList.District)).Exists())
            //{
            //    //add new column
            //    Alter.Table(NameCompatibilityManager.GetTableName(typeof(ZipcodeList)))
            //        .AddColumn(nameof(ZipcodeList.District)).AsString().Nullable();
            //}

        }

        #endregion
    }
}
