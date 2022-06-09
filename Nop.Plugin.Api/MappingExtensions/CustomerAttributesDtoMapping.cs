using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Api.AutoMapper;
using Nop.Plugin.Api.DTO.CustomerAttributes;

namespace Nop.Plugin.Api.MappingExtensions
{
    public static class CustomerAttributesDtoMapping
    {
        public static CustomerAttributesDto ToDto(this CustomerAttribute stateProvince)
        {
            return stateProvince.MapTo<CustomerAttribute, CustomerAttributesDto>();
        }
    }
}
