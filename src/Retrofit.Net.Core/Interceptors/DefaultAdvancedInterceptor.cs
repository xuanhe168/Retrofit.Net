using Retrofit.Net.Core.Models;

namespace Retrofit.Net.Core.Interceptors
{
    public class DefaultAdvancedInterceptor : IAdvancedInterceptor
    {
        public Response<dynamic> Intercept(IChain chain)
        {
            return chain.Proceed(chain.Request());
        }
    }
}