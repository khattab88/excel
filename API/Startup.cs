using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Repositories;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using API.Dtos.Mappings;
using System.Reflection;
using System.IO;

namespace API
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

            services.AddScoped<INationalParkRepository, NationalParkRepository>();

            services.AddAutoMapper(typeof(Mappings));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ExcelOpenAPISpec",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Excel API",
                        Version = "1",
                        Description = "Excle API Demo",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact() 
                        {
                            Email = "mahmoud.khattab@excelsystems-eg.com",
                            Name = "Mahmoud Khattab",
                            Url = new Uri("https://www.github.com/khattab88")
                        }
                    });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                options.IncludeXmlComments(xmlCommentsFullPath);
            });

            services.AddCors();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // https://localhost:{port}/swagger/ExcelOpenAPISpec/swagger.json
            app.UseSwagger();

            // https://localhost:{port}/swagger/index.html
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/ExcelOpenAPISpec/swagger.json", "Excel API");
                options.RoutePrefix = "";
            });

            app.UseCors(builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
