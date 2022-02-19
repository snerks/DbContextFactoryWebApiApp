using DbContextFactoryWebApiApp.CustomMiddleware;
using DbContextFactoryWebApiApp.DataAccess;
using DbContextFactoryWebApiApp.Extensions;
using DbContextFactoryWebApiApp.Models;
using DbContextFactoryWebApiApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DbContextFactoryWebApiApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<ICustomerService, CustomerService>();

            services.ConfigurePOCO<ConnectionStringMap>(Configuration.GetSection("ConnectionStrings"));

            services.AddScoped<IDbContextFactory, DbContextFactory>(
                serviceProvider => new DbContextFactory(serviceProvider.GetRequiredService<ConnectionStringMap>()));

            services.AddScoped<CRMContext>(serviceProvider =>
            {
                var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory>();
                return dbContextFactory.Create();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();
            app.UseTenantDBMapper();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
