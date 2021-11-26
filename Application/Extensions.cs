using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Command.Classes;
using SharedKernel.Query.Classes;

namespace Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCommands();
            services.AddQueries();
            
            return services;
        }
    }
}