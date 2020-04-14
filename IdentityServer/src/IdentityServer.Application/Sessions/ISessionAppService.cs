using System.Threading.Tasks;
using Abp.Application.Services;
using IdentityServer.Sessions.Dto;

namespace IdentityServer.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
