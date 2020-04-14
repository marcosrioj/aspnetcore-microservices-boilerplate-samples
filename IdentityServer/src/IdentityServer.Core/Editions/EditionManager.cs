using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;

namespace IdentityServer.Editions
{
    public class EditionManager : AbpEditionManager
    {
        public EditionManager(
            IRepository<Edition> editionRepository, 
            IAbpZeroFeatureValueStore featureValueStore)
            : base(
                editionRepository,
                featureValueStore)
        {
        }
    }
}
