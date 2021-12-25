using Castle.DynamicProxy;

namespace Retrofit.Net.Core
{
    public class Retrofit
    {
        static readonly ProxyGenerator _generator = new ProxyGenerator();

        public string _BaseUrl { get; private set; }
        RetrofitClient _client { get; set; }

        public Retrofit BaseUrl(string url)
        {
            _BaseUrl = url;
            return this;
        }

        public Retrofit Client(RetrofitClient client)
        {
            _client = client;
            return this;
        }

        public Retrofit Build()
        {
            return this;
        }

        public T Create<T>()where T : class
        {
            return _generator.CreateInterfaceProxyWithoutTarget<T>(new RestInterceptor(this));
        }

        public static Retrofit Builder()
        {
            return new Retrofit();
        }
    }
}
