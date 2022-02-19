using System.Linq;
using System.Threading.Tasks;
using DbContextFactoryWebApiApp.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DbContextFactoryWebApiApp.CustomMiddleware
{
    public class TenantDBMappingMiddleware
    {
        private readonly RequestDelegate next;

        public TenantDBMappingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // https://github.com/explorer14/WebApplication_MultiTenant
            var serviceIdClaim = httpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "service-id");

            if (serviceIdClaim != null)
            {
                if (!string.IsNullOrWhiteSpace(serviceIdClaim.Value))
                {
                    if (int.TryParse(serviceIdClaim.Value, out var parsedServiceId))
                    {
                        var dbContextFactory =
                            httpContext.RequestServices.GetRequiredService<IDbContextFactory>();

                        // Set ServiceId for the scope of the Request
                        dbContextFactory.ServiceId = parsedServiceId;
                    }
                }
            }

            await next(httpContext);
        }
    }

    public static class TenantDBMappingMiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantDBMapper(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<TenantDBMappingMiddleware>();
        }
    }
}
