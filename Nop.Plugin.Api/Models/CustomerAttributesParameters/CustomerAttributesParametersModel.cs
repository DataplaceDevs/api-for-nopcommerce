using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nop.Plugin.Api.ModelBinders;

namespace Nop.Plugin.Api.Models.CustomerAttributesParameters
{
    [ModelBinder(typeof(ParametersModelBinder<CustomerAttributesParametersModel>))]
    public class CustomerAttributesParametersModel
    {
        public CustomerAttributesParametersModel()
        {
            Name = string.Empty;
        }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
