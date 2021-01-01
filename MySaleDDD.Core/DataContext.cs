using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySaleDDD.Common.Extention;
using MySaleDDD.Core.FluentAPI;
using MySaleDDD.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySaleDDD.Core
{
    public class DataContext:IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public DataContext(DbContextOptions options):base(options)
        { 
            
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            AddConfiguration(builder);
        }
        public void AddConfiguration(ModelBuilder builder)
        {
            builder.AddConfiguration(new UnitMap());
            builder.AddConfiguration(new BrandMap());
            builder.AddConfiguration(new ProductMap());
        }
    }
}
