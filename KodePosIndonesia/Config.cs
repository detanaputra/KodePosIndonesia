using System;

namespace KodePosIndonesia
{
    public class Config
    {
        public bool GetFromLocalData { get; set; } = true;
        internal static Uri BaseUri = new Uri("https://kodepos-2d475.firebaseio.com/");
        internal static Uri ProvinceUri = new Uri(BaseUri.ToString() + "/list_propinsi.json");

        public Config() { }
    }
}