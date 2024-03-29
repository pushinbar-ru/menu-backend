using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pushinbar.API.Options;
using Pushinbar.Common.Models.Alcohol;
using Pushinbar.Common.Models.Eat;
using Pushinbar.Common.Models.NotAlcohol;
using Pushinbar.Common.Models.Snack;
using Pushinbar.KonturMarket.Client;
using Pushinbar.Repositories;
using Pushinbar.Services.Products;
using Pushinbar.Services.Products.Alcohol;
using Pushinbar.Services.Products.Eat;
using Pushinbar.Services.Products.NotAlcohol;
using Pushinbar.Services.Products.Snack;
using Ydb.Sdk;
using Ydb.Sdk.Table;
using Ydb.Sdk.Yc;

namespace Pushinbar.API
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
            var konturMarketOptions = new KonturMarketOptions();
            Configuration.GetSection(konturMarketOptions.OptionsTitle).Bind(konturMarketOptions);

            var dbOptions = new DbOptions();
            Configuration.GetSection(dbOptions.OptionsTitle).Bind(dbOptions);

            services.AddTransient<KonturMarketClient>((context) => 
                new KonturMarketClient(konturMarketOptions.ApiKey, konturMarketOptions.ShopId));

            AddDB(services);

            services.AddSingleton<AlcoholRepository>();
            services.AddSingleton<NotAlcoholRepository>();
            services.AddSingleton<EatRepository>();
            services.AddSingleton<SnackRepository>();
            
            
            services.AddSingleton<IProductsService<AlcoholProduct>, AlcoholProductsService>();
            services.AddSingleton<IProductsService<NotAlcoholProduct>, NotAlcoholProductsService>();
            services.AddSingleton<IProductsService<EatProduct>, EatProductsService>();
            services.AddSingleton<IProductsService<SnackProduct>, SnackProductsService>();
            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MenuWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MenuWebApi v1"));
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        public static IServiceCollection AddDB(IServiceCollection services)
        {
            var saProvider = new ServiceAccountProvider(
                saFilePath: "creds.json", // Path to file with service account JSON info
                loggerFactory: new LoggerFactory());

            saProvider.Initialize().GetAwaiter().GetResult();
            var config = new DriverConfig(
                endpoint: "grpcs://ydb.serverless.yandexcloud.net:2135",
                database: "/ru-central1/b1gs6rm7jilgibstbues/etncq92l3lr1lachc2v1",
                credentials: saProvider
            );

            var driver = new Driver(
                config: config,
                loggerFactory: new LoggerFactory()
            );

            driver.Initialize().GetAwaiter().GetResult(); // Make sure to await driver initialization
            return services
                .AddSingleton(new TableClient(driver, new TableClientConfig()));
        }
    }
}
