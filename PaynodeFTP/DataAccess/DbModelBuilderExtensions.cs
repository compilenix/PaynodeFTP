using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace PaynodeFTP.DataAccess
{
    public static class DbModelBuilderExtensions
    {
        private class SubclassOfGenericResult
        {
            public bool IsSpecifiedGenericType { get; set; }
            public Type[] GenericTypeArguments { get; set; }
        }

        private static SubclassOfGenericResult IsSubclassOfGeneric(Type generic, Type toCheck) {
            while (toCheck != null && toCheck != typeof(object)) {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur && toCheck.GenericTypeArguments.Length > 0) {
                    return new SubclassOfGenericResult
                    {
                        IsSpecifiedGenericType = true,
                        GenericTypeArguments = toCheck.GenericTypeArguments ?? new Type[0]
                    };
                }
                toCheck = toCheck.BaseType;
            }

            return new SubclassOfGenericResult
            {
                IsSpecifiedGenericType = false,
                GenericTypeArguments = new Type[0]
            };
        }

        public static void AddEntitiesFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            SubclassOfGenericResult result = null;

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass) continue;

                result = IsSubclassOfGeneric(typeof(EntityTypeConfiguration<>), type);
                if (!result.IsSpecifiedGenericType) continue;

                var entityTypeConfiguration = Activator.CreateInstance(type) as IEntityTypeConfiguration;
                // TODO: add checks
                entityTypeConfiguration.RegisterEntityOnModel(modelBuilder);
                entityTypeConfiguration.Configure();
            }
        }
    }
}