using Retrofit.Net.Core.Converts;
using Retrofit.Net.Core.Test.Interceptors;
using System.Diagnostics;

namespace Retrofit.Net.Core.Test
{
    public class PetSrviceTest
    {
        readonly Retrofit? _retrofit;
        public PetSrviceTest()
        {
            var client = new RetrofitClient.Builder()
                .AddInterceptor(new HeaderInterceptor())
                .AddInterceptor(new SimpleInterceptorDemo())
                .AddTimeout(TimeSpan.FromSeconds(10)) // The default wait time after making an http request is 6 seconds
                .Build();
            _retrofit = new Retrofit.Builder()
                .AddBaseUrl("https://localhost:7283") // Base Url
                .AddClient(client)
                .AddConverter(new DefaultXmlConverter()) // The internal default is ¡®DefaultJsonConverter¡¯ if you don¡¯t call ¡®.AddConverter(new DefaultJsonConverter())¡¯
                .Build();
        }

        [Fact]
        public void GetBaiduHomeTest()
        {
            var service = _retrofit!.Create<IPetService>();
            var s = service.GetBaiduHome();
            s.IsSuccessful.Equals(true);
        }

        [Fact]
        public async void FindPetsTest()
        {
            var service = _retrofit!.Create<IPetService>();
            var resp = await service.FindPets(121, "red");

            resp.IsSuccessful.Equals(false);
        }
    }
}