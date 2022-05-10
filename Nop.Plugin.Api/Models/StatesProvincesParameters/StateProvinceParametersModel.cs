using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nop.Plugin.Api.ModelBinders;

namespace Nop.Plugin.Api.Models.StatesProvincesParameters
{
    [ModelBinder(typeof(ParametersModelBinder<StateProvinceParametersModel>))]
    public class StateProvinceParametersModel
    {
        public StateProvinceParametersModel()
        {
            Abbreviation = string.Empty;
            CountryId = 0;
        }

        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("country_id")]
        public int CountryId { get; set; }

    }
}
