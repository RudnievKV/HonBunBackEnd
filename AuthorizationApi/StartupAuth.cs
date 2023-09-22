using AuthorizationApi.Common;
using AuthorizationApi.DBContext;
using AuthorizationApi.Repos;
using AuthorizationApi.Repos.Interfaces;
using AuthorizationApi.Services;
using AuthorizationApi.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApi
{
    public class StartupAuth
    {

        public StartupAuth(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthRepo, AuthRepo>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddControllers();
            services.AddDbContext<MyDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:SqlServer"]));
            var authOptionsConfiguration = Configuration.GetSection("Auth");
            services.Configure<AuthOptions>(authOptionsConfiguration);

            services.AddCors(options =>
            {
                options.AddPolicy(CORSPolicies.StandartCORSPolicy, builder =>
                {
                    if (Environment.IsDevelopment())
                    {
                        builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed((host) => true)
                            .AllowCredentials();
                    }
                    else
                    {
                        builder.AllowAnyHeader()
                            .WithMethods("POST")
                            .AllowAnyOrigin();
                        //.WithOrigins("https://wonderful-field-0689dbf03.3.azurestaticapps.net/", "http://localhost:4200");
                    }
                });
                //options.AddDefaultPolicy(
                //    builder =>
                //    {
                //        builder.AllowAnyHeader()
                //        .AllowAnyMethod()
                //        .SetIsOriginAllowed((host) => true)
                //        .AllowCredentials();
                //    });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthorizationApi", Version = "v1" });
            });

            services.AddAutoMapper(typeof(StartupAuth));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthorizationApi v1"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
