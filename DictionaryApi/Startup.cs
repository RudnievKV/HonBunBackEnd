using AutoMapper;
using DictionaryApi.DBContext;
using DictionaryApi.Models;
using DictionaryApi.Models.Profiles;
using DictionaryApi.Services;
using DictionaryApi.Services.Interfaces;
using MeCab;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Xml;
using Azure.Core;
using Newtonsoft.Json;
using System.IO;
using DictionaryApi.Common;

namespace DictionaryApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            this.environment = environment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment environment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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



            services.AddScoped<IDictionaryWordService, DictionaryWordService>();
            services.AddScoped<IDictionaryKanjiService, DictionaryKanjiService>();

            services.AddSingleton<IConnectionThrottlingPipeline, ConnectionThrottlingPipeline>();

            // Auto Mapper Configurations
            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new WordProfile());
            //    mc.AddProfile(new KanjiProfile());
            //    mc.AddProfile(new NameProfile());
            //});

            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options =>
            {
                options.AddPolicy(CORSPolicies.StandartCORSPolicy, builder =>
                {
                    if (environment.IsDevelopment())
                    {
                        builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed((host) => true)
                            .AllowCredentials();
                    }
                    else
                    {
                        builder.AllowAnyHeader()
                            .WithMethods("GET")
                            .WithOrigins("https://honbunnoankiapi.azurewebsites.net", "https://domain2.com");
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

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new NullToEmptyStringConverter());
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DictionaryApi", Version = "v1" });
            });


            //XmlDocument doc = new XmlDocument();
            //doc.Load(@"F:\Work\Japan\HonbunNoAnki\Dictionaries\Kanjidic2 (Kanji)\kanjidic2.xml");
            //string jsonText = JsonConvert.SerializeXmlNode(doc);
            //File.WriteAllText(@"F:\Work\Japan\HonbunNoAnki\Dictionaries\Kanjidic2 (Kanji)\kanjidic2_v7.json", jsonText);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DictionaryApi v1"));
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
