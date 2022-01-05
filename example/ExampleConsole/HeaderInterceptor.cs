using Retrofit.Net.Core.Interceptors;
using Retrofit.Net.Core.Models;

namespace ExampleConsole;

public class HeaderInterceptor : IInterceptor
{
    public Response<dynamic> Intercept(IChain chain)
    {
        // Get token from local file system
        string? token = null;
        if(File.Exists("token.txt"))token = File.ReadAllText("token.txt");

        // Add token above
        Request request = chain.Request().NewBuilder()
            .AddHeader("Authorization", $"Bearer {token}")
            .Build();

        Response<dynamic> response = chain.Proceed(request);


        return response;
    }
}