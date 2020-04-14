using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MicroserviceSample.Configuration;

namespace MicroserviceSample.Web.Host.Startup
{
    [DependsOn(
       typeof(MicroserviceSampleWebCoreModule))]
    public class MicroserviceSampleWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MicroserviceSampleWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MicroserviceSampleWebHostModule).GetAssembly());
        }
    }
}
