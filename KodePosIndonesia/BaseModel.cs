using System.Security.Principal;

namespace KodePosIndonesia
{
    public class BaseModel
    {
        //private static string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        //private static string basePath = @"contentFiles";
        //internal static string ProvinceFilePath = Path.Combine(baseDir, basePath, "province.csv");
        //internal static string CityFilePath = Path.Combine(baseDir, basePath, "city.csv");
        //internal static string DistrictFilePath = Path.Combine(baseDir, basePath, "district.csv");
        //internal static string SubDistrictFilePath = Path.Combine(baseDir, basePath, "subdistrict.csv");
        //internal static string PostalCodeFilePath = Path.Combine(baseDir, basePath, "postal_code.csv");

        internal static string ProvinceFilePath = "KodePosIndonesia.contentFiles.province.csv";
        internal static string CityFilePath = "KodePosIndonesia.contentFiles.city.csv";
        internal static string DistrictFilePath = "KodePosIndonesia.contentFiles.district.csv";
        internal static string SubDistrictFilePath = "KodePosIndonesia.contentFiles.subdistrict.csv";
        internal static string PostalCodeFilePath = "KodePosIndonesia.contentFiles.postal_code.csv";

        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
