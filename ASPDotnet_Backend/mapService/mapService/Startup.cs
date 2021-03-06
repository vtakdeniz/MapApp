using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using mapService.Models;
using Microsoft.Extensions.Options;
using mapService.Controllers;
using mapService.DBConfig;
using mapService.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using mapService.Utils;
namespace mapService
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
            services.AddControllers(options => {

                // Support for BsonDocument
                options.OutputFormatters.Insert(0, new BsonDocumentOutputFormatter());
                options.InputFormatters.Insert(0, new BsonDocumentInputFormatter());

            });
            services.AddSingleton<IDbClient,DbClient>();
            services.Configure<mapServiceDatabaseSettings>(
                    Configuration.GetSection(nameof(mapServiceDatabaseSettings)));
            services.AddTransient<IBranchServices, BranchServices>();
            services.AddTransient<IPolygonServices, PolygonServices>();
            services.AddTransient<IHospitalServices, HospitalServices>();
            //services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true) // allow any origin
              .AllowCredentials()); // allow credentials

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
