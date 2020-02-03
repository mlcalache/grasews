using System;

namespace Grasews.Infra.CrossCutting.Helpers
{
    public static class EnumHelper
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}