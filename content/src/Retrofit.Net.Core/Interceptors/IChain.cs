using Retrofit.Net.Core.Models;

namespace Retrofit.Net.Core.Interceptors;

public interface IChain
{
    Request Request();
    Response<dynamic> Proceed(Request request);
}