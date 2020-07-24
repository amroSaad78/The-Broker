using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

namespace OcelotApiGw
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IConfiguration _cfg;        
        public Startup(IConfiguration configuration)
        {
            _cfg = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var identityUrl = _cfg.GetValue<string>("IdentityUrl");
            var authenticationProviderKey = "U3@msP9a@57zmRqd42L";            

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());
            /*.AddUrlGroup(new Uri(_cfg["CatalogUrlHC"]), name: "catalogapi-check", tags: new string[] { "catalogapi" })
            .AddUrlGroup(new Uri(_cfg["OrderingUrlHC"]), name: "orderingapi-check", tags: new string[] { "orderingapi" })
            .AddUrlGroup(new Uri(_cfg["BasketUrlHC"]), name: "basketapi-check", tags: new string[] { "basketapi" })
            .AddUrlGroup(new Uri(_cfg["IdentityUrlHC"]), name: "identityapi-check", tags: new string[] { "identityapi" })
            .AddUrlGroup(new Uri(_cfg["MarketingUrlHC"]), name: "marketingapi-check", tags: new string[] { "marketingapi" })
            .AddUrlGroup(new Uri(_cfg["PaymentUrlHC"]), name: "paymentapi-check", tags: new string[] { "paymentapi" })
            .AddUrlGroup(new Uri(_cfg["LocationUrlHC"]), name: "locationapi-check", tags: new string[] { "locationapi" });*/
            /* for javascript client
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });
            */
            
            services.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, x =>
                {
                    x.Authority = identityUrl;
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidAudiences = new[] { "orders", "apartment", "villa", "townhouse", "penthouse", "chalet", "twinhouse",
                                                 "duplex", "fullfloor", "halffloor", "wholebuilding", "land", "bulksale",
                                                 "bungalows", "hotelapartments",  "ivilla", "payments", "basket"
                        }
                    };
                    x.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents()
                    {
                        OnAuthenticationFailed = async ctx =>
                        {
                            
                        },
                        OnTokenValidated = async ctx =>
                        {
                            
                        },

                        OnMessageReceived = async ctx =>
                        {
                            
                        }
                    };
                });

            services.AddOcelot(_cfg);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*
            var pathBase = _cfg["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }
            */
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });

            //app.UseCors("CorsPolicy");

            app.UseOcelot().Wait();
        }
    }
}
