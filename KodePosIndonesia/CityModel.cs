namespace KodePosIndonesia
{
    /// <summary>
    /// Kota/Kabupaten
    /// </summary>
    public class CityModel : BaseModel
    {
        internal static string CollectionName = "Cities";
        public int ProvinceId { get; set; }
    }
}