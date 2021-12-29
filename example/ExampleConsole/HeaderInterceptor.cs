using Retrofit.Net.Core.Interceptors;
using Retrofit.Net.Core.Models;

namespace ExampleConsole;

public class HeaderInterceptor : IInterceptor
{
    public Response<dynamic> Intercept(IChain chain)
    {
        Request request = chain.Request().NewBuilder()
            .AddHeader("Token","123")
            .Build();
        Response<dynamic> response = chain.Proceed(request);
        return response;
    }
}