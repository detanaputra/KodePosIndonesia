namespace KodePosIndonesia
{
    public interface IKodePos
    {
        IRepository<CityModel> CityRepository { get; }
        IRepository<DistrictModel> DistrictRepository { get; }
        IRepository<ProvinceModel> ProvinceRepository { get; }
        IRepository<SubDistrictModel> SubDistrictRepository { get; }

        void Dispose();
    }
}