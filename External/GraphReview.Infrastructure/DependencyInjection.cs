using GraphReview.Domain.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace GraphReview.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
