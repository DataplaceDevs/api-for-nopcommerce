using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO;

namespace Nop.Plugin.Api.DTO.StatesProvinces
{
    public class StateProvinceRootObject : ISerializableObject
    {
        public StateProvinceRootObject()
        {            
            StateProvince = new StateProvinceDto();
        }

        [JsonProperty("state_province")]
        public StateProvinceDto StateProvince { get; set; }
        

        public string GetPrimaryPropertyName()
        {
            return "state_province";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(StateProvinceDto);
        }
    }
}
