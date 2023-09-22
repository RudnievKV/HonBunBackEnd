using HonbunNoAnkiApi.Common;
using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.DBThrottlePipeline;
using HonbunNoAnkiApi.Models.DictionaryModels;
using HonbunNoAnkiApi.Repositories;
using HonbunNoAnkiApi.Repositories.Interfaces;
using HonbunNoAnkiApi.Services;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HonbunNoAnkiApi
{
    public class StartupHonbun
    {
        public StartupHonbun(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            this.environment = environment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment environment;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:SqlServer"]).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).EnableSensitiveDataLogging());


            services.Configure<DBSettings>(
               Configuration.GetSection("CosmosDBSettings"));

            services.AddSingleton<IDBSettings>(sp =>
                sp.GetRequiredService<IOptions<DBSettings>>().Value);



            //services.AddSingleton(s =>
            //    new CosmosClient(Configuration.GetSection("CosmosDBSettings")["ConnectionString"])
            //);


            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(Configuration.GetSection("CosmosDBSettings")["ConnectionString"])
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            services.AddSingleton(new MongoClient(settings));




            services.AddScoped<IDictionaryWordRepo, DictionaryWordRepo>();
            services.AddScoped<IDictionaryKanjiRepo, DictionaryKanjiRepo>();
            services.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IStageRepo, StageRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IWordCollectionRepo, WordCollectionRepo>();
            services.AddScoped<IWordRepo, WordRepo>();
            services.AddScoped<IMeaningReadingRepo, MeaningReadingRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();



            services.AddScoped<IDictionaryWordService, DictionaryWordService>();
            services.AddScoped<IDictionaryKanjiService, DictionaryKanjiService>();
            services.AddScoped<IStageService, StageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWordCollectionService, WordCollectionService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<IMeaningReadingService, MeaningReadingService>();

            services.AddSingleton<IConnectionThrottlingPipeline, ConnectionThrottlingPipeline>();






            services.AddControllers()
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }); ;

            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,

                        ValidateLifetime = true,

                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(), // HS256
                        ValidateIssuerSigningKey = true,
                    };
                });


            services.AddCors(options =>
            {
                options.AddPolicy(CORSPolicies.StandartCORSPolicy, builder =>
                {
                    if (environment.IsDevelopment())
                    {
                        builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("http://localhost:4200")
                            .AllowCredentials();
                    }
                    else
                    {
                        builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                        //.WithOrigins("https://wonderful-field-0689dbf03.3.azurestaticapps.net/", "http://localhost:4200");
                    }
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HonbunNoAnki", Version = "v1" });
            });

            services.AddAutoMapper(typeof(StartupHonbun));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HonbunNoAnki v1"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
