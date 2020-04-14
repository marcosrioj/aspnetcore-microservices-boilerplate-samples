using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IdentityServer.MultiTenancy.Dto;

namespace IdentityServer.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

