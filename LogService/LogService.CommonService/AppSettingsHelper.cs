using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogService.CommonService
{
    /// <summary>
    /// AppSettings辅助类
    /// </summary>
    internal class AppSettingsHelper
    {
        public static IConfiguration Configuration { get; set; }
        static AppSettingsHelper()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载 
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }

    }
}
