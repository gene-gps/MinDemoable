using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloProxSys.MDPApp.APIService;
using GloProxSys.MDPApp.Dashboard.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GloProxSys.MDPApp.Dashboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            //services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            //services.AddAWSService<Amazon.DynamoDBv2.IAmazonDynamoDB>();

            //var authSettings = Configuration.GetSection("Auth").Get<AuthConfiguration>();

            //var url = "https://cognito-idp.{0}.amazonaws.com/{1}/.well-known/openid-configuration";

            //https://lbadri.wordpress.com/2018/02/25/asp-net-core-2-0-oidc-authentication-using-aws-cognito/
            //services.AddAuthentication(options =>
           // {
           //     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
           //     options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
           //     options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
           // })
           //.AddCookie()
           //.AddOpenIdConnect(options =>
           //{
           //    options.ResponseType = "code";
           //    options.MetadataAddress = string.Format(url, authSettings.Region, authSettings.UserPoolID);
           //    options.ClientId = authSettings.ClientID;
           //    options.ClientSecret = authSettings.ClientSecret;
           //});
		   
            var config = new ApiConfig();
            Configuration.GetSection("API").Bind(config);
            services.AddTransient<IAPIDataService>((s) =>
            {
                return new APIDataService(config);
            });
            services.AddTransient<IAnalyticDataService>((s) =>
            {
                return new DemoAnalyticDataService();
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                	name: "default",
                    template: "{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}
