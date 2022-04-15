using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Misc.SOneWeather.Infrastructure
{
    /// <summary>
    /// Represents plugin route provider
    /// </summary>
    public class RouteProvider : IRouteProvider
    {
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="endpointRouteBuilder">Route builder</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(name: "WeatherLookup",
                pattern: $"/weather",
                defaults: new { controller = "SOneWeather", action = "Lookup" });
        }

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        //public int Priority => 0;

        public int Priority
        {
            get
            {
                return int.MaxValue;
                ;
            }
        }
    }
}