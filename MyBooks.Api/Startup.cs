using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBooks.Data.Models;
using MyBooks.Data.Interfaces;
using MyBooks.Core.Services;
using AutoMapper;
using NLog;
using System.IO;
using MyBooks.Api.Filters;
using Newtonsoft.Json;

namespace MyBooks.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; 
            }); 
            services.AddDbContext<MyBooksDBContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:MyBooksDb"]));
            services.AddHttpContextAccessor();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IUserRolesService, UserRoleService>();
            services.AddScoped<IShopCartService, ShopCartService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();
          
        }
    }
}
