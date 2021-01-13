using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySaleDDD.Common.Extention;
using MySaleDDD.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace MySaleDDD.Core.FluentAPI
{
    public class OrderMap : DBEntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("Orders");
            entity.HasKey(x=>x.Id);
            entity.Property(x => x.Titel).IsRequired();
            entity.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
