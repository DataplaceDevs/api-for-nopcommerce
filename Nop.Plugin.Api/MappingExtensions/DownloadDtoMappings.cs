using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Media;
using Nop.Plugin.Api.AutoMapper;
using Nop.Plugin.Api.DTO.Orders;

namespace Nop.Plugin.Api.MappingExtensions
{
    public static class DownloadDtoMappings
    {
        public static DownloadDto ToDto(this Download orderNote)
        {
            return orderNote.MapTo<Download, DownloadDto>();
        }
    }
}
