using KodePosIndonesia;
using KodePosIndonesia.Extensions;

namespace KodePosIndonesiaTest
{
    public class AddressTest
    {
        [Test]
        public async Task GetAsync()
        {
            using KodePos kodePos = new();
            SubDistrictModel subDistrict = await kodePos.SubDistrictRepository.GetSingleAsync("e93fc7a2-8fa9-4439-8794-44cd01717ede");
            AddressModel address = await subDistrict.GetCompleteAddress();
            
            Assert.Multiple(() => 
            {
                Assert.That(address, Is.Not.Null);
                Assert.That(address.Province, Is.Not.Null);
                Assert.That(address.City, Is.Not.Null);
                Assert.That(address.District, Is.Not.Null);
                Assert.That(address.SubDistrict, Is.Not.Null);
                Assert.That(address.Name, Is.EqualTo("KEBAGUSAN"));
                Assert.That(address.District.ToString(), Is.EqualTo("PASAR MINGGU"));
                Assert.That(address.City.ToString(), Is.EqualTo("JAKARTA SELATAN"));
                Assert.That(address.Province.ToString(), Is.EqualTo("DKI JAKARTA"));
            });
        }
    }
}
