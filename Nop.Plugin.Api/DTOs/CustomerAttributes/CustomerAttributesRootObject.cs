using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nop.Plugin.Api.DTO.CustomerAttributes
{
    public class CustomerAttributesRootObject : ISerializableObject
    {
        public CustomerAttributesRootObject()
        {
            CustomerAttributes = new CustomerAttributesDto();
        }

        [JsonProperty("customer_attributes")]
        public CustomerAttributesDto CustomerAttributes { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "customer_attributes";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(CustomerAttributesDto);
        }
    }
}
