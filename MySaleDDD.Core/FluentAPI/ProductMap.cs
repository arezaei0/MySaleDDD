using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySaleDDD.Common.Extention;
using MySaleDDD.Core.Models;

namespace MySaleDDD.Core.FluentAPI
{
    public class ProductMap : DBEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Products");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Titel).IsRequired();
            entity.HasOne(u => u.Unit).WithMany().HasForeignKey(x => x.UnitId).
                OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(u => u.Unit).WithMany().HasForeignKey(x => x.UnitId).
    OnDelete(DeleteBehavior.Restrict);
        }
    }
}
