using ExampleConsole.Models;
using Retrofit.Net.Core.Interceptors;
using Retrofit.Net.Core.Models;

namespace ExampleConsole;

public abstract class BaseInterceptor : IInterceptor
{
    private string? _token = null;

    protected TokenModel? OnGetToken()
    {
        return null;
    }

    protected Dictionary<string, string>? OnGetHeaders()
    {
        return new Dictionary<string, string>();
    }
    
    public Response<dynamic> Intercept(IChain chain)
    {
        return null;
    }
}