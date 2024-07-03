using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Contract;
using System.Reflection;

namespace Modules.CA.EndPoints
{
    public class Initializer : IModule
    {
        public Assembly[] GetAssemblies()
        {
            return [
                typeof(Initializer).Assembly,
                typeof(Application.DependencyInjection).Assembly,
                typeof(Domain.DependencyInjection).Assembly,
                typeof(Infra.DependencyInjection).Assembly,
                typeof(Events.DependencyInjection).Assembly,
            ];
        }

        public IServiceCollection Initialize(IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
