using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Nop.Data;
using Nop.Plugin.Misc.SOneWeather.Domains;
using Nop.Plugin.Misc.SOneWeather.Models;
using Nop.Services.Configuration;

namespace Nop.Plugin.Misc.SOneWeather.Services
{
    public class SOneWeatherService
    {
        TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;


        #region Fields

        private readonly ISettingService _settingService;
        private readonly IRepository<WeatherReport> _weatherRepo;

        #endregion


        #region Ctr

        public SOneWeatherService(ISettingService settingService, IRepository<WeatherReport> weatherRepo)
        {
            _settingService = settingService;
            _weatherRepo = weatherRepo;
        }

        #endregion


        public async Task<GeoLocation> GetCoordinates(string postalCode)
        {
            var result = new GeoLocation
            {
                PostalCode = postalCode
            };

            var callUrl = "https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyBi6I_cyEBGjPYnKBnCpRvwj6SXx8iVBD8&address=" + postalCode;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(callUrl);
            if (response.IsSuccessStatusCode)
            {
                GooglGeoResponse geo = await response.Content.ReadAsAsync<GooglGeoResponse>();
                if (geo.results.Length <= 0)
                    return result;
                result.Latitude = geo.results[0].geometry.location.lat.ToString();
                result.Longitude = geo.results[0].geometry.location.lng.ToString();
            }

            return result;
        }

        public async Task<WeatherReport> GetWeather(GeoLocation coordinates)
        {
            var result = new WeatherReport
            {
                PostalCode = coordinates.PostalCode,
                Latitude = coordinates.Latitude,
                Longitude = coordinates.Longitude,
                Timestamp = DateTime.UtcNow
            };

            var callUrl = "https://api.openweathermap.org/data/2.5/weather?lat=" + coordinates.Latitude + "&lon=" + coordinates.Longitude + "&units=imperial&appid=800a3f8dc7a089347269a3957e059ee1";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(callUrl);
            if (response.IsSuccessStatusCode)
            {
                WeatherResponse w = await response.Content.ReadAsAsync<WeatherResponse>();
                result.Main = w.weather[0].main;
                result.Icon = w.weather[0].icon;
                result.Temp = Math.Round(w.main.temp).ToString();
                result.FeelsLike = Math.Round(w.main.feels_like).ToString();
                result.Humidity = Math.Round(w.main.humidity).ToString();
                result.WindSpeed = Math.Round(w.wind.speed).ToString();
                result.City = w.name;

                await _weatherRepo.InsertAsync(result);
            }

            return result;
        }
    }
}