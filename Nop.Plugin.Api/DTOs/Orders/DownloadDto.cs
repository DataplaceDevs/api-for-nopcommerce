using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO.Base;

namespace Nop.Plugin.Api.DTO.Orders
{
    [JsonObject(Title = "download")]
    public class DownloadDto : BaseDto
    {
        [JsonProperty("download_guid")]
        [JsonIgnore]
        public Guid DownloadGuid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether DownloadUrl property should be used
        /// </summary>
        [JsonProperty("use_download_url")]
        public bool UseDownloadUrl { get; set; }

        /// <summary>
        /// Gets or sets a download URL
        /// </summary>
        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; }

        /// <summary>
        /// Gets or sets the download binary
        /// </summary>
        [JsonProperty("download_binary")]
        //public byte[] DownloadBinary { get; set; }
        public string DownloadBinary { get; set; }

        /// <summary>
        /// The mime-type of the download
        /// </summary>
        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        /// <summary>
        /// The filename of the download
        /// </summary>
        [JsonProperty("file_name")]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the extension
        /// </summary>
        [JsonProperty("extension")]
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the download is new
        /// </summary>
        [JsonProperty("is_new")]
        [JsonIgnore]
        public bool IsNew { get; set; }

    }
}
