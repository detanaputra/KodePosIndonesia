namespace KodePosIndonesia
{
    public class PostalCodeModel : BaseModel
    {
        public string? SubDistrict { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public int PostalCode { get; set; }
    }
}