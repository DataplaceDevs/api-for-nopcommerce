using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Services
{
    public static class CustomerDocumentsEnums
    {
        public enum ECustomerDocuments
        {
            [Description("CPF/CNPJ")]
            CPF_CNPJ = 0,
            [Description("RG/IE")]
            RG_IE = 1
        }
    }

    public static class EnumExtensions
    {
        public static string GetEnumDescription(this CustomerDocumentsEnums.ECustomerDocuments value)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value
               .GetType()
               .GetField(value.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
