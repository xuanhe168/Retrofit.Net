using Castle.Core.Internal;
using Retrofit.Net.Core.Converts;
using Retrofit.Net.Core.Test.Interceptors;

namespace Retrofit.Net.Core.Test;

public class WechatServiceTest
{
    readonly Retrofit? _retrofit;

    public WechatServiceTest()
    {
        var client = new RetrofitClient.Builder()
            .AddInterceptor(new HeaderInterceptor())
            .AddInterceptor(new SimpleInterceptorDemo())
            .AddTimeout(TimeSpan.FromSeconds(10)) // The default wait time after making an http request is 6 seconds
            .Build();
        _retrofit = new Retrofit.Builder()
            .AddBaseUrl("https://localhost:7283") // Base Url
            .AddClient(client)
            .AddConverter(new DefaultXmlConverter()) // The internal default is ‘DefaultJsonConverter’ if you don’t call ‘.AddConverter(new DefaultJsonConverter())’
            .Build();
    }

    [Fact]
    public async void GetAccessToken()
    {
        var service = _retrofit!.Create<IWechatService>();
        var s = await service.GetMiniAccessToken("wx8c5bb9b8c1475e6f", "25b1b582857bf746b4bf3e82de427a7d");

        var a = 10;
    }
}
