using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using MicroserviceSample.Product;
using Shouldly;
using Xunit;

namespace MicroserviceSample.Tests.Products
{
    public class ProductAppService_Tests : MicroserviceSampleTestBase
    {
        private readonly IProductAppService _productsAppService;

        public ProductAppService_Tests()
        {
            _productsAppService = Resolve<IProductAppService>();
        }

        [Fact]
        public async Task Should_Get_All_Products()
        {
            //Arrange
            var input = new PagedAndSortedResultRequestDto();

            // Act
            var output = await _productsAppService.GetAll(input);

            // Assert
            output.TotalCount.ShouldBeGreaterThanOrEqualTo(0);
        }
    }
}
