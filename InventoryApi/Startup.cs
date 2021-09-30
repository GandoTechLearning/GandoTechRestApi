using Domain.DataService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InventoryApi
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
            services
               .AddAuthentication("Bearer")
               .AddIdentityServerAuthentication("Bearer", options =>
               {
                   options.ApiName = "inventoryapi";
                   options.Authority = "https://localhost:44395/";
               });

            services.AddAuthorization(x =>
            {
                x.AddPolicy("read", policy =>
                  policy.RequireClaim("scope", "inventoryapi.read"));
                x.AddPolicy("write", policy =>
                 policy.RequireClaim("scope", "inventoryapi.write"));
            });

            services
                .AddSingleton<ProductService, ProductService>()
                .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
