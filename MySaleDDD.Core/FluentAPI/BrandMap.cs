using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySaleDDD.Common.Extention;
using MySaleDDD.Core.Models;

namespace MySaleDDD.Core.FluentAPI
{
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
