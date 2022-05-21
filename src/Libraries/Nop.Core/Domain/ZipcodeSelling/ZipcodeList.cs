namespace Nop.Core.Domain.ZipcodeSelling
{
    /// <summary>
    /// Represents a vendor
    /// </summary>
    public partial class ZipcodeList : BaseEntity
    {

        public string StateCircleName { get; set; }
        public string CityRegionName { get; set; }
        public string DivisionName { get; set; }
        public string OfficeName { get; set; }
        public string PostalZipcode { get; set; }
        public string StateName { get; set; }
        public string District { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
}
