﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyWebApp.Interface;
using MyWebApp.Middlewares;
using MyWebApp.Models;
using MyWebApp.Repositories;
using MyWebApp.Services;
using MyWebApp.Shared;

#nullable disable

namespace MyWebApp
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _env = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddDbContext<DBContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectString")));
            }
            else
            {
                services.AddDbContext<DBContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("ServerDatabaseConnectString")));
            }

            services.AddCors();

            services.AddControllers();

            if (_env.IsDevelopment())
            {
                services.AddSingleton<DBContext>(s => new DBContext(Configuration.GetConnectionString("LocalDatabaseConnectString")));
            }
            else
            {
                services.AddSingleton<DBContext>(s => new DBContext(Configuration.GetConnectionString("DatabaseConnectString")));
            }

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(options => options.Filters.Add(new ValidationFilterAttribute()));

            services.Configure<AppSettings>(Configuration.GetSection("Jwt"));

            services.AddSingleton<IUserService, UserRepository>();
            services.AddSingleton<IStateService, StateRepository>();
            services.AddSingleton<IAuthorizeJwt, JwtService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_Web_Core", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                        {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                      },
                      new List<string>()
                    }
                  });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(
                   options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Web_Core v1"));
            }
           
            app.UseMiddleware<JwtAuthorize>();

            app.ConfigureExceptionHandler();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}