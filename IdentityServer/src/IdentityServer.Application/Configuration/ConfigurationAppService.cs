using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using IdentityServer.Configuration.Dto;
using Shared.Configuration;

namespace IdentityServer.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : IdentityServerAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
