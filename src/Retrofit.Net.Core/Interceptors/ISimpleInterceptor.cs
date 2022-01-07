using Retrofit.Net.Core.Models;

namespace Retrofit.Net.Core.Interceptors
{
    public interface ISimpleInterceptor
    {
        void OnRequest(Request request);
        void OnResponse(Response<dynamic> response);
    }
}
