
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Contract;
using Modules.VSA.Persistence;
using System.Reflection;

namespace Modules.VSA;

public class Initializer : IModule
{
    public Assembly[] GetAssemblies()
    {
        return [
            typeof(Initializer).Assembly
        ];
    }

    public IServiceCollection Initialize(IServiceCollection services, IConfiguration configuration)
    {
        // 데이터베이스 등록
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("VSA")));


        return services;
    }
}
