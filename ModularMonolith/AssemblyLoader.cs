using Microsoft.Extensions.DependencyModel;
using ModularMonolith.Contract;
using System.Reflection;

namespace ModularMonolith
{
    public static class AssemblyLoader
    {
        public static Assembly[] LoadModules(IServiceCollection services, IConfiguration configuration)
        {
            // 현재 애플리케이션 도메인에 로드된 모든 어셈블리 가져오기
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            // DependencyContext를 사용하여 모든 런타임 라이브러리 로드
            var dependencies = DependencyContext.Default;
            var runtimeLibraries = dependencies.RuntimeLibraries;

            foreach (var library in runtimeLibraries.Where(o=>o.Name.StartsWith("Modules")))
            {
                foreach (var assemblyName in library.GetDefaultAssemblyNames(dependencies))
                {
                    if (!loadedAssemblies.Any(a => a.GetName().Name == assemblyName.Name))
                    {
                        try
                        {
                            var assembly = Assembly.Load(assemblyName);
                            loadedAssemblies.Add(assembly);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to load assembly {assemblyName}: {ex.Message}");
                        }
                    }
                }
            }

            // IModule 인터페이스를 구현하는 모든 클래스를 찾음
            IEnumerable<Type> moduleTypes = loadedAssemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IModule).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .ToList();

            foreach (Type moduleType in moduleTypes)
            {
                // IModule 인스턴스를 생성
                IModule module = (IModule)Activator.CreateInstance(moduleType);

                // 모듈의 어셈블리를 가져와 리스트에 추가
                loadedAssemblies.AddRange(module.GetAssemblies());

                // 모듈 초기화
                services = module.Initialize(services, configuration);
            }

            // 중복 제거 후 배열로 변환하여 반환
            return loadedAssemblies.ToArray();
        }
    }
}
