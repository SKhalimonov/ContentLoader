using AutoMapper;
using ContentLoader.Core.Configurations;
using ContentLoader.Core.Services;
using ContentLoader.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ContentLoader
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Config = Configuration.Get<Config>();
            Config.ContentRootPath = environment.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        public Config Config { set; get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddAutoMapper(typeof(Startup));

            services.AddCors(o => o.AddPolicy("ContentLoaderOrigins", builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(Config.AllowedHosts);
            }));

            services.AddSingleton(typeof(Config), Config);
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
