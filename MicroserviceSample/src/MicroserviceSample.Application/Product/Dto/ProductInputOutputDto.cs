using Abp.Application.Services.Dto;
using MicroserviceSample.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceSample.Product.Dto
{
    public class ProductInputOutputDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
