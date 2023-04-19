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
        public async Task GetAllProvince()
        {
            KodePos kodePos = new();
            await foreach(ProvinceModel a in kodePos.ProvinceRepository.GetAllAsync())
            {
                Debug.WriteLine($"{a.Id} : {a.Name}");
            }
            Assert.Pass();
        }

        [Test]
        public async Task GetProvinceById()
        {
            KodePos kodePos = new();
            ProvinceModel a = await kodePos.ProvinceRepository.GetByIdAsync(22);
            if (a != null) 
            {
                if(a.Name == "BALI")
                {
                    Assert.That(true);
                }
                else
                {
                    Assert.Fail(@"Province Name is not Bali, but " + a.Name);
                }
            }
            else
            {
                Assert.Fail("Fail, a is null");
            }
        }

        [Test]
        public async Task CountProvince()
        {
            KodePos kodePos = new();
            IEnumerable<ProvinceModel> a = await kodePos.ProvinceRepository.GetAllPolledAsync();
            if(a.Count() == 34)
            {
                Assert.That(true);
            }
            else
            {
                Assert.Fail("Records count is not 34");
            }
        }
    }
}