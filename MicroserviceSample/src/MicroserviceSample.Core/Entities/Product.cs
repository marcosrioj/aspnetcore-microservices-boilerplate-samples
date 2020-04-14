using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MicroserviceSample.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceSample.Entities
{
    public class Product : FullAuditedEntity, IPassivable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
