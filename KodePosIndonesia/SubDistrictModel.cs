namespace KodePosIndonesia
{
    /// <summary>
    /// Desa
    /// </summary>
    public class SubDistrictModel : BaseModel
    {
        internal static string CollectionName = "SubDistricts";
        public int DistrictId { get; set; }
        public int PostalCode { get; set; }
    }
}
