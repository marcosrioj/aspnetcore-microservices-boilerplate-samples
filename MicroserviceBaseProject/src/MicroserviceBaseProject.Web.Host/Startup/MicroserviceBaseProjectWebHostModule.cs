using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MicroserviceBaseProject.Configuration;

namespace MicroserviceBaseProject.Web.Host.Startup
{
    [DependsOn(
       typeof(MicroserviceBaseProjectWebCoreModule))]
    public class MicroserviceBaseProjectWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MicroserviceBaseProjectWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MicroserviceBaseProjectWebHostModule).GetAssembly());
        }
    }
}
