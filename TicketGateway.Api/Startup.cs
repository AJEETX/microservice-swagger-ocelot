using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;
using Ocelot.JwtAuthorize;

namespace TicketGateway.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("ocelot.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelotJwtAuthorize();
            services.AddOcelot(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app)
        {
            var microservices = new Dictionary<string, string>(
                            new KeyValuePair<string, string>[] {
                    KeyValuePair.Create("identity", "identity"),
                    KeyValuePair.Create("helpdesk", "helpdesk")
                            });
            app.UseMvc()
               .UseSwagger()
               .UseSwaggerUI(options =>
               {
                   microservices.Keys.ToList().ForEach(key =>
                   {
                       app.Map($"/{key}/swagger.json", b =>
                       {
                           b.Run(async x =>
                           {
                               var json = File.ReadAllText($"{key}.json");
                               await x.Response.WriteAsync(json);
                           });
                       });
                       options.SwaggerEndpoint($"/{key}/swagger.json", $"{microservices[key]} ");
                   });
                   options.DocumentTitle = "Record Point Demo";
               });
            app.UseOcelot().Wait();
        }
    }
}
