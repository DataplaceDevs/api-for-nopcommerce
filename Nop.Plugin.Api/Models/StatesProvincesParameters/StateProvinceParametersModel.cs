﻿using System;
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
            Country = string.Empty;
        }

        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

    }
}