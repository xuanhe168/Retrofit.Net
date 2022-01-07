using Retrofit.Net.Core.Interceptors;
using Retrofit.Net.Core.Models;
using System.Diagnostics;

namespace ExampleConsole
{
    public class SimpleInterceptorDemo : ISimpleInterceptor
    {
        public void OnRequest(Request request)
        {
            Debug.WriteLine($"REQUEST[{request.Method}] => PATH: {request.Path}");
        }

        public void OnResponse(Response<dynamic> response)
        {
            Debug.WriteLine($"RESPONSE[{response.StatusCode}] => Message: {response.Message}");
        }
    }
}