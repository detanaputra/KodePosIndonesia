using CsvHelper.Configuration;

using KodePosIndonesia.FirebaseHttpClients;

using Microsoft.Extensions.DependencyInjection;

using System.Globalization;
using System.Net.Http;

namespace KodePosIndonesia
{
    public enum Source { Sqlite, Csv, Firebase}

    public class KodePos : IDisposable
    {
        public Source Source { get; set; } = Source.Firebase;
        
        private readonly CsvConfiguration configuration;
        private KodePosContext? kodePosContext;
        private bool disposed = false;
        private HttpClientFactory httpClientFactory;

        public IRepository<ProvinceModel> ProvinceRepository
        {
            get
            {
                if (provinceRepository == null)
                {
                    switch (Source)
                    {
                        case Source.Sqlite:
                            provinceRepository ??= new SQLiteRepository<ProvinceModel>(kodePosContext.Provinces);
                            break;
                        case Source.Csv:
                            provinceRepository ??= new LocalDataRepository<ProvinceModel>(configuration, BaseModel.ProvinceFilePath);
                            break;
                        case Source.Firebase:
                            provinceRepository ??= new FirebaseRepository<ProvinceModel>(httpClientFactory.CreateClient<IProvinceClient>());
                            break;
                        default:
                            break;
                    }
                }
                return provinceRepository;
            }
        }
        private IRepository<ProvinceModel> provinceRepository;

        public IRepository<CityModel> CityRepository
        {
            get
            {
                if (cityRepository == null)
                {
                    if (Source == Source.Sqlite)
                    {
                        cityRepository ??= new SQLiteRepository<CityModel>(kodePosContext.Cities);
                    }
                    else
                    {
                        cityRepository ??= new LocalDataRepository<CityModel>(configuration, BaseModel.CityFilePath);
                    }
                }
                return cityRepository;
            }
        }
        private IRepository<CityModel> cityRepository;

        private IRepository<DistrictModel> districtRepository;
        public IRepository<DistrictModel> DistrictRepository
        {
            get
            {
                if (districtRepository == null)
                {
                    if (Source == Source.Sqlite)
                    {
                        districtRepository ??= new SQLiteRepository<DistrictModel>(kodePosContext.Districts);
                    }
                    else
                    {
                        districtRepository ??= new LocalDataRepository<DistrictModel>(configuration, BaseModel.DistrictFilePath);
                    }
                }
                return districtRepository;
            }
        }

        private IRepository<SubDistrictModel> subDistrictRepository;
        public IRepository<SubDistrictModel> SubDistrictRepository
        {
            get
            {
                if (subDistrictRepository == null)
                {
                    if (Source == Source.Sqlite)
                    {
                        subDistrictRepository ??= new SQLiteRepository<SubDistrictModel>(kodePosContext.SubDistricts);
                    }
                    else
                    {
                        subDistrictRepository ??= new LocalDataRepository<SubDistrictModel>(configuration, BaseModel.SubDistrictFilePath);
                    }
                }
                return subDistrictRepository;
            }
        }

        private IRepository<PostalCodeModel> postalCodeRepository;
        public IRepository<PostalCodeModel> PostalCodeRepository
        {
            get
            {
                if (postalCodeRepository == null)
                {
                    if (Source == Source.Sqlite)
                    {
                        postalCodeRepository ??= new SQLiteRepository<PostalCodeModel>(kodePosContext.PostalCodes);
                    }
                    else
                    {
                        postalCodeRepository ??= new LocalDataRepository<PostalCodeModel>(configuration, BaseModel.SubDistrictFilePath);
                    }
                }
                return postalCodeRepository;
            }
        }

        public KodePos()
        {
            this.configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
            switch (Source)
            {
                case Source.Sqlite:
                    this.kodePosContext = new KodePosContext();
                    break;
                case Source.Csv:
                    break;
                case Source.Firebase:
                    ServiceCollection serviceCollection = new();
                    ConfigureService(serviceCollection);
                    ServiceProvider services = serviceCollection.BuildServiceProvider();
                    httpClientFactory = services.GetRequiredService<HttpClientFactory>();
                    break;
                default:
                    break;
            }
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
            if(!disposed)
            {
                if (disposing)
                {
                    if (kodePosContext != null)
                    {
                        kodePosContext.Dispose();
                        kodePosContext = null;
                    }
                }

                disposed = true;
            }
        }

        private void ConfigureService(ServiceCollection services)
        {
            //services.AddHttpClient();
            //services.AddHttpClient(nameof(ProvinceModel), options =>
            //{
            //    options.BaseAddress = new Uri("https://kodepos-c2535-default-rtdb.asia-southeast1.firebasedatabase.app/Provinces");
            //});

            services.AddHttpClient<IProvinceClient, ProvinceClient<ProvinceModel>>();

        }
    }
}