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
            [Description("NONE")]
            NONE = -1,
            [Description("CPF/CNPJ")]
            CPF_CNPJ = 0,
            [Description("RG/IE")]
            RG_IE = 1,
            [Description("RFC")]
            RFC = 2
        }

        public enum ETypeDocument
        {
            InscriFed = 0,
            InscriEst = 1
        }
    }

    public static class CustomerDocuments
    {
        public static string CPFCNPJ_ATTRIBUTE_NAME = "CPF/CNPJ";
        public static string RGIE_ATTRIBUTE_NAME = "RG/IE";
        public static string RFC_ATTRIBUTE_NAME = "RFC";

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

        public static string GetEnumDescription(this CustomerDocumentsEnums.ETypeDocument value)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value
               .GetType()
               .GetField(value.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static int GetEnumValue(this CustomerDocumentsEnums.ETypeDocument value)
        {
            return (int)value;
        }
    }    
}
