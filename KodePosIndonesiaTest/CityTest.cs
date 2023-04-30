using KodePosIndonesia;

namespace KodePosIndonesiaTest
{
    public class CityTest
    {
        [Test]
        public async Task GetAsync()
        {
            using KodePos kodePos = new();
            IEnumerable<CityModel> cities = await kodePos.CityRepository.GetAsync(1);
            Assert.That(cities, Is.Not.Null);
            Assert.That(cities.Count(), Is.EqualTo(6));
        }

        [Test]
        public async Task GetAsyncNull()
        {
            using KodePos kodePos = new();
            IEnumerable<CityModel> cities = await kodePos.CityRepository.GetAsync(-1);
            Assert.That(cities, Is.Null);
        }

        [Test]
        public async Task GetSingleAsync()
        {
            using KodePos kodePos = new();
            CityModel city = await kodePos.CityRepository.GetSingleAsync("bcfb099c-b931-4128-9933-fea5b4615cf5");
            Assert.That(city, Is.Not.Null);
            Assert.That(city.Name, Is.EqualTo("JAKARTA SELATAN"));
        }

        [Test]
        public async Task GetSingleAsyncNull()
        {
            using KodePos kodePos = new();
            // trigger not found
            CityModel city = await kodePos.CityRepository.GetSingleAsync("nullvalue");
            Assert.That(city, Is.Null);
        }
    }
}
