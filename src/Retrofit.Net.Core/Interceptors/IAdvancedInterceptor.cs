using Retrofit.Net.Core.Models;

namespace Retrofit.Net.Core.Interceptors;

public interface IAdvancedInterceptor
{
    Response<dynamic> Intercept(IChain chain);
}