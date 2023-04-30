namespace KodePosIndonesia
{
    public class AddressModel : BaseModel
    {
        public SubDistrictModel SubDistrict { get; set; }
        public DistrictModel District { get; set; }
        public CityModel City { get; set; }
        public ProvinceModel Province { get; set; }

        public int PostalCode
        {
            get => SubDistrict.PostalCode;
        }
        public override string Name { get => SubDistrict.Name; }
        public override int Id { get => SubDistrict.Id;}

        public override string ToString()
        {
            return $"{PostalCode}, {Name}, {District.ToString()}, {City.ToString()}, {Province.ToString()}";
        }
    }
}