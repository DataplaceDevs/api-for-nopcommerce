using System;
using Newtonsoft.Json;

namespace Nop.Plugin.Api.DTO.Orders
{
    public class OrderNoteRootObject : ISerializableObject
    {
        public OrderNoteRootObject()
        {
            OrderNote = new OrderNoteDto();
        }

        [JsonProperty("ordernote")]
        public OrderNoteDto OrderNote { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "ordernote";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(OrderNoteDto);
        }
    }
}
