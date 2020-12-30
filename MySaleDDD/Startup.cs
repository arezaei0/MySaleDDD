using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySaleDDD.AutoMapper;
using MySaleDDD.Core;
using MySaleDDD.Core.Models;
using MySaleDDD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Connectionstring"), x => x.MigrationsAssembly("MySaleDDD.Core")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(Options =>
            {
                Options.Password.RequiredLength = 6;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireNonAlphanumeric = false;


            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped(typeof(IGenericRepository<>),typeof( GenericRepository<>));

            IMapper mapper = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); }).CreateMapper();
            services.AddSingleton(mapper);

            services.AddSession(Options =>
            {
                Options.Cookie.IsEssential = true;
                Options.Cookie.HttpOnly = true;
                Options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
