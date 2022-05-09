using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Api.AutoMapper;
using Nop.Plugin.Api.DTO.Orders;

namespace Nop.Plugin.Api.MappingExtensions
{
    public static class OrderNoteDtoMappings
    {
        public static OrderNoteDto ToDto(this OrderNote orderNote)
        {
            return orderNote.MapTo<OrderNote, OrderNoteDto>();
        }
    }
}
