using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Grasews.Infra.CrossCutting.Helpers.Extensions
{
    public static class MigrationExtension
    {
        public static void SeedEnumValues<T, TEnum>(this IDbSet<T> dbSet, Func<TEnum, T> converter)
            where T : class => Enum.GetValues(typeof(TEnum))
                                   .Cast<object>()
                                   .Select(value => converter((TEnum)value))
                                   .ToList()
                                   .ForEach(instance => dbSet.AddOrUpdate(instance));
    }
}