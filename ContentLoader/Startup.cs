using AutoMapper;
using ContentLoader.Core.Configurations;
using ContentLoader.Core.Services;
using ContentLoader.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace ContentLoader
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddAutoMapper(typeof(Startup));

            Config config = Configuration.Get<Config>();
            config.WebPath = env.WebRootPath;

            services.AddCors(o => o.AddPolicy("ContentLoaderOrigins", builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(config.AllowedHosts);
            }));

            services.AddSingleton(typeof(Config), config);
            services.AddScoped<IMediaService, MediaService>();
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile("Logs/ContentLoader-{Date}.txt");
            app.UseMvc();
        }
    }
}
