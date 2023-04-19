using KodePosIndonesia;

namespace KodePosIndonesiaTest
{
    public class DistrictTest
    {
        [Test]
        public async Task GetAsync()
        {
            using KodePos kodePos = new();
            IEnumerable<DistrictModel> districts = await kodePos.DistrictRepository.GetAsync(3);
            Assert.That(districts, Is.Not.Null);
            Assert.That(districts.Count(), Is.EqualTo(10));
        }

        [Test]
        public async Task GetSingleAsync()
        {
            using KodePos kodePos = new();
            DistrictModel district = await kodePos.DistrictRepository.GetSingleAsync("36b404a3-2461-4077-8cf8-13674d1e9b11");
            Assert.That(district, Is.Not.Null);
            Assert.That(district.Name, Is.EqualTo("PASAR MINGGU"));
        }
    }
}