namespace KodePosIndonesia
{
    public enum ProvinceIndex { Id }
    public enum CityIndex { Id, ProvinceId }
    public enum DistrictIndex { Id, CityId}
    public enum SubDistrictIndex { Id, DistrictId, PostalCode }
}