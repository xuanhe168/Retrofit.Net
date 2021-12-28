using Retrofit.Net.Core.Models;

namespace Retrofit.Net.Core.Interceptors;

public interface IInterceptor
{
    Response<dynamic> Intercept(IChain chain);
}