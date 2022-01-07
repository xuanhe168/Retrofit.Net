using Retrofit.Net.Core.Interceptors;
using Retrofit.Net.Core.Models;

namespace ExampleConsole;

public class HeaderInterceptor : IAdvancedInterceptor
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
        if(response.StatusCode == 401)
        {
            // Get a new token and return
            // The way to get the new token here depends on you,
            // you can ask the backend to write an API to refresh the token
            request = chain.Request().NewBuilder()
                .AddHeader("Authorization", $"Bearer <new token>")
                .Build();
            // relaunch!
            response = chain.Proceed(request);
        }
        return response;
    }
}
