using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Repositories;

namespace Infrastructure.Persistence
{
    internal static class Extensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITodoItemRepository, SqliteTodoItemRepository>();
            services.AddScoped<ITodoListRepository, SqliteTodoListRepository>();

            services.AddDbContext<ApplicationContext>();

            return services;
        }
    }
}