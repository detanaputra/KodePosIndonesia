using KodePosIndonesia;

namespace KodePosIndonesiaTest
{
    public class SubDistrictTest
    {
        [Test]
        public async Task GetAsync()
        {
            using KodePos kodePos = new();
            IEnumerable<SubDistrictModel> subDistricts = await kodePos.SubDistrictRepository.GetAsync(21);
            Assert.That(subDistricts, Is.Not.Null);
            Assert.That(subDistricts.Count(), Is.EqualTo(7));
        }

        [Test]
        public async Task GetSingleAsync()
        {
            using KodePos kodePos = new();
            SubDistrictModel subDistrict = await kodePos.SubDistrictRepository.GetSingleAsync("e93fc7a2-8fa9-4439-8794-44cd01717ede");
            Assert.That(subDistrict, Is.Not.Null);
            Assert.That(subDistrict.Name, Is.EqualTo("KEBAGUSAN"));
        }
    }
}
