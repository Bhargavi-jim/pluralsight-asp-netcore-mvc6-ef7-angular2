using Microsoft.Extensions.Configuration;
using MyWorld.Services.Interfaces;

namespace MyWorld.Services
{
    public class AppSettingService : IAppSettingService
    {
        IConfigurationRoot _configuration;
        
        public AppSettingService(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }
        public string GetAppSetting(string appSetting)
        {
            return _configuration[appSetting]; 
        }
    }
}