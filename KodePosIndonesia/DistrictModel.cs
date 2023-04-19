namespace KodePosIndonesia
{
    /// <summary>
    /// Kecamatan
    /// </summary>
    public class DistrictModel : BaseModel
    {
        internal static string CollectionName = "Districts";
        public int CityId { get; set; }
    }
}