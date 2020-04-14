using Abp.EntityFrameworkCore;
using MicroserviceSample.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceSample.EntityFrameworkCore.Repositories
{
    public class ProductRepository : MicroserviceSampleRepositoryBase<Entities.Product>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<MicroserviceSampleDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}
