using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO.Base;

namespace Nop.Plugin.Api.DTO.StatesProvinces
{
    [JsonObject(Title = "state_province")]
    public class StateProvinceDto : BaseDto
    {
        [JsonProperty("country_id")]
        public int CountryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; }

        [JsonProperty("display_order")]
        public int DisplayOrder { get; set; }

    }
}
