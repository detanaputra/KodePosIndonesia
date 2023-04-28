using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics;

namespace KodePosIndonesia.Extensions
{
    public static class SubDistrictModelExtension
    {
        public static async Task<AddressModel> GetCompleteAddress(this SubDistrictModel subDistrict)
        {
            IHttpClientFactory httpClientFactory = KodePos.ServiceProvider.GetRequiredService<IHttpClientFactory>();
            IRepository<DistrictModel> districts = new FirebaseRepository<DistrictModel>(httpClientFactory.CreateClient(nameof(DistrictModel)), DistrictIndex.Id.ToString());
            IRepository<CityModel> cities = new FirebaseRepository<CityModel>(httpClientFactory.CreateClient(nameof(CityModel)), CityIndex.Id.ToString());
            IRepository<ProvinceModel> provinces = new FirebaseRepository<ProvinceModel>(httpClientFactory.CreateClient(nameof(ProvinceModel)), ProvinceIndex.Id.ToString());
            AddressModel postalCode = new AddressModel { SubDistrict = subDistrict };
            postalCode.District = await districts.GetSingleAsync(subDistrict.DistrictId);
            postalCode.City = await cities.GetSingleAsync(postalCode.District.CityId);
            postalCode.Province = await provinces.GetSingleAsync(postalCode.City.ProvinceId);

            Debug.WriteLine(postalCode);
            return postalCode;
        }
    }
}