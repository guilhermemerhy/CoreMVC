using AutoMapper;
using Core.Data;
using Core.Infra.CrossCutting.Identity.Data;
using Core.Infra.CrossCutting.Identity.Models;
using Core.IoC;
using Core.MVC.Models;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO.Compression;

namespace Core.MVC
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
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddMemoryCache();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
            .AddJsonOptions(options => {
                options.SerializerSettings.DateFormatString = "dd/MM/yyyy";
            });

            services.AddAutoMapper();
            
            //IoC
            services.RegisterServices();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 2;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;



            })
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(o => {
                  o.LoginPath = new PathString("/login");
                  o.AccessDeniedPath = new PathString("/home/access-denied");
              })
              .AddFacebook(o =>
              {
                  o.AppId = Configuration["Facebook:AppId"];
                  o.AppSecret = Configuration["Facebook:AppSecret"];
              });

            //Compression
            services.Configure<GzipCompressionProviderOptions>(
              options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            //Elmah
            services.AddElmahIo(o =>
            {
                o.ApiKey = Configuration["Elmah:ApiKey"];
                o.LogId = new Guid(Configuration["Elmah:LogId"]);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }



            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseElmahIo();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
