using System.ComponentModel;
using System.Linq;

namespace Grasews.Infra.CrossCutting.Helpers.Extensions
{
    public static class EnumExtension
    {
        //public static string GetEnumDescription(this Enum value)
        //{
        //    var fi = value.GetType().GetField(value.ToString());

        //    var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        //    return attributes != null && attributes.Length > 0 ? attributes[0].Description : value.ToString();
        //}

        public static string GetEnumDescription<TEnum>(this TEnum item)
            => item.GetType()
                   .GetField(item.ToString())
                   .GetCustomAttributes(typeof(DescriptionAttribute), false)
                   .Cast<DescriptionAttribute>()
                   .FirstOrDefault()?.Description ?? string.Empty;
    }
}