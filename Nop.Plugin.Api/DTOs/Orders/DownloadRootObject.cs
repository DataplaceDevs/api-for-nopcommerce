using System;
using Newtonsoft.Json;

namespace Nop.Plugin.Api.DTO.Orders
{
    public class DownloadRootObject : ISerializableObject
    {
        public DownloadRootObject()
        {
            Download = new DownloadDto();
        }

        [JsonProperty("download")]
        public DownloadDto Download { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "download";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(DownloadDto);
        }
    }
}
