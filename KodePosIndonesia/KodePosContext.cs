using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace KodePosIndonesia
{
    public class KodePosContext : DbContext
    {
        internal DbSet<ProvinceModel> Provinces { get; set; }
        internal DbSet<CityModel> Cities { get; set; }
        internal DbSet<DistrictModel> Districts { get; set; }
        internal DbSet<SubDistrictModel> SubDistricts { get; set; }
        internal DbSet<PostalCodeModel> PostalCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Assembly assembly = typeof(KodePosContext).Assembly;
            string connectionString = "Data Source=database.db";
            Stream? stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.database.db");

            SqliteConnection connection = new(connectionString);
            connection.Open();

            optionsBuilder.UseSqlite(connection);

            // Uncomment the following line if you want to use the database in-memory
            //optionsBuilder.UseSqlite("Data Source=:memory:;Mode=Memory;Cache=Shared");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProvinceModel>().ToTable(nameof(Provinces));
            modelBuilder.Entity<CityModel>().ToTable(nameof(Cities));
            modelBuilder.Entity<DistrictModel>().ToTable(nameof(Districts));
            modelBuilder.Entity<SubDistrictModel>().ToTable(nameof(SubDistricts));
            modelBuilder.Entity<PostalCodeModel>().ToView("VAll");
            base.OnModelCreating(modelBuilder);
        }
    }
}
