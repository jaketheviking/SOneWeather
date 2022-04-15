using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.SOneWeather.Services;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Misc.SOneWeather.Controllers
{

    public class SOneWeatherController : BasePluginController
    {
        #region Fields

        private readonly SOneWeatherService _weatherService;

        #endregion


        #region Ctor

        public SOneWeatherController(SOneWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        #endregion


        public IActionResult Lookup(string message = null)
        {
            return View("~/Plugins/Misc.SOneWeather/Views/Lookup.cshtml", message);
        }

        [HttpPost]
        public async Task<IActionResult> LookupWeather(IFormCollection form)
        {
            var postalCode = form["PostalCode"];
            if (!Regex.Match(postalCode, @"^\d{5}(?:[-\s]\d{4})?$").Success)
                return RedirectToAction("Lookup", new { message = "We encountered a problem with the code you entered." });

            var coordinates = await _weatherService.GetCoordinates(postalCode);
            if(String.IsNullOrWhiteSpace(coordinates.Latitude) || String.IsNullOrWhiteSpace(coordinates.Latitude))
                return RedirectToAction("Lookup", new { message = "The zip code you submitted could not be found." });

            var weather = await _weatherService.GetWeather(coordinates);

            return View("~/Plugins/Misc.SOneWeather/Views/WeatherReport.cshtml", weather);
        }
    }
}
