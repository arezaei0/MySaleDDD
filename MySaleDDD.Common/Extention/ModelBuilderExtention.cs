using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaleDDD.Common.Extention
{
    public static class ModelBuilderExtention
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder builder, DBEntityConfiguration<TEntity> confige) where TEntity : class
        {
            builder.Entity<TEntity>(confige.Configure);
        }

    }
    public abstract class DBEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }

}
