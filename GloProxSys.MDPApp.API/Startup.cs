using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloProxSys.MDPApp.API;
using GloProxSys.MDPApp.API.Configuration;
using GloProxSys.MDPApp.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Westwind.AspNetCore.Formatters;

namespace GloProxSys.MDPApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()));
            services.AddOptions();

            var dataConfig = new DataConfig();
            Configuration.GetSection("Data").Bind(dataConfig);
            services.AddTransient<IDataService>((s) =>
            {
                return new DataService(dataConfig);
            });

            //TO-Do Add S3 Resource Paths


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "GPS Demo API", Version = "v1" });
            });

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            //services.AddAWSService<Amazon.S3.IAmazonS3>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            
            app.UseSwagger();
            //Don't include for prod/aws.  Doesn't support ui
            //if (env.IsDevelopment())
            //{
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GPS Demo API V1");
                });
            //}

            app.Use((context, next) =>
            {
                context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                return next.Invoke();
            });

            app.UseMvc();
        }
    }
}
