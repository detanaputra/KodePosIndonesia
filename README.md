# KodePosIndonesia
## General information

This is a library that provides the list of Province (Propinsi), City (Kota/Kabupaten), District (Kecamatan), SubDistrict (Kelurahan/Desa) and its Postal Code. Initially, I wrote it to assist many of my personal project. This library points to my free plan Firebase Realtime Database, hence need an internet connection. Since I made this in my free time only, further development may be uncertain.

## Disclaimer

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## How to Use
### Instancing
There are 2 ways of instancing the unit of work. You can choose the one that suit your need.
1. Using dependency injection (recommended)

    1. Register the interface and concrete class as a service
    ```c#
    using KodePosIndonesia;
    // bunch of codes here

    builder.Services.AddScoped<IKodePos, KodePos>();
    
    ```
    2. Resolve the injected interface, 
    The way to resolve depends on what framework you are working. You might need to modify the code below accordingly. This example below is for Blazor .Net 6 project.
    ```c#
    using KodePosIndonesia;
    // bunch of codes here

    @inject IKodePos kodePos;
    ```

2. Using a `using` statement
    ```c#
    // import namespace
    using KodePosIndonesia;
    // bunch of codes here

    // instancing KodePos object
    using KodePos kodePos = new();
    // using API. See the section below.


    // or if you're using older version of .Net
    // instancing KodePos object.
    using (KodePos kodePos = new KodePos())
    {
        // using API. See the section below.
    }
    ```


### Using the API

Your application should provides the mechanism for user to select a model and then store it to a field or property. in this example, I will use these fields to store user selected model.
```c#
private ProvinceModel selectedProvince;
private CityModel selectedCity;
private DistrictModel selectedDistrict;
private SubDistrictModel selectedSubDistrict;
```

Get all provinces.
```c#
IEnumerable<ProvinceModel> provinceList = await kodePos.ProvinceRepository.GetAsync();   
```
After you get the province list, you should execute the user selecting mechanism and store it to a field.

Get the Cities based on selected province.
```c#
IEnumerable<CityModel> cityList = await kodePos.CityRepository.GetAsync(selectedProvince.Id);
```
Get the Districts based on selected city.
```c#
IEnumerable<DistrictModel> districtList = await kodePos.DistrictRepository.GetAsync(selectedCity.Id);
```
Get the SubDistricts based on selected district.
```c#
IEnumerable<SubDistrictModel> subDistrictList = await kodePos.SubDistrictRepository.GetAsync(selectedDistrict.Id);
```
You can find the 5 digit Indonesian postal code in SubDistrictModel. Here are the properties.
```c#
public class SubDistrictModel : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PostalCode { get; set; }
    public int DistrictId { get; set; }
}
```

### Find the SubDistrict based on PostalCode

Sometime, you need to provide a mechanism for user to search an address based on its postal code. Here is how to do it.
```c#
//set the index
kodePos.SubDistrictRepository.IndexOn = SubDistrictIndex.PostalCode.ToString();

int postalCode = 12520;
IEnumerable<SubDistrictModel> subDistrictList = await kodePos.SubDistrictRepository.GetAsync(postalCode);

// [optional] you need to revert the index back only if you want to search based on DistrictId.
kodePos.SubDistrictRepository.IndexOn = SubDistrictIndex.DistrictId.ToString();
```

### Get the complete address

This library provides the AddressModel that defines all needed property.
```c#
public class AddressModel : BaseModel
{
    public SubDistrictModel SubDistrict { get; set; }
    public DistrictModel District { get; set; }
    public CityModel City { get; set; }
    public ProvinceModel Province { get; set; }
}
```
You get the model by executing the SubDistrictModel extension.
```c#
using KodePosIndonesia.Extensions;

AddressModel address = await selectedSubDistrict.GetCompleteAddress();
```
This process is expensive and should be used only when you don't have its parent model. If you do, you should create the object yourself.
```c#
AddressModel address = new AddressModel()
{
    SubDistrict = selectedSubDistrict,
    District = selectedDistrict,
    City = selectedCity,
    Province = selectedProvince
}
```

## Credit
Thanks to [Edwin](https://github.com/edwin) for providing the SQL data. You can visit his repository in [this link](https://github.com/edwin/database-kodepos-seluruh-indonesia).
I have transformed the data to suit my class and have cleaned the data for duplicates.
| Record         | Count     |
|----------------|-----------|
| Province       | 34        |
| City           | 475       |
| District       | 6,994     |
| SubDistrict    | 81,225    |