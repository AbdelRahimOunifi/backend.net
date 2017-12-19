using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ECApiRT.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ECApiRT.Midleware;
using Microsoft.IdentityModel.Tokens;

namespace ECApiRT
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

            var connection = @"Server=(localdb)\mssqllocaldb;Database=ECApiRT;Trusted_Connection=True;";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = "Ounifi.AbdelRahim.JWT",
                ValidAudience = "Ounifi.AbdelRahim.JWT",
                IssuerSigningKey = JwtSecurityKey.Create("this is my custom Secret key for authnetication")
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                    return Task.CompletedTask;
                }
            };
        });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            services.AddCors();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PrivilegeAdmin",
                    policy => policy.RequireClaim("Privilege","admin"));
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCors(builder =>
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod());
            app.UseMvcWithDefaultRoute();

        }
    }
}
