using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySaleDDD.Common.Extention;
using MySaleDDD.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaleDDD.Core.FluentAPI
{
    public class UnitMap : DBEntityConfiguration<Unit>
    {
        public override void Configure(EntityTypeBuilder<Unit> entity)
        {
            entity.ToTable("Units");
            entity.HasKey(x=>x.Id);
            entity.Property(x => x.Titel).IsRequired();
        }
    }
    public class BrandMap : DBEntityConfiguration<Brand>
    {
        public override void Configure(EntityTypeBuilder<Brand> entity)
        {
            entity.ToTable("Brands");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Titel).IsRequired();
        }
    }
}
