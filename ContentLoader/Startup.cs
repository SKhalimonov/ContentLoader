using AutoMapper;
using ContentLoader.Core.Configurations;
using ContentLoader.Core.Services;
using ContentLoader.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContentLoader
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
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddAutoMapper(typeof(Startup));

            Config config = Configuration.Get<Config>();

            services.AddCors(o => o.AddPolicy("ContentLoaderOrigins", builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            }));

            services.AddSingleton(typeof(Config), config);
            services.AddScoped<IMediaService, MediaService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
