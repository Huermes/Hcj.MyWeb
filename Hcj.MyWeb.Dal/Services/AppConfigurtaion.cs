using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Hcj.MyWeb.Dal.Services
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class AppConfigurtaion
    {
        public static IConfiguration Configuration { get; set; }
        static AppConfigurtaion()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();

        }
    }
}
