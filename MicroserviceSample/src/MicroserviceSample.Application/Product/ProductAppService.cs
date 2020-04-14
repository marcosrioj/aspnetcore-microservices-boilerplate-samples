using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using MicroserviceSample.Product.Dto;
using Microsoft.EntityFrameworkCore;
using Shared.Authorization;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using MicroserviceSample.Repositories;

namespace MicroserviceSample.Product
{
    [AbpAuthorize(PermissionNames.Pages_MicroserviceSample_Product)]
    public class ProductAppService : MicroserviceSampleAppServiceBase, IProductAppService
    {
        private readonly IProductRepository _productRepository;

        public ProductAppService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductInputOutputDto> Get(EntityDto input)
        {
            var entity = await GetEntityByIdAsync(input.Id);
            return ObjectMapper.Map<ProductInputOutputDto>(entity);
        }

        public async Task<PagedResultDto<ProductInputOutputDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            var query = _productRepository.GetAll();

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<ProductInputOutputDto>(
                totalCount,
                entities.Select(x => ObjectMapper.Map<ProductInputOutputDto>(x)).ToList()
            );
        }

        public async Task<ProductInputOutputDto> Create(ProductInputOutputDto input)
        {
            var entity = ObjectMapper.Map<Entities.Product>(input);

            await _productRepository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<ProductInputOutputDto>(entity);
        }

        public async Task<ProductInputOutputDto> Update(ProductInputOutputDto input)
        {
            var entity = await GetEntityByIdAsync(input.Id);

            ObjectMapper.Map(input, entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<ProductInputOutputDto>(entity);
        }

        public Task Delete(EntityDto input)
        {
            return _productRepository.DeleteAsync(input.Id);
        }

        private Task<Entities.Product> GetEntityByIdAsync(int id)
        {
            return _productRepository.GetAsync(id);
        }

        /// <summary>
        /// Should apply sorting if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        private IQueryable<Entities.Product> ApplySorting(IQueryable<Entities.Product> query, IPagedAndSortedResultRequest input)
        {
            //Try to sort query if available
            var sortInput = input as ISortedResultRequest;
            if (sortInput != null)
            {
                if (!sortInput.Sorting.IsNullOrWhiteSpace())
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }

            //IQueryable.Task requires sorting, so we should sort if Take will be used.
            if (input is ILimitedResultRequest)
            {
                return query.OrderByDescending(e => e.Id);
            }

            //No sorting
            return query;
        }

        /// <summary>
        /// Should apply paging if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        private IQueryable<Entities.Product> ApplyPaging(IQueryable<Entities.Product> query, IPagedAndSortedResultRequest input)
        {
            //Try to use paging if available
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
                return query.PageBy(pagedInput);
            }

            //Try to limit query result if available
            var limitedInput = input as ILimitedResultRequest;
            if (limitedInput != null)
            {
                return query.Take(limitedInput.MaxResultCount);
            }

            //No paging
            return query;
        }
    }
}
