using Nop.Core;
using Nop.Core.Domain.Configuration;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Stores;
using Nop.Services.Tasks;

namespace Nop.Plugin.Misc.SOneWeather
{
    /// <summary>
    /// Represents the S-One Weather plugin
    /// </summary>
    public class SOneWeatherPlugin : BasePlugin
    {
        #region Fields

        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly ISettingService _settingService;
        private readonly IStoreService _storeService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public SOneWeatherPlugin(IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            IScheduleTaskService scheduleTaskService,
            ISettingService settingService,
            IStoreService storeService,
            IWebHelper webHelper)
        {
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _scheduleTaskService = scheduleTaskService;
            _settingService = settingService;
            _storeService = storeService;
            _webHelper = webHelper;
        }

        #endregion
    }
}
