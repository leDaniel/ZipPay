using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ZipPay.Data;
using ZipPay.Models;

namespace ZipPay
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
            //read db connection config from environment variables if available, else from connection strings
            var dbServer = Configuration["DBServer"] ?? Configuration.GetConnectionString("DBServer");
            var dbPort = Configuration["DBPort"] ?? Configuration.GetConnectionString("DBPort");
            var dbName = Configuration["DBName"] ?? Configuration.GetConnectionString("DBName");
            var dbUser = Configuration["DBUser"] ?? Configuration.GetConnectionString("DBUser");
            var dbPassword = Configuration["DBPassword"] ?? Configuration.GetConnectionString("DBPassword");

            var connectionString = $"Server={dbServer};Port={dbPort};Database={dbName};Uid={dbUser};Pwd={dbPassword};";
            System.Console.WriteLine(connectionString);

            services.AddDbContext<ZipPayContext>(opt => opt.UseMySql(connectionString, 
                mySqlOptions =>
                {
                    mySqlOptions
                    .EnableRetryOnFailure(
                    maxRetryCount: 30,                    
                    maxRetryDelay: TimeSpan.FromSeconds(100),
                    errorNumbersToAdd: null); 
                }
            ));

            services.AddControllers();

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "ZipPay API";
                    document.Info.Description = "An API to allow user and account management for Zip Pay";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Daniel Krishnapillai"     
                    };
                };
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IZipPayRepo, SQLZipPayRepo>();             

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PrepDB.PrepPopulation(app);
        }
    }
}
