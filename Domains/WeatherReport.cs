using System;
using Nop.Core;

namespace Nop.Plugin.Misc.SOneWeather.Domains
{
    public class WeatherReport : BaseEntity
    {
        public string Main { get; set; }

        public string Icon { get; set; }

        public string Temp { get; set; }

        public string FeelsLike { get; set; }

        public string Humidity { get; set; }

        public string WindSpeed { get; set; }

        public string PostalCode { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string City { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
