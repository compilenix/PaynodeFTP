using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication2.DataAccess
{
    public class EntityTypeConfiguration<T> : IEntityTypeConfiguration
        where T : class
    {
        internal EntityTypeBuilder<T> Builder { get; private set; }

        internal EntityTypeConfiguration()
        {
        }

        public virtual void Configure()
        {
        }

        public void RegisterEntityOnModel(ModelBuilder builder)
        {
            if (builder == null) throw new ArgumentException(nameof(builder));
            
            Builder = builder.Entity<T>();
        }
    }

}