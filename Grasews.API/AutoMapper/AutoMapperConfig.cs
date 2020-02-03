using AutoMapper;
using System;
using System.Linq;

namespace Grasews.API.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static MapperConfiguration CreateMapperConfiguration()
        {
            return new MapperConfiguration(p =>
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("Grasews."));

                p.AddProfiles(assemblies);
            });
        }
    }
}