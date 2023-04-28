using Microsoft.Extensions.DependencyInjection;

namespace KodePosIndonesia
{
    public class KodePos : IDisposable, IKodePos
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        private bool disposed = false;
        private IHttpClientFactory httpClientFactory;

        public IRepository<ProvinceModel> ProvinceRepository
        {
            get
            {
                provinceRepository ??= new FirebaseRepository<ProvinceModel>(httpClientFactory.CreateClient(nameof(ProvinceModel)), ProvinceIndex.Id.ToString());
                return provinceRepository;
            }
        }
        private IRepository<ProvinceModel>? provinceRepository;

        public IRepository<CityModel> CityRepository
        {
            get
            {
                cityRepository ??= new FirebaseRepository<CityModel>(httpClientFactory.CreateClient(nameof(CityModel)), CityIndex.ProvinceId.ToString());
                return cityRepository;
            }
        }
        private IRepository<CityModel>? cityRepository;

        public IRepository<DistrictModel> DistrictRepository
        {
            get
            {
                districtRepository ??= new FirebaseRepository<DistrictModel>(httpClientFactory.CreateClient(nameof(DistrictModel)), DistrictIndex.CityId.ToString());
                return districtRepository;
            }
        }
        private IRepository<DistrictModel>? districtRepository;

        public IRepository<SubDistrictModel> SubDistrictRepository
        {
            get
            {
                subDistrictRepository ??= new FirebaseRepository<SubDistrictModel>(httpClientFactory.CreateClient(nameof(SubDistrictModel)), SubDistrictIndex.DistrictId.ToString());
                return subDistrictRepository;
            }
        }
        private IRepository<SubDistrictModel>? subDistrictRepository;

        public KodePos()
        {
            ServiceCollection serviceCollection = new();
            ConfigureService(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            httpClientFactory = ServiceProvider.GetRequiredService<IHttpClientFactory>();
        }

        ~KodePos()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    provinceRepository?.Dispose();
                    cityRepository?.Dispose();
                    districtRepository?.Dispose();
                    subDistrictRepository?.Dispose();
                }

                disposed = true;
            }
        }

        private void ConfigureService(ServiceCollection services)
        {
            services.AddHttpClient(nameof(ProvinceModel), options =>
            {
                options.BaseAddress = new Uri("https://kodepos-c2535-default-rtdb.asia-southeast1.firebasedatabase.app/Provinces/");

            });
            services.AddHttpClient(nameof(CityModel), options =>
            {
                options.BaseAddress = new Uri("https://kodepos-c2535-default-rtdb.asia-southeast1.firebasedatabase.app/Cities/");
            });
            services.AddHttpClient(nameof(DistrictModel), options =>
            {
                options.BaseAddress = new Uri("https://kodepos-c2535-default-rtdb.asia-southeast1.firebasedatabase.app/Districts/");
            });
            services.AddHttpClient(nameof(SubDistrictModel), options =>
            {
                options.BaseAddress = new Uri("https://kodepos-c2535-default-rtdb.asia-southeast1.firebasedatabase.app/SubDistricts/");
            });
        }
    }
}