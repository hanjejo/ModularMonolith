using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ModularMonolith.Contract
{
    /// <summary>
    /// 모듈 초기화를 위한 코드를 작성합니다.
    /// 해당 영역에서 의존성 등록 작업을 수행하며, 모듈에 속한 어셈블리를 반환하는 코드를 작성합니다.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// 모듈에서 사용하는 어셈블리 목록을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public Assembly[] GetAssemblies();

        /// <summary>
        /// 모듈의 서비스 의존성을 등록합니다.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public IServiceCollection Initialize(IServiceCollection services, IConfiguration configuration);
    }
}
