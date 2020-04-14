using System.Threading.Tasks;
using IdentityServer.Configuration.Dto;

namespace IdentityServer.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
