using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Shared.Authorization.Users;
using IdentityServer.Editions;
using Shared.MultiTenancy;

namespace IdentityServer.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
