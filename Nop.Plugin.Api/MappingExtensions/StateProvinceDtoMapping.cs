using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Directory;
using Nop.Plugin.Api.AutoMapper;
using Nop.Plugin.Api.DTO.StatesProvinces;

namespace Nop.Plugin.Api.MappingExtensions
{
    public static class StateProvinceDtoMapping
    {
        public static StateProvinceDto ToDto(this StateProvince stateProvince)
        {
            return stateProvince.MapTo<StateProvince, StateProvinceDto>();
        }

    }
}
