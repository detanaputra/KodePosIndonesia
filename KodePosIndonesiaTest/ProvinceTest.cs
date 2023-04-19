using KodePosIndonesia;

using System.Diagnostics;

namespace KodePosIndonesiaTest
{
    public class ProvinceTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAsync()
        {
            using KodePos kodePos = new();
            IEnumerable<ProvinceModel> provinceModels = await kodePos.ProvinceRepository.GetAsync();
            foreach (ProvinceModel a in provinceModels)
            {
                Debug.WriteLine($"{a.Id} : {a.Name}");
            }
            Assert.That(provinceModels, Is.Not.Null);
            Assert.That(provinceModels.Count(), Is.EqualTo(34));
        }

        [Test]
        public async Task GetSingleAsync()
        {
            using KodePos kodePos = new();
            ProvinceModel a = await kodePos.ProvinceRepository.GetSingleAsync("27a85166-ff53-480d-afab-c560aa5bc4d5");
            Assert.That(a, Is.Not.Null);
            Assert.That(a.Name, Is.EqualTo("DKI JAKARTA"));
        }
    }
}